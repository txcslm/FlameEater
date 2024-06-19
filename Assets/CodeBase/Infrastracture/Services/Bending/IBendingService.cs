using Services.Interfaces;

namespace Services.Bending
{
	public interface IBendingService: IService
	{
		void ManageBending();

		bool EnablePlanet { get; set; }
	}
}