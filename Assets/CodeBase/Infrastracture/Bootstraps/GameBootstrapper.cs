using StateMachine;
using UI;
using UnityEngine;
using Utility;

namespace Bootstraps
{
	public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
	{
		[SerializeField] private CanvasGroupViewer _curtainPrefab;

		private Game _game;

		private void Awake()
		{
			_game = new Game(this, Instantiate(_curtainPrefab));
			_game.StateMachine.Enter<BootstrapState>();
			DontDestroyOnLoad(this);
		}
	}
}