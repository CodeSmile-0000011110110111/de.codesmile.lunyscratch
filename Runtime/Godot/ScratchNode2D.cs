#if GODOT
using Godot;

namespace LunyScratch;

/// <summary>
/// Base class for all Scratch-style behaviors in Godot.
/// Automatically initializes GodotScratchRuntime on first use.
/// </summary>
public partial class ScratchNode2D : Node2D
{
	public override void _Ready()
	{
		// Trigger GodotScratchRuntime initialization
		_ = GodotScratchRuntime.Instance;
	}
}
#endif
