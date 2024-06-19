using System;
using CodeBase.Data;
using CodeBase.GameLogic.CharacterLogic.Colliding;
using CodeBase.GameLogic.Environments;
using CodeBase.GameLogic.Interfaces;
using CodeBase.Infrastracture.Services.Interfaces;
using UnityEngine;

namespace CodeBase.GameLogic.CharacterLogic.Health
{
	public class CharacterHealth : MonoBehaviour, ISavedProgress, IDamageable
	{
		[SerializeField] private Scaler _scaler;
		[SerializeField] private Steam _steam;

		private State _state;

		public event Action HealthChanged;

		public float Current
		{
			get => _state.CurrentHealth;

			set
			{
				if (Math.Abs(_state.CurrentHealth - value) < 0.01f)
					return;
				_state.CurrentHealth = value;
				HealthChanged?.Invoke();
			}
		}

		public float Max
		{
			get => _state.MaxHealth;

			set => _state.MaxHealth = value;
		}

		public void LoadProgress(PlayerProgress progress)
		{
			_state = progress.CharacterState;
			HealthChanged?.Invoke();
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

			_steam.PlaySteam();
			damage *= damageMultiplier;

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