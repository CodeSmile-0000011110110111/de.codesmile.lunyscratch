// Copyright (C) 2021-2025 Steffen Itterheim
// Refer to included LICENSE file for terms and conditions.

using UnityEngine;

namespace LunyScratch
{
	public sealed class UnityTransform : ITransform
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
