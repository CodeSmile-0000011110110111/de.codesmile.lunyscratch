#if GODOT
using Godot;
using System;

namespace LunyScratch
{
	/// <summary>
	/// Godot runtime singleton that manages step execution.
	/// Auto-initializes when first accessed.
	/// </summary>
	public sealed partial class GodotScratchRuntime : Node, IScratchRuntime
	{
		private static GodotScratchRuntime s_Instance;

		private readonly ScratchBlockRunner _blockRunner = new();

		public static GodotScratchRuntime Instance => s_Instance;

		internal static void Initialize()
		{
			if (s_Instance != null)
				return;

			// Create runtime node and add to scene tree
			s_Instance = new GodotScratchRuntime();
			s_Instance.Name = nameof(GodotScratchRuntime);

			// Add to the root to persist across scenes
			var root = Engine.GetMainLoop() as SceneTree;
			root.Root.CallDeferred(Node.MethodName.AddChild, s_Instance);

			// Initialize the engine abstraction
			ScratchEngine.Initialize(s_Instance, new GodotScratchActions());
		}

		public void RunBlock(IScratchBlock block) => _blockRunner.AddBlock(block);

		public override void _Process(Double deltaTimeInSeconds) => _blockRunner.Process(deltaTimeInSeconds);

		public override void _ExitTree()
		{
			_blockRunner.Dispose();
			s_Instance = null;
		}
	}
}
#endif
