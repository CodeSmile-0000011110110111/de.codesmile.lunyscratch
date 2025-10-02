#if UNREAL
using UnrealSharp.CoreUObject;
using UnrealSharp.Engine;

namespace LunyScratch;

public sealed class UnrealScratchActions : IScratchActions
{
	private readonly AActor _actor;
	private readonly Queue<Action> _actionQueue = new();

	public IGameEngineObject EngineObject => new UnrealEngineObject(_actor);

	public UnrealScratchActions(AActor actor) => _actor = actor;

	public void Log(String message) => UObject.PrintString(message, printToScreen:false);

	public void ShowMessage(String message, Single duration) => UObject.PrintString(message, duration:duration);

	public void PlaySound(String soundName, Single volume) => throw new NotImplementedException();

	public Single GetDeltaTime() => throw new NotImplementedException();

	public Double GetCurrentTime() => _actor.GameTimeSinceCreation;

	public void RunStep(IStep step) => throw new NotImplementedException();

	public void Wait(Single seconds)
	{
		var startTime = _actor.GameTimeSinceCreation;
		_actionQueue.Enqueue(() =>
		{
			if (_actor.GameTimeSinceCreation - startTime >= seconds)
				return;

			_actionQueue.Enqueue(_actionQueue.Dequeue());
		});
	}

	public void MoveBy(Single x, Single y, Single z)
	{
		var location = _actor.ActorLocation;
		location.X += x;
		location.Y += y;
		location.Z += z;
		_actor.SetActorLocation(location);
	}

	public void RotateBy(Single pitch, Single yaw, Single roll)
	{
		var rotation = _actor.ActorRotation;
		rotation.Pitch += pitch;
		rotation.Yaw += yaw;
		rotation.Roll += roll;
		_actor.SetActorRotation(rotation);
	}

	public void SetPosition(Single x, Single y, Single z) => _actor.SetActorLocation(new FVector(x, y, z));

	public void SetRotation(Single pitch, Single yaw, Single roll) => _actor.SetActorRotation(new FRotator(pitch, yaw, roll));

	public void Execute()
	{
		if (_actionQueue.Count > 0)
		{
			var action = _actionQueue.Dequeue();
			action?.Invoke();
		}
	}
}
#endif
