namespace StateMachine.Interfaces
{
	public interface IState : IExitState
	{
		void Enter();
	}
}