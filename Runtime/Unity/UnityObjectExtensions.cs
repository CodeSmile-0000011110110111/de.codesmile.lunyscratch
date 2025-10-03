#if UNITY_6000_0_OR_NEWER
using UnityEngine;

namespace LunyScratch
{
	public static class UnityObjectExtensions
	{
		public static IEngineObject AsEngineObject(this Object obj) => new UnityEngineObject(obj);
	}
}
#endif
