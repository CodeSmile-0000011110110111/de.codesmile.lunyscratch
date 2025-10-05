#if UNREAL
using UnrealSharp.CoreUObject;
using UnrealSharp.Engine;

namespace LunyScratch;

public class UnrealEngineObject : IEngineObject
{
	private readonly UObject _engineObject;

	public static implicit operator UnrealEngineObject(UObject unrealObject) => new(unrealObject);

	public UnrealEngineObject(UObject unrealObject) => _engineObject = unrealObject;
	public void SetEnabled(Boolean enabled)
	{
		switch (_engineObject)
		{
			case AActor actor:
				actor.ActorTickEnabled = enabled;
				break;

			case USceneComponent sceneComponent:
				sceneComponent.SetHiddenInGame(!enabled);
				break;

			default:
				throw new ArgumentOutOfRangeException(nameof(_engineObject), _engineObject, null);
		}
	}

	public Object GetEngineObject() => _engineObject;
}
#endif
