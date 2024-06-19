using CodeBase.Const;
using CodeBase.Infrastracture.Services.Interfaces;
using UnityEngine;

namespace CodeBase.Infrastracture.Services.Input
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