using CodeBase.GameLogic.CharacterLogic.Handlers;
using CodeBase.GameLogic.CharacterLogic.Timers;
using UnityEngine;

namespace CodeBase.GameLogic.CharacterLogic
{
	public class Character : MonoBehaviour
	{
		[SerializeField] private DieTimer _dieTimer;
		[SerializeField] private DieHandler _dieHandler;

		private void Awake()
		{
			_dieHandler.Initialize();
			_dieTimer.Initialize();
		}
	}
}