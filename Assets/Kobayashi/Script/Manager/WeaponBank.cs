using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBank : MonoBehaviour
{
    public enum BossWeapon
    {
        ThrowWeapon,
        FallWeapon,
        SlashWeapon,
        SentryAmo,
        SpecialWeapon,
    }

    private static readonly string[] weaponList =
    {
        "BossWeapon/ThrowWeapon",
        "BossWeapon/FallWeapon",
        "BossWeapon/SlashWeapon",
        "BossWeapon/SentryAmo",
        "BossWeapon/SpecialWeapon",
        "NULL_WEAPON",
    };

    public static WeaponBaseClass TryGetWeapon(BossWeapon weapon)
    {

        WeaponBaseClass WeaponPrefab = Resources.Load<WeaponBaseClass>(weaponList[(int)weapon]);
        if(WeaponPrefab != null)
        {
            return WeaponPrefab;
        }
        else
        {
            return null;
        }
    }
}
