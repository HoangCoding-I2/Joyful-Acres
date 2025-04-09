using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TileFieldState { Empty, Sown, Watered}

public class CropTile : MonoBehaviour
{
    private TileFieldState _state;

    [Header(" Elements ")]
    [SerializeField] private Transform _cropParent;
    private void Start()
    {
        _state = TileFieldState.Empty;
    }
    public bool IsEmpty()
    {
        return _state == TileFieldState.Empty;
    }
    public void Sow(CropDataSO cropDataSO)
    {
        _state = TileFieldState.Sown;
        Crop crop = Instantiate(cropDataSO.CropPrefab, transform.position, Quaternion.identity, _cropParent);
    }
}
