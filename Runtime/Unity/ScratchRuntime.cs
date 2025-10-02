#if UNITY_6000_0_OR_NEWER
using System;
using System.Collections.Generic;
using UnityEngine;

namespace LunyScratch
{
	[DefaultExecutionOrder(Int16.MinValue)]
	[AddComponentMenu("GameObject/")] // Do not list in "Add Component" menu
	[DisallowMultipleComponent]
	public sealed class ScratchRuntime : MonoBehaviour
	{
		private static ScratchRuntime s_Instance;
		private static Boolean s_Initialized;

		private readonly List<IStep> _steps = new();

		public static ScratchRuntime Instance => s_Instance;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void Initialize()
		{
			if (s_Initialized == false)
			{
				s_Initialized = true;

				var go = new GameObject(nameof(ScratchRuntime));
				s_Instance = go.AddComponent<ScratchRuntime>();
				DontDestroyOnLoad(go);

				GameEngine.Initialize(new UnityScratchActions(s_Instance));
			}
			else
				throw new Exception($"{nameof(ScratchRuntime)}: attempt to duplicate singleton!");
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
