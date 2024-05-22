using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using GameLogic.CharacterLogic.Animation;
using GameLogic.CharacterLogic.Movement;
using UnityEngine;

namespace GameLogic.CharacterLogic.Handlers
{
    public class DieHandler : MonoBehaviour
    {
        [SerializeField] private Transform _viewTransform;
        [SerializeField] private Canvas _deathScreen;
        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private CharacterMovement _characterMovement;
        
        [Header("Audio Sources")]
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private List<AudioSource> _audioSources;
        
        [Header("Death Animation")]
        [SerializeField] private DeathAnimator _deathAnimator;
        [SerializeField] private float _dieTime = 0.5f;
        
        private WaitForSeconds _wait;

        private event Action Died;
        
        private bool IsDead => _viewTransform.localScale == Vector3.zero;

        private void OnEnable() =>
            Died += HandleDeath;

        private void OnDisable() =>
            Died -= HandleDeath;
        
        public void Initialize() =>
            _wait = new WaitForSeconds(_dieTime);

        private void FixedUpdate()
        {
            if (IsDead == false)
                return;

            Died?.Invoke();
        }

        private void HandleDeath() =>
            StartCoroutine(DieCoroutine(_dieTime, _audioSource, _camera, _deathScreen, _characterMovement));

        private IEnumerator DieCoroutine(float time, AudioSource audioSource, CinemachineVirtualCamera cam, Canvas canvas, CharacterMovement characterMovement)
        {
            if (audioSource is null)
                throw new ArgumentNullException(nameof(audioSource));

            if (cam is null)
                throw new ArgumentNullException(nameof(cam));
            
            if (characterMovement is null)
                throw new ArgumentNullException(nameof(characterMovement));
            
            foreach (AudioSource source in _audioSources)
                source.DOFade(0, time);

            characterMovement.enabled = false;
            _deathAnimator.AnimateDeath(time, cam);
            yield return _wait;
            
            Time.timeScale = 0.0f;
            audioSource.Play();
            ShowDeathScreen(canvas);
        }

        private static void ShowDeathScreen(Canvas canvas)
        {
            if (canvas is null)
                throw new ArgumentNullException(nameof(canvas));

            canvas.gameObject.SetActive(true);
        }
    }
}