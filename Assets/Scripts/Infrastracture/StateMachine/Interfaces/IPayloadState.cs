namespace StateMachine.Interfaces
{
	public interface IPayloadState<in TPayload> : IExitState
	{
		void Enter(TPayload payload);
	}
}