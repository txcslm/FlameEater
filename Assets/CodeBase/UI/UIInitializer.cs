using CodeBase.UI.Score;
using UnityEngine;

namespace CodeBase.UI
{
	public class UIInitializer : MonoBehaviour
	{
		[SerializeField] private ScoreView _scoreView;
		[SerializeField] private DeathScreen _deathScreen;
		
		private ScoreHandler _scoreHandler;

		private void Awake()
		{
			_scoreHandler = new ScoreHandler();
			_deathScreen.Initialize();
			
			_scoreView.Initialize(_scoreHandler);
		}
	}
}