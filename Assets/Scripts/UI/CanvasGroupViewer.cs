using UI.Interfaces;
using UnityEngine;

namespace UI
{
	[RequireComponent(typeof(CanvasGroup))]
	public class CanvasGroupViewer : MonoBehaviour, IStateViewer
	{
		[SerializeField] private CanvasGroup _canvasGroup;

		public void Enable() =>
			_canvasGroup.alpha = 1f;

		public void Disable() =>
			_canvasGroup.alpha = 0f;
	}
}