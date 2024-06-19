using System;
using System.Collections.Generic;
using Factories.Interfaces;
using Services;
using Services.Interfaces;
using Services.LoadLevels.Interfaces;
using Services.SaveLoad;
using StateMachine.Interfaces;
using UI;
using UnityEngine;

namespace StateMachine
{
	public class GameStateMachine
	{
		private readonly Dictionary<Type,IExitState> _states;
		private IExitState _currentState;
		

		public GameStateMachine(ILoadSceneService loadSceneService, CanvasGroupViewer curtain, AllServices services)
		{
			_states = new Dictionary<Type, IExitState>
			{
				[typeof(BootstrapState)] = new BootstrapState(this, loadSceneService, services),
				[typeof(LoadLevelState)] = new LoadLevelState(this, loadSceneService, curtain, services.Single<IGameFactory>(), services.Single<IPersistentProgressService>()),
				[typeof(GameLoopState)] = new GameLoopState(this, services.Single<IGameFactory>(), loadSceneService),
				[typeof(LoadProgressState)] = new LoadProgressState(this, services.Single<IPersistentProgressService>(), services.Single<ISaveLoadService>()),
				[typeof(LoadMainMenuState)] = new LoadMainMenuState(this, curtain,  services.Single<IGameFactory>(), loadSceneService)
			};
		}
		public void Enter<TState>() where TState : class, IState
		{
			IState state = ChangeState<TState>();
			state.Enter();
		}

		public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>
		{
			TState state = ChangeState<TState>();
			state.Enter(payload);

		}

		private TState ChangeState<TState>() where TState : class, IExitState
		{
			_currentState?.Exit();

			TState state = GetState<TState>();
			_currentState = state;
			Debug.Log($"Enter in {typeof(TState)}");

			return state;
		}

		private TState GetState<TState>() where TState : class, IExitState =>
			_states[typeof(TState)] as TState;
	}
}
