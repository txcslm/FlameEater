using System;
using System.Collections;
using CodeBase.Infrastructure.Services.LoadLevels.Interfaces;
using CodeBase.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.Services.LoadLevels
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
			// if (SceneManager.GetActiveScene().name == sceneName)
			// {
			// 	sceneLoaded?.Invoke();
			// 	yield break;
			// }

			AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(sceneName);
			
			Debug.Log(SceneManager.GetActiveScene().name);

			while (waitNextScene!.isDone == false)
				yield return null;
			
			sceneLoaded?.Invoke();
		}
	}
}