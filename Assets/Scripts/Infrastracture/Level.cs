using GameLogic.CharacterLogic;
using GameLogic.CharacterLogic.Handlers;
using GameLogic.CharacterLogic.Timers;
using GameLogic.Environments.Initializer;
using Shaders;
using UI;
using UI.Score;
using UnityEngine;

public class Level
{
	private readonly Character _character;
	private readonly BendingService _bendingService;
	private readonly DieHandler _dieHandler;
	private readonly DeathScreen _deathScreen;
	private readonly SpawnerInitializer _spawnerInitializer;
	private readonly ScoreView _scoreView;
	private readonly DieTimer _dieTimer;

	private readonly ScoreHandler _scoreHandler;

	public Level(Character character, BendingService bendingService, DieHandler dieHandler, DeathScreen deathScreen,
		SpawnerInitializer spawnerInitializer, ScoreView scoreView, DieTimer dieTimer)
	{
		_bendingService = bendingService;
		_deathScreen = deathScreen;
		_spawnerInitializer = spawnerInitializer;
		_character = character;
		_dieHandler = dieHandler;
		_scoreView = scoreView;
		_dieTimer = dieTimer;

		_scoreHandler = new ScoreHandler();
	}

	public void Start()
	{
		_scoreHandler.Reset();

		Time.timeScale = 1.0f;
		_bendingService.Initialize();
		_spawnerInitializer.Initialize();
		_dieHandler.Initialize();
		_deathScreen.Initialize();
		_scoreView.Initialize(_scoreHandler);
		_dieTimer.Initialize();
	}
}