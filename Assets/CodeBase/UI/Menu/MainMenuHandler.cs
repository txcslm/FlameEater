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
		[SerializeField] private Button _backToMenuButton;
		[SerializeField] private CanvasGroup _optionsCanvasGroup;
		[SerializeField] private CanvasGroup _loadLevelsCanvasGroup;
		[SerializeField] private CanvasGroup _mainMenuCanvasGroup;
		[SerializeField] private CanvasGroup _backToMenuCanvasGroup;

		private Canvas _loadLevelsCanvas;
		private Canvas _optionsCanvas;
		private Canvas _mainMenuCanvas;
		private Canvas _backToMenuCanvas;

		public event Action PlayButtonClicked;

		private void Awake()
		{
			_optionsCanvas = _optionsCanvasGroup.gameObject.GetComponent<Canvas>();
            _loadLevelsCanvas = _loadLevelsCanvasGroup.gameObject.GetComponent<Canvas>();
            _mainMenuCanvas = _mainMenuCanvasGroup.gameObject.GetComponent<Canvas>();
            _backToMenuCanvas = _backToMenuCanvasGroup.gameObject.GetComponent<Canvas>();

            HideBackToMenu();
            HideLoadLevelsMenu();
            HideOptionMenu();
		}

		private void OnEnable()
		{
			_playButton.onClick.AddListener(OnPlayButtonClick);
			_loadLevelsButton.onClick.AddListener(OnLoadLevelsButtonClick);
			_optionsButton.onClick.AddListener(OnOptionsButtonClick);
			_backToMenuButton.onClick.AddListener(OnBackToMenuButtonClick);
		}

		private void OnDisable()
		{
			_playButton.onClick.RemoveListener(OnPlayButtonClick);
			_loadLevelsButton.onClick.RemoveListener(OnLoadLevelsButtonClick);
			_optionsButton.onClick.RemoveListener(OnOptionsButtonClick);
			_backToMenuButton.onClick.RemoveListener(OnBackToMenuButtonClick);
		}

		private void OnBackToMenuButtonClick()
		{
			HideBackToMenu();
			HideLoadLevelsMenu();
			HideOptionMenu();
			ShowMainMenu();
		}

		private void OnPlayButtonClick() =>
			LoadLevel();

		private void OnLoadLevelsButtonClick()
		{
			ShowLoadLevelsMenu();
			HideMainMenu();
			ShowBackToMenu();
		}

		private void OnOptionsButtonClick()
		{
			ShowOptionMenu();
			HideMainMenu();
			ShowBackToMenu();
		}

		private void LoadLevel()
		{
			HideMainMenu();
			PlayButtonClicked?.Invoke();
		}

		private void ShowOptionMenu()
		{
			_optionsCanvas.enabled = true;
			_optionsCanvasGroup.alpha = 1f;

		}

		private void ShowLoadLevelsMenu()
		{
			_loadLevelsCanvas.enabled = true;
			_loadLevelsCanvasGroup.alpha = 1f;
		}

		private void ShowMainMenu()
		{
			_mainMenuCanvas.enabled = true;
			_mainMenuCanvasGroup.alpha = 1f;
		}

		private void ShowBackToMenu()
		{
			_backToMenuCanvasGroup.alpha = 1f;
			_backToMenuCanvas.enabled = true;
		}

		private void HideBackToMenu()
		{
			_backToMenuCanvasGroup.alpha = 0f;
			_backToMenuCanvas.enabled = false;
		}

		private void HideLoadLevelsMenu()
		{
			_loadLevelsCanvasGroup.alpha = 0f;
			_loadLevelsCanvas.enabled = false;
		}

		private void HideOptionMenu()
		{
			_optionsCanvasGroup.alpha = 0f;
			_optionsCanvas.enabled = false;
		}

		private void HideMainMenu()
		{
			_mainMenuCanvasGroup.alpha = 0f;
			_mainMenuCanvas.enabled = false;
		}
	}
}