using GameLogic.Interfaces;
using UnityEngine;

namespace GameLogic.CharacterLogic.Colliding
{
	public class TriggerHandler : MonoBehaviour, IInteractable
	{
		[SerializeField] [Range(2.0f, 7.0f)]private float _dieTime = 2.0f;
		private Collider _collider;
		[SerializeField] private ParticleSystem _particleSystem;

		private void Awake()
		{
			_collider = GetComponent<Collider>();
		}
		
		public void Interact()
		{
			if(_particleSystem == null)
				throw new System.NullReferenceException("Particle system is null");
			
			_particleSystem.Play();
			Die(_dieTime);
		}
		
		private void Die(float time)
		{
			_collider.enabled = false;
			Destroy(gameObject, time);
		}
	}
}