using UnityEngine;
using UnityEngine.Serialization;

namespace GameLogic.EnemyLogic.Movement
{
	public class CloudMovement : MonoBehaviour
	{
		[SerializeField] [Range( 1.0f, 5.0f)] private float _minSpeed = 1.0f;
		[SerializeField][Range(1.0f, 10.0f)] private float _maxSpeed = 5.0f;
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
			transform.Translate(Vector3.forward * step);

			if (transform.position.z > _frontBoundary == false)
				return;
			
			Vector3 newPosition = transform.position;
			newPosition.z = _backBoundary;
			transform.position = newPosition;
			SetRandomSpeed(_minSpeed, _maxSpeed);
		}

		private void SetRandomSpeed(float minSpeed, float maxSpeed) =>
			_speed = Random.Range(minSpeed, maxSpeed);
	}
}