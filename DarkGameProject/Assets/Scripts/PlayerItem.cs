using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PlayerItem : MonoBehaviour
{
    private bool activePlayer;
    private bool firstTimeToActive = false;

    public bool isPromotingPlayer = false;

    private GameObject playerFaceContent;
    private GameObject playerFaceItem;
    private GameObject playerLosingFace;

    private GameMode gameMode;

    public int batchOrder;  //诞生批次，用以计算lifetime

    private float moveSpeed;
    private bool moveState = false;
    public bool moveAnime = false;
    private int moveOrder = 0;

    public GameObject currentObject;
    public GameObject currentDarkObject;
    public GameObject nextObject;
    private GameplayMapping gameplayMapping;

    //玩法事件相关
    public List<GameObject> gameplayActionObjectList;
    public List<Vector3Int> gameplayActionPosList;
    public List<bool> gameplayActionEventList;

    private List<Vector3Int> pathList;

    private Tweener movingAnime;

    private float liveTime = 0f;
    private List<float> circleLifeTimeList;
    private float cuMoney = 0f;

    public int circleNum = 0;  //圈数
    private bool circleAdd = false;

    private bool basicDataCheck = true;

    public float basicPayRate;  //基础付费率
    public float basicChurnRate;  //基础留存率
    public int basicPayAmount;  //基础付费额
    public float itemChurnRate;  //item留存率

    public float finalChurnRate = 0f; //终留存率
    public float finalPayingRate = 0f; //终付费率
    public float finalPayingAmount = 0f; //终付费额

    public float satisfactionIndex;  //满意度（心情值）
    public float socialBound = 0;  //社交绑带
    public float accountBound = 0;   //账户绑带

    private float basicChurnDampingValue;  //（基础+黑暗）留存衰减
    private float socialDampingValue;  //社交绑带衰减
    private float accountChurnDampingValue;  //账户绑带衰减

    private float basicChurnReset;
    private float socialChurnReset;
    private float accountChurnReset;

    private GameObject moneyIcon;
    private bool moneyAnime = false;
    public float basicMoneyCheckInterval;
    private float basicMoneyCheckTiming = 0f;

    public float basicLivingInterval;
    private float basicLivingTiming = 0f;

    //基础模块
    private bool gameplayAction = false;
    private float playerInbornMood;

    //exp
    private float expCheckInterval;
    private float expCheckTiming = 0f;
    private int expAdd = 0;

    //在场效果
    public List<List<float>> triggerRetentionEffectList;  //trigger留存效果
    public List<List<float>> triggerSocialBoundEffectList;  //trigger留存效果
    public List<List<float>> triggerPayingRateEffectList;  //trigger付费率效果
    public List<List<float>> triggerPayingAmountEffectList;  //trigger付费额效果

    //心情图片
    private GameObject moodPic01;
    private GameObject moodPic02;
    private GameObject moodPic03;
    private GameObject moodPic04;
    private GameObject moodPic05;
    private GameObject moodPic06;
    private GameObject moodPic07;
    private GameObject moodPic08;
    private GameObject moodPic09;


    private void Awake()
    {
        gameMode = GameObject.Find("Main Camera").gameObject.GetComponent<GameMode>();
        gameplayMapping = GameObject.Find("Main Camera").gameObject.GetComponent<GameplayMapping>();

        moveSpeed = 3f;  //像素/s
     
        expAdd = 10 * 2;   //exp  10

        pathList = new List<Vector3Int>();
        circleLifeTimeList = new List<float>();

        triggerRetentionEffectList = new List<List<float>>();
        triggerSocialBoundEffectList = new List<List<float>>();
        triggerPayingRateEffectList = new List<List<float>>();
        triggerPayingAmountEffectList = new List<List<float>>();

        GameObject playerFeedbackContent = this.gameObject.transform.Find("PlayerFeedbackContent").gameObject;
        GameObject moneyContent = playerFeedbackContent.transform.Find("MoneyContent").gameObject;
        moneyIcon = moneyContent.transform.Find("MoneyIcon").gameObject;

        playerFaceContent = this.gameObject.transform.Find("PlayerFaceContent").gameObject;
        playerLosingFace = this.gameObject.transform.Find("PlayerLosingFace").gameObject;

        moodPic01 = playerFaceContent.transform.Find("PlayerFace_0").gameObject;
        moodPic02 = playerFaceContent.transform.Find("PlayerFace_1").gameObject;
        moodPic03 = playerFaceContent.transform.Find("PlayerFace_2").gameObject;
        moodPic04 = playerFaceContent.transform.Find("PlayerFace_3").gameObject;
        moodPic05 = playerFaceContent.transform.Find("PlayerFace_4").gameObject;
        moodPic06 = playerFaceContent.transform.Find("PlayerFace_5").gameObject;
        moodPic07 = playerFaceContent.transform.Find("PlayerFace_6").gameObject;
        moodPic08 = playerFaceContent.transform.Find("PlayerFace_7").gameObject;
        moodPic09 = playerFaceContent.transform.Find("PlayerFace_8").gameObject;

        activePlayer = false;

        satisfactionIndex = 70;

        basicPayRate = 0.20f;  //基础付费率
        basicChurnRate = 0.33f;  //基础留存率 33
        basicPayAmount = 1;  //基础付费额

        basicChurnDampingValue = 3f;
        socialDampingValue = 1.6f;
        accountChurnDampingValue = 1.5f;

        playerInbornMood = 90f;  //出生心情
        satisfactionIndex = playerInbornMood;

}

    // Start is called before the first frame update
    void Start()
    {
        moneyIcon.GetComponent<SpriteRenderer>().DOFade(0f, 0f);

        expCheckTiming = expCheckInterval;

        //诞生后填充到记录中
        gameMode.userObjectPerTimeListList[batchOrder].Add(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(gameMode.gameDynamicProcess == false)
        {
            Destroy(this.gameObject);
        }

        //时间刷新
        //更新统计时间
        if (gameMode.gameDynamicProcess == true && gameMode.gameProcessPause == false)
        {
            gameMode.statisticsInterval = (gameMode.mainRoadDistance / moveSpeed) / 100f;
            int inter = gameplayMapping.mainGameplayItemList.Count - 1;
            expCheckInterval = gameMode.statisticsInterval / (inter * 4);  //exp 检测间隔
            basicLivingInterval = gameMode.statisticsInterval / (inter * 1);  //基础留存 检测间隔
            basicMoneyCheckInterval = gameMode.statisticsInterval / (inter * 2);  //基础付费 检测间隔

            //首次激活（首次经过101时激活）
            if (currentObject.gameObject.name == "GameplayItem_101(Clone)")
            {
                if (firstTimeToActive == false)
                {
                    firstTimeToActive = true;
                    activePlayer = true;

                    PlayerMoodLogic();
                }
                else if (firstTimeToActive == true)
                {
                    if (circleAdd == false)
                    {
                        circleAdd = true;
                        circleNum++;

                        circleLifeTimeList.Add(liveTime);
                    }
                }
            }
            else
            {
                if (circleAdd == true)
                {
                    circleAdd = false;
                }
            }
        }

        //心情表情
        if (gameMode.gameDynamicProcess == true && gameMode.gameProcessPause == false)
        {
            moodPic01.transform.localScale = new Vector3(0, 0, 0);
            moodPic02.transform.localScale = new Vector3(0, 0, 0);
            moodPic03.transform.localScale = new Vector3(0, 0, 0);
            moodPic04.transform.localScale = new Vector3(0, 0, 0);
            moodPic05.transform.localScale = new Vector3(0, 0, 0);
            moodPic06.transform.localScale = new Vector3(0, 0, 0);
            moodPic07.transform.localScale = new Vector3(0, 0, 0);
            moodPic08.transform.localScale = new Vector3(0, 0, 0);
            moodPic09.transform.localScale = new Vector3(0, 0, 0);

            if (satisfactionIndex >= 80)
            {
                moodPic01.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (satisfactionIndex >= 70 && satisfactionIndex < 80)
            {
                moodPic02.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (satisfactionIndex >= 60 && satisfactionIndex < 70)
            {
                moodPic03.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (satisfactionIndex >= 50 && satisfactionIndex < 60)
            {
                moodPic04.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (satisfactionIndex >= 40 && satisfactionIndex < 50)
            {
                moodPic05.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (satisfactionIndex >= 30 && satisfactionIndex < 40)
            {
                moodPic06.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (satisfactionIndex >= 20 && satisfactionIndex < 30)
            {
                moodPic07.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (satisfactionIndex >= 10 && satisfactionIndex < 20)
            {
                moodPic08.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (satisfactionIndex < 10)
            {
                moodPic09.transform.localScale = new Vector3(1, 1, 1);
            }
        }

            //累积生存时间
        if (gameMode.gameDynamicProcess == true && gameMode.gameProcessPause == false)
        {
            liveTime += Time.deltaTime;
        }

        //exp
        if (gameMode.gameDynamicProcess == true && gameMode.gameProcessPause == false)
        {
            ExpAddLogic();
        }

        //行为检测：移动 付费 留存
        if (gameMode.gameDynamicProcess == true && gameMode.gameProcessPause == false)
        {
            if(movingAnime != null)
            {
                if(movingAnime.IsPlaying() == false && moveState == true && moveAnime == true)
                {
                    movingAnime.Play();
                }
            }

            //移动
            PlayerItemMove();

            if (activePlayer == true && basicDataCheck == true)
            {
                //基础付费
                PlayerPayingCheckMethod1();
                //基础留存
                PlayerLivingMethod1();
            }
        }
        else if (gameMode.gameDynamicProcess == true && gameMode.gameProcessPause == true)
        {
            if (movingAnime != null)
            {
                if (movingAnime.IsPlaying() == true && moveState == true && moveAnime == true)
                {
                    movingAnime.Pause();
                }
            }
        }

        
        
    }

    public void PlayerDetailSet(GameObject startG)
    {
        currentObject = startG;
    }

    //角色移动 && 玩法逻辑触发
    void PlayerItemMove()
    {
        //是否有玩法动作
        if(gameplayAction == true)
        {
            //开始对应玩法的动作
            basicDataCheck = false;  //暂停基础检测

            if(moveState == false && moveAnime == false)
            {
                gameplayActionObjectList = new List<GameObject>();
                gameplayActionPosList = new List<Vector3Int>();
                gameplayActionEventList = new List<bool>();

                currentObject.GetComponent<GameplayItem>().GameplayEventLogic(this.gameObject);

                moveState = true;
                moveAnime = false;
                moveOrder = 0;
            }

            if(moveState == true && moveAnime == false)
            {
                //移动
                Vector3Int nextPoint = new Vector3Int(0, 0, 0);
                bool eventDone = false;

                if(gameplayActionPosList.Count == 1)
                {
                    eventDone = true;  //直接进入最终事件
                }
                else if(gameplayActionPosList.Count > 1)
                {
                    if (moveOrder == gameplayActionPosList.Count - 1)
                    {
                        eventDone = true;  
                    }
                    else if (moveOrder < gameplayActionPosList.Count - 1)
                    {
                        nextPoint = gameplayActionPosList[moveOrder];
                    }
                }

                if(eventDone == false)
                {
                    float distance = System.Math.Abs(Vector3.Distance(nextPoint, this.gameObject.transform.position));
                    float duration = distance / moveSpeed;

                    moveAnime = true;
                    moveOrder++;

                    movingAnime = this.gameObject.transform.DOLocalMove(nextPoint, duration).SetEase(Ease.Linear);

                    //移动后事件
                    if (gameplayActionEventList[moveOrder - 1] == true)
                    {
                        movingAnime.OnComplete(() => gameplayActionObjectList[moveOrder - 1].GetComponent<GameplayItem>().GameplayEvent(this.gameObject));
                    }

                    movingAnime.OnKill(() => moveAnime = false);
                }
                else if (eventDone == true)
                {
                    //最后事件
                    gameplayActionObjectList[gameplayActionObjectList.Count - 1].GetComponent<GameplayItem>().GameplayEvent(this.gameObject);

                    //全部搞完
                    gameplayAction = false;
                    basicDataCheck = true;
                    moveState = false;
                }
            }      
        }

        //触发移动
        if(gameplayAction == false)
        {
            if (moveState == false && moveAnime == false)
            {
                //寻找路线（优先特殊，其次普通）
                if (currentObject.GetComponent<GameplayItem>().specialOutputLinkItemList.Count > 0)
                {
                    nextObject = currentObject.GetComponent<GameplayItem>().specialOutputLinkItemList[0];
                    pathList = currentObject.GetComponent<GameplayItem>().specialOutputPathPosListList[0];
                }
                else if (currentObject.GetComponent<GameplayItem>().outputLinkItemList.Count > 0)
                {
                    nextObject = currentObject.GetComponent<GameplayItem>().outputLinkItemList[0];
                    pathList = currentObject.GetComponent<GameplayItem>().outputPathPosListList[0];
                }
                //
                if (nextObject != null && nextObject != currentObject)
                {
                    moveState = true;
                    moveAnime = false;
                    moveOrder = 0;
                }
            }

            if (moveState == true && moveAnime == false)
            {
                //移动
                Vector3Int nextPoint = new Vector3Int(0, 0, 0);

                if (moveOrder == pathList.Count)
                {
                    nextPoint = new Vector3Int(Mathf.RoundToInt(nextObject.transform.position.x), Mathf.RoundToInt(nextObject.transform.position.y), Mathf.RoundToInt(nextObject.transform.position.z));
                }
                else if (moveOrder < pathList.Count)
                {
                    nextPoint = pathList[moveOrder];
                }

                float distance = System.Math.Abs(Vector3.Distance(nextPoint, this.gameObject.transform.position));
                float duration = distance / moveSpeed;

                moveAnime = true;
                moveOrder++;

                movingAnime = this.gameObject.transform.DOLocalMove(nextPoint, duration).SetEase(Ease.Linear);
                movingAnime.OnComplete(() => moveAnime = false);
                movingAnime.OnKill(() => movingAnime = null);

                if (moveOrder > pathList.Count)
                {
                    //移动完成。
                    currentObject = nextObject;
                    gameplayAction = true;
                    moveState = false;
                }
            }
        }   
    }

    //角色付费效果
    void PlayerPaying(float pay)
    {
        if(moneyAnime == false)
        {
            moneyAnime = true;
            Sequence s = DOTween.Sequence();
            s.Insert(0f, moneyIcon.GetComponent<SpriteRenderer>().DOFade(1f, 0.2f));

            s.Insert(0f, moneyIcon.transform.DOScaleX(-1f, 0.4f));
            s.Insert(0.4f, moneyIcon.transform.DOScaleX(1f, 0.4f));
            s.Insert(0.8f, moneyIcon.transform.DOScaleX(-1f, 0.4f));
            s.Insert(1.2f, moneyIcon.transform.DOScaleX(1f, 0.4f));

            s.Insert(0f, moneyIcon.transform.DOLocalMoveY(0.6f, 2f).SetRelative()).SetEase(Ease.OutCubic);
            s.Insert(1.8f, moneyIcon.GetComponent<SpriteRenderer>().DOFade(0f, 0.2f));

            s.OnComplete(() => moneyIcon.transform.localPosition = new Vector3(0, 0, 0));
            s.OnKill(() => moneyAnime = false);

            gameMode.companyMoney += pay;
            //
            gameMode.cumulativeRevenue += pay;
            gameMode.revenuePerTime += pay;

            cuMoney += pay;

            if(isPromotingPlayer == true)
            {
                gameMode.promotingRevenue += pay;
            }
        }
        
    }

    //角色流失效果
    void PlayerLosing()
    {
        activePlayer = false;

        //流失表情的变更
        for(int i = 0; i < playerFaceContent.transform.childCount; i++)
        {
            GameObject pFaceI = playerFaceContent.transform.GetChild(i).gameObject;
            if(pFaceI.transform.localScale == new Vector3(1, 1, 1))
            {
                playerLosingFace.GetComponent<SpriteRenderer>().sprite = pFaceI.GetComponent<SpriteRenderer>().sprite;
                break;
            }
        }

        playerFaceContent.transform.localScale = new Vector3(0, 0, 0);
        playerLosingFace.transform.localScale = new Vector3(1, 1, 1);

        gameMode.losingUsersPerTime += 1;

        //lifetime添加
        int circleN = circleNum - 1; //圈数
        float halfCircle = 0f;
        if(circleLifeTimeList.Count == 1)  //1圈都没跑完
        {
            halfCircle = (liveTime - circleLifeTimeList[0]) / gameMode.statisticsInterval;
            if(halfCircle >= 1)
            {
                halfCircle = 0.9f;
            }
        }
        else   //大于1圈
        {
            float avCircleT = (circleLifeTimeList[circleLifeTimeList.Count - 1] - circleLifeTimeList[0]) / circleN;
            halfCircle = (liveTime - circleLifeTimeList[circleLifeTimeList.Count - 1]) / avCircleT;
        }
        float wholeLifetime = circleN + halfCircle;
        gameMode.userLifePerTimeListList[batchOrder].Add(wholeLifetime);
        //
        gameMode.userMoneyPerTimeListList[batchOrder].Add(cuMoney);  //死的时候加累计金钱
        //本体列表销毁
        gameMode.userObjectPerTimeListList[batchOrder].Remove(this.gameObject);

        //satisfactionIndex

        //动画
        Sequence ss = DOTween.Sequence();
        //ss.Insert(0f, playerLosingFace.transform.DOMoveZ(1f, 1f).SetRelative());
        ss.Insert(1f, playerLosingFace.transform.DOScale(new Vector3(0, 0, 0), 0.6f));
        ss.Insert(1.4f, playerLosingFace.GetComponent<SpriteRenderer>().DOFade(0f, 0.2f));

        ss.OnComplete(() => movingAnime.Kill());
        ss.OnKill(() => DestroySelf());

        //Invoke("DestroySelf", 2f);
    }

    //付费-基础监测（伴随时间）
    void PlayerPayingCheckMethod1()
    {
        basicMoneyCheckTiming += Time.deltaTime;
        if(basicMoneyCheckTiming >= basicMoneyCheckInterval)
        {
            gameMode.purchaseRateA += 1;

            //拉取全局和触发效果中的item效果, 计算payingRate和payingAmount
            //全局效果
            float itemPayingRate = 0f;
            if (gameMode.globalPayingRateEffectList.Count != 0)
            {
                for (int i = 0; i < gameMode.globalPayingRateEffectList.Count; i++)
                {
                    List<float> idEffect = gameMode.globalPayingRateEffectList[i];
                    itemPayingRate += idEffect[1];
                }
            }
            float itemPayingAmount = 0f;
            if (gameMode.globalPayingAmountEffectList.Count != 0)
            {
                for (int i = 0; i < gameMode.globalPayingAmountEffectList.Count; i++)
                {
                    List<float> idEffect = gameMode.globalPayingAmountEffectList[i];
                    itemPayingAmount += idEffect[1];
                }
            }
            //trigger
            if (triggerPayingRateEffectList.Count != 0)
            {
                for (int i = 0; i < triggerPayingRateEffectList.Count; i++)
                {
                    List<float> idEffect = triggerPayingRateEffectList[i];
                    itemPayingRate += idEffect[1];
                }
            }
            if (triggerPayingAmountEffectList.Count != 0)
            {
                for (int i = 0; i < triggerPayingAmountEffectList.Count; i++)
                {
                    List<float> idEffect = triggerPayingAmountEffectList[i];
                    itemPayingAmount += idEffect[1];
                }
            }

            finalPayingRate = basicPayRate + itemPayingRate;
            finalPayingAmount = basicPayAmount * (1 + itemPayingAmount);

            //统计贡献
            //全局
            if (gameMode.globalPayingRateEffectList.Count != 0)
            {
                for (int i = 0; i < gameMode.globalPayingRateEffectList.Count; i++)
                {
                    List<float> idEffect = gameMode.globalPayingRateEffectList[i];
                    float effectReset = (idEffect[1] / finalPayingRate) * finalPayingAmount;
                    List<float> newIDEffect = new List<float>();
                    newIDEffect.Add(idEffect[0]);
                    newIDEffect.Add(effectReset);
                    gameMode.contributionOfRevenue.Add(newIDEffect);
                }
            }
            if (gameMode.globalPayingAmountEffectList.Count != 0)
            {
                for (int i = 0; i < gameMode.globalPayingAmountEffectList.Count; i++)
                {
                    List<float> idEffect = gameMode.globalPayingAmountEffectList[i];
                    float effectReset = basicPayAmount * (1 + idEffect[1]);
                    List<float> newIDEffect = new List<float>();
                    newIDEffect.Add(idEffect[0]);
                    newIDEffect.Add(effectReset);
                    gameMode.contributionOfRevenue.Add(newIDEffect);
                }
            }
            //trigger
            if (triggerPayingRateEffectList.Count != 0)
            {
                for (int i = 0; i < triggerPayingRateEffectList.Count; i++)
                {
                    List<float> idEffect = triggerPayingRateEffectList[i];
                    float effectReset = (idEffect[1] / finalPayingRate) * finalPayingAmount;
                    List<float> newIDEffect = new List<float>();
                    newIDEffect.Add(idEffect[0]);
                    newIDEffect.Add(effectReset);
                    gameMode.contributionOfRevenue.Add(newIDEffect);
                }
            }
            if (triggerPayingAmountEffectList.Count != 0)
            {
                for (int i = 0; i < triggerPayingAmountEffectList.Count; i++)
                {
                    List<float> idEffect = triggerPayingAmountEffectList[i];
                    float effectReset = basicPayAmount * (1 + idEffect[1]);
                    List<float> newIDEffect = new List<float>();
                    newIDEffect.Add(idEffect[0]);
                    newIDEffect.Add(effectReset);
                    gameMode.contributionOfRevenue.Add(newIDEffect);
                }
            }

            //概率
            float rate = Random.Range(0f, 1f);
            if(rate <= finalPayingRate)
            {
                PlayerPaying(finalPayingAmount);
                gameMode.purchaseRateS += 1;
            }

            basicMoneyCheckTiming = 0f;
        }

    }
    //付费-玩法检测（玩法触发）
    public void PlayerPayingCheckMethod2(int itemID, float payR, float payM)
    {
        gameMode.purchaseRateA += 1;

        //拉取全局和触发效果中的item效果, 计算payingRate和payingAmount
        //全局效果
        float itemPayingRate = 0f;
        if (gameMode.globalPayingRateEffectList.Count != 0)
        {
            for (int i = 0; i < gameMode.globalPayingRateEffectList.Count; i++)
            {
                List<float> idEffect = gameMode.globalPayingRateEffectList[i];
                itemPayingRate += idEffect[1];
            }
        }
        float itemPayingAmount = 0f;
        if (gameMode.globalPayingAmountEffectList.Count != 0)
        {
            for (int i = 0; i < gameMode.globalPayingAmountEffectList.Count; i++)
            {
                List<float> idEffect = gameMode.globalPayingAmountEffectList[i];
                itemPayingAmount += idEffect[1];
            }
        }
        //trigger
        if (triggerPayingRateEffectList.Count != 0)
        {
            for (int i = 0; i < triggerPayingRateEffectList.Count; i++)
            {
                List<float> idEffect = triggerPayingRateEffectList[i];
                itemPayingRate += idEffect[1];
            }
        }
        if (triggerPayingAmountEffectList.Count != 0)
        {
            for (int i = 0; i < triggerPayingAmountEffectList.Count; i++)
            {
                List<float> idEffect = triggerPayingAmountEffectList[i];
                itemPayingAmount += idEffect[1];
            }
        }

        //添加触发者的属性
        finalPayingRate = basicPayRate + itemPayingRate;
        float cFinalPayingRate = finalPayingRate + payR;
        finalPayingAmount = basicPayAmount * (1 + itemPayingAmount);
        float cFinalPayingAmount = basicPayAmount * (1 + itemPayingAmount + payM);

        //统计贡献
        //全局
        if (gameMode.globalPayingRateEffectList.Count != 0)
        {
            for (int i = 0; i < gameMode.globalPayingRateEffectList.Count; i++)
            {
                List<float> idEffect = gameMode.globalPayingRateEffectList[i];
                float effectReset = (idEffect[1] / cFinalPayingRate) * cFinalPayingAmount;
                List<float> newIDEffect = new List<float>();
                newIDEffect.Add(idEffect[0]);
                newIDEffect.Add(effectReset);
                gameMode.contributionOfRevenue.Add(newIDEffect);
            }
        }
        if (gameMode.globalPayingAmountEffectList.Count != 0)
        {
            for (int i = 0; i < gameMode.globalPayingAmountEffectList.Count; i++)
            {
                List<float> idEffect = gameMode.globalPayingAmountEffectList[i];
                float effectReset = basicPayAmount * (1 + idEffect[1]);
                List<float> newIDEffect = new List<float>();
                newIDEffect.Add(idEffect[0]);
                newIDEffect.Add(effectReset);
                gameMode.contributionOfRevenue.Add(newIDEffect);
            }
        }
        //trigger
        if (triggerPayingRateEffectList.Count != 0)
        {
            for (int i = 0; i < triggerPayingRateEffectList.Count; i++)
            {
                List<float> idEffect = triggerPayingRateEffectList[i];
                float effectReset = (idEffect[1] / cFinalPayingRate) * cFinalPayingAmount;
                List<float> newIDEffect = new List<float>();
                newIDEffect.Add(idEffect[0]);
                newIDEffect.Add(effectReset);
                gameMode.contributionOfRevenue.Add(newIDEffect);
            }
        }
        if (triggerPayingAmountEffectList.Count != 0)
        {
            for (int i = 0; i < triggerPayingAmountEffectList.Count; i++)
            {
                List<float> idEffect = triggerPayingAmountEffectList[i];
                float effectReset = basicPayAmount * (1 + idEffect[1]);
                List<float> newIDEffect = new List<float>();
                newIDEffect.Add(idEffect[0]);
                newIDEffect.Add(effectReset);
                gameMode.contributionOfRevenue.Add(newIDEffect);
            }
        }
        //触发者的贡献
        if(payR > 0)
        {
            float effectReset = (payR / cFinalPayingRate) * cFinalPayingAmount;
            List<float> newIDEffect = new List<float>();
            newIDEffect.Add(itemID);
            newIDEffect.Add(effectReset);
            gameMode.contributionOfRevenue.Add(newIDEffect);
        }
        if(payM > 0)
        {
            float effectReset = payM;
            List<float> newIDEffect = new List<float>();
            newIDEffect.Add(itemID);
            newIDEffect.Add(effectReset);
            gameMode.contributionOfRevenue.Add(newIDEffect);
        }

        //概率
        float rate = Random.Range(0f, 1f);
        if (rate <= cFinalPayingRate)
        {
            PlayerPaying(cFinalPayingAmount);
            gameMode.purchaseRateS += 1;
        }

    }

    //留存-基础监测（伴随时间）
    void PlayerLivingMethod1()
    {
        basicLivingTiming += Time.deltaTime;
        if (basicLivingTiming >= basicLivingInterval)
        {
            gameMode.retentionRateA += 1;

            //拉取全局和触发效果中的item效果, 计算itemChurnRate
            
            itemChurnRate = 0f;
            //global留存效果
            if (gameMode.globalRetentionEffectList.Count != 0)
            {
                for(int i = 0; i < gameMode.globalRetentionEffectList.Count; i++)
                {
                    List<float> idEffect = gameMode.globalRetentionEffectList[i];
                    itemChurnRate += idEffect[1];
                }
            }

            socialBound = 0f;
            //global留存效果
            if (gameMode.globalSocialBoundEffectList.Count != 0)
            {
                for (int i = 0; i < gameMode.globalSocialBoundEffectList.Count; i++)
                {
                    List<float> idEffect = gameMode.globalSocialBoundEffectList[i];
                    socialBound += idEffect[1];
                }
            }

            //trigger留存效果
            if (triggerRetentionEffectList.Count != 0)
            {
                for (int i = 0; i < triggerRetentionEffectList.Count; i++)
                {
                    List<float> idEffect = triggerRetentionEffectList[i];
                    itemChurnRate += idEffect[1];
                }
            }

            //trigger留存效果
            if (triggerSocialBoundEffectList.Count != 0)
            {
                for (int i = 0; i < triggerSocialBoundEffectList.Count; i++)
                {
                    List<float> idEffect = triggerSocialBoundEffectList[i];
                    socialBound += idEffect[1];
                }
            }

            ChurnRateReset();  //刷新最终留存率

            //统计贡献
            //全局
            if (gameMode.globalRetentionEffectList.Count != 0)
            {
                for (int i = 0; i < gameMode.globalRetentionEffectList.Count; i++)
                {
                    List<float> idEffect = gameMode.globalRetentionEffectList[i];
                    float effectReset = (idEffect[1] / (itemChurnRate + basicChurnRate)) * basicChurnReset;
                    List<float> newIDEffect = new List<float>();
                    newIDEffect.Add(idEffect[0]);
                    newIDEffect.Add(effectReset);
                    gameMode.contributionOfLivetime.Add(newIDEffect);
                }
            }
            if (gameMode.globalSocialBoundEffectList.Count != 0)
            {
                for (int i = 0; i < gameMode.globalSocialBoundEffectList.Count; i++)
                {
                    List<float> idEffect = gameMode.globalSocialBoundEffectList[i];
                    float effectReset = (idEffect[1] / socialBound) * socialChurnReset;
                    List<float> newIDEffect = new List<float>();
                    newIDEffect.Add(idEffect[0]);
                    newIDEffect.Add(effectReset);
                    gameMode.contributionOfLivetime.Add(newIDEffect);
                }
            }
            //trigger
            if (triggerRetentionEffectList.Count != 0)
            {
                for (int i = 0; i < triggerRetentionEffectList.Count; i++)
                {
                    List<float> idEffect = triggerRetentionEffectList[i];
                    float effectReset = (idEffect[1] / (itemChurnRate + basicChurnRate)) * basicChurnReset;
                    List<float> newIDEffect = new List<float>();
                    newIDEffect.Add(idEffect[0]);
                    newIDEffect.Add(effectReset);
                    gameMode.contributionOfLivetime.Add(newIDEffect);
                }
            }
            if (triggerSocialBoundEffectList.Count != 0)
            {
                for (int i = 0; i < triggerSocialBoundEffectList.Count; i++)
                {
                    List<float> idEffect = triggerSocialBoundEffectList[i];
                    float effectReset = (idEffect[1] / (itemChurnRate + basicChurnRate)) * basicChurnReset;
                    List<float> newIDEffect = new List<float>();
                    newIDEffect.Add(idEffect[0]);
                    newIDEffect.Add(effectReset);
                    gameMode.contributionOfLivetime.Add(newIDEffect);
                }
            }

            //概率
            float rate = Random.Range(0f, 1f);
            if (rate <= (1 - finalChurnRate))
            {
                PlayerLosing();
                gameMode.retentionRateS += 1;
            }

            basicLivingTiming = 0f;
        }

    }

    void DestroySelf()
    {
        Destroy(this.gameObject);
    }

    //exp
    void ExpAddLogic()
    {
        expCheckTiming += Time.deltaTime;
        if(expCheckTiming >= expCheckInterval)
        {
            gameMode.playerExp += expAdd;
            expCheckTiming = 0f;
        }
    }

    void ChurnRateReset()
    {
        //留存率重新计数
        //基础衰减
        float basicDamping = Mathf.Pow(basicChurnDampingValue, circleNum) / 100f;
        if(basicDamping > 1f)
        {
            basicDamping = 1f;
        }
        basicChurnReset = (basicChurnRate + itemChurnRate) * (1 - basicDamping);

        //basicChurnReset = basicChurnReset * (satisfactionIndex / 100f);   //满意度调整

        //社交band
        float socialDamping = Mathf.Pow(socialDampingValue, circleNum) / 100f;
        if (socialDamping > 1f)
        {
            socialDamping = 1f;
        }
        float socialChurn = socialBound;
        socialChurnReset = socialChurn * (1 - socialDamping);

        //社交band
        float accountDamping = Mathf.Pow(accountChurnDampingValue, circleNum) / 100f;
        if (accountDamping > 1f)
        {
            accountDamping = 1f;
        }
        float accountChurn = accountBound;
        accountChurnReset = accountChurn * (1 - accountDamping);

        finalChurnRate = basicChurnReset + socialChurnReset + accountChurnReset;
        if(finalChurnRate > 1)
        {
            finalChurnRate = 1f;
        }
    }

    void PlayerMoodLogic()
    {
        satisfactionIndex = playerInbornMood + gameMode.globalMoodChange;
    }
}
