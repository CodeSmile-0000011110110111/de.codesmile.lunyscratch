#if GODOT
using Godot;

namespace LunyScratch
{
	public sealed partial class GodotScratchActions
	{
		public static IScratchBlock Enable(Godot.GodotObject obj) => ScratchActions.Enable(new GodotEngineObject(obj));

		public static IScratchBlock Disable(Godot.GodotObject obj) => ScratchActions.Disable(new GodotEngineObject(obj));
	}
#endif
}
