# Character Rotater
### Rotate ur character with ease!
## Note: This mod will work in both singleplayer and multiplayer, and should also work if you are the host. (You can also use emotes with this mod installed)

## Features
- **In-Game UI:** Press a key to open a menu to control all settings live.
- **Enable/Disable Toggle:** Easily turn the entire effect on or off.
- **Dual Modes:** Choose between a continuous **Rotating** effect or a fixed **Static** angle.
- **Full Axis Control:** Use sliders to control the X, Y, and Z axes for both speed (rotating) and angle (static).
- **Configurable & Saved:** All settings, including your custom menu key, are saved to a config file in your `BepInEx/config` folder.

## How to Use
Press the **Insert** key (by default) to open and close the menu. This key can be changed in the config file.

## Configuration
The configuration file (`com.s0apy.CRotater.cfg`) will be generated in your `BepInEx/config` folder after running the game once with the mod installed.

### 1. General
| **Setting**               | **Default** | **Description**                                                                 |
|---------------------------|-------------|---------------------------------------------------------------------------------|
| `Enabled`                 | Disabled    | The main switch to turn the entire rotator mod on or off.                       |
| `StaticMode`              | Disabled    | If disabled, sliders control spin speed. If enabled, sliders control fixed angle. |

### 2. Controls
| **Setting**               | **Default** | **Description**                                                                 |
|---------------------------|-------------|---------------------------------------------------------------------------------|
| `ToggleMenuKey`           | Insert      | The key used to open and close the mod's in-game settings menu.                 |

### 3. Rotation
| **Setting**               | **Default** | **Description**                                                                 |
|---------------------------|-------------|---------------------------------------------------------------------------------|
| `ValueX`                  | 0           | Controls the X-axis. Represents **speed** in Rotating mode or **angle** in Static mode. |
| `ValueY`                  | 100         | Controls the Y-axis. Represents **speed** in Rotating mode or **angle** in Static mode. |
| `ValueZ`                  | 0           | Controls the Z-axis. Represents **speed** in Rotating mode or **angle** in Static mode. |