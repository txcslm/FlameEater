using System;
using GameLogic.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameLogic.CharacterLogic.Handlers
{
	public class TriggerHandler : MonoBehaviour, IInteractable
	{
		private const float MinPitchValue = 0.8f;
		private const float MaxPitchValue = 1.1f;
		
		[SerializeField] [Range(2.0f, 7.0f)]private float _dieTime = 2.0f;
		[SerializeField] private ParticleSystem _particleSystem;
		
		private AudioSource _audioSource;
		private Collider _collider;

		public void Initialize()
		{
			_collider = GetComponent<Collider>();
			_audioSource = GetComponent<AudioSource>();
		}
		
		public void Interact()
		{
			if(_particleSystem is null)
				throw new NullReferenceException("Particle system is null");
			
			PlayInteractive(_particleSystem, _audioSource);
			Die(_dieTime, _audioSource);
		}

		private static void PlayInteractive(ParticleSystem particleSystem, AudioSource audioSource)
		{
			if (particleSystem is null)
				throw new ArgumentNullException(nameof(particleSystem));
			
			if (audioSource is null)
				throw new ArgumentNullException(nameof(audioSource));

			audioSource.pitch = Random.Range(MinPitchValue, MaxPitchValue);
			
			particleSystem.Play();
			audioSource.Play();
			
		}
		
		private void Die(float time, AudioSource audioSource)
		{
			if (time < 0)
				time = 0;
			
			_collider.enabled = false;
			Destroy(gameObject, time);

			audioSource.Play();
		}
	}
}