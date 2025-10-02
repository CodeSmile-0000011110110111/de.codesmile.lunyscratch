#if UNREAL
using UnrealSharp.Engine;

namespace LunyScratch;

public sealed class UnrealScratchRuntime : UGameInstanceSubsystem
{
	private static UnrealScratchRuntime _instance;
	private readonly List<IStep> _steps = new();

	public static UnrealScratchRuntime Instance => _instance;

	public UnrealScratchRuntime()
	{
		if (_instance != null)
			throw new Exception($"{nameof(UnrealScratchRuntime)} singleton duplication");

		_instance = this;
	}

	public override void Dispose()
	{
		base.Dispose();
		_instance = null;
	}

	public void RunStep(IStep steps)
	{
		if (!_steps.Contains(steps))
			_steps.Add(steps);
	}

	public void Tick(Single deltaTime)
	{
		foreach (var actions in _steps)
			actions.Execute();
	}
}
#endif
