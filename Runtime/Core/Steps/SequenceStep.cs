// Base interface for FSM steps

using System;
using System.Collections.Generic;

namespace LunyScratch
{
	// Sequence of steps
	public sealed class SequenceStep : IStep
	{
		private readonly List<IStep> _steps;
		private Int32 _currentIndex;

		public SequenceStep(List<IStep> steps)
		{
			_steps = steps;
			_currentIndex = 0;
		}

		public void OnEnter()
		{
			_currentIndex = 0;
			if (_steps.Count > 0)
				_steps[0].OnEnter();
		}

		public void Execute()
		{
			if (_currentIndex >= _steps.Count) return;

			var currentStep = _steps[_currentIndex];
			currentStep.Execute();

			if (currentStep.IsComplete())
			{
				currentStep.OnExit();
				_currentIndex++;

				if (_currentIndex < _steps.Count)
					_steps[_currentIndex].OnEnter();
			}
		}

		public void OnExit()
		{
			if (_currentIndex < _steps.Count)
				_steps[_currentIndex].OnExit();
		}

		public Boolean IsComplete() => _currentIndex >= _steps.Count;
	}
}
