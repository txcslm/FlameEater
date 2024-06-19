using Services;
using Services.Interfaces;
using Services.LoadLevels;
using StateMachine;
using UI;
using Utility;

public class Game
{
	public GameStateMachine StateMachine { get; }

	public Game(ICoroutineRunner coroutineRunner, CanvasGroupViewer curtain)
	{
		StateMachine = new GameStateMachine(new UnityLoadSceneService(coroutineRunner), curtain, AllServices.Container);
		
	}
}