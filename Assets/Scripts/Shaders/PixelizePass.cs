using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Shaders
{
    public class PixelizePass : ScriptableRenderPass
    {
        private readonly static int BlockCount = Shader.PropertyToID("_BlockCount");
        private readonly static int BlockSize = Shader.PropertyToID("_BlockSize");
        private readonly static int HalfBlockSize = Shader.PropertyToID("_HalfBlockSize");
    
        private readonly int _pixelBufferID = Shader.PropertyToID("_PixelBuffer");
        private readonly PixelizeFeature.CustomPassSettings _settings;
        private readonly Material _material;
    
        private RenderTargetIdentifier _colorBuffer;
        private RenderTargetIdentifier _pixelBuffer;
        private int _pixelScreenHeight;
        private int _pixelScreenWidth;

        public PixelizePass(PixelizeFeature.CustomPassSettings settings)
        {
            _settings = settings;
            renderPassEvent = settings.renderPassEvent;
            _material = CoreUtils.CreateEngineMaterial("Hidden/Pixelize");
        }

        public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
        {
            _colorBuffer = renderingData.cameraData.renderer.cameraColorTargetHandle;

            RenderTextureDescriptor descriptor = renderingData.cameraData.cameraTargetDescriptor;

            _pixelScreenHeight = _settings.screenHeight;
            _pixelScreenWidth = (int)(_pixelScreenHeight * renderingData.cameraData.camera.aspect + 0.5f);

            _material.SetVector(BlockCount, new Vector2(_pixelScreenWidth, _pixelScreenHeight));
            _material.SetVector(BlockSize, new Vector2(1.0f / _pixelScreenWidth, 1.0f / _pixelScreenHeight));
            _material.SetVector(HalfBlockSize, new Vector2(0.5f / _pixelScreenWidth, 0.5f / _pixelScreenHeight));

            descriptor.height = _pixelScreenHeight;
            descriptor.width = _pixelScreenWidth;

            cmd.GetTemporaryRT(_pixelBufferID, descriptor, FilterMode.Point);
            _pixelBuffer = new RenderTargetIdentifier(_pixelBufferID);
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer cmd = CommandBufferPool.Get();
            using (new ProfilingScope(cmd, new ProfilingSampler("Pixelize Pass")))
            {
                // Assuming colorBuffer and pixelBuffer are of type RenderTargetIdentifier
                cmd.Blit(_colorBuffer, _pixelBuffer, _material, 0);
                cmd.Blit(_pixelBuffer, _colorBuffer);
            }

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        public override void OnCameraCleanup(CommandBuffer cmd)
        {
            if (cmd == null) throw new System.ArgumentNullException(nameof(cmd));
            cmd.ReleaseTemporaryRT(_pixelBufferID);
        }
    }
}