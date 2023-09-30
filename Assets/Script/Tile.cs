using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Tile : MonoBehaviour
{
    #region Properties
    [Header("COMPONENTS")]
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private MeshRenderer meshPhantom;
    public bool built = false;
    public enum TileState
    {
        Empty = 0,
        Phantom = 1,
        Built = 2
    }
    public TileState tileState = TileState.Empty;
    #endregion

    #region Methods
    public void BuildTile()
    {
        tileState = TileState.Built;
        built = true;
        mesh.transform.localScale = Vector3.zero;
        mesh.gameObject.SetActive(true);
        HidePhantomTile();
        mesh.transform.DOScale(1.1f, .2f).OnComplete(() =>
        {
            mesh.transform.DOScale(1f, .2f);
        });
    }

    public void ShowPhantomTile()
    {
        tileState = TileState.Phantom;
        meshPhantom.transform.localScale = Vector3.zero;
        meshPhantom.gameObject.SetActive(true);
        meshPhantom.transform.DOScale(1.1f, .2f).OnComplete(() =>
        {
            meshPhantom.transform.DOScale(1f, .2f);
        });
    }

    public void HidePhantomTile()
    {
        tileState = TileState.Empty;
        meshPhantom.transform.DOScale(1.1f, .2f).OnComplete(() =>
        {
            meshPhantom.transform.DOScale(0f, .2f);
            meshPhantom.gameObject.SetActive(false);
        });
    }

    #endregion
}
