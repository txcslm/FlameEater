using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.LoadLevels;
using CodeBase.Infrastructure.StateMachine;
using CodeBase.UI;
using CodeBase.Utility;

namespace CodeBase.Infrastructure
{
	public class Game
	{
		public GameStateMachine StateMachine { get; }

		public Game(ICoroutineRunner coroutineRunner, CanvasGroupViewer curtain)
		{
			StateMachine = new GameStateMachine(new UnityLoadSceneService(coroutineRunner), curtain, AllServices.Container);
		
		}
	}
}