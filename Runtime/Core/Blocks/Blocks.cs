// Copyright (C) 2021-2025 Steffen Itterheim
// Refer to included LICENSE file for terms and conditions.

using System;
using System.Collections.Generic;

namespace LunyScratch
{
	public static class Blocks
	{
		public static IScratchBlock Say(String message, Double duration = 0) => new ExecuteBlock(() =>
		{
			ScratchEngine.Actions.ShowMessage(message, duration);
		});

		public static IScratchBlock PlaySound(String soundName, Double volume = 1.0f) => new ExecuteBlock(() =>
		{
			ScratchEngine.Actions.PlaySound(soundName, volume);
		});

		// OBJECT
		public static IScratchBlock Disable(IEngineObject obj) => new ExecuteBlock(() => obj.SetEnabled(false));

		public static IScratchBlock Enable(IEngineObject obj) => new ExecuteBlock(() => obj.SetEnabled(true));

		// CONTROL
		public static IScratchBlock Wait(Double seconds) => new WaitBlock(seconds);
		public static IScratchBlock RepeatForever(params IScratchBlock[] blocks) => new RepeatForeverBlock(new List<IScratchBlock>(blocks));

		public static IScratchBlock RepeatForever(Action block)
		{
			var blocks = new List<IScratchBlock>();
			blocks.Add(new ExecuteBlock(block));
			return new RepeatForeverBlock(blocks);
		}

		public static IScratchBlock RepeatWhileTrue(Func<Boolean> condition, params IScratchBlock[] blocks) =>
			new RepeatWhileTrueBlock(condition, new List<IScratchBlock>(blocks));

		public static IScratchBlock RepeatUntilTrue(Func<Boolean> condition, params IScratchBlock[] blocks) =>
			new RepeatUntilTrueBlock(condition, new List<IScratchBlock>(blocks));
	}
}
