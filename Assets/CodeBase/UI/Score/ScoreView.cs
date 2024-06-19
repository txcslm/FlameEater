using TMPro;
using UnityEngine;

namespace CodeBase.UI.Score
{
	public class ScoreView : MonoBehaviour
	{
		[SerializeField] private TMP_Text _score;

		private ScoreHandler _scoreHandler;

		public void Initialize(ScoreHandler scoreHandler)
		{
			_scoreHandler = scoreHandler;
			scoreHandler.ValueChanged += OnValueChanged;
		}

		private void OnDisable() =>
			_scoreHandler.ValueChanged -= OnValueChanged;

		private void OnValueChanged(int score) =>
			_score.text = score.ToString();
	}
}