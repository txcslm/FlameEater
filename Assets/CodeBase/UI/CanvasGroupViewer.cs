using System.Collections;
using CodeBase.UI.Interfaces;
using TMPro;
using UnityEngine;

namespace CodeBase.UI
{
	[RequireComponent(typeof(CanvasGroup))]
	public class CanvasGroupViewer : MonoBehaviour, IStateViewer
	{
		private const float FadeTime = 0.03f;
		
		private readonly WaitForSeconds _wait = new WaitForSeconds(FadeTime);
		
		[SerializeField] private CanvasGroup _canvasGroup;
		[SerializeField] private TextMeshProUGUI _text;

		private void Awake()
		{
			DontDestroyOnLoad(this);
		}

		public void Show()
		{
			gameObject.SetActive(true);
			_canvasGroup.alpha = 1f;
		}

		public void Hide()
		{
			StartCoroutine(FadeIn());
		}

		private IEnumerator FadeIn()
		{
			while (_canvasGroup.alpha > 0)
			{
				_canvasGroup.alpha -= FadeTime;
				yield return _wait;
				
			}
			gameObject.SetActive(false);
		}
	}
}