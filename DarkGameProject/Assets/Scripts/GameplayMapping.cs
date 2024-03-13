using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
//using System.Collections.Generic;

public class GameplayMapping : MonoBehaviour
{
    public GameObject pathItem;
    private GameObject pathLayer;

    public Tilemap basicTilemap; // 引用你的Tilemap组件
    public Tilemap illegalTilemap; // 引用你的Tilemap组件

    public List<Vector3Int> mapIllegalList;

    //主gameplay列表
    public List<GameObject> mainGameplayItemList;
    //dark列表
    public List<GameObject> darkGameplayItemList;

    private GameObject gameplayObjectLayer;

    //路点列表
    public List<Vector3Int> roadPointList;


    private void Awake()
    {
        basicTilemap = GameObject.Find("Tilemap").gameObject.GetComponent<Tilemap>();
        illegalTilemap = GameObject.Find("IllegalTile").gameObject.GetComponent<Tilemap>();
        gameplayObjectLayer = GameObject.Find("GameplayObject").gameObject;
        pathLayer = GameObject.Find("RoadObject").gameObject;

        if (mainGameplayItemList == null)
        {
            mainGameplayItemList = new List<GameObject>();
        }
        if(roadPointList == null)
        {
            roadPointList = new List<Vector3Int>();
        }
        if(darkGameplayItemList == null)
        {
            darkGameplayItemList = new List<GameObject>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(mapIllegalList == null)
        {
            mapIllegalList = new List<Vector3Int>();
        }
        TileMapCheck();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(mainGameplayItemList.Count + " - " + darkGameplayItemList.Count);
    }

    void TileMapCheck()
    {
        BoundsInt bounds = illegalTilemap.cellBounds; // 获取Tilemap的边界
        TileBase[] allTiles = illegalTilemap.GetTilesBlock(bounds); // 获取边界内所有Tile的数组

        //筛选非法格子坐标
        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    // 如果Tile是Tile类型，则获取其sprite名称
                    if (tile is Tile)
                    {
                        //Debug.Log("Tile at position " + new Vector3Int(x + bounds.xMin, y + bounds.yMin, 0) + " has sprite: " + ((Tile)tile).sprite.name);
                        //检查此格是否合法
                        if(((Tile)tile).sprite.name == "MapGrid_None")
                        {
                            Vector3Int po = new Vector3Int(x + bounds.xMin + 1, y + bounds.yMin + 1, 0); //因为地图位移=>坐标调整
                            if(mapIllegalList.Contains(po) == false)
                            {
                                mapIllegalList.Add(po);
                                //Debug.Log(po);
                            }
                        }
                    }
                    else
                    {
                        // Tile不是Tile类型，可能是自定义Tile
                        //Debug.Log("Tile at position " + new Vector3Int(x + bounds.xMin, y + bounds.yMin, 0) + " is a custom Tile.");
                    }
                }
                else
                {
                    //Debug.Log("No tile at position " + new Vector3Int(x + bounds.xMin, y + bounds.yMin, 0));
                }
            }
        }

        //筛选基础格子坐标并为MappingNode赋值
        foreach (var pos in basicTilemap.cellBounds.allPositionsWithin)
        {
            bool walkable = !mapIllegalList.Contains(pos);
            MappingNode mappingNode = new MappingNode(pos, walkable);
            // 可以将node存储到一个Node类型的二维数组或其他数据结构中，以便后续使用
        }

    }

    public void MappingUpdate(Vector3Int st, Vector3Int tt)
    {
        List<Vector3Int> P = new List<Vector3Int>();
        P = FindPath(st, tt);
        if(P != null)
        {
            for(int i = 0; i < P.Count; i++)
            {
                //Debug.Log(P[i]);
                
            }
        }

    }

    // 实现A*算法的方法
    // 计算从A点到B点的路径
    public List<Vector3Int> FindPath(Vector3Int start, Vector3Int target)
    {
        List<MappingNode> openSet = new List<MappingNode>();
        HashSet<MappingNode> closedSet = new HashSet<MappingNode>();
        MappingNode startNode = new MappingNode(start, true);
        MappingNode targetNode = new MappingNode(target, true);

        int maxIterations = 1500; // 最大迭代次数
        int iterations = 0;

        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            iterations++;
            if (iterations > maxIterations)
            {
                //Debug.Log("寻路失败：超出最大迭代次数");
                Debug.Log("Pathfinding failed: Exceeded the maximum number of iterations");
                return new List<Vector3Int>(); // 返回一个空路径
            }

            MappingNode currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode.position == target)
            {
                return RetracePath(startNode, currentNode);
            }

            if(GetNeighbours(currentNode) != null && GetNeighbours(currentNode).Count > 0)
            {
                foreach (var neighbour in GetNeighbours(currentNode))
                {
                    if (!neighbour.walkable || closedSet.Contains(neighbour))
                    {
                        continue;
                    }

                    int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                    if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = newMovementCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, targetNode);
                        neighbour.parent = currentNode;

                        if (!openSet.Contains(neighbour))
                        {
                            openSet.Add(neighbour);
                        }
                    }
                }
            }
            else
            {
                Debug.Log("无法找到相邻合法点");
                return new List<Vector3Int>(); // 返回一个空路径
            }
        }

        return new List<Vector3Int>(); // 如果找不到路径，返回空列表
    }

    // 获取节点的所有邻居
    List<MappingNode> GetNeighbours(MappingNode mappingNode)
    {
        List<MappingNode> neighbours = new List<MappingNode>();

        // 仅考虑上下左右四个方向
        Vector3Int[] directions = new Vector3Int[]
        {
        new Vector3Int(0, 1, 0), // 上
        new Vector3Int(0, -1, 0), // 下
        new Vector3Int(-1, 0, 0), // 左
        new Vector3Int(1, 0, 0) // 右
        };

        foreach (var dir in directions)
        {
            Vector3Int neighbourPosition = mappingNode.position + dir;
            if (basicTilemap.cellBounds.Contains(neighbourPosition) && !mapIllegalList.Contains(neighbourPosition))
            {
                neighbours.Add(new MappingNode(neighbourPosition, true));
            }
        }

        return neighbours;
    }

    // 重建路径
    List<Vector3Int> RetracePath(MappingNode startNode, MappingNode endNode)
    {
        List<Vector3Int> path = new List<Vector3Int>();
        MappingNode currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode.position);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        return path;
    }

    // 计算两个节点之间的距离
    int GetDistance(MappingNode nodeA, MappingNode nodeB)
    {
        int distX = Mathf.Abs(nodeA.position.x - nodeB.position.x);
        int distY = Mathf.Abs(nodeA.position.y - nodeB.position.y);

        return distX + distY; // 因为只允许上下左右移动，所以使用曼哈顿距离
    }

    //地图路网坐标获取
    public void AllMapRoadListNew()
    {
        roadPointList = new List<Vector3Int>();

        for (int i = 0; i < pathLayer.transform.childCount; i++)
        {
            GameObject pItem = pathLayer.transform.GetChild(i).gameObject;
            Vector3Int pPos = new Vector3Int(Mathf.RoundToInt(pItem.transform.position.x), Mathf.RoundToInt(pItem.transform.position.y), Mathf.RoundToInt(pItem.transform.position.z));
            if(roadPointList.Contains(pPos) == false)
            {
                roadPointList.Add(pPos);
            }
            
        }
    }


    
}

