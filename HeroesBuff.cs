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
using BTD_Mod_Helper.Api.ModOptions;

// =========== MAIN ===========

[assembly: MelonInfo(typeof(HeroesBuff.HeroesBuff), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace HeroesBuff;

public class HeroesBuff : BloonsTD6Mod
{
    public static readonly ModSettingBool EnableQuincyBuff = true;
    public static readonly ModSettingBool EnableGwendolinBuff = true;
    public static readonly ModSettingBool EnableSaudaBuff = true;
    public static readonly ModSettingBool EnableBenjaminBuff = true;


    public override void OnApplicationStart()
    {
        ModHelper.Msg<HeroesBuff>("HeroesBuff loaded!");
    }

    public override void OnNewGameModel(GameModel result)
    {   
        Buffs_Heroes buffs = new Buffs_Heroes();

        //Heroes Buff
        for (int i=0; i < result.towers.Count; i++)
        {
            TowerModel hero = result.towers[i];
            switch (hero.baseId)
            {
                case "Quincy":
                    if (EnableQuincyBuff) buffs.QuincyBuff(hero);
                    break;
                case "Sauda":
                    if (EnableSaudaBuff) buffs.SaudaBuff(hero);
                    break;
                case "Benjamin":
                    if (EnableBenjaminBuff) buffs.BenjaminBuff(hero);
                    break;
                case "Gwendolin":
                    if (EnableGwendolinBuff) buffs.GwendolinBuff(hero);
                    break;
                default:
                    break;
            }
        }
    }
}