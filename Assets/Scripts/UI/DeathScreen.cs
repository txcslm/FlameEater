using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
	public class DeathScreen : MonoBehaviour
	{
		[SerializeField] private Button _restartButton;
		[SerializeField] private Button _exitButton;
		[SerializeField] private int _sceneIndex;

		private int _currentSceneIndex;

		public void Initialize()
		{
			GetComponent<Image>();
			_currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		}

		private void OnEnable()
		{
			_restartButton.onClick.AddListener(ReloadScene);
			_exitButton.onClick.AddListener(ExitScene);
		}

		public void Show() =>
			gameObject.SetActive(true);

		private void OnDisable()
		{
			_restartButton.onClick.RemoveListener(ReloadScene);
			_exitButton.onClick.RemoveListener(ExitScene);
		}

		private void ExitScene() =>
			SceneManager.LoadScene(_sceneIndex);

		private void ReloadScene()
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			Time.timeScale = 1.0f;
		}
	}
}