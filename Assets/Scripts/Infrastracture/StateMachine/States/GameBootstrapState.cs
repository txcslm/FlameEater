using StateMachine.Interfaces;
using UnityEngine;

namespace StateMachine
{
	public class GameBootstrapState : IState
	{
		public void Enter()
		{
			Debug.LogWarning("Entering GameBootstrapState");
		}

		public void Exit()
		{
		}
	}
}