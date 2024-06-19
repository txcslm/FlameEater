using CodeBase.Infrastracture.Services;
using CodeBase.Infrastracture.Services.Bending;
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