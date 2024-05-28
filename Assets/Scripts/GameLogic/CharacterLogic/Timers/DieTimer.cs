using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using GameLogic.CharacterLogic.Colliding;
using GameLogic.CharacterLogic.Handlers;
using GameLogic.Interfaces;

namespace GameLogic.CharacterLogic.Timers
{
	[RequireComponent(typeof(SphereCollider))]
	public class DieTimer : MonoBehaviour, IInteractable
	{
		[SerializeField] private Slider _timerSlider;
		[SerializeField] private Scaler _scaler;
		[SerializeField] private float _totalTime = 10f;
		[SerializeField] private DieHandler _dieHandler;
		[SerializeField] private float _timeIncrement = 5f;
		[SerializeField] [Range(0.1f, 100f)] private float _scaleReductionFactor = 5f;

		private float _currentTime;
		private Coroutine _deathCountdownCoroutine;
		private SphereCollider _collider;
		private WaitForSecondsRealtime _waitForSeconds;

		public void Initialize()
		{
			_waitForSeconds = new WaitForSecondsRealtime(1f);
			_collider = GetComponent<SphereCollider>();
			_currentTime = _totalTime;
			_timerSlider.maxValue = _totalTime;
			_timerSlider.value = _totalTime;

			_scaler.ScaleChanged += HandleScaleChanged;
			_dieHandler.Died += HandleDeath;

			_deathCountdownCoroutine = StartCoroutine(DeathCountdown());
		}

		private void OnDestroy()
		{
			StopDeathCountdown();
			_scaler.ScaleChanged -= HandleScaleChanged;
			_dieHandler.Died -= HandleDeath;
		}

		public void Interact()
		{
			AddTime(_timeIncrement);
		}

		private void HandleScaleChanged(float scaleValue)
		{
			if (_scaler is null)
				throw new ArgumentNullException(nameof(_scaler));

			if (_collider.radius <= 0.5f)
				_collider.radius = 0.5f;

			_collider.radius += scaleValue;
		}
		

		private IEnumerator DeathCountdown()
		{
			while (_currentTime > 0)
			{
				_currentTime -= 1f;
				_timerSlider.value = _currentTime;

				float scaleValue = Time.deltaTime;
				ViewScalingDamage(_scaler, scaleValue);
				ColliderScalingDamage(_scaler, scaleValue / _scaleReductionFactor);
				HandleScaleChanged(-scaleValue / _scaleReductionFactor);

				yield return _waitForSeconds;
			}

			_dieHandler.InvokeDeath();
			StopDeathCountdown();
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

		private void HandleDeath()
		{
			StopDeathCountdown();
		}

		private void AddTime(float time)
		{
			_currentTime += time;

			if (_currentTime > _totalTime)
				_currentTime = _totalTime;

			_timerSlider.value = _currentTime;
		}

		private void StopDeathCountdown()
		{
			if (_deathCountdownCoroutine == null)
				return;

			StopCoroutine(_deathCountdownCoroutine);
			_deathCountdownCoroutine = null;
		}
	}
}