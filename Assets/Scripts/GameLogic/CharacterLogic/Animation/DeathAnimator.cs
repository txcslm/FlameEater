using System;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

namespace GameLogic.CharacterLogic.Animation
{
	public class DeathAnimator : MonoBehaviour
	{
		[Header("Move Animation")]
		[SerializeField] private Vector3 _dieTargetPosition;
		[SerializeField] private Ease _dieAnimationEaseMove;
		[Header("Rotate Animation")]
		[SerializeField] private Ease _dieAnimationEaseRotate;
		[SerializeField] private RotateMode _dieAnimationRotateMode;
		[SerializeField] private Vector3 _dieTargetRotation;
		
		public void AnimateDeath(float time, CinemachineVirtualCamera cam)
		{
			if (cam is null)
				throw new ArgumentNullException(nameof(cam));
			
			if (time < 0)
				time = 0;

			cam.transform.DOMove(_dieTargetRotation, time).SetEase(_dieAnimationEaseMove);
			cam.transform.DORotate(_dieTargetRotation, time, _dieAnimationRotateMode).SetEase(_dieAnimationEaseRotate);
		}
	}
}