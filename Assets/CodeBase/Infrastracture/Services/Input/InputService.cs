using Services.Interfaces;
using Services.LoadLevels.Interfaces;
using UnityEngine;

namespace Services.Input
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