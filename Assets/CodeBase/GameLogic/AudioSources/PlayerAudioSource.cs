using UnityEngine;

namespace CodeBase.GameLogic.AudioSources
{
	public class PlayerAudioSource : MonoBehaviour
	{
		[SerializeField] private AudioSource _audioSource;
		
		public void PlayDamageSound() =>
			_audioSource.Play();
	}
}