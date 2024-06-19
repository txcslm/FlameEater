using UnityEngine;

namespace CodeBase.Infrastracture.Services.Interfaces
{
	public interface IInputService : IService
	{
		Vector2 Axis { get; }
	
	}
}
