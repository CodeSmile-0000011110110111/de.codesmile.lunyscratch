// IScratchEngine.cs
using System;

namespace LunyScratch
{
	public static class ScratchEngine
	{
		private static IScratchRuntime s_Runtime;
		private static IScratchActions s_Actions;

		public static void Initialize(IScratchRuntime runtime, IScratchActions actions)
		{
			s_Runtime = runtime;
			s_Actions = actions;
		}

		public static IScratchRuntime Runtime => s_Runtime;
		public static IScratchActions Actions => s_Actions ?? throw new Exception("Scratch Engine: Actions not initialized");
	}
}
