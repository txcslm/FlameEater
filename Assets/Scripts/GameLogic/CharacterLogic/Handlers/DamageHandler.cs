using System;
using GameLogic.AudiSources;
using GameLogic.Interfaces;
using UnityEngine;

namespace GameLogic.CharacterLogic.Handlers
{
	[RequireComponent(typeof(BoxCollider))] [RequireComponent(typeof(Rigidbody))]
	public class DamageHandler : MonoBehaviour
	{
		private const int Damage = 2;
		
		private void OnTriggerStay(Collider other)
		{
			if (other.TryGetComponent(out IDamageable  damageable))
				damageable.TakeDamage(Damage);
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent(out PlayerAudioSource playerAudioSource))
				playerAudioSource.PlayDamageSound();
		}
	}
}