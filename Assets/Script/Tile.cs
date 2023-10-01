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
    [SerializeField] private MeshRenderer unsafeGround;
    public bool built = false;

    [Header("MATERIALS")]
    [SerializeField] private List<Material> unsafeMaterialList = new List<Material>();
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

    #region Unity API
    private void Start()
    {
        int rotation = Random.Range(0, 3);
        unsafeGround.transform.localEulerAngles = new Vector3(unsafeGround.transform.localEulerAngles.x, rotation * 90, unsafeGround.transform.localEulerAngles.z);
        for (int i = 0; i < meshList.Count; i++)
        {
            meshList[i].transform.localEulerAngles = new Vector3(meshList[i].transform.localEulerAngles.x, rotation * 90, meshList[i].transform.localEulerAngles.z);
        }
        unsafeGround.material = unsafeMaterialList[Random.Range(0, unsafeMaterialList.Count)];
    }
    #endregion
}
