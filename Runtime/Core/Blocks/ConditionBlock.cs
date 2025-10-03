// Copyright (C) 2021-2025 Steffen Itterheim
// Refer to included LICENSE file for terms and conditions.

using System;

namespace LunyScratch
{
	// Conditional step (checks condition before proceeding)
	public sealed class ConditionBlock : IScratchBlock
	{
		private readonly Func<Boolean> _condition;
		private Boolean _result;

		public ConditionBlock(Func<Boolean> condition)
		{
			_condition = condition;
			_result = false;
		}

		public void OnEnter() => _result = false;

		public void Run(Double deltaTimeInSeconds) => _result = _condition();

		public Boolean IsComplete() => _result;
	}
}
