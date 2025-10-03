using System;
using System.Collections.Generic;

namespace LunyScratch
{
	public sealed class Scratch
	{
		public static void Run(params IScratchBlock[] blocks)
		{
			var sequence = new SequenceBlock(new List<IScratchBlock>(blocks));
			ScratchEngine.Runtime.RunBlock(sequence);
		}

		public static void RepeatForever(params IScratchBlock[] blocks) => Run(new RepeatForeverBlock(new List<IScratchBlock>(blocks)));

		public static void RepeatWhileTrue(Func<Boolean> condition, params IScratchBlock[] blocks) =>
			Run(new RepeatWhileTrueBlock(condition, new List<IScratchBlock>(blocks)));

		public static void RepeatUntilTrue(Func<Boolean> condition, params IScratchBlock[] blocks) =>
			Run(new RepeatUntilTrueBlock(condition, new List<IScratchBlock>(blocks)));
	}
}
