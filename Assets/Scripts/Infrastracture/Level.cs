using Factories.Interfaces;
using GameLogic.CharacterLogic;
using GameLogic.Environments.Initializer;
using Shaders;
using UI;
using UI.Score;
using UnityEngine;

public class Level
{
	private readonly BendingService _bendingService;
	private readonly SpawnerInitializer _spawnerInitializer;
	private readonly ScoreView _scoreView;
	private readonly DeathScreen _deathScreen;
	private readonly ScoreHandler _scoreHandler;

	public Level(BendingService bendingService,
		SpawnerInitializer spawnerInitializer,
		ScoreView scoreView, DeathScreen deathScreen)
	{
		_bendingService = bendingService;
		_spawnerInitializer = spawnerInitializer;
		_scoreView = scoreView;
		_deathScreen = deathScreen;

		_scoreHandler = new ScoreHandler();
	}

	public void Start()
	{
		_scoreHandler.Reset();

		Time.timeScale = 1.0f;
		_bendingService.Initialize();
		_spawnerInitializer.Initialize();
		_deathScreen.Initialize();
		_scoreView.Initialize(_scoreHandler);

	}
}