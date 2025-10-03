// Copyright (C) 2021-2025 Steffen Itterheim
// Refer to included LICENSE file for terms and conditions.

using System;
using System.Collections.Generic;

namespace LunyScratch
{
	public sealed class RepeatUntilTrueBlock : RepeatBlockBase
	{
		private readonly Func<Boolean> _condition;

		public RepeatUntilTrueBlock(Func<Boolean> condition, List<IScratchBlock> blocks)
			: base(blocks) => _condition = condition;

		protected override Boolean ShouldExitLoop() => _condition(); // Exit when condition becomes true
	}
}
