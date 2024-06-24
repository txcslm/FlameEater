using CodeBase.Const;
using CodeBase.Data;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.GameLogic.CharacterLogic.Movement
{
	[RequireComponent(typeof(CharacterController))]
	public class CharacterMovement : MonoBehaviour, ISavedProgress
	{
		[SerializeField] private CharacterController _controller;
		[SerializeField] [Range(30.0f, 100.0f)] private float _playerSpeed = 50.0f;
		
		private Camera _camera;
		private IInputService _inputService;

		private void Awake() =>
			_inputService = AllServices.Container.Single<IInputService>();

		private void Start() =>
			_camera = Camera.main;

		private void FixedUpdate() =>
			Move(_playerSpeed, Time.deltaTime);

		public void UpdateProgress(PlayerProgress progress) =>
			progress.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevel(), transform.position.AsVector3Data());

		public void LoadProgress(PlayerProgress progress)
		{
			if (CurrentLevel() != progress.WorldData.PositionOnLevel.LevelName)
				return;

			Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;
			
			if(savedPosition != null)
				Warp(to: savedPosition);
		}

		private void Warp(Vector3Data to)
		{
			_controller.enabled = false;
			transform.position = to.AsUnityVector3().AddY(_controller.height);
			_controller.enabled = true;
		}

		private void Move(float speed, float deltaTime)
		{
			Vector3 move = Vector3.zero;

			if (_inputService.Axis.sqrMagnitude > Constant.Epsilon)
			{
				move = _camera.transform.TransformDirection(_inputService.Axis);
				move.y = 0.0f;
				move = move.normalized;

				transform.forward = move;
			}
			move += Physics.gravity;
			_controller.Move(move * (speed * deltaTime));
		}

		private static string CurrentLevel() =>
			SceneManager.GetActiveScene().name;
	}
}