using HarmonyLib;
using Mirror;
using UnityEngine;

namespace Character_Rotater.Patches
{
	[HarmonyPatch(typeof(Player))]
	internal class RotationPatch
	{
		[HarmonyPostfix, HarmonyPatch("Update")]
		public static void OverrideRotation(Player __instance)
		{
			if (__instance == Player._mainPlayer && Main.rotatorEnabled)
			{
				Quaternion newRotation;

				if (Main.isStatic)
				{
					newRotation = Quaternion.Euler(
						Main.rotationValueX,
						Main.rotationValueY,
						Main.rotationValueZ
					);
				}
				else
				{
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