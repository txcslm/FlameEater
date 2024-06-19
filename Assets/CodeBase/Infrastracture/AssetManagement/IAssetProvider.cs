using CodeBase.Infrastracture.Services.Interfaces;
using UnityEngine;

namespace CodeBase.Infrastracture.AssetManagement
{
	public interface IAssetProvider : IService
	{
		GameObject Instantiate(string path);

		GameObject Instantiate(string path, Vector3 position);
	}
}