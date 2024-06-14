using System;
using Factories.Interfaces;
using Services.LoadLevels.Interfaces;
using StateMachine.Interfaces;
using UI;
using UI.Menu;

namespace StateMachine
{
	public class LoadMainMenuState : IState
	{
		private readonly GameStateMachine _gameStateMachine;
		private readonly CanvasGroupViewer _curtain;
		private readonly IGameFactory _factory;
		
		private MainMenuHandler _mainMenuHandler;

		public LoadMainMenuState(GameStateMachine gameStateMachine, CanvasGroupViewer curtain, IGameFactory factory)
		{
			_gameStateMachine = gameStateMachine;
			_curtain = curtain;
			_factory = factory;
		}

		public void Enter()
		{			
			_curtain.Show();
			_factory.CleanUp();
			_mainMenuHandler = _factory.CreateMenu().GetComponent<MainMenuHandler>();
			_curtain.Hide();
			_mainMenuHandler.PlayButtonClicked += OnLoaded;
		}

		public void Exit()
		{
			_mainMenuHandler.PlayButtonClicked -= OnLoaded;
		}

		private void OnLoaded() =>
			_gameStateMachine.Enter<LoadProgressState>();
	}
}