#if UNITY_6000_0_OR_NEWER
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace LunyScratch
{
	[DefaultExecutionOrder(Int16.MinValue)]
	[AddComponentMenu("GameObject/")] // Do not list in "Add Component" menu
	[DisallowMultipleComponent]
	public sealed class UnityScratchRuntime : MonoBehaviour, IScratchRuntime
	{
		private static UnityScratchRuntime s_Instance;
		private static Boolean s_Initialized;

		private readonly ScratchBlocks _scratchBlocks = new();

		public static UnityScratchRuntime Instance => s_Instance;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void Initialize()
		{
			if (s_Initialized == false)
			{
				s_Initialized = true;

				var go = new GameObject(nameof(UnityScratchRuntime));
				s_Instance = go.AddComponent<UnityScratchRuntime>();
				DontDestroyOnLoad(go);

				ScratchEngine.Initialize(s_Instance, new UnityScratchActions(s_Instance));
			}
			else
				throw new Exception($"{nameof(UnityScratchRuntime)}: attempt to duplicate singleton!");
		}

		private void Update()
		{
			var deltaTimeInSeconds = Time.deltaTime;
			_scratchBlocks.Process(deltaTimeInSeconds);
		}

		private void OnDestroy()
		{
			_scratchBlocks.Dispose();
			s_Instance = null;
			s_Initialized = false;
		}

		public void RunBlock(IScratchBlock block) => _scratchBlocks.Run(block);
	}
}
#endif
