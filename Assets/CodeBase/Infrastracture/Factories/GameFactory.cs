using System;
using System.Collections.Generic;
using AssetManagement;
using Const.AssetManagement;
using Factories.Interfaces;
using Services.Interfaces;
using UnityEngine;

namespace Factories
{
	public class GameFactory : IGameFactory
	{
		private readonly IAssetProvider _assetProvider;

		public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
		public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
		public GameObject CharacterGameObject { get; private set; }
		public GameObject UIGameObject { get; private set; }
		public GameObject AudioSourceGameObject { get; private set; }

		public event Action CharacterCreated;
		public event Action UICreated;
		public event Action AudioSourceCreated;

		public GameFactory(IAssetProvider assetProvider) =>
			_assetProvider = assetProvider;

		public void CreateHud() =>
			InstantiateRegistered(AssetPath.HudPath);

		public GameObject CreateMenu() =>
			InstantiateRegistered(AssetPath.MainMenuPath);

		public void CreateBendingHandler() =>
			InstantiateRegistered(AssetPath.BendingHandlerPath);

		public void CreateMap(GameObject at) =>
			InstantiateRegistered(AssetPath.MapPath, at.transform.position);

		public void CreateVirtualCamera() =>
			InstantiateRegistered(AssetPath.VirtualCameraPath);

		public GameObject CreateCharacter(GameObject at)
		{
			CharacterGameObject = InstantiateRegistered(AssetPath.PlayerPath, at.transform.position);

			CharacterCreated?.Invoke();

			return CharacterGameObject;
		}

		public GameObject CreateUI()
		{
			UIGameObject = InstantiateRegistered(AssetPath.UIPath);
			
			UICreated?.Invoke();
			
			return UIGameObject;
		}

		public GameObject CreateAudioSource()
		{
			AudioSourceGameObject = InstantiateRegistered(AssetPath.AudioSourcePath);
			
			AudioSourceCreated?.Invoke();
			
			return AudioSourceGameObject;
		}

		public void CleanUp()
		{
			ProgressReaders.Clear();
			ProgressWriters.Clear();
		}

		private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
		{
			GameObject gameObject = _assetProvider.Instantiate(prefabPath, at);
			RegisterProgressWatchers(gameObject);
			return gameObject;
		}

		private GameObject InstantiateRegistered(string prefabPath)
		{
			GameObject gameObject = _assetProvider.Instantiate(prefabPath);
			RegisterProgressWatchers(gameObject);
			return gameObject;
		}

		private void RegisterProgressWatchers(GameObject gameObject)
		{
			foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
				Register(progressReader);
		}

		private void Register(ISavedProgressReader progressReader)
		{
			if (progressReader is ISavedProgress progressWriter)
				ProgressWriters.Add(progressWriter);

			ProgressReaders.Add(progressReader);
		}
	}
}