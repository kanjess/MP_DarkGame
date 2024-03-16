using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameMode : MonoBehaviour
{
    public int maxPlayer;
    public int maxPlayerAdd;
    public int mainRoadDistance = 0;

    //在场效果
    public List<int> globalEffectObject;  //生效中的全局效果（进场后添加）
    //public List<int> triggerEffectObject;  //生效中的触发效果（触发后添加）
    public List<List<float>> globalRetentionEffectList;  //全局留存效果
    public List<List<float>> globalSocialBoundEffectList;  //全局留存效果
    public List<List<float>> globalPayingRateEffectList;  //全局付费率效果
    public List<List<float>> globalPayingAmountEffectList;  //全局付费额效果
    public List<List<float>> globalMoodEffectList;  //全局心情效果
    public float globalMoodChange;

    public bool gameDynamicProcess = false;
    public bool gameProcessPause = false;

    public GameObject playerBornPoint;
    public GameObject playerStartObject; //GameplayItem_999

    public GameObject playerItem;

    private GameObject playerItemLayer;

    private float playerCreateInterval;
    private float playerCreateTiming = 0f;

    public float companyMoney = 0;

    //升级相关
    public int playerLevel = 1;
    public int playerExp = 0;
    public int basicLevelUpExp = 1000;
    public int levelUpExp = 1000;
    public float levelUpExpIncreaseValue = 0.5f;

    //升级解锁
    public List<GameObject> gameItemList;

    private DesignPanel designPanel;
    private GameplayMapping gameplayMapping;
    private GameplayEffect gameplayEffect;

    //数据统计
    public float statisticsInterval;
    private float statisticsTiming = 0f;
    public bool startStatistics = false;
    //全局
    public float cumulativeRevenue = 0;
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
    public float revenuePerTime = 0;
    public List<float> revenuePerTimeList;
    //每日平均总人数
    public int usersPerTime = 0;
    public int losingUsersPerTime = 0;
    public List<int> usersPerTimeList;
    //每用户付费
    public List<float> revenuePerUsersList;
    //生命周期lifetime
    public int userBatchOrder = 0;
    public List<List<GameObject>> userObjectPerTimeListList;
    public List<List<float>> userLifePerTimeListList;
    public List<float> userLifetimeList;
    //item贡献比例统计
    public List<List<float>> contributionOfLivetime;
    public List<List<float>> contributionOfRevenue;
    public List<List<float>> sortedContributionOfLivetime;
    public List<List<float>> sortedContributionOfRevenue;
    //CLV
    public List<float> clvList;
    public float lastCLV = 0f;


    private void Awake()
    {
        maxPlayer = 20;  //20
        maxPlayerAdd = 2;

        playerCreateInterval = 1f;

        playerItemLayer = GameObject.Find("PlayerObject").gameObject;

        designPanel = GameObject.Find("Canvas").gameObject.GetComponent<DesignPanel>();
        gameplayMapping = this.gameObject.GetComponent<GameplayMapping>();
        gameplayEffect = this.gameObject.GetComponent<GameplayEffect>();

        gameItemList = new List<GameObject>();

        globalEffectObject = new List<int>();
        //triggerEffectObject = new List<int>();

        //统计
        statisticsInterval = 15f;  //统计间隔
        retentionRateList = new List<float>();
        purchaseRateList = new List<float>();
        revenuePerTimeList = new List<float>();
        usersPerTimeList = new List<int>();
        revenuePerUsersList = new List<float>();

        userBatchOrder = 0;
        List<GameObject> uoptList = new List<GameObject>();
        userObjectPerTimeListList = new List<List<GameObject>>();
        userObjectPerTimeListList.Add(uoptList);
        List<float> ulptList = new List<float>();
        userLifePerTimeListList = new List<List<float>>();
        userLifePerTimeListList.Add(ulptList);
        userLifetimeList = new List<float>();

        contributionOfLivetime = new List<List<float>>();
        contributionOfRevenue = new List<List<float>>();
        sortedContributionOfLivetime = new List<List<float>>();
        sortedContributionOfRevenue = new List<List<float>>();

        clvList = new List<float>();
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

            if (startStatistics == true)
            {
                StatisticsCalculating();
            }
            
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
                pItem.GetComponent<PlayerItem>().batchOrder = userBatchOrder;
                cumulativePlayers++;

                if(cumulativePlayers >= 60 && startStatistics == false)  //1秒1个
                {
                    designPanel.ReportBtnIn(1);
                }
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

            //每日平均总人数
            usersPerTime = playerItemLayer.transform.childCount + losingUsersPerTime;
            usersPerTimeList.Add(usersPerTime);
            losingUsersPerTime = 0;
            usersPerTime = 0;

            //平均用户付费
            float rpu = (float)revenuePerTimeList[revenuePerTimeList.Count - 1] / (float)usersPerTimeList[usersPerTimeList.Count - 1];
            revenuePerUsersList.Add(rpu);

            //用户lifetime
            userBatchOrder++;
            List<GameObject> uoptList = new List<GameObject>();
            userObjectPerTimeListList.Add(uoptList);
            List<float> ulptList = new List<float>();
            userLifePerTimeListList.Add(ulptList);
            //是否显示
            for(int i = 0; i < userObjectPerTimeListList.Count; i++)
            {
                if(userObjectPerTimeListList[i].Count == 0 && userLifePerTimeListList[i].Count > 0)  //该时间段玩家已全部销毁
                {
                    float lifeT = userLifePerTimeListList[i].Average();
                    if(i + 1 > userLifetimeList.Count)
                    {
                        userLifetimeList.Add(lifeT);
                    }      
                }
                else if (userObjectPerTimeListList[i].Count > 0 || userLifePerTimeListList[i].Count == 0)
                {
                    break;
                }
            }

            //贡献比例 计算 & 排序
            List<List<float>> calContributionOfLivetime = new List<List<float>>();
            List<List<float>> calContributionOfRevenue = new List<List<float>>();
            sortedContributionOfLivetime = new List<List<float>>();
            sortedContributionOfRevenue = new List<List<float>>();
            //先求和
            if(contributionOfLivetime.Count > 0)
            {
                for (int i = 0; i < contributionOfLivetime.Count; i++)
                {
                    List<float> listIDEffect = contributionOfLivetime[i];
                    float itemID = listIDEffect[0];
                    float effectV = listIDEffect[1];

                    bool hasContain = false;
                    for (int aa = 0; aa < calContributionOfLivetime.Count; aa++)
                    {
                        if (calContributionOfLivetime[aa][0] == itemID)
                        {
                            //拥有
                            hasContain = true;
                            calContributionOfLivetime[aa][1] += effectV;
                            break;
                        }
                    }
                    if (hasContain == false)
                    {
                        calContributionOfLivetime.Add(listIDEffect);
                    }
                }
            }
            if(contributionOfRevenue.Count > 0)
            {
                for (int i = 0; i < contributionOfRevenue.Count; i++)
                {
                    List<float> listIDEffect = contributionOfRevenue[i];
                    float itemID = listIDEffect[0];
                    float effectV = listIDEffect[1];

                    bool hasContain = false;
                    for (int aa = 0; aa < calContributionOfRevenue.Count; aa++)
                    {
                        if (calContributionOfRevenue[aa][0] == itemID)
                        {
                            //拥有
                            hasContain = true;
                            calContributionOfRevenue[aa][1] += effectV;
                            break;
                        }
                    }
                    if (hasContain == false)
                    {
                        calContributionOfRevenue.Add(listIDEffect);
                    }
                }
            }
            //再排序
            if(calContributionOfLivetime.Count > 0)
            {
                sortedContributionOfLivetime = calContributionOfLivetime.OrderByDescending(listSingle => listSingle[1]).ToList();
            }
            if(calContributionOfRevenue.Count > 0)
            {
                sortedContributionOfRevenue = calContributionOfRevenue.OrderByDescending(listSingle => listSingle[1]).ToList();
            }
            contributionOfLivetime = new List<List<float>>();
            contributionOfRevenue = new List<List<float>>();

            //CLV
            if(userLifetimeList.Count > clvList.Count)
            {
                float lf = userLifetimeList[userLifetimeList.Count - 1];
                float cv = revenuePerUsersList[userLifetimeList.Count - 1];

                clvList.Add(lf * cv);

                lastCLV = clvList[clvList.Count - 1];
            }

            //全局平均
            averageRetention = retentionRateList.Average();
            averagePeymentConversion = purchaseRateList.Average();
            averageRevenuePerPlayer = (float)cumulativeRevenue / (float)cumulativePlayers;

        }

        //绘制
        //统计报表更新
        designPanel.PurchaseRateChart();
        designPanel.AverageRevenuePerUserChart();
        designPanel.RetentionRateChart();
        designPanel.AverageLifetimeChart();
        designPanel.LifetimePieChart();
        designPanel.RevenuePieChart();
        designPanel.CustomerLifetimeValueChart();
    }

    //Effect检测和入库（全局效果）
    public void ObjectGlobalEffectListCreate()
    {
        globalEffectObject = new List<int>();
        //triggerEffectObject = new List<int>();

        //筛选item列表，分别装入global列表
        for(int i = 0; i < gameplayMapping.mainGameplayItemList.Count; i++)
        {
            GameObject gItem = gameplayMapping.mainGameplayItemList[i];

            if(gItem.GetComponent<GameplayItem>().isGlobalEffect == true)
            {
                globalEffectObject.Add(gItem.GetComponent<GameplayItem>().itemID);
            }

        }

        for (int i = 0; i < gameplayMapping.darkGameplayItemList.Count; i++)
        {
            GameObject gItem = gameplayMapping.darkGameplayItemList[i];

            if (gItem.GetComponent<GameplayItem>().isGlobalEffect == true)
            {
                globalEffectObject.Add(gItem.GetComponent<GameplayItem>().itemID);
            }
        }

        //全局各效果入表
        globalRetentionEffectList = new List<List<float>>();
        globalSocialBoundEffectList = new List<List<float>>();
        globalPayingRateEffectList = new List<List<float>>();
        globalPayingAmountEffectList = new List<List<float>>();
        globalMoodEffectList = new List<List<float>>();

        globalMoodChange = 0f;

        for(int i = 0; i < globalEffectObject.Count; i++)
        {
            float retentionE = gameplayEffect.GameItemEffect(globalEffectObject[i], "retention");
            float socialBoundE = gameplayEffect.GameItemEffect(globalEffectObject[i], "socialBound");
            float payingRateE = gameplayEffect.GameItemEffect(globalEffectObject[i], "payingRate");
            float payingAmountE = gameplayEffect.GameItemEffect(globalEffectObject[i], "payingAmount");
            float moodE = gameplayEffect.GameItemEffect(globalEffectObject[i], "mood");

            if(retentionE != 0f)
            {
                List<float> effectL = new List<float>();
                effectL.Add(globalEffectObject[i]);
                effectL.Add(retentionE);

                globalRetentionEffectList.Add(effectL);
            }

            if (socialBoundE != 0f)
            {
                List<float> effectL = new List<float>();
                effectL.Add(globalEffectObject[i]);
                effectL.Add(socialBoundE);

                globalSocialBoundEffectList.Add(effectL);
            }

            if (payingRateE != 0f)
            {
                List<float> effectL = new List<float>();
                effectL.Add(globalEffectObject[i]);
                effectL.Add(payingRateE);

                globalPayingRateEffectList.Add(effectL);
            }

            if (payingAmountE != 0f)
            {
                List<float> effectL = new List<float>();
                effectL.Add(globalEffectObject[i]);
                effectL.Add(payingAmountE);

                globalPayingAmountEffectList.Add(effectL);
            }

            if (moodE != 0f)
            {
                List<float> effectL = new List<float>();
                effectL.Add(globalEffectObject[i]);
                effectL.Add(moodE);

                globalMoodEffectList.Add(effectL);

                globalMoodChange += moodE;
            }

        }

    }

}
