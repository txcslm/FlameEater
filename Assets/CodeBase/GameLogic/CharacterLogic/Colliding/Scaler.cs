using System;
using System.Collections.Generic;
using CodeBase.GameLogic.Interfaces;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace CodeBase.GameLogic.CharacterLogic.Colliding
{
	public class Scaler : MonoBehaviour, IInteractable
	{
		private const float ScaleValue = 2f;
		private const float MaxColliderRadiusValue = 8.0f;
		private const float MinColliderRadiusValue = 0.5f;
		private const float MinScaleZ = 0.5f;
		private const float ScaleY = 2f;

		[SerializeField] private List<Transform> _particlesTransforms;
		[SerializeField] private SphereCollider _collider;
		[SerializeField] private float _scaleTime = 2f;
		
		private readonly Vector3 _maxScale = new Vector3(17, 2, 17);

		public event Action<float> ScaleChanged;

		private void FixedUpdate()
		{
			if (_collider.radius < MinColliderRadiusValue)
				_collider.radius = MinColliderRadiusValue;
		}

		public void Interact()
		{
			Scale(ScaleValue);
			ScaleCollider(ScaleValue);
		}

		public void Scale(float scaleValue)
		{
			foreach (Transform particleTransform in _particlesTransforms)
			{
				Vector3 targetScale = particleTransform.localScale + new Vector3(scaleValue, 0f, scaleValue);

				if (particleTransform.localScale.z >= MinScaleZ)
					particleTransform.localScale = new Vector3(Mathf.Lerp(particleTransform.localScale.x, targetScale.x, _scaleTime),
						ScaleY,
						Mathf.Lerp(particleTransform.localScale.z, targetScale.z, _scaleTime));
				else
					particleTransform.localScale = Vector3.zero;

				NormalizeParticleScale(particleTransform);
			}

			ScaleChanged?.Invoke(scaleValue);
		}

		private void NormalizeParticleScale(Transform particleTransform)
		{
			if (particleTransform.localScale.z > _maxScale.z)
				particleTransform.localScale = _maxScale;
		}

		public void ScaleCollider(float scaleValue)
		{
			const float scalingFactor = 2f;

			if (_collider is null)
				throw new ArgumentNullException(nameof(_collider));

			_collider.radius += scaleValue / scalingFactor;

			if (_collider.radius > MaxColliderRadiusValue)
				_collider.radius = MaxColliderRadiusValue;
		}
	}
}