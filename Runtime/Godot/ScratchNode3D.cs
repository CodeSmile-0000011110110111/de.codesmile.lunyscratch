#if GODOT
using Godot;

namespace LunyScratch
{
	/// <summary>
	/// Base class for all Scratch-style behaviors in Godot.
	/// Automatically initializes GodotScratchRuntime on first use.
	/// </summary>
	public partial class ScratchNode3D : Node3D
	{
		public override void _Ready() => GodotScratchRuntime.Initialize();
	}
}
#endif
