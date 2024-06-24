using UnityEngine;

namespace CodeBase.Infrastructure.Bootstraps
{
	public class GameRunner : MonoBehaviour
	{
		[SerializeField] private GameBootstrapper _bootstrapperPrefab;

		private void Awake() =>
			InitBootstrapper();

		private void InitBootstrapper()
		{
			GameBootstrapper bootstrapper = FindObjectOfType<GameBootstrapper>();

			if (bootstrapper == null)
				Instantiate(_bootstrapperPrefab);
		}
	}
}