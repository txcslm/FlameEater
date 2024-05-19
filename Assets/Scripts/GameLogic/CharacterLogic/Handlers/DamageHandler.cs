using System;
using GameLogic.Interfaces;
using UnityEngine;

namespace GameLogic.CharacterLogic.Handlers
{
	[RequireComponent(typeof(BoxCollider))] [RequireComponent(typeof(Rigidbody))]
	public class DamageHandler : MonoBehaviour
	{
		[SerializeField] private AudioSource _audioSource;
		
		private const int Damage = 2;

		private void OnTriggerStay(Collider other)
		{
			if (other.TryGetComponent(out IDamageable  damageable))
			{
				damageable.TakeDamage(Damage);
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent(out IDamageable  damageable))
				PlaySound(_audioSource);
		}

		private static void PlaySound(AudioSource audioSource)
		{
			if(audioSource is null)
				throw new ArgumentNullException(nameof(audioSource));
			
			audioSource.Play();
		}

		private static void StopSound(AudioSource audioSource)
		{
			if(audioSource is null)
				throw new ArgumentNullException(nameof(audioSource));
			
			audioSource.Stop();
		}
	}
}