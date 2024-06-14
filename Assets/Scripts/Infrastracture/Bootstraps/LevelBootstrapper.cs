using Factories.Interfaces;
using GameLogic.CharacterLogic.Handlers;
using GameLogic.CharacterLogic.Timers;
using GameLogic.Environments.Initializer;
using Shaders;
using UI;
using UI.Score;
using UnityEngine;

namespace Bootstraps
{
	public class LevelBootstrapper : MonoBehaviour
	{
		[SerializeField] private BendingService _bendingService;
		[SerializeField] private SpawnerInitializer _spawnerInitializer;
		
		private Level _level;

		private void Awake()
		{
			_level = new Level(_bendingService, _spawnerInitializer);
			_level.Start();
		}
	}
}