using System;

namespace LunyScratch
{
	public interface IScratchBlock
	{
		void OnCreate() {}
		void OnDestroy() {}
		void OnEnter() {}
		void OnExit() {}

		void Run(Double deltaTimeInSeconds) {}
		Boolean IsComplete() => true;
	}
}
