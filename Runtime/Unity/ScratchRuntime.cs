#if UNITY_6000_0_OR_NEWER
using System;
using UnityEngine;

namespace LunyScratch
{
	[DefaultExecutionOrder(Int16.MinValue)]
	[AddComponentMenu("GameObject/")] // Do not list in "Add Component" menu
	[DisallowMultipleComponent]
	public sealed class ScratchRuntime : MonoBehaviour, IEngineRuntime
	{
		private static ScratchRuntime s_Instance;
		private static Boolean s_Initialized;

		private readonly BlockRunner _blockRunner = new();

		public static ScratchRuntime Instance => s_Instance;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void Initialize()
		{
			if (s_Initialized)
				return;

			s_Initialized = true;

			var go = new GameObject(nameof(ScratchRuntime));
			s_Instance = go.AddComponent<ScratchRuntime>();
			DontDestroyOnLoad(go);

			GameEngine.Initialize(s_Instance, new UnityActions());
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
