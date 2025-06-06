using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerToolSelector : MonoBehaviour
{
    public enum Tool { None, Sow, Water, Harvest }
    private Tool _activeTool;

    [Header(" Elements ")]
    [SerializeField] private Image[] _toolImages;

    [Header(" Settings ")]
    [SerializeField] private Color _selectedToolColor;

    [Header(" Actions ")]
    public Action<Tool> OnToolSelected;
    private void Start()
    {
        SelectTool(0);
    }


    public void SelectTool(int toolIndex)
    {
        _activeTool = (Tool)toolIndex;
        for (int i = 0; i < _toolImages.Length; i++)
        {
            if (i == toolIndex)
            {
                _toolImages[i].color = _selectedToolColor;
            }
            else
            {
                _toolImages[i].color = Color.white;
            }
        }
        OnToolSelected?.Invoke(_activeTool);
    }

    public bool CanSow()
    {
        return _activeTool == Tool.Sow;
    }
    public bool CanWater()
    {
        return _activeTool == Tool.Water;
    }
    public bool CanHarvest()
    {
        return _activeTool == Tool.Harvest;
    }

}
