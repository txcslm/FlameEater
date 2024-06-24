using CodeBase.Data;
using CodeBase.Infrastructure.Services.Interfaces;

namespace CodeBase.Infrastructure.Services.PersistentProgress
{
	public class PersistentProgressService : IPersistentProgressService
	{
		public PlayerProgress Progress { get; set; }
	}
}