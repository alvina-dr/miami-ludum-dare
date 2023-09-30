using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPCtrl : MonoBehaviour
{
    public static GPCtrl Instance { get; private set; }

    #region Properties
    public GeneralData GeneralData;
    public Player player;

    [Header("TILE MANAGER")]
    [SerializeField] private float tileTimer;
    private List<Tile> tileList = new List<Tile>();
    private Tile closestEmptyTile;
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
        SetupMap();
        tileTimer = GeneralData.tileFrequency;
    }

    private void Update()
    {
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
    }
    #endregion
}
