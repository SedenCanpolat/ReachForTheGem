# Reach For The Gem

**Reach For The Gem** is a stealth-like 3D game developed using Unity.  
Players must find keys, overcome various obstacles waiting for them, and reach the gem without being caught by the enemy.

🎮 **Play From Here**: [https://seden.itch.io/reach-for-the-jem](https://seden.itch.io/reach-for-the-jem)

![08172-ezgif com-video-to-gif-converter](https://github.com/user-attachments/assets/bf5490ac-f0db-4d36-93a3-835001acd25b)

## 👨‍💻 Developer: Seden Canpolat

I have been developing this game solo to enhance my coding skills. **It's still in development and serves as a prototype.** So far in this project, I have implemented:

- Player movement  
- Field of view system for both the player and the enemy  
- Camera controls that follow the player  
- Enemy movement with and without AI  
- Inventory system for both in-game use and UI  
- Player interactions with the environment  
- Transition system  
- Scene management  
- Animations  
- Bomb, security camera, and laser mechanics to detect the player


![08173-ezgif com-video-to-gif-converter](https://github.com/user-attachments/assets/ac623966-d479-4c41-a941-7b3eab7a9e3d)

## Key Technical Implementations

### 1. AI State Machine with NavMesh Pathfinding

Implemented a finite state machine with four behaviors: **Patrol**, **Chase**, **Return**, and **LookAround**. State transitions are managed through a switch statement for clear and maintainable AI logic.

- **NavMesh Integration**  
  Uses `NavMeshAgent.SetDestination()` for intelligent pathfinding around obstacles, with automatic path recalculation during pursuit.

- **Dual Implementation**  
  Includes both NavMesh-based and `CharacterController`-based enemy movement as separate implementations, highlighting the tradeoffs between Unity’s built-in navigation and manual control.

### 2. Dynamic Third-Person Camera

Implemented a third-person camera that smoothly follows the player using vector math and `Vector3.Lerp`:

```csharp
target = _player.transform.position 
         - (playerF * _cameraDepth) 
         + (playerU * _cameraHeight);

transform.position = Vector3.Lerp(
    transform.position,
    target,
    Time.deltaTime * _followSpeed
);
```

The camera calculates its position relative to the player’s forward and up vectors, enabling dynamic following that rotates with the player while maintaining a consistent distance and height.

### 3. Field of View Detection with Visual Debugging

Built a vision system combining `Physics.OverlapSphere` for radius-based detection and raycasting for line-of-sight validation. Detected colliders are sorted by distance to prioritize closer targets.

Implemented `OnDrawGizmos()` to visualize detection ranges with color-coded feedback:
- Red for blocked line of sight  
- Green for clear visibility  
- Yellow for detection radius  

This allows efficient debugging without constant playtesting.

### 4. Event-Driven Architecture with Delegates

Implemented flexible system communication using `Func` and `Action` delegates:

```csharp
public Func<bool> CheckCanInteract;
public Action OnInteracted;
```

This creates a decoupled architecture where systems communicate through contracts rather than direct references.

### 5. Complex Multi-State Interaction System

- **LockedObject**  
  Implements key validation using ScriptableObject ID matching, paired object logic (one door blocking another), internal item spawning with rotation-based positioning, and state persistence through `IResetUpdater`.

- **InsideOut Component**  
  Handles item spawning with intelligent positioning based on container rotation, automatically adjusting spawn locations for differently oriented objects.

- **Proximity Detection**  
  Uses `Physics.OverlapSphere` in `PlayerDistance` to detect nearby interactables and display context-sensitive UI prompts (e.g., “Press E”), combining physics queries with event-driven UI updates.

### 6. LINQ-Based Component Discovery and Interface-Based Reset System

Used LINQ to dynamically discover all resettable objects:

```csharp
_resetUpdaters = GetComponentsInChildren<MonoBehaviour>()
    .OfType<IResetUpdater>()
    .ToList();
```

This creates a fully automatic reset system. New hazards are included simply by implementing the `IResetUpdater` interface, with no manual registration required.

### 7. Mouse Wheel Inventory with Visual Feedback

Implemented inventory scrolling using modulo arithmetic for circular navigation:

```csharp
_selectedIndex = (_selectedIndex + 1) % _inventory.Count;
```

The UI frame moves to highlight selected slots, and a hand sprite displays the equipped item, providing clear visual feedback.

### 8. Abstract Base Class with Template Method Pattern

Created **Countdowners** as an abstract base class for timed hazards. The base class handles shared countdown logic, while derived classes (such as `Explosion` and `SecurityCamera`) implement hazard-specific behavior in `AfterAction()`. This demonstrates the Template Method pattern and strong code reuse.

### 9. Coroutine-Based Animation and Transitions

Built a transition system using coroutines with `CanvasGroup` fade effects and `Action` callbacks to trigger game state changes during fades. Uses `Time.unscaledDeltaTime` to ensure transitions function while the game is paused.

The `MovingObject` class uses coroutines with `Vector3.MoveTowards` for smooth door animations, with direction-based logic controlled through enums.


![08174-ezgif com-video-to-gif-converter](https://github.com/user-attachments/assets/e0ef922e-ae28-4aab-b81d-ee72ba7eb0fe)
