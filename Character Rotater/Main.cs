using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace Character_Rotater
{
    [BepInPlugin(ModInfo.GUID, ModInfo.NAME, ModInfo.VERSION)]
    public class Main : BaseUnityPlugin
    {
        internal static ManualLogSource Log;
        private readonly Harmony harmony = new Harmony(ModInfo.GUID);

        // --- BepInEx Configuration Entries ---
        private static ConfigEntry<bool> configRotatorEnabled;
        private static ConfigEntry<bool> configIsStatic; // New entry for the mode
        private static ConfigEntry<float> configRotationValueX;
        private static ConfigEntry<float> configRotationValueY;
        private static ConfigEntry<float> configRotationValueZ;
        private static ConfigEntry<KeyCode> configToggleMenuKey;

        // --- Our Mod's Settings ---
        public static bool rotatorEnabled
        {
            get => (bool)configRotatorEnabled.Value;
            set => configRotatorEnabled.Value = value;
        }
        public static bool isStatic
        {
            get => (bool)configIsStatic.Value;
            set => configIsStatic.Value = value;
        }
        public static float rotationValueX
        {
            get => (float)configRotationValueX.Value;
            set => configRotationValueX.Value = value;
        }
        public static float rotationValueY
        {
            get => (float)configRotationValueY.Value;
            set => configRotationValueY.Value = value;
        }
        public static float rotationValueZ
        {
            get => (float)configRotationValueZ.Value;
            set => configRotationValueZ.Value = value;
        }
        
        // --- UI Variables ---
        private bool showMenu = false;
        private Rect windowRect = new Rect(20, 20, 300, 250); // Made window taller for new option
        private readonly int windowId = new System.Random().Next(1000, 9999);

        private void Awake()
        {
            Log = Logger;
            
            // --- Initialize Configuration ---
            configRotatorEnabled = Config.Bind("1. General", "Enabled", false, "Enable or disable the character rotator.");
            configIsStatic = Config.Bind("1. General", "StaticMode", false, "If true, sets a static rotation angle. If false, character spins continuously.");
            
            configToggleMenuKey = Config.Bind("2. Controls", "ToggleMenuKey", KeyCode.Insert, "The key to press to show/hide the rotator menu.");
            
            // Renamed keys for clarity in the config file
            configRotationValueX = Config.Bind("3. Rotation", "ValueX", 0f, "Rotation Speed (if not static) or Angle (if static) on the X-axis.");
            configRotationValueY = Config.Bind("3. Rotation", "ValueY", 100f, "Rotation Speed (if not static) or Angle (if static) on the Y-axis.");
            configRotationValueZ = Config.Bind("3. Rotation", "ValueZ", 0f, "Rotation Speed (if not static) or Angle (if static) on the Z-axis.");

            harmony.PatchAll();
            Log.LogInfo($"[{ModInfo.NAME} v{ModInfo.VERSION}] has loaded! Press '{(KeyCode)configToggleMenuKey.Value}' to open the menu.");
        }
        
        private void Update()
        {
            if (Input.GetKeyDown((KeyCode)configToggleMenuKey.Value))
            {
                showMenu = !showMenu;
            }
        }
        
        private void OnGUI()
        {
            if (!showMenu) return;
            windowRect = GUI.Window(windowId, windowRect, DrawWindow, "Character Rotator");
        }

        private void DrawWindow(int id)
        {
            rotatorEnabled = GUILayout.Toggle(rotatorEnabled, "Enable Rotator");
            isStatic = GUILayout.Toggle(isStatic, "Static Mode"); // New toggle for the mode

            GUILayout.Space(10); 
            
            // Determine the label based on the current mode
            string label = isStatic ? "Angle" : "Speed";
            
            GUILayout.Label($"X-Axis {label}: {rotationValueX:F0}");
            rotationValueX = GUILayout.HorizontalSlider(rotationValueX, -360f, 360f);

            GUILayout.Label($"Y-Axis {label}: {rotationValueY:F0}");
            rotationValueY = GUILayout.HorizontalSlider(rotationValueY, -360f, 360f);

            GUILayout.Label($"Z-Axis {label}: {rotationValueZ:F0}");
            rotationValueZ = GUILayout.HorizontalSlider(rotationValueZ, -360f, 360f);

            GUILayout.Space(10); 
            
            if (GUILayout.Button("Reset to Defaults"))
            {
                isStatic = (bool)configIsStatic.DefaultValue;
                rotationValueX = (float)configRotationValueX.DefaultValue;
                rotationValueY = (float)configRotationValueY.DefaultValue;
                rotationValueZ = (float)configRotationValueZ.DefaultValue;
            }
            
            GUI.DragWindow();
        }
    }
}