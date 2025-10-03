// IScratchEngine.cs
using System;

namespace LunyScratch
{
	internal static class GameEngine
	{
		private static IEngineRuntime s_Runtime;
		private static IEngineActions s_Actions;

		internal static void Initialize(IEngineRuntime runtime, IEngineActions actions)
		{
			s_Runtime = runtime;
			s_Actions = actions;
		}

		internal static IEngineRuntime Runtime => s_Runtime;
		internal static IEngineActions Actions => s_Actions ?? throw new Exception("Scratch Engine: Actions not initialized");
	}
}
