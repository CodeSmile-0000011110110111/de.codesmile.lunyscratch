#if UNITY_6000_0_OR_NEWER
using UnityEngine;

namespace LunyScratch
{
	/// <summary>
	/// Unity-specific extensions for ScratchActions that automatically wrap Unity objects.
	/// </summary>
	public static class UnityBlocks
	{
		public static IScratchBlock Enable(Object obj) => Blocks.Enable(new UnityEngineObject(obj));
		public static IScratchBlock Disable(Object obj) => Blocks.Disable(new UnityEngineObject(obj));
	}
}
#endif
