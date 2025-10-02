// Copyright (C) 2021-2025 Steffen Itterheim
// Refer to included LICENSE file for terms and conditions.

using System;
using System.Collections.Generic;

namespace LunyScratch
{
	// Repeat forever step
	public sealed class RepeatForeverStep : RepeatStepBase
	{
		public RepeatForeverStep(List<IStep> steps)
			: base(steps) {}

		protected override Boolean ShouldExitLoop() => false; // Never exits
	}
}
