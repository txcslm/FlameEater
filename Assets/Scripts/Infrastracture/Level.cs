using GameLogic.Environments.Initializer;
using Shaders;
using UI.Score;
using UnityEngine;

public class Level
{
	private readonly BendingService _bendingService;
	private readonly SpawnerInitializer _spawnerInitializer;
	private readonly ScoreHandler _scoreHandler;

	public Level(BendingService bendingService,
		SpawnerInitializer spawnerInitializer)
	{
		_bendingService = bendingService;
		_spawnerInitializer = spawnerInitializer;

		_scoreHandler = new ScoreHandler();
	}

	public void Start()
	{
		_scoreHandler.Reset();

		Time.timeScale = 1.0f;
		_bendingService.Initialize();
		_spawnerInitializer.Initialize();

	}
}