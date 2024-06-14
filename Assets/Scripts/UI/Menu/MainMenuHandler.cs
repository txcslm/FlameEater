using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
	public class MainMenuHandler : MonoBehaviour
	{
		[SerializeField] private Button _playButton;
		[SerializeField] private Button _loadLevelsButton;
		[SerializeField] private Button _optionsButton;
		[SerializeField] private CanvasGroup _optionsCanvasGroup;
		[SerializeField] private CanvasGroup _loadLevelsCanvasGroup;
		[SerializeField] private CanvasGroup _mainMenuCanvasGroup;

		public event Action PlayButtonClicked;

		private void OnEnable()
		{
			_playButton.onClick.AddListener(OnPlayButtonClick);
			_loadLevelsButton.onClick.AddListener(OnLoadLevelsButtonClick);
			_optionsButton.onClick.AddListener(OnOptionsButtonClick);
		}

		private void OnDisable()
		{
			_playButton.onClick.RemoveListener(OnPlayButtonClick);
			_loadLevelsButton.onClick.RemoveListener(OnLoadLevelsButtonClick);
			_optionsButton.onClick.RemoveListener(OnOptionsButtonClick);
		}


		private void OnPlayButtonClick()
		{
			Debug.Log("Play");
			LoadLevel();
		}

		private void OnLoadLevelsButtonClick()
		{
			Debug.Log("LoadLevels");
			ShowLoadLevelsMenu();
		}

		private void OnOptionsButtonClick()
		{
			Debug.Log("Options");
			ShowOptionMenu();
		}

		private void LoadLevel()
		{
			HideMainMenu();
			PlayButtonClicked?.Invoke();
		}

		private void ShowOptionMenu()
		{
			_optionsCanvasGroup.gameObject.GetComponent<Canvas>().enabled = true;
			_optionsCanvasGroup.alpha = 1f;
			HideMainMenu();
		}

		private void ShowLoadLevelsMenu()
		{
			_loadLevelsCanvasGroup.gameObject.GetComponent<Canvas>().enabled = true;
			_loadLevelsCanvasGroup.alpha = 1f;
			HideMainMenu();
		}

		private void ShowMainMenu()
		{
			_mainMenuCanvasGroup.gameObject.GetComponent<Canvas>().enabled = true;
			_mainMenuCanvasGroup.alpha = 1f;
		}

		private void HideLoadLevelsMenu()
		{
			_loadLevelsCanvasGroup.alpha = 0f;
			_loadLevelsCanvasGroup.gameObject.GetComponent<Canvas>().enabled = false;
		}

		private void HideOptionMenu()
		{
			_optionsCanvasGroup.alpha = 0f;
			_optionsCanvasGroup.gameObject.GetComponent<Canvas>().enabled = false;
		}

		private void HideMainMenu()
		{
			_mainMenuCanvasGroup.alpha = 0f;
			_mainMenuCanvasGroup.gameObject.GetComponent<Canvas>().enabled = false;
		}


	}
}