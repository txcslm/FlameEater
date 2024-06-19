using System;

namespace CodeBase.Infrastracture.Services.LoadLevels.Interfaces
{
	public interface ILoadSceneService
	{
		void Load(string sceneName, Action sceneLoaded);
	}
}