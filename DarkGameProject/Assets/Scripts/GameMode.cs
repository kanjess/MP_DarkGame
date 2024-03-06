using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    public int companyMoney = 0;

    //升级相关
    public int playerLevel = 1;
    public int playerExp = 0;
    public int basicLevelUpExp = 1000;
    public int levelUpExp = 1000;
    public float levelUpExpIncreaseValue = 0.5f;

    //升级解锁
    public List<GameObject> gameItemList;

    private DesignPanel designPanel;
    //数据统计
    private float statisticsInterval;
    private float statisticsTiming = 0f;
    //全局
    public int cumulativeRevenue = 0;
    public int cumulativePlayers = 0;
    public float averageRetention = 0f;
    public float averagePeymentConversion = 0f;
    public float averageRevenuePerPlayer = 0f;
    //留存
    public float retentionRateA = 0f;
    public float retentionRateS = 0f;
    public List<float> retentionRateList;
    //付费率统计
    public float purchaseRateA = 0f;
    public float purchaseRateS = 0f;
    public List<float> purchaseRateList;
    //每日付费额
    public int revenuePerTime = 0;
    public List<int> revenuePerTimeList;



    private void Awake()
    {
        maxPlayer = 10;

        playerCreateInterval = 1f;

        playerItemLayer = GameObject.Find("PlayerObject").gameObject;

        designPanel = GameObject.Find("Canvas").gameObject.GetComponent<DesignPanel>();

        gameItemList = new List<GameObject>();

        //统计
        statisticsInterval = 15f;  //统计间隔
        retentionRateList = new List<float>();
        purchaseRateList = new List<float>();
        revenuePerTimeList = new List<int>();


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

    //生成新玩家
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
                cumulativePlayers++;
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

            //留存率
            float rRate = 0f;
            if (retentionRateA != 0)
            {
                rRate = retentionRateS / retentionRateA;
                rRate = 1 - rRate;
            }
            retentionRateList.Add(rRate);
            retentionRateA = 0f;
            retentionRateS = 0f;

            //付费率
            float pRate = 0f;
            if (purchaseRateA != 0)
            {
                pRate = purchaseRateS / purchaseRateA;
            }
            purchaseRateList.Add(pRate);
            purchaseRateA = 0f;
            purchaseRateS = 0f;

            //每日付费额
            revenuePerTimeList.Add(revenuePerTime);
            revenuePerTime = 0;

            //全局平均
            averageRetention = retentionRateList.Average();
            averagePeymentConversion = purchaseRateList.Average();
            averageRevenuePerPlayer = (float)cumulativeRevenue / (float)cumulativePlayers;

        }

        //绘制
        //统计报表更新
        designPanel.PurchaseRateChart();
        designPanel.AverageRevenueChart();
    }

}
