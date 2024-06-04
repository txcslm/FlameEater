using System;
using System.Collections.Generic;
using Services;
using Services.Interfaces;
using UnityEngine;

namespace Factories.Interfaces
{
	public interface IGameFactory : IService
	{
		
		List<ISavedProgressReader> ProgressReaders { get; }

		List<ISavedProgress> ProgressWriters { get; }
		GameObject CharacterGameObject { get; }
		
		public event Action CharacterCreated;
		

		void CleanUp();
		GameObject CreateCharacter(GameObject at);

		void CreateHud();
	}
}