#if UNITY_6000_0_OR_NEWER
using System;
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

		private readonly ScratchBlockRunner _blockRunner = new();

		public static UnityScratchRuntime Instance => s_Instance;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void Initialize()
		{
			if (s_Initialized)
				return;

			s_Initialized = true;
			
			var go = new GameObject(nameof(UnityScratchRuntime));
			s_Instance = go.AddComponent<UnityScratchRuntime>();
			DontDestroyOnLoad(go);

			ScratchEngine.Initialize(s_Instance, new UnityScratchActions());
		}

		public void RunBlock(IScratchBlock block) => _blockRunner.AddBlock(block);

		private void Update()
		{
			var deltaTimeInSeconds = Time.deltaTime;
			_blockRunner.Process(deltaTimeInSeconds);
		}

		private void OnDestroy()
		{
			_blockRunner.Dispose();
			s_Instance = null;
			s_Initialized = false;
		}
	}
}
#endif
