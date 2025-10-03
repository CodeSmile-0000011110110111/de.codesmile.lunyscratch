// Copyright (C) 2021-2025 Steffen Itterheim
// Refer to included LICENSE file for terms and conditions.

using System;
using System.Collections.Generic;

namespace LunyScratch
{
	public sealed class RepeatForeverBlock : RepeatBlockBase
	{
		public RepeatForeverBlock(List<IScratchBlock> blocks)
			: base(blocks) {}

		protected override Boolean ShouldExitLoop() => false; // Never exits
	}
}
