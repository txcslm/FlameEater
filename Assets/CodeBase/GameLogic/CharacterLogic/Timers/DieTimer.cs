using System;
using System.Collections;
using CodeBase.GameLogic.CharacterLogic.Colliding;
using CodeBase.GameLogic.CharacterLogic.Handlers;
using CodeBase.GameLogic.Interfaces;
using CodeBase.Infrastracture.Factories.Interfaces;
using CodeBase.Infrastracture.Services;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.GameLogic.CharacterLogic.Timers
{
	[RequireComponent(typeof(SphereCollider))]
	public class DieTimer : MonoBehaviour, IInteractable
	{
		private const float MinColliderRadiusValue = 1f;
		private const float MaxColliderRadiusValue = 12f;
		private const float NormalColliderRadiusValue = 2f;

		[SerializeField] private Scaler _scaler;
		[SerializeField] private DieHandler _dieHandler;
		[SerializeField] private Transform _view;

		[Header("Timer Settings")]
		[SerializeField] private float _totalTime = 10f;
		[SerializeField] private float _timeIncrement = 5f;

		private float _currentTime;
		private Coroutine _deathCountdownCoroutine;
		private SphereCollider _collider;
		private WaitForSeconds _waitForSeconds;
		private SliderHandler _timerSlider;
		private IGameFactory _factory;

		private void FixedUpdate()
		{
			if (_collider.radius <= MinColliderRadiusValue)
				_collider.radius = NormalColliderRadiusValue;
		}

		private void OnDestroy()
		{
			StopDeathCountdown();
			_scaler.ScaleChanged -= HandleScaleChanged;
			_dieHandler.Died -= HandleDeath;
			_factory.UICreated -= OnUICreated;
		}

		public void Initialize()
		{
			_factory = AllServices.Container.Single<IGameFactory>();
			_waitForSeconds = new WaitForSeconds(Time.deltaTime);
			_collider = GetComponent<SphereCollider>();
			_currentTime = _totalTime;
			
			if(_factory.UIGameObject is null)
				_factory.UICreated += OnUICreated;
			else
				InitializeTimerSlider();
			
			_timerSlider.MaxValue = _totalTime;
			_timerSlider.Value = _totalTime;

			_scaler.ScaleChanged += HandleScaleChanged;
			_dieHandler.Died += HandleDeath;
			_deathCountdownCoroutine ??= StartCoroutine(DeathCountdown());
		}

		public void Interact() =>
			AddTime(_timeIncrement);

		private void HandleScaleChanged(float scaleValue)
		{
			const float scaleFactor = 3f;
			
			if (_scaler is null)
				throw new ArgumentNullException(nameof(_scaler));

			_collider.radius += scaleValue / scaleFactor;

			if (_collider.radius > MaxColliderRadiusValue)
				_collider.radius = MaxColliderRadiusValue;
		}

		private void OnUICreated() =>
			InitializeTimerSlider();

		private void InitializeTimerSlider() =>
			_timerSlider = _factory.UIGameObject.GetComponentInChildren<SliderHandler>();

		private IEnumerator DeathCountdown()
		{
			float initialTime = _currentTime;
			float initialScale = _view.localScale.x;

			while (_currentTime > 0)
			{
				_currentTime -= Time.deltaTime;
				_timerSlider.Value = _currentTime;

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

		private void HandleDeath() =>
			StopDeathCountdown();

		private void AddTime(float time)
		{
			_currentTime += time;

			if (_currentTime > _totalTime)
				_currentTime = _totalTime;

			_timerSlider.Value = _currentTime;
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