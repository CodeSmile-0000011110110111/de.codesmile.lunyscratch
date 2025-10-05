using System;
using System.Collections.Generic;

namespace LunyScratch
{
	// Base class for repeating block sequences
	public abstract class RepeatBlockBase : IScratchBlock
	{
		protected readonly List<IScratchBlock> _blocks;
		protected Int32 _currentIndex;
		protected Boolean _shouldExit;

		protected RepeatBlockBase(List<IScratchBlock> blocks) => _blocks = blocks;
		protected RepeatBlockBase(params IScratchBlock[] blocks) => _blocks = new List<IScratchBlock>(blocks);

		public void OnEnter()
		{
			_currentIndex = 0;
			_shouldExit = false;

			// Check exit condition immediately
			if (ShouldExitLoop())
			{
				_shouldExit = true;
				return;
			}

			if (_blocks.Count > 0)
				_blocks[0].OnEnter();
		}

		public void Run(Double deltaTimeInSeconds)
		{
			if (_shouldExit || _blocks.Count == 0) return;

			// Check exit condition before executing
			if (ShouldExitLoop())
			{
				_shouldExit = true;
				return;
			}

			var currentBlock = _blocks[_currentIndex];
			currentBlock.Run(deltaTimeInSeconds);

			if (currentBlock.IsComplete())
			{
				currentBlock.OnExit();
				_currentIndex++;

				// Check if we've completed all blocks in the sequence
				if (_currentIndex >= _blocks.Count)
				{
					// Check exit condition before restarting
					if (ShouldExitLoop())
					{
						_shouldExit = true;
						return;
					}

					// Loop back to the beginning
					_currentIndex = 0;
				}

				_blocks[_currentIndex].OnEnter();
			}
		}

		public void OnExit()
		{
			if (_blocks.Count > 0 && _currentIndex < _blocks.Count)
				_blocks[_currentIndex].OnExit();
		}

		public Boolean IsComplete() => _shouldExit;

		// Override this to define when the loop should exit
		protected abstract Boolean ShouldExitLoop();
	}
}
