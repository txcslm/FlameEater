using AssetManagement;
using Factories;
using Factories.Interfaces;
using Services;
using Services.Bending;
using Services.Input;
using Services.Interfaces;
using Services.LoadLevels.Interfaces;
using Services.PersistentProgress;
using Services.SaveLoad;
using Shaders;
using StateMachine.Interfaces;
using UnityEngine;

namespace StateMachine
{
	public class BootstrapState : IState
	{
		private const string MainMenuSceneName = "MainMenu";

		private readonly ILoadSceneService _loadSceneService;
		private readonly GameStateMachine _stateMachine;
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
			_loadSceneService.Load(MainMenuSceneName, sceneLoaded: EnterLoadMainMenu);
		}

		public void Exit()
		{
			
		}

		private void EnterLoadMainMenu() =>
			_stateMachine.Enter<LoadMainMenuState>();

		private void RegisterServices()
		{
			_services.RegisterAsSingle<IInputService>(RegisterInputService());
			_services.RegisterAsSingle<IAssetProvider>(new AssetProvider());
			_services.RegisterAsSingle<IPersistentProgressService>(new PersistentProgressService());
			_services.RegisterAsSingle<IGameFactory>(new GameFactory(_services.Single<IAssetProvider>()));
			_services.RegisterAsSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IPersistentProgressService>(), _services.Single<IGameFactory>()));
			_services.RegisterAsSingle<IBendingService>(new BendingService());
		}

		private static IInputService RegisterInputService()
		{
			if(Application.isEditor)
				return  new StandaloneInputService();
			
			return new MobileInputService();
		}
	}
}