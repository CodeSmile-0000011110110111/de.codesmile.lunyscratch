// Copyright (C) 2021-2025 Steffen Itterheim
// Refer to included LICENSE file for terms and conditions.

using System;

namespace LunyScratch
{
	public interface IScratchBlock
	{
		void OnCreate() {}
		void OnDestroy() {}
		void OnEnter();
		void OnExit();

		void Run(Double deltaTimeInSeconds);
		Boolean IsComplete();
	}
}
