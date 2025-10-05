
using System;

namespace LunyScratch
{
	public static partial class Blocks
	{
		public static IScratchBlock Wait(Double seconds) => new WaitBlock(seconds);
		public static IScratchBlock RepeatForever(params IScratchBlock[] blocks) => new RepeatForeverBlock(blocks);
		public static IScratchBlock RepeatForever(Action block) => RepeatForever(new ExecuteBlock(block));

		public static IScratchBlock RepeatWhileTrue(Func<Boolean> condition, params IScratchBlock[] blocks) =>
			new RepeatWhileTrueBlock(condition, blocks);

		public static IScratchBlock RepeatUntilTrue(Func<Boolean> condition, params IScratchBlock[] blocks) =>
			new RepeatUntilTrueBlock(condition, blocks);
	}
}
