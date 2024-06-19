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

		[SerializeField] [Range(2.0f, 2.5f)] private float _dieTime = 2.0f;
		[SerializeField] private ParticleSystem _particleSystem;
		[SerializeField] private AudioSource _audioSource;
		[SerializeField] private Collider _collider;
		[SerializeField] private int _points;

		public static event Action<int> OnObjectDestroyed;


		public void Interact()
		{
			PlayInteractive(_particleSystem, _audioSource);
			Die(_dieTime);
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

		private void Die(float time)
		{
			if (time < 0)
				time = 0;

			OnObjectDestroyed?.Invoke(_points);
			_collider.enabled = false;
			Destroy(gameObject, time);
		}
	}
}