using UnityEngine;

namespace GameLogic.AudiSources
{
	public class PlayerAudioSource : MonoBehaviour
	{
		[SerializeField] private AudioSource _audioSource;
		
		public void PlayDamageSound() =>
			_audioSource.Play();
	}
}