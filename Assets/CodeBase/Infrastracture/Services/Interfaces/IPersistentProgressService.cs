using Data;

namespace Services.Interfaces
{
	public interface IPersistentProgressService : IService
	{
		PlayerProgress Progress { get; set; }
	}
}