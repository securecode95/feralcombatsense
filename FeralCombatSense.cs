using HarmonyLib;
using UnityEngine;

[HarmonyPatch(typeof(ItemAction))]
[HarmonyPatch("StartAction")]
public class FeralCombatSense
{
    static void Postfix(ItemAction __instance, ItemActionData _actionData, bool _bReleased)
    {
        if (__instance is ItemActionRanged)
        {
            if (_actionData.invData == null) return;

            // Get the player entity who is holding the weapon
            EntityAlive entity = _actionData.invData.holdingEntity;
            EntityPlayerLocal player = entity as EntityPlayerLocal;
            if (player == null) return;

            UnityEngine.Debug.Log("[FeralCombatSense] Gun fired by player, triggering zombie sense!");

            float detectionRadius = 200f;
            foreach (Entity entityZombie in GameManager.Instance.World.Entities.list)
            {
                if (entityZombie is EntityZombie zombie && zombie.IsAlive() && !zombie.IsDead())
                {
                    float distance = Vector3.Distance(player.position, zombie.position);
                    if (distance <= detectionRadius)
                    {
                        zombie.Buffs.AddBuff("feralCombatSenseBuff");
                        UnityEngine.Debug.Log($"[FeralCombatSense] Zombie {zombie.EntityName} ({zombie.entityId}) heard the gunshot at distance {distance:F1}.");
                    }
                }
            }
        }
    }
}