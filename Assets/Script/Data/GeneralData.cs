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
    public float tileSpawnNumber;

    [Header("PLAYER STATS")]
    public int playerMaxHealth;
    public int playerNormalSpeed;

    [Header("SPAWN STATS")]
    public float timeRateReduction;
    public List<float> gameStage = new List<float>();

    [Header("FX")]
    public GameObject deathParticles;

    [Header("SOUND")]
    public List<AudioClip> enemyDamageSound = new List<AudioClip>();
    public List<AudioClip> playerDamageSound = new List<AudioClip>();
    public List<AudioClip> gameOverSound = new List<AudioClip>();
    public List<AudioClip> menuSound = new List<AudioClip>();
    public List<AudioClip> upgradeSound = new List<AudioClip>();
}
