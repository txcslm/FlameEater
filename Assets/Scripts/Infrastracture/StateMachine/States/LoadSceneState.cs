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
			Debug.Log("Entering LoadSceneState");
			_stateViewer.Enable();
			_loadSceneService.Load(MainMenu, Exit);
		}

		public void Exit()
		{
			Debug.Log("Exiting LoadSceneState");
			_stateViewer.Disable();
			_nextState.Enter();
		}
	}
	
}