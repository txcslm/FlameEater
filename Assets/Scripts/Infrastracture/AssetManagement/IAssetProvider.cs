using Services.Interfaces;
using UnityEngine;

namespace AssetManagement
{
	public interface IAssetProvider : IService
	{
		GameObject Instantiate(string path);

		GameObject Instantiate(string path, Vector3 position);
	}
}