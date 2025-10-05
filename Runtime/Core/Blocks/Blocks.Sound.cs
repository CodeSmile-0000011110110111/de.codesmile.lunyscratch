
using System;

namespace LunyScratch
{
	public static partial class Blocks
	{
		public static IScratchBlock PlaySound(String soundName, Double volume = 1.0f) => new ExecuteBlock(() =>
		{
			GameEngine.Actions.PlaySound(soundName, volume);
		});
	}
}
