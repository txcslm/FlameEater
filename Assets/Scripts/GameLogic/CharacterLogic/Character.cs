using System;
using GameLogic.CharacterLogic.Colliding;
using GameLogic.Environments;
using GameLogic.Interfaces;
using UnityEngine;

namespace GameLogic.CharacterLogic
{
	public class Character : MonoBehaviour, IDamageable
	{
		[SerializeField] private Transform _flameViewTransform;
		[SerializeField] private Scaler _scaler;
		[SerializeField] private Steam _steam;

		public void Initialize()
		{
			_steam = GetComponentInChildren<Steam>();
		}
		public void TakeDamage(int damage)
		{
			const int damageMultiplier = 2;
			
			if (damage <= 0)
				damage = 1;
			
			damage *= damageMultiplier;
			
			ViewScalingDamage(_scaler,0.5f, damage);
			ColliderScalingDamage(_scaler, damage);
			_steam.PlaySteam();
		}
		

		private static void ViewScalingDamage(Scaler scaler, float scaleTime, int damage)
		{
			if(scaler is null)
				throw new ArgumentNullException(nameof(scaler));
			
			if(scaleTime < 0)
				scaleTime = 0;

			damage = -damage;
			
			scaler.Scaling(scaleTime, damage);
		}

		private static void ColliderScalingDamage(Scaler scaler, float damage)
		{
			const float damageFactor = 1.2f;
			
			if (scaler is null)
				throw new ArgumentNullException(nameof(scaler));
			
			if (damage <= 0)
				damage = 1;

			damage = -damage/damageFactor;

			scaler.ColliderScaling(damage);
		}
	}
}