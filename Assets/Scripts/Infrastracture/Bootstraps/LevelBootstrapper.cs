using GameLogic.CharacterLogic;
using GameLogic.CharacterLogic.Handlers;
using GameLogic.Environments.Initializer;
using GameLogic.Spawners;
using Shaders;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Bootstraps
{
	public class LevelBootstrapper : MonoBehaviour
	{
		[SerializeField] private Character _character;
		[SerializeField] private BendingManager _bendingManager;
		[SerializeField] private DieHandler _dieHandler;
		[SerializeField] private DeathScreen _deathScreen;
		[SerializeField] private SpawnerInitializer _spawnerInitializer;

		private void Awake()
		{
			Time.timeScale = 1.0f;

			_bendingManager.Initialize();
			_character.Initialize();
			_spawnerInitializer.Initialize();
			_dieHandler.Initialize();
			_deathScreen.Initialize();
			DontDestroyOnLoad(this);
		}
	}
}