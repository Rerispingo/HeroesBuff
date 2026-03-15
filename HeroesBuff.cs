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
        //Heroes Buff
        for (int i=0; i < result.towers.Count; i++)
        {
            TowerModel hero = result.towers[i];
            switch (hero.baseId)
            {
                case "Quincy":
                    QuincyBuff(hero);
                    break;
                case "Sauda":
                    SaudaBuff(hero);
                    break;
                case "Benjamin":
                    BenjaminBuff(hero);
                    break;
                default:
                    break;
            }
        }
    }

    public void QuincyBuff(TowerModel hero)
    {
        if (hero.tier >= 15)
        {
            hero.range *= 1.75f;
            hero.GetAttackModel().range *= 1.75f;
        }else
        {
            hero.range *= 1.5f;
            hero.GetAttackModel().range *= 1.5f;
        }

        if (hero.tier >= 4) hero.GetAttackModel().weapons[0].projectile.GetDamageModel().damage += 1;
        if (hero.tier >= 15) hero.GetAttackModel().weapons[0].projectile.GetDamageModel().damage += 2;

        if (hero.tier >= 2) hero.GetAttackModel().weapons[0].projectile.pierce += 1;
        if (hero.tier >= 15) hero.GetAttackModel().weapons[0].projectile.pierce += 2;

        if (hero.tier >= 5) hero.GetAttackModel().weapons[0].projectile.GetDamageModel().immuneBloonProperties = Il2Cpp.BloonProperties.None;

        //Rapid Shot Ability Buffs
        if (hero.tier >= 3)
        {
            var rapidShotAbility = hero.GetBehavior<AbilityModel>("AbilityModel_RapidShotAbility");
            rapidShotAbility.Cooldown += 0.66f;
        }
        // Storm of Arrows Ability Buffs
        if (hero.tier >= 10)
        {
            var SOAAbility = hero.GetBehavior<AbilityModel>("AbilityModel_StormOfArrowsAbility");
            var SOAProjectile = SOAAbility.GetBehavior<ActivateAttackModel>().attacks[0].weapons[0].projectile;
            var SOADamageModel = SOAProjectile.GetBehavior<CreateProjectileOnExhaustFractionModel>().projectile.GetDamageModel();

            SOADamageModel.damage *= 2.5f;
        }

        // Buff confirmation
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

        //Leaping Sword Ability Buffs
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

        // Buff confirmation
        if (hero.tier == 20) ModHelper.Msg<HeroesBuff>("Buffing " + hero.baseId + "...");
        return;
    }

    public void BenjaminBuff(TowerModel hero)
    {
        hero.cost -= 450;
        hero.GetBehavior<Il2CppAssets.Scripts.Models.Towers.Behaviors.PerRoundCashBonusTowerModel>().cashPerRound *= 2.5f;

        // Buff confirmation
        if (hero.tier == 20) ModHelper.Msg<HeroesBuff>("Buffing " + hero.baseId + "...");
        return;
    }
}