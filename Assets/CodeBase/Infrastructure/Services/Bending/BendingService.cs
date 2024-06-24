using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace CodeBase.Infrastructure.Services.Bending
{
  [ExecuteAlways]
  [Serializable]
  public class BendingService : IBendingService
  {
    private const string BendingFeature = "ENABLE_BENDING";
    private const string PlanetFeature = "ENABLE_BENDING_PLANET";
    public bool EnablePlanet { get; set; }

    private void OnEnable()
    {
      if (Application.isPlaying == false)
        return;
    
      RenderPipelineManager.beginCameraRendering += OnBeginCameraRendering;
      RenderPipelineManager.endCameraRendering += OnEndCameraRendering;
    }

    private void OnDisable()
    {
      RenderPipelineManager.beginCameraRendering -= OnBeginCameraRendering;
      RenderPipelineManager.endCameraRendering -= OnEndCameraRendering;
      
      Shader.DisableKeyword(BendingFeature);
      Shader.DisableKeyword(PlanetFeature);
    }

    public void ManageBending()
    {
      if (Application.isPlaying)
        Shader.EnableKeyword(BendingFeature);
      else
        Shader.DisableKeyword(BendingFeature);
      
      if (EnablePlanet)
        Shader.EnableKeyword(PlanetFeature);
      else
        Shader.DisableKeyword(PlanetFeature);
    }

    private static void OnBeginCameraRendering(ScriptableRenderContext ctx, Camera cam)
    {
      cam.cullingMatrix = Matrix4x4.Ortho(-150, 150, -150, 150, 0.01f, 100) * cam.worldToCameraMatrix;
    }

    private static void OnEndCameraRendering (ScriptableRenderContext ctx, Camera cam) =>
      cam.ResetCullingMatrix();
  }
}