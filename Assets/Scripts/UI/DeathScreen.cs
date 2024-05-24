using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
	public class DeathScreen : MonoBehaviour
	{
		[SerializeField] private Button _restartButton;
		[SerializeField] private Button _exitButton;
		[SerializeField] private int _sceneIndex;

		private Image _image;
		private int _currentSceneIndex;

		public void Initialize()
		{
			_image = GetComponent<Image>();
			_currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		}
		
		private void OnEnable()
		{
			_restartButton.onClick.AddListener(ReloadScene);
			_exitButton.onClick.AddListener(Exit);
		}

		private void OnDisable()
		{
			_restartButton.onClick.RemoveListener(ReloadScene);
			_exitButton.onClick.RemoveListener(Exit);
		}

		public void Show()
		{
			gameObject.SetActive(true);
		}
		
		private void Exit() =>
			SceneManager.LoadScene(_sceneIndex);

		private void ReloadScene() =>
			SceneManager.LoadScene(_currentSceneIndex);
	}
}