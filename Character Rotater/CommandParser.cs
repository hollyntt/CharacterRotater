using UnityEngine;

namespace Character_Rotater
{
    public static class CommandParser
    {
        public static void HandleCommand(string[] args)
        {
            if (args.Length < 1)
            {
                PrintHelp();
                return;
            }

            switch (args[0].ToLower())
            {
                case "toggle":
                    Main.rotatorEnabled = !Main.rotatorEnabled;
                    LogChatMessage($"Rotator {(Main.rotatorEnabled ? "Enabled" : "Disabled")}.");
                    break;
                case "mode":
                    HandleModeCommand(args);
                    break;
                case "set":
                    HandleSetCommand(args);
                    break;
                case "reset":
                    Main.ResetSettingsToDefault();
                    LogChatMessage("Rotation values have been reset to their defaults.");
                    break;
                default:
                    PrintHelp();
                    break;
            }
        }

        private static void HandleModeCommand(string[] args)
        {
            if (args.Length > 1 && (args[1].ToLower() == "static" || args[1].ToLower() == "rotating"))
            {
                Main.isStatic = (args[1].ToLower() == "static");
                LogChatMessage($"Rotator mode set to {(Main.isStatic ? "Static" : "Rotating")}.");
            }
            else
            {
                LogChatMessage("Usage: /rotator mode <static|rotating>");
            }
        }

        private static void HandleSetCommand(string[] args)
        {
            if (args.Length < 2)
            {
                LogChatMessage("Usage: /rotator set <axis><value> OR /rotator set <x> <y> <z>");
                return;
            }

            if (args.Length > 3 && TryParseValue(args[1], Main.rotationValueX, out float x) &&
                TryParseValue(args[2], Main.rotationValueY, out float y) &&
                TryParseValue(args[3], Main.rotationValueZ, out float z))
            {
                Main.rotationValueX = x;
                Main.rotationValueY = y;
                Main.rotationValueZ = z;
                LogChatMessage($"Rotation set to X:{x:F0}, Y:{y:F0}, Z:{z:F0}.");
                return;
            }

            bool valueChanged = false;
            for (int i = 1; i < args.Length; i++)
            {
                string arg = args[i].ToLower();
                char axis = arg[0];
                string valueStr = arg.Substring(1);

                if (string.IsNullOrEmpty(valueStr) && i + 1 < args.Length)
                {
                    valueStr = args[i + 1];
                    i++;
                }

                switch (axis)
                {
                    case 'x':
                        if (TryParseValue(valueStr, Main.rotationValueX, out float newX)) { Main.rotationValueX = newX; valueChanged = true; }
                        break;
                    case 'y':
                        if (TryParseValue(valueStr, Main.rotationValueY, out float newY)) { Main.rotationValueY = newY; valueChanged = true; }
                        break;
                    case 'z':
                        if (TryParseValue(valueStr, Main.rotationValueZ, out float newZ)) { Main.rotationValueZ = newZ; valueChanged = true; }
                        break;
                }
            }

            if (valueChanged)
            {
                LogChatMessage($"Current rotation: X:{Main.rotationValueX:F0}, Y:{Main.rotationValueY:F0}, Z:{Main.rotationValueZ:F0}.");
            }
            else
            {
                LogChatMessage("Invalid format. Use `/rotator set x<val> y<val>...`");
            }
        }
        
        private static bool TryParseValue(string input, float currentValue, out float result)
        {
            input = input.Trim();
            result = currentValue;
            if (string.IsNullOrEmpty(input)) return false;

            if (input.StartsWith("+"))
            {
                if (float.TryParse(input.Substring(1), out float addValue))
                {
                    result = Mathf.Clamp(currentValue + addValue, -360f, 360f);
                    return true;
                }
            }
            else if (input.StartsWith("-"))
            {
                if (float.TryParse(input, out float subValue))
                {
                    result = Mathf.Clamp(currentValue + subValue, -360f, 360f);
                    return true;
                }
            }
            else if (float.TryParse(input, out float absoluteValue))
            {
                result = Mathf.Clamp(absoluteValue, -360f, 360f);
                return true;
            }
            return false;
        }

        // *** THIS IS THE CORRECTED METHOD ***
        private static void LogChatMessage(string message)
        {
            // Use the New_ChatMessage method from the decompiled ChatBehaviour class.
            // We add a color tag to make our mod's messages stand out.
            Player._mainPlayer?._chatBehaviour?.New_ChatMessage($"<color=#00FFFF>[Rotator]</color> {message}");
        }

        private static void PrintHelp()
        {
            LogChatMessage("--- Character Rotator Help ---");
            LogChatMessage("/rotator toggle - Toggles the rotator on/off.");
            LogChatMessage("/rotator mode <static|rotating>");
            LogChatMessage("/rotator set <x/y/z><value> - e.g., /rot set y90 x+10");
            LogChatMessage("/rotator reset - Resets all rotation values to default.");
        }
    }
}