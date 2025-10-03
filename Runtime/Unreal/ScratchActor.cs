#if UNREAL
using UnrealSharp.Engine;

namespace LunyScratch;

public abstract class ScratchActor : AActor
{
	protected UnrealScratchActions Actions { get; private set; }

	protected override void BeginPlay()
	{
		PrintString($"====> {nameof(ScratchActor)} BeginPlay()");

		base.BeginPlay();
		Actions = new UnrealScratchActions(this);
		OnStart();
	}

	public override void Tick(Single deltaTime)
	{
		PrintString($"====> {nameof(ScratchActor)} Tick({deltaTime})");
		base.Tick(deltaTime);
		OnUpdate(deltaTime);
	}

	protected abstract void OnStart();
	protected abstract void OnUpdate(Single deltaTime);
}
#endif
