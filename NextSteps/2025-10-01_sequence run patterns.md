## Key Questions for Formalizing Parallel Sequences

### 1. **Lifecycle & Ownership**
- Should parallel sequences share the same lifecycle as the parent `MonoBehaviour`? YES
- What happens when the GameObject is destroyed — do all sequences stop? YES
- Can sequences outlive their creator (continue across scenes)? NO

### 2. **Synchronization & Coordination**
- Do parallel sequences ever need to communicate or wait for each other?
- Should there be a way to "join" multiple sequences (wait for all to complete)?
- Can one sequence cancel/pause another?

All: defer decision

### 3. **Error Handling**
- If one parallel sequence throws an exception, should others continue? YES
- Should there be a "supervisor" that restarts failed sequences? NO

### 4. **Discoverability & Debugging**
- How does a developer see what sequences are running?
- Should sequences have names/IDs for debugging? YES, optional
- Can you inspect a sequence's current step in the inspector?

### 5. **Scratch Semantics**
- In Scratch, each sprite has multiple independent scripts that run when the green flag is clicked—is that your mental model? YES
- Should there be an explicit `Parallel(seq1, seq2, seq3)` construct, or is "call `Sequence.Run()` multiple times" sufficient? Suffices for now ..

---

## Your Options

### **Option A: Implicit (Current Approach)**
```csharp
Sequence.Run(movement);  // Starts independently
Sequence.Run(lights);    // Starts independently
```


**Pros:**
- Simple, already working
- Mirrors Scratch's "multiple scripts per sprite" model
- No new syntax needed

**Cons:**
- No way to know these are related
- Can't wait for both to finish
- Hard to stop all related sequences

**Best for:** Truly independent behaviors (movement, lights, sounds)

---

### **Option B: Explicit Parallel Container**
```csharp
Sequence.Run(
    Parallel(
        // Finishes when ALL children finish
        Sequence(movement steps),
        Sequence(light steps),
        Sequence(sound steps)
    )
);
```


**Pros:**
- Clear relationships in code
- Can add "wait for all" semantics
- Easier to debug (grouped in hierarchy)

**Cons:**
- More complex implementation
- What does "complete" mean? (first child? all children? never?)
- Doesn't match Scratch's flat script model

**Best for:** Coordinated behaviors that need to synchronize

---

### **Option C: Named Sequence Groups**
```csharp
Sequence.Run("police_car", movement);
Sequence.Run("police_car", lights);

// Later...
Sequence.StopGroup("police_car");
```


**Pros:**
- Maintains independence
- Adds lifecycle control
- Good for debugging (can list all sequences in "police_car")

**Cons:**
- Magic strings
- Still no synchronization primitives

**Best for:** Managing sets of independent behaviors

---

### **Option D: Hybrid - Parallel + Forever**
```csharp
Sequence.Run(
    Parallel(
        RepeatForever(MoveCar),
        RepeatForever(BlinkLights),
        Sequence(countdown, PlaySound("gogogo"))
    )
);
```


**Semantics:** Parallel completes when all **finite** children complete. Infinite children keep running.

**Pros:**
- Handles your exact use case elegantly
- Countdown happens once, loops run forever
- Clear structure

**Cons:**
- Tricky semantics (what if no finite children?)
- May not match user expectations

---

## My Recommendation: **Option A + Option C**

### Why
Your current implicit approach already works and matches Scratch's mental model. **Don't add complexity yet.**

However, add **minimal lifecycle control**:

```csharp
// In Start()
var movementSeq = Sequence.Run(movement);
var lightSeq = Sequence.Run(lights);

// In OnDestroy()
movementSeq.Stop();
lightSeq.Stop();
```


Or for groups:
```csharp
Sequence.Run(this, movement);  // Tied to MonoBehaviour
Sequence.Run(this, lights);

// Auto-cleanup on destroy
```


### What to Answer First
1. **Should sequences auto-stop when their MonoBehaviour dies?** (Probably yes)
2. **Do you need to wait for multiple sequences?** (Only add `Parallel` if yes)
3. **How do you debug what's running?** (Add logging/naming)

Start simple. Add `Parallel` only when you have a concrete use case that *requires* synchronization. Your police car doesn't—the countdown and movement don't need to coordinate.
