## When to Start Unit Testing

**Right now** is ideal. Here's why:

### Your Current Position
- ✅ Core abstractions are stable (`IStep` interface, `Sequence`, `RepeatForever`)
- ✅ You have 2-3 engine integrations to maintain
- ✅ You're about to add conditionals (`If`, `IfElse`, `RepeatUntil`)
- ⚠️ No tests means refactoring across engines is risky

**The threshold**: Once you have >3 control flow primitives and >1 engine, tests save more time than they cost.

You've crossed that threshold.

---

## How to Unit Test Cross-Engine Code

### Architecture: Separate Pure Logic from Engine Bindings

Your code already hints at this with `IGameEngineObject`. Formalize it:

```
LunyScratch/
├── LunyScratch.Core/          # Pure C#, no engine dependencies
│   ├── IStep.cs
│   ├── Sequence.cs
│   ├── Wait.cs
│   ├── If.cs
│   ├── RepeatForever.cs
│   └── ICondition.cs
│
├── LunyScratch.Core.Tests/    # Pure C# unit tests (NUnit/xUnit)
│   ├── SequenceTests.cs
│   ├── ConditionalTests.cs
│   └── RepeatTests.cs
│
├── LunyScratch.Unity/         # Unity-specific wrappers
│   ├── UnityEngineObject.cs
│   ├── UnitySequenceRunner.cs (MonoBehaviour)
│   └── Unity-specific actions
│
├── LunyScratch.Godot/         # Godot-specific wrappers
│   └── GodotEngineObject.cs
│
└── LunyScratch.Unreal/        # UnrealSharp wrappers
    └── UnrealEngineObject.cs
```


### The Key Insight

**Test the core library in isolation**. Engine wrappers are just thin adapters.

---

## Practical Setup

### 1. Create a Pure C# Test Project

**In Visual Studio / Rider:**

Create a new **Class Library** project targeting `.NET Standard 2.1` (compatible with Unity 2021+):

```xml
<!-- LunyScratch.Core.csproj -->
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>netstandard2.1</TargetFramework>
    <LangVersion>9.0</LangVersion>
  </PropertyGroup>
</Project>
```


**Move core classes here** (no `UnityEngine` or `Godot` using statements allowed):
- `IStep.cs`
- `Sequence.cs`
- `Wait.cs`
- `RepeatForever.cs`
- `ICondition.cs`
- `If.cs`, `IfElse.cs` (new)

---

### 2. Create the Test Project

```xml
<!-- LunyScratch.Core.Tests.csproj -->
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net6.0</TargetFramework>
    <IsPackable>false</IsPackable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="NUnit" Version="3.14.0" />
    <PackageReference Include="NUnit3TestAdapter" Version="4.5.0" />
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.8.0" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\LunyScratch.Core\LunyScratch.Core.csproj" />
  </ItemGroup>
</Project>
```


---

### 3. Write Engine-Agnostic Tests

```csharp
using NUnit.Framework;

namespace LunyScratch.Core.Tests
{
    [TestFixture]
    public class SequenceTests
    {
        [Test]
        public void EmptySequence_CompletesImmediately()
        {
            var sequence = new Sequence();
            
            bool completed = sequence.Step(0.016f);
            
            Assert.IsTrue(completed);
        }

        [Test]
        public void Sequence_ExecutesStepsInOrder()
        {
            var log = new List<int>();
            var sequence = new Sequence(
                new CallbackStep(() => log.Add(1)),
                new CallbackStep(() => log.Add(2)),
                new CallbackStep(() => log.Add(3))
            );

            sequence.Step(0.016f);

            Assert.AreEqual(new[] { 1, 2, 3 }, log);
        }

        [Test]
        public void Sequence_WaitsForAsyncSteps()
        {
            var log = new List<string>();
            var wait = new Wait(0.1f);
            var sequence = new Sequence(
                new CallbackStep(() => log.Add("before")),
                wait,
                new CallbackStep(() => log.Add("after"))
            );

            // First frame
            sequence.Step(0.05f);
            Assert.AreEqual(new[] { "before" }, log);

            // Second frame (completes wait)
            sequence.Step(0.06f);
            Assert.AreEqual(new[] { "before", "after" }, log);
        }

        [Test]
        public void Sequence_ResetsCorrectly()
        {
            int callCount = 0;
            var sequence = new Sequence(
                new CallbackStep(() => callCount++)
            );

            sequence.Step(0.016f);
            Assert.AreEqual(1, callCount);

            sequence.Reset();
            sequence.Step(0.016f);
            Assert.AreEqual(2, callCount); // Called again after reset
        }

        // Helper step for testing
        private class CallbackStep : IStep
        {
            private readonly Action _callback;
            private bool _executed;

            public CallbackStep(Action callback) => _callback = callback;

            public bool Step(float deltaTime)
            {
                if (!_executed)
                {
                    _callback();
                    _executed = true;
                }
                return true;
            }

            public void Reset() => _executed = false;
        }
    }
}
```


---

```csharp
using NUnit.Framework;

namespace LunyScratch.Core.Tests
{
    [TestFixture]
    public class ConditionalTests
    {
        [Test]
        public void If_ExecutesThenBranch_WhenConditionTrue()
        {
            bool executed = false;
            var ifStep = new If(
                condition: new MockCondition(true),
                thenStep: new CallbackStep(() => executed = true)
            );

            ifStep.Step(0.016f);

            Assert.IsTrue(executed);
        }

        [Test]
        public void If_SkipsThenBranch_WhenConditionFalse()
        {
            bool executed = false;
            var ifStep = new If(
                condition: new MockCondition(false),
                thenStep: new CallbackStep(() => executed = true)
            );

            bool completed = ifStep.Step(0.016f);

            Assert.IsFalse(executed);
            Assert.IsTrue(completed); // Should complete immediately
        }

        [Test]
        public void If_EvaluatesConditionOnlyOnce()
        {
            int evaluateCount = 0;
            var condition = new MockCondition(() => 
            {
                evaluateCount++;
                return true;
            });

            var ifStep = new If(
                condition: condition,
                thenStep: new Wait(0.1f) // Multi-frame step
            );

            ifStep.Step(0.05f); // Frame 1
            ifStep.Step(0.05f); // Frame 2
            ifStep.Step(0.05f); // Frame 3

            Assert.AreEqual(1, evaluateCount, "Condition should only evaluate once");
        }

        [Test]
        public void IfElse_ExecutesCorrectBranch()
        {
            string executed = "";
            var ifElse = new IfElse(
                condition: new MockCondition(false),
                thenStep: new CallbackStep(() => executed = "then"),
                elseStep: new CallbackStep(() => executed = "else")
            );

            ifElse.Step(0.016f);

            Assert.AreEqual("else", executed);
        }

        // Test helpers
        private class MockCondition : ICondition
        {
            private readonly Func<bool> _evaluator;

            public MockCondition(bool result) : this(() => result) { }
            public MockCondition(Func<bool> evaluator) => _evaluator = evaluator;

            public bool Evaluate() => _evaluator();
        }

        private class CallbackStep : IStep
        {
            private readonly Action _callback;
            public CallbackStep(Action callback) => _callback = callback;
            public bool Step(float deltaTime) { _callback(); return true; }
            public void Reset() { }
        }
    }
}
```


---

```csharp
using NUnit.Framework;

namespace LunyScratch.Core.Tests
{
    [TestFixture]
    public class RepeatTests
    {
        [Test]
        public void RepeatForever_NeverCompletes()
        {
            var repeat = new RepeatForever(
                new CallbackStep(() => { })
            );

            for (int i = 0; i < 100; i++)
            {
                bool completed = repeat.Step(0.016f);
                Assert.IsFalse(completed, $"Should never complete (iteration {i})");
            }
        }

        [Test]
        public void RepeatForever_ResetsChildAfterEachIteration()
        {
            int executionCount = 0;
            var repeat = new RepeatForever(
                new CallbackStep(() => executionCount++)
            );

            repeat.Step(0.016f); // Iteration 1
            repeat.Step(0.016f); // Iteration 2
            repeat.Step(0.016f); // Iteration 3

            Assert.AreEqual(3, executionCount);
        }

        [Test]
        public void RepeatUntil_StopsWhenConditionTrue()
        {
            int iterations = 0;
            var repeat = new RepeatUntil(
                condition: new MockCondition(() => iterations >= 3),
                repeatedStep: new CallbackStep(() => iterations++)
            );

            // Should complete after 3 iterations
            bool completed = false;
            for (int i = 0; i < 10; i++)
            {
                completed = repeat.Step(0.016f);
                if (completed) break;
            }

            Assert.IsTrue(completed);
            Assert.AreEqual(3, iterations);
        }

        // Test helpers
        private class MockCondition : ICondition
        {
            private readonly Func<bool> _evaluator;
            public MockCondition(Func<bool> evaluator) => _evaluator = evaluator;
            public bool Evaluate() => _evaluator();
        }

        private class CallbackStep : IStep
        {
            private readonly Action _callback;
            public CallbackStep(Action callback) => _callback = callback;
            public bool Step(float deltaTime) { _callback(); return true; }
            public void Reset() { }
        }
    }
}
```


---

## Running Tests

### Command Line
```shell script
dotnet test LunyScratch.Core.Tests
```


### In IDE
- **Rider**: Test Explorer (Alt+8)
- **Visual Studio**: Test → Test Explorer

---

## Engine-Specific Testing (Integration Tests)

For Unity-specific features (e.g., `UnityEngineObject`), use Unity's Test Framework:

```csharp
// In Unity project: Assets/Tests/UnityIntegrationTests.cs
using NUnit.Framework;
using UnityEngine;
using LunyScratch;

public class UnityIntegrationTests
{
    [Test]
    public void UnityEngineObject_DisablesBehaviour()
    {
        var go = new GameObject();
        var light = go.AddComponent<Light>();
        
        var wrapper = new UnityEngineObject(light);
        wrapper.SetEnabled(false);
        
        Assert.IsFalse(light.enabled);
        
        Object.DestroyImmediate(go);
    }
}
```


But keep these **minimal**—most logic should be testable in pure C#.

---

## Benefits You Get Immediately

1. **Refactor fearlessly** - Change `IStep` signature without breaking 3 engines
2. **Document behavior** - Tests show how conditionals/sequences should work
3. **Catch regressions** - Adding `RepeatUntil` won't break `RepeatForever`
4. **Fast iteration** - Run 100 tests in <1 second vs. launching Unity 3 times

---

## Workflow Tomorrow

1. ✅ **Get UnrealSharp compiling** (2 hours)
2. ✅ **Extract core logic to `.Core` project** (1 hour)
3. ✅ **Write 5-10 core tests** (1 hour)
4. ✅ **Implement `If`/`IfElse` with tests** (remaining time)

By end of day, you'll have provable correctness across all engines without launching any of them. That's the power of unit testing with proper separation.
