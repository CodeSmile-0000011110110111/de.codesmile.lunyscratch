using System;

namespace LunyScratch
{
	public sealed class Scratch
	{
		public static void Run(SequenceBlock sequence) => GameEngine.Runtime.RunBlock(sequence);
		public static void Run(params IScratchBlock[] blocks) => Run(new SequenceBlock(blocks));

		public static void RepeatForever(params IScratchBlock[] blocks) => Run(new RepeatForeverBlock(blocks));
		public static void RepeatForever(Action block) => RepeatForever(new ExecuteBlock(block));

		public static void RepeatWhileTrue(Func<Boolean> condition, params IScratchBlock[] blocks) =>
			Run(new RepeatWhileTrueBlock(condition, blocks));

		public static void RepeatUntilTrue(Func<Boolean> condition, params IScratchBlock[] blocks) =>
			Run(new RepeatUntilTrueBlock(condition, blocks));
	}
}
