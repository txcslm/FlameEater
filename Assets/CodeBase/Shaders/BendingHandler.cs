using System;
using Services;
using Services.Bending;
using UnityEngine;

namespace Shaders
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