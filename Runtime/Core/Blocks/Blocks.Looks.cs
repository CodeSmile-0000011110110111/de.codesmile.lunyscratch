
using System;

namespace LunyScratch
{
	public static partial class Blocks
	{
		public static IScratchBlock Say(String message, Double duration = 0) => new ExecuteBlock(() =>
		{
			GameEngine.Actions.ShowMessage(message, duration);
		});
	}
}
