#if UNITY_6000_0_OR_NEWER
using UnityEngine;

namespace LunyScratch
{
	/// <summary>
	/// Unity-specific extensions for ScratchActions that automatically wrap Unity objects.
	/// </summary>
	public sealed partial class UnityScratchActions
	{
		public static IScratchBlock Enable(Object obj) => ScratchActions.Enable(new UnityEngineObject(obj));
		public static IScratchBlock Disable(Object obj) => ScratchActions.Disable(new UnityEngineObject(obj));
	}
}
#endif
