#if UNITY_6000_0_OR_NEWER
using System;
using System.Collections.Generic;
using UnityEngine;

namespace LunyScratch
{
	[DefaultExecutionOrder(Int16.MinValue)]
	[AddComponentMenu("GameObject/")] // Do not list in "Add Component" menu
	[DisallowMultipleComponent]
	public sealed class UnityScratchRuntime : MonoBehaviour
	{
		private static UnityScratchRuntime s_Instance;
		private static Boolean s_Initialized;

		private readonly List<IStep> _steps = new();

		// Self-initializing singleton property
		public static UnityScratchRuntime Instance => s_Initialized ? s_Instance : CreateScratchRuntimeObjectAndComponent();

		public static void Initialize()
		{
			if (s_Initialized == false)
				CreateScratchRuntimeObjectAndComponent();
		}

		private static UnityScratchRuntime CreateScratchRuntimeObjectAndComponent()
		{
			// Create a new GameObject with ScratchRuntime component
			var go = new GameObject(nameof(UnityScratchRuntime));
			s_Instance = go.AddComponent<UnityScratchRuntime>();
			s_Initialized = true;
			DontDestroyOnLoad(go);

			// Initialize the engine abstraction
			GameEngine.Initialize(new UnityScratchActions(s_Instance));

			return s_Instance;
		}

		private void Awake()
		{
			if (s_Instance != null)
				throw new Exception($"{gameObject.name} ({gameObject.GetInstanceID()}) adds {nameof(UnityScratchRuntime)} duplicate!");
		}

		private void Update()
		{
			// Execute all active FSM steps
			for (var i = _steps.Count - 1; i >= 0; i--)
			{
				var step = _steps[i];
				step.Execute();

				if (step.IsComplete())
				{
					step.OnExit();
					_steps.RemoveAt(i);
				}
			}
		}

		private void OnDestroy()
		{
			_steps.Clear();
			s_Instance = null;
			s_Initialized = false;
		}

		// Register a new step sequence
		public void RunStep(IStep step)
		{
			step.OnEnter();
			Instance._steps.Add(step);
		}
	}
}
#endif
