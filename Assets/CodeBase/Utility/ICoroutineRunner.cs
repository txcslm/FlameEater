using System.Collections;
using UnityEngine;

namespace CodeBase.Utility
{
	public interface ICoroutineRunner
	{
		Coroutine StartCoroutine(IEnumerator coroutine);
	}
}