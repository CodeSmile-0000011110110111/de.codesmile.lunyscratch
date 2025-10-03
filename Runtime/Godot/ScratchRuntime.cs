#if GODOT
using Godot;
using System;

namespace LunyScratch
{
	/// <summary>
	/// Godot runtime singleton that manages step execution.
	/// Auto-initializes when first accessed.
	/// </summary>
	public sealed partial class ScratchRuntime : Node, IEngineRuntime
	{
		private static ScratchRuntime s_Instance;

		private readonly BlockRunner _blockRunner = new();

		public static ScratchRuntime Instance => s_Instance;

		internal static void Initialize()
		{
			if (s_Instance != null)
				return;

			// Create runtime node and add to scene tree
			s_Instance = new ScratchRuntime();
			s_Instance.Name = nameof(ScratchRuntime);

			// Add to the root to persist across scenes
			var root = Engine.GetMainLoop() as SceneTree;
			root.Root.CallDeferred(Node.MethodName.AddChild, s_Instance);

			// Initialize the engine abstraction
			GameEngine.Initialize(s_Instance, new GodotActions());
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
