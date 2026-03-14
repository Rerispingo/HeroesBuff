global using BTD_Mod_Helper.Extensions;
using MelonLoader;
using BTD_Mod_Helper;
using HeroesBuff;
using Il2CppAssets.Scripts.Models;
using System;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Data.Behaviors.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;

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
                if (hero.baseId == "Quincy") QuincyBuff(hero);
                if (hero.baseId == "Sauda") SaudaBuff(hero);
                if (hero.baseId == "Benjamin") BenjaminBuff(hero);
            }
        }
    }

    public void QuincyBuff(TowerModel hero)
    {
        hero.range *= 1.4f;
        hero.GetAttackModel().range *= 1.4f;

        if (hero.tier >= 4) hero.GetAttackModel().weapons[0].projectile.GetDamageModel().damage += 1;
        if (hero.tier >= 10) hero.GetAttackModel().weapons[0].projectile.GetDamageModel().damage += 1;
        if (hero.tier >= 15) hero.GetAttackModel().weapons[0].projectile.GetDamageModel().damage += 2;

        if (hero.tier >= 2) hero.GetAttackModel().weapons[0].projectile.pierce += 1;
        if (hero.tier >= 9) hero.GetAttackModel().weapons[0].projectile.pierce += 2;
        if (hero.tier >= 15) hero.GetAttackModel().weapons[0].projectile.pierce += 2;

        if (hero.tier >= 4) hero.GetAttackModel().weapons[0].projectile.GetDamageModel().immuneBloonProperties = Il2Cpp.BloonProperties.None;

        if (hero.tier == 20) ModHelper.Msg<HeroesBuff>("Buffing " + hero.baseId + "...");
        return;
    }

    public void SaudaBuff(TowerModel hero)
    {
        float rateMultiplier = 1f + (0.1f * hero.tier);

        hero.range *= 1.25f;
        hero.GetAttackModel().range *= 1.25f;
        
        if (hero.tier >= 10) hero.GetAttackModel().weapons[0].projectile.GetDamageModel().damage += 2;
        if (hero.tier >= 20) hero.GetAttackModel().weapons[0].projectile.GetDamageModel().damage += 2;
        
        hero.GetAttackModel().weapons[0].projectile.pierce += 2;
        hero.GetAttackModel().weapons[0].Rate /= rateMultiplier;

        if (hero.tier >= 3) hero.GetAttackModel().weapons[0].projectile.GetDamageModel().immuneBloonProperties = Il2Cpp.BloonProperties.None;

        if (hero.tier == 20) ModHelper.Msg<HeroesBuff>("Buffing " + hero.baseId + "...");
        return;
    }

    public void BenjaminBuff(TowerModel hero)
    {
        hero.cost -= 450;
        hero.GetBehavior<Il2CppAssets.Scripts.Models.Towers.Behaviors.PerRoundCashBonusTowerModel>().cashPerRound *= 2.5f;

        if (hero.tier == 20) ModHelper.Msg<HeroesBuff>("Buffing " + hero.baseId + "...");
        return;
    }
}