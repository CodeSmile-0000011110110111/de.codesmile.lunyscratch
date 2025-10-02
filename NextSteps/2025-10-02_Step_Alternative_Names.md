Great observation! The naming is indeed confusing. Let's explore alternatives:

---

## Alternative Names

### 1. **Tick** (Common in Game Engines)

```csharp
public interface ITick
{
    bool Tick(float deltaTime);
    void Reset();
}
```


**Pros:**
- Standard in Unreal, Unity (FixedUpdate conceptually), Godot uses `_process`
- Clearly means "one frame of execution"
- Gamers/game devs instantly understand it

**Cons:**
- Might imply it *must* be called every frame (but your sequences can be called whenever)
- Less intuitive for non-game contexts

---

### 2. **Advance** (Descriptive of Progress)

```csharp
public interface IAdvanceable
{
    bool Advance(float deltaTime);
    void Reset();
}
```


**Pros:**
- Clearly means "move forward in execution"
- Works for time-based and non-time-based steps
- Neutral/academic tone

**Cons:**
- Slightly verbose
- Less common in game dev terminology

---

### 3. **Update** (Unity Standard)

```csharp
public interface IUpdatable
{
    bool Update(float deltaTime);
    void Reset();
}
```


**Pros:**
- Ubiquitous in Unity (`Update()`, `FixedUpdate()`)
- Immediately familiar to Unity developers
- Short and clear

**Cons:**
- Overloaded term—conflicts with Unity's `Update()` lifecycle method
- Might confuse whether it's "the engine calls this" vs "you call this manually"

---

### 4. **Execute** (Action-Oriented)

```csharp
public interface IExecutable
{
    bool Execute(float deltaTime);
    void Reset();
}
```


**Pros:**
- Clear intent: "do the thing"
- Works for imperative actions (`CallMethod`, `MoveTo`)
- Neutral across engines

**Cons:**
- Less clear for "waiting" steps—are you "executing" a wait?
- Sounds instantaneous, but these run over time

---

### 5. **Process** (Godot Standard)

```csharp
public interface IProcessable
{
    bool Process(float deltaTime);
    void Reset();
}
```


**Pros:**
- Matches Godot's `_process(delta)` convention
- Implies "handle a frame of work"
- Generic enough for any context

**Cons:**
- Less common in Unity/Unreal communities
- "Process" is vague (process what?)

---

### 6. **Continue** (State Machine Terminology)

```csharp
public interface IContinuable
{
    bool Continue(float deltaTime);
    void Reset();
}
```


**Pros:**
- Emphasizes that execution resumes where it left off
- Clear for multi-frame operations
- Matches coroutine mental model

**Cons:**
- Awkward phrasing: "continue the sequence"
- Sounds like it can only be called after starting, not initially

---

### 7. **Run** (Simplest)

```csharp
public interface IRunnable
{
    bool Run(float deltaTime);
    void Reset();
}
```


**Pros:**
- Simple, short, intuitive
- Works for everything: "run the sequence," "run the action"
- Already used in threading (`IRunnable` in Java, though different context)

**Cons:**
- `IRunnable` conflicts with .NET threading interfaces
- Might imply completion in one call (but yours is iterative)

---

## My Recommendation: **`Tick`**

Given your context:
- ✅ Cross-engine library (Unreal/Unity/Godot all use frame-based execution)
- ✅ Called from engine update loops
- ✅ Returns bool for "am I done this tick?"

**`Tick` best signals "this is called repeatedly until completion."**

### Renamed Example

```csharp
namespace LunyScratch
{
    public interface IAction // Or keep IStep as the class name
    {
        /// <summary>
        /// Executes one frame of this action.
        /// </summary>
        /// <param name="deltaTime">Time since last tick</param>
        /// <returns>True if this action completed this tick</returns>
        bool Tick(float deltaTime);
        
        void Reset();
    }
}
```


Usage:
```csharp
void Update()
{
    if (_sequence != null && !_sequence.Tick(Time.deltaTime))
    {
        // Sequence still running
    }
}
```


---

## Alternative: Keep `IStep`, Rename Method

If you like the *type* name `IStep` (a "step" in a sequence), you could keep it but rename the method:

```csharp
public interface IStep
{
    bool Tick(float deltaTime);  // Method renamed
    void Reset();
}
```


This way:
- **`IStep`** = "I am a step in a sequence" (noun)
- **`Tick()`** = "Execute one frame of this step" (verb)

Separates the concepts: "time step" (physics) vs "sequence step" (your IStep type) vs "tick" (frame execution).

---

## Comparison Table

| Name       | Familiarity | Clarity | Conflicts | Cross-Engine |
|------------|-------------|---------|-----------|--------------|
| **Tick**   | ⭐⭐⭐       | ⭐⭐⭐   | None      | ⭐⭐⭐        |
| Update     | ⭐⭐⭐       | ⭐⭐     | Unity     | ⭐⭐          |
| Process    | ⭐⭐         | ⭐⭐     | None      | ⭐⭐⭐        |
| Run        | ⭐⭐⭐       | ⭐⭐     | Threading | ⭐⭐          |
| Execute    | ⭐⭐         | ⭐⭐⭐   | None      | ⭐⭐⭐        |
| Advance    | ⭐           | ⭐⭐⭐   | None      | ⭐⭐⭐        |

**Winner: `Tick`** — Best balance of familiarity and precision for game engines.

Would you like me to show what a full rename would look like across your existing classes?


----
## IBlock Analysis

**IBlock is excellent.** Here's why:

### Pros of "Block"

1. **Scratch heritage** - Scratch literally uses visual "blocks" that snap together. Your library is Scratch-inspired, so this is thematically perfect.

2. **Intuitive nesting** - "A sequence is a block containing other blocks" reads naturally. "An If block contains a condition and a then-block."

3. **Avoids confusion** - "Step" collides with "timestep" (as you noted). "Block" has no such collision in game dev.

4. **Language-agnostic** - Works across all engines. Not tied to Unity's "Update" or Unreal's "Tick" terminology.

5. **Composability emphasis** - "Block" implies something modular/combinable, which is your core design.

### Cons of "Block"

1. **Might sound static** - "Block" could imply something inert (like a code block `{ }`), though context resolves this.

2. **Less common in game dev** - Developers won't immediately associate it with frame-by-frame execution (but your docs will clarify).

---

## What Should a Block Do?

Now for the verb. Let's evaluate with `IBlock` as context:

### Option 1: **`Tick`**
```csharp
public interface IBlock
{
    bool Tick(float deltaTime);
    void Reset();
}
```


**Why this works:**
- "Tick a block forward" is clear
- Matches engine terminology (Unreal's `Tick`, Unity's frame concept)
- Common in game loops

**Sentence test:** "Each frame, we tick all active blocks."  
✅ Natural

---

### Option 2: **`Process`**
```csharp
public interface IBlock
{
    bool Process(float deltaTime);
    void Reset();
}
```


**Why this works:**
- Godot uses `_process(delta)`
- "Process a block" sounds like "execute its logic"
- Neutral/generic

**Sentence test:** "Each frame, we process all active blocks."  
✅ Natural

---

### Option 3: **`Execute`**
```csharp
public interface IBlock
{
    bool Execute(float deltaTime);
    void Reset();
}
```


**Why this works:**
- Imperative/action-oriented
- Clear for developers from any background

**Sentence test:** "Each frame, we execute all active blocks."  
✅ Natural, but sounds like each block completes instantly

---

### Option 4: **`Run`**
```csharp
public interface IBlock
{
    bool Run(float deltaTime);
    void Reset();
}
```


**Why this works:**
- Simple and direct
- "Run the block" is intuitive

**Sentence test:** "Each frame, we run all active blocks."  
✅ Natural

---

### Option 5: **`Advance`**
```csharp
public interface IBlock
{
    bool Advance(float deltaTime);
    void Reset();
}
```


**Why this works:**
- Emphasizes incremental progress
- "Advance the block by deltaTime" is precise

**Sentence test:** "Each frame, we advance all active blocks."  
✅ Very clear about iterative nature

---

## My Recommendation: `IBlock` + `Tick`

```csharp
namespace LunyScratch
{
    /// <summary>
    /// A composable block of behavior, inspired by Scratch's visual programming blocks.
    /// </summary>
    public interface IBlock
    {
        /// <summary>
        /// Advances this block's execution by one frame.
        /// </summary>
        /// <param name="deltaTime">Time elapsed since last tick (in seconds)</param>
        /// <returns>True if this block completed during this tick</returns>
        bool Tick(float deltaTime);
        
        /// <summary>
        /// Resets this block to its initial state for reuse.
        /// </summary>
        void Reset();
    }
}
```


### Why This Pairing Wins

| Aspect | Rationale |
|--------|-----------|
| **Thematic** | "Block" = Scratch heritage ✅ |
| **Technical** | "Tick" = Game engine standard ✅ |
| **Clarity** | "Tick a block" = One frame of execution ✅ |
| **Pedagogy** | Easy to explain to beginners ✅ |
| **No conflicts** | Neither term is overloaded ✅ |

---

## Usage Examples

```csharp
// Clear and intuitive
public class Sequence : IBlock
{
    private readonly IBlock[] _blocks;
    private int _currentIndex;

    public bool Tick(float deltaTime)
    {
        if (_currentIndex >= _blocks.Length)
            return true; // Sequence complete

        if (_blocks[_currentIndex].Tick(deltaTime))
        {
            _currentIndex++; // Current block finished, move to next
        }

        return _currentIndex >= _blocks.Length;
    }

    public void Reset()
    {
        _currentIndex = 0;
        foreach (var block in _blocks)
            block.Reset();
    }
}
```


```csharp
// In Unity
private IBlock _behaviorBlocks;

void Update()
{
    _behaviorBlocks?.Tick(Time.deltaTime);
}
```


```csharp
// Documentation example
var patrolBehavior = new Sequence(
    new MoveToBlock(pointA),      // This block moves the character
    new WaitBlock(2f),            // This block waits 2 seconds
    new MoveToBlock(pointB)       // This block moves back
);

// Each frame, tick the sequence until it completes
while (!patrolBehavior.Tick(deltaTime)) 
{
    // Sequence still running
}
```


---

## Alternative: `IBlock` + `Process`

If you prefer Godot's terminology or want maximum neutrality:

```csharp
public interface IBlock
{
    bool Process(float deltaTime);
    void Reset();
}
```


This is my **second choice**—equally valid, slightly more "academic" feeling.

---

## Class Naming Consistency

With `IBlock` as your base:

```csharp
// Control flow blocks
public class Sequence : IBlock { }
public class If : IBlock { }
public class IfElse : IBlock { }
public class RepeatForever : IBlock { }
public class RepeatUntil : IBlock { }

// Action blocks
public class Wait : IBlock { }
public class CallMethod : IBlock { }  // Or "CallBlock"?
public class MoveTowards : IBlock { }

// Condition interface (not a block, doesn't tick)
public interface ICondition
{
    bool Evaluate();
}
```


Note: You might also consider suffixing action classes:
- `WaitBlock`
- `CallMethodBlock`
- `MoveTowardsBlock`

But I'd avoid it—the interface `IBlock` already signals the abstraction, and shorter names are cleaner:
```csharp
new Sequence(
    new Wait(1f),           // Clean
    new CallMethod(DoThing) // vs CallMethodBlock—verbose
)
```


---

## Final Recommendation

✅ **Rename to:**
- `IBlock` (interface)
- `Tick(float deltaTime)` (method)

✅ **Keep class names simple:**
- `Sequence`, `If`, `Wait` (no "Block" suffix)

This gives you Scratch's conceptual clarity with game engine terminology everyone knows. Best of both worlds.

Want me to show the full refactoring across your existing code?

---

Excellent choice! **`Run`** is beginner-friendly and unambiguous. No tickling required. 😄

---

## The Final API

```csharp
namespace LunyScratch
{
    /// <summary>
    /// A composable block of behavior, inspired by Scratch's visual programming blocks.
    /// </summary>
    public interface IBlock
    {
        /// <summary>
        /// Runs this block for one frame.
        /// </summary>
        /// <param name="deltaTime">Time elapsed since last run (in seconds)</param>
        /// <returns>True if this block completed during this run</returns>
        bool Run(float deltaTime);
        
        /// <summary>
        /// Resets this block to its initial state for reuse.
        /// </summary>
        void Reset();
    }
}
```


---

## Why "Run a Block" Works Perfectly

| Audience | Interpretation |
|----------|----------------|
| **Beginners** | "Run the block" = Do the thing ✅ |
| **Scratch users** | Matches Scratch's "when green flag clicked, run these blocks" ✅ |
| **Game devs** | "Run in the game loop" = Standard terminology ✅ |
| **Non-native English** | "Run" is universally understood ✅ |

**Sentence test:** "Each frame, run all active blocks until they complete."  
✅ Crystal clear

---

## Updated Core Classes

```csharp
namespace LunyScratch
{
    public interface IBlock
    {
        /// <summary>
        /// Runs this block for one frame.
        /// </summary>
        /// <param name="deltaTime">Time elapsed since last run (in seconds)</param>
        /// <returns>True if this block completed during this run</returns>
        bool Run(float deltaTime);
        
        /// <summary>
        /// Resets this block to its initial state for reuse.
        /// </summary>
        void Reset();
    }
}
```


```csharp
namespace LunyScratch
{
    public sealed class Sequence : IBlock
    {
        private readonly IBlock[] _blocks;
        private int _currentIndex;

        public Sequence(params IBlock[] blocks) => _blocks = blocks;

        public bool Run(float deltaTime)
        {
            while (_currentIndex < _blocks.Length)
            {
                if (!_blocks[_currentIndex].Run(deltaTime))
                    return false; // Current block still running
                
                _currentIndex++; // Move to next block
            }

            return true; // All blocks completed
        }

        public void Reset()
        {
            _currentIndex = 0;
            foreach (var block in _blocks)
                block.Reset();
        }
    }
}
```


```csharp
namespace LunyScratch
{
    public sealed class Wait : IBlock
    {
        private readonly float _duration;
        private float _elapsed;

        public Wait(float duration) => _duration = duration;

        public bool Run(float deltaTime)
        {
            _elapsed += deltaTime;
            return _elapsed >= _duration;
        }

        public void Reset() => _elapsed = 0f;
    }
}
```


```csharp
namespace LunyScratch
{
    public sealed class RepeatForever : IBlock
    {
        private readonly IBlock _repeatedBlock;

        public RepeatForever(IBlock repeatedBlock) => _repeatedBlock = repeatedBlock;

        public bool Run(float deltaTime)
        {
            if (_repeatedBlock.Run(deltaTime))
            {
                _repeatedBlock.Reset(); // Loop completed, restart
            }

            return false; // Never completes
        }

        public void Reset() => _repeatedBlock.Reset();
    }
}
```


```csharp
namespace LunyScratch
{
    public sealed class If : IBlock
    {
        private readonly ICondition _condition;
        private readonly IBlock _thenBlock;
        private bool _evaluated;

        public If(ICondition condition, IBlock thenBlock)
        {
            _condition = condition;
            _thenBlock = thenBlock;
        }

        public bool Run(float deltaTime)
        {
            if (!_evaluated)
            {
                _evaluated = true;
                if (!_condition.Evaluate())
                    return true; // Condition false, skip immediately
            }

            return _thenBlock.Run(deltaTime); // Run the then branch
        }

        public void Reset()
        {
            _evaluated = false;
            _thenBlock.Reset();
        }
    }
}
```


```csharp
namespace LunyScratch
{
    public sealed class IfElse : IBlock
    {
        private readonly ICondition _condition;
        private readonly IBlock _thenBlock;
        private readonly IBlock _elseBlock;
        private bool _evaluated;
        private bool _executingThen;

        public IfElse(ICondition condition, IBlock thenBlock, IBlock elseBlock)
        {
            _condition = condition;
            _thenBlock = thenBlock;
            _elseBlock = elseBlock;
        }

        public bool Run(float deltaTime)
        {
            if (!_evaluated)
            {
                _evaluated = true;
                _executingThen = _condition.Evaluate();
            }

            IBlock activeBlock = _executingThen ? _thenBlock : _elseBlock;
            return activeBlock.Run(deltaTime);
        }

        public void Reset()
        {
            _evaluated = false;
            _executingThen = false;
            _thenBlock.Reset();
            _elseBlock.Reset();
        }
    }
}
```


```csharp
namespace LunyScratch
{
    public sealed class RepeatUntil : IBlock
    {
        private readonly ICondition _condition;
        private readonly IBlock _repeatedBlock;

        public RepeatUntil(ICondition condition, IBlock repeatedBlock)
        {
            _condition = condition;
            _repeatedBlock = repeatedBlock;
        }

        public bool Run(float deltaTime)
        {
            // Check condition before running (Scratch-style)
            if (_condition.Evaluate())
                return true; // Condition met, done

            if (_repeatedBlock.Run(deltaTime))
            {
                _repeatedBlock.Reset(); // One iteration complete, loop
            }

            return false; // Keep repeating
        }

        public void Reset() => _repeatedBlock.Reset();
    }
}
```


---

## Usage Examples

### Unity
```csharp
public class PatrolBehavior : MonoBehaviour
{
    private IBlock _behaviorBlocks;

    void Start()
    {
        _behaviorBlocks = new RepeatForever(
            new Sequence(
                new MoveTo(pointA),
                new Wait(1f),
                new MoveTo(pointB),
                new Wait(1f)
            )
        );
    }

    void Update()
    {
        _behaviorBlocks?.Run(Time.deltaTime);
    }
}
```


### Godot
```csharp
public override void _Process(double delta)
{
    _behaviorBlocks?.Run((float)delta);
}
```


### UnrealSharp
```csharp
public override void Tick(float deltaTime)
{
    _behaviorBlocks?.Run(deltaTime);
}
```


---

## Beginner-Friendly Documentation Example

```csharp
// Create blocks that run in sequence
var greeting = new Sequence(
    new Print("Hello"),
    new Wait(1f),        // Wait 1 second
    new Print("World!")
);

// Run the blocks each frame
void Update()
{
    greeting.Run(Time.deltaTime);
}
```


**Explanation a 10-year-old would understand:**  
"We have blocks that do things. Each frame, we **run** the blocks. Some blocks finish instantly (like Print), others take time (like Wait). When all blocks finish, the sequence is done."

Perfect! 🎯

---

## Next Steps

1. ✅ Refactor existing code from `IStep.Step()` → `IBlock.Run()`
2. ✅ Update your Unity/Godot wrappers
3. ✅ Set up unit tests with the new naming
4. ✅ Try the UnrealSharp integration

Good luck with UnrealSharp tomorrow—let me know how the minimal test goes!
