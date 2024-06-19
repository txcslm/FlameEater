using System;

namespace Services.LoadLevels.Interfaces
{
	public interface ILoadSceneService
	{
		void Load(string sceneName, Action sceneLoaded);
	}
}