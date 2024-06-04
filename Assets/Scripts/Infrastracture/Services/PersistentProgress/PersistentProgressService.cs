using Data;
using Services.Interfaces;

namespace Services.PersistentProgress
{
	public class PersistentProgressService : IPersistentProgressService
	{
		public PlayerProgress Progress { get; set; }
	}
}