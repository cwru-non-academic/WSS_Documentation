# WssStimulationCore

Unity-agnostic WSS stimulation core that manages connection, setup via a queued step runner, and a background streaming loop. Public mutator methods enqueue device edits and return immediately.

- Source: `Assets/SubModules/WSSInterfacingModule/WSSBaseCode/WssStimulationCore.cs`
- Implements: IStimulationCore, IBasicStimulation

Signature:

~~~csharp
public sealed class WssStimulationCore : IStimulationCore, IBasicStimulation
~~~

Notes:
- This manual page is a temporary summary so you can reach the core quickly from Getting Started.
- For full API (XML docs, members, parameters), enable DocFX API metadata for your project; I can wire the Unity/Newtonsoft references and regenerate.
