using CodeBase.Infrastructure.Factories.Interfaces;
using CodeBase.Infrastructure.Services.LoadLevels.Interfaces;
using CodeBase.Infrastructure.StateMachine.Interfaces;
using CodeBase.UI;

namespace CodeBase.Infrastructure.StateMachine.States
{
	public class GameLoopState : IState
	{
		private const string MainMenuSceneName = "MainMenu";

		private readonly GameStateMachine _stateMachine;
		private readonly IGameFactory _factory;
		private readonly ILoadSceneService _loadSceneService;

		private DeathScreen _deathScreen;

		public GameLoopState(GameStateMachine stateMachine, IGameFactory factory, ILoadSceneService loadSceneService)
		{
			_stateMachine = stateMachine;
			_factory = factory;
			_loadSceneService = loadSceneService;
		}

		public void Enter()
		{
			_factory.CleanUp();
			
			if (_factory.UIGameObject is null)
				_factory.UICreated += OnUICreated;
			else
				InitializeDeathScreen();

			_deathScreen.RestartSceneLoaded += OnRestartSceneLoaded;
			_deathScreen.MainMenuSceneLoaded += OnMainMenuSceneLoaded;
		}

		public void Exit()
		{
			_deathScreen.RestartSceneLoaded -= OnRestartSceneLoaded;
			_deathScreen.MainMenuSceneLoaded -= OnMainMenuSceneLoaded;
		}

		private void OnMainMenuSceneLoaded() =>
			_loadSceneService.Load(MainMenuSceneName, EnterLoadMainMenuState);

		private void OnRestartSceneLoaded(string sceneName) =>
			EnterLoadProgressState();

		private void OnUICreated() =>
			InitializeDeathScreen();

		private void EnterLoadProgressState() =>
			_stateMachine.Enter<LoadProgressState>();

		private void EnterLoadMainMenuState() =>
			_stateMachine.Enter<LoadMainMenuState>();

		private void InitializeDeathScreen() =>
			_deathScreen = _factory.UIGameObject.GetComponentInChildren<DeathScreen>();
	}
}