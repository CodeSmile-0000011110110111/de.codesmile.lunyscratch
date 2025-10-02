#if UNREAL
using UnrealSharp.CoreUObject;

namespace LunyScratch;

public class UnrealEngineObject : IGameEngineObject
{
	private readonly UObject _unrealObject;

	public UnrealEngineObject(UObject unrealObject) => _unrealObject = unrealObject;
	public void SetEnabled(Boolean enabled) => throw new NotImplementedException();

	public Object GetEngineObject() => _unrealObject;
}
#endif
