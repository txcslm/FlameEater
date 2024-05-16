using UnityEngine;

namespace GameLogic.CharacterLogic.Movement
{
	[RequireComponent(typeof(CharacterController))]
	public class CharacterMovement : MonoBehaviour
	{
		private const string HorizontalAxis = "Horizontal";
		private const string VerticalAxis = "Vertical";

		[SerializeField] private CharacterController _controller;
		[SerializeField] [Range(30.0f, 100.0f)] private float _playerSpeed = 50.0f;
	

		private void Update() =>
			Move(_playerSpeed, Time.deltaTime);

		private void Move(float speed, float deltaTime)
		{
			Vector3 move = new Vector3(Input.GetAxis(HorizontalAxis), 0, Input.GetAxis(VerticalAxis)).normalized;

			if (_controller.isGrounded)
				_controller.Move(move * deltaTime * speed + Vector3.down);
			else
				_controller.Move(_controller.velocity + Physics.gravity * deltaTime);
		}
	}
}