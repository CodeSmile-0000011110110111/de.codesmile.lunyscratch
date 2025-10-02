using UnityEngine;

namespace LunyScratch
{
	/// <summary>
	/// Unity-specific extensions for ScratchActions that automatically wrap Unity objects.
	/// </summary>
	public sealed partial class UnityScratchActions
	{
		public static IStep Enable(Object obj) => ScratchActions.Enable(new UnityEngineObject(obj));
		public static IStep Disable(Object obj) => ScratchActions.Disable(new UnityEngineObject(obj));
	}
}
