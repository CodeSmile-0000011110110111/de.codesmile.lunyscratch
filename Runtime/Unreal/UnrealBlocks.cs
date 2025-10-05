#if UNREAL
using UnrealSharp.CoreUObject;
using UnrealSharp.Engine;

namespace LunyScratch;

public static class UnrealBlocks
{
	public static IScratchBlock Enable(USceneComponent obj) => Blocks.Enable(new UnrealEngineObject(obj));
	public static IScratchBlock Disable(USceneComponent obj) => Blocks.Disable(new UnrealEngineObject(obj));
}
#endif
