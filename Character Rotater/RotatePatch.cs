using HarmonyLib;
using Mirror;
using UnityEngine;

namespace Character_Rotater.Patches
{
	[HarmonyPatch(typeof(Player))]
	internal class RotationPatch
	{
		[HarmonyPatch("Update")]
		[HarmonyPostfix]
		public static void OverrideRotation(Player __instance)
		{
			if (__instance == Player._mainPlayer && Main.rotatorEnabled)
			{
				Quaternion newRotation;

				// Check which mode is active
				if (Main.isStatic)
				{
					// STATIC MODE: Use the slider values directly as angles.
					newRotation = Quaternion.Euler(
						Main.rotationValueX,
						Main.rotationValueY,
						Main.rotationValueZ
					);
				}
				else
				{
					// ROTATING MODE: Use Time.time to create continuous spinning.
					newRotation = Quaternion.Euler(
						Time.time * Main.rotationValueX,
						Time.time * Main.rotationValueY,
						Time.time * Main.rotationValueZ
					);
				}

				NetworkTransformUnreliable ntu = __instance._netTransform;
				if (ntu != null)
				{
					ntu.transform.rotation = newRotation;
				}
			}
		}
	}
}