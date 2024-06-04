using UnityEngine;

namespace Services.Interfaces
{
	public interface IInputService : IService
	{
		Vector2 Axis { get; }
	
	}
}
