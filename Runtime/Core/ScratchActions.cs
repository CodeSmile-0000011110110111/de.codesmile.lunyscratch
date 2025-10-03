// Copyright (C) 2021-2025 Steffen Itterheim
// Refer to included LICENSE file for terms and conditions.

using System;
using System.Collections.Generic;

namespace LunyScratch
{
	public static class ScratchActions
	{
		public static IScratchBlock Say(String message, Double duration = 0) => new ExecuteBlock(() =>
		{
			GameEngine.Current.ShowMessage(message, (Single)duration);
		});

		public static IScratchBlock PlaySound(String soundName, Double volume = 1.0f) => new ExecuteBlock(() =>
		{
			GameEngine.Current.PlaySound(soundName, (Single)volume);
		});

		public static IScratchBlock Wait(Double seconds) => new WaitBlock((Single)seconds);

		public static IScratchBlock Disable(IGameEngineObject obj) => new ExecuteBlock(() => obj.SetEnabled(false));

		public static IScratchBlock Enable(IGameEngineObject obj) => new ExecuteBlock(() => obj.SetEnabled(true));

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
