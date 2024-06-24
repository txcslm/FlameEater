using CodeBase.Data;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Interfaces;
using CodeBase.Infrastructure.Services.SaveLoad;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace CodeBase.UI.Menu.Options
{
	public class SoundSwitcher : MonoBehaviour, ISavedProgress
	{
		private ISaveLoadService _saveLoadService;
		
		[SerializeField] private Toggle _switchSoundToggle;
		[SerializeField] private AudioMixer _audioMixer;

		private void Awake()
		{
			_saveLoadService = AllServices.Container.Single<ISaveLoadService>();
		}
		private void OnEnable() =>
			_switchSoundToggle.onValueChanged.AddListener(SwitchSound);

		private void OnDisable() =>
			_switchSoundToggle.onValueChanged.RemoveListener(SwitchSound);

		private void SwitchSound(bool value)
		{
			if (value)
				_audioMixer.SetFloat("MasterVolume", 0);
			else
				_audioMixer.SetFloat("MasterVolume", -80);
			
			_saveLoadService.SaveProgress();
		}

		public void LoadProgress(PlayerProgress progress) =>
			progress.ToggleState.IsSoundOn = _switchSoundToggle.isOn;

		public void UpdateProgress(PlayerProgress progress) =>
			_switchSoundToggle.isOn = progress.ToggleState.IsSoundOn;
	}
}