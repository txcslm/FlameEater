using CodeBase.Data;

namespace CodeBase.Infrastracture.Services.Interfaces
{
	public interface ISavedProgressReader
	{
		void LoadProgress(PlayerProgress progress);
	}
}