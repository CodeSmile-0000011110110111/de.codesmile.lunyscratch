using System;

namespace LunyScratch
{
	public sealed class RepeatForeverBlock : RepeatBlockBase
	{
		public RepeatForeverBlock(params IScratchBlock[] blocks)
			: base(blocks) {}


		protected override Boolean ShouldExitLoop() => false; // Never exits
	}
}
