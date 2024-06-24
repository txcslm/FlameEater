using System;

namespace CodeBase.Infrastructure.Services.LoadLevels.Interfaces
{
	public interface ILoadSceneService
	{
		void Load(string sceneName, Action sceneLoaded);
	}
}