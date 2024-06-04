using Data;
using Services.Interfaces;
using Services.SaveLoad;
using StateMachine.Interfaces;

namespace StateMachine
{
	public class LoadProgressState : IState
	{
		private readonly GameStateMachine _stateMachine;
		private readonly IPersistentProgressService _progressService;
		private readonly ISaveLoadService _saveLoadService;

		public LoadProgressState(GameStateMachine stateMachine, IPersistentProgressService progressService, ISaveLoadService saveLoadService)
		{
			_stateMachine = stateMachine;
			_progressService = progressService;
			_saveLoadService = saveLoadService;
		}

		public void Enter()
		{
			LoadProgressOrInitNew();
			_stateMachine.Enter<LoadLevelState, string>(_progressService.Progress.WorldData.PositionOnLevel.LevelName);
		}

		public void Exit()
		{

		}

		private void LoadProgressOrInitNew() =>
			_progressService.Progress = 
				_saveLoadService.LoadProgress() 
				?? NewProgress();

		private PlayerProgress NewProgress()
		{
			PlayerProgress playerProgress = new PlayerProgress("Level");
			playerProgress.CharacterState.MaxHealth = 50.0f;
			playerProgress.CharacterState.ResetHealth();
			
			return playerProgress;
		}
	}
}