using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UfoData{

    public int ufoDurability;
    public float ufoSpeed;
    public float durEffectValue;

    public UfoData(UfoControl ufoData)
    {
        ufoDurability = ufoData.ufoDurability;
        ufoSpeed = ufoData.ufoSpeed;
        durEffectValue = ufoData.durEffectValue;
    }
}

[System.Serializable]
public class GameData
{
    public int spaceMineValue;
    public int spaceMineForDurUpgrade;
    public int spaceMineForSpeedUpgrade;

    public GameData(GameControl gameData)
    {
        spaceMineValue = gameData.spaceMineValue;
        spaceMineForDurUpgrade = gameData.spaceMineForDurUpgrade;
        spaceMineForSpeedUpgrade = gameData.spaceMineForSpeedUpgrade;
    }

}
