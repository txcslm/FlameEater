using CodeBase.Data;

namespace CodeBase.Infrastructure.Services.Interfaces
{
	public interface ISavedProgress : ISavedProgressReader
	{
		void UpdateProgress(PlayerProgress progress);
	}
}