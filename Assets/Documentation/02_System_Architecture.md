# System Architecture and Workflow Guide

## 1. Main application startup

1.  `GameManager.Awake()` assigns the static `GameManager.instance` and
    loads `SaveData` using `GeneralUtility.LoadFile<SaveData>()`.
2.  If no save exists, `GameManager` creates an empty `SaveData`.
3.  `AccountManager.Awake()` loads available player-name strings from
    its configured `TextAsset`.
4.  `AccountManager.Start()` connects account creation UI callbacks and
    starts `DelayInit()`.
5.  After a one-second wait, `DelayInit()` calls `LoadAllAccount()` and
    `LoadLocalSavePlayer()`.
6.  `LoadLocalSavePlayer()` reads `LocalPlayerSave` from PlayerPrefs and
    attempts to select that profile.

The one-second delay is an authored implementation detail. The code does
not guarantee all UI or external initialization is complete merely
because this delay elapsed.

## 2. Account creation and selection

-   `AccountManager.SubmitAccountCreation()` checks that a player name
    and avatar name are non-empty.
-   It calls `GameManager.CreatePlayer()`.
-   `GameManager.CreatePlayer()` generates a GUID, copies the entered
    name/avatar, adds the profile to `saveFile.players`, selects it,
    adds its login UI, and saves.
-   `AccountManager.SetCurrentPlayer(string)` searches by GUID. On
    success it updates `currentSelectedPlayer`, saves the GUID in
    PlayerPrefs, and updates the profile UI.
-   `GameManager.SelectPlayer(string)` is another profile-selection
    method; it updates `GeneralUtility.CURRENTPLAYERID` and
    `currentSelectedPlayer`, but does not itself save the PlayerPrefs
    key.

## 3. Topic and module navigation

-   `TopicManager.GetTopic(ArTopics)` searches its configured
    `TopicData` list.
-   `ModuleInitializer.InitializeModules()` activates the module panel,
    resolves the `TopicData`, and asks `UIManager` to set up/enable the
    selected topic UI.
-   `ModuleInitializer.Setup()` creates one module UI prefab for each
    configured `Module`.
-   `ModuleUI.Setup()` copies module content and assessment
    configuration to the UI component.
-   `ModuleUI` routes quiz assessments to `MultipleChoiceManager`;
    non-quiz assessments load the configured assessment scene.
-   `ModuleInitializer` reads and updates module completion state
    against the active `PlayerSaveData`.

## 4. Learning content

`UIManager.InitializeModuleContent()` creates `ContentUI` instances from
a `PhotoTextContentData` asset. `ContentUI` presents the content item
and offers view actions for 3D models or video. Other UIManager methods
switch between text, photo, model, and video panels.
`ContentDataManager` is a holder for configured lists of video, text,
and photo-text content assets.

`ModuleContentController` remembers the selected module and topic. When
its scroll completion callback receives a position with `y <= 0.01f`, it
marks the module read and checks assessments that reference the
completed module.

## 5. Quiz assessment

1.  The module assessment UI obtains a quiz by `QuizId`.
2.  `MultipleChoiceManager.InitializeQuestions()` instantiates one
    question prefab per question in `QuizData`.
3.  Each `MultipleChoiceController` records the chosen answer.
4.  `CheckAllAnsweredQuestions()` controls visibility of the submit
    button depending on whether all question controllers have a choice.
5.  `OnSubmitAnswers()` counts correct responses, displays the result
    through `UIManager`, calls `GeneralUtility.SaveAssesmentProgress()`,
    and saves the `GameManager` save object.

The score stored is the number of correct answers. The current save
helper sets `isCompleted = true`.

## 6. PC assembly assessment

Main classes: - `AssemblyManager` --- scene-level coordinator. -
`AssembleAssesment` --- validates an assembly attempt and calls
`AssemblyPart`. - `AssemblyPart` --- component state, prerequisite
checks, snap movement, and state transitions. - `AssemblySlot` ---
identifies the matching part and snap point. - `AssemblySlotTrigger` ---
clickable slot visual. - `AssemblyCameraManager` --- camera switching. -
`AssemblyUIManager` and `CinematicUI` --- interaction/guidance UI.

Flow: 1. `AssemblyManager.Awake()` finds `CorrectPositionPart`,
`AssemblySlotTrigger`, and `AssemblyPart` scene objects. 2. `Start()`
selects the current assessment entry and calls `SetupParts()`. 3.
`SetupParts()` resolves each part's slot by `PCParts` ID. 4.
`AssembleAssesment.TryAssemble(part)` calls `part.CanAssemble()`. 5. If
prerequisites pass, `AssemblyPart.Assemble()` starts a tween to the
correct slot and marks `IsAssembled = true`. 6. The tween completion
callback performs camera/visual/cinematic actions, checks assessment
completion, and advances the current assessment.

Important: the `PCAssesmentData.assemblySteps` ordered sequence and the
part alignment method are not actively checked in the current
`TryAssemble()` method; related code is commented out. Active assembly
gating is based on each part's `requiredPartsToAssemble` list.

## 7. PC disassembly assessment

Main classes: - `DisassemblyManager` - `DisassemblyAssesment` -
`AssemblyPart` - `DisassemblyCameraManager` - `DisassemblySelection` -
`DisassemblyUIManager` - `ComponentPartAnimator` / `PcPartsAnimation`

Flow: 1. `DisassemblyManager.Awake()` finds all `AssemblyPart` objects,
including inactive ones. 2. `DisassemblyAssesment.TryDisassemble(part)`
ignores null or already-disassembled parts. 3.
`AssemblyPart.CanDisassemble()` checks `requiredPartsToDisassemble`. 4.
If allowed, `AssemblyPart.Disassemble()` sets `IsAssembled` false. 5.
`DisassemblyManager.DisablePart()` animates the part if a
`PcPartsAnimation` component exists; otherwise it scales the part to
zero. 6. When all tracked parts report `IsAssembled == false`, the
disassembly end UI is opened.

The ordered disassembly sequence stored in `PCAssesmentData` is not
actively checked by the current `TryDisassemble()` code.

## 8. Networking / LAN cable assessment

This is a cable-configuration assessment, not evidence of a multiplayer
networking implementation.

-   `NetworkAssesmentManager` stores the selected `Mode` and links the
    assessment, camera, and UI managers.
-   `NetworkUIManager` lets the learner choose Straight-Through or
    Crossover, changes camera view, and displays completion/failure.
-   `LanWireInteraction.OnPointerDown()` passes the selected wire to
    `NetworkAssesment.SetLanwire()`.
-   The first selected wire is highlighted. Selecting a second wire
    triggers `NetworkAssesment.Swap()`, which tween-moves the two wire
    objects to each other's world positions.
-   `NetworkAssesment.CheckSequence()` sorts each end's configured wire
    array by world X coordinate, reads `lanId`, selects the correct
    sequence for the current mode, and compares both ends.
-   Both arrays must match for the result to be successful.

The drag/drop event methods in `LanWireInteraction` currently contain
commented-out movement/swap code. The active mechanism is click/select
two wires to swap.

## 9. Persistence flow

`GeneralUtility.SaveFile<T>()` serializes the supplied object with
`JsonUtility.ToJson(data, true)` and writes it to
`Application.persistentDataPath/save.json`.

`GeneralUtility.LoadFile<T>()` returns default when the file does not
exist; otherwise it reads and deserializes JSON.

Assessment progress is added/updated by
`GeneralUtility.SaveAssesmentProgress()`. The lookup currently searches
by `assesmentName` only; the topic is assigned when creating a new
record but is not included in the existing-record lookup.

## 10. Integration and scene references

The scenes contain many serialized Unity component references and
third-party components. To fully verify a workflow: - Open the relevant
scene in the project's specified Unity version. - Check every serialized
field on the manager/controller objects. - Confirm prefab references and
button/event callbacks. - Run the scene in Play Mode and exercise the
full workflow. - Inspect Console exceptions and warnings.
