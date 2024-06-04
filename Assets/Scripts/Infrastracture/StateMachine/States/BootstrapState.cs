using AssetManagement;
using Factories;
using Factories.Interfaces;
using Services;
using Services.Input;
using Services.Interfaces;
using Services.LoadLevels.Interfaces;
using Services.PersistentProgress;
using Services.SaveLoad;
using StateMachine.Interfaces;
using UnityEngine;

namespace StateMachine
{
	public class BootstrapState : IState
	{
		private const string MainMenuSceneName = "MainMenu";
		
		private readonly GameStateMachine _stateMachine;
		private readonly ILoadSceneService _loadSceneService;
		private readonly  AllServices _services;

		public BootstrapState(GameStateMachine stateMachine, ILoadSceneService loadSceneService, AllServices services)
		{
			_stateMachine = stateMachine;
			_loadSceneService = loadSceneService;
			_services = services;
			
			RegisterServices();
		}

		public void Enter()
		{
			_loadSceneService.Load(MainMenuSceneName, sceneLoaded: EnterLoadLevel);
		}

		public void Exit()
		{
			
		}

		private void EnterLoadLevel() =>
			_stateMachine.Enter<LoadProgressState>();

		private void RegisterServices()
		{
			_services.RegisterAsSingle<IInputService>(RegisterInputService());
			_services.RegisterAsSingle<IAssetProvider>(new AssetProvider());
			_services.RegisterAsSingle<IPersistentProgressService>(new PersistentProgressService());
			_services.RegisterAsSingle<IGameFactory>(new GameFactory(_services.Single<IAssetProvider>()));
			_services.RegisterAsSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IPersistentProgressService>(), _services.Single<IGameFactory>()));
		}

		private static IInputService RegisterInputService()
		{
			if(Application.isEditor)
				return  new StandaloneInputService();
			
			return new MobileInputService();
		}
	}
}