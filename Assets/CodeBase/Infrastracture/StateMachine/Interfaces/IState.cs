namespace CodeBase.Infrastracture.StateMachine.Interfaces
{
	public interface IState : IExitState
	{
		void Enter();
	}
}