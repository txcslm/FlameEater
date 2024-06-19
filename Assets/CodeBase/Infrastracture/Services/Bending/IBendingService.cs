using CodeBase.Infrastracture.Services.Interfaces;

namespace CodeBase.Infrastracture.Services.Bending
{
	public interface IBendingService: IService
	{
		void ManageBending();

		bool EnablePlanet { get; set; }
	}
}