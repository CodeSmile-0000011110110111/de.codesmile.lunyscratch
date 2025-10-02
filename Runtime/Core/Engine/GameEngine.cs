// IScratchEngine.cs
using System;

namespace LunyScratch
{
	public static class GameEngine
	{
		private static IScratchActions s_Instance;

		public static void Initialize(IScratchActions actions) => s_Instance = actions;

		public static IScratchActions Current => s_Instance ?? throw new Exception("Scratch Engine not initialized");
	}
}
