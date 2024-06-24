using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Bending;
using UnityEngine;

namespace CodeBase.Shaders
{
	public class BendingHandler : MonoBehaviour
	{
		private IBendingService _bendingService;

		private void Awake()
		{
			_bendingService = AllServices.Container.Single <IBendingService>();
			
			_bendingService.ManageBending();
		}
		
		
	}
}