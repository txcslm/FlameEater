using CodeBase.Data;
using CodeBase.Infrastracture.Services.Interfaces;

namespace CodeBase.Infrastracture.Services.SaveLoad
{
	public interface ISaveLoadService : IService
	{
		PlayerProgress LoadProgress();
		void SaveProgress();
	}
}