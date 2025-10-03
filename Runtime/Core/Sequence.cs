using System;
using System.Collections.Generic;

namespace LunyScratch
{
	public sealed class Sequence
	{
		// Run sequence of blocks
		public static void Run(params IScratchBlock[] blocks)
		{
			var sequence = new SequenceBlock(new List<IScratchBlock>(blocks));
			GameEngine.Current.RunBlock(sequence);
		}

		// Repeat blocks forever
		public static void RepeatForever(params IScratchBlock[] blocks) => Run(new RepeatForeverBlock(new List<IScratchBlock>(blocks)));

		// Repeat blocks while condition is true
		public static void RepeatWhileTrue(Func<Boolean> condition, params IScratchBlock[] blocks) =>
			Run(new RepeatWhileTrueBlock(condition, new List<IScratchBlock>(blocks)));

		// Repeat blocks until condition becomes true
		public static void RepeatUntilTrue(Func<Boolean> condition, params IScratchBlock[] blocks) =>
			Run(new RepeatUntilTrueBlock(condition, new List<IScratchBlock>(blocks)));
	}
}
