using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using Factories.Interfaces;
using GameLogic.CharacterLogic.Animation;
using GameLogic.CharacterLogic.Movement;
using Services;
using UI;
using UnityEngine;
using UnityEngine.Audio;

namespace GameLogic.CharacterLogic.Handlers
{
    public class DieHandler : MonoBehaviour
    {
        [SerializeField] private Transform _viewTransform;
        [SerializeField] private CharacterMovement _characterMovement;

        [Header("Audio Settings")]
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private List<AudioSource> _audioSources;
        [SerializeField] private AudioMixer _audioMixer;

        [Header("Death Animation")]
        [SerializeField] private DeathAnimator _deathAnimator;

        [SerializeField] private float _dieTime = 0.5f;

        private DeathScreen _deathScreen;
        private WaitForSeconds _wait;
        private IGameFactory _factory;

        public event Action Died;

        private bool IsDead => _viewTransform.localScale.x <= 0.0f;

        private void OnEnable() =>
            Died += HandleDeath;

        private void OnDisable()
        {
            Died -= HandleDeath;
            _factory.AudioSourceCreated -= OnAudioSourceCreated;
            _factory.UICreated -= OnUICreated;
        }
        
        public void Initialize()
        {
            _factory = AllServices.Container.Single<IGameFactory>();

            if (_factory.AudioSourceGameObject is null)
                _factory.AudioSourceCreated += OnAudioSourceCreated;
            else
                InitializeAudioSource();
            
            if(_factory.UIGameObject is null)
                _factory.UICreated += OnUICreated;
            else
                InitializeDeathScreen();
            
            _wait = new WaitForSeconds(_dieTime);
        }

        private void FixedUpdate()
        {
            if (IsDead == false)
                return;

            InvokeDeath();
        }

        private void OnAudioSourceCreated() =>
            InitializeAudioSource();

        private void OnUICreated() =>
            InitializeDeathScreen();

        private void InitializeAudioSource() =>
            _audioSources.Add(_factory.AudioSourceGameObject.GetComponent<AudioSource>());

        private void InitializeDeathScreen() =>
            _deathScreen = _factory.UIGameObject.GetComponentInChildren<DeathScreen>();

        public void InvokeDeath() =>
            Died?.Invoke();

        private void HandleDeath() =>
            StartCoroutine(DieCoroutine(_dieTime, _audioSource, _deathScreen, _characterMovement));

        private IEnumerator DieCoroutine(float time, AudioSource audioSource, DeathScreen deathScreen, CharacterMovement characterMovement)
        {
            if (audioSource is null)
                throw new ArgumentNullException(nameof(audioSource));
            
            if (characterMovement is null)
                throw new ArgumentNullException(nameof(characterMovement));

            foreach (AudioSource source in _audioSources)
            {
                yield return source.DOFade(-80f, time);
                source.Stop();
            }

            characterMovement.enabled = false;
            _deathAnimator.AnimateDeath(time);
            yield return _wait;
            
            Time.timeScale = 0.0f;
            ShowDeathScreen(deathScreen);
            audioSource.Play();
        }

        private static void ShowDeathScreen(DeathScreen deathScreen)
        {
            if (deathScreen is null)
                throw new ArgumentNullException(nameof(deathScreen));

            deathScreen.Show();
        }
    }
}
