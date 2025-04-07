using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropTile : MonoBehaviour
{
    public enum State { Empty, Sown, Watered}
    private State _state;

    [Header(" Elements ")]
    [SerializeField] private Transform _cropParent;
    private void Start()
    {
        _state = State.Empty;
    }
    public bool IsEmpty()
    {
        return _state == State.Empty;
    }
    public void Sow(CropDataSO cropDataSO)
    {
        _state = State.Sown;
        Crop crop = Instantiate(cropDataSO.CropPrefab, transform.position, Quaternion.identity, _cropParent);
    }
}
