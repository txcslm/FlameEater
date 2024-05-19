using System;
using GameLogic.CharacterLogic.Colliding;
using GameLogic.Interfaces;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameLogic.CharacterLogic
{
	public class Character : MonoBehaviour, IDamageable
	{
		[SerializeField] private Transform _flameViewTransform;
		[SerializeField] private Scaler _scaler;
		[SerializeField] private ParticleSystem _steam;
		
		private void Update()
		{
			if(_flameViewTransform.transform.localScale.x <= 0)
				Die();
		}
		
		public void TakeDamage(int damage)
		{
			const int damageMultiplier = -2;
			
			if (damage <= 0)
				throw new ArgumentOutOfRangeException(nameof(damage));
			
			Scaling(_scaler,0.5f, damageMultiplier * damage);
			PlayDamageAnimation(_steam);
		}

		private static void PlayDamageAnimation(ParticleSystem particleSystem)
		{
			if (particleSystem is null)
				throw new ArgumentNullException(nameof(particleSystem));
			
			particleSystem.Play();
		}

		private static void Scaling(Scaler scaler, float scaleTime, int value)
		{
			if(scaler is null)
				throw new ArgumentNullException(nameof(scaler));
			
			if(scaleTime < 0)
				scaleTime = 0;
			
			scaler.Scaling(scaleTime, value);
			scaler.ColliderScaling(value);
		}
		
		private void Die()
		{
			Debug.Log("Character died");
		}
	}
}