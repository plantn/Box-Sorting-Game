# Box Sorting Game

Unity 2D implementation demonstrating finite state machine AI, physics-based interactions, and modular software architecture.

## Technical Overview

This project implements autonomous NPC behavior through a finite state machine, demonstrating core software engineering principles including SOLID design, clean architecture, and performance optimization techniques.

## Architecture

### SOLID Principles Implementation

**Single Responsibility Principle**
- `NPCController`: NPC behavior management and state coordination
- `BoxEntity`: Box properties and scoring mechanics
- `ScoreManager`: Game score tracking and persistence
- `RailScript`: Object movement acceleration logic

**Open/Closed Principle**
- `NPCStateMachine`: Abstract base enabling state extension without modification
- `EntityBase`: Extensible foundation for entity implementations
- Interface-driven design allowing behavior extension

**Liskov Substitution Principle**
- State classes fully interchangeable through `NPCStateMachine` abstraction
- Entity implementations substitutable through `EntityBase` hierarchy

**Interface Segregation Principle**
- `IPickable`: Minimal interface defining only pickup contract
- Clean separation between entity lifecycle and interaction behavior

**Dependency Inversion Principle**
- High-level modules depend on abstractions (`NPCStateMachine`, `IPickable`)
- Concrete implementations isolated from direct dependencies

### Design Patterns

**Finite State Machine**
```
IdleState → SeekState → PickupState → CarryState → DropState → IdleState
```
- Encapsulated state behavior with clean transition logic
- Centralized state management through abstract base class
- Event-driven state transitions with validation

**Singleton Pattern**
- `ScoreManager`: Thread-safe lazy initialization
- Single responsibility for game state persistence
- Controlled access to shared game data

**Entity-Component Architecture**
- Modular entity design through `EntityBase` abstraction
- Component composition enabling flexible object behavior
- Clear separation between data and behavior

## AI Behavior Implementation

### State Machine Architecture

**IdleState**
- Position centering at world origin
- Box detection within defined radius
- Transition logic to SeekState when targets available

**SeekState**
- Closest box calculation using spatial queries
- Movement toward target with configurable speed
- Distance-based transition to PickupState

**PickupState**
- Precise positioning relative to target object
- Parent-child relationship establishment preserving scale
- Directional hand animation based on relative position

**CarryState**
- Color-based dropzone identification
- Navigation to appropriate sorting location
- Continuous target validation during transport

**DropState**
- Object release with physics preservation
- Timeout implementation preventing immediate re-pickup
- State cleanup and transition back to IdleState

## Project Structure

```
Assets/Scripts/
├── Core/                 # System managers and singletons
├── Entities/             # Game object implementations
│   ├── NPC/             # NPC controller and state machine
│   │   └── States/      # FSM state implementations
│   └── Box/             # Box entity and behavior
├── Shared/              # Common interfaces and constants
├── Systems/             # Game systems (spawning, etc.)
└── World/               # Environment interaction scripts
```

## Dependencies

- Unity 2022.3+ LTS
- Universal Render Pipeline
- Input System Package
- TextMeshPro
- Physics2D Module

## License

MIT License - See LICENSE file for complete terms.
