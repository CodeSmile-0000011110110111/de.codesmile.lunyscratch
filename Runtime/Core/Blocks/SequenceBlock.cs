using System;
using System.Collections.Generic;

namespace LunyScratch
{
	public sealed class SequenceBlock : IScratchBlock
	{
		private readonly List<IScratchBlock> _blocks;
		private Int32 _currentIndex;

		public SequenceBlock(List<IScratchBlock> blocks)
		{
			_blocks = blocks;
			_currentIndex = 0;
		}

		public void OnEnter()
		{
			_currentIndex = 0;
			if (_blocks.Count > 0)
				_blocks[0].OnEnter();
		}

		public void Run()
		{
			if (_currentIndex >= _blocks.Count) return;

			var currentBlock = _blocks[_currentIndex];
			currentBlock.Run();

			if (currentBlock.IsComplete())
			{
				currentBlock.OnExit();
				_currentIndex++;

				if (_currentIndex < _blocks.Count)
					_blocks[_currentIndex].OnEnter();
			}
		}

		public void OnExit()
		{
			if (_currentIndex < _blocks.Count)
				_blocks[_currentIndex].OnExit();
		}

		public Boolean IsComplete() => _currentIndex >= _blocks.Count;
	}
}
