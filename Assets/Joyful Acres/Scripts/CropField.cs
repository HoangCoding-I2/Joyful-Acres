﻿using System;
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
    private int _tilesWatered;

    [Header(" Actions ")]
    public static Action<CropField> OnFullSown;
    public static Action<CropField> OnFullWatered;

    private void Start()
    {
        _state = TileFieldState.Empty;
        StoreTiles();
    }

    [NaughtyAttributes.Button]
    public void InstantlySowTiles()
    {
        for (int i = 0; i < _cropTiles.Count; i++)
        {
            Sow(_cropTiles[i]);
        }
    }

    [NaughtyAttributes.Button]
    public void InstantlyWaterTiles()
    {
        for (int i = 0; i < _cropTiles.Count; i++)
        {
            Water(_cropTiles[i]);
        }
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
    public void WaterCollidedCallback(Vector3[] waterPositions)
    {
        for (int i = 0; i < waterPositions.Length; i++)
        {
            CropTile closestCropTile = GetClosestCropTile(waterPositions[i]);
            if (closestCropTile == null)
            {
                continue;
            }
            if (!closestCropTile.IsSown())
            {
                continue;
            }
            Water(closestCropTile);

        }
    }
    private void Water(CropTile cropTile)
    {
        cropTile.Water();
        _tilesWatered++;
        if (_tilesWatered == _cropTiles.Count)
        {
            FieldFullyWatered();
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
    private void FieldFullyWatered()
    {
        _state = TileFieldState.Watered;
        OnFullWatered?.Invoke(this);
        Debug.Log("full nuoc");
    }


    public bool IsEmpty()
    {
        return _state == TileFieldState.Empty;
    }
    public bool IsSown()
    {
        return _state == TileFieldState.Sown;
    }
    public bool IsWatered()
    {
        return _state == TileFieldState.Watered;
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
