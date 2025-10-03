// Copyright (C) 2021-2025 Steffen Itterheim
// Refer to included LICENSE file for terms and conditions.

using System;

namespace LunyScratch
{
	internal interface IEngineActions
	{
		void Log(String message);
		void ShowMessage(String message, Double duration);
		void PlaySound(String soundName, Double volume);
		Double GetDeltaTimeInSeconds();
		Double GetCurrentTimeInSeconds();
	}
}
