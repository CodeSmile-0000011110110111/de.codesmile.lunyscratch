#if UNREAL
using UnrealSharp.Attributes;
using UnrealSharp.UnrealSharpCore;

namespace LunyScratch;

[UClass]
public sealed class UScratchRuntime : UCSGameInstanceSubsystem, IEngineRuntime
{
	private static UScratchRuntime s_Instance;

	private readonly BlockRunner _blockRunner = new();

	public static UScratchRuntime Instance => s_Instance;

	protected override void Initialize(FSubsystemCollectionBaseRef collection)
	{
		base.Initialize(collection);

		if (s_Instance != null)
			throw new Exception($"{nameof(UScratchRuntime)} singleton duplication");

		s_Instance = this;
		GameEngine.Initialize(s_Instance, new UnrealActions());

		IsTickable = true;
	}

	public override void Dispose()
	{
		base.Dispose();
		GameEngine.Dispose();
		s_Instance = null;
	}

	protected override void Tick(Single deltaTime)
	{
		_blockRunner.Process(deltaTime);
	}

	public void RunBlock(IScratchBlock block) => _blockRunner.AddBlock(block);
}
#endif
