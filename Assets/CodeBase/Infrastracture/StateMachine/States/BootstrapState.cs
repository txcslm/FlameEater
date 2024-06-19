using CodeBase.Infrastracture.AssetManagement;
using CodeBase.Infrastracture.Factories;
using CodeBase.Infrastracture.Factories.Interfaces;
using CodeBase.Infrastracture.Services;
using CodeBase.Infrastracture.Services.Bending;
using CodeBase.Infrastracture.Services.Input;
using CodeBase.Infrastracture.Services.Interfaces;
using CodeBase.Infrastracture.Services.LoadLevels.Interfaces;
using CodeBase.Infrastracture.Services.PersistentProgress;
using CodeBase.Infrastracture.Services.SaveLoad;
using CodeBase.Infrastracture.StateMachine.Interfaces;
using UnityEngine;

namespace CodeBase.Infrastracture.StateMachine.States
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