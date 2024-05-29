using System;
using GameLogic.CharacterLogic.Colliding;
using GameLogic.Environments;
using GameLogic.Interfaces;
using UnityEngine;

namespace GameLogic.CharacterLogic
{
	public class Character : MonoBehaviour, IDamageable
	{
		[SerializeField] private Scaler _scaler;
		[SerializeField] private Steam _steam;

		public void Initialize()
		{
			_steam = GetComponentInChildren<Steam>();
		}

		public void TakeDamage(float damage)
		{
			const float damageMultiplier = 2.0f;

			if (damage <= 0.0f)
				damage = 1.0f;

			damage *= damageMultiplier;

			_steam.PlaySteam();
			ViewScalingDamage(_scaler, damage);
			ColliderScalingDamage(_scaler, damage);
		}

		private static void ViewScalingDamage(Scaler scaler, float damage)
		{
			if (scaler is null)
				throw new ArgumentNullException(nameof(scaler));

			scaler.Scaling(-damage);
		}

		private static void ColliderScalingDamage(Scaler scaler, float damage)
		{
			if (scaler is null)
				throw new ArgumentNullException(nameof(scaler));

			scaler.ColliderScaling(-damage);
		}
	}
}