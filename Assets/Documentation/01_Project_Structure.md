# Project Structure and Configuration

## Root folders

-   `Assets/` --- scripts, scenes, prefabs, ScriptableObjects, models,
    textures, UI, shaders, and third-party assets.
-   `Packages/` --- Unity Package Manager manifest and lockfile.
-   `ProjectSettings/` --- editor version, build settings, input
    settings, player settings, graphics/rendering settings, and other
    project configuration.

## Asset directory overview

The scanned `Assets/` tree contains 80 C# scripts, 14 scene files, 35
prefabs, 60 `.asset` files, and 16 shaders.

  Folder                                             File count
  ------------------------------------------------ ------------
  `Assets`                                                   66
  `Assets/3DModels`                                          43
  `Assets/3DModels/ForChopping`                               2
  `Assets/3DModels/FrontPanel`                               20
  `Assets/3DModels/Frontv2`                                   2
  `Assets/3DModels/NewBlend`                                  2
  `Assets/3DModels/NewVersion`                               18
  `Assets/3DModels/PCParts`                                  18
  `Assets/3DModels/PC_RamClip`                                2
  `Assets/3DModels/RJ45`                                     10
  `Assets/Dark UI`                                           10
  `Assets/Dark UI/Demo`                                       2
  `Assets/Dark UI/Free`                                     130
  `Assets/Dark UI/New Icons`                                668
  `Assets/Dark UI/Prefabs Buttons`                           12
  `Assets/Dark UI/Prefabs Panel`                             16
  `Assets/Dark UI/Ribbons`                                   18
  `Assets/Editor`                                             6
  `Assets/Editor/Migration`                                   2
  `Assets/Editor/Vuforia`                                     1
  `Assets/Faces`                                              7
  `Assets/Faces/Bodies`                                       6
  `Assets/Faces/Faces`                                        6
  `Assets/Faces/Hairs`                                       28
  `Assets/Faces/Kits`                                        34
  `Assets/Faces/Sample`                                       6
  `Assets/Fonts`                                             44
  `Assets/Layer Lab`                                          1
  `Assets/Layer Lab/2D Icons-CasualIconPack`                  2
  `Assets/Materials`                                         34
  `Assets/Plugins`                                            1
  `Assets/Plugins/Demigiant`                                  1
  `Assets/Prefabs`                                           15
  `Assets/Prefabs/PCPartsModel`                              12
  `Assets/QuickOutline`                                       5
  `Assets/QuickOutline/Resources`                             2
  `Assets/QuickOutline/Samples`                               2
  `Assets/QuickOutline/Scripts`                               2
  `Assets/Resources`                                          4
  `Assets/Scenes`                                            12
  `Assets/Scriptables`                                       15
  `Assets/Scriptables/Content`                                8
  `Assets/Scriptables/PCAssesment`                            2
  `Assets/Scriptables/Quiz`                                   6
  `Assets/Scripts`                                           28
  `Assets/Scripts/Assembly`                                   8
  `Assets/Scripts/Disassembly`                               10
  `Assets/Scripts/Interface`                                  2
  `Assets/Scripts/Managers`                                  17
  `Assets/Scripts/NetworkingAssesment`                       14
  `Assets/Scripts/Scriptable`                                14
  `Assets/Scripts/StaticClass`                                2
  `Assets/Scripts/UI`                                        16
  `Assets/Settings`                                          14
  `Assets/SkymonIconPackFree`                                 5
  `Assets/SkymonIconPackFree/skymon-icons-white`            400
  `Assets/Sprites`                                           37
  `Assets/Sprites/ComputerSystem`                            10
  `Assets/StreamingAssets`                                    1
  `Assets/StreamingAssets/Vuforia`                            4
  `Assets/TextMesh Pro`                                       4
  `Assets/TextMesh Pro/Fonts`                                 4
  `Assets/TextMesh Pro/Resources`                             9
  `Assets/TextMesh Pro/Shaders`                              46
  `Assets/TextMesh Pro/Sprites`                               6
  `Assets/Texture`                                            4
  `Assets/Textures`                                           4
  `Assets/TutorialInfo`                                       4
  `Assets/TutorialInfo/Icons`                                 2
  `Assets/TutorialInfo/Scripts`                               3
  `Assets/Utility`                                            4
  `Assets/Video`                                              4
  `Assets/_Recovery`                                          4
  `Assets/textures 1`                                         8

## Main project script folders

-   `Assets/Scripts/Managers/` --- central application managers for
    accounts, topics, UI, quizzes, videos, and saving.
-   `Assets/Scripts/Assembly/` --- assembly assessment manager, data,
    part/slot interaction, camera helpers, and assembly UI.
-   `Assets/Scripts/Disassembly/` --- disassembly manager, selection,
    camera, animation, and UI.
-   `Assets/Scripts/NetworkingAssesment/` --- LAN wire and RJ45
    interaction, sequence checking, camera and UI management.
-   `Assets/Scripts/Scriptable/` --- ScriptableObject definitions for
    topic, quiz, content, and assessment data.
-   `Assets/Scripts/UI/` --- reusable UI controllers and presentation
    helpers.
-   `Assets/Utility/` --- tween and UI tween helpers.
-   `Assets/Editor/` --- custom editor utilities.
-   `Assets/Plugins/` --- imported third-party plugins, including
    DOTween and QuickOutline.

## Build scenes

Enabled in Build Settings:

## Version and packages

m_EditorVersion: 6000.5.1f1 m_EditorVersionWithRevision: 6000.5.1f1
(0d9463e84828)

The complete dependency list is in `Packages/manifest.json`. Notable
packages are Unity URP, Input System, Cinemachine, AI Navigation,
Animation Rigging, Timeline, UGUI, Visual Scripting, Multiplayer Center,
Test Framework, and Vuforia (local package archive).

## Notes

-   `Assets/Scenes/` includes the main application and assessment
    scenes; other folders also contain demo/sample scenes from imported
    assets.
-   `Assets/_Recovery/` contains recovery scene files. Their intended
    status is not established by the project files alone.
-   The project includes `.meta` files, which should remain
    version-controlled alongside their corresponding assets.
