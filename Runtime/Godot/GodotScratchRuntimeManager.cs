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
		private readonly List<IStep> m_Steps = new();

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
			// Execute all active FSM steps
			for (var i = m_Steps.Count - 1; i >= 0; i--)
			{
				var step = m_Steps[i];
				step.Execute();

				if (step.IsComplete())
				{
					step.OnExit();
					m_Steps.RemoveAt(i);
				}
			}
		}

		public override void _ExitTree()
		{
			if (s_Instance == this)
				s_Instance = null;
		}

		// Public method for engine to register steps
		public void RegisterStep(IStep step)
		{
			step.OnEnter();
			m_Steps.Add(step);
		}
	}
}
#endif
