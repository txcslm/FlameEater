using UnityEngine;

public class CursorFollower : MonoBehaviour
{
	[SerializeField] private Camera _mainCamera;

	private void Update()
	{
		FollowMouse();
	}

	private void FollowMouse()
	{
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.z = -_mainCamera.transform.position.z;
		Vector3 worldMousePosition = _mainCamera.ScreenToWorldPoint(mousePosition);
		transform.position = worldMousePosition;
	}
}