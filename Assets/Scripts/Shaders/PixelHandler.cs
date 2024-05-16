using UnityEngine;

namespace Shaders
{
	[ExecuteInEditMode]
	public class PixelHandler : MonoBehaviour
	{
		[SerializeField] private Material _effectMaterial;

		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			Graphics.Blit(src, dest, _effectMaterial);
		}
	}
}
