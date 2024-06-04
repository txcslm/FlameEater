using Data;
using Services.Interfaces;

namespace Services.SaveLoad
{
	public interface ISaveLoadService : IService
	{
		PlayerProgress LoadProgress();
		
		void SaveProgress();
	}
}