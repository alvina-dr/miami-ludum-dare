using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPCtrl : MonoBehaviour
{
    public static GPCtrl Instance { get; private set; }

    #region Properties
    [Header("COMPONENTS")]
    public GeneralData GeneralData;
    public Player player;
    public UICtrl UICtrl;

    [Header("TILE MANAGER")]
    [SerializeField] private float tileTimer;
    private List<Tile> tileList = new List<Tile>();
    private Tile closestEmptyTile;

    [Header("SPAWN")]
    private float startTime;
    private List<float> timerList = new List<float>();
    [SerializeField] private float spawnRadius;
    [SerializeField] private List<EnemyData> enemyDataList = new List<EnemyData>();
    private int currentGameStage = 0;

    [Header("UPGRADE")]
    public List<UpgradeData> upgradeDataList = new List<UpgradeData>();

    public bool pause = false;
    #endregion

    #region
    public void SetupMap()
    {
        for (int i = 0; i < GeneralData.width; i++)
        {
            for (int j = 0; j < GeneralData.height; j++)
            {
                InstantiateTile(i, j);
            }
        }
    }

    public void InstantiateTile(int _x, int _y)
    {
        Tile _tile = Instantiate(GeneralData.tilePrefab);
        _tile.transform.position = new Vector3(_x * GeneralData.tileRatio, 0, _y * GeneralData.tileRatio);
        tileList.Add(_tile);
    }

    public Tile SearchCloseEmptyTile(Vector2 _position)
    {
        Tile closestFreeTile = null;
        float smallerDistance = 1000;
        List<Tile> _freeTileList = tileList.FindAll(x => !x.built);
        for (int i = 0; i < _freeTileList.Count; i++)
        {
            float _distance = Vector3.Distance(_freeTileList[i].transform.position, new Vector3(_position.x, 0, _position.y));
            if (smallerDistance > _distance) {
                smallerDistance = _distance;
                closestFreeTile = _freeTileList[i];
            }
        }
        return closestFreeTile;
    }

    private void SpawnEnemy(Enemy enemyPrefab, float _angle = 0, float _radiusBonus = 0, bool center = false)
    {
        if (_angle == 0) _angle = UnityEngine.Random.Range(0f, 2.0f * Mathf.PI);
        Vector3 pos = new Vector3((spawnRadius + _radiusBonus) * Mathf.Cos(_angle), 1, (spawnRadius + _radiusBonus) * Mathf.Sin(_angle));
        if (center) pos = Vector3.zero;
        Instantiate(enemyPrefab).transform.position = pos;
    }
    #endregion

    #region Unity API
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        //LOAD ENEMY
        EnemyData[] enemyDataArray = Resources.LoadAll<EnemyData>("EnemyData");
        for (int i = 0; i < enemyDataArray.Length; i++)
        {
            enemyDataList.Add(enemyDataArray[i]);
        }

        //LOAD UPGRADE
        UpgradeData[] upgradeDataArray = Resources.LoadAll<UpgradeData>("UpgradeData");
        for (int i = 0; i < upgradeDataArray.Length; i++)
        {
            upgradeDataList.Add(upgradeDataArray[i]);
        }

        startTime = Time.time;
        tileTimer = GeneralData.tileFrequency;
        foreach (EnemyData enemy in enemyDataList)
        {
            timerList.Add(enemy.spawnRate);
        }
        SetupMap();
    }

    private void Update()
    {
        if (pause) return;

        float timeSinceStart = Time.time - startTime;
        tileTimer += Time.deltaTime;
        
        Tile _closestEmptyTile = SearchCloseEmptyTile(new Vector2(player.transform.position.x, player.transform.position.z));
        if (_closestEmptyTile != closestEmptyTile)
        {
            if (closestEmptyTile != null && !closestEmptyTile.built) closestEmptyTile.HidePhantomTile();
            closestEmptyTile = _closestEmptyTile;
            closestEmptyTile.ShowPhantomTile();
        }
        if (tileTimer >= GeneralData.tileFrequency)
        {
            if (closestEmptyTile != null) closestEmptyTile.BuildTile();
            tileTimer = 0;
        }

        if (timeSinceStart > GeneralData.gameStage[currentGameStage])
        {
            if (GeneralData.gameStage.Count > currentGameStage + 1)
            {
                for (int i = 0; i < enemyDataList.Count; i++)
                {
                    enemyDataList[i].spawnTime *= GeneralData.timeRateReduction;
                }
                currentGameStage++;
            }
        }
        for (int i = 0; i < timerList.Count; i++)
        {
            if (timeSinceStart < enemyDataList[i].spawnTime)
            {
                continue;
            }
            timerList[i] -= Time.fixedDeltaTime;
            if (timerList[i] <= 0f)
            {
                timerList[i] = enemyDataList[i].spawnRate;
                SpawnEnemy(enemyDataList[i].enemyPrefab);
            }
        }
    }
    #endregion
}
