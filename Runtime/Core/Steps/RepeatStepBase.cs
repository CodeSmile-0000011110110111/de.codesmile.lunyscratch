// Copyright (C) 2021-2025 Steffen Itterheim
// Refer to included LICENSE file for terms and conditions.

using System;
using System.Collections.Generic;

namespace LunyScratch
{
	// Base class for repeating step sequences
	public abstract class RepeatStepBase : IStep
	{
		protected readonly List<IStep> _steps;
		protected Int32 _currentIndex;
		protected Boolean _shouldExit;

		protected RepeatStepBase(List<IStep> steps) => _steps = steps;

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

			if (_steps.Count > 0)
				_steps[0].OnEnter();
		}

		public void Execute()
		{
			if (_shouldExit || _steps.Count == 0) return;

			// Check exit condition before executing
			if (ShouldExitLoop())
			{
				_shouldExit = true;
				return;
			}

			var currentStep = _steps[_currentIndex];
			currentStep.Execute();

			if (currentStep.IsComplete())
			{
				currentStep.OnExit();
				_currentIndex++;

				// Check if we've completed all steps in the sequence
				if (_currentIndex >= _steps.Count)
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

				_steps[_currentIndex].OnEnter();
			}
		}

		public void OnExit()
		{
			if (_steps.Count > 0 && _currentIndex < _steps.Count)
				_steps[_currentIndex].OnExit();
		}

		public Boolean IsComplete() => _shouldExit;

		// Override this to define when the loop should exit
		protected abstract Boolean ShouldExitLoop();
	}
}
