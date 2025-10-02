// Copyright (C) 2021-2025 Steffen Itterheim
// Refer to included LICENSE file for terms and conditions.

using System;
using System.Collections.Generic;

namespace LunyScratch
{
	public static class ScratchActions
	{
		public static IStep Say(String message, Double duration = 0) => new ActionStep(() =>
		{
			GameEngine.Current.ShowMessage(message, (Single)duration);
		});

		public static IStep PlaySound(String soundName, Double volume = 1.0f) => new ActionStep(() =>
		{
			GameEngine.Current.PlaySound(soundName, (Single)volume);
		});

		public static IStep Wait(Double seconds) => new WaitStep((Single)seconds);

		public static IStep Disable(IGameEngineObject obj) => new ActionStep(() => obj.SetEnabled(false));

		public static IStep Enable(IGameEngineObject obj) => new ActionStep(() => obj.SetEnabled(true));

		public static IStep RepeatForever(params IStep[] steps) => new RepeatForeverStep(new List<IStep>(steps));
		
		public static IStep RepeatForever(Action step)
		{
			var steps = new List<IStep>();
			steps.Add(new ActionStep(step));
			return new RepeatForeverStep(steps);
		}

		public static IStep RepeatWhileTrue(Func<Boolean> condition, params IStep[] steps) =>
			new RepeatWhileTrueStep(condition, new List<IStep>(steps));

		public static IStep RepeatUntilTrue(Func<Boolean> condition, params IStep[] steps) =>
			new RepeatUntilTrueStep(condition, new List<IStep>(steps));
	}
}
