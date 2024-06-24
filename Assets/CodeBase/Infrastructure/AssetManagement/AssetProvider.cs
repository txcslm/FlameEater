using UnityEngine;

namespace CodeBase.Infrastructure.AssetManagement
{
	public class AssetProvider : IAssetProvider
	{
		public GameObject Instantiate(string path)
		{
			GameObject prefab = Resources.Load<GameObject>(path);
			return Object.Instantiate(prefab);
		}

		public GameObject Instantiate(string path, Vector3 position)
		{
			GameObject prefab = Resources.Load<GameObject>(path);
			return Object.Instantiate(prefab, position, Quaternion.identity);
		}
	}
}