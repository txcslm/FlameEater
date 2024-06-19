using CodeBase.Data;

namespace CodeBase.Infrastracture.Services.Interfaces
{
	public interface IPersistentProgressService : IService
	{
		PlayerProgress Progress { get; set; }
	}
}