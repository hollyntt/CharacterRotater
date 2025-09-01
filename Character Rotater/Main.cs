using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

// NO [BepInDependency] attribute needed anymore. This is a standalone mod.
namespace Character_Rotater
{
    [BepInPlugin(ModInfo.GUID, ModInfo.NAME, ModInfo.VERSION)]
    public class Main : BaseUnityPlugin
    {
        internal static ManualLogSource Log;
        private readonly Harmony harmony = new Harmony(ModInfo.GUID);

        // ... (All your ConfigEntry and property definitions remain exactly the same)
        #region Config Entries and Properties
        internal static ConfigEntry<bool> configRotatorEnabled;
        internal static ConfigEntry<bool> configIsStatic;
        internal static ConfigEntry<float> configRotationValueX;
        internal static ConfigEntry<float> configRotationValueY;
        internal static ConfigEntry<float> configRotationValueZ;
        internal static ConfigEntry<KeyCode> configToggleMenuKey;

        public static bool rotatorEnabled { get => (bool)configRotatorEnabled.Value; set => configRotatorEnabled.Value = value; }
        public static bool isStatic { get => (bool)configIsStatic.Value; set => configIsStatic.Value = value; }
        public static float rotationValueX { get => (float)configRotationValueX.Value; set => configRotationValueX.Value = value; }
        public static float rotationValueY { get => (float)configRotationValueY.Value; set => configRotationValueY.Value = value; }
        public static float rotationValueZ { get => (float)configRotationValueZ.Value; set => configRotationValueZ.Value = value; }
        #endregion

        // ... (All your UI variables remain the same)
        #region UI Variables
        private bool showMenu = false;
        private Rect windowRect = new Rect(20, 20, 300, 250);
        private readonly int windowId = new System.Random().Next(1000, 9999);
        #endregion

        private void Awake()
        {
            Log = Logger;
            
            // ... (Your Config.Bind calls remain the same)
            #region Config Binding
            configRotatorEnabled = Config.Bind("1. General", "Enabled", false, "Enable or disable the character rotator.");
            configIsStatic = Config.Bind("1. General", "StaticMode", false, "If true, sets a static rotation angle. If false, character spins continuously.");
            configToggleMenuKey = Config.Bind("2. Controls", "ToggleMenuKey", KeyCode.Insert, "The key to press to show/hide the rotator menu.");
            configRotationValueX = Config.Bind("3. Rotation", "ValueX", 0f, "Rotation Speed (if not static) or Angle (if static) on the X-axis.");
            configRotationValueY = Config.Bind("3. Rotation", "ValueY", 100f, "Rotation Speed (if not static) or Angle (if static) on the Y-axis.");
            configRotationValueZ = Config.Bind("3. Rotation", "ValueZ", 0f, "Rotation Speed (if not static) or Angle (if static) on the Z-axis.");
            #endregion

            // Harmony will now find and apply our new ChatPatch automatically.
            harmony.PatchAll();
            
            Log.LogInfo($"[{ModInfo.NAME} v{ModInfo.VERSION}] has loaded! Press '{(KeyCode)configToggleMenuKey.Value}' to open the menu.");
        }
        
        // ... (The rest of the file - ResetSettingsToDefault, Update, OnGUI, DrawWindow - remains exactly the same)
        #region Public Methods and UI
        public static void ResetSettingsToDefault()
        {
            isStatic = (bool)configIsStatic.DefaultValue;
            rotationValueX = (float)configRotationValueX.DefaultValue;
            rotationValueY = (float)configRotationValueY.DefaultValue;
            rotationValueZ = (float)configRotationValueZ.DefaultValue;
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
            isStatic = GUILayout.Toggle(isStatic, "Static Mode");
            GUILayout.Space(10); 
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
                ResetSettingsToDefault();
            }
            
            GUI.DragWindow();
        }
        #endregion
    }
}