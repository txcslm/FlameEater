using System;
using System.Collections.Generic;
using GameLogic.Environments.World;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameLogic.Spawners
{
	public class EnvironmentSpawner : MonoBehaviour
	{
		[Header("Environment Lists")] [SerializeField]
		private List<Building> _buildings;

		[SerializeField] private List<Rock> _rocks;
		[SerializeField] private List<Wood> _woods;
		[SerializeField] private List<GoodCloud> _goodClouds;
		[SerializeField] private List<BadCloud> _badClouds;

		[Header("Spawn Settings")] [SerializeField]
		private Transform _spawnPoint;

		[SerializeField] private Vector3 _spawnPointOffset;

		[Header("Object Counts")] [SerializeField]
		private int _buildingCount = 10;

		[SerializeField] private int _rockCount = 20;
		[SerializeField] private int _treeCount = 30;
		[SerializeField] private int _goodCloudCount = 5;

		[SerializeField] private int _badCloudCount = 3;

		public void Initialize()
		{
			SpawnEnvironment(_buildings, _buildingCount);
			SpawnEnvironment(_rocks, _rockCount);
			SpawnEnvironment(_woods, _treeCount);
			SpawnEnvironment(_goodClouds, _goodCloudCount);
			SpawnEnvironment(_badClouds, _badCloudCount);
		}

		private void SpawnEnvironment<T>(List<T> environments, int count) where T : WorldEnvironment
		{
			if (environments == null)
				throw new ArgumentNullException(nameof(environments));

			int spawnedCount = 0;
			int maxCount = count <= 0 ? environments.Count : Math.Min(count, environments.Count);

			while (spawnedCount < maxCount)
			{
				Vector3 spawnPosition = GetRandomSpawnPosition();
				Instantiate(environments[spawnedCount].gameObject, spawnPosition, Quaternion.identity, _spawnPoint);
				spawnedCount++;
			}
		}

		private Vector3 GetRandomSpawnPosition()
		{
			float randomX = Random.Range(_spawnPoint.position.x - _spawnPointOffset.x, _spawnPoint.position.x + _spawnPointOffset.x);
			float randomZ = Random.Range(_spawnPoint.position.z - _spawnPointOffset.z, _spawnPoint.position.z + _spawnPointOffset.z);
			return new Vector3(randomX, _spawnPoint.position.y, randomZ);
		}
	}
}