using System;
using GameLogic.AudiSources;
using UnityEngine;

namespace GameLogic.Environments
{
	public class Steam : MonoBehaviour
	{
		[SerializeField] private ParticleSystem _steam;
		
		public void PlaySteam()
		{
			PlayDamageAnimation(_steam);
		}

		private static void PlayDamageAnimation(ParticleSystem particle)
		{
			if (particle is null)
				throw new ArgumentNullException(nameof(particle));

			particle.Play();
		}
	}
}