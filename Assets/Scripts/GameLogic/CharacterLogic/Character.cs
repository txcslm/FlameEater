using System;
using GameLogic.CharacterLogic.Colliding;
using GameLogic.Environments;
using GameLogic.Interfaces;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameLogic.CharacterLogic
{
	public class Character : MonoBehaviour, IDamageable
	{
		[SerializeField] private Transform _flameViewTransform;
		[SerializeField] private Scaler _scaler;
		[SerializeField] private Steam _steam;
		[SerializeField] private AudioSource _audioSource;

		public void Initialize()
		{
			_steam = GetComponentInChildren<Steam>();
		}
		public void TakeDamage(int damage)
		{
			const int damageMultiplier = -2;

			if (damage <= 0)
				damage = 1;
			
			Scaling(_scaler,0.5f, damageMultiplier * damage);
			_steam.PlaySteam();
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
	}
}