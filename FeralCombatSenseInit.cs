namespace FeralCombatSenseMod;

using HarmonyLib;
using UnityEngine;

public class FeralCombatSenseInit : IModApi
{
    public void InitMod(Mod mod)
    {
        Debug.Log("[FeralCombatSense] InitMod starting...");
        var harmony = new Harmony("com.retrobalder.feralcombatsense");
        harmony.PatchAll();
        Debug.Log("[FeralCombatSense] Harmony patches applied");
    }
}