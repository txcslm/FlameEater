using Data;

namespace Services.Interfaces
{
	public interface ISavedProgressReader
	{
		void LoadProgress(PlayerProgress progress);
	}
}