using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PlayerItem : MonoBehaviour
{
    private bool activePlayer;
    private bool firstTimeToActive = false;

    private GameObject playerFaceContent;
    private GameObject playerFaceItem;
    private GameObject playerLosingFace;

    private GameMode gameMode;

    private float moveSpeed;
    private bool moveState = false;
    private bool moveAnime = false;
    private int moveOrder = 0;

    public GameObject currentObject;
    public GameObject nextObject;

    private List<Vector3Int> pathList;

    private Tweener movingAnime;

    private float liveTime = 0f;

    public float basicPayRate = 0.25f;  //基础付费率
    public float basicChurnRate = 0.25f;  //基础流失率
    public float basicPayAmount = 100f;  //基础付费额



    private GameObject moneyIcon;
    private bool moneyAnime = false;
    public float basicMoneyCheckInterval;
    private float basicMoneyCheckTiming = 0f;

    public float basicLivingInterval;
    private float basicLivingTiming = 0f;


    private void Awake()
    {
        gameMode = GameObject.Find("Main Camera").gameObject.GetComponent<GameMode>();

        moveSpeed = 3f;  //像素/s
        basicMoneyCheckInterval = 1f;
        basicLivingInterval = 5f;

        pathList = new List<Vector3Int>();

        GameObject playerFeedbackContent = this.gameObject.transform.Find("PlayerFeedbackContent").gameObject;
        GameObject moneyContent = playerFeedbackContent.transform.Find("MoneyContent").gameObject;
        moneyIcon = moneyContent.transform.Find("MoneyIcon").gameObject;

        playerFaceContent = this.gameObject.transform.Find("PlayerFaceContent").gameObject;
        playerLosingFace = this.gameObject.transform.Find("PlayerLosingFace").gameObject;

        activePlayer = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        moneyIcon.GetComponent<SpriteRenderer>().DOFade(0f, 0f);


    }

    // Update is called once per frame
    void Update()
    {
        //首次激活（首次经过101时激活）
        if(currentObject.gameObject.name == "GameplayItem_101(Clone)" && firstTimeToActive == false)
        {
            firstTimeToActive = true;
            activePlayer = true;
        }

        //行为检测：移动 付费 留存
        if(gameMode.gameDynamicProcess == true && gameMode.gameProcessPause == false)
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

            if (activePlayer == true)
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

        //累积生存时间
        if (gameMode.gameDynamicProcess == true && gameMode.gameProcessPause == false)
        {
            liveTime += Time.deltaTime;
        }

    }

    public void PlayerDetailSet(GameObject startG)
    {
        currentObject = startG;
    }

    //角色移动
    void PlayerItemMove()
    {
        if(moveState == false && moveAnime == false)
        {
            //优先特殊，其次普通
            if(currentObject.GetComponent<GameplayItem>().specialOutputLinkItemList.Count > 0)
            {
                nextObject = currentObject.GetComponent<GameplayItem>().specialOutputLinkItemList[0];
                pathList = currentObject.GetComponent<GameplayItem>().specialOutputPathPosListList[0];
            }
            else if(currentObject.GetComponent<GameplayItem>().outputLinkItemList.Count > 0)
            {
                nextObject = currentObject.GetComponent<GameplayItem>().outputLinkItemList[0];
                pathList = currentObject.GetComponent<GameplayItem>().outputPathPosListList[0];
            }
            //
            if(nextObject != null && nextObject != currentObject)
            {
                moveState = true;
                moveAnime = false;
                moveOrder = 0;
            }
        }

        if(moveState == true && moveAnime == false)
        {
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
                currentObject = nextObject;

                moveState = false;
            }

        }
    }


    //角色付费
    void PlayerPaying()
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

            gameMode.companyMoney += basicPayAmount;
        }
        
    }

    //角色流失
    void PlayerLosing()
    {
        activePlayer = false;
        playerFaceContent.transform.localScale = new Vector3(0, 0, 0);
        playerLosingFace.transform.localScale = new Vector3(1, 1, 1);

        Sequence ss = DOTween.Sequence();
        //ss.Insert(0f, playerLosingFace.transform.DOMoveZ(1f, 1f).SetRelative());
        ss.Insert(1f, playerLosingFace.transform.DOScale(new Vector3(0,0,0), 0.6f));
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
            //概率
            float rate = Random.Range(0f, 1f);
            if(rate <= basicPayRate)
            {
                PlayerPaying();
            }

            basicMoneyCheckTiming = 0f;
        }

    }
    //付费-玩法检测（玩法触发）
    void PlayerPayingCheckMethod2()
    {
        //this.gameObject.GetComponent<BoxCollider2D>().co
    }

    //留存-基础监测（伴随时间）
    void PlayerLivingMethod1()
    {
        basicLivingTiming += Time.deltaTime;
        if (basicLivingTiming >= basicLivingInterval)
        {
            //概率
            float rate = Random.Range(0f, 1f);
            if (rate <= basicChurnRate)
            {
                PlayerLosing();
            }

            basicLivingTiming = 0f;
        }

    }

    void DestroySelf()
    {
        Destroy(this.gameObject);
    }

}
