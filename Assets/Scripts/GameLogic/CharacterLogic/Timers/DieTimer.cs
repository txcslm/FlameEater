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
		private const float MinColliderRadiusValue = 0.5f;
		private const float MaxColliderRadiusValue = 12f;
		private const float OneSecond = 1f;

		[SerializeField] private Scaler _scaler;
		[SerializeField] private DieHandler _dieHandler;
		[SerializeField] private Transform _view;

		[Header("Timer Settings")] 
		[SerializeField] private Slider _timerSlider;
		[SerializeField] private float _totalTime = 10f;
		[SerializeField] private float _timeIncrement = 5f;

		private float _currentTime;
		private Coroutine _deathCountdownCoroutine;
		private SphereCollider _collider;
		private WaitForSecondsRealtime _waitForSeconds;

		private void FixedUpdate()
		{
			if (_collider.radius <= MinColliderRadiusValue)
				_collider.radius = 1f;
		}

		private void OnDestroy()
		{
			StopDeathCountdown();
			_scaler.ScaleChanged -= HandleScaleChanged;
			_dieHandler.Died -= HandleDeath;
		}

		public void Initialize()
		{
			_waitForSeconds = new WaitForSecondsRealtime(OneSecond);
			_collider = GetComponent<SphereCollider>();
			_currentTime = _totalTime;
			_timerSlider.maxValue = _totalTime;
			_timerSlider.value = _totalTime;

			_scaler.ScaleChanged += HandleScaleChanged;
			_dieHandler.Died += HandleDeath;
			_deathCountdownCoroutine ??= StartCoroutine(DeathCountdown());
		}

		public void Interact()
		{
			AddTime(_timeIncrement);
		}

		private void HandleScaleChanged(float scaleValue)
		{
			if (_scaler is null)
				throw new ArgumentNullException(nameof(_scaler));

			_collider.radius += scaleValue;

			if (_collider.radius > MaxColliderRadiusValue)
				_collider.radius = MaxColliderRadiusValue;
		}

		private IEnumerator DeathCountdown()
		{
			float initialTime = _currentTime;
			float initialScale = _view.localScale.x;

			while (_currentTime > 0)
			{
				_currentTime -= OneSecond;
				_timerSlider.value = _currentTime;

				float currentScale = _view.localScale.x;
				float targetScale = _currentTime / initialTime * initialScale;

				if (currentScale > initialScale)
					initialScale = currentScale;

				float scaleFactor = currentScale - targetScale;

				_scaler.Scale(-scaleFactor);
				_scaler.ScaleCollider(-scaleFactor);
				HandleScaleChanged(-scaleFactor);

				yield return _waitForSeconds;
			}

			_dieHandler.InvokeDeath();
			StopDeathCountdown();
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