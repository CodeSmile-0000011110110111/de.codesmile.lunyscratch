// Copyright (C) 2021-2025 Steffen Itterheim
// Refer to included LICENSE file for terms and conditions.

using UnityEngine;

namespace LunyScratch
{
	public sealed class UnityRigidbody : IRigidbody
	{
		private readonly Rigidbody _rigidbody;

		public IVector3 LinearVelocity
		{
			get => new UnityVector3(_rigidbody.linearVelocity);
			set => _rigidbody.linearVelocity = ((UnityVector3)value).ToUnity();
		}

		public IVector3 Position
		{
			get => new UnityVector3(_rigidbody.position);
			set => _rigidbody.position = ((UnityVector3)value).ToUnity();
		}

		public UnityRigidbody(Rigidbody rigidbody) => _rigidbody = rigidbody;
	}
}
