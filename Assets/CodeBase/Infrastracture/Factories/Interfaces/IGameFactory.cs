using System;
using System.Collections.Generic;
using CodeBase.Infrastracture.Services.Interfaces;
using UnityEngine;

namespace CodeBase.Infrastracture.Factories.Interfaces
{
	public interface IGameFactory : IService
	{
		
		List<ISavedProgressReader> ProgressReaders { get; }
		List<ISavedProgress> ProgressWriters { get; }
		GameObject CharacterGameObject { get; }
		GameObject UIGameObject { get; }
		GameObject AudioSourceGameObject { get; }
		
		public event Action CharacterCreated;
		public event Action UICreated;
		public event Action AudioSourceCreated;
		
		
		void CleanUp();
		GameObject CreateCharacter(GameObject at);

		void CreateHud();
		
		GameObject CreateMenu();
		
		GameObject CreateUI();

		GameObject CreateAudioSource();

		void CreateBendingHandler();

		void CreateMap(GameObject at);

		void CreateVirtualCamera();
	}
}