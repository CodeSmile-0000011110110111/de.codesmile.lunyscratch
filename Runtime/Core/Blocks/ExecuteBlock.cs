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

		public void Run(Double deltaTimeInSeconds)
		{
			if (!_executed)
			{
				_action?.Invoke();
				_executed = true;
			}
		}

		public Boolean IsComplete() => _executed;
	}
}
