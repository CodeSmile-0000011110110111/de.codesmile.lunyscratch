#if GODOT
using System;
using Godot;

namespace LunyScratch
{
	public sealed partial class GodotScratchActions : IScratchActions
	{
		private readonly GodotScratchRuntime _runtime;

		public GodotScratchActions(GodotScratchRuntime runtime)
		{
			_runtime = runtime;
		}

		public void Log(String message)
		{
			GD.Print(message);
		}

		public void ShowMessage(String message, Single duration)
		{
			GD.Print($"[Message] {message}");
			// TODO: Implement UI message display if needed
			// Could use a Label or RichTextLabel in the scene
		}

		public void PlaySound(String soundName, Single volume)
		{
			GD.Print($"[Sound] {soundName} at volume {volume}");
			// TODO: Implement sound playback
			// Could use AudioStreamPlayer with loaded AudioStream resources
		}

		public Single GetDeltaTime() => (Single)_runtime.GetProcessDeltaTime();

		public Double GetCurrentTime() => Time.GetTicksMsec() / 1000.0;
	}
}
#endif
