using UnityEngine;

namespace LunyScratch
{
	public static class UnityEngineObjectExt
	{
		public static IGameEngineObject AsEngineObject(this Object obj) => new UnityEngineObject(obj);
	}
}
