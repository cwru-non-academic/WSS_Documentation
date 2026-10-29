# README contrastive-framing inventory

Read-only whole-file review of all ten preservation-branch README sources, including their complete prose and examples. Supplemental locally available origin/main snapshots are reviewed separately below. Quotes below are exact source text joined across indicated lines. Only review artifacts outside repositories were written. Code negation, shell flags such as --no-build, and Git insteadOf configuration keys are excluded as executable syntax, not rhetoric. Ordinary chronological before/after and neutral choice lists are excluded unless they express a meaningful restriction or contrast. Headings and genuine limits are included with KEEP verdicts. No source edits or runtime/build validation were performed.

## Snapshot

- Core/README.md: docs/release-1.0-onboarding-review at 698a6a09d1f0603a0a3f776dcf2a7c8952d91340
- CSharp/README.md: docs/release-1.0-onboarding-review at 11de7b7b88bca2d182fd20808fcc10973ec0781f
- CSharpCLI/README.md: docs/release-1.0-onboarding-review at b49c08b14825f7366f41bc2cc69afd797788b70d
- Python/README.md: docs/release-1.0-onboarding-review at e0008f7234bcb4fa16eb911672156633c959f688
- PythonCLI/README.md: docs/release-1.0-onboarding-review at 13f92cce16a65fed014ec1a517612424f8b0ffc3
- ROS/README.md: docs/release-1.0-onboarding-review at 6c130a8887c68b96147dd8b96a2d1461ea41b19b
- Unity/README.md: docs/release-1.0-onboarding-review at f72f609f5a9aab37e4e8277965d633b0d628e67f
- UnityGUI/README.md: docs/release-1.0-onboarding-review at 75b8cbafc5b9633b529ab8422993fe35959c9c06
- MATLAB/README.md: docs/release-1.0-onboarding-review at e8ddfe9a0c9479d971571c1af7c3aca7c49e1196
- MATLABCLI/README.md: docs/release-1.0-onboarding-review at 4d115e731823c1a7c5dc50aee55a4de2baaf11ac

## Preservation-branch semantic contrasts and boundaries

57 contrast/boundary passages: 47 KEEP, 10 REWRITE, 0 REMOVE. Twelve neutral candidates are retained in the excluded appendix. Original entry numbers stay stable for cross-references.

### 1. Core/README.md:23-24

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\Core\README.md

> Keep one coherent release together; do not mix individual DLLs from other releases. Integration repositories already carry their pinned distribution.

Reader question: Can I replace an individual DLL?

Verdict: **KEEP**. Compatibility restriction; preserve the coherent-release rule.

### 2. Core/README.md:52-52

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\Core\README.md

> ## First checks without hardware

Reader question: Which checks can I run with no device?

Verdict: **KEEP**. Useful task boundary in a heading.

### 4. Core/README.md:67-69

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\Core\README.md

> This consumer uses TestModeTransport and does not open hardware. For an interactive simulated application, follow the [C# CLI README](https://github.com/cwru-non-academic/CLI_CSharp_WSS_Application#readme).

Reader question: Will this consumer connect to hardware?

Verdict: **KEEP**. Explicit test-side-effect limit; not ornamental contrast.

### 5. Core/README.md:78-79

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\Core\README.md

> Automated software tests and package validation are separate from physical device testing. Hardware diagnostics are opt-in; see the test guide before using them.

Reader question: Do these checks validate a physical device?

Verdict: **KEEP**. Distinguishes validation evidence and opt-in hardware diagnostics.

### 6. CSharp/README.md:10-11

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\CSharp\README.md

> - Keep the complete committed `lib/` distribution, including `backends/` and   `runtimes/`, with the library. Do not replace individual DLLs from another release.

Reader question: Can I upgrade one bundled DLL?

Verdict: **KEEP**. Preserves a real dependency compatibility constraint.

### 7. CSharp/README.md:26-28

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\CSharp\README.md

> If your application's project file is at the repository root, exclude the submodule's source from its default SDK compile glob by adding this inside `MyApp.csproj` before building:

Reader question: Why does a root project compile submodule files?

Verdict: **KEEP**. Compile-glob exclusion is necessary setup, not rhetoric.

### 9. CSharp/README.md:49-49

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\CSharp\README.md

> ## First run without hardware

Reader question: How do I try this with no device?

Verdict: **KEEP**. Simulator entry heading.

### 10. CSharp/README.md:51-53

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\CSharp\README.md

> Use this as a console application's `Program.cs`. Test mode does not open Serial or BLE devices. The example creates configuration in `Config/`, waits up to five seconds for initialization, reports state, and disposes the controller.

Reader question: Will the first example open Serial/BLE?

Verdict: **KEEP**. Real simulator side-effect boundary.

### 11. CSharp/README.md:73-76

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\CSharp\README.md

> Resolve the application-owned configuration directory before creating the controller. `Initialize()` starts its background ticks; callers do not need their own `Tick()` loop. Dispose the controller on exit. Test mode is a simulated lifecycle; `Conformance` is the separate deterministic emulator used by the tests.

Reader question: Who owns ticking, and which test mode is this?

Verdict: **REWRITE**. Keep both scheduling and mode distinctions, state ownership directly.

Optional direct rewrite: Resolve the application configuration directory before construction. Initialize starts the controller's background tick loop. Dispose on exit. Test simulates lifecycle; Conformance supplies the deterministic test emulator.

### 12. CSharp/README.md:78-82

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\CSharp\README.md

> For hardware, select `StimulationTransportKind.Serial` with `SerialPort`, or `StimulationTransportKind.Ble` with `BleDeviceName`, `BleDeviceId` or `BleAutoSelect`. The default transport is Serial, so select Test explicitly for software-only experiments. Review configuration and device setup before using stimulation methods.

Reader question: Which explicit transport should my software-only example choose?

Verdict: **KEEP**. Default live Serial versus explicit Test changes device I/O.

### 13. CSharpCLI/README.md:20-20

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\CSharpCLI\README.md

> ## Start without hardware

Reader question: How do I run without a device?

Verdict: **KEEP**. Useful simulator heading.

### 14. CSharpCLI/README.md:39-40

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\CSharpCLI\README.md

> Conformance mode creates and removes its own temporary configuration and does not connect to hardware.

Reader question: Does conformance touch my files or hardware?

Verdict: **KEEP**. Temporary config and no-hardware boundaries matter.

### 15. CSharpCLI/README.md:55-57

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\CSharpCLI\README.md

> Replace `COM3` with the intended port (for example `/dev/ttyUSB0` on Linux). BLE also accepts `--ble-device-id=ID` or `--ble-auto` for a compatible Nordic UART Service device. Running without options selects live Serial with auto-detection.

Reader question: What happens when I omit transport options?

Verdict: **KEEP**. Default live Serial/auto-selection is operationally important.

### 16. CSharpCLI/README.md:71-71

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\CSharpCLI\README.md

> | `--serial-smoke` | Construct and dispose Serial without opening a device, then exit |

Reader question: Does serial-smoke open a port?

Verdict: **KEEP**. Defines exact load-only check scope.

### 17. Python/README.md:10-10

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\Python\README.md

> - A .NET runtime compatible with the bundled WSS assemblies, with the same architecture as Python. `pythonnet` bridges Python to that runtime; installing the Python package alone does not install .NET.

Reader question: Does pip install the .NET runtime?

Verdict: **KEEP**. Common dependency misunderstanding with a real setup consequence.

### 18. Python/README.md:13-13

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\Python\README.md

> On macOS/Linux, select CoreCLR before starting Python: `export PYTHONNET_RUNTIME=coreclr`. In PowerShell, use `$env:PYTHONNET_RUNTIME = 'coreclr'` when using CoreCLR. Use a runtime compatible with your release payload; a .NET SDK is only needed when building .NET code.

Reader question: Do I need the SDK or just a runtime?

Verdict: **KEEP**. Only-needed clause reduces prerequisite burden accurately.

### 20. Python/README.md:32-32

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\Python\README.md

> ## First run without hardware

Reader question: How do I test without a device?

Verdict: **KEEP**. Simulator navigation.

### 21. Python/README.md:67-67

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\Python\README.md

> The controller advances the stack on its background tick thread. Keep it alive while using it, wait for `Started()` to confirm the normal initialized stack is running, and always call `Shutdown()` in cleanup. `Ready()` reports a transient setup state; the default setup starts stimulation and advances to the started/streaming states covered by `Started()`. In this example those states use simulated transport only. The application owns config/log paths; avoid relying on the current working directory or `WssConfig.default()` when the library is a submodule.

Reader question: What state, ticking, cleanup and paths should I use?

Verdict: **REWRITE**. Preserve transient Ready, simulated-only scope and explicit paths; replace avoid-relying corrective tail with a direct instruction.

Optional direct rewrite: The controller owns background ticks. Wait for Started and call Shutdown in cleanup. Ready is transient during default startup. This example uses simulated transport. Supply application-owned configuration and log paths explicitly when using a submodule.

### 22. Python/README.md:71-71

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\Python\README.md

> Keep the same lifecycle and replace simulation with your selected transport. For serial, use `test_mode=False` and an explicit `serial_port` such as `COM5` or `/dev/ttyACM0`. For BLE, set `transport="ble"` and an exact `ble_device_name` or `ble_device_id`; `ble_auto=True` explicitly permits automatic selection. BLE uses the packaged Windows, Linux, or macOS backend. Do not combine BLE selection with serial/test options.

Reader question: Can BLE be combined with serial/test flags?

Verdict: **KEEP**. Real conflicting-option constraint; retain transport-specific choices.

### 23. PythonCLI/README.md:19-19

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\PythonCLI\README.md

> The submodule command uses HTTPS for the SSH-style URLs stored in older submodule entries; it needs no global Git configuration change.

Reader question: Does this command affect all my Git repositories?

Verdict: **REWRITE**. Command-local scope is useful; state it positively.

Optional direct rewrite: The command applies its HTTPS substitution to this submodule update invocation.

### 25. PythonCLI/README.md:23-23

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\PythonCLI\README.md

> ## First run without hardware

Reader question: How do I start with no device?

Verdict: **KEEP**. Simulator navigation.

### 26. PythonCLI/README.md:30-30

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\PythonCLI\README.md

> At the prompt, enter `status` to inspect readiness, `help` for commands, and `quit` to shut down. `--test` selects simulated transport and does not open a serial/BLE connection. Allow setup to complete before issuing commands.

Reader question: Does --test connect to hardware?

Verdict: **KEEP**. Device-I/O boundary.

### 27. PythonCLI/README.md:32-32

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\PythonCLI\README.md

> The app uses `Config/` for `stimConfig.json`, `stimParams.json`, and `modelConfig.json`; logs go to `logs/`. These paths are relative to the application checkout, not your shell's current directory.

Reader question: Where do configuration and logs resolve?

Verdict: **REWRITE**. Positive location rule fully answers the question.

Optional direct rewrite: The app resolves Config/ and logs/ relative to its application checkout.

### 28. PythonCLI/README.md:48-48

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\PythonCLI\README.md

> Use your device's exact advertised name, or `--ble-device-id` for its identifier. `--ble-auto` explicitly permits automatic selection. Keep `--test` separate from physical-transport options.

Reader question: May I mix --test and physical options?

Verdict: **KEEP**. Conflicting options are a real configuration limit.

### 29. ROS/README.md:23-23

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\ROS\README.md

> The submodule command uses HTTPS for the SSH-style URLs stored in older submodule entries; it needs no global Git configuration change.

Reader question: Will the Git override persist globally?

Verdict: **REWRITE**. Keep useful scope in positive form.

Optional direct rewrite: This invocation applies the HTTPS substitution while initializing the submodule.

### 30. ROS/README.md:25-25

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\ROS\README.md

> Use the same Python environment for `pip`, `colcon`, and the running node. `HFI_WSS_Python_Interface/` is the retained local submodule directory name; the current repository is `Python_WSS_Integration_Lib`. Keep its complete `Cs_Libraries/` directory and platform backend/native assets together.

Reader question: Why is the checkout directory named HFI when the repo is Python?

Verdict: **KEEP**. Necessary distinction between retained directory and canonical repository.

### 31. ROS/README.md:27-27

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\ROS\README.md

> ## First run without hardware

Reader question: How do I try ROS without a device?

Verdict: **KEEP**. Simulator entry heading.

### 32. ROS/README.md:38-38

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\ROS\README.md

> This command does not open a serial/BLE connection. The default node maps `/dom/ch1/stimulation_value` through `/dom/ch9/stimulation_value` to `ch1` through `ch9`. In another sourced terminal:

Reader question: Does the example launch connect to Serial/BLE?

Verdict: **KEEP**. Defines test-side-effect scope.

### 34. ROS/README.md:52-52

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\ROS\README.md

> The supplied launch entry point is `ros2 launch hfi_ros_wss_driver launch_ros_wss_driver.launch.py config_file:=session.yaml`. This launcher reads a flat WSS settings YAML from the package's `config/` directory, rather than the ROS parameter-file format above. Set its paths and topic names for your installation before launching; use the explicit simulation command above for the first run.

Reader question: Can I use the same YAML for ros2 run and the supplied launcher?

Verdict: **REWRITE**. Preserve incompatible file schemas; explain each positively.

Optional direct rewrite: The supplied launcher reads flat WSS settings YAML from package config/. The ros2 run --params-file form uses a node-name/ros__parameters block. Set paths and topics for the selected entry point.

### 37. Unity/README.md:19-19

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\Unity\README.md

> 2. Add `"com.unity.nuget.newtonsoft-json": "3.2.1"` to the `dependencies` object in `Packages/manifest.json`. The library's bundled Newtonsoft DLL is excluded by its plugin import settings; keep those `.meta` files.

Reader question: Which Newtonsoft assembly should Unity import?

Verdict: **KEEP**. Exclusion/import metadata prevents duplicate assembly conflicts.

### 38. Unity/README.md:21-21

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\Unity\README.md

> 4. Add a `Stimulation` component to a GameObject. Before entering Play mode, set **Transport Type** to **Test** for an in-memory startup without device I/O. Auto selection and Device Name are ignored in this mode.

Reader question: Will Test use configured device discovery/name?

Verdict: **KEEP**. Test mode's lack of device I/O and ignored selectors are genuine behavior.

### 39. Unity/README.md:23-23

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\Unity\README.md

> The library includes the managed Core and transport DLLs under `Plugins/WSS_Core_Interface`. Do not add another copy of those assemblies to the project.

Reader question: Should I import another copy of Core?

Verdict: **KEEP**. Duplicate-assembly restriction.

### 40. Unity/README.md:48-48

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\Unity\README.md

> Enter Play mode and check the Unity Console. `Stimulation` initializes on enable, ticks in `Update`, and shuts down when disabled or when Play mode ends. Call its methods on Unity's main thread. The example only reports status; it sends no application stimulation commands.

Reader question: Does the sample script stimulate, or only report status?

Verdict: **KEEP**. Precise application-command scope; avoids implying automatic Core setup sends no commands.

### 42. Unity/README.md:58-58

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\Unity\README.md

> Update the entire compatible Core artifact set together with its plugin metadata; avoid mixing DLLs from different releases. Core releases are published from [WSSCore](https://github.com/cwru-non-academic/WSSCore/releases). The application records the library version through its submodule commit.

Reader question: Can I combine payloads across releases?

Verdict: **KEEP**. Compatibility rule; whole-artifact update required.

### 44. UnityGUI/README.md:21-21

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\UnityGUI\README.md

> For an existing clone, run `git submodule update --init --recursive` (or the HTTPS command above) before opening Unity. Add this repository's root folder in Unity Hub, then open it with the pinned editor. Allow package resolution and script import to finish. Keep the wrapper's bundled DLLs and `.meta` files together; do not add duplicate Core assemblies.

Reader question: Can I add duplicate Core DLLs after opening Unity?

Verdict: **KEEP**. Concrete assembly-conflict prevention rule.

### 45. UnityGUI/README.md:23-23

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\UnityGUI\README.md

> ## Run without hardware

Reader question: How do I run with no device?

Verdict: **KEEP**. Simulator heading.

### 46. UnityGUI/README.md:26-26

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\UnityGUI\README.md

> 2. Before pressing Play, find the scene's **Stimulation** component and set **Transport Type** to **Test**. The Test transport simulates WSS without opening serial or BLE connections.

Reader question: Will Test open Serial/BLE?

Verdict: **KEEP**. Concrete transport behavior.

### 47. UnityGUI/README.md:27-27

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\UnityGUI\README.md

> 3. Disable the scene's **portScannerMUIP** components for this run so the serial-selection UI does not deactivate or reselect the stimulation component. Ensure the GameObject containing **Stimulation** is active.

Reader question: Why must I disable portScannerMUIP for simulation?

Verdict: **KEEP**. Explains an otherwise surprising necessary action.

### 48. UnityGUI/README.md:28-28

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\UnityGUI\README.md

> 4. Set **Test Mode** on any **CrabSerialCNTR** component to true, or keep that component disabled. This is a separate optional serial-input source and has its own transport setting.

Reader question: Does Crab input inherit the WSS Test transport?

Verdict: **KEEP**. Independent optional input source requires its own selection.

### 49. UnityGUI/README.md:31-31

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\UnityGUI\README.md

> The scene's saved device settings are not a simulator preset; set both the WSS and optional input-source settings before Play.

Reader question: Can I press Play with the saved scene settings?

Verdict: **REWRITE**. Keep required configuration action; positive prose can replace the corrective premise.

Optional direct rewrite: Before Play, set the WSS transport and optional input-source settings to their simulator values.

### 51. UnityGUI/README.md:44-44

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\UnityGUI\README.md

> In the simulator, explore the scene's on-screen controls first. The keyboard handlers in `MinMax` use **Space** for a test input, **Up/Down** to adjust stimulation, **Left/Right** to select the bound, and **Escape** or **F8** to stop. Releasing Space or an adjustment key zeroes the corresponding input. File input and external serial input are separate workflows selected by the application UI.

Reader question: Are file-driven and external-serial input the same workflow?

Verdict: **KEEP**. Genuine UI workflow distinction.

### 52. UnityGUI/README.md:48-48

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\UnityGUI\README.md

> Stop Play mode, review the configuration, and change the **Stimulation** transport to **Serial** or **BLE**. Choose **Auto** or provide a manual **Device Name** (serial port, BLE advertised name, or `id:<identifier>`). Only enable the optional Crab serial input when that input source is intentionally in use.

Reader question: When should I enable optional Crab input?

Verdict: **KEEP**. Only-enable is an operational restriction for intentional input selection.

### 53. MATLAB/README.md:16-19

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\MATLAB\README.md

> Select the CLR in a fresh MATLAB process before loading any .NET assembly. See MathWorks' [runtime selection documentation](https://www.mathworks.com/help/matlab/ref/dotnetenv.html) for your MATLAB release. Runtime selection can persist across sessions; the library does not select or change it for you.

Reader question: Who selects the runtime, and does the wrapper change it?

Verdict: **REWRITE**. Keep fresh-process selection and persistence; express caller ownership directly.

Optional direct rewrite: Select the CLR in a fresh MATLAB process before loading .NET assemblies. Runtime selection is caller-owned and can persist between sessions; consult MathWorks documentation for your release.

### 54. MATLAB/README.md:33-34

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\MATLAB\README.md

> `git submodule update --init --recursive` after cloning. The managed build uses the bundled Core DLL, so a separate Core checkout is unnecessary.

Reader question: Do I need to clone/build Core separately?

Verdict: **REWRITE**. Build dependency is useful; positive statement is sufficient.

Optional direct rewrite: The managed host builds against the Core DLL bundled in this library.

### 55. MATLAB/README.md:36-36

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\MATLAB\README.md

> ## First run without hardware

Reader question: How do I try MATLAB without a device?

Verdict: **KEEP**. Simulator heading.

### 56. MATLAB/README.md:38-41

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\MATLAB\README.md

> Save the following function as `wss_smoke.m` in your application root, then call `wss_smoke` from that directory in a fresh MATLAB process. The in-memory emulator is selected explicitly and does not open a physical transport. Function scope ensures cleanup runs on return or error:

Reader question: Will this smoke function open a physical connection?

Verdict: **KEEP**. Explicit emulator side effects plus cleanup scope.

### 58. MATLAB/README.md:66-70

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\MATLAB\README.md

> `TestMode=true` selects Core's simulated transport; `ConformanceMode=true` selects the behavior-test emulator. Choose one. Without either flag, initialization uses serial and can automatically select a port. For a deliberate serial session, pass `'SerialPort', 'COM3'` (or your operating system's port path) and omit the emulator flags.

Reader question: Which flags conflict, and what happens when I omit both?

Verdict: **KEEP**. Real mutually exclusive choices and live serial default.

### 59. MATLAB/README.md:80-82

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\MATLAB\README.md

> Initialize once, wait for the operational state, and call `Shutdown()` when finished. Automatic setup advances into `Started()`/streaming; `Ready()` is a separate state and can be transient during that sequence. The example waits for `Started()`.

Reader question: Should I wait for Ready or Started?

Verdict: **KEEP**. Status predicates differ; transient Ready is essential lifecycle information.

### 60. MATLAB/README.md:83-86

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\MATLAB\README.md

> `onCleanup` handles normal errors and Ctrl+C unwinding; forced process termination cannot run cleanup. The managed host ticks independently of MATLAB's execution queue. Inspect `LastTickError` if a background operation fails; do not mutate raw configuration controllers concurrently with ticking.

Reader question: Does cleanup survive forced termination; may I mutate raw controllers while ticking?

Verdict: **KEEP**. Two genuine lifecycle/concurrency limits. Independent managed ticking also explains normal MATLAB behavior.

### 62. MATLAB/README.md:95-95

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\MATLAB\README.md

> From this repository, run managed checks without MATLAB or hardware:

Reader question: Can I run the managed checks without MATLAB or hardware?

Verdict: **KEEP**. Exact prerequisite/test-scope boundary.

### 63. MATLABCLI/README.md:27-27

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\MATLABCLI\README.md

> ## Run without hardware

Reader question: How do I run with no device?

Verdict: **KEEP**. Simulator heading.

### 64. MATLABCLI/README.md:37-40

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\MATLABCLI\README.md

> `--test` uses the simulated transport. At the prompt, type `status`, `help`, then `quit` to close the controller and return to MATLAB. Setup is asynchronous, so an immediate `status` can report `Ready=0`. Use `--conformance` instead of `--test` to select the in-memory behavior-test emulator; the two flags are mutually exclusive.

Reader question: How do Test and Conformance differ; can I select both?

Verdict: **KEEP**. Real mode distinction and mutual exclusivity.

### 65. MATLABCLI/README.md:42-42

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\MATLABCLI\README.md

> For a finite terminal invocation with no hardware connection:

Reader question: Can I run a finite no-hardware batch?

Verdict: **KEEP**. Useful execution mode and I/O scope.

### 66. MATLABCLI/README.md:48-48

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\MATLABCLI\README.md

> For help without initializing the controller:

Reader question: Does help initialize a controller?

Verdict: **KEEP**. Real help-mode side-effect boundary.

### 67. MATLABCLI/README.md:57-58

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\MATLABCLI\README.md

> The application reads `Config/` and writes logs under `logs/`, independent of MATLAB's current directory. Review your configuration using the

Reader question: Are config/log locations affected by MATLAB's current directory?

Verdict: **REWRITE**. Direct application-anchored location rule suffices.

Optional direct rewrite: Configuration resolves to the application's Config/ directory, and logs resolve under its logs/ directory.

### 68. MATLABCLI/README.md:60-68

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\MATLABCLI\README.md

> Omitting `--test` and `--conformance` selects serial; pass `--serial` with a specific port when intentionally connecting a device:  ```matlab wss_cli.main({'--serial', 'COM3', '--config', fullfile(pwd, 'Config')}); ```  Replace `COM3` with your platform's port name. Without `--serial`, serial mode can automatically select a port.

Reader question: Which mode is selected when flags or a port are omitted?

Verdict: **KEEP**. Live Serial and auto-selection defaults materially change behavior.

### 69. MATLABCLI/README.md:73-74

Source: C:\Users\Owner\Documents\Codex\2026-10-28\task-6\MATLABCLI\README.md

> `--max-retries`, `--tick`, and repeatable `--command` values. `--wrapper-root` explicitly selects a local development wrapper without changing the submodule pin.

Reader question: Does --wrapper-root update the recorded library dependency?

Verdict: **KEEP**. Runtime override versus committed submodule pin is a real scope distinction.

## Excluded preservation-branch candidates

- 3. Core/README.md:61-61: For a minimal independent consumer of an extracted distribution: — EXCLUDED: Neutral description of an independent package consumer; no opposing proposition.

- 8. CSharp/README.md:36-37: Alternatively, keep the application project in a sibling directory such as `src/MyApp/`, outside `external/`, and use that project path in the commands. — EXCLUDED: Ordinary actionable project-layout alternative.

- 19. Python/README.md:28-28: For an application that already includes the submodule, use `git submodule update --init --recursive` instead of `git submodule add`. Commit the application's submodule pin so collaborators get the same wrapper and DLL payload. — EXCLUDED: Ordinary new-versus-existing checkout command selection.

- 24. PythonCLI/README.md:21-21: On Windows, use `py -m venv .venv` and `.\.venv\Scripts\Activate.ps1` instead of the two environment commands above. For an existing checkout, run `git submodule sync --recursive` and the same HTTPS submodule update command before installing. — EXCLUDED: Ordinary platform-specific environment-command substitution.

- 33. ROS/README.md:48-48: Set `ch1_topic` through `ch9_topic` to your publisher topic names; an empty string disables a subscription. Incoming magnitudes are clamped to `[0.0, 1.0]` and passed to `StimWithMode("chN", value)`. — EXCLUDED: Neutral parameter behavior (empty topic disables subscription), without rhetorical opposition.

- 35. ROS/README.md:58-58: For a console without ROS, use the [Python CLI](https://github.com/cwru-non-academic/CLI_Python_WSS_Application#readme). — EXCLUDED: Goal-based link to the console application.

- 36. Unity/README.md:16-16: When cloning an application that already includes the library, use `git clone --recurse-submodules <application-url>` instead. Keep the application's recorded submodule commit so its scripts and bundled DLLs stay together. — EXCLUDED: Ordinary existing-application checkout instruction.

- 41. Unity/README.md:52-52: Stop Play mode before changing the transport. Select **Serial** or **BLE** and choose automatic selection or disable **Auto** and enter a specific **Device Name**. Serial expects a port name; BLE accepts an advertised name, `name:<name>`, or `id:<identifier>` understood by the host backend. Keep the component disabled until configuration and device selection are ready: enabling it begins connection and device setup. — EXCLUDED: Ordinary manual/automatic selector instructions.

- 43. UnityGUI/README.md:15-15: The recorded submodule URL uses SSH. If you use HTTPS without an SSH key, clone without `--recurse-submodules`, enter the repository, and initialize the public submodule with a command-local URL override: — EXCLUDED: Ordinary HTTPS/SSH checkout paths with prerequisites.

- 50. UnityGUI/README.md:42-42: Start with these examples and keep a separate configuration set for each experiment. See the [shared configuration reference](https://cwru-non-academic.github.io/WSS_Documentation/) for field meanings and setup behavior. — EXCLUDED: Neutral experiment-configuration practice; 'separate' is literal.

- 57. MATLAB/README.md:63-64: For a standalone checkout, clone this repository, build `managed/Wss.Matlab.Managed.csproj`, and set `wrapperRoot = pwd` instead. — EXCLUDED: Ordinary standalone-checkout path substitution.

- 61. MATLAB/README.md:90-91: Core defines their units, limits and protocol semantics. Finger aliases and `chN` identify one-based channels; target `0` broadcasts, while `1` through `3` identify devices. — EXCLUDED: Neutral channel/target identifier mapping; 'while' connects two definitions.

Code-only exclusions (not counted as prose contrasts):

- Core/README.md:43: `dotnet build WSS_Core_Interface.sln -c Release --no-restore` — executable syntax, code condition or diagnostic literal; not rhetorical prose.
- Core/README.md:57: `dotnet test tests/Core/WSS.Core.Tests/WSS.Core.Tests.csproj -c Release --no-build --no-restore` — executable syntax, code condition or diagnostic literal; not rhetorical prose.
- Core/README.md:58: `dotnet test tests/BLE/WSS.BLE.Tests/WSS.BLE.Tests.csproj -c Release --no-build --no-restore` — executable syntax, code condition or diagnostic literal; not rhetorical prose.
- CSharp/README.md:66: `while (!controller.Started() && timeout.Elapsed < TimeSpan.FromSeconds(5))` — executable syntax, code condition or diagnostic literal; not rhetorical prose.
- CSharp/README.md:68: `if (!controller.Started())` — executable syntax, code condition or diagnostic literal; not rhetorical prose.
- Python/README.md:46: `if not config_dir.exists():` — executable syntax, code condition or diagnostic literal; not rhetorical prose.
- Python/README.md:58: `    while not controller.Started() and time.monotonic() < deadline:` — executable syntax, code condition or diagnostic literal; not rhetorical prose.
- Python/README.md:60: `    if not controller.Started():` — executable syntax, code condition or diagnostic literal; not rhetorical prose.
- Python/README.md:61: `        raise TimeoutError("WSS did not reach its running state; inspect wss-smoke.log")` — executable syntax, code condition or diagnostic literal; not rhetorical prose.
- PythonCLI/README.md:12: `git -c 'url.https://github.com/.insteadOf=git@github.com:' submodule update --init --recursive` — executable syntax, code condition or diagnostic literal; not rhetorical prose.
- ROS/README.md:16: `git -c 'url.https://github.com/.insteadOf=git@github.com:' submodule update --init --recursive` — executable syntax, code condition or diagnostic literal; not rhetorical prose.
- UnityGUI/README.md:18: `git -c "url.https://github.com/.insteadOf=git@github.com:" submodule update --init --recursive` — executable syntax, code condition or diagnostic literal; not rhetorical prose.
- MATLAB/README.md:55: `while ~controller.Started() && toc(setupTimer) < 10` — executable syntax, code condition or diagnostic literal; not rhetorical prose.
- MATLAB/README.md:58: `assert(controller.Started(), 'WSS setup did not become operational.');` — executable syntax, code condition or diagnostic literal; not rhetorical prose.

## Supplemental local origin/main snapshots

All ten locally available origin/main README snapshots received a whole-file semantic pass. These are local remote-tracking refs, read without fetch. Their freshness and current live publication are NOT established. No claim is made about the live default-branch files or deployed website. Findings describe wording at the recorded hashes; they do not validate old implementation/CI claims. Several historical migration passages are already replaced by the preservation draft. KEEP means preserve the substantive boundary, potentially in maintainer documentation rather than the onboarding README.

- Core/README.md: local origin/main at 80c74f0101387d74cd637ba2a6e0527b4c541d2e
- CSharp/README.md: local origin/main at 021bf55f184b4bad4c78edf73a7f5d52a7a80221
- CSharpCLI/README.md: local origin/main at 22db07e7921442d16a6fe62cfa22b0fc5bf467c8
- Python/README.md: local origin/main at b527733d0c3fb213a515cd5a3e7b4a0d66b461c0
- PythonCLI/README.md: local origin/main at 22139ec606140f900068b8f5fae469aab00f86e7
- ROS/README.md: local origin/main at aeae9b85fd42f37d3601e498c90047f68f40a89c
- Unity/README.md: local origin/main at 74c0bb7e47dd2abf3859bec181c495abdda6aaa0
- UnityGUI/README.md: local origin/main at 5309762b1d253f61e250a36f336135f4b1cee9af
- MATLAB/README.md: local origin/main at a61f0a3d093edffb461c1c48c3138f6efbff7274
- MATLABCLI/README.md: local origin/main at 9ad6b987247c1f0e42b877e1596e227f0b624f15

There are 72 semantic contrast/boundary paragraph groups and eight neutral paragraph groups below; a group can contain multiple related clauses, each covered by its disposition. Python local origin/main has no rhetorical contrast findings; its package/submodule alternatives and ownership statements are neutral. Cross-references identify equivalent draft restrictions rather than treating them as newly discovered defects.

### M1. Core/README.md:20-22 at local origin/main 80c74f0101387d74cd637ba2a6e0527b4c541d2e

> The Windows wrapper calls the separate production C++/WinRT **`WssBleWindows.dll`**. Windows BLE requires a Windows x64 process. Linux BLE requires BlueZ and system D-Bus. There are no macOS/Android BLE backends or Python bindings.

Reader question: Which backend/platforms exist?

Verdict: **REWRITE**. Keep platform limits, but reconcile stale no-macOS/Python claim with the same README's macOS section and intended 1.0 support. Draft replaces this status wording.

### M2. Core/README.md:26-27 at local origin/main 80c74f0101387d74cd637ba2a6e0527b4c541d2e

> Install .NET 8 (test runtime) and .NET 9 (ArtifactBuild), with NuGet access. Managed projects can be built without a native compiler:

Reader question: Is a native compiler required for managed builds?

Verdict: **KEEP**. Real build prerequisite boundary.

### M3. Core/README.md:56-58 at local origin/main 80c74f0101387d74cd637ba2a6e0527b4c541d2e

> This produces `artifacts/universal/`, `artifacts/WSS-Universal-local.zip`, and `artifacts/SHA256SUMS.txt`. The payload destination must be new. Use `--destination <new-payload-directory> --tag <TAG>` to select a different output.

Reader question: May artifact output overwrite an existing directory?

Verdict: **KEEP**. New-destination requirement is an actual build constraint; different-output wording is incidental.

### M4. Core/README.md:67-72 at local origin/main 80c74f0101387d74cd637ba2a6e0527b4c541d2e

> The ZIP contains one `WSS-Universal/` directory: one shared Core/Serial/BLE set, both backend dependency trees, production Windows native code, and the Ports runtime assets. Verify the checksum before extraction and preserve the complete payload for redistribution. Do not mix independently sourced DLLs or download separate platform packages. The [artifact guide](tools/ArtifactBuild/README.md) lists the exact inventory, deployment rules, and validation commands.

Reader question: May I mix individual DLLs/platform packages?

Verdict: **KEEP**. Dependency-integrity limit; same rule as preservation entry 1.

### M5. Core/README.md:76-80 at local origin/main 80c74f0101387d74cd637ba2a6e0527b4c541d2e

> Reference `WSS_Core_Interface.dll`, `WSS.Transport.Serial.dll`, and `WSS.Transport.BLE.dll` as needed from the same extracted artifact. Serial types are defined directly in the Serial assembly; the BLE assembly does not forward them. Rebuild internal libraries, applications, and Unity assembly references against the final assembly set. Constructors/options and shared transport contracts are preserved.

Reader question: Are Serial types forwarded by BLE?

Verdict: **KEEP**. Real assembly/API compatibility boundary.

### M6. Core/README.md:82-85 at local origin/main 80c74f0101387d74cd637ba2a6e0527b4c541d2e

> The facade automatically loads the current platform backend from `AppContext.BaseDirectory/backends/windows` or `backends/linux`. Deploy the full backend/runtime trees without duplicate shared WSS DLLs in backend directories. Hosts that preload backend assemblies can register explicitly:

Reader question: May backend directories duplicate shared DLLs?

Verdict: **KEEP**. Real runtime assembly-resolution constraint.

### M7. Core/README.md:93-98 at local origin/main 80c74f0101387d74cd637ba2a6e0527b4c541d2e

> For .NET applications use `System.IO.Ports` NuGet **8.0.0** to select the host runtime implementation; do not overwrite it with the portable root net462 asset. Keep the original complete artifact for redistribution. Unity plugin deployment and native resolution are host-specific; configure the production native DLL for Windows x86_64, preload/register if needed, and validate Editor/player loading. See [consumer integration requirements](tools/ArtifactBuild/CSHARP-HANDOFF.md).

Reader question: May the portable Ports asset overwrite the host runtime implementation?

Verdict: **KEEP**. Concrete package/runtime-selection restriction.

### M8. Core/README.md:111-122 at local origin/main 80c74f0101387d74cd637ba2a6e0527b4c541d2e

> `.github/workflows/artifacts.yml` runs for PRs targeting `dev`, `main`, or `net2_BLE`, manual dispatch, and pushed `v*` tags. `Core conformance` runs the functional suite once on Linux. `Build and validate artifacts` builds its own inputs on `windows-2022` and validates one universal ZIP without functional suites. `Core consumer` and `Serial consumer` jobs run on Windows/Linux/macOS; `BLE consumer` jobs combine source functional suites and packaged consumers on Linux and Windows (`windows-2022` for isolated shim/production-native testing). All consumers use the same verified, extracted ZIP. Serial smoke checks do not exercise port I/O; classified Linux controller skips do not suppress dependencies. See [CI Jobs and Test Responsibilities](tests/README.md#ci-jobs-and-test-responsibilities) for the authoritative catalog and placement policy. Named logs, job summaries and downloadable NUnit TRX artifacts expose actual execution and failures.

Reader question: What do CI compatibility and Serial smoke prove?

Verdict: **KEEP**. Preserve load-only and dependency-check boundaries; same evidence distinction as preservation entry 5.

### M9. Core/README.md:124-129 at local origin/main 80c74f0101387d74cd637ba2a6e0527b4c541d2e

> Only successful `v*` tag pushes publish after all component, packaging, and consumer jobs pass. The release job downloads the already-tested ZIP/checksum and verifies them without rebuilding or repackaging. Hyphenated tags are prereleases. PRs, ordinary branch pushes, and manual validation do not publish. Hardware diagnostics are opt-in and are not release-gate substitutes for automated tests. # macOS BLE

Reader question: Which events publish, and does release rebuild?

Verdict: **KEEP**. Publication triggers and immutable tested-payload policy are substantive maintainer contracts.

### M11. CSharp/README.md:7-7 at local origin/main 021bf55f184b4bad4c78edf73a7f5d52a7a80221

> This repository now owns the integration/library layer only. The CLI application should live in a separate repo and consume this library as a git submodule.

Reader question: What belongs to library versus CLI?

Verdict: **REWRITE**. State present ownership directly: This repository supplies the integration; the CLI consumes it as a submodule. Remove now/should-live transition framing.

### M13. CSharp/README.md:36-40 at local origin/main 021bf55f184b4bad4c78edf73a7f5d52a7a80221

> Both artifact properties must identify the same root. The project references Core, Serial, and the Standard 2.0 BLE facade directly. System.IO.Ports NuGet 8.0.0 selects the runnable host implementation; the root net462 alternate is not copied over it. Both backend trees, the Windows native wrapper/production DLL, shared dependencies, and all runtime trees propagate to build and publish outputs. Preflight verifies their hashes against the candidate.

Reader question: Does the net462 asset replace runtime-selected Ports?

Verdict: **KEEP**. Concrete runtime asset rule, equivalent to Core current-main entry 7.

### M14. CSharp/README.md:46-51 at local origin/main 021bf55f184b4bad4c78edf73a7f5d52a7a80221

> - `wss_producer_repository`: `cwru-non-academic/WSSCore` - `wss_run_id`: exact successful `artifacts.yml` run (manual dispatch or tag push, not a PR merge-ref run) - `wss_commit`: full expected producer SHA - `wss_archive`: `WSS-Universal-ci.zip` for an untagged run - `wss_sha256`: expected archive SHA-256, obtained from that run's `SHA256SUMS.txt` - `correlation_id`: optional single-line identifier

Reader question: Which producer run kinds are accepted?

Verdict: **KEEP**. PR merge-ref exclusion is a provenance trust boundary.

### M15. CSharp/README.md:53-60 at local origin/main 021bf55f184b4bad4c78edf73a7f5d52a7a80221

> Alternatively set `wss_release_tag` instead of `wss_run_id`, with the matching universal archive name, expected tag commit, and SHA-256. Tag selection resolves exactly one successful authoritative tag-push run and downloads its retained Actions artifact, not manually attached release assets; expired/missing artifacts fail closed. There is no release/default fallback and no support for the old two-archive format. The resolver verifies run/workflow/repository, successful completion, exact commit, artifact availability, checksum and exact ZIP inventory before extraction. All jobs consume one verified tree; `wss-candidate-provenance` retains the producer run/artifact ID, commit and archive hash.

Reader question: Can tag selection fall back to release attachments/defaults/two archives?

Verdict: **KEEP**. Preserve exact accepted source and fail-closed rules. The introductory tag/run alternative is ordinary syntax within a substantive restriction paragraph.

### M16. CSharp/README.md:62-63 at local origin/main 021bf55f184b4bad4c78edf73a7f5d52a7a80221

> Use exactly the same candidate inputs for CLI preflight, with `integration_sha` selecting the updated Integration source. No submodule promotion is required for this test.

Reader question: Does preflight require dependency promotion?

Verdict: **KEEP**. Real distinction between read-only candidate testing and dependency publication.

### M17. CSharp/README.md:65-70 at local origin/main 021bf55f184b4bad4c78edf73a7f5d52a7a80221

> **Transition coordination:** committed `lib/` still contains the old distribution. Normal Integration CI cannot pass with that payload and the new references/tests; preflight must use the explicit universal candidate. Do not overlay old DLLs or add compatibility shims. After preflights pass, a separate promotion phase must update committed artifacts and the promotion scripts. The shared resolver now requires candidate provenance, so existing promotion calls supplying only a release tag are intentionally not runnable unchanged.

Reader question: Why is committed-state CI temporarily incompatible?

Verdict: **REWRITE**. Temporary migration restrictions belong in a dated maintainer/readiness record; release README should state complete compatible-payload prerequisites.

### M18. CSharp/README.md:74-80 at local origin/main 021bf55f184b4bad4c78edf73a7f5d52a7a80221

> Routine `promote-wss.yml` retains **lib-only** bot PRs to `dev`. Dispatch it on `dev` with `approval_json` and `correlation_id`. Schema 2 is illustrated by `.github/universal-bootstrap.json`: producer repository/commit/run/artifact/archive/hash, plus both downstream source SHAs and successful preflight run IDs. Future promotions supply fresh matching values, never a default. Both preflight provenance records and the CLI checkout-verification log must match. Source comparison allows only explicitly listed promotion/CI automation and documentation changes from the tested source; application, reference and preflight changes require new evidence.

Reader question: What may routine promotion change and which evidence is fresh?

Verdict: **KEEP**. lib-only/allowlisted scope and no-default proof are genuine promotion limits.

### M19. CSharp/README.md:82-85 at local origin/main 021bf55f184b4bad4c78edf73a7f5d52a7a80221

> `New-WssLib.ps1` deterministically stages all 25 universal files plus `lib/wss-candidate.json`. No DLL overlay or Ports runtime exclusion is used. Existing PR/branch reuse requires identical bytes, source, approval and correlation. Native branch probing still distinguishes exit 2 (absence) from API/authentication failures. No force push or auto-merge is used.

Reader question: Can staging overlay DLLs, force-push or auto-merge?

Verdict: **KEEP**. Actual tool side-effect and data-integrity boundaries.

### M20. CSharp/README.md:89-96 at local origin/main 021bf55f184b4bad4c78edf73a7f5d52a7a80221

> Do not merge source-only changes first or relax normal CI. Use `Prepare-UniversalBootstrap.ps1` in a clean **separate worktree** descended from the validated source plus reviewed automation. The script is pinned to the first verified candidate, Integration `cd181c1f2abca8048fc2d1181dbf9beca9050e21`/run `38420209110`, and CLI `a3b272b23fba4e566d7c1fe3bfafa048cca9806c`/run `38420541537`. It checks current `dev` ancestry, exact source compatibility and an explicit bootstrap path allowlist. Without `-Stage` it only verifies/stages temporary files; with `-Stage` it creates `bootstrap/universal-wss-378a489` and stages dependencies/gitlink. It never commits, pushes, opens PRs or merges.

Reader question: What does bootstrap stage and what actions remain manual?

Verdict: **KEEP**. Preserve Stage switch behavior and publication limits in maintainer reference; source-only/normal-CI restriction is substantive.

### M21. CSharp/README.md:98-102 at local origin/main 021bf55f184b4bad4c78edf73a7f5d52a7a80221

> The two `.github/promotion-evidence/*.zip.b64` files preserve original provenance ZIP bytes before one-day retention expires. Digests are pinned to official Actions metadata (Integration artifact `11826726567`, CLI artifact `11825383860`). Only this fixed bootstrap can use them; run success/source identity, the CLI checkout log and producer artifact remain live requirements. Routine promotion has no expired-proof fallback.

Reader question: When may archived proofs be used?

Verdict: **KEEP**. Fixed-bootstrap exception versus routine live-evidence rule is substantive.

### M22. CSharp/README.md:104-111 at local origin/main 021bf55f184b4bad4c78edf73a7f5d52a7a80221

> Each retained proof is 480 Base64 characters representing a 358-byte ZIP whose only entry is a 299-byte `candidate.json`. Inspection confirmed exactly the seven public candidate provenance fields: no secrets, credentials, logs, WSS binaries or unrelated data. Retain both files through the Integration and CLI bootstrap reviews/merges. They can be removed in a separately reviewed cleanup after both coordinated migrations finish and the pinned bootstrap path is retired. That cleanup must also retire/update its proof-specific safety tests; deleting the archives alone would break normal Integration CI. Routine promotion restrictions and live approval checks must remain intact.

Reader question: What is stored in proof files, and when may they be removed?

Verdict: **KEEP**. Content and cleanup dependency restrictions are factual scope boundaries.

### M24. CSharp/README.md:120-123 at local origin/main 021bf55f184b4bad4c78edf73a7f5d52a7a80221

> Inspect staged dependencies and the whole branch diff to `dev`. After separate authorization, commit the lib delta, push the bootstrap branch and open an Integration `dev` PR using generated `bootstrap-pr-body.md`. This PR includes validated source, reviewed automation and exact binaries. Require all normal committed-state Integration CI jobs and human review before manual merge.

Reader question: When may the operator publish/merge?

Verdict: **KEEP**. Authorization/CI/human-review gates are actual workflow constraints.

### M25. CSharp/README.md:125-126 at local origin/main 021bf55f184b4bad4c78edf73a7f5d52a7a80221

> Bootstrap deliberately stays outside the routine bot/lib-only merge trigger. After Integration merges, create a separate CLI worktree from its reviewed source/automation commit and run:

Reader question: Does bootstrap trigger normal automatic promotion?

Verdict: **KEEP**. Actual trigger exclusion prevents unintended cascade.

### M26. CSharp/README.md:132-136 at local origin/main 021bf55f184b4bad4c78edf73a7f5d52a7a80221

> It verifies the actual Integration merge SHA in `dev`, latest successful normal CI at the PR's final head, approved source, committed provenance and universal bytes before staging the gitlink. The preflight-only commit cannot be the final target. Publish/review the coordinated CLI PR only after authorization; require normal CLI CI before manual merge. Incompatible `dev` advancement or unavailable producer/log evidence stops preparation rather than overwriting/substituting.

Reader question: May preflight-only SHAs or incompatible advancement be published?

Verdict: **KEEP**. Actual publication/provenance constraint.

### M27. CSharp/README.md:147-154 at local origin/main 021bf55f184b4bad4c78edf73a7f5d52a7a80221

> The same workflow resolves current Integration and CLI `dev` source SHAs, automatically dispatches their existing preflights, and pins CLI's selected Integration source to the Integration preflight SHA. A branch move during dispatch is rejected, not silently accepted. Both preflights must finish successfully with matching candidate provenance before the existing schema-2 approval validator permits promotion. The deterministic correlation ID is `wss-<producer-run>-<artifact-id>`. Replays reuse uniquely correlated runs and the exact approval rather than dispatching new preflights; failed, missing, ambiguous, expired or mismatched evidence stops promotion. GitHub/API errors are never interpreted as absence.

Reader question: What happens on replay, branch moves or API errors?

Verdict: **KEEP**. Retain fail-closed identity/reuse semantics; not ornamental reassurance.

### M28. CSharp/README.md:156-160 at local origin/main 021bf55f184b4bad4c78edf73a7f5d52a7a80221

> Integration uses `automation/wss-<run>-<hash>` with lib-only diffs. Manual merge triggers `dispatch-cli-promotion.yml`, using trusted base code and checking bot origin, lib-only scope, approval, merge SHA and normal CI. It dispatches CLI on `dev` with `integration_sha`, `integration_pr`, `approval_json` and `correlation_id`. CLI validates with trusted Integration `dev` tools before creating write credentials, retains gitlink-only PRs, and uses normal CI.

Reader question: Which files do Integration/CLI promotion PRs change?

Verdict: **KEEP**. lib-only/gitlink-only change-scope limits.

### M29. CSharp/README.md:162-166 at local origin/main 021bf55f184b4bad4c78edf73a7f5d52a7a80221

> An identical complete dependency payload is a verified no-op: no metadata-only PR is created, existing committed provenance is retained, and the freshly verified schema-2 approval is retained in the promotion run's `wss-automatic-approval` artifact. An open matching PR is reused; an already merged/closed candidate cannot create a replacement or rollback PR. Existing branches and unrelated changes are not overwritten or force-pushed.

Reader question: What happens to duplicate/closed/stale candidates?

Verdict: **KEEP**. No-op, replacement, rollback and overwrite boundaries are substantive.

### M30. CSharp/README.md:168-176 at local origin/main 021bf55f184b4bad4c78edf73a7f5d52a7a80221

> Deployment prerequisite: publish/review this Integration automation before enabling the Core handoff. Existing `WSS_GITHUB_APP_ID` / `WSS_GITHUB_APP_PRIVATE_KEY` must be accessible in WSSCore as well as downstream repositories. The Core handoff needs Integration Actions write only. Integration needs producer/evidence Actions and Contents read, downstream Contents read/Actions write for dispatch, and its existing Contents/PR write for authoring. Dispatch/write tokens are created only after their corresponding trust checks. No workflow merges PRs, writes `dev` directly, bypasses protections, invokes bootstrap, tags or publishes a release. Once deployed and configured, reviewed Integration and CLI PR merges remain the only routine human actions (plus the initiating WSSCore PR merge).

Reader question: Which credentials and autonomous writes are permitted?

Verdict: **KEEP**. Least-privilege scope and human merge/release boundaries matter to maintainers.

### M31. CSharp/README.md:178-183 at local origin/main 021bf55f184b4bad4c78edf73a7f5d52a7a80221

> The orchestration has a 60-minute bound; timeouts and missing permissions fail explicitly. Existing Actions artifact/log retention still applies, including CLI preflight provenance's one-day retention: complete the reviewed cascade while its evidence remains available. Expired evidence must be investigated/revalidated; no archived bootstrap proof or stale committed dependencies are substituted. Hosted Windows/Linux/macOS checks retain their existing scope and do not establish physical Linux BLE or stimulation operation.

Reader question: Can expired proof be substituted, and do hosted checks validate hardware?

Verdict: **KEEP**. Evidence freshness and physical-validation limits; latter matches preservation entry 5.

### M32. CSharp/README.md:200-201 at local origin/main 021bf55f184b4bad4c78edf73a7f5d52a7a80221

> The enum makes transport choices mutually exclusive. Applications using conformance mode can obtain the WSS Core-owned conformance capability without accessing or replacing the transport:

Reader question: Are transports exclusive, and how is conformance exposed?

Verdict: **KEEP**. Actual enum and capability-access boundary; matches preservation entry 12/22 transport-choice reasoning.

### M33. CSharp/README.md:218-219 at local origin/main 021bf55f184b4bad4c78edf73a7f5d52a7a80221

> The C# integration library only exposes this capability. Validation rules and observations remain implemented by WSS Core's `IWssConformance`.

Reader question: Who implements validation rules?

Verdict: **REWRITE**. Positive ownership answers the question: The integration exposes IWssConformance; Core implements its validation rules and observations.

### M34. CSharp/README.md:223-227 at local origin/main 021bf55f184b4bad4c78edf73a7f5d52a7a80221

> - Serial transport remains available through the vendor serial stack. - Test transport remains available for simulated runs. - BLE transport is exposed through `BleNusTransport` and is intended for Nordic UART Service-compatible devices. - The universal artifact provides platform backends for Windows and Linux; macOS supports Core/Serial only. - The current WSS BLE path assumes an unpaired, unencrypted NUS connection and does not request BLE pairing.

Reader question: What platforms and pairing behavior are supported?

Verdict: **REWRITE**. Keep actual pairing/transport limits; update stale macOS Core/Serial-only claim for the intended 1.0 distribution, without inventing device qualification.

### M36. CSharpCLI/README.md:61-61 at local origin/main 22db07e7921442d16a6fe62cfa22b0fc5bf467c8

> Run the deterministic, hardware-independent consumer conformance check:

Reader question: Can conformance run without hardware?

Verdict: **KEEP**. Simulator test-scope boundary; preservation entry 14.

### M37. CSharpCLI/README.md:67-67 at local origin/main 22db07e7921442d16a6fe62cfa22b0fc5bf467c8

> Verify that the Serial transport can be loaded and constructed without opening a serial device or requiring hardware:

Reader question: Does Serial smoke open a port?

Verdict: **KEEP**. Load-only side-effect boundary; preservation entry 16.

### M38. CSharpCLI/README.md:81-92 at local origin/main 22db07e7921442d16a6fe62cfa22b0fc5bf467c8

> - `--transport=serial|ble|test|conformance`: select exactly one transport; defaults to Serial - `--serial=NAME`: explicit serial device used by the Serial transport - `--ble-auto`: scan for and auto-select a compatible BLE device - `--ble-device-name=NAME`: select an exact BLE device name - `--ble-device-id=ID`: select an explicit BLE device identifier - `--config=PATH`: override the config directory - `--max-retries=N`: max setup retries before startup fails - `--tick=MS`: controller tick interval in milliseconds - `--test`: alias for `--transport=test` - `--conformance`: alias for `--transport=conformance`; initialize the WSS emulator and validate initialization plus one direct analog scenario - `--serial-smoke`: construct and dispose the Serial transport without connecting to hardware; used for release compatibility testing - `--help`: print usage information

Reader question: Which CLI choices connect and which only construct?

Verdict: **KEEP**. Mutually exclusive transport choice and smoke no-connect scope are real limits; preservation entries 15–16.

### M39. CSharpCLI/README.md:96-96 at local origin/main 22db07e7921442d16a6fe62cfa22b0fc5bf467c8

> By default, the CLI resolves the repository root explicitly and uses the app repo's `Config/` directory from there. Conformance mode instead creates and removes a temporary deterministic configuration from the shared WSS stimulation fixture.

Reader question: Which configuration does Conformance use?

Verdict: **KEEP**. Real mode-specific file location/lifetime distinction.

### M41. CSharpCLI/README.md:128-129 at local origin/main 22db07e7921442d16a6fe62cfa22b0fc5bf467c8

> - `CLI general conformance` runs hardware-independent Test transport, CLI conformance, and shared Integration conformance scenarios once. - `CLI platform compatibility (Windows)`, `CLI platform compatibility (Ubuntu)`, and `CLI platform compatibility (macOS)` build and run platform loading and Serial construction smoke checks on each operating system.

Reader question: How are functional and platform checks separated?

Verdict: **KEEP**. Real validation-responsibility/evidence boundary.

### M42. CSharpCLI/README.md:131-141 at local origin/main 22db07e7921442d16a6fe62cfa22b0fc5bf467c8

> The manually triggered `.github/workflows/wss-preflight.yml` validates the CLI against an exact universal WSS candidate. Supply `wss_producer_repository=cwru-non-academic/WSSCore`, `wss_run_id`, `wss_commit` (full SHA), `wss_archive=WSS-Universal-ci.zip`, and `wss_sha256`. Use the **same tuple** for Integration preflight. The selected Integration source owns the shared resolver: it rejects failed/wrong-commit/wrong-workflow runs and PR merge-ref artifacts, verifies the archive against both the pinned hash and `SHA256SUMS.txt`, and validates the exact nested universal inventory before extraction. A published universal release can be selected with `wss_release_tag` instead of `wss_run_id`, with its matching archive/hash/commit. It must resolve to one successful tag-push producer run with retained Actions assets; the archive is downloaded from that run, not from manually attached release assets. There is no default release or old two-archive fallback.

Reader question: Which candidate artifacts can preflight accept?

Verdict: **KEEP**. No-default/no-two-archive/release-attachment restrictions are provenance requirements.

### M43. CSharpCLI/README.md:143-149 at local origin/main 22db07e7921442d16a6fe62cfa22b0fc5bf467c8

> Set `integration_sha` to the full SHA of updated Integration source for preflight only. If omitted, the committed gitlink is used exactly as before; obsolete pinned source fails explicitly rather than silently selecting a newer branch. Test jobs disable committed `lib/`, use the one verified universal root, and check complete build/publish propagation, including both backends, the production Windows native library, and all runtime assets. The root net462 Ports alternate is deliberately not copied over NuGet's Ports 8 host implementation. The workflow retains `wss-candidate-provenance` and reports the tested Integration/CLI SHAs.

Reader question: What happens to stale source and runtime-selected Ports?

Verdict: **KEEP**. Exact-source failure behavior and runtime asset preservation are substantive.

### M44. CSharpCLI/README.md:151-153 at local origin/main 22db07e7921442d16a6fe62cfa22b0fc5bf467c8

> Normal CLI CI still tests the committed submodule and its committed binaries. The conditional direct Serial reference leaves that old committed state untouched while adding a direct reference for universal inputs. No gitlink update is part of preflight development.

Reader question: Does preflight change the committed dependency?

Verdict: **KEEP**. Read-only candidate-testing versus committed-state validation boundary.

### M45. CSharpCLI/README.md:155-155 at local origin/main 22db07e7921442d16a6fe62cfa22b0fc5bf467c8

> The platform checks cover Windows, Ubuntu, and macOS build/runtime and Serial compatibility. They verify packaged backend and runtime trees where applicable, but the macOS check does not establish BLE runtime support. These non-hardware checks do not prove communication with physical hardware.

Reader question: What does macOS compatibility CI prove?

Verdict: **KEEP**. Keep evidence limitation; refresh for eventual release evidence rather than infer physical BLE from load checks.

### M46. CSharpCLI/README.md:170-177 at local origin/main 22db07e7921442d16a6fe62cfa22b0fc5bf467c8

> Routine `promote-integration.yml` remains gitlink-only. Dispatch on `dev` with `integration_sha`, `integration_pr`, schema-2 `approval_json`, and `correlation_id`. Validation uses trusted Integration **dev** tooling, not scripts from a caller-nominated commit. It requires matching successful preflights, the actual Integration PR merge SHA contained in `dev`, latest successful normal Integration CI, matching committed provenance, exact universal bytes and source compatibility. Write credentials are created only after these checks. Existing PR reuse remains exact-gitlink/metadata-only; branch lookup errors are not absence. Normal CLI CI verifies complete build/publish propagation from the committed Integration `lib/`.

Reader question: Which tooling may author a routine promotion PR?

Verdict: **KEEP**. Trusted-tooling, exact-gitlink and credential-creation boundaries are substantive.

### M47. CSharpCLI/README.md:179-189 at local origin/main 22db07e7921442d16a6fe62cfa22b0fc5bf467c8

> For routine automatic updates, no operator dispatch or provenance entry is needed: WSSCore's trusted `dev` push runs the universal producer and all consumers, then automatically hands its exact run/artifact/commit/checksum to Integration's existing promotion workflow. That workflow runs both existing downstream preflights against the same candidate, pins CLI's Integration source, validates schema-2 approval, and opens a lib-only Integration PR. Its reviewed merge invokes the existing dispatcher above with the **actual merged** Integration revision, producing the gitlink-only CLI PR. Reviewed merges and normal CI remain mandatory; no bootstrap, auto-merge, release or manual DLL copying is involved. Missing/expired/mismatched evidence stops the cascade. The automatic implementation still requires an authorized real run to establish end-to-end acceptance; hosted CI does not prove physical BLE or stimulation operation.

Reader question: Which steps are automated, and what remains human-reviewed?

Verdict: **KEEP**. Autonomous side-effect limits and physical-validation caveat are substantive; temporary acceptance status belongs in readiness record.

### M48. CSharpCLI/README.md:191-196 at local origin/main 22db07e7921442d16a6fe62cfa22b0fc5bf467c8

> The first migration cannot use ordinary gitlink-only promotion because CLI source changes must accompany adoption. Use the explicitly pinned `Prepare-UniversalBootstrap.ps1` in the Integration automation worktree, with `-Repository CLI`, a clean separate CLI worktree containing the validated source plus reviewed automation, a new scratch directory, the **merged Integration PR number**, and `-Stage`. It never commits, pushes or creates PRs. Full instructions and the fixed approval/proof records are in Integration's README and `.github/universal-bootstrap.json`.

Reader question: Why does first migration require a different workflow?

Verdict: **REWRITE**. Retain bootstrap staging/publication limits in a dated maintainer migration record; release README should link routine dependency maintenance.

### M49. CSharpCLI/README.md:198-202 at local origin/main 22db07e7921442d16a6fe62cfa22b0fc5bf467c8

> Do not target Integration's preflight-only SHA. After the coordinated Integration PR passes normal CI and is manually merged, prepare the CLI source-plus-final-gitlink PR, obtain publication authorization, and require normal CLI CI and review before manual merge. This one-time manual initiation leaves the permanent Integration bot/lib-only dispatch trigger unchanged. Subsequent Integration merges return to that automatic dispatch and ordinary gitlink-only CLI promotion.

Reader question: Which Integration revision may CLI finally pin?

Verdict: **KEEP**. Merged-commit and reviewed-CI requirements are substantive.

### M50. PythonCLI/README.md:32-32 at local origin/main 22139ec606140f900068b8f5fae469aab00f86e7

> The CLI can also import the wrapper directly from the checked-out submodule during editable app development, but installing the wrapper repo explicitly is the cleaner setup.

Reader question: Should I import source directly or install the wrapper?

Verdict: **REWRITE**. Unnecessary cleaner-setup comparison: Install the wrapper explicitly from the checked-out submodule for editable app development.

### M51. PythonCLI/README.md:39-39 at local origin/main 22139ec606140f900068b8f5fae469aab00f86e7

> The CLI now constructs `WssConfig` explicitly from app-repo paths instead of relying on the old combined-repo current-working-directory layout.

Reader question: Where do app config paths resolve?

Verdict: **REWRITE**. Replace historical old-layout contrast with present rule: The CLI constructs WssConfig from application-checkout paths. Matches preservation entry 27.

### M52. ROS/README.md:72-72 at local origin/main aeae9b85fd42f37d3601e498c90047f68f40a89c

> No unit tests are currently included in this repository.

Reader question: Are unit tests supplied?

Verdict: **KEEP**. Genuine test-availability limit; reassess against current repository before retaining in 1.0 prose.

### M53. ROS/README.md:76-82 at local origin/main aeae9b85fd42f37d3601e498c90047f68f40a89c

> - Repo-owned WSS JSON configuration lives under `Config/`. - The wrapper submodule remains at `HFI_WSS_Python_Interface/` and provides the Python package and `Cs_Libraries/` DLLs. - `test_mode: true` switches the wrapper to its test transport instead of serial transport. - `max_retries` defaults to `5` and `tick_interval_ms` defaults to `10`. - The number of non-empty channel topics updates `Config/stimConfig.json` `maxWSS` automatically: 1-3 topics -> `1`, 4-6 topics -> `2`, 7-9 topics -> `3`. - Leave a channel topic empty in the YAML to disable that channel subscription. - The default example maps `/dom/index/stimulation_value` to `ch2`.

Reader question: How do Test mode and disabled topics work?

Verdict: **KEEP**. Serial-versus-simulator and empty-topic behavior are real configuration boundaries.

### M54. Unity/README.md:9-20 at local origin/main 74c0bb7e47dd2abf3859bec181c495abdda6aaa0

> ## How to add a submodule to exiting project 1. Close other software using the code solution (Unity, Visual Studio, etc) 2. If using git desktop, open the command prompt by going to `Repository>Open in Command Prompt` 3. Use command `git submodule add <submoduleURL> <pathInProject>`  	- if using a submodule inside of Unity make sure the path is inside the Assests folder 	- Ex HTTPS: `git submodule add https://github.com/cwru-non-academic/WSSInterfacingCode Assets\SubModules\WSSInterfacingModule`  	- Ex SSH: `git submodule add git@github.com:cwru-non-academic/HFI_WSS_Interface.git Assets\SubModules\WSSInterfacingModule` 4. Click `Current repository>Add>Add existing repository..` and locate the folder where you installed the submodule `<pathInProject>`  5. Remove old scripts from the project that are now part of the submodule 6. Open solution dependent software and let it refactor. 7. (Unity Only) Make sure all you scripts in the scene are still available and linked correctly. 8. (Unity only) Add `"com.unity.nuget.newtonsoft-json": "3.2.1",` to your package manager manifest under dependencies found in `<project>/Packages/manifest.json`

Reader question: Which setup actions are Unity-specific?

Verdict: **KEEP**. Unity-only host prerequisites are actionable limits; ordinary HTTPS/SSH example alternatives in the same list are not findings.

### M55. Unity/README.md:22-29 at local origin/main 74c0bb7e47dd2abf3859bec181c495abdda6aaa0

> ## How to commit a git project that has submodules (Git Desktop) 1. This only applies if the changes were made to the submodule, otherwise just commit as normal. 2. Inside of the git Desktop a warning will appear that says there are submodule chnages. 3. Changes to submodules must be commited first in the submodules repo. 4. If the submodule repo is already setup, just click open repository shortcut under the submodule changes warning. 5. If it is not setup, click the shortcut to add it to Git Desktop. 6. Once in the submodule repo, commit the changes as normal. 7. Return to the main repo and commit the changes there (The chnage in commit ID will be not be selected by default. select it and then commit).

Reader question: How are submodule changes committed?

Verdict: **REWRITE**. Preserve submodule-before-parent requirement and unselected-pointer behavior; remove just/otherwise filler and use direct conditional instructions.

### M57. UnityGUI/README.md:9-10 at local origin/main 5309762b1d253f61e250a36f336135f4b1cee9af

> ## 5. Modify your GLOBAL .gitconfig Not everyone may have the same path to UnityYAMLMerge (i.e. different operating systems or install locations). Because of this, we suggest you modify your local config to define the "unityyamlmerge" merge tool that this repository points to. To do this:

Reader question: Why configure the merge tool path locally?

Verdict: **REWRITE**. State positive rule: Configure the UnityYAMLMerge path for your installed Editor and OS. Also resolve GLOBAL heading versus local-config mismatch.

### M58. UnityGUI/README.md:17-17 at local origin/main 5309762b1d253f61e250a36f336135f4b1cee9af

> ii. Open the `.git/config` file by navigating to your local repo folder, open the hidden `.git` folder, then open the config file (note: this is different from the `.gitconfig` file in the repoΓÇÖs root folder)

Reader question: Which Git config file should I edit?

Verdict: **KEEP**. Real file-identity distinction prevents modifying the wrong configuration.

### M59. UnityGUI/README.md:51-59 at local origin/main 5309762b1d253f61e250a36f336135f4b1cee9af

> ## Starting a new project or adding version control to an old one? If you are starting a new project you can start working now, but if you are adding version control to an old one follow these steps: 1. Paste your project files into this repository on your local machine.  2. Open the project on the target Unity version, so all the files are modified as necessary.  3. If you had Unity Version Control (formerly Collab or Plastic SCM), disable it now. 4. Save your project and close Unity. 5. Delete left over files from Unity Version Control like x.meta.private or y.meta.private.meta (some files may be hidden so enable see hidden files) 6. Commit and push. 7. If you are having issues with .meta files check and modify your gitignore or make sure there are no empty folders.

Reader question: What extra steps apply to adding version control to an old project?

Verdict: **KEEP**. Retain meaningful new/existing workflow distinction and actual empty-folder caveat; generic template setup can be relocated for startup-focused 1.0 README.

### M60. MATLAB/README.md:9-14 at local origin/main a61f0a3d093edffb461c1c48c3138f6efbff7274

> - `src/+wss/`: `WssConfig`, `StimulationController`, assembly loading. - `managed/`: a small .NET tick/lifecycle host; no MATLAB callbacks. - `Cs_Libraries/`: versioned Core/dependency payload, including separate backend assets. - `Config/`: reusable configuration examples; consumers supply their own runtime paths. - `tests/`: managed lifecycle checks and MATLAB configuration/lifecycle checks. - `conformance/`: Core's canonical catalog exercised through the MATLAB public API.

Reader question: Does managed ticking call MATLAB callbacks?

Verdict: **KEEP**. Real host-threading boundary; separate backend directories are neutral inventory within same paragraph.

### M61. MATLAB/README.md:16-19 at local origin/main a61f0a3d093edffb461c1c48c3138f6efbff7274

> This initial port follows Python wrapper revision `79322a82d285cc526c77ea660beb2993cb04f5d9`. Payload provenance and hashes are in `Cs_Libraries/payload-source.json`. No downloaded or locally built substitute Core is silently selected.

Reader question: Can assembly loading silently select another Core?

Verdict: **REWRITE**. State explicit payload selection positively: Assembly loading uses the configured bundled Cs_Libraries payload; provenance is in payload-source.json.

### M62. MATLAB/README.md:29-32 at local origin/main a61f0a3d093edffb461c1c48c3138f6efbff7274

> Use a MATLAB release with a supported .NET runtime. The initial target is Windows with modern Core namespaces; older namespace layouts and Linux/macOS transport behavior have not been validated. Choose the CLR explicitly in a fresh MATLAB session before any .NET calls. For a release that supports .NET 8 selection:

Reader question: Which MATLAB/runtime platforms are validated?

Verdict: **REWRITE**. Move temporary unvalidated platform status into centralized release readiness; final README must describe the supported host/runtime combinations.

### M63. MATLAB/README.md:44-49 at local origin/main a61f0a3d093edffb461c1c48c3138f6efbff7274

> `dotnetenv` is a caller decision and can persist between MATLAB sessions; this library does not change it. Consult MathWorks' [runtime documentation](https://www.mathworks.com/help/matlab/ref/dotnetenv.html) and [assembly loading guide](https://www.mathworks.com/help/matlab/matlab_external/building-a-net-application-for-matlab-examples.html) for the installed release. The managed host targets `netstandard2.0`; the complete payload and selected runtime still require execution checks together.

Reader question: Does the library select the CLR?

Verdict: **REWRITE**. State caller ownership positively; same preservation entry 53. Move pending checks to readiness.

### M64. MATLAB/README.md:51-55 at local origin/main a61f0a3d093edffb461c1c48c3138f6efbff7274

> `TestMode=true` selects Core's simulated transport. `ConformanceMode=true` selects the in-memory emulator and exposes `GetConformance()`. They are mutually exclusive. Without either option, initialization constructs the serial transport, as in Python. Construction alone never opens a transport. All development checks use the emulator or managed probes; no hardware execution has been performed.

Reader question: Which modes conflict, and when does transport I/O start?

Verdict: **KEEP**. Mutual exclusion, live Serial default and construction-versus-initialization boundaries matter. Historical hardware evidence belongs in maintainer record.

### M65. MATLAB/README.md:66-70 at local origin/main a61f0a3d093edffb461c1c48c3138f6efbff7274

> The managed thread calls `Tick` independently of the MATLAB execution queue. Ticks and public facade operations share a lock. The configured interval is a best-effort delay between completed ticks, not a real-time guarantee. A tick failure attempts a stop and is exposed via `LastTickError`; subsequent calls fail. `TickCount` supports verification during blocking input and busy MATLAB.

Reader question: Is tick timing a real-time guarantee?

Verdict: **KEEP**. Best-effort scheduling and subsequent failure behavior are real limits.

### M66. MATLAB/README.md:72-77 at local origin/main a61f0a3d093edffb461c1c48c3138f6efbff7274

> For conformance's transient runtime-edit pause assertion, an optional output from `UpdateEventRatio` observes the real paused state while an emulator-only send gate prevents Core's independent setup worker from finishing the edit. The gate honors cancellation and releases in `finally`; ordinary no-output commands remain ungated. This keeps all 18 canonical assertion sites without depending on MATLAB sampling a short asynchronous state transition.

Reader question: How does the emulator observe a short pause?

Verdict: **KEEP**. Emulator-only gate and ordinary no-output behavior are real test-mechanism limits; better detailed maintainer/conformance placement than onboarding.

### M67. MATLAB/README.md:79-85 at local origin/main a61f0a3d093edffb461c1c48c3138f6efbff7274

> Use `onCleanup` before `Initialize` so normal errors and Ctrl+C unwind through `Shutdown`. Shutdown waits for in-flight managed work, then attempts broadcast stop, shutdown and disposal. It is idempotent. Forced process termination cannot run cleanup. The log path currently receives managed-host lifecycle/error diagnostics; Core's own default sink is unchanged. Config controllers returned by `GetCoreConfigCTRL`/`GetModelConfigCTRL` are raw managed objects; callers must not mutate them concurrently with ticking.

Reader question: Which shutdown cases and concurrent mutations are unsupported?

Verdict: **KEEP**. Forced termination and raw-controller concurrency are real lifecycle limits; same preservation entry 60.

### M68. MATLAB/README.md:89-89 at local origin/main a61f0a3d093edffb461c1c48c3138f6efbff7274

> Managed checks, requiring no MATLAB and no hardware:

Reader question: Can managed checks run without MATLAB/hardware?

Verdict: **KEEP**. Same prerequisite boundary as preservation entry 62.

### M69. MATLAB/README.md:105-110 at local origin/main a61f0a3d093edffb461c1c48c3138f6efbff7274

> Conformance uses the production MATLAB wrapper and Core evaluator, including the initialization, analog, six normalized, runtime edit and stop scenarios. The MATLAB port preserves the 18 assertion sites in the canonical C# fixture at SHA-256 `1cdfb9448a1776011eadc9aef8356362216032013a4f268f50242746c50a0b44`; it is a source port, not the byte-identical NUnit fixture. Equivalence is pending MATLAB execution and review. Failures are collected across scenarios.

Reader question: Is the MATLAB fixture byte-identical to NUnit?

Verdict: **KEEP**. Source-port versus byte-identical-fixture distinction affects evidence. Pending equivalence belongs in readiness, not final onboarding.

### M70. MATLAB/README.md:112-117 at local origin/main a61f0a3d093edffb461c1c48c3138f6efbff7274

> The initial checks ran on MATLAB R2026b (`26.2.0.3386108`) with .NET 8 on Windows: managed host checks passed; all 10 catalog scenarios passed through the wrapper and all 10 through the CLI factory. All nine MATLAB unit/runtime tests passed, including busy-MATLAB tick progression, failed assembly loading, and normal CLI quit/command-failure disposal. MATLAB Code Analyzer reported zero findings at that checkpoint. These are emulator checks, not physical-device qualification.

Reader question: Do emulator passes qualify physical devices?

Verdict: **KEEP**. Real validation evidence boundary; same preservation entry 5.

### M71. MATLAB/README.md:119-124 at local origin/main a61f0a3d093edffb461c1c48c3138f6efbff7274

> The CLI's MATLAB Engine harness passed a quit/interruption/quit sequence with a blocking input double, real MATLAB Engine cancellation, background tick progress and synchronous disposal/reset checks. Native console `input` and keyboard Ctrl+C remain manual checks: the execution tool cannot deliver input to the launched MATLAB console. Run `checkBlockedInput` from the `conformance/` folder on the MATLAB path, and separately interrupt the CLI with Ctrl+C.

Reader question: Can the execution harness deliver native input/Ctrl+C?

Verdict: **KEEP**. Real harness limitation; maintainer validation record is appropriate placement.

### M72. MATLAB/README.md:126-129 at local origin/main a61f0a3d093edffb461c1c48c3138f6efbff7274

> Windows serial runtime and BLE/native ABI load-only compatibility checks also passed through both wrapper and CLI compatibility entry points, separately from behavioral conformance. No port Open, radio scan, ConnectAsync or stimulation operation is used by those checks. Ubuntu/macOS execution remains unverified.

Reader question: What does runtime/ABI load-only success validate?

Verdict: **KEEP**. Real load-only versus behavior/hardware boundary. Temporary OS execution status belongs in centralized checklist.

### M75. MATLABCLI/README.md:21-24 at local origin/main 9ad6b987247c1f0e42b877e1596e227f0b624f15

> The submodule pins published wrapper revision `fdc7584e77d62b4d3a56ff608e7a1ced1afd2dcf`, including the reusable implementation, Core payload, managed host and exact MIT license. The default CLI uses this committed submodule; no sibling checkout or local override is required.

Reader question: Is a sibling wrapper needed for ordinary use?

Verdict: **REWRITE**. Positive present rule suffices: The CLI uses its committed wrapper submodule by default.

### M76. MATLABCLI/README.md:26-27 at local origin/main 9ad6b987247c1f0e42b877e1596e227f0b624f15

> The explicit `--wrapper-root` override remains available for deliberate local development against a separate wrapper checkout. It never changes the gitlink.

Reader question: Does a wrapper-root override modify the gitlink?

Verdict: **KEEP**. Actual runtime override versus committed dependency boundary; preservation entry 69.

### M77. MATLABCLI/README.md:61-65 at local origin/main 9ad6b987247c1f0e42b877e1596e227f0b624f15

> The app restores its temporary MATLAB path additions on return and never calls `savepath`. Assembly/CLR loading lasts for the MATLAB process. Default config and log paths are anchored to the application, DLL paths to the selected wrapper. User path overrides are anchored when parsed. Default transport is serial, as in Python; use `--test` or `--conformance` for development without hardware.

Reader question: Does the app persist path changes; which default opens hardware?

Verdict: **KEEP**. No-savepath and explicit simulator-versus-live-Serial boundaries are substantive.

### M78. MATLABCLI/README.md:67-73 at local origin/main 9ad6b987247c1f0e42b877e1596e227f0b624f15

> Options: `--serial`, `--config`, `--cs-libs`, `--log-file`, `--max-retries`, `--tick`, `--test`, `--conformance`, `--command`, `--wrapper-root`, `--help`. Commands: `help`, `start`, `stop`, `status`, `stim`, `analog`, `reload-core`, `reload-params`, `save-params`, `quit`/`exit`. Quoted reload paths may contain spaces. Initialization is asynchronous: creation of the managed stack does not mean it has reached Ready. Batch command errors propagate to MATLAB's nonzero failure exit; interactive command errors are shown without abandoning cleanup.

Reader question: Does construction mean usable initialization; are errors cleaned up?

Verdict: **KEEP**. Async state and cleanup limits are substantive; clarify Started as in preservation entry 59.

### M79. MATLABCLI/README.md:85-93 at local origin/main 9ad6b987247c1f0e42b877e1596e227f0b624f15

> The consumer conformance entry uses `wss_cli.createController`, not a substitute managed/Python implementation. On MATLAB R2026b with .NET 8, all 10 consumer catalog scenarios passed, and the MATLAB unit/runtime suite passed including CLI quit and command-failure disposal. The Engine harness also passed blocking input-double quit/interruption/quit, with real MATLAB cancellation and repeated cleanup. Native console input/keyboard Ctrl+C remain manual checks. Windows serial/BLE load-only compatibility passed separately; Ubuntu/macOS remain unverified. No port was opened or physical device connected. The managed helper has separate .NET-only checks in the wrapper.

Reader question: What implementation and hardware behavior did tests cover?

Verdict: **REWRITE**. State production factory positively; retain separate conformance/load-only evidence and missing native checks in maintainer record. Avoid substitute-implementation contrast.

### M80. MATLABCLI/README.md:101-102 at local origin/main 9ad6b987247c1f0e42b877e1596e227f0b624f15

> It uses the MATLAB-bundled Engine API directly and installs nothing. Its temporary input shadow is confined to the isolated test process.

Reader question: Does the Engine harness install anything?

Verdict: **KEEP**. Concrete side-effect scope, useful when executing the harness.

## Excluded local origin/main candidates

- M10 Core/README.md:131-133, local origin/main 80c74f0101387d74cd637ba2a6e0527b4c541d2e: The separate CoreBluetooth backend and universal native bridge are described in [MacBleBridge](tools/MacBleBridge/README.md). The reviewed receiver migration and Python-first candidate sequence are in [macOS BLE rollout](tools/ci/MACOS_BLE_ROLLOUT.md). — EXCLUDED: Literal separate component and navigation; no rhetorical contrast.

- M12 CSharp/README.md:13-15, local origin/main 021bf55f184b4bad4c78edf73a7f5d52a7a80221: - `src/` - library project containing `StimulationController` and `StimulationOptions` - `lib/` - vendor-provided and compatibility `.dll` dependencies required by the library - `temp/CLI_CSharp_WSS_Application/` - temporary handoff copy of the files that belong in the separate CLI repo — EXCLUDED: Neutral directory inventory; separate repo is literal.

- M23 CSharp/README.md:113-114, local origin/main 021bf55f184b4bad4c78edf73a7f5d52a7a80221: After authorization to commit the reviewed automation, create a separate Integration worktree from that automation commit. Run from the Integration tooling checkout: — EXCLUDED: Ordinary isolated-worktree setup; separate is literal.

- M35 CSharpCLI/README.md:27-27, local origin/main 22db07e7921442d16a6fe62cfa22b0fc5bf467c8: If you already cloned the app repo without submodules: — EXCLUDED: Ordinary existing-checkout alternative.

- M40 CSharpCLI/README.md:98-101, local origin/main 22db07e7921442d16a6fe62cfa22b0fc5bf467c8: - default config path: `<repo-root>/Config` - override: `--config=PATH` - the resolved config path is converted to an absolute path before constructing `StimulationController` - the config directory is created if it does not already exist — EXCLUDED: Neutral missing-directory creation behavior; no rhetorical opposition.

- M56 Unity/README.md:34-39, local origin/main 74c0bb7e47dd2abf3859bec181c495abdda6aaa0: ## How to pull a submodule change (Git Desktop) 1. Make sure the git submodule repo is already part of your repos in git desktop 2. If not add it by following `How to add a submodule to exiting project` 3. Go to the submodule repo and pull changes as normal.  4. Pulling a submodule will add a change in commit ID to the main repo.  	- Multiple changes in commit ID can be commited to the main repo as a single commit and together with other changes to the main repo. — EXCLUDED: Ordinary missing-setup branch; if not is conditional, not rhetoric.

- M73 MATLABCLI/README.md:3-6, local origin/main 9ad6b987247c1f0e42b877e1596e227f0b624f15: Application-owned MATLAB console UI. The reusable MATLAB wrapper is a separate repository, consumed as a pinned submodule at `external/wss-matlab-wrapper`. This mirrors the Python application/wrapper split and leaves the wrapper usable by other CLIs and GUIs. — EXCLUDED: Neutral literal repository split and relation to Python; draft already states ownership positively.

- M74 MATLABCLI/README.md:10-15, local origin/main 9ad6b987247c1f0e42b877e1596e227f0b624f15: - `src/+wss_cli/`: parser, production `createController` factory and command routing. - `Config/`: application-owned runtime JSON examples. - `logs/`: ignored runtime host logs. - `external/wss-matlab-wrapper/`: library submodule; owns managed helper and Core DLLs. - `tests/`: MATLAB tests for application argument/path behavior. - `conformance/`: consumer entry point into the wrapper's shared Core scenarios. — EXCLUDED: Literal Git-ignore/directory inventory, not rhetorical contrast.

Additional neutral candidate — Python/README.md:63 (local origin/main): The application repo should own its CLI and other app-specific orchestration, while importing `WssConfig` and `StimulationController` from this package. — EXCLUDED: while links app ownership with importing the wrapper; no opposing proposition. Shell --no-restore, Git insteadOf, C# Boolean negation, YAML false values and ordinary if-missing setup paths are executable or conditional instructions. They are excluded, not counted as rhetorical contrasts. Neutral chronology and simple option lists were read in whole-file context.

## Scope and evidence

Complete semantic source scope: ten preservation-branch READMEs plus ten local origin/main READMEs, at the hashes above. This is a wording audit with exact source quotes, not a current-platform certification, implementation audit or live website inspection. No repository edits, fetches, branch changes, builds or runtime executions. No review-branch files beyond these README paths were assigned to this report. Layering and other Docs branches are handled separately by the parent.
