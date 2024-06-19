using UnityEngine;

namespace CodeBase.GameLogic.Environments.Movement
{
	public class CloudMover : MonoBehaviour
	{
		[SerializeField] [Range(1.0f, 5.0f)] private float _minSpeed = 1.0f;
		[SerializeField] [Range(1.0f, 10.0f)] private float _maxSpeed = 5.0f;
		[SerializeField] [Range(0.0f, 200.0f)] private float _frontBoundary = 200.0f;
		[SerializeField] [Range(-200.0f, 0.0f)] private float _backBoundary = -200.0f;

		private float _speed;

		private void Start() =>
			SetRandomSpeed(_minSpeed, _maxSpeed);

		private void FixedUpdate() =>
			MoveCloud();

		private void MoveCloud()
		{
			float step = _speed * Time.deltaTime;
			transform.Translate(Vector3.forward * step, Space.Self);

			if (transform.localPosition.z <= _frontBoundary)
				return;

			Vector3 newPosition = transform.localPosition;
			newPosition.z = _backBoundary;
			transform.localPosition = newPosition;
			SetRandomSpeed(_minSpeed, _maxSpeed);
		}

		private void SetRandomSpeed(float minSpeed, float maxSpeed) =>
			_speed = Random.Range(minSpeed, maxSpeed);
	}
}