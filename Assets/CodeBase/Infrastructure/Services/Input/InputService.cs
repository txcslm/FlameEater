using CodeBase.Const;
using CodeBase.Infrastructure.Services.Interfaces;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
	public abstract class InputService : IInputService
	{
		public abstract Vector2 Axis { get; }
		
		protected static Vector2 SimpleInputAxis() =>
			new Vector2(
				SimpleInput.GetAxis(Constant.HorizontalAxis),
				SimpleInput.GetAxis(Constant.VerticalAxis));
	}
}