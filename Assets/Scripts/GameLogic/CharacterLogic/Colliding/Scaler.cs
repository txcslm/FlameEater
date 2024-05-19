using System;
using System.Collections.Generic;
using GameLogic.Interfaces;
using UnityEngine;
using DG.Tweening;

namespace GameLogic.CharacterLogic.Colliding
{
	public class Scaler : MonoBehaviour, IInteractable
	{
		private const float ScaleValue = 1.2f;
		
		[SerializeField] private List<Transform> _particlesTransforms;
		[Space] [SerializeField] private SphereCollider _collider;
		[Header("Scale settings")]
		[SerializeField] [Range(0, 5)] private float _scaleTime;
		
		public void Interact()
		{
			Scaling(_scaleTime, ScaleValue);
			ColliderScaling(ScaleValue);
		}

		public void Scaling(float scaleTime, float scaleValue)
		{
			foreach (Transform particleTransform in _particlesTransforms)
			{
				Vector3 targetScale = new Vector3(
					particleTransform.localScale.x + scaleValue, 
					particleTransform.localScale.y,
					particleTransform.localScale.z + scaleValue);

				if (particleTransform.localScale.z >= 0)
				{
					particleTransform.DOScale(targetScale, scaleTime).SetEase(Ease.InOutQuad);
				}
				else
				{
					particleTransform.localScale = Vector3.zero;
				}
			}
		}

		public void ColliderScaling(float scaleValue)

		{
			const float scalingFactor = 5f;

			if (_collider == null)
				throw new ArgumentNullException(nameof(_collider));

			if (_collider.radius <= 0)
				_collider.radius = 0.5f;

			_collider.radius += scaleValue / scalingFactor;
		}
	}
}
