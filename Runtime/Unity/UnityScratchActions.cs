#if UNITY_6000_0_OR_NEWER
using System;
using UnityEngine;

namespace LunyScratch
{
	public sealed partial class UnityScratchActions : IScratchActions
	{
		private readonly ScratchRuntime _runtime;

		public UnityScratchActions(ScratchRuntime runtime) => _runtime = runtime;

		public Double GetCurrentTime() => Time.time;

		public void RunStep(IStep step) => _runtime.RunStep(step);

		public void Log(String message) => Debug.Log(message);

		public void ShowMessage(String message, Single duration) => Debug.Log($"[Say] {message}");

		public void PlaySound(String soundName, Single volume) => Debug.Log($"[PlaySound] {soundName} @ {volume}");

		public Single GetDeltaTime() => Time.deltaTime;

	}
}
#endif
