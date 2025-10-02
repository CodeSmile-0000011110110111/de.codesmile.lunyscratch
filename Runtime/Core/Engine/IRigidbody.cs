// ScratchActor.cs

namespace LunyScratch
{
	public interface IRigidbody
	{
		IVector3 LinearVelocity { get; set; }
		IVector3 Position { get; set; }
	}
}
