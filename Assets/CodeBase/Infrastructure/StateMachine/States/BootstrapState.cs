using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factories;
using CodeBase.Infrastructure.Factories.Interfaces;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Bending;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.Interfaces;
using CodeBase.Infrastructure.Services.LoadLevels.Interfaces;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.StateMachine.Interfaces;
using UnityEngine;

namespace CodeBase.Infrastructure.StateMachine.States
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
			if (Application.isMobilePlatform)
				return new MobileInputService();
			
			return new StandaloneInputService();
		}
	}
}