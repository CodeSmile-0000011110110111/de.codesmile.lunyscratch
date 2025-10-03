#if GODOT
using System;
using System.Collections.Generic;
using Godot;

namespace LunyScratch
{
	/// <summary>
	/// Godot runtime singleton that manages step execution.
	/// Auto-initializes when first accessed.
	/// </summary>
	public partial class GodotScratchRuntime : Node
	{
		private static GodotScratchRuntime s_Instance;
		private readonly List<IScratchBlock> _blocks = new();

		// Self-initializing singleton property
		public static GodotScratchRuntime Instance
		{
			get
			{
				if (s_Instance == null)
				{
					// Create runtime node and add to scene tree
					s_Instance = new GodotScratchRuntime();
					s_Instance.Name = "ScratchRuntime";
					
					// Add to the root to persist across scenes
					var root = Engine.GetMainLoop() as SceneTree;
					root?.Root.CallDeferred(Node.MethodName.AddChild, s_Instance);
					
					// Initialize the engine abstraction
					GameEngine.Initialize(new GodotScratchActions(s_Instance));
				}
				return s_Instance;
			}
		}

		public override void _Ready()
		{
			if (s_Instance != null && s_Instance != this)
			{
				QueueFree();
				return;
			}

			s_Instance = this;
			
			// Initialize if not already done
			if (GameEngine.Current == null)
			{
				GameEngine.Initialize(new GodotScratchActions(this));
			}
		}

		public override void _Process(Double delta)
		{
			// Execute all active FSM blocks
			for (var i = _blocks.Count - 1; i >= 0; i--)
			{
				var block = _blocks[i];
				block.Run();

				if (block.IsComplete())
				{
					block.OnExit();
					_blocks.RemoveAt(i);
				}
			}
		}

		public override void _ExitTree()
		{
			if (s_Instance == this)
				s_Instance = null;
		}

		public void RunBlock(IScratchBlock block)
		{
			block.OnEnter();
			_blocks.Add(block);
		}
	}
}
#endif
