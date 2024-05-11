using Services.LoadLevels;
using StateMachine.Interfaces;
using UI;
using UI.Interfaces;
using Utility;

namespace StateMachine
{
	public class GameStateMachine
	{
		private readonly IState _loadSceneState;

		public GameStateMachine(IStateViewer loadCurtain, ICoroutineRunner coroutineRunner)
		{
			IState gameBootstrap = new GameBootstrapState();
			_loadSceneState = new LoadSceneState(loadCurtain, new UnityLoadSceneService(coroutineRunner), gameBootstrap);
		}

		public void Start()
		{
			_loadSceneState.Enter();
		}
	}
}