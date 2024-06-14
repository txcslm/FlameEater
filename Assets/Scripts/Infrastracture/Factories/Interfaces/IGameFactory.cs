using System;
using System.Collections.Generic;
using Services.Interfaces;
using UnityEngine;

namespace Factories.Interfaces
{
	public interface IGameFactory : IService
	{
		
		List<ISavedProgressReader> ProgressReaders { get; }
		List<ISavedProgress> ProgressWriters { get; }
		GameObject CharacterGameObject { get; }
		GameObject UIGameObject { get; }
		
		public event Action CharacterCreated;
		public event Action UICreated;
		
		
		void CleanUp();
		GameObject CreateCharacter(GameObject at);

		void CreateHud();
		
		GameObject CreateMenu();
		
		GameObject CreateUI();
	}
}