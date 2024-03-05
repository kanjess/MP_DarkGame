using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    public int maxPlayer;

    public bool gameDynamicProcess = false;
    public bool gameProcessPause = false;

    public GameObject playerBornPoint;
    public GameObject playerStartObject; //GameplayItem_999

    public GameObject playerItem;

    private GameObject playerItemLayer;

    private float playerCreateInterval;
    private float playerCreateTiming = 0f;

    public float companyMoney = 0f;

    //升级相关
    public int playerLevel = 1;
    public int playerExp = 0;
    public int basicLevelUpExp = 1000;
    public int levelUpExp = 1000;
    public float levelUpExpIncreaseValue = 0.5f;

    //升级解锁
    public List<GameObject> gameItemList;

    //数据统计
    private float statisticsInterval;
    private float statisticsTiming = 0f;
    //付费率统计
    public float purchaseRateA = 0f;
    public float purchaseRateS = 0f;
    public List<float> purchaseRateList;


    private void Awake()
    {
        maxPlayer = 10;

        playerCreateInterval = 1f;

        playerItemLayer = GameObject.Find("PlayerObject").gameObject;

        gameItemList = new List<GameObject>();

        //统计
        statisticsInterval = 15f;  //统计间隔
        purchaseRateList = new List<float>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameDynamicProcess == true && gameProcessPause == false)
        {
            GameDynamicProcessPlay();

            StatisticsCalculating();
        }
    }

    void GameDynamicProcessPlay()
    {
        playerCreateTiming += Time.deltaTime;

        if(playerCreateTiming >= playerCreateInterval)
        {
            playerCreateTiming = 0f;

            if (playerItemLayer.transform.childCount < maxPlayer)
            {
                GameObject pItem = Instantiate(playerItem) as GameObject;
                pItem.transform.SetParent(playerItemLayer.transform);
                pItem.transform.position = playerBornPoint.transform.position;
                pItem.GetComponent<PlayerItem>().PlayerDetailSet(playerStartObject);
            }    
        }
    }

    void StatisticsCalculating()
    {
        statisticsTiming += Time.deltaTime;

        if(statisticsTiming >= statisticsInterval)
        {
            statisticsTiming = 0f;

            //统计结果计算
            //付费率
            float pRate = 0f;
            if (purchaseRateA != 0)
            {
                pRate = purchaseRateS / purchaseRateA;
            }
            purchaseRateList.Add(pRate);
            purchaseRateA = 0f;
            purchaseRateS = 0f;

            //Debug.Log(pRate);
               
        }
    }

}
