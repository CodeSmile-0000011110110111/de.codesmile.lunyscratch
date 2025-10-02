// Copyright (C) 2021-2025 Steffen Itterheim
// Refer to included LICENSE file for terms and conditions.

using System;

namespace LunyScratch
{
	// Wait step (delays for N seconds)
	public sealed class WaitStep : IStep
	{
		private readonly Double _duration;
		private Double _startTime;

		public WaitStep(Double duration)
		{
			_duration = duration;
			_startTime = 0;
		}

		public void OnEnter() => _startTime = GameEngine.Current.GetCurrentTime();
		public void OnExit() {}

		public void Execute() {} // No accumulation needed

		public Boolean IsComplete() => GameEngine.Current.GetCurrentTime() >= _startTime + _duration;
	}
}
