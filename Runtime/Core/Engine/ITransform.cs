namespace LunyScratch
{
	public interface ITransform
	{
		IVector3 Position { get; set; }
		IVector3 Forward { get; }
	}
}
