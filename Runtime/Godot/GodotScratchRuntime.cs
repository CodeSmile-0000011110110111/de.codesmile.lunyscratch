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

		private readonly ScratchBlocks _scratchBlocks = new();

		// Self-initializing singleton property
		public static GodotScratchRuntime Instance => s_Instance ??= Initialize();

		private static GodotScratchRuntime Initialize()
		{
			// Create runtime node and add to scene tree
			var instance = new GodotScratchRuntime();
			instance.Name = nameof(GodotScratchRuntime);

			// Add to the root to persist across scenes
			var root = Engine.GetMainLoop() as SceneTree;
			root?.Root.CallDeferred(Node.MethodName.AddChild, instance);

			// Initialize the engine abstraction
			ScratchEngine.Initialize(instance, new GodotScratchActions(instance));

			return instance;
		}

		public override void _Process(Double deltaTime)
		{
			var deltaTimeInSeconds = (Single)deltaTime;
			_scratchBlocks.Process(deltaTimeInSeconds);
		}

		public override void _ExitTree()
		{
			_scratchBlocks.Dispose();
			s_Instance = null;
		}

		public void RunBlock(IScratchBlock block) => _scratchBlocks.Run(block);
	}
}
#endif
