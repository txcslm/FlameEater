using CodeBase.Data;

namespace CodeBase.Infrastructure.Services.Interfaces
{
	public interface ISavedProgressReader
	{
		void LoadProgress(PlayerProgress progress);
	}
}