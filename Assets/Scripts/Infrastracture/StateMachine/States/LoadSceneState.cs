using System;
using Services.LoadLevels.Interfaces;
using StateMachine.Interfaces;
using UI.Interfaces;
using UnityEngine;

namespace StateMachine
{
	public class LoadSceneState : IState
	{
		private const string MainMenu = "MainMenu";
		
		private readonly IStateViewer _stateViewer;
		private readonly ILoadSceneService _loadSceneService;
		private readonly IState _nextState;

		public LoadSceneState(IStateViewer stateViewer, ILoadSceneService loadSceneService, IState nextState)
		{
			_stateViewer = stateViewer;
			_loadSceneService = loadSceneService;
			_nextState = nextState;
		}

		public void Enter()
		{
			_stateViewer.Enable();
			_loadSceneService.Load(MainMenu, Exit);
		}

		public void Exit()
		{
			_stateViewer.Disable();
			_nextState.Enter();
		}
	}
	
}