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
