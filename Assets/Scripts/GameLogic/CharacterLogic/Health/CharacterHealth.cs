using System;
using Data;
using GameLogic.CharacterLogic.Colliding;
using GameLogic.Environments;
using GameLogic.Interfaces;
using Services.Interfaces;
using UnityEngine;

namespace GameLogic.CharacterLogic.Health
{
	public class CharacterHealth : MonoBehaviour, ISavedProgress, IDamageable
	{
		[SerializeField] private Scaler _scaler;
		[SerializeField] private Steam _steam;
		
		private State _state;

		public float Current 
		{ 
			get => _state.CurrentHealth; 
			set => _state.CurrentHealth = value; 
		}

		public float Max
		{
			get => _state.MaxHealth;
			set => _state.MaxHealth = value;
		}

		public void LoadProgress(PlayerProgress progress)
		{
			_state = progress.CharacterState;
		}

		public void UpdateProgress(PlayerProgress progress)
		{
			progress.CharacterState.CurrentHealth = Current;
			progress.CharacterState.MaxHealth = Max;

		}
		public void TakeDamage(float damage)
		{
			const float damageMultiplier = 2.0f;

			if (damage <= 0.0f)
				damage = 1.0f;

			damage *= damageMultiplier;

			_steam.PlaySteam();
			ScaleViewDamage(_scaler, damage);
			ScaleColliderDamage(_scaler, damage);
		}

		private static void ScaleViewDamage(Scaler scaler, float damage)
		{
			if (scaler is null)
				throw new ArgumentNullException(nameof(scaler));

			scaler.Scale(-damage);
		}

		private static void ScaleColliderDamage(Scaler scaler, float damage)
		{
			if (scaler is null)
				throw new ArgumentNullException(nameof(scaler));

			scaler.ScaleCollider(-damage);
		}
	}
}