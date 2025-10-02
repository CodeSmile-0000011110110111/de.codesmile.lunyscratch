#if UNITY_6000_0_OR_NEWER
using UnityEngine;

namespace LunyScratch
{
	public static class UnityEngineObjectExt
	{
		public static IGameEngineObject AsEngineObject(this Object obj) => new UnityEngineObject(obj);
	}
}
#endif
