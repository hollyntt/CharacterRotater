# Character Rotator
### Rotate ur character with ease!
## Note: This mod will work in both singleplayer and multiplayer, and should also work if you are the host.

## Features
- **In-Game UI:** Press a key to open a menu to control all settings live.
- **Advanced Chat Commands:** Control all features with powerful and flexible chat commands, built directly into the mod.
- **Dual Modes:** Choose between a continuous **Rotating** effect or a fixed **Static** angle.
- **Full Axis Control:** Use sliders or commands to control the X, Y, and Z axes.
- **Configurable & Saved:** All settings are saved to a config file.

## How to Use
- Press the **Insert** key (by default) to open and close the menu.
- Use the chat commands listed below for quick access.

## Chat Commands
The main command is `/rotator`, which can be shortened to `/rot`.

- `/rotator help` - Lists commands iykyk.
- `/rotator toggle` - Toggles the rotator on or off.
- `/rotator mode <static|rotating>` - Sets the mode to a fixed angle or continuous spinning.
- `/rotator set <axis><value>` - Sets one or more rotation values.
- `/rotator reset` - Resets rotation values to the config defaults.

### Advanced `set` Command Examples
- **Set a single value:** `/rot set y 90`
- **Set multiple values:** `/rot set x 0 y 180 z 0`
- **Use relative values:** `/rot set y+15` (Adds 15 to the current Y value)
- **Combine arguments:** `/rot set x-10 y45` (Subtracts 10 from X, sets Y to 45)

## Configuration
### 1. General
| Setting      | Default  | Description                                                                 |
|--------------|----------|-----------------------------------------------------------------------------|
| `Enabled`    | Disabled | The main switch to turn the entire rotator mod on or off.                       |
| `StaticMode` | Disabled | If disabled, sliders control spin **speed**. If enabled, sliders control fixed **angle**. |

### 2. Controls
| Setting         | Default | Description                                                                 |
|-----------------|---------|-----------------------------------------------------------------------------|
| `ToggleMenuKey` | Insert  | The key used to open and close the mod's in-game settings menu.                 |

### 3. Rotation
| Setting  | Default | Description                                                                 |
|----------|---------|-----------------------------------------------------------------------------|
| `ValueX` | 0       | Controls the X-axis. Represents **speed** in Rotating mode or **angle** in Static mode. |
| `ValueY` | 100     | Controls the Y-axis. Represents **speed** in Rotating mode or **angle** in Static mode. |
| `ValueZ` | 0       | Controls the Z-axis. Represents **speed** in Rotating mode or **angle** in Static mode. |