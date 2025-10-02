#if UNITY_6000_0_OR_NEWER
using UnityEngine;

namespace LunyScratch
{
	/// <summary>
	/// Base class for all Scratch-style behaviors.
	/// Automatically initializes ScratchRuntime on first use.
	/// </summary>
	public abstract class ScratchBehaviour : MonoBehaviour
	{
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void Initialize() => UnityScratchRuntime.Initialize();
	}
}
#endif
