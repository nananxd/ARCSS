# Setup and Developer Guide

## 1. Opening the project

1.  Extract the project folder.
2.  Open it using Unity Editor `6000.5.1f1` as declared in
    `ProjectSettings/ProjectVersion.txt`.
3.  Allow Unity Package Manager to resolve dependencies.
4.  Confirm the local Vuforia package archive referenced by
    `Packages/manifest.json` is present in the project package source.
5.  Wait for asset import and shader compilation to finish.
6.  Open `Assets/Scenes/SampleScene.unity` to inspect the main
    application.

Do not delete or regenerate `.meta` files casually; they preserve GUID
references used by scenes, prefabs, and assets.

## 2. Build Settings

The following scenes are enabled in the scanned Build Settings: -
`Assets/Scenes/SampleScene.unity` -
`Assets/Scenes/AsessmentScene.unity` -
`Assets/Scenes/Disassemble.unity` -
`Assets/Scenes/NetworkingAssesment.unity`

The project code loads the disassembly scene by the string name
`Disassemble`. Other assessment scene routing uses the
`AssesmentSceneName` enum and `ModuleUI` scene-loading code. Confirm
scene names and build indices if changing the build list.

## 3. Adding a learning topic

1.  Create/configure a `TopicData` ScriptableObject.
2.  Set its `ArTopics` enum value.
3.  Add ordered `Module` entries.
4.  Set each module's content type, name, description, content asset,
    video/model references, and sprites as applicable.
5.  For assessment modules, configure `AssesmentDetails`, including quiz
    or scene assessment settings and required module-name strings.
6.  Add the `TopicData` asset to the `TopicManager` serialized list.
7.  Ensure the main scene's `UIManager` has a `ModuleInitializer`
    assigned for the topic.
8.  Verify module prefab and assessment UI references in the Inspector.

## 4. Adding a quiz

1.  Create a `QuizData` asset.
2.  Select an available `QuizId` (or add a new enum value in
    `QuizData.cs`).
3.  Add questions and answer choices.
4.  Mark the correct choice(s) using `isCorrectAnswer`.
5.  Add the asset to `MultipleChoiceManager.quizzes`.
6.  Configure the module's `AssesmentDetails` with
    `isQuizAssesment = true` and the matching `quizId`.
7.  Verify question prefab, parent transform, submit button, and result
    UI references.

The current `MultipleChoiceManager.InitializeQuestions()` appends
generated controllers to its list. Ensure any repeated quiz opening
clears prior spawned UI/list entries as appropriate; `ClearList()`
exists but should be checked for correct GameObject destruction and
parent cleanup before reuse.

## 5. Adding an assembly part or assessment step

1.  Add the corresponding `PCParts` enum value if a new part identity is
    needed.
2.  Configure an `AssemblyPart` on the part GameObject with the matching
    `partID`.
3.  Configure an `AssemblySlot` with the same `partId` and a snap point.
4.  Configure prerequisite lists (`requiredPartsToAssemble` and
    `requiredPartsToDisassemble`) in the Inspector.
5.  Ensure the scene has an `AssemblyManager`, `AssembleAssesment`,
    camera manager, and UI references.
6.  Add any correct-position marker/slot trigger and camera helpers
    required by the scene.
7.  Verify the part is found by `AssemblyManager` and its slot is
    resolved by ID.

Note: ordered steps in `PCAssesmentData` are not currently enforced by
the active assembly/disassembly attempt methods.

## 6. Adding a networking assessment wire

1.  Assign a unique `lanId` to each wire object.
2.  Add/configure `LanWireInteraction` and its collider/outline
    component.
3.  Include the wire in the correct serialized wire array (`lanObjects`
    or `lanObjects2`) on `NetworkAssesment`.
4.  Configure the straight-through and crossover correct sequence arrays
    for both connector ends.
5.  Verify that the world X ordering matches the intended left-to-right
    pin sequence.
6.  Test selecting two wires and submitting the sequence for both modes.

## 7. Persistence and profile testing

-   Save file: `Application.persistentDataPath/save.json`.
-   Selected profile key: `LocalPlayerSave` in PlayerPrefs.
-   Test a new profile, app restart, profile reselection, module
    completion, quiz completion, and multiple profiles.
-   Back up the save before changing the serialized `SaveData` shape.
-   If changing serialized field names or types, consider
    migration/versioning; the current save format has no explicit schema
    version.

## 8. Known code review items to verify

These are potential issues visible from source and should be tested
before refactoring:

1.  `AccountManager.SetCurrentPlayer(string)` handles a missing ID by
    showing a warning, but callers should ensure `currentSelectedPlayer`
    is not used afterward as if selection succeeded.
2.  `AccountManager.LoadAllAccount()` instantiates account UI for every
    saved player; ensure it is not called repeatedly without clearing
    prior children.
3.  `ModuleInitializer.CheckIfModuleDoneReading()` initially searches
    saved module data by `moduleName` without topic in the lookup;
    same-named modules in different topics could collide.
4.  `ModuleInitializer.CompleteModuleReading()` accesses
    `foundModule.moduleName` to find its UI before checking
    `foundModule` for null. Validate this path if the supplied
    module/topic does not match.
5.  `ModuleInitializer` has `spawnedAssesment` and an assessment prefab
    field, but the shown setup loop creates `modulePrefab` for each
    configured module; inspect whether separate assessment prefab
    generation is obsolete or handled elsewhere.
6.  `GeneralUtility.SaveAssesmentProgress()` searches existing
    assessment records by assessment name only, not topic; same-named
    assessments in different topics could share a record.
7.  `AssesmentSaveManager` maintains its own `saveFile` and
    `currentSelectedPlayer` fields; verify Inspector/runtime
    initialization before using it alongside the central `GameManager`
    save object.
8.  `MultipleChoiceManager.ClearList()` destroys each
    `MultipleChoiceController` component reference rather than
    explicitly destroying its owning GameObject; confirm whether this is
    intended.
9.  `NetworkAssesment.Swap()` starts `DelaySwap()` and the coroutine
    assumes both selected wire references remain valid; test rapid
    repeated selections.
10. `LanWireInteraction` drag/drop methods currently have their movement
    implementation commented out.
11. `AssemblyPart.Assemble()` marks `IsAssembled` true immediately after
    starting a tween; if the tween is interrupted, state and visual
    position may diverge.
12. `AssemblyManager.SetNextCurrentAssesment()` calls `NextAssesment()`
    only when the current parts are complete, but `NextAssesment()` does
    not guard later camera setup against reaching the final list index.
    Test the last assessment.
13. `DisassemblyManager.CheckIfAllDisassemble()` checks all found
    `AssemblyPart` components, including inactive ones; ensure the list
    represents only parts expected to be disassembled.
14. `ModelViewerControl` directly uses legacy `Input.touchCount` /
    `Input.GetTouch` while the manifest also declares the Input System
    package. Verify Active Input Handling settings and device behavior.
15. Scene build settings enable four scenes; confirm any other scene
    loaded by name or enum is included and correctly named.

These are review prompts, not confirmed runtime defects. Some may be
intentional or protected by scene-specific configuration.
