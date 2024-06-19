using DG.Tweening;
using UnityEngine;

namespace CodeBase.GameLogic.CharacterLogic.Animation
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
		
		public void AnimateDeath(float time)
		{
			
			if (time < 0)
				time = 0;
		}
	}
}