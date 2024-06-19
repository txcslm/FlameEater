using System.Collections;
using GameLogic.AudiSources;
using GameLogic.Interfaces;
using UnityEngine;

namespace GameLogic.CharacterLogic.Handlers
{
	[RequireComponent(typeof(BoxCollider))] [RequireComponent(typeof(Rigidbody))]
	public class DamageHandler : MonoBehaviour
	{
		private const float Damage = 2.5f;
		private const float DamageInterval = 0.2f; 
		
		private readonly WaitForSeconds _waitForSeconds = new WaitForSeconds(DamageInterval);

		private Coroutine _damageCoroutine;
		
		private void OnTriggerStay(Collider other)
		{
			if (other.TryGetComponent(out IDamageable damageable))
			{
				_damageCoroutine ??= StartCoroutine(DealDamageOverTime(damageable));
			}
		}
		
		private void OnTriggerExit(Collider other)
		{
			if (other.TryGetComponent(out IDamageable damageable) == false)
				return;
			
			if (_damageCoroutine is null)
				return;
				
			StopCoroutine(_damageCoroutine);
			_damageCoroutine = null;
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent(out PlayerAudioSource playerAudioSource))
				playerAudioSource.PlayDamageSound();
		}
		
		private IEnumerator DealDamageOverTime(IDamageable damageable)
		{
			while (true)
			{
				damageable.TakeDamage(Damage);
				yield return _waitForSeconds;
			}
		}
	}
}