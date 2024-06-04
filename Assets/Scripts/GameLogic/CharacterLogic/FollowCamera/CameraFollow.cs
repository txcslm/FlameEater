using Cinemachine;
using Factories.Interfaces;
using Services;
using UnityEngine;

namespace GameLogic.CharacterLogic.FollowCamera
{
	[RequireComponent(typeof(CinemachineVirtualCamera))]
	public class CameraFollow : MonoBehaviour
	{
		private CinemachineVirtualCamera _virtualCamera;
		private IGameFactory _gameFactory;
		private Transform _followTarget;

		private void Awake()
		{
			_virtualCamera = GetComponent<CinemachineVirtualCamera>();
			_gameFactory = AllServices.Container.Single<IGameFactory>();
		}

		private void Start()
		{
			if (_gameFactory.CharacterGameObject is null)
				_gameFactory.CharacterCreated += OnCharacterCreated;
			else
				InitializeHeroTransform();
		}

		private void InitializeHeroTransform()
		{
			_followTarget = _gameFactory.CharacterGameObject.transform;
			
			if (_virtualCamera == null)
				return;
			
			_virtualCamera.Follow = _followTarget;
		}

		private void OnCharacterCreated()
		{
			InitializeHeroTransform();
		}
	}
}