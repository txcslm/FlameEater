using CodeBase.Infrastructure.Factories.Interfaces;
using CodeBase.Infrastructure.Services.LoadLevels.Interfaces;
using CodeBase.Infrastructure.StateMachine.Interfaces;
using CodeBase.UI;
using CodeBase.UI.Menu;
using UnityEngine;

namespace CodeBase.Infrastructure.StateMachine.States
{
	public class LoadMainMenuState : IState
	{
		private const string LevelSceneName = "Level";
		
		private readonly GameStateMachine _gameStateMachine;
		private readonly CanvasGroupViewer _curtain;
		private readonly IGameFactory _factory;
		private readonly ILoadSceneService _loadSceneService;
		
		private MainMenuHandler _mainMenuHandler;

		public LoadMainMenuState(GameStateMachine gameStateMachine, CanvasGroupViewer curtain, IGameFactory factory, ILoadSceneService loadSceneService)
		{
			_gameStateMachine = gameStateMachine;
			_curtain = curtain;
			_factory = factory;
			_loadSceneService = loadSceneService;
		}

		public void Enter()
		{
			Time.timeScale = 1f;
			_curtain.Show();
			_factory.CleanUp();
			_mainMenuHandler = _factory.CreateMenu().GetComponent<MainMenuHandler>();
			_curtain.Hide();
			_mainMenuHandler.PlayButtonClicked += OnLoaded;
			
		}

		public void Exit() =>
			_mainMenuHandler.PlayButtonClicked -= OnLoaded;

		private void OnLoaded() =>
			_loadSceneService.Load(LevelSceneName, EnterLevelScene);

		private void EnterLevelScene() =>
			_gameStateMachine.Enter<LoadProgressState>();
	}
}