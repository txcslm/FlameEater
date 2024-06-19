using System;
using GameLogic.CharacterLogic.Handlers;

namespace UI.Score
{
	public class ScoreHandler
	{
		private int _value;

		public event Action<int> ValueChanged;

		public ScoreHandler() =>
			TriggerHandler.OnObjectDestroyed += HandleObjectDestroyed;

		~ScoreHandler() =>
			TriggerHandler.OnObjectDestroyed -= HandleObjectDestroyed;

		private void HandleObjectDestroyed(int points)
		{
			Add(points);
		}

		public void Add(int value)
		{
			_value += value;
			ValueChanged?.Invoke(_value);
		}

		public void Reset()
		{
			_value = 0;
			ValueChanged?.Invoke(_value);
		}
	}
}