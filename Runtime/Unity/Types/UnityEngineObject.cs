#if UNITY_6000_0_OR_NEWER
using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LunyScratch
{
	internal sealed class UnityEngineObject : IEngineObject
	{
		private readonly Object _engineObject;

		public static implicit operator UnityEngineObject(Object engineObject) => new(engineObject);

		public UnityEngineObject(Object engineObject) => _engineObject = engineObject;

		public void SetEnabled(Boolean enabled)
		{
			switch (_engineObject)
			{
				case Behaviour behaviour:
					behaviour.enabled = enabled;
					break;
				case GameObject go:
					go.SetActive(enabled);
					break;
			}
		}
	}
}
#endif
