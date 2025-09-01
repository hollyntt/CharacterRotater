using HarmonyLib;

namespace Character_Rotater.Patches
{
    // We target the game's main ChatBehaviour class and the method for sending messages.
    [HarmonyPatch(typeof(ChatBehaviour), "Cmd_SendChatMessage")]
    internal class ChatPatch
    {
        // A Prefix patch runs BEFORE the original method.
        // It lets us see the message and decide if we want to block it.
        [HarmonyPrefix]
        public static bool InterceptChatCommands(string _message)
        {
            string text = _message.Trim();
            
            // Check if the message starts with our command or its alias.
            if (text.StartsWith("/rotator") || text.StartsWith("/rot"))
            {
                // Split the message into parts. e.g., "/rot set y 90"
                string[] parts = text.Split(' ');
                
                // Create a new array for the arguments, skipping the first part ("/rotator" or "/rot")
                string[] args = new string[parts.Length - 1];
                for (int i = 1; i < parts.Length; i++)
                {
                    args[i - 1] = parts[i];
                }

                // Send the arguments to our parser to be handled.
                CommandParser.HandleCommand(args);

                // Return FALSE to "consume" the command and stop it from being sent as a public chat message.
                return false;
            }

            // If it's not our command, return TRUE to let the original method run as normal.
            return true;
        }
    }
}