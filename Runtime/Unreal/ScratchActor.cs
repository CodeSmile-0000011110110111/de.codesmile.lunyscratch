#if UNREAL
using UnrealSharp.Attributes;
using UnrealSharp.Engine;

namespace LunyScratch;

// FIXME: UnrealSharp currently doesn't allow abstract AActor subclassing => https://github.com/UnrealSharp/UnrealSharp/issues/560
[UClass]
public /*abstract*/ class AScratchActor : AActor
{
	protected override void BeginPlay()
	{
		base.BeginPlay();
		OnReady();
	}

	public override void Tick(Single deltaTime)
	{
		base.Tick(deltaTime);
		OnUpdate(deltaTime);
	}

	protected virtual void OnReady() {}
	protected virtual void OnUpdate(Single deltaTime) {}
}
#endif
