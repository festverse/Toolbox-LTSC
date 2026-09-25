# Atlas Toolbox (Windows 10 LTSC Edition)

Welcome to the **Atlas Toolbox LTSC Fork**! This is a modified version of the official [AtlasOS Toolbox](https://github.com/Atlas-OS/atlas-toolbox), specifically tweaked to play nice with **Windows 10 IoT Enterprise LTSC 2021 (Build 19044)** and **AtlasOS v0.4.1**.

## Why does this exist?
The official Atlas Toolbox is a fantastic piece of software, but it was built with Windows 11 and Atlas v0.5.0+ in mind. If you try to run the official release on an older LTSC build or a v0.4.x playbook, it blocks you with a strict compatibility screen. 

This fork removes those roadblocks. It patches the internal OS version checks, expands the registry scanning to recognize non-standard playbook GUIDs, and safely falls back on older Windows 10 services (like `TabletInputService` instead of Win11's `TextInputManagementService`).

## What works?
Pretty much everything you actually need!
* Core configuration (Power saving, Hibernation, Sleep)
* Network & Troubleshooting resets
* Services toggles (Bluetooth, Printing, Network Discovery)
* Boot configuration and custom animations

**Note on "Ghost Features":**
You'll still see toggles for Windows 11-exclusive features like **Copilot, Recall, Snap Layouts, and Gallery**. Flipping these switches *won't crash the app* (they safely write to your registry), but Windows 10 will simply ignore them. Just pretend they aren't there!

## Disclaimer & Support
> **Please read this before using!**
> 
> This is a purely unofficial fork created for **educational and personal usage**. 
> 
> **Absolutely NO support will be provided by me or the official AtlasOS team.** Please do *not* go to the official Atlas Discord or GitHub complaining about bugs found in this specific fork—they won't be able to help you.
> 
> That being said, we are all human! If you find a glaring issue or bug specifically related to this fork, feel free to open an issue on this repository and I'll take a look whenever I have some free time.

## Building from Source
If you want to compile this yourself, you'll need the **.NET 8 SDK** installed.

Open a terminal in the project directory and run:
```powershell
# Restore dependencies
dotnet restore

# Build a self-contained executable for Windows x64
dotnet publish AtlasToolbox\AtlasToolbox.csproj -c Release -p:Platform=x64 -r win-x64 --self-contained true -o .\publish
```
Your ready-to-use `.exe` will be waiting for you in the `publish` folder. Enjoy!
