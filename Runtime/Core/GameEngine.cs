// IScratchEngine.cs
using System;

namespace LunyScratch
{
	public static class GameEngine
	{
		private static IEngineRuntime s_Runtime;
		private static IEngineActions s_Actions;

		public static void Initialize(IEngineRuntime runtime, IEngineActions actions)
		{
			s_Runtime = runtime;
			s_Actions = actions;
		}

		public static IEngineRuntime Runtime => s_Runtime;
		public static IEngineActions Actions => s_Actions ?? throw new Exception("Scratch Engine: Actions not initialized");
	}
}
