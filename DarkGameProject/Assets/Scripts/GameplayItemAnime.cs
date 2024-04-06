using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class GameplayItemAnime : MonoBehaviour
{
    private GameMode gameMode;
    public GameObject animeContent;
    private int itemID;

    private bool anime1State = false;
    private bool anime2State = false;
    private bool anime3State = false;

    private void Awake()
    {
        gameMode = GameObject.Find("Main Camera").gameObject.GetComponent<GameMode>();
        itemID = this.gameObject.GetComponent<GameplayItem>().itemID;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.GetComponent<GameplayItem>().isMain == false && this.gameObject.GetComponent<GameplayItem>().hasPath == false)
        {
            if(animeContent != null && gameMode.gameProcessPause == false && gameMode.gameDynamicProcess == true)
            {
                AnimePlay(0);
            }
        }
    }

    public void AnimePlay(int order)
    {
        if(itemID == 101)  //基础
        {
            if(order == 0)
            {
                if(anime1State == false)
                {
                    anime1State = true;
                    GameObject animeItem = animeContent.transform.GetChild(order).gameObject;
                    Tweener anime = animeItem.transform.DOLocalRotate(new Vector3(0, 0, -90), 0.3f).SetRelative();
                    anime.OnComplete(() => anime1State = false);
                }
            }
            else if (order == 1)
            {
                if (anime2State == false)
                {
                    anime2State = true;
                    GameObject animeItem = animeContent.transform.GetChild(order).gameObject;
                    Tweener anime = animeItem.transform.DOLocalRotate(new Vector3(0, 0, -90), 0.3f).SetRelative();
                    anime.OnComplete(() => anime2State = false);
                }
            }
        }
        else if(itemID == 102)  //pve
        {
            if (order == 0)
            {
                if (anime1State == false)
                {
                    anime1State = true;
                    GameObject animeItem = animeContent.transform.GetChild(order).gameObject;
                    Vector3 pos0 = animeItem.transform.position;
                    Vector3 pos1 = animeItem.transform.GetChild(0).gameObject.transform.position;

                    Sequence s = DOTween.Sequence();
                    s.Insert(0f, animeItem.transform.DOMove(pos1, 0.15f));
                    s.Insert(0.15f, animeItem.transform.DOMove(pos0, 0.15f));

                    s.OnComplete(() => anime1State = false);
                }
            }
            else if(order == 1)
            {
                if (anime2State == false)
                {
                    anime2State = true;
                    GameObject animeItem = animeContent.transform.GetChild(order).gameObject;
                    Vector3 pos0 = animeItem.transform.position;
                    Vector3 pos1 = animeItem.transform.GetChild(0).gameObject.transform.position;

                    Sequence s = DOTween.Sequence();
                    s.Insert(0f, animeItem.transform.DOMove(pos1, 0.15f));
                    s.Insert(0.15f, animeItem.transform.DOMove(pos0, 0.15f));

                    s.OnComplete(() => anime2State = false);
                }
            }
        }
        else if (itemID == 103)  //pvp
        {
            if (order == 0 || order == 2)
            {
                if (anime1State == false)
                {
                    anime1State = true;
                    GameObject animeItem1 = animeContent.transform.GetChild(0).gameObject;
                    GameObject animeItem2 = animeContent.transform.GetChild(1).gameObject;
                    Vector3 item1angle0 = new Vector3(0, 0, 40);
                    Vector3 item1angle1 = new Vector3(0, 0, 0);
                    Vector3 item2angle0 = new Vector3(0, 0, -40);
                    Vector3 item2angle1 = new Vector3(0, 0, 0);

                    Sequence s1 = DOTween.Sequence();
                    s1.Insert(0f, animeItem1.transform.DOLocalRotate(item1angle0, 0.2f));
                    s1.Insert(0.2f, animeItem1.transform.DOLocalRotate(item1angle1, 0.1f));

                    Sequence s2 = DOTween.Sequence();
                    s2.Insert(0f, animeItem2.transform.DOLocalRotate(item2angle0, 0.2f));
                    s2.Insert(0.2f, animeItem2.transform.DOLocalRotate(item2angle1, 0.1f));

                    s1.OnComplete(() => anime1State = false);
                }
            }
            else if (order == 1 || order == 3)
            {
                if (anime2State == false)
                {
                    anime2State = true;
                    GameObject animeItem1 = animeContent.transform.GetChild(2).gameObject;
                    GameObject animeItem2 = animeContent.transform.GetChild(3).gameObject;
                    Vector3 item1angle0 = new Vector3(0, 0, 5f);
                    Vector3 item1angle1 = new Vector3(0, 0, 0);
                    Vector3 item1angle2 = new Vector3(0, 0, -5f);
                    Vector3 item2angle0 = new Vector3(0, 0, -5f);
                    Vector3 item2angle1 = new Vector3(0, 0, 0);
                    Vector3 item2angle2 = new Vector3(0, 0, 5f);

                    Sequence s1 = DOTween.Sequence();
                    s1.Insert(0f, animeItem1.transform.DOLocalRotate(item1angle0, 0.075f));
                    s1.Insert(0.075f, animeItem1.transform.DOLocalRotate(item1angle2, 0.15f));
                    s1.Insert(0.225f, animeItem1.transform.DOLocalRotate(item1angle1, 0.075f));

                    Sequence s2 = DOTween.Sequence();
                    s2.Insert(0f, animeItem2.transform.DOLocalRotate(item2angle0, 0.075f));
                    s2.Insert(0.075f, animeItem2.transform.DOLocalRotate(item2angle2, 0.15f));
                    s2.Insert(0.225f, animeItem2.transform.DOLocalRotate(item2angle1, 0.075f));

                    s1.OnComplete(() => anime2State = false);
                }
            }

        }
        else if (itemID == 104)  //运营
        {
            if (order == 0)
            {
                if (anime1State == false)
                {
                    anime1State = true;
                    GameObject animeItem = animeContent.transform.GetChild(0).gameObject;

                    Sequence s = DOTween.Sequence();
                    s.Insert(0f, animeItem.transform.DOScaleY(-1f, 0.075f));
                    s.Insert(0.075f, animeItem.transform.DOScaleY(1f, 0.075f));
                    s.Insert(0.15f, animeItem.transform.DOScaleY(-1f, 0.075f));
                    s.Insert(0.225f, animeItem.transform.DOScaleY(1f, 0.075f));

                    s.OnComplete(() => anime1State = false);
                }
            }
            else if (order == 1)
            {
                if (anime2State == false)
                {
                    anime2State = true;
                    GameObject animeItem1 = animeContent.transform.GetChild(1).gameObject;
                    GameObject animeItem2 = animeContent.transform.GetChild(2).gameObject;
                    Vector3 item1angle0 = new Vector3(0, 0, -10f);
                    Vector3 item1angle1 = new Vector3(0, 0, 0);
                    Vector3 item1angle2 = new Vector3(0, 0, 10f);
                    Vector3 item2angle0 = new Vector3(0, 0, 10f);
                    Vector3 item2angle1 = new Vector3(0, 0, 0);
                    Vector3 item2angle2 = new Vector3(0, 0, -10f);

                    Sequence s1 = DOTween.Sequence();
                    s1.Insert(0f, animeItem1.transform.DOLocalRotate(item1angle0, 0.075f));
                    s1.Insert(0.075f, animeItem1.transform.DOLocalRotate(item1angle2, 0.15f));
                    s1.Insert(0.225f, animeItem1.transform.DOLocalRotate(item1angle1, 0.075f));

                    Sequence s2 = DOTween.Sequence();
                    s2.Insert(0f, animeItem2.transform.DOLocalRotate(item2angle0, 0.075f));
                    s2.Insert(0.075f, animeItem2.transform.DOLocalRotate(item2angle2, 0.15f));
                    s2.Insert(0.225f, animeItem2.transform.DOLocalRotate(item2angle1, 0.075f));

                    s1.OnComplete(() => anime2State = false);
                }
            }

        }
        else if(itemID  == 999)  //appStore
        {
            GameObject animeItem1 = animeContent.transform.GetChild(0).gameObject;
            GameObject animeItem2 = animeContent.transform.GetChild(1).gameObject;

            if(animeItem1.transform.localScale == new Vector3(0,0,0) && animeItem2.transform.localScale == new Vector3(0, 0, 0))
            {
                animeItem1.transform.localScale = new Vector3(1, 1, 1);
                animeItem2.transform.localScale = new Vector3(0, 0, 0);
            }
            else if (animeItem1.transform.localScale == new Vector3(1, 1, 1) && animeItem2.transform.localScale == new Vector3(0, 0, 0))
            {
                animeItem1.transform.localScale = new Vector3(1, 1, 1);
                animeItem2.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (animeItem1.transform.localScale == new Vector3(1, 1, 1) && animeItem2.transform.localScale == new Vector3(1, 1, 1))
            {
                animeItem1.transform.localScale = new Vector3(0, 0, 0);
                animeItem2.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (animeItem1.transform.localScale == new Vector3(0, 0, 0) && animeItem2.transform.localScale == new Vector3(1, 1, 1))
            {
                animeItem1.transform.localScale = new Vector3(0, 0, 0);
                animeItem2.transform.localScale = new Vector3(0, 0, 0);
            }
        }
        else if(itemID == 411)  //pvp ranking
        {
            if (anime1State == false)
            {
                anime1State = true;
                GameObject animeItem1 = animeContent.transform.GetChild(0).gameObject;
                GameObject animeItem2 = animeItem1.transform.GetChild(0).gameObject;
                //Vector3 pos1 = new Vector3(0.98f, 0, 0);
                //Vector3 pos0 = new Vector3(0, 0, 0);
                //animeItem1.transform.localPosition = pos0;
                animeItem2.transform.localScale = new Vector3(0, 0, 0);

                Sequence s = DOTween.Sequence();
                s.Insert(0f, animeItem1.transform.DOScaleY(-1f, 0.1f));
                s.Insert(0.1f, animeItem1.transform.DOScaleY(1f, 0.1f));
                s.Insert(0.2f, animeItem2.transform.DOScale(new Vector3(1,1,1), 0.1f));

                s.OnComplete(() => anime1State = false);
            }
        }
        else if (itemID == 412)  //强制社交
        {
            if (anime1State == false)
            {
                anime1State = true;
                GameObject animeItem1 = animeContent.transform.GetChild(0).gameObject;
                GameObject animeItem2 = animeContent.transform.GetChild(1).gameObject;
                Vector3 item1pos0 = new Vector3(animeItem1.transform.localPosition.x, animeItem1.transform.localPosition.y + 0.2f, animeItem1.transform.localPosition.z);
                Vector3 item1pos1 = animeItem1.transform.localPosition;
                //animeItem1.transform.localPosition = item1pos0;
                Vector3 item2pos0 = new Vector3(animeItem2.transform.localPosition.x, animeItem2.transform.localPosition.y - 0.2f, animeItem2.transform.localPosition.z);
                Vector3 item2pos1 = animeItem2.transform.localPosition;
                //animeItem2.transform.localPosition = item2pos0;

                Sequence s = DOTween.Sequence();
                s.Insert(0f, animeItem1.transform.DOLocalMove(item1pos0, 0.15f));
                s.Insert(0f, animeItem2.transform.DOLocalMove(item2pos0, 0.15f));
                s.Insert(0.15f, animeItem1.transform.DOLocalMove(item1pos1, 0.15f));
                s.Insert(0.15f, animeItem2.transform.DOLocalMove(item2pos1, 0.15f));

                s.OnComplete(() => anime1State = false);
            }
        }
        else if (itemID == 421)  //货币
        {
            if (anime1State == false)
            {
                anime1State = true;
                GameObject animeItem = animeContent.transform.GetChild(0).gameObject;
                float x0 = animeItem.transform.localPosition.x;
                float x1 = animeItem.transform.localPosition.x - 0.03f;
                float x2 = animeItem.transform.localPosition.x + 0.03f;

                Sequence s = DOTween.Sequence();
                s.Insert(0f, animeItem.transform.DOLocalMoveX(x1, 0.3f));
                s.Insert(0.3f, animeItem.transform.DOLocalMoveX(x0, 0.3f));
                s.Insert(0.6f, animeItem.transform.DOLocalMoveX(x2, 0.3f));
                s.Insert(0.9f, animeItem.transform.DOLocalMoveX(x0, 0.3f));

                s.OnComplete(() => anime1State = false);
            }
        }
        else if (itemID == 422)  //ADs
        {
            if (anime1State == false)
            {
                anime1State = true;
                SpriteRenderer animeItem1 = animeContent.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
                SpriteRenderer animeItem2 = animeContent.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>();
                SpriteRenderer animeItem3 = animeContent.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>();
                SpriteRenderer animeItem4 = animeContent.transform.GetChild(3).gameObject.GetComponent<SpriteRenderer>();
                SpriteRenderer animeItem5 = animeContent.transform.GetChild(4).gameObject.GetComponent<SpriteRenderer>();

                Sequence s = DOTween.Sequence();
                s.Insert(0f, animeItem1.DOFade(0f, 0.15f));
                s.Insert(0.15f, animeItem2.DOFade(0f, 0.15f));
                s.Insert(0.3f, animeItem3.DOFade(0f, 0.15f));
                s.Insert(0.45f, animeItem4.DOFade(0f, 0.15f));
                s.Insert(0.6f, animeItem5.DOFade(0f, 0.15f));

                s.Insert(0.15f, animeItem1.DOFade(1f, 0.15f));
                s.Insert(0.3f, animeItem2.DOFade(1f, 0.15f));
                s.Insert(0.45f, animeItem3.DOFade(1f, 0.15f));
                s.Insert(0.6f, animeItem4.DOFade(1f, 0.15f));
                s.Insert(0.75f, animeItem5.DOFade(1f, 0.15f));

                s.OnComplete(() => anime1State = false);
            }
        }
        else if (itemID == 401)  //pay to skip
        {
            if (anime1State == false)
            {
                anime1State = true;
                GameObject animeItem1 = animeContent.transform.GetChild(0).gameObject; 

                Sequence s = DOTween.Sequence();
                s.Insert(0f, animeItem1.transform.DOScaleY(1f, 0.15f));
                s.Insert(0.15f, animeItem1.transform.DOScaleY(-1f, 0.15f));

                s.OnComplete(() => anime1State = false);
            }
        }
        else if (itemID == 402)  //Grinding
        {
            if (anime1State == false)
            {
                anime1State = true;
                GameObject animeItem1 = animeContent.transform.GetChild(0).gameObject;
                GameObject animeItem2 = animeContent.transform.GetChild(1).gameObject;
                GameObject animeItem3 = animeContent.transform.GetChild(2).gameObject;
                GameObject animeItem4 = animeContent.transform.GetChild(3).gameObject;

                animeItem1.transform.DOLocalRotate(new Vector3(0, 0, -90), 0.3f).SetRelative();

                Sequence s = DOTween.Sequence();

                s.Insert(0f, animeItem2.transform.DOScaleX(1f, 0.1f));
                s.Insert(0.15f, animeItem3.transform.DOScaleX(1f, 0.1f));
                s.Insert(0.3f, animeItem4.transform.DOScaleX(1f, 0.1f));

                s.Insert(0.1f, animeItem2.transform.DOScaleX(-1f, 0.1f));
                s.Insert(0.25f, animeItem3.transform.DOScaleX(-1f, 0.1f));
                s.Insert(0.35f, animeItem4.transform.DOScaleX(-1f, 0.1f));

                s.OnComplete(() => anime1State = false);
            }
        }
        else if (itemID == 403)  //体力（自动）
        {
            if (anime1State == false)
            {
                anime1State = true;
                SpriteRenderer animeItem1 = animeContent.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
                SpriteRenderer animeItem2 = animeContent.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>();
                SpriteRenderer animeItem3 = animeContent.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>();

                Sequence s = DOTween.Sequence();
                s.Insert(0f, animeItem1.DOFade(0f, 0.5f));
                s.Insert(0.5f, animeItem2.DOFade(0f, 0.5f));
                s.Insert(1f, animeItem3.DOFade(0f, 0.5f));

                s.Insert(0.5f, animeItem1.DOFade(1f, 0.5f));
                s.Insert(1f, animeItem2.DOFade(1f, 0.5f));
                s.Insert(1.5f, animeItem3.DOFade(1f, 0.5f));

                s.OnComplete(() => anime1State = false);
            }
        }
        else if (itemID == 431)  //次日登陆奖励
        {
            if (anime1State == false)
            {
                anime1State = true;
                GameObject animeItem1 = animeContent.transform.GetChild(0).gameObject;
                GameObject animeItem2 = animeContent.transform.GetChild(1).gameObject;

                Sequence s = DOTween.Sequence();

                s.Insert(0f, animeItem1.transform.DOLocalRotate(new Vector3(0, 0, -360), 0.3f).SetRelative());
                s.Insert(0f, animeItem2.transform.DOLocalMoveX(1.3f, 0.15f));
                s.Insert(0.15f, animeItem2.transform.DOLocalMoveX(1.2f, 0.15f));

                s.OnComplete(() => anime1State = false);
            }
        }
        else if (itemID == 432)  //每日任务
        {
            if (anime1State == false)
            {
                anime1State = true;
                GameObject animeItem1 = animeContent.transform.GetChild(0).gameObject;
                GameObject animeItem2 = animeContent.transform.GetChild(1).gameObject;
                GameObject animeItem3 = animeContent.transform.GetChild(2).gameObject;
                GameObject animeItem4 = animeContent.transform.GetChild(3).gameObject;

                Sequence s = DOTween.Sequence();

                s.Insert(0f, animeItem1.transform.DOLocalRotate(new Vector3(0, 0, -360), 0.3f).SetRelative());
                s.Insert(0f, animeItem2.GetComponent<SpriteRenderer>().DOFade(0f, 0.15f));
                s.Insert(0.1f, animeItem3.GetComponent<SpriteRenderer>().DOFade(0f, 0.15f));
                s.Insert(0.2f, animeItem4.GetComponent<SpriteRenderer>().DOFade(0f, 0.15f));
                s.Insert(0.15f, animeItem2.GetComponent<SpriteRenderer>().DOFade(1f, 0.15f));
                s.Insert(0.25f, animeItem3.GetComponent<SpriteRenderer>().DOFade(1f, 0.15f));
                s.Insert(0.35f, animeItem4.GetComponent<SpriteRenderer>().DOFade(1f, 0.15f));

                s.OnComplete(() => anime1State = false);
            }
        }
        else if (itemID == 433)  //在线奖励
        {
            if (anime1State == false)
            {
                anime1State = true;
                GameObject animeItem1 = animeContent.transform.GetChild(0).gameObject;
                GameObject animeItem2 = animeContent.transform.GetChild(1).gameObject;
                GameObject animeItem3 = animeContent.transform.GetChild(2).gameObject;

                Tweener anime1 = animeItem1.transform.DOLocalRotate(new Vector3(0, 0, -360), 0.3f).SetRelative();
                animeItem2.transform.DOLocalRotate(new Vector3(0, 0, -90), 0.3f).SetRelative();
                animeItem3.transform.DOLocalRotate(new Vector3(0, 0, -14), 0.3f).SetRelative();

                anime1.OnComplete(() => anime1State = false);
            }
        }
        else if (itemID == 434)  //礼包
        {
            if (anime1State == false)
            {
                anime1State = true;
                GameObject animeItem1 = animeContent.transform.GetChild(0).gameObject;
                GameObject animeItem2 = animeContent.transform.GetChild(1).gameObject;
                GameObject animeItem3 = animeContent.transform.GetChild(2).gameObject;
                GameObject animeItem4 = animeContent.transform.GetChild(3).gameObject;

                animeItem1.transform.DOLocalRotate(new Vector3(0, 0, -360), 0.3f).SetRelative();
                animeItem2.transform.DOLocalRotate(new Vector3(0, 0, -90), 0.3f).SetRelative();
                animeItem3.transform.DOLocalRotate(new Vector3(0, 0, -14), 0.3f).SetRelative();

                Sequence s = DOTween.Sequence();

                s.Insert(0f, animeItem4.transform.DOScale(new Vector3(-1.2f, -1.2f, 1), 0.15f));
                s.Insert(0.15f, animeItem4.transform.DOScale(new Vector3(-1, -1, 1), 0.15f));

                s.OnComplete(() => anime1State = false);
            }
        }
        else if (itemID == 435)  //宝箱
        {
            if (anime1State == false)
            {
                anime1State = true;
                GameObject animeItem1 = animeContent.transform.GetChild(0).gameObject;
                GameObject animeItem2 = animeContent.transform.GetChild(1).gameObject;
                GameObject animeItem3 = animeContent.transform.GetChild(2).gameObject;

                //float x0 = 1.3f;
                float x1 = 0.5f;

                animeItem1.transform.DOLocalRotate(new Vector3(0, 0, -180), 0.3f).SetRelative();
                animeItem2.transform.localPosition = new Vector3(1.3f, 0.1f, 0f);
                animeItem3.transform.localPosition = new Vector3(1.3f, -0.16f, 0f);

                Sequence s = DOTween.Sequence();

                s.Insert(0f, animeItem2.transform.DOLocalMoveX(x1, 0.3f));
                s.Insert(0.2f, animeItem3.transform.DOLocalMoveX(x1, 0.3f));

                s.OnComplete(() => anime1State = false);
            }
        }

    }

}
