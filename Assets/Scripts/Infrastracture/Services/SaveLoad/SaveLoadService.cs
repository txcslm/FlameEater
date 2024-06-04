using Data;
using Factories.Interfaces;
using Services.Interfaces;

namespace Services.SaveLoad
{
	public class SaveLoadService : ISaveLoadService
	{
		private readonly IPersistentProgressService _progressService;
		private readonly IGameFactory _factory;
		private const string ProgressKey = "Progress";

		public SaveLoadService(IPersistentProgressService progressService, IGameFactory factory)
		{
			_progressService = progressService;
			_factory = factory;
		}

		public void SaveProgress()
		{
			foreach (ISavedProgress progressWriter in _factory.ProgressWriters)
				progressWriter.UpdateProgress(_progressService.Progress);

			Agava.YandexGames.Utility.PlayerPrefs.SetString(ProgressKey,
				_progressService.Progress
					.ToJson());
		}

		public PlayerProgress LoadProgress() =>
			Agava.YandexGames.Utility.PlayerPrefs.GetString(ProgressKey)?
				.ToDeserialized<PlayerProgress>();
	}
}