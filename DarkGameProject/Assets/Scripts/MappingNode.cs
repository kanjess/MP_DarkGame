using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MappingNode
{
    public Vector3Int position; // 节点在地图上的位置
    public bool walkable; // 节点是否可行走
    public MappingNode parent; // 节点的父节点，用于最后重建路径

    public int gCost; // 从起点到当前节点的成本
    public int hCost; // 从当前节点到终点的估算成本
    public int fCost { get { return gCost + hCost; } } // 节点的总成本

    public int pathCount = 0;

    public List<Vector3Int> startEndPosList; //起1终1起2终2

    public MappingNode(Vector3Int _position, bool _walkable)
    {
        if(startEndPosList == null)
        {
            startEndPosList = new List<Vector3Int>();
        }

        position = _position;
        walkable = _walkable;
    }


    
}
