using System;
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

        #region UI Variables
        private bool showMenu = false;
        private Rect windowRect = new Rect(20, 20, 300, 250);
        private readonly int windowId = new System.Random().Next(1000, 9999);
        #endregion

        #region End of Support
        // Mod stops working at this exact moment (UTC). Change the date/time here.
        internal static readonly DateTime EndOfSupportUtc = new DateTime(2026, 11, 30, 0, 0, 0, DateTimeKind.Utc);

        private const string DevPing = "@runeaphobia";
        private const string DevContact = "ping @runeaphobia in the Atlyss Modding Central or runem0dz Discord server";
        private const string RunemodzInvite = "https://discord.gg/ePhX4Fb2we";
        private const string ModdingCentralInvite = "https://discord.gg/CqdS3aZJZn";
        private const string NotifyChannelUrl = "https://discord.com/channels/1395549829962137690/1395556467083575447";

        internal static bool IsExpired => DateTime.UtcNow >= EndOfSupportUtc;

        private bool expiredHandled = false;
        private Rect noticeRect;

        private void HandleExpiry()
        {
            expiredHandled = true;
            configRotatorEnabled.Value = false;
            harmony.UnpatchSelf();
            Log.LogError($"[{ModInfo.NAME} v{ModInfo.VERSION}] This mod is DEPRECATED. It reached End of Support on {EndOfSupportUtc:yyyy-MM-dd HH:mm} UTC and has been disabled.");
            Log.LogError($"[{ModInfo.NAME}] To avoid conflicts in the future, it MUST be uninstalled: delete the Character Rotater .dll from BepInEx/plugins.");
            Log.LogError($"[{ModInfo.NAME}] Please also notify the developer: {DevContact}. Channel: {NotifyChannelUrl}");
        }

        private void DrawExpiryNotice(int id)
        {
            GUILayout.Label("This mod has reached its End of Support date and no longer functions.");
            GUILayout.Space(8);
            GUILayout.Label("Please REMOVE it (delete the Character Rotater .dll from BepInEx/plugins).");
            GUILayout.Space(8);
            GUILayout.Label($"You must also notify the developer: ping {DevPing} in the notification channel " +
                            "of the Atlyss Modding Central server (or the runem0dz server) to confirm you've seen this notice.");
            GUILayout.Space(8);

            if (GUILayout.Button("Open notification channel (Atlyss Modding Central)"))
                Application.OpenURL(NotifyChannelUrl);

            if (GUILayout.Button("Join Atlyss Modding Central"))
                Application.OpenURL(ModdingCentralInvite);

            if (GUILayout.Button("Join runem0dz"))
                Application.OpenURL(RunemodzInvite);

            if (GUILayout.Button($"Copy {DevPing} to clipboard"))
                GUIUtility.systemCopyBuffer = DevPing;

            GUI.DragWindow();
        }
        #endregion

        private void Awake()
        {
            Log = Logger;

            #region Config Binding
            configRotatorEnabled = Config.Bind("1. General", "Enabled", false, "Enable or disable the character rotator.");
            configIsStatic = Config.Bind("1. General", "StaticMode", false, "If true, sets a static rotation angle. If false, character spins continuously.");
            configToggleMenuKey = Config.Bind("2. Controls", "ToggleMenuKey", KeyCode.Insert, "The key to press to show/hide the rotator menu.");
            configRotationValueX = Config.Bind("3. Rotation", "ValueX", 0f, "Rotation Speed (if not static) or Angle (if static) on the X-axis.");
            configRotationValueY = Config.Bind("3. Rotation", "ValueY", 100f, "Rotation Speed (if not static) or Angle (if static) on the Y-axis.");
            configRotationValueZ = Config.Bind("3. Rotation", "ValueZ", 0f, "Rotation Speed (if not static) or Angle (if static) on the Z-axis.");
            #endregion

            // End of Support check: if expired, never patch the game.
            if (IsExpired)
            {
                HandleExpiry();
                return;
            }

            harmony.PatchAll();

            int daysLeft = (int)(EndOfSupportUtc - DateTime.UtcNow).TotalDays;
            Log.LogInfo($"[{ModInfo.NAME} v{ModInfo.VERSION}] loaded. Support ends in {daysLeft} day(s). Press '{configToggleMenuKey.Value}' to open the menu.");
        }

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
            // Catches expiry that happens mid-session.
            if (!expiredHandled && IsExpired) HandleExpiry();
            if (expiredHandled) return;

            if (Input.GetKeyDown((KeyCode)configToggleMenuKey.Value))
            {
                showMenu = !showMenu;
            }
        }

        private void OnGUI()
        {
            if (expiredHandled)
            {
                if (noticeRect.width == 0)
                    noticeRect = new Rect((Screen.width - 460) / 2f, (Screen.height - 340) / 2f, 460, 340);
                noticeRect = GUI.Window(windowId + 1, noticeRect, DrawExpiryNotice, "Character Rotater - End of Support");
                return;
            }

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