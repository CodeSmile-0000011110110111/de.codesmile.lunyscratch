// Copyright (C) 2021-2025 Steffen Itterheim
// Refer to included LICENSE file for terms and conditions.

using System;

namespace LunyScratch
{
	// Action step (executes immediately)
	public sealed class ExecuteBlock : IScratchBlock
	{
		private readonly Action _action;
		private Boolean _executed;

		public ExecuteBlock(Action action)
		{
			_action = action;
			_executed = false;
		}
		public void OnEnter() => _executed = false;

		public void Run()
		{
			if (!_executed)
			{
				_action?.Invoke();
				_executed = true;
			}
		}

		public void OnExit() {}
		public Boolean IsComplete() => _executed;
	}
}
