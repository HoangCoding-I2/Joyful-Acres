using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropField : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Transform _tilesParent;
    private List<CropTile> _cropTiles = new List<CropTile>();

    [Header(" Settings ")]
    [SerializeField] private CropDataSO _cropDataSO;
    private TileFieldState _state;
    private int _tilesSown;

    [Header(" Actions ")]
    public static Action<CropField> OnFullSown;

    private void Start()
    {
        _state = TileFieldState.Empty;
        StoreTiles();
    }
    private void Update()
    {
        
    }
    private void StoreTiles()
    {
        for (int i = 0; i < _tilesParent.childCount; i++)
        {
            _cropTiles.Add(_tilesParent.GetChild(i).GetComponent<CropTile>());
        }
    }
    public void SeedsCollidedCallback(Vector3[] seedPositions)
    {
        for (int i = 0; i < seedPositions.Length; i++)
        {
            CropTile closestCropTile = GetClosestCropTile(seedPositions[i]);

            if (closestCropTile == null)
            {
                continue;
            }
            if (!closestCropTile.IsEmpty())
            {
                continue;
            }
            Sow(closestCropTile);
        }
    }

    private void Sow(CropTile cropTile)
    {
        cropTile.Sow(_cropDataSO);
        _tilesSown++;
        if (_tilesSown == _cropTiles.Count)
        {
            FieldFullySown();
        }
    }

    private void FieldFullySown()
    {
        _state = TileFieldState.Sown;
        OnFullSown?.Invoke(this);
    }

    public bool IsEmpty()
    {
        return _state == TileFieldState.Empty;
    }
    private CropTile GetClosestCropTile(Vector3 seedPosition)
    {
        float minDistance = 1000;
        int closestCropTileIndex = -1;

        for (int i = 0; i < _cropTiles.Count; i++)
        {
            CropTile cropTile = _cropTiles[i];
            float distanceTileSeed = Vector3.Distance(cropTile.transform.position, seedPosition);
            if (distanceTileSeed < minDistance)
            {
                minDistance = distanceTileSeed;
                closestCropTileIndex = i;
            }
        }
        if (closestCropTileIndex == -1)
        {
            return null;
        }
        return _cropTiles[closestCropTileIndex];
    }
}
