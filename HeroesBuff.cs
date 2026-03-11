global using BTD_Mod_Helper.Extensions;
using MelonLoader;
using BTD_Mod_Helper;
using HeroesBuff;
using Il2CppAssets.Scripts.Models;
using System;

[assembly: MelonInfo(typeof(HeroesBuff.HeroesBuff), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace HeroesBuff;

public class HeroesBuff : BloonsTD6Mod
{
    public override void OnApplicationStart()
    {
        ModHelper.Msg<HeroesBuff>("HeroesBuff loaded!");
    }

    public override void OnNewGameModel(GameModel result)
    {
        for (int i=0; i < result.towers.Count; i++)
        {
            var hero = result.towers[i];

            if (hero.IsHero())
            {
                ModHelper.Msg<HeroesBuff>("Buffing " + hero.baseId + "...");
                // Sauda Modifications
                if (hero.baseId == "Sauda")
                {
                    float damageMultiplier = 25f + (5f * hero.tier);
                    float rateMultiplier = 2f + (0.5f * hero.tier);

                    hero.range *= 1.2f;
                    hero.GetAttackModel().range *= 1.2f;

                    hero.GetAttackModel().weapons[0].projectile.GetDamageModel().damage *= damageMultiplier;
                    hero.GetAttackModel().weapons[0].Rate /= rateMultiplier;
                    hero.GetAttackModel().weapons[0].projectile.pierce *= damageMultiplier;

                    hero.GetAttackModel().weapons[0].projectile.GetDamageModel().immuneBloonProperties = Il2Cpp.BloonProperties.None;
                }else
                {
                    try
                    {
                        hero.range *= 1.2f;
                        hero.GetAttackModel().range *= 1.2f;
                        hero.GetAttackModel().weapons[0].projectile.GetDamageModel().damage *= 1.2f;
                        hero.GetAttackModel().weapons[0].Rate /= 1.2f;
                        hero.GetAttackModel().weapons[0].projectile.pierce *= 1.2f;
                    }
                    catch (Exception) {}
                }
            }
        }
    }


}