using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
	public class DeathScreen : MonoBehaviour
	{
		[SerializeField] private Button _restartButton;
		[SerializeField] private Button _exitButton;
        
		private Canvas _canvas;

		public event Action<string> RestartSceneLoaded;
		public event Action MainMenuSceneLoaded;

		private void OnEnable()
		{
			_restartButton.onClick.AddListener(ReloadScene);
			_exitButton.onClick.AddListener(ExitScene);
		}

		private void OnDisable()
		{
			_restartButton.onClick.RemoveListener(ReloadScene);
			_exitButton.onClick.RemoveListener(ExitScene);
		}

		public void Initialize()
		{
			_canvas = GetComponent<Canvas>();
			_canvas.enabled = false;
		}

		public void Show() =>
			_canvas.enabled = true;

		private void ExitScene() =>
			MainMenuSceneLoaded?.Invoke();

		private void ReloadScene() =>
			RestartSceneLoaded?.Invoke(SceneManager.GetActiveScene().name);
	}
}