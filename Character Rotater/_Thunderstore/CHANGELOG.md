# Changelog
## 1.0.4
- **DEPRECATED:** This mod has reached End of Support on **Nov 30th, 2026 (00:00 UTC)**.
- Added an End of Support check. It runs when the mod loads and while the game is running.
- After the cutoff the mod unpatches itself, turns the rotator off, and stops handling `/rotator` and `/rot` chat commands.
- After the cutoff an in-game notice tells the user to uninstall the mod and notify the developer, with buttons for the Discord servers and the notification channel.
- After the cutoff the BepInEx log states the mod is deprecated and must be uninstalled to avoid conflicts in the future.
- Added a log line on load showing how many days of support remain.
- Added expiry guards to the rotation patch and the chat command patch as a backup.
- Source is now open on GitHub: https://github.com/hollyntt/CharacterRotater

## 1.0.3
- EOS: Set to Nov 30th

## 1.0.2
- Readme stays stoopid

## 1.0.1
- Homebrewery intergration

## 1.0.0
- Initial release