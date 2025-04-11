using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TileFieldState { Empty, Sown, Watered}

public class CropTile : MonoBehaviour
{
    private TileFieldState _state;

    [Header(" Elements ")]
    [SerializeField] private Transform _cropParent;
    [SerializeField] private MeshRenderer _tileRenderer;
    private Crop _crop;
    private void Start()
    {
        _state = TileFieldState.Empty;
    }
    
    public bool IsEmpty()
    {
        return _state == TileFieldState.Empty;
    }
    public bool IsSown()
    {
        return _state == TileFieldState.Sown;
    }
    public void Sow(CropDataSO cropDataSO)
    {
        _state = TileFieldState.Sown;
        _crop = Instantiate(cropDataSO.CropPrefab, transform.position, Quaternion.identity, _cropParent);
    }
    public void Water()
    {
        _state = TileFieldState.Watered;
        _crop.ScaleUp();
        _tileRenderer.gameObject.LeanColor(Color.white * .3f, 1);

        /*
         * cách thủ công có thể áp dụng cho crop thủ công như cách này
        StartCoroutine(ColorTileCoroutine());
        */
    }
    /*
    IEnumerator ColorTileCoroutine()
    {
        float duration = 1f;
        float timer = 0f;
        while (timer < duration)
        {
            float t = timer / duration;
            Color lerpedColor = Color.Lerp(Color.white, Color.white * .3f,t);
            _tileRenderer.material.color = lerpedColor;
            timer += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }
    */
}
