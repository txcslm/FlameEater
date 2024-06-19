using CodeBase.Data;
using CodeBase.Infrastracture.Services.Interfaces;

namespace CodeBase.Infrastracture.Services.PersistentProgress
{
	public class PersistentProgressService : IPersistentProgressService
	{
		public PlayerProgress Progress { get; set; }
	}
}