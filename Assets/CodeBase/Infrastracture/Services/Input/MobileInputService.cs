using UnityEngine;

namespace CodeBase.Infrastracture.Services.Input
{
	public class MobileInputService : InputService
	{
		public override Vector2 Axis => SimpleInputAxis();
	}
}