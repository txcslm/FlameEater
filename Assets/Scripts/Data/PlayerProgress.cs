using System;

namespace Data
{
	[Serializable]
	public class PlayerProgress
	{
		public State CharacterState;
		public WorldData WorldData;

		public PlayerProgress(string initialLevel)
		{
			WorldData = new WorldData(initialLevel);
			CharacterState = new State();
		}
	}
}