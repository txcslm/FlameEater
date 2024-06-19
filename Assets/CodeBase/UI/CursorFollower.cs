using UnityEngine;

namespace CodeBase.UI
{
	public class CursorFollower : MonoBehaviour
	{
		private void Update() =>
			FollowMouse();

		private void FollowMouse()
		{
			Vector3 mousePosition = Input.mousePosition;
			
			if (Camera.main == null)
				return;
			
			mousePosition.z = -Camera.main.transform.position.z;
			Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
			transform.position = worldMousePosition;
		}
	}
}