// Copyright (C) 2021-2025 Steffen Itterheim
// Refer to included LICENSE file for terms and conditions.

using System;

namespace LunyScratch
{
	// Wait block (delays for N seconds)
	public sealed class WaitBlock : IScratchBlock
	{
		private readonly Double _duration;
		private Double _startTime;

		public WaitBlock(Double duration)
		{
			_duration = duration;
			_startTime = 0;
		}

		public void OnEnter() => _startTime = GameEngine.Actions.GetCurrentTimeInSeconds();
		public void Run(Double deltaTimeInSeconds) {}
		public Boolean IsComplete() => GameEngine.Actions.GetCurrentTimeInSeconds() >= _startTime + _duration;
	}
}
