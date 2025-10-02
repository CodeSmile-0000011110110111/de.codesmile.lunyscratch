using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LunyScratch
{
	public sealed class UnityEngineObject : IGameEngineObject
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
