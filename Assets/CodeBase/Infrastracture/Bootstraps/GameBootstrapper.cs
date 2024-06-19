using CodeBase.Infrastracture.StateMachine.States;
using CodeBase.UI;
using CodeBase.Utility;
using UnityEngine;

namespace CodeBase.Infrastracture.Bootstraps
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