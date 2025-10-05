using System;
using System.Collections.Generic;

namespace LunyScratch
{
	public sealed class RepeatWhileTrueBlock : RepeatBlockBase
	{
		private readonly Func<Boolean> _condition;

		public RepeatWhileTrueBlock(Func<Boolean> condition, params IScratchBlock[] blocks)
			: base(blocks) => _condition = condition;

		public RepeatWhileTrueBlock(Func<Boolean> condition, List<IScratchBlock> blocks)
			: base(blocks) => _condition = condition;

		protected override Boolean ShouldExitLoop() => !_condition(); // Exit when condition becomes false
	}
}
