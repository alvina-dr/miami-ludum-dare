using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Tile : MonoBehaviour
{
    #region Properties
    [Header("COMPONENTS")]
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private List<MeshRenderer> meshList = new List<MeshRenderer>();
    [SerializeField] private MeshRenderer meshPhantom;
    public bool built = false;
    public enum TileState
    {
        Empty = 0,
        Built = 1
    }
    public TileState state;
    #endregion

    #region Methods
    public void BuildTile()
    {
        state = TileState.Built;
        mesh = meshList[Random.Range(0, meshList.Count)];
        built = true;
        mesh.transform.localScale = Vector3.zero;
        mesh.gameObject.SetActive(true);
        HidePhantomTile();
        mesh.transform.DOScale(1.1f, .2f).OnComplete(() =>
        {
            mesh.transform.DOScale(1f, .2f);
        });
    }

    public void DestroyTile()
    {
        built = false;
        state = TileState.Empty;
        mesh.transform.DOScale(1.1f, .2f).OnComplete(() =>
        {
            mesh.transform.DOScale(0f, .2f).OnComplete(() =>
            {
                mesh.gameObject.SetActive(false);
            });
        });
    }

    public void ShowPhantomTile()
    {
        meshPhantom.transform.localScale = Vector3.zero;
        meshPhantom.gameObject.SetActive(true);
        meshPhantom.transform.DOScale(1.1f, .2f).OnComplete(() =>
        {
            meshPhantom.transform.DOScale(1f, .2f);
        });
    }

    public void HidePhantomTile()
    {
        meshPhantom.transform.DOScale(1.1f, .2f).OnComplete(() =>
        {
            meshPhantom.transform.DOScale(0f, .2f);
            meshPhantom.gameObject.SetActive(false);
        });
    }

    #endregion
}
