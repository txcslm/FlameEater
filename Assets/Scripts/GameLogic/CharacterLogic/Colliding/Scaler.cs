using System;
using System.Collections.Generic;
using GameLogic.Interfaces;
using UnityEngine;
using DG.Tweening;

namespace GameLogic.CharacterLogic.Colliding
{
	public class Scaler : MonoBehaviour, IInteractable
	{
		private const float ScaleIncrement = 2.0f;
		
		[SerializeField] private List<Transform> _particlesTransforms;
		[Space] [SerializeField] private SphereCollider _collider;
		[Header("Scale settings")]
		[SerializeField] [Range(0, 5)] private float _scaleTime;
		
		public void Interact()
		{
			Scaling(_scaleTime, ScaleIncrement);
			ColliderScaling(ScaleIncrement);
		}

		private void Scaling(float scaleTime, float scaleIncrement)
		{
			foreach (Transform particleTransform in _particlesTransforms)
			{
				Vector3 targetScale = new Vector3(
					particleTransform.localScale.x + scaleIncrement, 
					particleTransform.localScale.y,
					particleTransform.localScale.z + scaleIncrement);
				
				particleTransform.DOScale(targetScale, scaleTime).SetEase(Ease.InOutQuad);
			}
		}
		
		private void ColliderScaling(float scaleIncrement)
		{
			const float scalingFactor = 5f;
			
			if(_collider == null)
				throw new ArgumentNullException(nameof(_collider));
			
			_collider.radius += scaleIncrement / scalingFactor;
		}
	}
}
