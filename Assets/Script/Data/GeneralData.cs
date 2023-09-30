using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GeneralData", menuName = "ScriptableObjects/GeneralData", order = 1)]
public class GeneralData : ScriptableObject
{
    [Header("MAP STATS")]
    public int width;
    public int height;
    public int tileRatio;
    public Tile tilePrefab;
    public float tileFrequency;
    
    [Header("PLAYER STATS")]
    public int playerMaxHealth;
    public int playerNormalSpeed;

}
