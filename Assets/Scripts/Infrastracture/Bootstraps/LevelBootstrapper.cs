using DG.Tweening;
using GameLogic.CharacterLogic;
using GameLogic.CharacterLogic.Handlers;
using GameLogic.CharacterLogic.Timers;
using GameLogic.Environments.Initializer;
using GameLogic.Spawners;
using Shaders;
using UI;
using UI.Score;
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
		[SerializeField] private ScoreView _scoreView;
		[SerializeField] private DieTimer _dieTimer;

		private ScoreHandler _scoreHandler;

		private void Awake()
		{
			DOTween.Init(); // Инициализируем DOTween
			DOTween.SetTweensCapacity(200000, 2000);
			
			_scoreHandler = new ScoreHandler();
			_scoreHandler.Reset();
			
			Time.timeScale = 1.0f;
			_bendingManager.Initialize();
			_character.Initialize();
			_spawnerInitializer.Initialize();
			_dieHandler.Initialize();
			_deathScreen.Initialize();
			_scoreView.Initialize(_scoreHandler);
			_dieTimer.Initialize();
			

			//DontDestroyOnLoad(this);
		}
	}
}