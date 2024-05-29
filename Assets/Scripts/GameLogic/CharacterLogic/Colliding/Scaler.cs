using System;
using System.Collections.Generic;
using GameLogic.Interfaces;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace GameLogic.CharacterLogic.Colliding
{
	public class Scaler : MonoBehaviour, IInteractable
	{
		private const float ScaleValue = 2f;

		[SerializeField] private List<Transform> _particlesTransforms;
		[SerializeField] private SphereCollider _collider;
		[SerializeField] private float _scaleTime = 2f;
		
		private readonly Vector3 _maxScale = new Vector3(10, 2, 10);

		public event Action<float> ScaleChanged;

		private void FixedUpdate()
		{
			if (_collider.radius < 0.5f)
				_collider.radius = 0.5f;
		}

		public void Interact()
		{
			Scaling(ScaleValue);
			ColliderScaling(ScaleValue);
		}

		public void Scaling(float scaleValue)
		{
			
			foreach (Transform particleTransform in _particlesTransforms)
			{
				Vector3 targetScale = particleTransform.localScale + new Vector3(scaleValue, 0f, scaleValue);

				if (particleTransform.localScale.z >= 0.5f)
					particleTransform.localScale = new Vector3(Mathf.Lerp(particleTransform.localScale.x, targetScale.x, _scaleTime), 2f, Mathf.Lerp(particleTransform.localScale.z, targetScale.z, _scaleTime));
				else
					particleTransform.localScale = Vector3.zero;

				if (particleTransform.localScale.x > _maxScale.x)
					particleTransform.localScale = _maxScale;
			}

			ScaleChanged?.Invoke(scaleValue);
		}

		public void ColliderScaling(float scaleValue)
		{
			const float scalingFactor = 3f;

			if (_collider is null)
				throw new ArgumentNullException(nameof(_collider));

			_collider.radius += scaleValue / scalingFactor;
			
			if(_collider.radius > 5f)
				_collider.radius = 5f;
		}
	}
}