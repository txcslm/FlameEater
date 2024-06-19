using CodeBase.Data;

namespace CodeBase.Infrastracture.Services.Interfaces
{
	public interface ISavedProgress : ISavedProgressReader
	{
		void UpdateProgress(PlayerProgress progress);
	}
}