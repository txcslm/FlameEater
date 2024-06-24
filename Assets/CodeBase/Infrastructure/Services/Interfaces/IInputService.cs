using UnityEngine;

namespace CodeBase.Infrastructure.Services.Interfaces
{
	public interface IInputService : IService
	{
		Vector2 Axis { get; }
	
	}
}
