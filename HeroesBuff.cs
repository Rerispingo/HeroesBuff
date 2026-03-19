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
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;

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
                case "Gwendolin":
                    GwendolinBuff(hero);
                    break;
                default:
                    break;
            }
        }
    }

    private void QuincyBuff(TowerModel hero)
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

    private void GwendolinBuff(TowerModel hero)
    {
        var weaponModel = hero.GetAttackModel().weapons[0];

        // Cost Buff
        hero.cost *= 1f - 0.85f;

        // Range Buff
        if (hero.tier < 11) hero.range *= 1f + 0.25f;
        else hero.range *= 1f + 0.40f;

        // Burn Damage Buff
        if (hero.tier >= 6)
        {
            var burnEffect = weaponModel.projectile.GetBehavior<AddBehaviorToBloonModel>();

            burnEffect.GetBehavior<DamageOverTimeModel>().damage *= 3f;
            if (hero.tier >= 16) burnEffect.GetBehavior<DamageOverTimeModel>().damage *= 2f;
        }

        //Damage Buffs
        weaponModel.projectile.GetDamageModel().damage += 2f;
        if (hero.tier >= 6)
        {
            weaponModel.projectile.GetDamageModel().damage += 2f;
            if (hero.tier >= 12)
            {
                weaponModel.projectile.GetDamageModel().damage += 1f;
                if (hero.tier >= 18)
                {
                    weaponModel.projectile.GetDamageModel().damage *= 1f;
                }
            }
        }

        //Attack Speed Buffs
        if (hero.tier < 10) weaponModel.rate /= 1f + 0.25f;
        else if (hero.tier < 15) weaponModel.rate /= 1f + 75f;
        else weaponModel.rate /= 1f + 1f;

        //Cocktail Ability Buffs   
        if (hero.tier >= 3)
        {
            AbilityModel cocktailAbility = hero.GetBehavior<AbilityModel>("AbilityModel_WallOfFire");
         
            cocktailAbility.Cooldown *= 1f - 0.4f;
            cocktailAbility.cooldown *= 1f - 0.4f;
        }

        // Buff confirmation
        if (hero.tier == 20) ModHelper.Msg<HeroesBuff>("Buffing " + hero.baseId + "...");
        return;
    }

    private void SaudaBuff(TowerModel hero)
    {
        float rateMultiplier = 1f + (0.1f * hero.tier);

        hero.range *= 1.2f;
        hero.GetAttackModel().range *= 1.2f;
        
        if (hero.tier >= 10) hero.GetAttackModel().weapons[0].projectile.GetDamageModel().damage += 2;
        if (hero.tier >= 20) hero.GetAttackModel().weapons[0].projectile.GetDamageModel().damage += 1;
        
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

    private void BenjaminBuff(TowerModel hero)
    {
        hero.cost -= 450;
        hero.GetBehavior<Il2CppAssets.Scripts.Models.Towers.Behaviors.PerRoundCashBonusTowerModel>().cashPerRound *= 2.5f;

        // Buff confirmation
        if (hero.tier == 20) ModHelper.Msg<HeroesBuff>("Buffing " + hero.baseId + "...");
        return;
    }
}