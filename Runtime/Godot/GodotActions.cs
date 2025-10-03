#if GODOT
using Godot;
using System;

namespace LunyScratch
{
	internal sealed class GodotActions : IEngineActions
	{
		public void Log(String message) => GD.Print(message);

		public void ShowMessage(String message, Double duration) => GD.Print($"[Message] {message}");

		// TODO: Implement UI message display if needed
		// Could use a Label or RichTextLabel in the scene
		public void PlaySound(String soundName, Double volume) => GD.Print($"[Sound] {soundName} at volume {volume}");

		// TODO: Implement sound playback
		// Could use AudioStreamPlayer with loaded AudioStream resources
		public Double GetDeltaTimeInSeconds() => ScratchRuntime.Instance.GetProcessDeltaTime();

		public Double GetCurrentTimeInSeconds() => Time.GetTicksMsec() / 1000.0;
	}
}
#endif
