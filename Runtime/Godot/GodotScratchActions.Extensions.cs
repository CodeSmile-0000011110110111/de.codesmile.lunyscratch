#if GODOT
using Godot;

namespace LunyScratch;

public sealed partial class GodotScratchActions
{
	public static IScratchBlock Enable(GodotObject obj) => ScratchActions.Enable(new GodotEngineObject(obj));

	public static IScratchBlock Disable(GodotObject obj) => ScratchActions.Disable(new GodotEngineObject(obj));
}
#endif
