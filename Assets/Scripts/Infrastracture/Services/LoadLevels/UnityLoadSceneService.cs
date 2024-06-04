using System;
using System.Collections;
using Services.LoadLevels.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utility;

namespace Services.LoadLevels
{
	public class  UnityLoadSceneService : ILoadSceneService
	{
		private readonly ICoroutineRunner _coroutineRunner;

		public UnityLoadSceneService(ICoroutineRunner coroutineRunner) =>
			_coroutineRunner = coroutineRunner;

		public void Load(string sceneName, Action sceneLoaded = null) =>
			_coroutineRunner.StartCoroutine(LoadAsync(sceneName, sceneLoaded));

		private static IEnumerator LoadAsync(string sceneName, Action sceneLoaded)
		{
			if (SceneManager.GetActiveScene().name == sceneName)
			{
				sceneLoaded?.Invoke();
				yield break;
			}

			AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(sceneName);
			
			Debug.Log(nameof(waitNextScene));

			while (waitNextScene!.isDone == false)
				yield return null;
			
			sceneLoaded?.Invoke();
		}
	}
}