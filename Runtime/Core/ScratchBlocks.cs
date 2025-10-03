using System.Collections.Generic;

namespace LunyScratch
{
	public sealed class ScratchBlocks
	{
		private readonly List<IScratchBlock> _blocks = new();

		public void Process(float deltaTimeInSeconds)
		{
			// Execute all active FSM blocks
			for (var i = _blocks.Count - 1; i >= 0; i--)
			{
				var step = _blocks[i];
				step.Run(deltaTimeInSeconds);

				if (step.IsComplete())
				{
					step.OnExit();
					_blocks.RemoveAt(i);
				}
			}
		}

		public void Dispose()
		{
			_blocks.Clear();
		}

		public void Run(IScratchBlock block)
		{
			block.OnEnter();
			_blocks.Add(block);
		}
	}
}
