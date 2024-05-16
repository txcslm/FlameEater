using StateMachine;
using UI;
using Utility;

public class Game
{
	private readonly GameStateMachine _stateMachine;

	public Game(CanvasGroupViewer canvasGroupViewer, ICoroutineRunner coroutineRunner)
	{
		_stateMachine = new GameStateMachine(canvasGroupViewer, coroutineRunner);
	}

	public void Start()
	{
		_stateMachine.Start();
	}
}