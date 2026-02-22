# Group3_UnityAssignment2
# Unity Soccer Defense Challenge – Extended Version

This repository contains the completed and extended version of Unity Learn Challenge 4 (Soccer Gameplay Mechanics).  
The project was refactored, optimized, and expanded with new gameplay systems, UI, AI behaviors, and powerups to create a fully playable end-to-end experience.

---

## Project Overview

The player controls a physics-based ball and must defend their goal by deflecting enemy balls while scoring into the opponent’s goal.  
Waves of enemies increase in difficulty over time, with dynamic powerups and enhanced enemy AI.

This version fixes all original challenge bugs and introduces multiple new gameplay features and UI systems.

---

## Completed Challenge Requirements

- Enemy balls move toward Player Goal  
- Player can deflect enemies away  
- Powerups last for limited duration  
- New wave spawns when enemies cleared  
- Wave difficulty increases over time  
- Fully playable with no blocking bugs  

---

## Added Features

### Smash Powerup
- Player jumps and slams using Space  
- Radial knockback based on distance  
- Visual and timing feedback  
- UI cooldown indicator  

### Enhanced Enemy AI
- Aggressive (direct rush)  
- Defensive (avoid player)  
- Evasive (zig-zag movement)  
- Speed scales with wave level  

### Gameplay UI System
- Wave counter  
- 3-minute game timer  
- Powerup cooldown bars  
- Player and enemy goal counters  
- Pause screen  
- Controls screen  
- Game Over screen  
- Main Menu  

### GameManager Architecture
Centralized system controlling:
- Timer  
- Waves  
- Game state  
- Pause  
- UI updates  

### Code Optimization
- Inspector-configurable variables  
- Modular enemy behaviors  
- Reusable powerup system  
- Removed hard-coded values  
- Cached references  
- Physics moved to FixedUpdate  
- Eliminated redundant logic  

---

## Branching Strategy

The project used feature-based branching:

- main – Stable integrated version  
- feature/enemy_ai – Enemy behavior system  
- feature/powerup – Smash and powerup mechanics  
- feature/ui – UI screens and HUD  
- feature/code_optimization – Refactor and performance  

---

## Pull Requests and Merging

Each feature branch was developed independently and merged into main after testing.

Example merges:
- feature/enemy_ai to main  
- feature/ui to main  
- feature/powerup to main  
- feature/code_optimization to main  

---

## Conflict Resolution

Merge conflicts occurred mainly in:
- PlayerControllerX  
- GameManager  
- UI references  

Resolution approach:
1. Compared both branch implementations  
2. Preserved newest gameplay logic  
3. Re-tested powerups and UI  
4. Verified wave and timer integration  
5. Finalized unified architecture  

Conflicts were resolved in commit:
"fixed merge issue's and added gamemanager start game function to the HomeMenu"

---

## Major Commits

Gameplay and Powerups
- Added smash powerup and cooldown system  
- Improved smash to Space-triggered jump slam  
- Implemented player and enemy goal scoring  

Enemy AI
- Implemented 3 enemy behaviors  
- Added wave-scaled speed  
- Added debug colors  

UI System
- Designed all game UI screens  
- Added gameplay HUD with sliders and timer  
- Implemented pause and game over  
- Added wave counter and 3-minute timer  

Architecture and Refactor
- Added GameManager system  
- Refactored gameplay scripts  
- Configurable Inspector settings  
- Optimized spawn and AI logic  

---

## Team Roles

Almahahahhaa – UI system, GameManager, gameplay integration  
Yazan-k05 – Enemy AI and code optimization  
Malshebli379 – Powerup mechanics and smash system  

---

## How to Run

1. Open project in Unity 6 or Unity 2022+  
2. Open scene: MainScene  
3. Press Play  

Controls:
- Move: WASD or Arrow Keys  
- Smash: Space  
- Pause: UI Button  

---

## Repository Structure

Assets/
  Scripts/
    PlayerControllerX.cs  
    EnemyX.cs  
    SpawnManagerX.cs  
    GameManager.cs  
    GamePlayUI.cs  
  Prefabs/  
  UI/  
  Materials/  

---

## Assignment Coverage

Challenge Completion (50%)
All original Unity tasks fixed and extended

Version Control (15%)
- Feature branches  
- Frequent commits  
- Pull requests  
- Conflict resolution  
- Documented history  

Modifications and Creativity (10%)
- Smash powerup  
- Enemy AI types  
- UI system  
- GameManager architecture  
- Code optimization  

---

## Final Result

The project evolved from a broken Unity challenge prototype into a polished, fully playable mini-game with structured architecture, scalable AI, UI systems, and enhanced mechanics.

Unity Learn Challenge to complete game loop experience

---

Repository link:
Add your GitHub repository URL here
