#if UNITY_6000_0_OR_NEWER
using UnityEngine;

namespace LunyScratch
{
	internal sealed class UnityTransform : ITransform
	{
		private readonly Transform _transform;

		public IVector3 Position
		{
			get => new UnityVector3(_transform.position);
			set => _transform.position = ((UnityVector3)value).ToUnity();
		}

		public IVector3 Forward => new UnityVector3(_transform.forward);

		public UnityTransform(Transform transform) => _transform = transform;
	}
}
#endif
