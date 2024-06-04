Shader "Hidden/Pixelation"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque" "RenderPipeline" = "UniversalPipeline"
        }

        HLSLINCLUDE
        #pragma vertex vert
        #pragma fragment frag

        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

        struct Attributes
        {
            float4 positionOS : POSITION;
            float2 uv : TEXCOORD0;
        };

        struct Varyings
        {
            float4 positionHCS : SV_POSITION;
            float2 uv : TEXCOORD0;
        };

        TEXTURE2D(_MainTex);
        float4 _MainTex_TexelSize;
        float4 _MainTex_ST;

        //SAMPLER(sampler_MainTex);
        //Texture2D _MainTex;
        //SamplerState sampler_MainTex;

        SamplerState sampler_point_clamp;
        
        uniform float2 _BlockCount;
        uniform float2 _BlockSize;
        uniform float2 _HalfBlockSize;


        Varyings vert(Attributes IN)
        {
            Varyings OUT;
            OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
            OUT.uv = TRANSFORM_TEX(IN.uv, _MainTex);
            return OUT;
        }

        half4 frag(Varyings IN) : SV_TARGET
            {
                float2 block_pos = floor(IN.uv * _BlockCount);
                float2 block_center = block_pos * _BlockSize + _HalfBlockSize;

                float4 tex = SAMPLE_TEXTURE2D(_MainTex, sampler_point_clamp, block_center);
                //return float4(IN.uv,1,1);

                return tex;
            }

        ENDHLSL

        Pass
        {
            Name "Pixelation"

            HLSLPROGRAM
            
            ENDHLSL
        }
    }
}
