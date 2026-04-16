# Setup Order and Modification

[Home](../index.md) | [Back to Concepts](../concepts.md)

This guide explains the two main ways setup can happen in the WSS stack:

- initial setup driven by the core during startup
- later setup changes driven by commands exposed through an integration library or application

That distinction matters because you do not always need to modify the core just to change setup behavior.

## Two Ways Setup Can Happen

There are two important setup paths in the current architecture.

### 1. Initial Setup in the Core

This is the startup setup that happens automatically after connection succeeds.

- `WssStimulationCore.Initialize()` begins connection and setup
- once connected, the core calls `NormalSetup()`
- `NormalSetup()` seeds the setup queue for each target and drives the initial device configuration

This is the right place to make a change only when you want to modify the default setup that should happen every time the core starts.

### 2. Setup Changes After Startup

Setup can also be changed after initial startup by sending the right sequence of setup-related commands.

That means an integration library or application can often replace or override setup behavior without modifying the core itself, as long as it exposes the necessary methods.

Examples:

- loading a different config after startup
- clearing and recreating event or schedule definitions
- uploading a different waveform setup
- changing event configuration or schedule configuration during a session
- issuing enough setup commands to effectively create a whole new setup after the initial one is complete

So the key distinction is:

- modify the core when you want to change the default startup setup for every run
- use integration-library or application-level setup commands when you only want to change the setup that is active during a particular workflow or session

## Current Initial Setup Order

The current documented startup flow is:

1. `Initialize()`
2. connect transport
3. `NormalSetup()`
4. `Clear`
5. `Create`
6. `Config`
7. `Edit`
8. `Add`
9. `Sync`
10. queue drains to `Ready`
11. `StartStim(...)`
12. device reports `Started`
13. streaming begins

This is the best current high-level description of what the core does during automatic setup.

## Command-Intent Translation

Translating those buckets into likely command intent:

1. Clear old device state
2. Create base structures
3. Configure created items
4. Add relationships
5. Sync or activate prepared groups
6. Start stimulation
7. Begin streaming once the device has entered the started state

In more concrete terms, that usually means:

### 1. Clear Old Device State

- clear events
- clear schedules
- clear contacts
- clear all

Plain English: remove old programmed state so the new setup does not inherit unexpected leftover definitions from a previous run.

Example commands:

- `Clear(1)`
  - clears event definitions already stored on the device
- `Clear(2)`
  - clears schedule definitions already stored on the device
- `Clear(3)`
  - clears contact configurations already stored on the device
- `Clear(0)`
  - clears everything so setup starts from a clean state

### 2. Create Base Structures

- create contact configs
- create events
- create schedules

Plain English: create the device objects that later setup steps will edit and connect together.

Example commands:

- `CreateContactConfig(...)`
  - creates a contact pattern the device can use later in an event
- `CreateEvent(...)`
  - creates a stimulation event definition with IDs and base timing/shape information
- `CreateSchedule(...)`
  - creates a schedule container that events can later be added to

### 3. Configure Created Items

- edit event config
- change schedule config
- optional waveform or IPD setup

Plain English: now that the objects exist, fill in the details that make them behave the way you want.

Example commands:

- `EditEventAmp(...)`
  - changes the event's amplitude settings
- `EditEventPw(...)`
  - changes the event's pulse-width settings
- `EditEventShape(...)`
  - changes the waveform or shape settings used by the event
- `ChangeScheduleDuration(...)`
  - changes how long the schedule runs
- `ChangeScheduleState(...)`
  - changes whether the schedule is enabled or disabled
- `UpdateIPD(...)`
  - changes inter-phase delay behavior when that is part of the setup
- `WaveformSetup(...)`
  - uploads a waveform and points an event at it when waveform-based setup is needed

### 4. Add Relationships

- add event to schedule
- move/remove as needed

Plain English: connect the already-created pieces together so the device knows which events belong to which schedules.

Example commands:

- `AddEventToSchedule(...)`
  - inserts an event into a schedule so that schedule can trigger it
- `MoveEventToSchedule(...)`
  - moves an event from one schedule context to another
- `DeleteEventFromSchedule(...)`
  - removes an event from a schedule when the relationship should no longer exist

### 5. Sync or Activate Prepared Groups

- sync group
- prepare for start

Plain English: finalize grouped behavior so the prepared setup is ready to run coherently when stimulation starts.

Example commands:

- `SyncGroup(...)`
  - tells the device to synchronize grouped behavior before stimulation begins
- `ChangeGroupState(...)`
  - changes whether a group is active or inactive as part of the prepared setup

### 6. Start Stimulation

Plain English: once setup is complete, tell the device to begin stimulation using the programmed setup.

Example commands:

- `StartStim(...)`
  - tells the device to switch from prepared setup into active stimulation
- `StopStim(...)`
  - stops stimulation when needed; this is not part of setup creation, but it is often part of safely replacing or changing setup later

This is the ordering you should preserve unless you have a strong reason to change it.

## Why the Order Matters

The setup order exists to preserve dependencies between commands.

- Do not edit something before it exists.
- Do not add an event to a schedule before both the event and schedule exist.
- Do not sync or start before the device has the intended setup.
- Do not move `StartStim(...)` earlier unless you are sure the remaining setup actions are no longer required before stimulation begins.

In other words, the safe default is:

- clear first
- create before configure
- configure before add/sync/start
- start only after setup is complete

## When You Need To Modify the Core

Modify the core setup when you want to change the automatic startup behavior itself.

Typical cases:

- a new setup step must always happen after connect
- the order of startup commands must change for all users of the core
- startup setup must become firmware-dependent
- `Ready` should only be reached after a different or expanded set of setup steps completes

These changes usually belong in `WssStimulationCore.NormalSetup()` or the setup runner behavior around it.

## When You Do Not Need To Modify the Core

You do not need to modify the core if the integration library or application already exposes enough setup-related methods to recreate the desired setup after startup.

Typical cases:

- you want a different setup for one workflow or experiment
- you want to reload configuration while the program is running
- you want to replace events, schedules, or waveforms after the initial setup
- you want the application to own setup changes instead of baking them into every startup

In those cases, setup can be modified at a higher level by calling the exposed WSS methods in the right order.

## Safe Process for Changing Setup

Whether you modify startup setup or runtime setup, use this process:

1. identify the dependency of the new or changed step
2. place it after the objects it depends on exist
3. place it before any later step that assumes the change is already applied
4. check whether the step is firmware-specific and should be gated
5. decide whether it belongs only in initial setup or should also be possible after startup
6. confirm that `Ready` and `Started` still mean what the rest of the system expects

## Common Mistake

One common mistake is assuming that changing setup always requires changing the core.

That is not true.

The core only owns the default initial setup path. Once the application is running, setup can also be changed from higher layers if those methods are exposed.

## Related Guides

- [Core Architecture (Transport, Codec, Core)](core-architecture.md)
- [Start Here: Minor Core Modifications](start-here-minor-core-modifications.md)
- [Start Here: Developing an Application](start-here-developing-an-application.md)
- [Start Here: Building a New Integration Library](start-here-building-a-new-integration-library.md)

Navigation:

- [Back to Home](../index.md)
- [Back to Concepts](../concepts.md)
- [Previous: Core Architecture (Transport, Codec, Core)](core-architecture.md)
- [Next: Config Files Reference](config-files-reference.md)
