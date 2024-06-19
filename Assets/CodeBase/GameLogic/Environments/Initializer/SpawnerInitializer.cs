using System.Collections.Generic;
using CodeBase.GameLogic.Spawners;
using UnityEngine;

namespace CodeBase.GameLogic.Environments.Initializer
{
	public class SpawnerInitializer : MonoBehaviour
	{
		private List<EnvironmentSpawner> _environmentSpawners;

		private void Awake()
		{
			_environmentSpawners = new List<EnvironmentSpawner>(GetComponentsInChildren<EnvironmentSpawner>());

			foreach (EnvironmentSpawner environmentSpawner in _environmentSpawners)
			{
				environmentSpawner.Initialize();
			}
		}
	}
}