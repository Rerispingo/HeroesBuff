global using BTD_Mod_Helper.Extensions;
using MelonLoader;
using BTD_Mod_Helper;
using HeroesBuff;
using Il2CppAssets.Scripts.Models;
using System;
using Il2CppAssets.Scripts.Models.Towers;

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
            TowerModel hero = result.towers[i];

            if (hero.IsHero())
            {
                if (hero.baseId == "Sauda") SaudaBuff(hero);
            }
        }
    }

    public void SaudaBuff(TowerModel hero)
    {
        float damageMultiplier = 1f + (0.1f * hero.tier);
        float rateMultiplier = 1f + (0.1f * hero.tier);

        hero.range *= 1.2f;
        hero.GetAttackModel().range *= 1.2f;
        
        if (hero.tier >= 10) hero.GetAttackModel().weapons[0].projectile.GetDamageModel().damage += 2;
        if (hero.tier >= 20) hero.GetAttackModel().weapons[0].projectile.GetDamageModel().damage += 2;
        
        hero.GetAttackModel().weapons[0].projectile.pierce += 2;
        hero.GetAttackModel().weapons[0].Rate /= rateMultiplier;

        if (hero.tier >= 3) hero.GetAttackModel().weapons[0].projectile.GetDamageModel().immuneBloonProperties = Il2Cpp.BloonProperties.None;

        ModHelper.Msg<HeroesBuff>("Buffing " + hero.baseId + "...");
        return;
    }
}