using GameLogic.CharacterLogic.Handlers;
using GameLogic.CharacterLogic.Timers;
using UI;
using UnityEngine;

namespace GameLogic.CharacterLogic
{
	public class Character : MonoBehaviour
	{
		[SerializeField] private DieTimer _dieTimer;
		[SerializeField] private DieHandler _dieHandler;

		private void Awake()
		{
			_dieTimer.Initialize();
			_dieHandler.Initialize();
		}
	}
}