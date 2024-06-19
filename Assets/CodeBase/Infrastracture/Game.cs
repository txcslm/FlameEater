using CodeBase.Infrastracture.Services;
using CodeBase.Infrastracture.Services.LoadLevels;
using CodeBase.Infrastracture.StateMachine;
using CodeBase.UI;
using CodeBase.Utility;

namespace CodeBase.Infrastracture
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