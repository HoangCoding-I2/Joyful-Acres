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
    private int _tilesSown;
    private void Start()
    {
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
        Debug.Log("Field Fully Sown");
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
