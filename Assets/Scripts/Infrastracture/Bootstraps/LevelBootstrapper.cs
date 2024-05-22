using System.Collections.Generic;
using GameLogic.CharacterLogic;
using GameLogic.CharacterLogic.Handlers;
using Shaders;
using UnityEngine;

namespace Bootstraps
{
	public class LevelBootstrapper : MonoBehaviour
	{
		[SerializeField] private Character _character;
		[SerializeField] private BendingManager _bendingManager;
		[SerializeField] private List<TriggerHandler> _triggerHandlers;
		[SerializeField] private DieHandler _dieHandler;

		private void Awake()
		{
			DontDestroyOnLoad(this);
			Time.timeScale = 1.0f;

			_character.Initialize();
			_bendingManager.Initialize();
			_dieHandler.Initialize();

			foreach (TriggerHandler triggerHandler in _triggerHandlers)
				triggerHandler.Initialize();
		}
	}
}