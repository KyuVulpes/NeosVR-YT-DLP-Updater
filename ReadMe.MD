﻿# NeosVR YouTube-DL Plus Updater

This NeosVR Mod Loader modification enables the user to download the latest version of YouTube-DL Plus without having to do anything else.

## Installation

0. Install [NeosModLoader](https://github.com/neos-modding-group/NeosModLoader).
1. Place [YT_DLP_Updater.dll](https://github.com/KyuVulpes/NeosVR-YT-DLP-Updater/releases/latest/download/YT_DLP_Updater.dll) into your `nml_mods` folder.
   0. It should be in `C:\Program Files (x86)\Steam\steamapps\common\NeosVR\nml_mods`. If it doesn't exist, go ahead and create it.
2. Run NeosVR.

## What is the purpose

The reason for this mod is to help with video downloading issues. Now it won't solve all of them but you should see either a quicker download,
or a broader support for sites as YouTube-DL Plus adds support.

## Issue discovered

If you found an issue with the modification (i.e. it isn't in base NeosVR or the runtime), go ahead and open an issue report detailing the problem.
Though, I highly suspect that there shouldn't be any issues in the modification.

## Compiling

If you wish to compile the library yourself, you will need:
* An IDE (Integrated Development Environment)
  * JetBrains Rider
  * Visual Studio
  * Visual Studio Code
  * Notepad++
* .NET Framework 4.7.2 SDK
* NeosVR
  * Must have `NeosModLoader.dll`.

With NeosVR installed and modded, create a folder in the root of the project called `Dependencies`.
Inside that folder, copy `Newtonsoft.Json.dll` from `Neos_Data\Managed` and `NeosModLoader.dll`.
Next is to simply compile the code from whatever IDE you chose to use.
