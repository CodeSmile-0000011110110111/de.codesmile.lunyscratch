// Copyright (C) 2021-2025 Steffen Itterheim
// Refer to included LICENSE file for terms and conditions.

using System;

namespace LunyScratch
{
	public interface IScratchActions
	{
		void Log(String message);
		void ShowMessage(String message, Single duration);
		void PlaySound(String soundName, Single volume);
		Single GetDeltaTime();
		Double GetCurrentTime();
	}
}
