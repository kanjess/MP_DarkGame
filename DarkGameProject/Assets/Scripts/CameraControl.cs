using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraControl : MonoBehaviour
{
    public bool cameraCanMove = true;
    public bool inDragScreen = false;

    public float panSpeed;
    public float zoomSensitivity;
    public float minZoom;
    public float maxZoom;

    private Vector3 dragOrigin;

    private Vector3 minCorner;
    private Vector3 maxCorner;
    private BoundsInt bounds;

    public Tilemap map; // 地图的Tilemap组件

    public Tilemap normalMap; 
    //public Tilemap upMap; 
    //public Tilemap downMap; 

    private void Awake()
    {
        normalMap = GameObject.Find("Tilemap").gameObject.GetComponent<Tilemap>();
        //upMap = GameObject.Find("UpTile").gameObject.GetComponent<Tilemap>();
        //downMap = GameObject.Find("DownTile").gameObject.GetComponent<Tilemap>();
    }



    // Start is called before the first frame update
    void Start()
    {
        TileSetup(normalMap);
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraCanMove == true && BasicAction.gameplayItemAction == false && inDragScreen == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                dragOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 difference = dragOrigin - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Camera.main.transform.position = ClampCamera(Camera.main.transform.position + difference);
            }

            // 滑轮缩放
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            Camera.main.orthographicSize -= scroll * zoomSensitivity;
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minZoom, maxZoom);
            // 在调整大小后，可能需要重新调整相机位置以保持在边界内
            Camera.main.transform.position = ClampCamera(Camera.main.transform.position);
        }
        
        



    }

    private Vector3 ClampCamera(Vector3 targetPosition)
    {
        float vertExtent = Camera.main.orthographicSize;
        float horzExtent = vertExtent * Screen.width / Screen.height;

        // 根据Tilemap边界调整这些值
        float minX = minCorner.x + horzExtent;
        float maxX = maxCorner.x - horzExtent;
        float minY = minCorner.y + vertExtent;
        float maxY = maxCorner.y - vertExtent;

        // Clamp position
        Vector3 clampedPosition = new Vector3(
            Mathf.Clamp(targetPosition.x, minX, maxX),
            Mathf.Clamp(targetPosition.y, minY, maxY),
            targetPosition.z
        );

        return clampedPosition;
    }


    public void TileSetup(Tilemap mmm)
    {
        map = mmm;
        // 获取Tilemap的边界
        bounds = map.cellBounds;
        // 转换为世界坐标的边角点
        minCorner = map.CellToWorld(new Vector3Int(bounds.xMin, bounds.yMin, 0));
        maxCorner = map.CellToWorld(new Vector3Int(bounds.xMax, bounds.yMax, 0));

        //Debug.Log(bounds.yMin + " - " + bounds.yMax);
    }

}
