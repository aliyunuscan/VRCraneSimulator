# VR Crane Simulator: Occupational Health & Safety Training

A physics-driven Virtual Reality prototype designed to provide foundational crane operation experience, focusing on Occupational Health and Safety (OHS). Developed during my tenure as an Application and Simulation Developer Intern, this project prioritizes core mechanics and rapid prototyping to demonstrate practical safety concepts.

## 📋 Project Overview

The simulator places users in a risk-free virtual industrial environment, allowing them to practice basic load management and spatial awareness. To ensure a rapid development cycle and focus primarily on VR interactions, the project utilizes pre-made 3D visual assets. Therefore, rather than focusing on high-fidelity visual realism, the "realistic" aspect of this simulation is heavily dependent on utilizing the physics engine to test hazard prevention and basic mechanical handling.

🎮 **Click here to visit the Game Page!:** https://sidequestvr.com/app/43243/vr-tower-crane-simulator

## ✨ Key Features

- **Physics-Based Mechanics:** While not visually photorealistic, the simulation leverages the physics engine to simulate cable tension, load sway, and mechanical momentum to replicate operational challenges.
- **Rapid Prototyping Approach:** Built using ready-made visual assets, allowing the development focus to remain strictly on C# scripting, VR interactions, and the evaluation engine.
- **Dynamic Scoring & Evaluation Engine:** A real-time assessment system that grades performance based on:
  - **Safety Penalties:** Deducts points for critical errors, load collisions, sudden movements, or violations of basic OHS protocols.
  - **Operational Efficiency:** Rewards smooth handling and optimal completion times.
- **Universal VR Compatibility:** Primarily developed for **Oculus (Meta Quest)** using **Unity 6**, yet compatible with major VR platforms via the XR Interaction Toolkit.

## 🛠 Technical Stack

- **Game Engine:** Unity 6 (LTS)
- **Target Platform:** Meta Quest (Oculus) / Cross-Platform VR
- **XR Framework:** Unity XR Interaction Toolkit / OpenXR
- **Programming Language:** C#

## 🚀 Getting Started

### Prerequisites
- Unity 6 or higher.
- A compatible VR Headset (Meta Quest 2/3/Pro recommended).
- Oculus Link or Air Link for PC-based testing.

### Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/aliyunuscan/VRCraneSimulator.git
   ```
2. Open the project in the **Unity Hub** using **Unity 6**.
3. Ensure **XR Plug-in Management** is configured for your device in Project Settings.
4. Open the main scene located in `Assets/Scenes/MainSimulation`.
5. Build and run on your headset or use the Unity Editor's Play mode with a linked device.

## 🛡️ OHS Training Focus

This project transcends basic gameplay by acting as a professional training tool. It strictly enforces industrial safety guidelines, testing the operator's reaction times to simulated workplace hazards and their adherence to strict operational limits.

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
