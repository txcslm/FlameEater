using CodeBase.Infrastructure.Services.Interfaces;

namespace CodeBase.Infrastructure.Services.Bending
{
	public interface IBendingService: IService
	{
		void ManageBending();

		bool EnablePlanet { get; set; }
	}
}