namespace CodeBase.Infrastructure.StateMachine.Interfaces
{
	public interface IState : IExitState
	{
		void Enter();
	}
}