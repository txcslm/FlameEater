using CodeBase.Data;
using CodeBase.Infrastructure.Services.Interfaces;

namespace CodeBase.Infrastructure.Services.SaveLoad
{
	public interface ISaveLoadService : IService
	{
		PlayerProgress LoadProgress();
		void SaveProgress();
	}
}