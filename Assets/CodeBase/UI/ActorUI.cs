using System;
using GameLogic.CharacterLogic.Health;
using GameLogic.CharacterLogic.Timers;
using UnityEngine;

namespace UI
{
	public class ActorUI : MonoBehaviour
	{
		[SerializeField] private DieTimer _dieTimer;

		private CharacterHealth _characterHealth;
		
	}
}