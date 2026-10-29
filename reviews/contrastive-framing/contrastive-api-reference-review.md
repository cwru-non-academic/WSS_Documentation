# Contrastive wording audit: generated API reference

Read-only audit of `DocsNavigation/apiDocs/api/{core,csharp,unity}` on the current local navigation branch snapshot. Repository files were not edited. The previous API commit/push/dispatch rejection remains unchanged; no publication action was retried.

## Scope and method

Inventoried 61 managed-reference YAML files (Core 52, C# 4, Unity 5); excluded the three TOC YAML files. Extracted all 1763 nonempty summary, remarks and nested description fields before each references section. This covers parameter, return, exception, property-value and type/member prose represented through those fields. Signatures, declarations, type names, source paths, children/inheritance lists and references-only copies were excluded from contrast matching.

All extracted fields received a lexical/semantic-condition scan, with HTML attribute text stripped before candidate selection so DocFX's data-throw-if-not-resolved attribute did not create false contrast matches. The inventory below contains 205 distinct field texts across 324 occurrences. Identical whitespace-folded field text is grouped once, with every YAML occurrence/UID listed. HTML and generated XML copies are not independent findings; they inherit the YAML/source finding. Different texts remain separate even when they discuss the same contract.

The main inventory contains explicit contrasts and semantic alternatives; the appendix retains neutral constraints and positive optional-field matches so they are not counted as rhetorical defects. Search included comparisons such as same-as and differs-from, and not, rather than, instead, unlike, only, just, without, otherwise, except, unless, cannot, never, must, unsupported, optional, either, versus/vs, false/null/empty/missing/absent/invalid, no/neither, but/however. A match is not a defect. Most entries are necessary contracts and are marked KEEP. No Remarks requirement or concurrency guarantee is invented.

Limitations: this is a lexical-assisted inventory of the complete extracted prose-field population, not a guarantee that every implicit contrast without a recognizable marker was found. No rendered browser inspection, implementation verification or firmware/hardware testing was performed. Several baseline fields contain existing mojibake; quotes preserve it. Baseline metadata has no source.path/source remote records, so source candidates below are inferred from matching type declarations in local checkouts and are explicitly not generation provenance. Old HFI.Wss C# UIDs map to the current Wss.CSharpImplementation source candidate; source revisions and member correspondence must be verified before editing XML comments.

Inventory split: 119 contrast/semantic-alternative field texts and 86 neutral constraints/lexical candidates. These are audit entries, not counts of writing defects.

## Decisions

- KEEP: 110 main-inventory field texts. The 86 neutral appendix entries also remain unchanged.
- REWRITE: 5 main-inventory field texts.
- REWRITE / VERIFY: 1 main-inventory field text.
- VERIFY THEN REWRITE: 3 main-inventory field texts.

Apply approved wording changes to owning C# XML documentation comments, then regenerate pinned metadata and HTML. Do not hand-edit YAML/HTML. KEEP includes valid restrictions and boolean alternatives; it does not claim that stale metadata matches the release candidate implementation.

## Full deduplicated finding inventory

## Contrast / semantic alternative

### A001 — KEEP

Reader question: What does ModuleQuery send and cache on supported firmware versus older firmware?

Quoted field content (YAML block whitespace folded; markup retained):

```text
- When supported (J+): this method transmits the request and, upon reply, ProcessFrame caches the data-only slice under the sender's target address. Use <xref href="Wss.CoreModule.WssClient.TryGetModuleQueryData(Wss.CoreModule.WssTarget%2cSystem.Byte%5b%5d%40)" data-throw-if-not-resolved="false"></xref> to retrieve the last snapshot. - When not supported: this method does not transmit. For <code>moduleIndex == 1</code>, it seeds a default 16-byte zero array into the per-target cache, allowing the core to decode a default profile. The returned string indicates that defaults are being used.
```

Reason: This contrast specifies ownership, mutation, framing, transport choice, cancellation, or physical-I/O boundaries rather than rhetorical emphasis. Preserve it.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WssClient.yml:526` — `remarks` — `Wss.CoreModule.WssClient.ModuleQuery(System.Int32,Wss.CoreModule.WssTarget,System.Threading.CancellationToken)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\WssClient.cs`

### A002 — KEEP

Reader question: Does StreamChangeNoIPI include an inter-pulse-interval field?

Quoted field content (YAML block whitespace folded; markup retained):

```text
'Streaming update: no IPI field.'
```

Reason: The distinction changes caller behavior, payload interpretation, valid input, lifetime, capability handling or state. Removing it would lose contract information.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WSSMessageIDs.yml:568` — `summary` — `Wss.CoreModule.WSSMessageIDs.StreamChangeNoIPI`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\WSSMessageIDs.cs`

### A003 — KEEP

Reader question: Which fields or payload groups does StreamChangeNoPA accept, require together or omit?

Quoted field content (YAML block whitespace folded; markup retained):

```text
'Streaming update: no pulse amplitude.'
```

Reason: The distinction changes caller behavior, payload interpretation, valid input, lifetime, capability handling or state. Removing it would lose contract information.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WSSMessageIDs.yml:588` — `summary` — `Wss.CoreModule.WSSMessageIDs.StreamChangeNoPA`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\WSSMessageIDs.cs`

### A004 — KEEP

Reader question: Which fields or payload groups does StreamChangeNoPW accept, require together or omit?

Quoted field content (YAML block whitespace folded; markup retained):

```text
'Streaming update: no pulse width.'
```

Reason: The distinction changes caller behavior, payload interpretation, valid input, lifetime, capability handling or state. Removing it would lose contract information.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WSSMessageIDs.yml:608` — `summary` — `Wss.CoreModule.WSSMessageIDs.StreamChangeNoPW`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\WSSMessageIDs.cs`

### A005 — KEEP

Reader question: Which state or result makes isVersionSupported true or false, and can output data still be present?

Quoted field content (YAML block whitespace folded; markup retained):

```text
<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the supplied version is recognized and supported; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.
```

Reason: This is a return-value/state or fallback contract. The alternatives tell callers how to branch and must remain.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WSSVersionHandler.yml:86` — `description` — `Wss.CoreModule.WSSVersionHandler.isVersionSupported(System.String)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\WSSVersionHandler.cs`

### A006 — KEEP

Reader question: If BASIC is unavailable, does TryUpdateWaveform throw, log, return false, or send a command?

Quoted field content (YAML block whitespace folded; markup retained):

```text
<code>true</code> if BASIC is available and the call succeeded; otherwise <code>false</code>.
```

Reason: This is a return-value/state or fallback contract. The alternatives tell callers how to branch and must remain.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.StimParamsBasicExtensions.yml:321` — `description` — `Wss.CoreModule.StimParamsBasicExtensions.TryUpdateWaveform(Wss.CalibrationModule.IStimParamsCore,Wss.CoreModule.WaveformBuilder,System.Int32,Wss.CoreModule.WssTarget)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\SideFunctions\StimParamsBasicExtensions.cs`

### A007 — KEEP

Reader question: Which state or result makes TryGetStimParam true or false, and can output data still be present?

Quoted field content (YAML block whitespace folded; markup retained):

```text
<code>true</code> if found, otherwise <code>false</code>.
```

Reason: This is a return-value/state or fallback contract. The alternatives tell callers how to branch and must remain.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CalibrationModule.IStimParamsCore.yml:314` — `description` — `Wss.CalibrationModule.IStimParamsCore.TryGetStimParam(System.String,System.Single@)`
- `DocsNavigation\apiDocs\api\core\Wss.ModelModule.ModelParamsLayer.yml:494` — `description` — `Wss.ModelModule.ModelParamsLayer.TryGetStimParam(System.String,System.Single@)`
- `DocsNavigation\apiDocs\api\unity\WSS.Unity.StimulationParams.yml:1311` — `description` — `WSS.Unity.StimulationParams.TryGetStimParam(System.String,System.Single@)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CalibrationModule\IStimParamsCore.cs`, `Core\ModelModule\ModelParamsLayer.cs`, `Unity\UnityImplementation\StimualtionParams.cs`

### A008 — KEEP

Reader question: Which state or result makes IsLEDSettingsAvailable true or false, and can output data still be present?

Quoted field content (YAML block whitespace folded; markup retained):

```text
<code>true</code> if LED settings are supported; otherwise, <code>false</code>.
```

Reason: This is a return-value/state or fallback contract. The alternatives tell callers how to branch and must remain.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WSSVersionHandler.yml:168` — `description` — `Wss.CoreModule.WSSVersionHandler.IsLEDSettingsAvailable`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\WSSVersionHandler.cs`

### A009 — KEEP

Reader question: Which state or result makes IsModuleQueryAvailable true or false, and can output data still be present?

Quoted field content (YAML block whitespace folded; markup retained):

```text
<code>true</code> if module query is supported; otherwise, <code>false</code>.
```

Reason: This is a return-value/state or fallback contract. The alternatives tell callers how to branch and must remain.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WSSVersionHandler.yml:191` — `description` — `Wss.CoreModule.WSSVersionHandler.IsModuleQueryAvailable`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\WSSVersionHandler.cs`

### A010 — KEEP

Reader question: Which state or result makes TryUnescapeAndValidate true or false, and can output data still be present?

Quoted field content (YAML block whitespace folded; markup retained):

```text
<code>true</code> if the checksum is valid; otherwise <code>false</code>.
```

Reason: This is a return-value/state or fallback contract. The alternatives tell callers how to branch and must remain.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WssFrameCodec.yml:148` — `description` — `Wss.CoreModule.WssFrameCodec.TryUnescapeAndValidate(System.ReadOnlySpan{System.Byte},System.Byte[]@)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\WssFrameCodec.cs`

### A011 — KEEP

Reader question: Which state or result makes IsFeatureAvailable true or false, and can output data still be present?

Quoted field content (YAML block whitespace folded; markup retained):

```text
<code>true</code> if the feature is available in this firmware version; otherwise, <code>false</code>.
```

Reason: This is a return-value/state or fallback contract. The alternatives tell callers how to branch and must remain.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WSSVersionHandler.yml:142` — `description` — `Wss.CoreModule.WSSVersionHandler.IsFeatureAvailable(System.String)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\WSSVersionHandler.cs`

### A012 — KEEP

Reader question: Which state or result makes TryGetModelParam``1 true or false, and can output data still be present?

Quoted field content (YAML block whitespace folded; markup retained):

```text
<code>true</code> if the key exists and was converted; otherwise <code>false</code>.
```

Reason: This is a return-value/state or fallback contract. The alternatives tell callers how to branch and must remain.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.ModelModule.ModelConfigController.yml:299` — `description` — `Wss.ModelModule.ModelConfigController.TryGetModelParam``1(System.String,``0@)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\ModelModule\ModelConfigController.cs`

### A013 — KEEP

Reader question: Which state or result makes IsModeValid true or false, and can output data still be present?

Quoted field content (YAML block whitespace folded; markup retained):

```text
<code>true</code> if the mode is supported; otherwise <code>false</code>.
```

Reason: This is a return-value/state or fallback contract. The alternatives tell callers how to branch and must remain.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.ModelModule.IModelParamsCore.yml:107` — `description` — `Wss.ModelModule.IModelParamsCore.IsModeValid`
- `DocsNavigation\apiDocs\api\core\Wss.ModelModule.ModelParamsLayer.yml:148` — `description` — `Wss.ModelModule.ModelParamsLayer.IsModeValid`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\ModelModule\IModelParamsCore.cs`, `Core\ModelModule\ModelParamsLayer.cs`

### A014 — KEEP

Reader question: Which state or result makes TryGet``1 true or false, and can output data still be present?

Quoted field content (YAML block whitespace folded; markup retained):

```text
<code>true</code> if the value exists and converts; otherwise <code>false</code>.
```

Reason: This is a return-value/state or fallback contract. The alternatives tell callers how to branch and must remain.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.ModelModule.ModelConfig.yml:129` — `description` — `Wss.ModelModule.ModelConfig.TryGet``1(System.String,``0@)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\ModelModule\ModelConfig.cs`

### A015 — KEEP

Reader question: If BASIC is unavailable, does TryGetBasic throw, log, return false, or send a command?

Quoted field content (YAML block whitespace folded; markup retained):

```text
<code>true</code> when BASIC is available, otherwise <code>false</code>.
```

Reason: This is a return-value/state or fallback contract. The alternatives tell callers how to branch and must remain.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CalibrationModule.IStimParamsCore.yml:1080` — `description` — `Wss.CalibrationModule.IStimParamsCore.TryGetBasic(Wss.CoreModule.IBasicStimulation@)`
- `DocsNavigation\apiDocs\api\core\Wss.CalibrationModule.StimParamsLayer.yml:1189` — `description` — `Wss.CalibrationModule.StimParamsLayer.TryGetBasic(Wss.CoreModule.IBasicStimulation@)`
- `DocsNavigation\apiDocs\api\core\Wss.ModelModule.ModelParamsLayer.yml:1318` — `description` — `Wss.ModelModule.ModelParamsLayer.TryGetBasic(Wss.CoreModule.IBasicStimulation@)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CalibrationModule\IStimParamsCore.cs`, `Core\CalibrationModule\StimParamsLayer.cs`, `Core\ModelModule\ModelParamsLayer.cs`

### A016 — KEEP

Reader question: What happens if I call or read Ready before initialization?

Quoted field content (YAML block whitespace folded; markup retained):

```text
<code>true</code> when ready; otherwise <code>false</code>. Returns <code>false</code> before initialization.
```

Reason: This is a return-value/state or fallback contract. The alternatives tell callers how to branch and must remain.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:1624` — `description` — `HFI.Wss.StimulationController.Ready`

Source XML-comment candidates (type-declaration match; verify revision/member): `CSharp\src\StimulationController.cs`

### A017 — KEEP

Reader question: What happens if I call or read Started before initialization?

Quoted field content (YAML block whitespace folded; markup retained):

```text
<code>true</code> when started; otherwise <code>false</code>. Returns <code>false</code> before initialization.
```

Reason: This is a return-value/state or fallback contract. The alternatives tell callers how to branch and must remain.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:1647` — `description` — `HFI.Wss.StimulationController.Started`

Source XML-comment candidates (type-declaration match; verify revision/member): `CSharp\src\StimulationController.cs`

### A018 — KEEP

Reader question: Which state or result makes IsFingerValid true or false, and can output data still be present?

Quoted field content (YAML block whitespace folded; markup retained):

```text
<code>true</code> when the channel is in range; otherwise <code>false</code>.
```

Reason: This is a return-value/state or fallback contract. The alternatives tell callers how to branch and must remain.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:1490` — `description` — `HFI.Wss.StimulationController.IsFingerValid(System.String)`

Source XML-comment candidates (type-declaration match; verify revision/member): `CSharp\src\StimulationController.cs`

### A019 — KEEP

Reader question: Which state or result makes TryGetStimParam true or false, and can output data still be present?

Quoted field content (YAML block whitespace folded; markup retained):

```text
<code>true</code> when the key exists; otherwise <code>false</code>.
```

Reason: This is a return-value/state or fallback contract. The alternatives tell callers how to branch and must remain.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\unity\WSS.Unity.Stimulation.yml:1063` — `description` — `WSS.Unity.Stimulation.TryGetStimParam(System.String,System.Single@)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Unity\UnityImplementation\Stimulation.Commands.cs`, `Unity\UnityImplementation\Stimulation.cs`

### A020 — KEEP

Reader question: Which state or result makes isModeValid true or false, and can output data still be present?

Quoted field content (YAML block whitespace folded; markup retained):

```text
<code>true</code> when the mode is valid; otherwise <code>false</code>.
```

Reason: This is a return-value/state or fallback contract. The alternatives tell callers how to branch and must remain.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:1597` — `description` — `HFI.Wss.StimulationController.isModeValid`

Source XML-comment candidates (type-declaration match; verify revision/member): `CSharp\src\StimulationController.cs`

### A021 — KEEP

Reader question: Which state or result makes TryGetStimParam true or false, and can output data still be present?

Quoted field content (YAML block whitespace folded; markup retained):

```text
<code>true</code> when the parameter exists; otherwise <code>false</code>.
```

Reason: This is a return-value/state or fallback contract. The alternatives tell callers how to branch and must remain.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:1157` — `description` — `HFI.Wss.StimulationController.TryGetStimParam(System.String,System.Single@)`

Source XML-comment candidates (type-declaration match; verify revision/member): `CSharp\src\StimulationController.cs`

### A022 — KEEP

Reader question: May I retain the BytesReceived buffer after the callback returns?

Quoted field content (YAML block whitespace folded; markup retained):

```text
<p> Implementations should invoke this event promptly as data becomes available. Callers should treat the provided buffer as read-only and copy it if they need to retain it. </p> <p> This event is typically raised on a background thread. Consumers should marshal to the main thread if required by their environment. </p>
```

Reason: This contrast specifies ownership, mutation, framing, transport choice, cancellation, or physical-I/O boundaries rather than rhetorical emphasis. Preserve it.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.Transports.ITransport.yml:100` — `remarks` — `Wss.Transports.ITransport.BytesReceived`
- `DocsNavigation\apiDocs\api\core\Wss.Transports.SerialPortTransport.yml:132` — `remarks` — `Wss.Transports.SerialPortTransport.BytesReceived`
- `DocsNavigation\apiDocs\api\core\Wss.Transports.TestModeTransport.yml:253` — `remarks` — `Wss.Transports.TestModeTransport.BytesReceived`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\Interfaces\ITransport.cs`, `Core\CoreModule\TestModeTransport.cs`, `Core\WSS.Transport.Serial\SerialPortTransport.cs`

### A023 — KEEP

Reader question: Which fields or payload groups does ContactConfigDefinition accept, require together or omit?

Quoted field content (YAML block whitespace folded; markup retained):

```text
<p> The setup arrays use caller-facing ordering where index 0 is closest to the switch and index 3 is farthest. Each element must be 0 (unused), 1 (source), or 2 (sink). </p> <p> LED configuration is optional and may be ignored by firmware that does not support LED settings. </p>
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.ContactConfigDefinition.yml:23` — `remarks` — `Wss.CoreModule.ContactConfigDefinition`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\ContactConfigDefinition.cs`

### A024 — KEEP

Reader question: How must ITransport consumers handle callback threading and arbitrary receive chunks?

Quoted field content (YAML block whitespace folded; markup retained):

```text
<p> This interface is intentionally minimal and transport-agnostic so higher-level code can be reused with different physical links (UART/COM, RFCOMM, BLE GATT, sockets). </p> <p> <b>Threading:</b> Implementations typically raise <xref href="Wss.Transports.ITransport.BytesReceived" data-throw-if-not-resolved="false"></xref> from a background thread. If your application requires main-thread access, marshal the callback to the main thread. </p> <p> <b>Chunking:</b> Incoming chunks may contain partial frames, multiple frames, or arbitrary boundaries. Always pass chunks to a frame codec/deframer instead of assuming message boundaries align with reads. </p>
```

Reason: This contrast specifies ownership, mutation, framing, transport choice, cancellation, or physical-I/O boundaries rather than rhetorical emphasis. Preserve it.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.Transports.ITransport.yml:29` — `remarks` — `Wss.Transports.ITransport`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\Interfaces\ITransport.cs`

### A025 — KEEP

Reader question: How must SerialPortTransport consumers handle callback threading and arbitrary receive chunks?

Quoted field content (YAML block whitespace folded; markup retained):

```text
<p><b>Threading:</b> <xref href="Wss.Transports.SerialPortTransport.BytesReceived" data-throw-if-not-resolved="false"></xref> is raised from a background task. If your app (e.g., Unity) requires main-thread processing, marshal the callback to the main thread before touching UI/GameObjects.</p> <p><b>Chunking:</b> Serial I/O does not preserve message boundaries. Each chunk may contain partial frames or multiple frames. Always pass chunks to a frame codec/deframer.</p> <p><b>Timeouts:</b> The read loop polls <xref href="System.IO.Ports.SerialPort.BytesToRead" data-throw-if-not-resolved="false"></xref> and catches <xref href="System.TimeoutException" data-throw-if-not-resolved="false"></xref>s, which are normal with short read timeouts.</p>
```

Reason: This contrast specifies ownership, mutation, framing, transport choice, cancellation, or physical-I/O boundaries rather than rhetorical emphasis. Preserve it.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.Transports.SerialPortTransport.yml:29` — `remarks` — `Wss.Transports.SerialPortTransport`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\WSS.Transport.Serial\SerialPortTransport.cs`

### A026 — REWRITE

Reader question: What validation and encoding apply to these fields?

Quoted field content (YAML block whitespace folded; markup retained):

```text
<xref href="Wss.CoreModule.ScheduleDefinition.ScheduleId" data-throw-if-not-resolved="false"></xref>, <xref href="Wss.CoreModule.ScheduleDefinition.DurationMs" data-throw-if-not-resolved="false"></xref>, and <xref href="Wss.CoreModule.ScheduleDefinition.SyncSignal" data-throw-if-not-resolved="false"></xref> use the same protocol validation and encoding rules as the previous positional overload.
```

Reason: The rule is needed; the historical comparison is not the rule. Link to the current shared encoding contract or state the actual rules rather than relying on an earlier overload.

Suggested direction: Use the current schedule-field validation/encoding reference; retain its units, bounds and wire representation.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.ScheduleDefinition.yml:22` — `remarks` — `Wss.CoreModule.ScheduleDefinition`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\ScheduleDefinition.cs`

### A027 — KEEP

Reader question: Does Deframe validate the checksum, and how is an invalid frame reported?

Quoted field content (YAML block whitespace folded; markup retained):

```text
0..N complete, unescaped frames (END removed). Checksum is not validated here.
```

Reason: This contrast specifies ownership, mutation, framing, transport choice, cancellation, or physical-I/O boundaries rather than rhetorical emphasis. Preserve it.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.IFrameCodec.yml:93` — `description` — `Wss.CoreModule.IFrameCodec.Deframe(System.Byte[])`
- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WssFrameCodec.yml:107` — `description` — `Wss.CoreModule.WssFrameCodec.Deframe(System.Byte[])`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\Interfaces\IFrameCodec.cs`, `Core\CoreModule\WssFrameCodec.cs`

### A028 — KEEP

Reader question: Which fields or payload groups does TryDecode accept, require together or omit?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Attempts to decode a ModuleQuery settings payload (data-only slice) into a <xref href="Wss.CoreModule.ModuleSettings" data-throw-if-not-resolved="false"></xref> instance.
```

Reason: This contrast specifies ownership, mutation, framing, transport choice, cancellation, or physical-I/O boundaries rather than rhetorical emphasis. Preserve it.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.ModuleSettings.yml:433` — `summary` — `Wss.CoreModule.ModuleSettings.TryDecode(System.ReadOnlySpan{System.Byte},Wss.CoreModule.ModuleSettings@)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\ModuleSettings.cs`

### A029 — KEEP

Reader question: Which failures does TryUpdateWaveform report as exceptions versus return values or logs?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Attempts to forward to BASIC.UpdateWaveform(WaveformBuilder,...) without throwing.
```

Reason: Capability availability and error/no-op behavior affect safe callers. Keep the restriction and the observable behavior.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.StimParamsBasicExtensions.yml:302` — `summary` — `Wss.CoreModule.StimParamsBasicExtensions.TryUpdateWaveform(Wss.CalibrationModule.IStimParamsCore,Wss.CoreModule.WaveformBuilder,System.Int32,Wss.CoreModule.WssTarget)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\SideFunctions\StimParamsBasicExtensions.cs`

### A030 — KEEP

Reader question: Which failures does TryGetStimParam report as exceptions versus return values or logs?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Attempts to retrieve a stimulation parameter value without throwing.
```

Reason: Capability availability and error/no-op behavior affect safe callers. Keep the restriction and the observable behavior.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CalibrationModule.StimParamsConfigController.yml:315` — `summary` — `Wss.CalibrationModule.StimParamsConfigController.TryGetStimParam(System.String,System.Single@)`
- `DocsNavigation\apiDocs\api\core\Wss.CalibrationModule.StimParamsLayer.yml:387` — `summary` — `Wss.CalibrationModule.StimParamsLayer.TryGetStimParam(System.String,System.Single@)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CalibrationModule\StimParamsConfigController.cs`, `Core\CalibrationModule\StimParamsLayer.cs`

### A031 — KEEP

Reader question: Does TryUnescapeAndValidate validate the checksum, and how is an invalid frame reported?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Attempts to unescape and validate a single frame (without END), returning false if the checksum does not match. Useful if you want checksum enforcement at the codec layer.
```

Reason: This is a return-value/state or fallback contract. The alternatives tell callers how to branch and must remain.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WssFrameCodec.yml:129` — `summary` — `Wss.CoreModule.WssFrameCodec.TryUnescapeAndValidate(System.ReadOnlySpan{System.Byte},System.Byte[]@)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\WssFrameCodec.cs`

### A033 — KEEP

Reader question: If BASIC is unavailable, does Save throw, log, return false, or send a command?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Broadcasts to all connected WSS units. No-op (logs an error) if basic stimulation is not supported.
```

Reason: Capability availability and error/no-op behavior affect safe callers. Keep the restriction and the observable behavior.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\unity\WSS.Unity.Stimulation.yml:374` — `remarks` — `WSS.Unity.Stimulation.Save`
- `DocsNavigation\apiDocs\api\unity\WSS.Unity.Stimulation.yml:429` — `remarks` — `WSS.Unity.Stimulation.load`
- `DocsNavigation\apiDocs\api\unity\WSS.Unity.Stimulation.yml:487` — `remarks` — `WSS.Unity.Stimulation.updateWaveform(System.Int32[],System.Int32)`
- `DocsNavigation\apiDocs\api\unity\WSS.Unity.Stimulation.yml:629` — `remarks` — `WSS.Unity.Stimulation.updateWaveform(Wss.CoreModule.WaveformBuilder,System.Int32)`
- `DocsNavigation\apiDocs\api\unity\WSS.Unity.Stimulation.yml:793` — `remarks` — `WSS.Unity.Stimulation.UpdateIPD(System.Int32,System.Int32)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Unity\UnityImplementation\Stimulation.Commands.cs`, `Unity\UnityImplementation\Stimulation.cs`

### A034 — KEEP

Reader question: If BASIC is unavailable, does Save throw, log, return false, or send a command?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Broadcasts to all connected WSS units. No-op and logs an error when basic stimulation is unavailable.
```

Reason: Capability availability and error/no-op behavior affect safe callers. Keep the restriction and the observable behavior.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\unity\WSS.Unity.StimulationParams.yml:365` — `remarks` — `WSS.Unity.StimulationParams.Save`
- `DocsNavigation\apiDocs\api\unity\WSS.Unity.StimulationParams.yml:420` — `remarks` — `WSS.Unity.StimulationParams.load`
- `DocsNavigation\apiDocs\api\unity\WSS.Unity.StimulationParams.yml:478` — `remarks` — `WSS.Unity.StimulationParams.updateWaveform(System.Int32[],System.Int32)`
- `DocsNavigation\apiDocs\api\unity\WSS.Unity.StimulationParams.yml:617` — `remarks` — `WSS.Unity.StimulationParams.updateWaveform(Wss.CoreModule.WaveformBuilder,System.Int32)`
- `DocsNavigation\apiDocs\api\unity\WSS.Unity.StimulationParams.yml:713` — `remarks` — `WSS.Unity.StimulationParams.WaveformSetup(Wss.CoreModule.WaveformBuilder,System.Int32)`
- `DocsNavigation\apiDocs\api\unity\WSS.Unity.StimulationParams.yml:781` — `remarks` — `WSS.Unity.StimulationParams.UpdateIPD(System.Int32,System.Int32)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Unity\UnityImplementation\StimualtionParams.cs`

### A035 — KEEP

Reader question: Does StimulateAnalog communicate with a physical device or operate entirely in memory?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Caches per-channel amplitude, pulse width, and IPI. No I/O here; the streaming loop pushes cached values to the device.
```

Reason: This contrast specifies ownership, mutation, framing, transport choice, cancellation, or physical-I/O boundaries rather than rhetorical emphasis. Preserve it.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CalibrationModule.StimParamsLayer.yml:1334` — `summary` — `Wss.CalibrationModule.StimParamsLayer.StimulateAnalog(System.Int32,System.Int32,System.Single,System.Int32)`
- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.IStimulationCore.yml:167` — `summary` — `Wss.CoreModule.IStimulationCore.StimulateAnalog(System.Int32,System.Int32,System.Single,System.Int32)`
- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WssStimulationCore.yml:287` — `summary` — `Wss.CoreModule.WssStimulationCore.StimulateAnalog(System.Int32,System.Int32,System.Single,System.Int32)`
- `DocsNavigation\apiDocs\api\core\Wss.ModelModule.ModelParamsLayer.yml:1463` — `summary` — `Wss.ModelModule.ModelParamsLayer.StimulateAnalog(System.Int32,System.Int32,System.Single,System.Int32)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CalibrationModule\StimParamsLayer.cs`, `Core\CoreModule\Interfaces\IStimulationCore.cs`, `Core\CoreModule\WssStimulationCore.cs`, `Core\ModelModule\ModelParamsLayer.cs`

### A036 — KEEP

Reader question: Which fields or payload groups does EditEventPw accept, require together or omit?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Changes an event's pulse widths: [standardPW, rechargePW, IPD] (μs). Uses 8-bit fields if all ≤ oneByte; otherwise encodes all three as 16-bit big-endian. Wire order matches firmware: [stdPW, IPD, rechPW].
```

Reason: This is a return-value/state or fallback contract. The alternatives tell callers how to branch and must remain.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WssClient.yml:955` — `summary` — `Wss.CoreModule.WssClient.EditEventPw(System.Int32,System.Int32[],Wss.CoreModule.WssTarget,System.Threading.CancellationToken)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\WssClient.cs`

### A040 — KEEP

Reader question: Which fields or payload groups does AmpCurveKey accept, require together or omit?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Convenience selector for amplitude curves. Returns "10mA" when the <xref href="Wss.CoreModule.ModuleSettings.HwConfigFlags.TenMilliAmpPA" data-throw-if-not-resolved="false"></xref> bit is set; otherwise "72mA".
```

Reason: This is a return-value/state or fallback contract. The alternatives tell callers how to branch and must remain.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.ModuleSettings.yml:384` — `summary` — `Wss.CoreModule.ModuleSettings.AmpCurveKey`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\ModuleSettings.cs`

### A041 — KEEP

Reader question: Which wire fields does ZeroOutStim send when it stops stimulation?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Convenience: streams zeros to PA, PW, and IPI (effectively stopping stimulation). Uses opcode 0x31 (PA+PW, no IPI) with trailing zeros to match the original layout.
```

Reason: The distinction changes caller behavior, payload interpretation, valid input, lifetime, capability handling or state. Removing it would lose contract information.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WssClient.yml:1753` — `summary` — `Wss.CoreModule.WssClient.ZeroOutStim(Wss.CoreModule.WssTarget,System.Threading.CancellationToken)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\WssClient.cs`

### A043 — KEEP

Reader question: What profile is available when a module probe is unsupported or has no data?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Creates a default (synthetic) 72 mA profile for cases where ModuleQuery is not supported or data is unavailable. Fields are set to conservative defaults and <xref href="Wss.CoreModule.ModuleSettings.IsPartial" data-throw-if-not-resolved="false"></xref> is true.
```

Reason: This is a return-value/state or fallback contract. The alternatives tell callers how to branch and must remain.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.ModuleSettings.yml:469` — `summary` — `Wss.CoreModule.ModuleSettings.CreateDefault72mA(System.Boolean)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\ModuleSettings.cs`

### A044 — REWRITE

Reader question: Which request-field combinations are valid?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Creates an event on the WSS device from a grouped request model. The request supports the same payload combinations as the previous overload set: basic, shape-only, amplitudes plus pulse widths, or shapes plus amplitudes plus pulse widths.
```

Reason: Keep all field-combination constraints; remove the historical overload-set comparison, which forces readers to consult an earlier API surface.

Suggested direction: List the supported field combinations directly, preserving paired-shape, paired-amplitude and pulse-width requirements wherever present.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WssClient.yml:717` — `summary` — `Wss.CoreModule.WssClient.CreateEvent(Wss.CoreModule.CreateEventRequest,Wss.CoreModule.WssTarget,System.Threading.CancellationToken)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\WssClient.cs`

### A046 — KEEP

Reader question: Does TryDecode take the data slice or an entire framed message?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Data-only bytes in the expected order (length ≥ 16 recommended).
```

Reason: This contrast specifies ownership, mutation, framing, transport choice, cancellation, or physical-I/O boundaries rather than rhetorical emphasis. Preserve it.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.ModuleSettings.yml:440` — `description` — `Wss.CoreModule.ModuleSettings.TryDecode(System.ReadOnlySpan{System.Byte},Wss.CoreModule.ModuleSettings@)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\ModuleSettings.cs`

### A048 — KEEP

Reader question: How does #ctor handle missing or invalid configuration: fallback, persistence or failure?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Default JSON content to save and use when the file does not exist.
```

Reason: This contrast specifies ownership, mutation, framing, transport choice, cancellation, or physical-I/O boundaries rather than rhetorical emphasis. Preserve it.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.DictConfigBase.yml:133` — `description` — `Wss.CoreModule.DictConfigBase.#ctor(System.String,Newtonsoft.Json.Linq.JObject)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\CoreJsonConfig\DictConfigBase.cs`

### A050 — KEEP

Reader question: Does Dispose disconnect, dispose owned resources or change the GameObject state?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Disposes the underlying transport only when this instance was constructed with <code>ownsTransport</code> set to <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>. This method does not call <xref href="Wss.CoreModule.WssClient.DisconnectAsync(System.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref> automatically.
```

Reason: This is a return-value/state or fallback contract. The alternatives tell callers how to branch and must remain.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WssClient.yml:294` — `remarks` — `Wss.CoreModule.WssClient.Dispose`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\WssClient.cs`

### A051 — VERIFY THEN REWRITE

Reader question: Is this command usable with my firmware, and is a response decoded?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Echo round-trip (0x07). Two opaque data bytes are echoed back. Untested, and reading from WSS devices is not yet implemented.
```

Reason: This combines an API restriction with temporary project-status prose. Check the final release implementation/firmware contract. Preserve any real unsupported-operation restriction; retire stale status wording only after verification.

Suggested direction: Describe the verified send/receive behavior and applicable firmware support. No supported capability or firmware threshold is inferred by this audit.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WssClient.yml:414` — `summary` — `Wss.CoreModule.WssClient.Echo(System.Int32,System.Int32,Wss.CoreModule.WssTarget,System.Threading.CancellationToken)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\WssClient.cs`

### A052 — KEEP

Reader question: Which transport or parameter behavior does the selected TenMilliAmpPA mode enable?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Enables 10 mA PA mode (bit 1; value 2). When not set, PA mapping uses the default curve.
```

Reason: This contrast specifies ownership, mutation, framing, transport choice, cancellation, or physical-I/O boundaries rather than rhetorical emphasis. Preserve it.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.ModuleSettings.HwConfigFlags.yml:79` — `summary` — `Wss.CoreModule.ModuleSettings.HwConfigFlags.TenMilliAmpPA`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\ModuleSettings.cs`

### A053 — KEEP

Reader question: Does releaseRadio disconnect, dispose owned resources or change the GameObject state?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Equivalent to invoking the wrapper shutdown path without disabling the GameObject.
```

Reason: This contrast specifies ownership, mutation, framing, transport choice, cancellation, or physical-I/O boundaries rather than rhetorical emphasis. Preserve it.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\unity\WSS.Unity.Stimulation.yml:219` — `remarks` — `WSS.Unity.Stimulation.releaseRadio`

Source XML-comment candidates (type-declaration match; verify revision/member): `Unity\UnityImplementation\Stimulation.Commands.cs`, `Unity\UnityImplementation\Stimulation.cs`

### A054 — KEEP

Reader question: Which framing bytes are included in TryUnescapeAndValidate input/output and checksum computation?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Escaped frame bytes (without the END terminator).
```

Reason: This contrast specifies ownership, mutation, framing, transport choice, cancellation, or physical-I/O boundaries rather than rhetorical emphasis. Preserve it.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WssFrameCodec.yml:139` — `description` — `Wss.CoreModule.WssFrameCodec.TryUnescapeAndValidate(System.ReadOnlySpan{System.Byte},System.Byte[]@)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\WssFrameCodec.cs`

### A055 — KEEP

Reader question: When is DeviceName used relative to automatic selection and DeviceId?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Exact BLE device name to scan for when <xref href="Wss.Transports.BleNusTransportOptions.AutoSelectDevice" data-throw-if-not-resolved="false"></xref> is disabled and <xref href="Wss.Transports.BleNusTransportOptions.DeviceId" data-throw-if-not-resolved="false"></xref> is not provided.
```

Reason: This contrast specifies ownership, mutation, framing, transport choice, cancellation, or physical-I/O boundaries rather than rhetorical emphasis. Preserve it.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.Transports.BleNusTransportOptions.yml:78` — `summary` — `Wss.Transports.BleNusTransportOptions.DeviceName`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\WSS.Transport.BLE\BleNusTransportOptions.cs`

### A061 — KEEP

Reader question: How do I select an explicit device/port for forcePort, and when is automatic selection used?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Forces connection to a specific COM port instead of auto-detecting.
```

Reason: This contrast specifies ownership, mutation, framing, transport choice, cancellation, or physical-I/O boundaries rather than rhetorical emphasis. Preserve it.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\unity\WSS.Unity.Stimulation.yml:107` — `summary` — `WSS.Unity.Stimulation.forcePort`
- `DocsNavigation\apiDocs\api\unity\WSS.Unity.Stimulationbasic.yml:85` — `summary` — `WSS.Unity.Stimulationbasic.forcePort`
- `DocsNavigation\apiDocs\api\unity\WSS.Unity.StimulationParams.yml:104` — `summary` — `WSS.Unity.StimulationParams.forcePort`

Source XML-comment candidates (type-declaration match; verify revision/member): `Unity\UnityImplementation\StimualtionParams.cs`, `Unity\UnityImplementation\Stimulation.Commands.cs`, `Unity\UnityImplementation\Stimulation.cs`, `Unity\UnityImplementation\StimulationBasic.cs`

### A062 — KEEP

Reader question: Which framing bytes are included in WssFrameCodec input/output and checksum computation?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Frame codec implementing a SLIP-like protocol: <ul><li><b>END</b> (<code>0xC0</code>) terminates a frame.</li><li><b>ESC</b> (<code>0xDB</code>) escapes special bytes inside the frame.</li><li><b>END_SUB</b> (<code>0xDC</code>) represents END when escaped.</li><li><b>ESC_SUB</b> (<code>0xDD</code>) represents ESC when escaped.</li></ul> Frames on the wire are <code>[sender][target][payload...][checksum][END]</code>. The checksum is computed over all bytes except the last two (checksum + END).
```

Reason: This contrast specifies ownership, mutation, framing, transport choice, cancellation, or physical-I/O boundaries rather than rhetorical emphasis. Preserve it.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WssFrameCodec.yml:21` — `summary` — `Wss.CoreModule.WssFrameCodec`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\WssFrameCodec.cs`

### A063 — KEEP

Reader question: How does GetModelParam``1 handle missing or invalid configuration: fallback, persistence or failure?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Get a required typed value from a dotted key. Throws if the key is missing or the value cannot be converted to <code class="typeparamref">T</code>.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.ModelModule.ModelConfigController.yml:235` — `summary` — `Wss.ModelModule.ModelConfigController.GetModelParam``1(System.String)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\ModelModule\ModelConfigController.cs`

### A064 — KEEP

Reader question: How does GetOrDefault``1 handle missing or invalid configuration: fallback, persistence or failure?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Get a typed value or a default when missing. Does not throw on absent keys.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.ModelModule.ModelConfigController.yml:323` — `summary` — `Wss.ModelModule.ModelConfigController.GetOrDefault``1(System.String,``0)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\ModelModule\ModelConfigController.cs`

### A086 — KEEP

Reader question: Which fields or payload groups does ModuleQuery accept, require together or omit?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Gets settings or information for the target WSS (opcode 0x01). Version-gated: on firmware that supports ModuleQuery (version J+), this sends the command and the reply is cached as data-only bytes per target. On older firmware, no command is sent; for index 1 (settings array) a default 16-byte payload is seeded in the cache so higher layers can decode default unit settings.
```

Reason: This contrast specifies ownership, mutation, framing, transport choice, cancellation, or physical-I/O boundaries rather than rhetorical emphasis. Preserve it.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WssClient.yml:516` — `summary` — `Wss.CoreModule.WssClient.ModuleQuery(System.Int32,Wss.CoreModule.WssTarget,System.Threading.CancellationToken)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\WssClient.cs`

### A088 — KEEP

Reader question: Does TryGetModuleQueryData return a cached response or issue a new query?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Gets the last cached ModuleQuery data (data-only slice) for a target, if present.
```

Reason: This contrast specifies ownership, mutation, framing, transport choice, cancellation, or physical-I/O boundaries rather than rhetorical emphasis. Preserve it.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WssClient.yml:237` — `summary` — `Wss.CoreModule.WssClient.TryGetModuleQueryData(Wss.CoreModule.WssTarget,System.Byte[]@)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\WssClient.cs`

### A090 — REWRITE

Reader question: Which lifecycle responsibilities does this controller own?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Host-agnostic wrapper around the full stimulation stack (core → params → model). Provides the same API surface as the Unity <code>Stimulation</code> MonoBehaviour but manages initialization, ticking, and shutdown inside a plain .NET application.
```

Reason: The lifecycle ownership is useful, but comparison with another wrapper makes the description harder to maintain and may imply broader API parity than the source warrants.

Suggested direction: Hosts the Core, parameter and model layers in a .NET application and manages initialization, background ticking and shutdown.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:72` — `summary` — `HFI.Wss.StimulationController`

Source XML-comment candidates (type-declaration match; verify revision/member): `CSharp\src\StimulationController.cs`

### A094 — KEEP

Reader question: If BASIC is unavailable, does Save throw, log, return false, or send a command?

Quoted field content (YAML block whitespace folded; markup retained):

```text
If the basic-stimulation API is unavailable, including before initialization, this method logs an error and returns without throwing.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:388` — `remarks` — `HFI.Wss.StimulationController.Save(System.Int32)`
- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:419` — `remarks` — `HFI.Wss.StimulationController.Save`
- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:443` — `remarks` — `HFI.Wss.StimulationController.load(System.Int32)`
- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:474` — `remarks` — `HFI.Wss.StimulationController.load`
- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:498` — `remarks` — `HFI.Wss.StimulationController.request_Configs(System.Int32,System.Int32,System.Int32)`
- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:535` — `remarks` — `HFI.Wss.StimulationController.updateWaveform(System.Int32[],System.Int32)`
- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:569` — `remarks` — `HFI.Wss.StimulationController.updateWaveform(System.Int32,System.Int32[],System.Int32)`
- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:606` — `remarks` — `HFI.Wss.StimulationController.updateWaveform(System.Int32,System.Int32,System.Int32)`
- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:643` — `remarks` — `HFI.Wss.StimulationController.updateWaveform(System.Int32,System.Int32,System.Int32,System.Int32)`
- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:683` — `remarks` — `HFI.Wss.StimulationController.updateWaveform(Wss.CoreModule.WaveformBuilder,System.Int32)`
- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:717` — `remarks` — `HFI.Wss.StimulationController.updateWaveform(System.Int32,Wss.CoreModule.WaveformBuilder,System.Int32)`
- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:754` — `remarks` — `HFI.Wss.StimulationController.loadWaveform(System.String,System.Int32)`
- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:788` — `remarks` — `HFI.Wss.StimulationController.WaveformSetup(Wss.CoreModule.WaveformBuilder,System.Int32)`
- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:822` — `remarks` — `HFI.Wss.StimulationController.WaveformSetup(System.Int32,Wss.CoreModule.WaveformBuilder,System.Int32)`
- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:859` — `remarks` — `HFI.Wss.StimulationController.UpdateIPD(System.Int32,System.Int32)`
- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:893` — `remarks` — `HFI.Wss.StimulationController.UpdateIPD(System.Int32,System.Int32,System.Int32)`

Source XML-comment candidates (type-declaration match; verify revision/member): `CSharp\src\StimulationController.cs`

### A095 — KEEP

Reader question: What happens when selectPort receives an omitted, null or empty value?

Quoted field content (YAML block whitespace folded; markup retained):

```text
If the dropdown contains only the <code>empty</code> placeholder, this method leaves the stimulation component disabled and does not change its configured port.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\unity\WSS.Unity.portScanner.yml:65` — `remarks` — `WSS.Unity.portScanner.selectPort`

Source XML-comment candidates (type-declaration match; verify revision/member): `Unity\UnityImplementation\portScanner.cs`

### A096 — KEEP

Reader question: Does GetModelParam throw when its requested key is absent?

Quoted field content (YAML block whitespace folded; markup retained):

```text
If the key does not exist.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.ModelModule.ModelConfigController.yml:264` — `description` — `Wss.ModelModule.ModelConfigController.GetModelParam``1(System.String)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\ModelModule\ModelConfigController.cs`

### A098 — KEEP

Reader question: What exception does SendAsync raise before a connection exists?

Quoted field content (YAML block whitespace folded; markup retained):

```text
If the transport is not connected.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.Transports.ITransport.yml:232` — `description` — `Wss.Transports.ITransport.SendAsync(System.Byte[],System.Threading.CancellationToken)`
- `DocsNavigation\apiDocs\api\core\Wss.Transports.SerialPortTransport.yml:273` — `description` — `Wss.Transports.SerialPortTransport.SendAsync(System.Byte[],System.Threading.CancellationToken)`
- `DocsNavigation\apiDocs\api\core\Wss.Transports.TestModeTransport.yml:430` — `description` — `Wss.Transports.TestModeTransport.SendAsync(System.Byte[],System.Threading.CancellationToken)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\Interfaces\ITransport.cs`, `Core\CoreModule\TestModeTransport.cs`, `Core\WSS.Transport.Serial\SerialPortTransport.cs`

### A099 — KEEP

Reader question: Which failures does ConnectAsync report as exceptions versus return values or logs?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Implementations may throw transport-specific exceptions (e.g., unauthorized port access, device not found).
```

Reason: This contrast specifies ownership, mutation, framing, transport choice, cancellation, or physical-I/O boundaries rather than rhetorical emphasis. Preserve it.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.Transports.ITransport.yml:158` — `description` — `Wss.Transports.ITransport.ConnectAsync(System.Threading.CancellationToken)`
- `DocsNavigation\apiDocs\api\core\Wss.Transports.SerialPortTransport.yml:193` — `description` — `Wss.Transports.SerialPortTransport.ConnectAsync(System.Threading.CancellationToken)`
- `DocsNavigation\apiDocs\api\core\Wss.Transports.TestModeTransport.yml:348` — `description` — `Wss.Transports.TestModeTransport.ConnectAsync(System.Threading.CancellationToken)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\Interfaces\ITransport.cs`, `Core\CoreModule\TestModeTransport.cs`, `Core\WSS.Transport.Serial\SerialPortTransport.cs`

### A100 — KEEP

Reader question: Does TestModeTransport communicate with a physical device or operate entirely in memory?

Quoted field content (YAML block whitespace folded; markup retained):

```text
In-memory transport for debugging and unit tests. No real I/O. Features: artificial latency, jitter, inbound chunking, and drop simulation. Default auto-responder validates checksum, flips sender/target, and echoes or replaces payload.
```

Reason: This contrast specifies ownership, mutation, framing, transport choice, cancellation, or physical-I/O boundaries rather than rhetorical emphasis. Preserve it.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.Transports.TestModeTransport.yml:33` — `summary` — `Wss.Transports.TestModeTransport`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\TestModeTransport.cs`

### A109 — KEEP

Reader question: How does LoadJson handle missing or invalid configuration: fallback, persistence or failure?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Loads the core configuration from disk at the specified <code class="paramref">path</code>. If <code class="paramref">path</code> is a directory, the file name <code>stimConfig.json</code> is appended. If the file does not exist or is invalid, a default configuration is created and saved. Safe to call multiple times; the operation is thread-safe.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.CoreConfigController.yml:185` — `summary` — `Wss.CoreModule.CoreConfigController.LoadJson(System.String)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\CoreJsonConfig\CoreConfigController.cs`

### A111 — KEEP

Reader question: Which fields or payload groups does MoveEventToSchedule accept, require together or omit?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Moves an event to a different schedule with a new delay offset. Fails on-device if <code class="paramref">delayMs</code> exceeds the schedule period.
```

Reason: The distinction changes caller behavior, payload interpretation, valid input, lifetime, capability handling or state. Removing it would lose contract information.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WssClient.yml:873` — `summary` — `Wss.CoreModule.WssClient.MoveEventToSchedule(System.Int32,System.Int32,System.Int32,Wss.CoreModule.WssTarget,System.Threading.CancellationToken)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\WssClient.cs`

### A113 — KEEP

Reader question: Does the None flag enable a finger switch?

Quoted field content (YAML block whitespace folded; markup retained):

```text
No fingerswitch.
```

Reason: The distinction changes caller behavior, payload interpretation, valid input, lifetime, capability handling or state. Removing it would lose contract information.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.ModuleSettings.FingerswitchConfig.yml:41` — `summary` — `Wss.CoreModule.ModuleSettings.FingerswitchConfig.None`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\ModuleSettings.cs`

### A114 — KEEP

Reader question: Which flags does Default enable?

Quoted field content (YAML block whitespace folded; markup retained):

```text
No special flags.
```

Reason: The distinction changes caller behavior, payload interpretation, valid input, lifetime, capability handling or state. Removing it would lose contract information.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.ModuleSettings.HwConfigFlags.yml:59` — `summary` — `Wss.CoreModule.ModuleSettings.HwConfigFlags.Default`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\ModuleSettings.cs`

### A115 — KEEP

Reader question: If BASIC is unavailable, does Save throw, log, return false, or send a command?

Quoted field content (YAML block whitespace folded; markup retained):

```text
No-op (logs an error) if basic stimulation is not supported.
```

Reason: Capability availability and error/no-op behavior affect safe callers. Keep the restriction and the observable behavior.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\unity\WSS.Unity.Stimulation.yml:343` — `remarks` — `WSS.Unity.Stimulation.Save(System.Int32)`
- `DocsNavigation\apiDocs\api\unity\WSS.Unity.Stimulation.yml:398` — `remarks` — `WSS.Unity.Stimulation.load(System.Int32)`
- `DocsNavigation\apiDocs\api\unity\WSS.Unity.Stimulation.yml:450` — `remarks` — `WSS.Unity.Stimulation.request_Configs(System.Int32,System.Int32,System.Int32)`
- `DocsNavigation\apiDocs\api\unity\WSS.Unity.Stimulation.yml:521` — `remarks` — `WSS.Unity.Stimulation.updateWaveform(System.Int32,System.Int32[],System.Int32)`
- `DocsNavigation\apiDocs\api\unity\WSS.Unity.Stimulation.yml:558` — `remarks` — `WSS.Unity.Stimulation.updateWaveform(System.Int32,System.Int32,System.Int32)`
- `DocsNavigation\apiDocs\api\unity\WSS.Unity.Stimulation.yml:592` — `remarks` — `WSS.Unity.Stimulation.updateWaveform(System.Int32,System.Int32,System.Int32,System.Int32)`
- `DocsNavigation\apiDocs\api\unity\WSS.Unity.Stimulation.yml:660` — `remarks` — `WSS.Unity.Stimulation.updateWaveform(System.Int32,Wss.CoreModule.WaveformBuilder,System.Int32)`
- `DocsNavigation\apiDocs\api\unity\WSS.Unity.Stimulation.yml:694` — `remarks` — `WSS.Unity.Stimulation.loadWaveform(System.String,System.Int32)`
- `DocsNavigation\apiDocs\api\unity\WSS.Unity.Stimulation.yml:725` — `remarks` — `WSS.Unity.Stimulation.WaveformSetup(Wss.CoreModule.WaveformBuilder,System.Int32)`
- `DocsNavigation\apiDocs\api\unity\WSS.Unity.Stimulation.yml:756` — `remarks` — `WSS.Unity.Stimulation.WaveformSetup(System.Int32,Wss.CoreModule.WaveformBuilder,System.Int32)`
- `DocsNavigation\apiDocs\api\unity\WSS.Unity.Stimulation.yml:827` — `remarks` — `WSS.Unity.Stimulation.UpdateIPD(System.Int32,System.Int32,System.Int32)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Unity\UnityImplementation\Stimulation.Commands.cs`, `Unity\UnityImplementation\Stimulation.cs`

### A116 — KEEP

Reader question: If BASIC is unavailable, does Save throw, log, return false, or send a command?

Quoted field content (YAML block whitespace folded; markup retained):

```text
No-op and logs an error when basic stimulation is unavailable from the wrapped core.
```

Reason: Capability availability and error/no-op behavior affect safe callers. Keep the restriction and the observable behavior.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\unity\WSS.Unity.StimulationParams.yml:334` — `remarks` — `WSS.Unity.StimulationParams.Save(System.Int32)`
- `DocsNavigation\apiDocs\api\unity\WSS.Unity.StimulationParams.yml:389` — `remarks` — `WSS.Unity.StimulationParams.load(System.Int32)`
- `DocsNavigation\apiDocs\api\unity\WSS.Unity.StimulationParams.yml:441` — `remarks` — `WSS.Unity.StimulationParams.request_Configs(System.Int32,System.Int32,System.Int32)`
- `DocsNavigation\apiDocs\api\unity\WSS.Unity.StimulationParams.yml:509` — `remarks` — `WSS.Unity.StimulationParams.updateWaveform(System.Int32,System.Int32[],System.Int32)`
- `DocsNavigation\apiDocs\api\unity\WSS.Unity.StimulationParams.yml:580` — `remarks` — `WSS.Unity.StimulationParams.updateWaveform(System.Int32,System.Int32,System.Int32,System.Int32)`
- `DocsNavigation\apiDocs\api\unity\WSS.Unity.StimulationParams.yml:648` — `remarks` — `WSS.Unity.StimulationParams.updateWaveform(System.Int32,Wss.CoreModule.WaveformBuilder,System.Int32)`
- `DocsNavigation\apiDocs\api\unity\WSS.Unity.StimulationParams.yml:682` — `remarks` — `WSS.Unity.StimulationParams.loadWaveform(System.String,System.Int32)`
- `DocsNavigation\apiDocs\api\unity\WSS.Unity.StimulationParams.yml:744` — `remarks` — `WSS.Unity.StimulationParams.WaveformSetup(System.Int32,Wss.CoreModule.WaveformBuilder,System.Int32)`
- `DocsNavigation\apiDocs\api\unity\WSS.Unity.StimulationParams.yml:815` — `remarks` — `WSS.Unity.StimulationParams.UpdateIPD(System.Int32,System.Int32,System.Int32)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Unity\UnityImplementation\StimualtionParams.cs`

### A119 — KEEP

Reader question: Is SerialPort used when I select BLE or Test transport?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Only used when <xref href="HFI.Wss.StimulationOptions.Transport" data-throw-if-not-resolved="false"></xref> is <xref href="HFI.Wss.StimulationTransportKind.Serial" data-throw-if-not-resolved="false"></xref>.
```

Reason: This contrast specifies ownership, mutation, framing, transport choice, cancellation, or physical-I/O boundaries rather than rhetorical emphasis. Preserve it.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationOptions.yml:72` — `remarks` — `HFI.Wss.StimulationOptions.SerialPort`

Source XML-comment candidates (type-declaration match; verify revision/member): `CSharp\src\StimulationController.cs`

### A123 — KEEP

Reader question: Can cancellation interrupt a write after transmission has begun?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Optional cancellation token to cancel the send before it is written. For some transports (e.g., synchronous serial writes), cancellation may only be observed before the write starts.
```

Reason: Capability availability and error/no-op behavior affect safe callers. Keep the restriction and the observable behavior.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.Transports.ITransport.yml:217` — `description` — `Wss.Transports.ITransport.SendAsync(System.Byte[],System.Threading.CancellationToken)`
- `DocsNavigation\apiDocs\api\core\Wss.Transports.SerialPortTransport.yml:258` — `description` — `Wss.Transports.SerialPortTransport.SendAsync(System.Byte[],System.Threading.CancellationToken)`
- `DocsNavigation\apiDocs\api\core\Wss.Transports.TestModeTransport.yml:415` — `description` — `Wss.Transports.TestModeTransport.SendAsync(System.Byte[],System.Threading.CancellationToken)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\Interfaces\ITransport.cs`, `Core\CoreModule\TestModeTransport.cs`, `Core\WSS.Transport.Serial\SerialPortTransport.cs`

### A128 — REWRITE

Reader question: What can I configure through these options?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Options that control transport selection, config paths, and tick behavior without exposing transport implementation details to the caller.
```

Reason: Listing caller-configurable settings answers the question; architectural concealment adds no actionable requirement here.

Suggested direction: Transport selection, configuration paths and background tick settings.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:161` — `description` — `HFI.Wss.StimulationController.#ctor(HFI.Wss.StimulationOptions)`

Source XML-comment candidates (type-declaration match; verify revision/member): `CSharp\src\StimulationController.cs`

### A131 — KEEP

Reader question: Can I mutate configuration values through ICoreConfig?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Read-only view of core configuration values loaded from a JSON config file.
```

Reason: This contrast specifies ownership, mutation, framing, transport choice, cancellation, or physical-I/O boundaries rather than rhetorical emphasis. Preserve it.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.ICoreConfig.yml:20` — `summary` — `Wss.CoreModule.ICoreConfig`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\Interfaces\ICoreConfig.cs`

### A133 — VERIFY THEN REWRITE

Reader question: Is this command usable with my firmware, and is a response decoded?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Requests specific battery and impedance data from the WSS device. TODO Not implemented in firmware, and reading from WSS devices is not yet implemented.
```

Reason: This combines an API restriction with temporary project-status prose. Check the final release implementation/firmware contract. Preserve any real unsupported-operation restriction; retire stale status wording only after verification.

Suggested direction: Describe the verified send/receive behavior and applicable firmware support. No supported capability or firmware threshold is inferred by this audit.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WssClient.yml:451` — `summary` — `Wss.CoreModule.WssClient.RequestAnalog(Wss.CoreModule.WssTarget,System.Threading.CancellationToken)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\WssClient.cs`

### A134 — KEEP

Reader question: How do I select an explicit device/port for NotifyCharacteristicUuid, and when is automatic selection used?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Required BLE notification characteristic UUID. Auto-select rejects devices that do not expose it with notify or indicate support.
```

Reason: This contrast specifies ownership, mutation, framing, transport choice, cancellation, or physical-I/O boundaries rather than rhetorical emphasis. Preserve it.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.Transports.BleNusTransportOptions.yml:153` — `summary` — `Wss.Transports.BleNusTransportOptions.NotifyCharacteristicUuid`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\WSS.Transport.BLE\BleNusTransportOptions.cs`

### A135 — KEEP

Reader question: How do I select an explicit device/port for ServiceUuid, and when is automatic selection used?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Required BLE service UUID. Auto-select only considers devices that expose this service.
```

Reason: This contrast specifies ownership, mutation, framing, transport choice, cancellation, or physical-I/O boundaries rather than rhetorical emphasis. Preserve it.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.Transports.BleNusTransportOptions.yml:104` — `summary` — `Wss.Transports.BleNusTransportOptions.ServiceUuid`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\WSS.Transport.BLE\BleNusTransportOptions.cs`

### A136 — KEEP

Reader question: How do I select an explicit device/port for WriteCharacteristicUuid, and when is automatic selection used?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Required BLE write characteristic UUID. Auto-select rejects devices that do not expose it with write-with-response support.
```

Reason: This contrast specifies ownership, mutation, framing, transport choice, cancellation, or physical-I/O boundaries rather than rhetorical emphasis. Preserve it.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.Transports.BleNusTransportOptions.yml:127` — `summary` — `Wss.Transports.BleNusTransportOptions.WriteCharacteristicUuid`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\WSS.Transport.BLE\BleNusTransportOptions.cs`

### A137 — KEEP

Reader question: Which logging sink becomes active after ResetSink?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Reset to the default behavior (Unity if present, otherwise Console/Debug).
```

Reason: This is a return-value/state or fallback contract. The alternatives tell callers how to branch and must remain.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.Log.yml:100` — `summary` — `Wss.CoreModule.Log.ResetSink`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\Support\Log.cs`

### A138 — REWRITE / VERIFY

Reader question: Is this a shared mutable reference, and which concurrent operations are safe?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Returned reference is live, not a copy and thread safe.
```

Reason: Live-reference ownership is useful. The phrase “not a copy and thread safe” has ambiguous coordination and an unverified concurrency guarantee. The audit must not supply the missing threading criterion.

Suggested direction: Returns the live configuration controller. Document its verified synchronization/access rules separately; do not infer that callers may mutate it concurrently.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CalibrationModule.StimParamsLayer.yml:1470` — `remarks` — `Wss.CalibrationModule.StimParamsLayer.GetCoreConfigController`
- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.IStimulationCore.yml:299` — `remarks` — `Wss.CoreModule.IStimulationCore.GetCoreConfigController`
- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WssStimulationCore.yml:737` — `remarks` — `Wss.CoreModule.WssStimulationCore.GetCoreConfigController`
- `DocsNavigation\apiDocs\api\core\Wss.ModelModule.ModelParamsLayer.yml:1599` — `remarks` — `Wss.ModelModule.ModelParamsLayer.GetCoreConfigController`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CalibrationModule\StimParamsLayer.cs`, `Core\CoreModule\Interfaces\IStimulationCore.cs`, `Core\CoreModule\WssStimulationCore.cs`, `Core\ModelModule\ModelParamsLayer.cs`

### A140 — VERIFY THEN REWRITE

Reader question: Is this command usable with my firmware, and is a response decoded?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Sends a request to the WSS target for a specific configuration type. This is a general-purpose request that can target output configs, events, schedules, and their various sub-configurations. Untested, and reading from WSS devices is not yet implemented.
```

Reason: This combines an API restriction with temporary project-status prose. Check the final release implementation/firmware contract. Preserve any real unsupported-operation restriction; retire stale status wording only after verification.

Suggested direction: Describe the verified send/receive behavior and applicable firmware support. No supported capability or firmware threshold is inferred by this audit.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WssClient.yml:568` — `summary` — `Wss.CoreModule.WssClient.RequestConfigs(System.Int32,System.Int32,Wss.CoreModule.WssTarget,System.Threading.CancellationToken)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\WssClient.cs`

### A141 — KEEP

Reader question: Does ZeroOutStim also change the cached channel values?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Sends a zero-out command. Does not alter cached values.
```

Reason: This contrast specifies ownership, mutation, framing, transport choice, cancellation, or physical-I/O boundaries rather than rhetorical emphasis. Preserve it.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CalibrationModule.StimParamsLayer.yml:1372` — `summary` — `Wss.CalibrationModule.StimParamsLayer.ZeroOutStim(Wss.CoreModule.WssTarget)`
- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.IStimulationCore.yml:206` — `summary` — `Wss.CoreModule.IStimulationCore.ZeroOutStim(Wss.CoreModule.WssTarget)`
- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WssStimulationCore.yml:328` — `summary` — `Wss.CoreModule.WssStimulationCore.ZeroOutStim(Wss.CoreModule.WssTarget)`
- `DocsNavigation\apiDocs\api\core\Wss.ModelModule.ModelParamsLayer.yml:1501` — `summary` — `Wss.ModelModule.ModelParamsLayer.ZeroOutStim(Wss.CoreModule.WssTarget)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CalibrationModule\StimParamsLayer.cs`, `Core\CoreModule\Interfaces\IStimulationCore.cs`, `Core\CoreModule\WssStimulationCore.cs`, `Core\ModelModule\ModelParamsLayer.cs`

### A142 — KEEP

Reader question: Which fields or payload groups does EditEventShape accept, require together or omit?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Shape ID used during the recharge phase (same mapping as <code class="paramref">standardShapeId</code>).
```

Reason: The distinction changes caller behavior, payload interpretation, valid input, lifetime, capability handling or state. Removing it would lose contract information.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WssClient.yml:1067` — `description` — `Wss.CoreModule.WssClient.EditEventShape(System.Int32,System.Int32,System.Int32,Wss.CoreModule.WssTarget,System.Threading.CancellationToken)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\WssClient.cs`

### A143 — KEEP

Reader question: If BASIC is unavailable, does updateWaveform throw, log, return false, or send a command?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Slots 0-10 are predefined. Slots 11-13 are custom. No-op and logs an error when basic stimulation is unavailable from the wrapped core.
```

Reason: Capability availability and error/no-op behavior affect safe callers. Keep the restriction and the observable behavior.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\unity\WSS.Unity.StimulationParams.yml:543` — `remarks` — `WSS.Unity.StimulationParams.updateWaveform(System.Int32,System.Int32,System.Int32)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Unity\UnityImplementation\StimualtionParams.cs`

### A144 — KEEP

Reader question: Which fields or payload groups does StreamChange accept, require together or omit?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Streams changes to PA (amplitude), PW (pulse width), and IPI (period) for up to three schedules in one shot. The grouped request may omit at most one of the three data groups. Uses opcodes: 0x30 = PA+PW+IPI, 0x31 = PA+PW (no IPI), 0x32 = PA+IPI (no PW), 0x33 = PW+IPI (no PA). All elements are 0..255 in this streaming variant (one byte each).
```

Reason: The distinction changes caller behavior, payload interpretation, valid input, lifetime, capability handling or state. Removing it would lose contract information.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WssClient.yml:1703` — `summary` — `Wss.CoreModule.WssClient.StreamChange(Wss.CoreModule.StreamChangeRequest,Wss.CoreModule.WssTarget,System.Threading.CancellationToken)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\WssClient.cs`

### A145 — KEEP

Reader question: Which fields or payload groups does ModuleSettings accept, require together or omit?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Strongly-typed snapshot of settings returned by a ModuleQuery-like payload. Includes a safe decoder for the data-only payload and helpers for selecting amplitude mapping behavior (e.g., 10 mA vs 72 mA based on HW flags).
```

Reason: This contrast specifies ownership, mutation, framing, transport choice, cancellation, or physical-I/O boundaries rather than rhetorical emphasis. Preserve it.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.ModuleSettings.yml:36` — `summary` — `Wss.CoreModule.ModuleSettings`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\ModuleSettings.cs`

### A146 — KEEP

Reader question: May the caller mutate a send buffer before SendAsync completes?

Quoted field content (YAML block whitespace folded; markup retained):

```text
The bytes to write. Callers should provide a buffer that will not be mutated during send.
```

Reason: This contrast specifies ownership, mutation, framing, transport choice, cancellation, or physical-I/O boundaries rather than rhetorical emphasis. Preserve it.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.Transports.ITransport.yml:214` — `description` — `Wss.Transports.ITransport.SendAsync(System.Byte[],System.Threading.CancellationToken)`
- `DocsNavigation\apiDocs\api\core\Wss.Transports.SerialPortTransport.yml:255` — `description` — `Wss.Transports.SerialPortTransport.SendAsync(System.Byte[],System.Threading.CancellationToken)`
- `DocsNavigation\apiDocs\api\core\Wss.Transports.TestModeTransport.yml:412` — `description` — `Wss.Transports.TestModeTransport.SendAsync(System.Byte[],System.Threading.CancellationToken)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\Interfaces\ITransport.cs`, `Core\CoreModule\TestModeTransport.cs`, `Core\WSS.Transport.Serial\SerialPortTransport.cs`

### A148 — KEEP

Reader question: Does #ctor disconnect, dispose owned resources or change the GameObject state?

Quoted field content (YAML block whitespace folded; markup retained):

```text
The core owns the lifetime of <code class="paramref">transport</code> and disposes it when the core is disposed. Call <xref href="Wss.CoreModule.WssStimulationCore.Shutdown" data-throw-if-not-resolved="false"></xref> to stop streaming/disconnect without disposing the transport.
```

Reason: This contrast specifies ownership, mutation, framing, transport choice, cancellation, or physical-I/O boundaries rather than rhetorical emphasis. Preserve it.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WssStimulationCore.yml:72` — `remarks` — `Wss.CoreModule.WssStimulationCore.#ctor(Wss.Transports.ITransport,Wss.CoreModule.WssStimulationCoreOptions)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\WssStimulationCore.cs`

### A149 — KEEP

Reader question: How does LoadObject``1 handle missing or invalid configuration: fallback, persistence or failure?

Quoted field content (YAML block whitespace folded; markup retained):

```text
The deserialized object, or <code class="paramref">defaults</code> when not present/invalid.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.JsonReader.yml:67` — `description` — `Wss.CoreModule.JsonReader.LoadObject``1(System.String,``0)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\CoreJsonConfig\JsonReader.cs`

### A150 — KEEP

Reader question: Which fields or payload groups does Frame accept, require together or omit?

Quoted field content (YAML block whitespace folded; markup retained):

```text
The protocol payload (e.g., <code>[cmd][len][...]</code>). The codec does not modify the payload.
```

Reason: This contrast specifies ownership, mutation, framing, transport choice, cancellation, or physical-I/O boundaries rather than rhetorical emphasis. Preserve it.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.IFrameCodec.yml:55` — `description` — `Wss.CoreModule.IFrameCodec.Frame(System.Byte,System.Byte,System.Byte[])`
- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WssFrameCodec.yml:67` — `description` — `Wss.CoreModule.WssFrameCodec.Frame(System.Byte,System.Byte,System.Byte[])`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\Interfaces\IFrameCodec.cs`, `Core\CoreModule\WssFrameCodec.cs`

### A152 — KEEP

Reader question: Does TryGetModuleQueryData return a defensive copy or the cached array?

Quoted field content (YAML block whitespace folded; markup retained):

```text
The returned array is the cached in-memory instance for the latest reply, not a defensive copy.
```

Reason: This contrast specifies ownership, mutation, framing, transport choice, cancellation, or physical-I/O boundaries rather than rhetorical emphasis. Preserve it.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WssClient.yml:238` — `remarks` — `Wss.CoreModule.WssClient.TryGetModuleQueryData(Wss.CoreModule.WssTarget,System.Byte[]@)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\WssClient.cs`

### A153 — KEEP

Reader question: Do AmpCurves array edits change live configuration, and how are they persisted?

Quoted field content (YAML block whitespace folded; markup retained):

```text
The returned array is the live in-memory configuration array, not a defensive copy. Mutating the returned array or its entries changes the loaded configuration until it is reloaded. Call <xref href="Wss.CoreModule.CoreConfigController.SaveJson" data-throw-if-not-resolved="false"></xref> to persist those changes.
```

Reason: This contrast specifies ownership, mutation, framing, transport choice, cancellation, or physical-I/O boundaries rather than rhetorical emphasis. Preserve it.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.CoreConfigController.yml:381` — `remarks` — `Wss.CoreModule.CoreConfigController.AmpCurves`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\CoreJsonConfig\CoreConfigController.cs`

### A156 — KEEP

Reader question: What happens when WssStimulationCoreOptions receives an omitted, null or empty value?

Quoted field content (YAML block whitespace folded; markup retained):

```text
These values affect configuration loading, setup retry behavior, and the default schedule/event values used during the core's initial device setup. Packet spacing is intentionally not exposed here because it remains a fixed hardware/radio constraint in the core implementation. The core constructor requires a non-empty <xref href="Wss.CoreModule.WssStimulationCoreOptions.ConfigPath" data-throw-if-not-resolved="false"></xref> and validates that <xref href="Wss.CoreModule.WssStimulationCoreOptions.MaxSetupTries" data-throw-if-not-resolved="false"></xref> is at least 1.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WssStimulationCoreOptions.yml:26` — `remarks` — `Wss.CoreModule.WssStimulationCoreOptions`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\WssStimulationCoreOptions.cs`

### A157 — KEEP

Reader question: How do I select an explicit device/port for Stimulation, and when is automatic selection used?

Quoted field content (YAML block whitespace folded; markup retained):

```text
This component composes <code>WssStimulationCore</code> -&gt; <code>StimParamsLayer</code> -&gt; <code>ModelParamsLayer</code> and exposes convenience methods that accept finger aliases (e.g., "index" or "ch2"). Lifecycle: the device is initialized in <xref href="Stimulation.OnEnable" data-throw-if-not-resolved="false"></xref>, ticked in <xref href="Stimulation.Update" data-throw-if-not-resolved="false"></xref>, and shut down in <xref href="Stimulation.OnDisable" data-throw-if-not-resolved="false"></xref>. When <xref href="Stimulation.forcePort" data-throw-if-not-resolved="false"></xref> is <code>false</code>, the wrapper auto-selects a serial port. When <xref href="Stimulation.testMode" data-throw-if-not-resolved="false"></xref> is <code>true</code>, it uses an in-memory test transport instead of hardware.
```

Reason: This is a return-value/state or fallback contract. The alternatives tell callers how to branch and must remain.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\unity\WSS.Unity.Stimulation.yml:69` — `remarks` — `WSS.Unity.Stimulation`

Source XML-comment candidates (type-declaration match; verify revision/member): `Unity\UnityImplementation\Stimulation.Commands.cs`, `Unity\UnityImplementation\Stimulation.cs`

### A158 — KEEP

Reader question: How do I select an explicit device/port for StimulationParams, and when is automatic selection used?

Quoted field content (YAML block whitespace folded; markup retained):

```text
This component composes <code>WssStimulationCore</code> -&gt; <code>StimParamsLayer</code> and exposes convenience methods that accept finger aliases (e.g., "index" or "ch2"). Lifecycle: the device is initialized in <xref href="StimulationParams.OnEnable" data-throw-if-not-resolved="false"></xref>, ticked in <xref href="StimulationParams.Update" data-throw-if-not-resolved="false"></xref>, and shut down in <xref href="StimulationParams.OnDisable" data-throw-if-not-resolved="false"></xref>. When <xref href="StimulationParams.forcePort" data-throw-if-not-resolved="false"></xref> is <code>false</code>, the wrapper auto-selects a serial port. When <xref href="StimulationParams.testMode" data-throw-if-not-resolved="false"></xref> is <code>true</code>, it uses an in-memory test transport instead of hardware.
```

Reason: This is a return-value/state or fallback contract. The alternatives tell callers how to branch and must remain.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\unity\WSS.Unity.StimulationParams.yml:66` — `remarks` — `WSS.Unity.StimulationParams`

Source XML-comment candidates (type-declaration match; verify revision/member): `Unity\UnityImplementation\StimualtionParams.cs`

### A159 — KEEP

Reader question: How do I select an explicit device/port for Stimulationbasic, and when is automatic selection used?

Quoted field content (YAML block whitespace folded; markup retained):

```text
This component wraps <code>WssStimulationCore</code> and forwards calls to <xref href="Wss.CoreModule.IStimulationCore" data-throw-if-not-resolved="false"></xref> and <xref href="Wss.CoreModule.IBasicStimulation" data-throw-if-not-resolved="false"></xref>. Lifecycle: the device is initialized in <xref href="Stimulationbasic.OnEnable" data-throw-if-not-resolved="false"></xref>, ticked in <xref href="Stimulationbasic.Update" data-throw-if-not-resolved="false"></xref>, and shut down in <xref href="Stimulationbasic.OnDisable" data-throw-if-not-resolved="false"></xref>. When <xref href="Stimulationbasic.forcePort" data-throw-if-not-resolved="false"></xref> is <code>false</code>, the wrapper auto-selects a serial port. When <xref href="Stimulationbasic.testMode" data-throw-if-not-resolved="false"></xref> is <code>true</code>, it uses an in-memory test transport instead of hardware.
```

Reason: This is a return-value/state or fallback contract. The alternatives tell callers how to branch and must remain.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\unity\WSS.Unity.Stimulationbasic.yml:47` — `remarks` — `WSS.Unity.Stimulationbasic`

Source XML-comment candidates (type-declaration match; verify revision/member): `Unity\UnityImplementation\StimulationBasic.cs`

### A160 — KEEP

Reader question: Can the started field disagree with Started(), and which state should I read?

Quoted field content (YAML block whitespace folded; markup retained):

```text
This is a controller-managed flag and may temporarily differ from <xref href="HFI.Wss.StimulationController.Started" data-throw-if-not-resolved="false"></xref>, which queries the underlying WSS stack for its current state.
```

Reason: The distinction changes caller behavior, payload interpretation, valid input, lifetime, capability handling or state. Removing it would lose contract information.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:105` — `remarks` — `HFI.Wss.StimulationController.started`

Source XML-comment candidates (type-declaration match; verify revision/member): `CSharp\src\StimulationController.cs`

### A161 — KEEP

Reader question: Can overlapping BleNusTransport calls perform concurrent GATT writes?

Quoted field content (YAML block whitespace folded; markup retained):

```text
This is a low-level byte transport only. Outbound messages are written with response to the NUS RX characteristic, inbound messages arrive as notifications from the NUS TX characteristic, and writes are serialized so only one GATT write is in flight at a time. Device selection follows <xref href="Wss.Transports.BleNusTransportOptions.AutoSelectDevice" data-throw-if-not-resolved="false"></xref>: when enabled, the transport scans for compatible BLE devices and chooses the best valid candidate; otherwise it requires an explicit <xref href="Wss.Transports.BleNusTransportOptions.DeviceId" data-throw-if-not-resolved="false"></xref> or <xref href="Wss.Transports.BleNusTransportOptions.DeviceName" data-throw-if-not-resolved="false"></xref>.
```

Reason: This is a return-value/state or fallback contract. The alternatives tell callers how to branch and must remain.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.Transports.BleNusTransport.yml:31` — `remarks` — `Wss.Transports.BleNusTransport`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\WSS.Transport.BLE\BleNusTransport.cs`

### A163 — KEEP

Reader question: What happens if I call or read Shutdown before initialization?

Quoted field content (YAML block whitespace folded; markup retained):

```text
This method blocks until the current tick loop exits and then shuts down and disposes the active WSS stack. It is safe to call multiple times; calls made before initialization return without error.
```

Reason: This is a return-value/state or fallback contract. The alternatives tell callers how to branch and must remain.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:228` — `remarks` — `HFI.Wss.StimulationController.Shutdown`

Source XML-comment candidates (type-declaration match; verify revision/member): `CSharp\src\StimulationController.cs`

### A164 — KEEP

Reader question: Does Dispose disconnect, dispose owned resources or change the GameObject state?

Quoted field content (YAML block whitespace folded; markup retained):

```text
This method performs a best-effort synchronous disconnect and logs disconnect failures instead of rethrowing them.
```

Reason: This contrast specifies ownership, mutation, framing, transport choice, cancellation, or physical-I/O boundaries rather than rhetorical emphasis. Preserve it.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.Transports.BleNusTransport.yml:365` — `remarks` — `Wss.Transports.BleNusTransport.Dispose`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\WSS.Transport.BLE\BleNusTransport.cs`

### A165 — KEEP

Reader question: What happens if I call or read resetRadio before initialization?

Quoted field content (YAML block whitespace folded; markup retained):

```text
This method stops the current tick loop, reinitializes the underlying WSS stack in place, and then starts ticking again. If the controller has not been initialized yet, this method does nothing.
```

Reason: This contrast specifies ownership, mutation, framing, transport choice, cancellation, or physical-I/O boundaries rather than rhetorical emphasis. Preserve it.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:276` — `remarks` — `HFI.Wss.StimulationController.resetRadio`

Source XML-comment candidates (type-declaration match; verify revision/member): `CSharp\src\StimulationController.cs`

### A166 — REWRITE

Reader question: Which request-field combinations are valid?

Quoted field content (YAML block whitespace folded; markup retained):

```text
This request supports the same payload shapes as the previous overload set: basic event, event with explicit shape IDs, event with amplitude and pulse-width data, or event with both shape and amplitude data. <xref href="Wss.CoreModule.CreateEventRequest.StandardShapeId" data-throw-if-not-resolved="false"></xref> and <xref href="Wss.CoreModule.CreateEventRequest.RechargeShapeId" data-throw-if-not-resolved="false"></xref> must be supplied together. <xref href="Wss.CoreModule.CreateEventRequest.StandardAmplitudes" data-throw-if-not-resolved="false"></xref> and <xref href="Wss.CoreModule.CreateEventRequest.RechargeAmplitudes" data-throw-if-not-resolved="false"></xref> must be supplied together. <xref href="Wss.CoreModule.CreateEventRequest.PulseWidths" data-throw-if-not-resolved="false"></xref> is required when amplitude arrays are supplied, and amplitude arrays are required when <xref href="Wss.CoreModule.CreateEventRequest.PulseWidths" data-throw-if-not-resolved="false"></xref> is supplied.
```

Reason: Keep all field-combination constraints; remove the historical overload-set comparison, which forces readers to consult an earlier API surface.

Suggested direction: List the supported field combinations directly, preserving paired-shape, paired-amplitude and pulse-width requirements wherever present.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.CreateEventRequest.yml:27` — `remarks` — `Wss.CoreModule.CreateEventRequest`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\CreateEventRequest.cs`

### A167 — KEEP

Reader question: Does this wrapper maintain started, or should I call Started()?

Quoted field content (YAML block whitespace folded; markup retained):

```text
This wrapper does not update this field; use <code>Started()</code> to query device state.
```

Reason: This contrast specifies ownership, mutation, framing, transport choice, cancellation, or physical-I/O boundaries rather than rhetorical emphasis. Preserve it.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\unity\WSS.Unity.Stimulation.yml:170` — `remarks` — `WSS.Unity.Stimulation.started`
- `DocsNavigation\apiDocs\api\unity\WSS.Unity.Stimulationbasic.yml:148` — `remarks` — `WSS.Unity.Stimulationbasic.started`
- `DocsNavigation\apiDocs\api\unity\WSS.Unity.StimulationParams.yml:167` — `remarks` — `WSS.Unity.StimulationParams.started`

Source XML-comment candidates (type-declaration match; verify revision/member): `Unity\UnityImplementation\StimualtionParams.cs`, `Unity\UnityImplementation\Stimulation.Commands.cs`, `Unity\UnityImplementation\Stimulation.cs`, `Unity\UnityImplementation\StimulationBasic.cs`

### A168 — KEEP

Reader question: If BASIC is unavailable, does SaveJson throw, log, return false, or send a command?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Threads safe, persists the current configuration to disk. No-op if not loaded yet.
```

Reason: Capability availability and error/no-op behavior affect safe callers. Keep the restriction and the observable behavior.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.CoreConfigController.yml:219` — `summary` — `Wss.CoreModule.CoreConfigController.SaveJson`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\CoreJsonConfig\CoreConfigController.cs`

### A170 — KEEP

Reader question: Which failures does DisconnectAsync report as exceptions versus return values or logs?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Thrown if not connected to the target.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WssClient.yml:216` — `description` — `Wss.CoreModule.WssClient.DisconnectAsync(System.Threading.CancellationToken)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\WssClient.cs`

### A180 — KEEP

Reader question: Which failures does SetFirmware report as exceptions versus return values or logs?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Thrown when <code class="paramref">v</code> is not supported by <code class="paramref">verHandler</code>.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.CoreConfigController.yml:465` — `description` — `Wss.CoreModule.CoreConfigController.SetFirmware(System.String,Wss.CoreModule.WSSVersionHandler)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\CoreJsonConfig\CoreConfigController.cs`

### A182 — KEEP

Reader question: What happens if I call or read StimulateAnalog before initialization?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Thrown when <xref href="HFI.Wss.StimulationController.Initialize" data-throw-if-not-resolved="false"></xref> has not been called.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:321` — `description` — `HFI.Wss.StimulationController.StimulateAnalog(System.String,System.Int32,System.Int32,System.Int32)`
- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:348` — `description` — `HFI.Wss.StimulationController.StartStimulation`
- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:372` — `description` — `HFI.Wss.StimulationController.StopStimulation`
- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:945` — `description` — `HFI.Wss.StimulationController.StimulateNormalized(System.String,System.Single)`
- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:979` — `description` — `HFI.Wss.StimulationController.GetStimIntensity(System.String)`
- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:1006` — `description` — `HFI.Wss.StimulationController.SaveParamsJson`
- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:1030` — `description` — `HFI.Wss.StimulationController.LoadParamsJson`
- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:1058` — `description` — `HFI.Wss.StimulationController.LoadParamsJson(System.String)`
- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:1092` — `description` — `HFI.Wss.StimulationController.AddOrUpdateStimParam(System.String,System.Single)`
- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:1126` — `description` — `HFI.Wss.StimulationController.GetStimParam(System.String)`
- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:1163` — `description` — `HFI.Wss.StimulationController.TryGetStimParam(System.String,System.Single@)`
- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:1193` — `description` — `HFI.Wss.StimulationController.GetAllStimParams`
- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:1224` — `description` — `HFI.Wss.StimulationController.SetChannelAmp(System.String,System.Single)`
- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:1258` — `description` — `HFI.Wss.StimulationController.SetChannelPWMin(System.String,System.Int32)`
- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:1292` — `description` — `HFI.Wss.StimulationController.SetChannelPWMax(System.String,System.Int32)`
- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:1326` — `description` — `HFI.Wss.StimulationController.SetChannelIPI(System.String,System.Int32)`
- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:1360` — `description` — `HFI.Wss.StimulationController.GetChannelAmp(System.String)`
- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:1394` — `description` — `HFI.Wss.StimulationController.GetChannelPWMin(System.String)`
- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:1428` — `description` — `HFI.Wss.StimulationController.GetChannelPWMax(System.String)`
- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:1462` — `description` — `HFI.Wss.StimulationController.GetChannelIPI(System.String)`
- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:1496` — `description` — `HFI.Wss.StimulationController.IsFingerValid(System.String)`
- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:1530` — `description` — `HFI.Wss.StimulationController.StimWithMode(System.String,System.Single)`
- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:1570` — `description` — `HFI.Wss.StimulationController.UpdateChannelParams(System.String,System.Int32,System.Int32,System.Int32)`
- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:1603` — `description` — `HFI.Wss.StimulationController.isModeValid`
- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:1676` — `description` — `HFI.Wss.StimulationController.GetModelConfigCTRL`
- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:1703` — `description` — `HFI.Wss.StimulationController.GetCoreConfigCTRL`
- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:1727` — `description` — `HFI.Wss.StimulationController.LoadCoreConfigFile`

Source XML-comment candidates (type-declaration match; verify revision/member): `CSharp\src\StimulationController.cs`

### A184 — KEEP

Reader question: Which failures does StreamChange report as exceptions versus return values or logs?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Thrown when a supplied group does not contain exactly 3 elements.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WssClient.yml:1738` — `description` — `Wss.CoreModule.WssClient.StreamChange(Wss.CoreModule.StreamChangeRequest,Wss.CoreModule.WssTarget,System.Threading.CancellationToken)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\WssClient.cs`

### A186 — KEEP

Reader question: What happens when CreateContactConfig receives an omitted, null or empty value?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Thrown when either setup array in <code class="paramref">definition</code> is null or does not contain exactly four entries.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WssClient.yml:666` — `description` — `Wss.CoreModule.WssClient.CreateContactConfig(Wss.CoreModule.ContactConfigDefinition,Wss.CoreModule.WssTarget,System.Threading.CancellationToken)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\WssClient.cs`

### A187 — KEEP

Reader question: Which failures does ConnectAsync report as exceptions versus return values or logs?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Thrown when no compatible BLE device can be resolved or the required service and characteristics are unavailable.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.Transports.BleNusTransport.yml:262` — `description` — `Wss.Transports.BleNusTransport.ConnectAsync(System.Threading.CancellationToken)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\WSS.Transport.BLE\BleNusTransport.cs`

### A188 — KEEP

Reader question: Which failures does #ctor report as exceptions versus return values or logs?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Thrown when the configured response timeout is not positive.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WssClient.yml:145` — `description` — `Wss.CoreModule.WssClient.#ctor(Wss.Transports.ITransport,Wss.CoreModule.IFrameCodec,Wss.CoreModule.WSSVersionHandler,Wss.CoreModule.WssClientOptions)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\WssClient.cs`

### A189 — KEEP

Reader question: How do I select an explicit device/port for #ctor, and when is automatic selection used?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Thrown when the port selection settings specify both an explicit port and auto-selection, or neither.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.Transports.SerialPortTransport.yml:83` — `description` — `Wss.Transports.SerialPortTransport.#ctor(Wss.Transports.SerialPortTransportOptions)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\WSS.Transport.Serial\SerialPortTransport.cs`

### A190 — KEEP

Reader question: Which fields or payload groups does CreateEvent accept, require together or omit?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Thrown when the request mixes incompatible combinations, such as only one shape ID, only one amplitude array, or pulse widths without both amplitude arrays.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WssClient.yml:747` — `description` — `Wss.CoreModule.WssClient.CreateEvent(Wss.CoreModule.CreateEventRequest,Wss.CoreModule.WssTarget,System.Threading.CancellationToken)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\WssClient.cs`

### A192 — KEEP

Reader question: Which failures does SendAsync report as exceptions versus return values or logs?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Thrown when the transport is not connected.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.Transports.BleNusTransport.yml:341` — `description` — `Wss.Transports.BleNusTransport.SendAsync(System.Byte[],System.Threading.CancellationToken)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\WSS.Transport.BLE\BleNusTransport.cs`

### A193 — KEEP

Reader question: Which state or result makes TryGetModuleSettings true or false, and can output data still be present?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Tries to obtain the last decoded ModuleSettings for a target by reading the client's cached ModuleQuery data and decoding it. Returns false if no data is cached or the payload is incomplete; when false, 'settings' may still be non-null but marked as partial.
```

Reason: This is a return-value/state or fallback contract. The alternatives tell callers how to branch and must remain.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WssStimulationCore.yml:825` — `summary` — `Wss.CoreModule.WssStimulationCore.TryGetModuleSettings(Wss.CoreModule.WssTarget,Wss.CoreModule.ModuleSettings@)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\WssStimulationCore.cs`

### A194 — KEEP

Reader question: Which state or result makes TryGetModuleQueryData true or false, and can output data still be present?

Quoted field content (YAML block whitespace folded; markup retained):

```text
True if cached data exists; otherwise false.
```

Reason: This is a return-value/state or fallback contract. The alternatives tell callers how to branch and must remain.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WssClient.yml:251` — `description` — `Wss.CoreModule.WssClient.TryGetModuleQueryData(Wss.CoreModule.WssTarget,System.Byte[]@)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\WssClient.cs`

### A195 — KEEP

Reader question: Which state or result makes TryDecode true or false, and can output data still be present?

Quoted field content (YAML block whitespace folded; markup retained):

```text
True when all expected fields were present (not partial). False indicates a short payload, but a best-effort decode is still returned in <code class="paramref">settings</code>.
```

Reason: This is a return-value/state or fallback contract. The alternatives tell callers how to branch and must remain.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.ModuleSettings.yml:446` — `description` — `Wss.CoreModule.ModuleSettings.TryDecode(System.ReadOnlySpan{System.Byte},Wss.CoreModule.ModuleSettings@)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\ModuleSettings.cs`

### A196 — KEEP

Reader question: Which state or result makes ProbeSupported true or false, and can output data still be present?

Quoted field content (YAML block whitespace folded; markup retained):

```text
True when the values originated from a real device probe, not defaults.
```

Reason: This is a return-value/state or fallback contract. The alternatives tell callers how to branch and must remain.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.ModuleSettings.yml:361` — `summary` — `Wss.CoreModule.ModuleSettings.ProbeSupported`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\ModuleSettings.cs`

### A197 — KEEP

Reader question: Which failures does TryGetModelParam``1 report as exceptions versus return values or logs?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Try to get a typed value without throwing.
```

Reason: Capability availability and error/no-op behavior affect safe callers. Keep the restriction and the observable behavior.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.ModelModule.ModelConfigController.yml:282` — `summary` — `Wss.ModelModule.ModelConfigController.TryGetModelParam``1(System.String,``0@)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\ModelModule\ModelConfigController.cs`

### A198 — KEEP

Reader question: Does portScanner disconnect, dispose owned resources or change the GameObject state?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Usage: - Place this script on a separate GameObject (not a child of the stimulation object). - Assign <xref href="portScanner.stim" data-throw-if-not-resolved="false"></xref> and <xref href="portScanner.serialList" data-throw-if-not-resolved="false"></xref> in the inspector. - Ensure the stimulation script has <code>forcePort</code> enabled so the dropdown selection is used. Enabling this component temporarily disables <xref href="portScanner.stim" data-throw-if-not-resolved="false"></xref> while scanning ports so the stimulation wrapper does not hold an outdated connection open during selection.
```

Reason: This contrast specifies ownership, mutation, framing, transport choice, cancellation, or physical-I/O boundaries rather than rhetorical emphasis. Preserve it.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\unity\WSS.Unity.portScanner.yml:27` — `remarks` — `WSS.Unity.portScanner`

Source XML-comment candidates (type-declaration match; verify revision/member): `Unity\UnityImplementation\portScanner.cs`

### A202 — KEEP

Reader question: Does GetRequired return a fallback or throw for an absent key?

Quoted field content (YAML block whitespace folded; markup retained):

```text
When the key is not present.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.ModelModule.ModelConfig.yml:202` — `description` — `Wss.ModelModule.ModelConfig.GetRequired``1(System.String)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\ModelModule\ModelConfig.cs`

### A203 — KEEP

Reader question: How do I select an explicit device/port for AutoSelectDevice, and when is automatic selection used?

Quoted field content (YAML block whitespace folded; markup retained):

```text
When true, scans for compatible BLE devices and auto-selects the best valid candidate. Auto-selection only accepts devices that expose the configured service and both required characteristics with the expected properties, then prefers stronger RSSI. When false, the caller must provide <xref href="Wss.Transports.BleNusTransportOptions.DeviceId" data-throw-if-not-resolved="false"></xref> or <xref href="Wss.Transports.BleNusTransportOptions.DeviceName" data-throw-if-not-resolved="false"></xref>.
```

Reason: This is a return-value/state or fallback contract. The alternatives tell callers how to branch and must remain.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.Transports.BleNusTransportOptions.yml:202` — `summary` — `Wss.Transports.BleNusTransportOptions.AutoSelectDevice`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\WSS.Transport.BLE\BleNusTransportOptions.cs`

### A205 — KEEP

Reader question: Can overlapping SendAsync calls perform concurrent GATT writes?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Writes are serialized so only one GATT write is in flight at a time.
```

Reason: This contrast specifies ownership, mutation, framing, transport choice, cancellation, or physical-I/O boundaries rather than rhetorical emphasis. Preserve it.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.Transports.BleNusTransport.yml:316` — `remarks` — `Wss.Transports.BleNusTransport.SendAsync(System.Byte[],System.Threading.CancellationToken)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\WSS.Transport.BLE\BleNusTransport.cs`

## Appendix: neutral constraints and lexical candidates

### A032 — KEEP

Reader question: Does StopStimulation update the controller flag as well as send the stop request?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Broadcasts a stop-stimulation command and marks <xref href="HFI.Wss.StimulationController.started" data-throw-if-not-resolved="false"></xref> as false.
```

Reason: This is a return-value/state or fallback contract. The alternatives tell callers how to branch and must remain.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:363` — `summary` — `HFI.Wss.StimulationController.StopStimulation`

Source XML-comment candidates (type-declaration match; verify revision/member): `CSharp\src\StimulationController.cs`

### A037 — KEEP

Reader question: How does #ctor handle missing or invalid configuration: fallback, persistence or failure?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Constructs a new stimulation parameter controller using the provided JSON path and max WSS value. Automatically creates default channel entries if missing.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CalibrationModule.StimParamsConfigController.yml:142` — `summary` — `Wss.CalibrationModule.StimParamsConfigController.#ctor(System.String,System.Int32)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CalibrationModule\StimParamsConfigController.cs`

### A038 — KEEP

Reader question: Which fields or payload groups does CreateContactConfig accept, require together or omit?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Contact configuration values to encode and send, including the contact id, four-output stimulation setup, four-output recharge setup, and optional LED bitmask.
```

Reason: Capability availability and error/no-op behavior affect safe callers. Keep the restriction and the observable behavior.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WssClient.yml:645` — `description` — `Wss.CoreModule.WssClient.CreateContactConfig(Wss.CoreModule.ContactConfigDefinition,Wss.CoreModule.WssTarget,System.Threading.CancellationToken)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\WssClient.cs`

### A039 — KEEP

Reader question: Which fields or payload groups does StimParamsBasicExtensions accept, require together or omit?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Convenience extension methods over <xref href="Wss.CalibrationModule.IStimParamsCore" data-throw-if-not-resolved="false"></xref> that forward to the optional BASIC layer (<xref href="Wss.CoreModule.IBasicStimulation" data-throw-if-not-resolved="false"></xref>).
```

Reason: Capability availability and error/no-op behavior affect safe callers. Keep the restriction and the observable behavior.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.StimParamsBasicExtensions.yml:27` — `summary` — `Wss.CoreModule.StimParamsBasicExtensions`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\SideFunctions\StimParamsBasicExtensions.cs`

### A042 — KEEP

Reader question: Which fields or payload groups does IStimParamsCore accept, require together or omit?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Core + parameters surface layered over <xref href="Wss.CoreModule.IStimulationCore" data-throw-if-not-resolved="false"></xref>. Provides normalized-drive stimulation, per-channel parameter access, and JSON persistence for stimulation parameters. BASIC is optional and can be exposed via <xref href="Wss.CalibrationModule.IStimParamsCore.TryGetBasic(Wss.CoreModule.IBasicStimulation%40)" data-throw-if-not-resolved="false"></xref>.
```

Reason: Capability availability and error/no-op behavior affect safe callers. Keep the restriction and the observable behavior.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CalibrationModule.IStimParamsCore.yml:52` — `summary` — `Wss.CalibrationModule.IStimParamsCore`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CalibrationModule\IStimParamsCore.cs`

### A045 — KEEP

Reader question: Which transport or parameter behavior does the selected Awake mode enable?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Creates the transport, then the full stimulation stack: core -&gt; params layer -&gt; model layer. Detects hardware if <code>forcePort</code> is false.
```

Reason: This is a return-value/state or fallback contract. The alternatives tell callers how to branch and must remain.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\unity\WSS.Unity.Stimulation.yml:191` — `summary` — `WSS.Unity.Stimulation.Awake`

Source XML-comment candidates (type-declaration match; verify revision/member): `Unity\UnityImplementation\Stimulation.Commands.cs`, `Unity\UnityImplementation\Stimulation.cs`

### A047 — KEEP

Reader question: How does LoadObject``1 handle missing or invalid configuration: fallback, persistence or failure?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Default instance to persist and return when missing/invalid.
```

Reason: This is a return-value/state or fallback contract. The alternatives tell callers how to branch and must remain.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.JsonReader.yml:61` — `description` — `Wss.CoreModule.JsonReader.LoadObject``1(System.String,``0)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\CoreJsonConfig\JsonReader.cs`

### A049 — KEEP

Reader question: How does LoadJObject handle missing or invalid configuration: fallback, persistence or failure?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Default JSON object to persist and return when missing.
```

Reason: This is a return-value/state or fallback contract. The alternatives tell callers how to branch and must remain.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.JsonReader.yml:136` — `description` — `Wss.CoreModule.JsonReader.LoadJObject(System.String,Newtonsoft.Json.Linq.JObject)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\CoreJsonConfig\JsonReader.cs`

### A056 — KEEP

Reader question: What happens when #ctor receives an omitted, null or empty value?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Exactly one port selection mode must be configured: either set <xref href="Wss.Transports.SerialPortTransportOptions.AutoSelectPort" data-throw-if-not-resolved="false"></xref> to <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> or provide a non-empty <xref href="Wss.Transports.SerialPortTransportOptions.PortName" data-throw-if-not-resolved="false"></xref>.
```

Reason: This is a return-value/state or fallback contract. The alternatives tell callers how to branch and must remain.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.Transports.SerialPortTransport.yml:64` — `remarks` — `Wss.Transports.SerialPortTransport.#ctor(Wss.Transports.SerialPortTransportOptions)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\WSS.Transport.Serial\SerialPortTransport.cs`

### A057 — KEEP

Reader question: What happens when SerialPortTransportOptions receives an omitted, null or empty value?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Exactly one port selection mode must be used: either set <xref href="Wss.Transports.SerialPortTransportOptions.AutoSelectPort" data-throw-if-not-resolved="false"></xref> to true, or provide a non-empty <xref href="Wss.Transports.SerialPortTransportOptions.PortName" data-throw-if-not-resolved="false"></xref>.
```

Reason: This is a return-value/state or fallback contract. The alternatives tell callers how to branch and must remain.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.Transports.SerialPortTransportOptions.yml:26` — `remarks` — `Wss.Transports.SerialPortTransportOptions`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\WSS.Transport.Serial\SerialPortTransportOptions.cs`

### A058 — KEEP

Reader question: Which fields or payload groups does TryGetBasic accept, require together or omit?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Exposes the optional BASIC capability if available from the wrapped core. Returns <code>true</code> and sets <code class="paramref">basic</code> if supported.
```

Reason: This is a return-value/state or fallback contract. The alternatives tell callers how to branch and must remain.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CalibrationModule.IStimParamsCore.yml:1067` — `summary` — `Wss.CalibrationModule.IStimParamsCore.TryGetBasic(Wss.CoreModule.IBasicStimulation@)`
- `DocsNavigation\apiDocs\api\core\Wss.CalibrationModule.StimParamsLayer.yml:1176` — `summary` — `Wss.CalibrationModule.StimParamsLayer.TryGetBasic(Wss.CoreModule.IBasicStimulation@)`
- `DocsNavigation\apiDocs\api\core\Wss.ModelModule.ModelParamsLayer.yml:1305` — `summary` — `Wss.ModelModule.ModelParamsLayer.TryGetBasic(Wss.CoreModule.IBasicStimulation@)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CalibrationModule\IStimParamsCore.cs`, `Core\CalibrationModule\StimParamsLayer.cs`, `Core\ModelModule\ModelParamsLayer.cs`

### A059 — KEEP

Reader question: How does GetOrDefault``1 handle missing or invalid configuration: fallback, persistence or failure?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Fallback value to return when the key is missing.
```

Reason: This is a return-value/state or fallback contract. The alternatives tell callers how to branch and must remain.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.ModelModule.ModelConfigController.yml:337` — `description` — `Wss.ModelModule.ModelConfigController.GetOrDefault``1(System.String,``0)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\ModelModule\ModelConfigController.cs`

### A060 — KEEP

Reader question: How does GetOrDefault``1 handle missing or invalid configuration: fallback, persistence or failure?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Fallback value when the key is absent.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.ModelModule.ModelConfig.yml:159` — `description` — `Wss.ModelModule.ModelConfig.GetOrDefault``1(System.String,``0)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\ModelModule\ModelConfig.cs`

### A065 — KEEP

Reader question: How does GetCalibMode handle missing or invalid configuration: fallback, persistence or failure?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Get the modelâ€™s calibration mode from <code>"calib.mode"</code>. Throws if missing.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.ModelModule.ModelConfigController.yml:363` — `summary` — `Wss.ModelModule.ModelConfigController.GetCalibMode`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\ModelModule\ModelConfigController.cs`

### A066 — KEEP

Reader question: How does GetFloat handle missing or invalid configuration: fallback, persistence or failure?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Gets a float or returns a default when missing.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.DictConfigBase.yml:293` — `summary` — `Wss.CoreModule.DictConfigBase.GetFloat(System.String,System.Single)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\CoreJsonConfig\DictConfigBase.cs`

### A067 — KEEP

Reader question: How does GetRequired``1 handle missing or invalid configuration: fallback, persistence or failure?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Gets a required value at <code class="paramref">key</code>. Throws if missing or invalid.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.ModelModule.ModelConfig.yml:184` — `summary` — `Wss.ModelModule.ModelConfig.GetRequired``1(System.String)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\ModelModule\ModelConfig.cs`

### A068 — KEEP

Reader question: How does GetString handle missing or invalid configuration: fallback, persistence or failure?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Gets a string or returns a default when missing.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.DictConfigBase.yml:353` — `summary` — `Wss.CoreModule.DictConfigBase.GetString(System.String,System.String)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\CoreJsonConfig\DictConfigBase.cs`

### A069 — KEEP

Reader question: How does GetInt handle missing or invalid configuration: fallback, persistence or failure?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Gets an int or returns a default when missing.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.DictConfigBase.yml:323` — `summary` — `Wss.CoreModule.DictConfigBase.GetInt(System.String,System.Int32)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\CoreJsonConfig\DictConfigBase.cs`

### A070 — KEEP

Reader question: What happens when AutoResponderAsync receives an omitted, null or empty value?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Gets or sets an optional override auto-responder. When null, the built-in responder is used.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.Transports.TestModeTransportOptions.yml:197` — `summary` — `Wss.Transports.TestModeTransportOptions.AutoResponderAsync`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\TestModeTransportOptions.cs`

### A071 — KEEP

Reader question: Must I provide PortName when automatic selection is disabled?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Gets or sets the explicit port name. Use values such as <code>COM5</code> on Windows or <code>/dev/ttyUSB0</code> on Linux/macOS. This must be provided when <xref href="Wss.Transports.SerialPortTransportOptions.AutoSelectPort" data-throw-if-not-resolved="false"></xref> is false.
```

Reason: This is a return-value/state or fallback contract. The alternatives tell callers how to branch and must remain.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.Transports.SerialPortTransportOptions.yml:50` — `summary` — `Wss.Transports.SerialPortTransportOptions.PortName`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\WSS.Transport.Serial\SerialPortTransportOptions.cs`

### A072 — KEEP

Reader question: What retry count is valid for MaxSetupTries?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Gets or sets the maximum number of setup retries before the core enters the Error state. This must be at least 1. The default is 5.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WssStimulationCoreOptions.yml:82` — `summary` — `Wss.CoreModule.WssStimulationCoreOptions.MaxSetupTries`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\WssStimulationCoreOptions.cs`

### A073 — KEEP

Reader question: What values are valid for ResponseTimeout?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Gets or sets the maximum time to wait for a correlated reply before canceling the request. This must be greater than zero. The default is 2 seconds.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WssClientOptions.yml:158` — `summary` — `Wss.CoreModule.WssClientOptions.ResponseTimeout`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\WssClientOptions.cs`

### A074 — KEEP

Reader question: Which fields or payload groups does RechargeAmplitudes accept, require together or omit?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Gets or sets the optional four-element amplitude array for the recharge phase. Must be supplied together with <xref href="Wss.CoreModule.CreateEventRequest.StandardAmplitudes" data-throw-if-not-resolved="false"></xref> and <xref href="Wss.CoreModule.CreateEventRequest.PulseWidths" data-throw-if-not-resolved="false"></xref>.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.CreateEventRequest.yml:208` — `summary` — `Wss.CoreModule.CreateEventRequest.RechargeAmplitudes`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\CreateEventRequest.cs`

### A075 — KEEP

Reader question: Which fields or payload groups does StandardAmplitudes accept, require together or omit?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Gets or sets the optional four-element amplitude array for the standard phase. Must be supplied together with <xref href="Wss.CoreModule.CreateEventRequest.RechargeAmplitudes" data-throw-if-not-resolved="false"></xref> and <xref href="Wss.CoreModule.CreateEventRequest.PulseWidths" data-throw-if-not-resolved="false"></xref>.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.CreateEventRequest.yml:182` — `summary` — `Wss.CoreModule.CreateEventRequest.StandardAmplitudes`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\CreateEventRequest.cs`

### A076 — KEEP

Reader question: What happens when Leds receives an omitted, null or empty value?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Gets or sets the optional LED bitmask. Leave null to omit LED configuration from the command payload.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.ContactConfigDefinition.yml:132` — `summary` — `Wss.CoreModule.ContactConfigDefinition.Leds`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\ContactConfigDefinition.cs`

### A077 — KEEP

Reader question: Which fields or payload groups does PulseWidths accept, require together or omit?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Gets or sets the optional pulse-width payload. This is required when amplitude arrays are supplied.
```

Reason: Capability availability and error/no-op behavior affect safe callers. Keep the restriction and the observable behavior.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.CreateEventRequest.yml:234` — `summary` — `Wss.CoreModule.CreateEventRequest.PulseWidths`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\CreateEventRequest.cs`

### A078 — KEEP

Reader question: Which fields or payload groups does RechargeShapeId accept, require together or omit?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Gets or sets the optional recharge-phase shape ID. Must be supplied together with <xref href="Wss.CoreModule.CreateEventRequest.StandardShapeId" data-throw-if-not-resolved="false"></xref>.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.CreateEventRequest.yml:156` — `summary` — `Wss.CoreModule.CreateEventRequest.RechargeShapeId`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\CreateEventRequest.cs`

### A079 — KEEP

Reader question: Which fields or payload groups does StandardShapeId accept, require together or omit?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Gets or sets the optional standard-phase shape ID. Must be supplied together with <xref href="Wss.CoreModule.CreateEventRequest.RechargeShapeId" data-throw-if-not-resolved="false"></xref>.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.CreateEventRequest.yml:130` — `summary` — `Wss.CoreModule.CreateEventRequest.StandardShapeId`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\CreateEventRequest.cs`

### A080 — KEEP

Reader question: What happens when FallbackPayload receives an omitted, null or empty value?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Gets or sets the payload used when an incoming checksum is invalid. When null, <xref href="Wss.Transports.TestModeTransport" data-throw-if-not-resolved="false"></xref> uses a default fallback payload.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.Transports.TestModeTransportOptions.yml:171` — `summary` — `Wss.Transports.TestModeTransportOptions.FallbackPayload`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\TestModeTransportOptions.cs`

### A081 — KEEP

Reader question: What happens when Rng receives an omitted, null or empty value?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Gets or sets the random generator used for jitter, chunk sizing, drops, and synthetic replies. When null, <xref href="Wss.Transports.TestModeTransport" data-throw-if-not-resolved="false"></xref> creates a default instance.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.Transports.TestModeTransportOptions.yml:145` — `summary` — `Wss.Transports.TestModeTransportOptions.Rng`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\TestModeTransportOptions.cs`

### A082 — KEEP

Reader question: What happens when InterPulseIntervals receives an omitted, null or empty value?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Gets or sets the three inter-pulse intervals, in milliseconds (0..255). Leave null to omit the IPI group.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.StreamChangeRequest.yml:100` — `summary` — `Wss.CoreModule.StreamChangeRequest.InterPulseIntervals`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\StreamChangeRequest.cs`

### A083 — KEEP

Reader question: What happens when PulseAmplitudes receives an omitted, null or empty value?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Gets or sets the three pulse amplitudes (0..255). Leave null to omit the PA group.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.StreamChangeRequest.yml:48` — `summary` — `Wss.CoreModule.StreamChangeRequest.PulseAmplitudes`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\StreamChangeRequest.cs`

### A084 — KEEP

Reader question: What happens when PulseWidths receives an omitted, null or empty value?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Gets or sets the three pulse widths (0..255). Leave null to omit the PW group.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.StreamChangeRequest.yml:74` — `summary` — `Wss.CoreModule.StreamChangeRequest.PulseWidths`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\StreamChangeRequest.cs`

### A085 — KEEP

Reader question: What happens when AutoSelectPort receives an omitted, null or empty value?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Gets or sets whether the transport should automatically select the best available serial port. When true, <xref href="Wss.Transports.SerialPortTransportOptions.PortName" data-throw-if-not-resolved="false"></xref> must be left empty.
```

Reason: This is a return-value/state or fallback contract. The alternatives tell callers how to branch and must remain.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.Transports.SerialPortTransportOptions.yml:78` — `summary` — `Wss.Transports.SerialPortTransportOptions.AutoSelectPort`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\WSS.Transport.Serial\SerialPortTransportOptions.cs`

### A087 — KEEP

Reader question: How does GetChannelAmpMode handle missing or invalid configuration: fallback, persistence or failure?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Gets the configured amplitude-control mode, warning and falling back to PW when invalid.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CalibrationModule.StimParamsConfigController.yml:816` — `summary` — `Wss.CalibrationModule.StimParamsConfigController.GetChannelAmpMode(System.Int32)`
- `DocsNavigation\apiDocs\api\core\Wss.CalibrationModule.StimParamsLayer.yml:1020` — `summary` — `Wss.CalibrationModule.StimParamsLayer.GetChannelAmpMode(System.Int32)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CalibrationModule\StimParamsConfigController.cs`, `Core\CalibrationModule\StimParamsLayer.cs`

### A089 — KEEP

Reader question: How does GetModeRequired handle missing or invalid configuration: fallback, persistence or failure?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Gets the required calibration mode from key "calib.mode". Throws if missing.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.ModelModule.ModelConfig.yml:91` — `summary` — `Wss.ModelModule.ModelConfig.GetModeRequired`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\ModelModule\ModelConfig.cs`

### A091 — KEEP

Reader question: What happens when SetModelParam``1 receives an omitted, null or empty value?

Quoted field content (YAML block whitespace folded; markup retained):

```text
If <code class="paramref">key</code> is null or whitespace.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.ModelModule.ModelConfigController.yml:217` — `description` — `Wss.ModelModule.ModelConfigController.SetModelParam``1(System.String,``0)`
- `DocsNavigation\apiDocs\api\core\Wss.ModelModule.ModelConfigController.yml:261` — `description` — `Wss.ModelModule.ModelConfigController.GetModelParam``1(System.String)`
- `DocsNavigation\apiDocs\api\core\Wss.ModelModule.ModelConfigController.yml:305` — `description` — `Wss.ModelModule.ModelConfigController.TryGetModelParam``1(System.String,``0@)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\ModelModule\ModelConfigController.cs`

### A092 — KEEP

Reader question: What happens when SetCalibMode receives an omitted, null or empty value?

Quoted field content (YAML block whitespace folded; markup retained):

```text
If <code class="paramref">mode</code> is null or whitespace.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.ModelModule.ModelConfigController.yml:401` — `description` — `Wss.ModelModule.ModelConfigController.SetCalibMode(System.String)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\ModelModule\ModelConfigController.cs`

### A093 — KEEP

Reader question: How does UpdateChannelParams handle missing or invalid configuration: fallback, persistence or failure?

Quoted field content (YAML block whitespace folded; markup retained):

```text
If finger maps to an invalid channel.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\unity\WSS.Unity.StimulationParams.yml:1377` — `description` — `WSS.Unity.StimulationParams.UpdateChannelParams(System.String,System.Int32,System.Int32,System.Int32)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Unity\UnityImplementation\StimualtionParams.cs`

### A097 — KEEP

Reader question: What happens when LoadParamsJson receives an omitted, null or empty value?

Quoted field content (YAML block whitespace folded; markup retained):

```text
If the path is null or whitespace.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CalibrationModule.StimParamsConfigController.yml:237` — `description` — `Wss.CalibrationModule.StimParamsConfigController.LoadParamsJson(System.String)`
- `DocsNavigation\apiDocs\api\core\Wss.CalibrationModule.StimParamsLayer.yml:303` — `description` — `Wss.CalibrationModule.StimParamsLayer.LoadParamsJson(System.String)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CalibrationModule\StimParamsConfigController.cs`, `Core\CalibrationModule\StimParamsLayer.cs`

### A101 — KEEP

Reader question: What happens when WssTargets receives an omitted, null or empty value?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Index 0 maps to <xref href="Wss.CoreModule.WssTarget.Wss1" data-throw-if-not-resolved="false"></xref>, index 1 to <xref href="Wss.CoreModule.WssTarget.Wss2" data-throw-if-not-resolved="false"></xref>, and index 2 to <xref href="Wss.CoreModule.WssTarget.Wss3" data-throw-if-not-resolved="false"></xref>. When null or shorter than three entries, the client falls back to the historical defaults for missing values.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WssClientOptions.yml:130` — `remarks` — `Wss.CoreModule.WssClientOptions.WssTargets`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\WssClientOptions.cs`

### A102 — KEEP

Reader question: How does #ctor handle missing or invalid configuration: fallback, persistence or failure?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Initializes a model configuration backed by the JSON file at <code class="paramref">path</code>. Creates the file with defaults when missing.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.ModelModule.ModelConfig.yml:61` — `summary` — `Wss.ModelModule.ModelConfig.#ctor(System.String)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\ModelModule\ModelConfig.cs`

### A103 — KEEP

Reader question: How does #ctor handle missing or invalid configuration: fallback, persistence or failure?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Initializes a stimulation-parameters config backed by the JSON file at <code class="paramref">path</code>. Creates the file with defaults when missing.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CalibrationModule.StimParamsConfig.yml:57` — `summary` — `Wss.CalibrationModule.StimParamsConfig.#ctor(System.String)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CalibrationModule\StimParamsConfig.cs`

### A104 — KEEP

Reader question: How does #ctor handle missing or invalid configuration: fallback, persistence or failure?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Initializes the configuration by loading a JSON file (or writing defaults when missing).
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.DictConfigBase.yml:123` — `summary` — `Wss.CoreModule.DictConfigBase.#ctor(System.String,Newtonsoft.Json.Linq.JObject)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\CoreJsonConfig\DictConfigBase.cs`

### A105 — KEEP

Reader question: Which fields or payload groups does StimParamsLayer accept, require together or omit?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Layer that wraps an <xref href="Wss.CoreModule.IStimulationCore" data-throw-if-not-resolved="false"></xref> and a <xref href="Wss.CalibrationModule.StimParamsConfigController" data-throw-if-not-resolved="false"></xref>. Computes pulse widths from normalized inputs and forwards stimulation to the core. Also exposes dotted-key parameter access and an optional BASIC capability.
```

Reason: Capability availability and error/no-op behavior affect safe callers. Keep the restriction and the observable behavior.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CalibrationModule.StimParamsLayer.yml:66` — `summary` — `Wss.CalibrationModule.StimParamsLayer`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CalibrationModule\StimParamsLayer.cs`

### A106 — KEEP

Reader question: How does LoadJObject handle missing or invalid configuration: fallback, persistence or failure?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Loads a JSON file into a <xref href="Newtonsoft.Json.Linq.JObject" data-throw-if-not-resolved="false"></xref>. If the file is missing, writes <code class="paramref">defaults</code> to disk and returns it.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.JsonReader.yml:123` — `summary` — `Wss.CoreModule.JsonReader.LoadJObject(System.String,Newtonsoft.Json.Linq.JObject)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\CoreJsonConfig\JsonReader.cs`

### A107 — KEEP

Reader question: How does LoadObject``1 handle missing or invalid configuration: fallback, persistence or failure?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Loads a JSON file into a typed object. If the file is missing or invalid, writes <code class="paramref">defaults</code> to disk and returns it.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.JsonReader.yml:48` — `summary` — `Wss.CoreModule.JsonReader.LoadObject``1(System.String,``0)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\CoreJsonConfig\JsonReader.cs`

### A108 — KEEP

Reader question: How does LoadJson handle missing or invalid configuration: fallback, persistence or failure?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Loads the configuration from disk. Creates and saves a default config if missing or invalid. Safe to call multiple times.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.CoreConfigController.yml:162` — `summary` — `Wss.CoreModule.CoreConfigController.LoadJson`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\CoreJsonConfig\CoreConfigController.cs`

### A110 — KEEP

Reader question: How does WssTargets handle missing or invalid configuration: fallback, persistence or failure?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Missing entries are backfilled with the historical defaults <code>0x81</code>, <code>0x82</code>, and <code>0x83</code>. When normalization changes the in-memory configuration, the updated values are immediately persisted.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.CoreConfigController.yml:331` — `remarks` — `Wss.CoreModule.CoreConfigController.WssTargets`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\CoreJsonConfig\CoreConfigController.cs`

### A112 — KEEP

Reader question: What MaxWss values are accepted by SetMaxWss?

Quoted field content (YAML block whitespace folded; markup retained):

```text
New maximum WSS count. Must be greater than zero.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.CoreConfigController.yml:419` — `description` — `Wss.CoreModule.CoreConfigController.SetMaxWss(System.Int32)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\CoreJsonConfig\CoreConfigController.cs`

### A117 — KEEP

Reader question: What happens when SetCalibMode receives an omitted, null or empty value?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Non-empty mode string, e.g., <code>"midpoint"</code>.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.ModelModule.ModelConfigController.yml:395` — `description` — `Wss.ModelModule.ModelConfigController.SetCalibMode(System.String)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\ModelModule\ModelConfigController.cs`

### A118 — KEEP

Reader question: What happens when #ctor receives an omitted, null or empty value?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Null <xref href="Wss.Transports.TestModeTransportOptions.Rng" data-throw-if-not-resolved="false"></xref> and <xref href="Wss.Transports.TestModeTransportOptions.FallbackPayload" data-throw-if-not-resolved="false"></xref> values are replaced with safe defaults.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.Transports.TestModeTransport.yml:292` — `remarks` — `Wss.Transports.TestModeTransport.#ctor(Wss.Transports.TestModeTransportOptions)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\TestModeTransport.cs`

### A120 — KEEP

Reader question: When can DisconnectAsync honor cancellation?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Optional cancellation token to abort a long-running disconnect.
```

Reason: Capability availability and error/no-op behavior affect safe callers. Keep the restriction and the observable behavior.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.Transports.ITransport.yml:180` — `description` — `Wss.Transports.ITransport.DisconnectAsync(System.Threading.CancellationToken)`
- `DocsNavigation\apiDocs\api\core\Wss.Transports.SerialPortTransport.yml:218` — `description` — `Wss.Transports.SerialPortTransport.DisconnectAsync(System.Threading.CancellationToken)`
- `DocsNavigation\apiDocs\api\core\Wss.Transports.TestModeTransport.yml:372` — `description` — `Wss.Transports.TestModeTransport.DisconnectAsync(System.Threading.CancellationToken)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\Interfaces\ITransport.cs`, `Core\CoreModule\TestModeTransport.cs`, `Core\WSS.Transport.Serial\SerialPortTransport.cs`

### A121 — KEEP

Reader question: When can ConnectAsync honor cancellation?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Optional cancellation token to abort the connect attempt.
```

Reason: Capability availability and error/no-op behavior affect safe callers. Keep the restriction and the observable behavior.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.Transports.ITransport.yml:143` — `description` — `Wss.Transports.ITransport.ConnectAsync(System.Threading.CancellationToken)`
- `DocsNavigation\apiDocs\api\core\Wss.Transports.SerialPortTransport.yml:178` — `description` — `Wss.Transports.SerialPortTransport.ConnectAsync(System.Threading.CancellationToken)`
- `DocsNavigation\apiDocs\api\core\Wss.Transports.TestModeTransport.yml:333` — `description` — `Wss.Transports.TestModeTransport.ConnectAsync(System.Threading.CancellationToken)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\Interfaces\ITransport.cs`, `Core\CoreModule\TestModeTransport.cs`, `Core\WSS.Transport.Serial\SerialPortTransport.cs`

### A122 — KEEP

Reader question: When can DeleteContactConfig honor cancellation?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Optional cancellation token to cancel the operation.
```

Reason: Capability availability and error/no-op behavior affect safe callers. Keep the restriction and the observable behavior.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WssClient.yml:694` — `description` — `Wss.CoreModule.WssClient.DeleteContactConfig(System.Int32,Wss.CoreModule.WssTarget,System.Threading.CancellationToken)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\WssClient.cs`

### A124 — KEEP

Reader question: When can RequestConfigs honor cancellation?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Optional cancellation token.
```

Reason: Capability availability and error/no-op behavior affect safe callers. Keep the restriction and the observable behavior.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WssClient.yml:612` — `description` — `Wss.CoreModule.WssClient.RequestConfigs(System.Int32,System.Int32,Wss.CoreModule.WssTarget,System.Threading.CancellationToken)`
- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WssClient.yml:654` — `description` — `Wss.CoreModule.WssClient.CreateContactConfig(Wss.CoreModule.ContactConfigDefinition,Wss.CoreModule.WssTarget,System.Threading.CancellationToken)`
- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WssClient.yml:735` — `description` — `Wss.CoreModule.WssClient.CreateEvent(Wss.CoreModule.CreateEventRequest,Wss.CoreModule.WssTarget,System.Threading.CancellationToken)`
- `DocsNavigation\apiDocs\api\core\Wss.Transports.TestModeTransport.yml:466` — `description` — `Wss.Transports.TestModeTransport.EnqueueIncoming(System.Byte[],System.Threading.CancellationToken)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\TestModeTransport.cs`, `Core\CoreModule\WssClient.cs`

### A125 — KEEP

Reader question: What happens when AutoResponderAsync receives an omitted, null or empty value?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Optional override auto-responder. If null, the built-in default responder is used.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.Transports.TestModeTransport.yml:199` — `summary` — `Wss.Transports.TestModeTransport.AutoResponderAsync`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\TestModeTransport.cs`

### A126 — KEEP

Reader question: Which fields or payload groups does Error accept, require together or omit?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Optional prefix to prepend to the exception string.
```

Reason: Capability availability and error/no-op behavior affect safe callers. Keep the restriction and the observable behavior.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.Log.yml:208` — `description` — `Wss.CoreModule.Log.Error(System.Exception,System.String)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\Support\Log.cs`

### A127 — KEEP

Reader question: What happens when SerialPort receives an omitted, null or empty value?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Optional serial device name (e.g., "COM3" or "/dev/ttyUSB0"). Uses auto-detect when null.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationOptions.yml:71` — `summary` — `HFI.Wss.StimulationOptions.SerialPort`

Source XML-comment candidates (type-declaration match; verify revision/member): `CSharp\src\StimulationController.cs`

### A129 — KEEP

Reader question: How does FallbackPayload handle missing or invalid configuration: fallback, persistence or failure?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Payload used when incoming checksum is invalid.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.Transports.TestModeTransport.yml:176` — `summary` — `Wss.Transports.TestModeTransport.FallbackPayload`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\TestModeTransport.cs`

### A130 — KEEP

Reader question: What values are valid for the EditEventRatio recharge ratio?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Ratio value (must be one of 1, 2, 4, 8).
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WssClient.yml:1143` — `description` — `Wss.CoreModule.WssClient.EditEventRatio(System.Int32,System.Int32,Wss.CoreModule.WssTarget,System.Threading.CancellationToken)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\WssClient.cs`

### A132 — KEEP

Reader question: How does GetStimParam handle missing or invalid configuration: fallback, persistence or failure?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Reads a parameter by dotted key. Throws if the key is missing.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CalibrationModule.IStimParamsCore.yml:271` — `summary` — `Wss.CalibrationModule.IStimParamsCore.GetStimParam(System.String)`
- `DocsNavigation\apiDocs\api\core\Wss.ModelModule.ModelParamsLayer.yml:449` — `summary` — `Wss.ModelModule.ModelParamsLayer.GetStimParam(System.String)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CalibrationModule\IStimParamsCore.cs`, `Core\ModelModule\ModelParamsLayer.cs`

### A139 — KEEP

Reader question: How does GetOrDefault``1 handle missing or invalid configuration: fallback, persistence or failure?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Returns the value at <code class="paramref">key</code> or a default when missing.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.ModelModule.ModelConfig.yml:149` — `summary` — `Wss.ModelModule.ModelConfig.GetOrDefault``1(System.String,``0)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\ModelModule\ModelConfig.cs`

### A147 — KEEP

Reader question: What happens when WssClientOptions receives an omitted, null or empty value?

Quoted field content (YAML block whitespace folded; markup retained):

```text
The constructor requires a non-null options instance and validates <xref href="Wss.CoreModule.WssClientOptions.ResponseTimeout" data-throw-if-not-resolved="false"></xref> to ensure it is greater than zero. Address settings control how logical targets map to on-wire receiver addresses.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WssClientOptions.yml:24` — `remarks` — `Wss.CoreModule.WssClientOptions`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\WssClientOptions.cs`

### A151 — KEEP

Reader question: Which fields or payload groups does StreamChangeRequest accept, require together or omit?

Quoted field content (YAML block whitespace folded; markup retained):

```text
The protocol supports omitting at most one of the three groups. Each supplied array must contain exactly three values, one per streamed schedule slot. The request itself is required by <xref href="Wss.CoreModule.WssClient.StreamChange(Wss.CoreModule.StreamChangeRequest%2cWss.CoreModule.WssTarget%2cSystem.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref>.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.StreamChangeRequest.yml:22` — `remarks` — `Wss.CoreModule.StreamChangeRequest`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\StreamChangeRequest.cs`

### A154 — KEEP

Reader question: What happens when TestModeTransportOptions receives an omitted, null or empty value?

Quoted field content (YAML block whitespace folded; markup retained):

```text
The transport constructor requires a non-null options instance. When <xref href="Wss.Transports.TestModeTransportOptions.Rng" data-throw-if-not-resolved="false"></xref> or <xref href="Wss.Transports.TestModeTransportOptions.FallbackPayload" data-throw-if-not-resolved="false"></xref> is left null, <xref href="Wss.Transports.TestModeTransport" data-throw-if-not-resolved="false"></xref> substitutes safe defaults.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.Transports.TestModeTransportOptions.yml:26` — `remarks` — `Wss.Transports.TestModeTransportOptions`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\TestModeTransportOptions.cs`

### A155 — KEEP

Reader question: How does BroadcastTarget handle missing or invalid configuration: fallback, persistence or failure?

Quoted field content (YAML block whitespace folded; markup retained):

```text
The value is read from the persisted hexadecimal string in <xref href="Wss.CoreModule.CoreConfig.broadcastTarget" data-throw-if-not-resolved="false"></xref>. If the stored value is missing or invalid, the historical default <code>0x8F</code> is returned and the normalized value is immediately persisted back to disk.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.CoreConfigController.yml:299` — `remarks` — `Wss.CoreModule.CoreConfigController.BroadcastTarget`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\CoreJsonConfig\CoreConfigController.cs`

### A162 — KEEP

Reader question: Does releaseRadio have the same cleanup and blocking behavior as Shutdown()?

Quoted field content (YAML block whitespace folded; markup retained):

```text
This is equivalent to calling <xref href="HFI.Wss.StimulationController.Shutdown" data-throw-if-not-resolved="false"></xref> and has the same blocking and idempotent behavior.
```

Reason: The distinction changes caller behavior, payload interpretation, valid input, lifetime, capability handling or state. Removing it would lose contract information.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:252` — `remarks` — `HFI.Wss.StimulationController.releaseRadio`

Source XML-comment candidates (type-declaration match; verify revision/member): `CSharp\src\StimulationController.cs`

### A169 — KEEP

Reader question: What happens when #ctor receives an omitted, null or empty value?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Thrown if <code class="paramref">core</code> is null.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CalibrationModule.StimParamsLayer.yml:121` — `description` — `Wss.CalibrationModule.StimParamsLayer.#ctor(Wss.CoreModule.IStimulationCore,System.String)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CalibrationModule\StimParamsLayer.cs`

### A171 — KEEP

Reader question: What happens when SendAsync receives an omitted, null or empty value?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Thrown when <code class="paramref">data</code> is null.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.Transports.BleNusTransport.yml:335` — `description` — `Wss.Transports.BleNusTransport.SendAsync(System.Byte[],System.Threading.CancellationToken)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\WSS.Transport.BLE\BleNusTransport.cs`

### A172 — KEEP

Reader question: What happens when CreateContactConfig receives an omitted, null or empty value?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Thrown when <code class="paramref">definition</code> is null.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WssClient.yml:663` — `description` — `Wss.CoreModule.WssClient.CreateContactConfig(Wss.CoreModule.ContactConfigDefinition,Wss.CoreModule.WssTarget,System.Threading.CancellationToken)`
- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WssClient.yml:1231` — `description` — `Wss.CoreModule.WssClient.CreateSchedule(Wss.CoreModule.ScheduleDefinition,Wss.CoreModule.WssTarget,System.Threading.CancellationToken)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\WssClient.cs`

### A173 — KEEP

Reader question: How does UpdateChannelParams handle missing or invalid configuration: fallback, persistence or failure?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Thrown when <code class="paramref">finger</code> maps to an invalid channel.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\unity\WSS.Unity.Stimulation.yml:1414` — `description` — `WSS.Unity.Stimulation.UpdateChannelParams(System.String,System.Int32,System.Int32,System.Int32)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Unity\UnityImplementation\Stimulation.Commands.cs`, `Unity\UnityImplementation\Stimulation.cs`

### A174 — KEEP

Reader question: What happens when #ctor receives an omitted, null or empty value?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Thrown when <code class="paramref">inner</code> is null.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.ModelModule.ModelParamsLayer.yml:123` — `description` — `Wss.ModelModule.ModelParamsLayer.#ctor(Wss.CalibrationModule.IStimParamsCore,System.String)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\ModelModule\ModelParamsLayer.cs`

### A175 — KEEP

Reader question: What happens when #ctor receives an omitted, null or empty value?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Thrown when <code class="paramref">options</code> is null.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.Transports.BleNusTransport.yml:143` — `description` — `Wss.Transports.BleNusTransport.#ctor(Wss.Transports.BleNusTransportOptions)`
- `DocsNavigation\apiDocs\api\core\Wss.Transports.SerialPortTransport.yml:80` — `description` — `Wss.Transports.SerialPortTransport.#ctor(Wss.Transports.SerialPortTransportOptions)`
- `DocsNavigation\apiDocs\api\core\Wss.Transports.TestModeTransport.yml:308` — `description` — `Wss.Transports.TestModeTransport.#ctor(Wss.Transports.TestModeTransportOptions)`
- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:170` — `description` — `HFI.Wss.StimulationController.#ctor(HFI.Wss.StimulationOptions)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\TestModeTransport.cs`, `Core\WSS.Transport.BLE\BleNusTransport.cs`, `Core\WSS.Transport.Serial\SerialPortTransport.cs`, `CSharp\src\StimulationController.cs`

### A176 — KEEP

Reader question: What happens when #ctor receives an omitted, null or empty value?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Thrown when <code class="paramref">path</code> is null, empty, or whitespace.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.CoreConfigController.yml:121` — `description` — `Wss.CoreModule.CoreConfigController.#ctor(System.String)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\CoreJsonConfig\CoreConfigController.cs`

### A177 — KEEP

Reader question: What happens when CreateEvent receives an omitted, null or empty value?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Thrown when <code class="paramref">request</code> is null.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WssClient.yml:744` — `description` — `Wss.CoreModule.WssClient.CreateEvent(Wss.CoreModule.CreateEventRequest,Wss.CoreModule.WssTarget,System.Threading.CancellationToken)`
- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WssClient.yml:1732` — `description` — `Wss.CoreModule.WssClient.StreamChange(Wss.CoreModule.StreamChangeRequest,Wss.CoreModule.WssTarget,System.Threading.CancellationToken)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\WssClient.cs`

### A178 — KEEP

Reader question: What happens when #ctor receives an omitted, null or empty value?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Thrown when <code class="paramref">transport</code> or <code class="paramref">options</code> is null.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WssStimulationCore.yml:91` — `description` — `Wss.CoreModule.WssStimulationCore.#ctor(Wss.Transports.ITransport,Wss.CoreModule.WssStimulationCoreOptions)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\WssStimulationCore.cs`

### A179 — KEEP

Reader question: What happens when #ctor receives an omitted, null or empty value?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Thrown when <code class="paramref">transport</code>, <code class="paramref">codec</code>, <code class="paramref">versionHandler</code>, or <code class="paramref">options</code> is null.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WssClient.yml:142` — `description` — `Wss.CoreModule.WssClient.#ctor(Wss.Transports.ITransport,Wss.CoreModule.IFrameCodec,Wss.CoreModule.WSSVersionHandler,Wss.CoreModule.WssClientOptions)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\WssClient.cs`

### A181 — KEEP

Reader question: What happens when SetFirmware receives an omitted, null or empty value?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Thrown when <code class="paramref">verHandler</code> is null.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.CoreConfigController.yml:462` — `description` — `Wss.CoreModule.CoreConfigController.SetFirmware(System.String,Wss.CoreModule.WSSVersionHandler)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\CoreJsonConfig\CoreConfigController.cs`

### A183 — KEEP

Reader question: What happens when #ctor receives an omitted, null or empty value?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Thrown when <xref href="Wss.CoreModule.WssStimulationCoreOptions.ConfigPath" data-throw-if-not-resolved="false"></xref> is null, empty, or whitespace.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.WssStimulationCore.yml:94` — `description` — `Wss.CoreModule.WssStimulationCore.#ctor(Wss.Transports.ITransport,Wss.CoreModule.WssStimulationCoreOptions)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\WssStimulationCore.cs`

### A185 — KEEP

Reader question: How does #ctor handle missing or invalid configuration: fallback, persistence or failure?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Thrown when an option value is invalid.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\csharp\HFI.Wss.StimulationController.yml:173` — `description` — `HFI.Wss.StimulationController.#ctor(HFI.Wss.StimulationOptions)`

Source XML-comment candidates (type-declaration match; verify revision/member): `CSharp\src\StimulationController.cs`

### A191 — KEEP

Reader question: How does #ctor handle missing or invalid configuration: fallback, persistence or failure?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Thrown when the supplied options are invalid.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.Transports.BleNusTransport.yml:146` — `description` — `Wss.Transports.BleNusTransport.#ctor(Wss.Transports.BleNusTransportOptions)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\WSS.Transport.BLE\BleNusTransport.cs`

### A199 — KEEP

Reader question: How does GetOrDefault``1 handle missing or invalid configuration: fallback, persistence or failure?

Quoted field content (YAML block whitespace folded; markup retained):

```text
Value at the key, or <code class="paramref">dflt</code> if absent.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.ModelModule.ModelConfigController.yml:343` — `description` — `Wss.ModelModule.ModelConfigController.GetOrDefault``1(System.String,``0)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\ModelModule\ModelConfigController.cs`

### A200 — KEEP

Reader question: What happens when #ctor receives an omitted, null or empty value?

Quoted field content (YAML block whitespace folded; markup retained):

```text
When <code class="paramref">path</code> is null or whitespace.
```

Reason: The negative/alternative identifies validation, fallback, capability gating or exception behavior. Keep the semantic condition.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.ModelModule.ModelConfigController.yml:77` — `description` — `Wss.ModelModule.ModelConfigController.#ctor(System.String)`
- `DocsNavigation\apiDocs\api\core\Wss.ModelModule.ModelConfigController.yml:173` — `description` — `Wss.ModelModule.ModelConfigController.LoadModelJson(System.String)`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\ModelModule\ModelConfigController.cs`

### A201 — KEEP

Reader question: Which BLE selector must I supply when automatic selection is disabled, and is scanning bounded?

Quoted field content (YAML block whitespace folded; markup retained):

```text
When <xref href="Wss.Transports.BleNusTransportOptions.AutoSelectDevice" data-throw-if-not-resolved="false"></xref> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>, callers must provide either <xref href="Wss.Transports.BleNusTransportOptions.DeviceId" data-throw-if-not-resolved="false"></xref> or <xref href="Wss.Transports.BleNusTransportOptions.DeviceName" data-throw-if-not-resolved="false"></xref>. Scan-based operations honor <xref href="Wss.Transports.BleNusTransportOptions.ScanTimeout" data-throw-if-not-resolved="false"></xref> when a timeout is configured.
```

Reason: This is a return-value/state or fallback contract. The alternatives tell callers how to branch and must remain.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.Transports.BleNusTransportOptions.yml:26` — `remarks` — `Wss.Transports.BleNusTransportOptions`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\WSS.Transport.BLE\BleNusTransportOptions.cs`

### A204 — KEEP

Reader question: Which fields or payload groups does useConfigAmpCurves accept, require together or omit?

Quoted field content (YAML block whitespace folded; markup retained):

```text
When true, use per-WSS amplitude curve parameters from this config. When false, use built-in defaults (72mA) or device-reported mode (10mA) mapping.
```

Reason: This is a return-value/state or fallback contract. The alternatives tell callers how to branch and must remain.

All occurrences / owning UIDs:

- `DocsNavigation\apiDocs\api\core\Wss.CoreModule.CoreConfig.yml:148` — `summary` — `Wss.CoreModule.CoreConfig.useConfigAmpCurves`

Source XML-comment candidates (type-declaration match; verify revision/member): `Core\CoreModule\CoreJsonConfig\CoreConfig.cs`
