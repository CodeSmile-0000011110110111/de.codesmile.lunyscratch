#if UNREAL
using UnrealSharp.CoreUObject;
using UnrealSharp.Engine;

namespace LunyScratch;

public static class UnrealScratchActionsExtensions
{
	public static UnrealScratchActions GetActions(this AActor actor) => new(actor);

	public static void MoveTo(this UnrealScratchActions actions, FVector position)
	{
		if (actions.EngineObject is AActor actor)
			actor.SetActorLocation(position);
	}

	public static void LookAt(this UnrealScratchActions actions, FVector target)
	{
		throw new NotImplementedException();
		// if (actions.EngineObject.GetEngineObject() is AActor actor)
		// {
		// 	var direction = (target - actor.ActorLocation).GetNormalized();
		// 	var rotation = direction.Rotation();
		// 	actor.SetActorRotation(rotation);
		// }
	}

	public static FVector GetPosition(this UnrealScratchActions actions)
	{
		if (actions.EngineObject is AActor actor)
			return actor.ActorLocation;

		return FVector.Zero;
	}

	public static FRotator GetRotation(this UnrealScratchActions actions)
	{
		if (actions.EngineObject is AActor actor)
			return actor.ActorRotation;

		return FRotator.ZeroRotator;
	}
}
#endif
