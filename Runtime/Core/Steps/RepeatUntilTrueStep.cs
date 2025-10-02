// Copyright (C) 2021-2025 Steffen Itterheim
// Refer to included LICENSE file for terms and conditions.

using System;
using System.Collections.Generic;

namespace LunyScratch
{
	// Repeat until condition becomes true
	public sealed class RepeatUntilTrueStep : RepeatStepBase
	{
		private readonly Func<Boolean> _condition;

		public RepeatUntilTrueStep(Func<Boolean> condition, List<IStep> steps)
			: base(steps) => _condition = condition;

		protected override Boolean ShouldExitLoop() => _condition(); // Exit when condition becomes true
	}
}
