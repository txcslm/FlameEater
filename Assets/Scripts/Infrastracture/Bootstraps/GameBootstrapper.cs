using UI;
using UnityEngine;
using Utility;

namespace Bootstraps
{
	public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
	{
		[SerializeField] private CanvasGroupViewer _canvasGroupViewer;
	
		private Game _game;
	
		private void Awake()
		{
			DontDestroyOnLoad(_canvasGroupViewer);
			_game = new Game(_canvasGroupViewer, this);
			_game.Start();
			DontDestroyOnLoad(this);
		}
	}
}