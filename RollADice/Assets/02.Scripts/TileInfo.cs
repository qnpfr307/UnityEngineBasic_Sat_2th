using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo : MonoBehaviour, IComparable<TileInfo>
{
    public int Id;

    protected virtual void Awake()
    {
        Id = transform.GetSiblingIndex() + 1;
    }

    public virtual void OnTile()
    {
        Debug.Log($"{Id} ĭ�� �����߽��ϴ�");
    }

    public int CompareTo(TileInfo other)
    {
        return this.Id - other.Id;
    }


    // ������ �����ε�
    public static bool operator > (TileInfo op1, TileInfo op2)
    {
        return op1.Id > op2.Id;
    }

    public static bool operator < (TileInfo op1, TileInfo op2)
    {
        return op1.Id < op2.Id;
    }
}
