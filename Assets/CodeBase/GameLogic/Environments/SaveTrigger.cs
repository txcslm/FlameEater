using CodeBase.Infrastracture.Services;
using CodeBase.Infrastracture.Services.SaveLoad;
using UnityEngine;

namespace CodeBase.GameLogic.Environments
{
	public class SaveTrigger : MonoBehaviour
	{
		private ISaveLoadService _saveLoadService;

		[SerializeField] private BoxCollider _boxCollider;

		private void Awake()
		{
			_saveLoadService = AllServices.Container.Single<ISaveLoadService>();
		}

		private void OnDrawGizmos()
		{
			if (_boxCollider is null)
				return;

			Gizmos.color = new Color32(30, 200, 30, 130);
			Gizmos.DrawCube(transform.position + _boxCollider.center, _boxCollider.size);
		}

		private void OnTriggerEnter(Collider other)
		{
			_saveLoadService.SaveProgress();

			Debug.Log(other.gameObject.name);
			Debug.Log("Save");
			gameObject.SetActive(false);
		}
	}
}