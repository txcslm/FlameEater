using System;

namespace CodeBase.Data
{
	[Serializable]
	public class PlayerProgress
	{
		public State CharacterState;
		public WorldData WorldData;
		public ToggleState ToggleState;

		public PlayerProgress(string initialLevel)
		{
			WorldData = new WorldData(initialLevel);
			CharacterState = new State();
			ToggleState = new ToggleState();
		}
	}
}