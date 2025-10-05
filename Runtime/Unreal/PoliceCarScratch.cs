#if UNREAL
using LunyScratch;
using UnrealSharp.Attributes;
using UnrealSharp.CoreUObject;
using UnrealSharp.Engine;

using static LunyScratch.Blocks;
using static LunyScratch.UnrealBlocks;

namespace ManagedScratchTest10;

[UClass]
public /*abstract*/ class AScratchActor2 : AActor
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

[UClass]
public class APoliceCarScratch : AScratchActor2
{
	private Single _accelerateDelay = 1.2f;
	private Single _acceleration = 0.312f;
	private Single _maxSpeed = 2f;
	private Single _stopX = 9f;
	private Single _stopVelocitySlowdown = 0.95f;
	private Single _stopVelocityY = -1f;

	private Single _speed;
	private FVector _heading;
	private UPrimitiveComponent _car;

	protected override void BeginPlay()
	{
		base.BeginPlay();

		ActorTickEnabled = true;
		_car = GetComponentByClass<UPrimitiveComponent>();
		_heading = ActorForwardVector;


		Scratch.Run(
			Wait(_accelerateDelay),
			RepeatForever(MoveCar)
		);

		_car.GetChildrenComponents(true, out var lights);
		Scratch.RepeatForever(
			Enable(lights[0]),
			Disable(lights[1]),
			Wait(0.12),
			Disable(lights[0]),
			Enable(lights[1]),
			Wait(0.1)
		);
	}


	private void MoveCar()
	{
		//PrintString($"MoveCar: {_car}");


		// if (transform.position.x < _stopX)
		// {
		// 	_speed = 0f;
		// 	var velocity = _rigidbody.linearVelocity;
		// 	velocity *= _stopVelocitySlowdown;
		// 	velocity.y = -_stopVelocityY;
		// 	_rigidbody.linearVelocity = velocity;
		// }
		// else if (_speed < _maxSpeed)
		 {
		 	_speed += _acceleration;
		    var velocity = _car.GetPhysicsLinearVelocity();
		    _car.SetPhysicsLinearVelocity(velocity + _heading * _speed);
		 }
	}
}
#endif
