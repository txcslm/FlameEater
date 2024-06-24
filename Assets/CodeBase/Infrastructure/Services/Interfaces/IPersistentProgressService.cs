using CodeBase.Data;

namespace CodeBase.Infrastructure.Services.Interfaces
{
	public interface IPersistentProgressService : IService
	{
		PlayerProgress Progress { get; set; }
	}
}