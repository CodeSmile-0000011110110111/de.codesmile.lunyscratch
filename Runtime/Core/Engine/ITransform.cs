// Copyright (C) 2021-2025 Steffen Itterheim
// Refer to included LICENSE file for terms and conditions.

namespace LunyScratch
{
	public interface ITransform
	{
		IVector3 Position { get; set; }
		IVector3 Forward { get; }
	}
}
