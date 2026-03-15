// =========== IMPORTS ===========

global using BTD_Mod_Helper.Extensions;
using MelonLoader;
using BTD_Mod_Helper;
using HeroesBuff;
using System;

using Il2CppAssets.Scripts.Models;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;

// =========== MAIN ===========

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

        hero.range *= 1.2f;
        hero.GetAttackModel().range *= 1.2f;
        
        if (hero.tier >= 10) hero.GetAttackModel().weapons[0].projectile.GetDamageModel().damage += 2;
        if (hero.tier >= 20) hero.GetAttackModel().weapons[0].projectile.GetDamageModel().damage += 2;
        
        hero.GetAttackModel().weapons[0].projectile.pierce += 1;
        hero.GetAttackModel().weapons[0].Rate /= rateMultiplier;

        if (hero.tier == 20) ModHelper.Msg<HeroesBuff>("Buffing " + hero.baseId + "...");

        //Leaping Sword Attack Buffs
        if (hero.tier >= 3)
        {
            var leapingSwordAbility = hero.GetBehavior<AbilityModel>("AbilityModel_LeapingSword");
            var leapingSwordModel = leapingSwordAbility.GetBehavior<LeapingSwordModel>();

            leapingSwordAbility.Cooldown /= 2f;
            leapingSwordAbility.cooldown /= 2f;

            var lsImpactDamageModel = leapingSwordModel.impactProjectileModel.GetDamageModel();

            lsImpactDamageModel.maxDamage *= 5;
            lsImpactDamageModel.damage *= 5;

            var lsDotDamageModel = leapingSwordModel.dotProjectileModel.GetDamageModel();
            var lsDotAgeModel = leapingSwordModel.dotProjectileModel.GetBehavior<AgeModel>();

            lsDotDamageModel.maxDamage *= 2;
            lsDotDamageModel.damage *= 2;
            lsDotAgeModel.Lifespan *= 1.5f;
        }

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