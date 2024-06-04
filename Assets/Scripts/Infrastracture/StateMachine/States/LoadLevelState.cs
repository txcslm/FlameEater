using Factories.Interfaces;
using Services.Interfaces;
using Services.LoadLevels.Interfaces;
using StateMachine.Interfaces;
using UI;
using UnityEngine;

namespace StateMachine
{
	public class LoadLevelState : IPayloadState<string>
	{
		private const string InitialPointTag = "InitialPoint";

		private readonly GameStateMachine _stateMachine;
		private readonly ILoadSceneService _loadSceneService;
		private readonly CanvasGroupViewer _curtain;
		private readonly IGameFactory _factory;
		private readonly IPersistentProgressService _progressService;

		public LoadLevelState(GameStateMachine stateMachine, ILoadSceneService loadSceneService, CanvasGroupViewer curtain, IGameFactory factory, IPersistentProgressService progressService)
		{
			_stateMachine = stateMachine;
			_loadSceneService = loadSceneService;
			_curtain = curtain;
			_factory = factory;
			_progressService = progressService;
		}

		public void Enter(string sceneName)
		{
			_curtain.Show();
			_factory.CleanUp();
			_loadSceneService.Load(sceneName, OnLoaded);
		}

		public void Exit() =>
			_curtain.Hide();

		private void OnLoaded()
		{
			InitGameWorld();
			InformProgressReaders();
			
			_stateMachine.Enter<GameLoopState>();
		}

		private void InformProgressReaders()
		{
			foreach(ISavedProgressReader progressReader in _factory.ProgressReaders)
				progressReader.LoadProgress(_progressService.Progress);
		}

		private void InitGameWorld()
		{
			_factory.CreateCharacter(at: GameObject.FindWithTag(InitialPointTag));

			_factory.CreateHud();
		}
	}
}