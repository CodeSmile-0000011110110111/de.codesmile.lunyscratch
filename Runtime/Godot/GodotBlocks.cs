#if GODOT
using Godot;

namespace LunyScratch
{
	public static class GodotBlocks
	{
		public static IScratchBlock Enable(GodotObject obj) => Blocks.Enable(new GodotEngineObject(obj));

		public static IScratchBlock Disable(GodotObject obj) => Blocks.Disable(new GodotEngineObject(obj));
	}
#endif
}
