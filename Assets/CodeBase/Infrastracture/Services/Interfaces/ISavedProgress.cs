using Data;

namespace Services.Interfaces
{
	public interface ISavedProgress : ISavedProgressReader
	{
		void UpdateProgress(PlayerProgress progress);
	}
}