#if GODOT
using Godot;

namespace LunyScratch;

public sealed partial class GodotScratchActions
{
	public static IStep Enable(GodotObject obj) => ScratchActions.Enable(new GodotEngineObject(obj));

	public static IStep Disable(GodotObject obj) => ScratchActions.Disable(new GodotEngineObject(obj));
}
#endif
