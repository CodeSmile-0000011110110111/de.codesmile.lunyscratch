#if UNITY_6000_0_OR_NEWER
using System;
using UnityEngine;

namespace LunyScratch
{
	internal sealed class UnityActions : IEngineActions
	{
		public Double GetCurrentTimeInSeconds() => Time.time;
		public Double GetDeltaTimeInSeconds() => Time.deltaTime;

		public void Log(String message) => Debug.Log(message);

		public void ShowMessage(String message, Double duration) => Debug.Log($"[Say] {message}");

		public void PlaySound(String soundName, Double volume) => Debug.Log($"[PlaySound] {soundName} @ {volume}");
	}
}
#endif
