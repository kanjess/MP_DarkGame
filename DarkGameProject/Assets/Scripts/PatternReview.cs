using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

//数据
[System.Serializable]
public class MainReviewItem
{
    public int id;
    public bool isPositive;
    public string commentString;
    public string commentString2;
    public string commentString3;
}

[System.Serializable]
public class MainReviewList
{
    public int id;
    public bool isPositive;
    public List<MainReviewItem> commentList;
}

[System.Serializable]
public class DarkReviewItem
{
    public int id;
    public int darkID;
    public bool isPositive;
    public string commentString;
    public string commentString2;
    public string commentString3;
}

[System.Serializable]
public class DarkReviewList
{
    public int id;
    public int darkID;
    public bool isPositive;
    public List<DarkReviewItem> commentList;
}

[System.Serializable]
public class PlayerHead
{
    public int id;
    public string playerHead;
    public string playerName;
}

[System.Serializable]
public class PlayerReview
{
    public int id;
    public string playerHead;
    public string playerName;
    public int darkID;
    public bool isPositive;
    public float rating;
    public int level;
    public int commentDay;
    public string comment;
}



public class PatternReview : MonoBehaviour
{
    private GameplayEffect gameplayEffect;
    private GameplayMapping gameplayMapping;
    private GameMode gameMode;

    private string mainReviewJson;
    public LitJson.JsonData mainReviewData;
    public List<MainReviewList> mainReviewListList;
    public Dictionary<int, MainReviewList> dic_mainReviewList;

    private string darkReviewJson;
    public LitJson.JsonData darkReviewData;
    public List<DarkReviewList> darkReviewListList;
    public Dictionary<int, DarkReviewList> dic_darkReviewList;

    private string playerHeadJson;
    public LitJson.JsonData playerHeadData;
    public List<PlayerHead> playerHeadList;
    public Dictionary<int, PlayerHead> dic_playerHead;

    //玩家评论存储
    public List<PlayerReview> playerReviewList;
    public List<PlayerReview> temPlayerReviewList;


    private void Awake()
    {
        gameplayEffect = GameObject.Find("Main Camera").gameObject.GetComponent<GameplayEffect>();
        gameplayMapping = GameObject.Find("Main Camera").gameObject.GetComponent<GameplayMapping>();
        gameMode = GameObject.Find("Main Camera").gameObject.GetComponent<GameMode>();

        MainReviewDataSetup();
        PlayerHeadDataSetup();
        playerReviewList = new List<PlayerReview>();
        temPlayerReviewList = new List<PlayerReview>();
    }


    // Start is called before the first frame update
    void Start()
    {
        DarkReviewDataSetup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //数据灌入
    void PlayerHeadDataSetup()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Json/playerReviews_Info");
        playerHeadJson = textAsset.text;

        playerHeadData = JsonMapper.ToObject(playerHeadJson);
        //数据初始化
        if (playerHeadList == null)
        {
            playerHeadList = new List<PlayerHead>();
        }
        if (playerHeadList.Count == 0)
        {
            PlayerHead playerHead;
            for (int i = 0; i < playerHeadData["playerReviews_Info"].Count; i++)
            {
                playerHead = new PlayerHead();
                //
                playerHead.id = int.Parse(playerHeadData["playerReviews_Info"][i]["id"].ToString());
                playerHead.playerHead = playerHeadData["playerReviews_Info"][i]["head"].ToString();
                playerHead.playerName = playerHeadData["playerReviews_Info"][i]["name"].ToString();
                //
                playerHeadList.Add(playerHead);
            }
        }
        //辞典初始化
        dic_playerHead = new Dictionary<int, PlayerHead>();
        foreach (PlayerHead def in playerHeadList)
        {
            dic_playerHead[def.id] = def;
        }

    }


    //数据灌入
    void MainReviewDataSetup()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Json/playerReviews_Main");
        mainReviewJson = textAsset.text;

        mainReviewData = JsonMapper.ToObject(mainReviewJson);
        //数据初始化
        if (mainReviewListList == null)
        {
            mainReviewListList = new List<MainReviewList>();
        }
        if (mainReviewListList.Count == 0)
        {
            //列表结构
            MainReviewList mainReviewList;
            for(int i = 0; i < 2; i++)
            {
                mainReviewList = new MainReviewList();

                mainReviewList.id = i + 1;
                if (i == 0)
                {
                    mainReviewList.isPositive = true;
                }
                else if(i == 1)
                {
                    mainReviewList.isPositive = false;
                }

                mainReviewList.commentList = new List<MainReviewItem>();

                mainReviewListList.Add(mainReviewList);
            }

            //item数据 灌入
            MainReviewItem mainReviewItem;
            for (int i = 0; i < mainReviewData["playerReviews_Main"].Count; i++)
            {
                mainReviewItem = new MainReviewItem();
                //
                mainReviewItem.id = int.Parse(mainReviewData["playerReviews_Main"][i]["id"].ToString());
                if(int.Parse(mainReviewData["playerReviews_Main"][i]["positive"].ToString()) == 1)
                {
                    mainReviewItem.isPositive = true;
                }
                else
                {
                    mainReviewItem.isPositive = false;
                }
                mainReviewItem.commentString = mainReviewData["playerReviews_Main"][i]["comments"].ToString();
                mainReviewItem.commentString2 = mainReviewData["playerReviews_Main"][i]["comments2"].ToString();
                mainReviewItem.commentString3 = mainReviewData["playerReviews_Main"][i]["comments3"].ToString();
                //
                if (mainReviewItem.isPositive == true)
                {
                    mainReviewListList[0].commentList.Add(mainReviewItem);
                }
                else
                {
                    mainReviewListList[1].commentList.Add(mainReviewItem);
                }
            }
        }
        //Debug.Log(mainReviewListList[0].commentList.Count + " - " + mainReviewListList[1].commentList.Count);
        //Debug.Log(mainReviewListList[0].commentList[0].commentString);

        //辞典初始化
        dic_mainReviewList = new Dictionary<int, MainReviewList>();
        foreach (MainReviewList def in mainReviewListList)
        {
            dic_mainReviewList[def.id] = def;
        }
    }
    public LitJson.JsonData GetMainReviewData(int id, string dataName)
    {
        for (int i = 0; i < mainReviewData[dataName].Count; i++)
        {
            if (mainReviewData[dataName][i]["id"].ToString() == id.ToString()) return mainReviewData[dataName][i];
        }
        return null;
    }

    //获得正/负评论数据结构
    public MainReviewList GetMainReviewList(bool isPositive)
    {
        int id = 1;
        if(isPositive == true)
        {
            id = 1;
        }
        else
        {
            id = 2;
        }

        if (dic_mainReviewList.ContainsKey(id))
        {
            return (dic_mainReviewList[id]);
        }
        return (new MainReviewList());
    }
    //获得评论文字的列表
    public List<string> GetMainReviewStringList(bool isPositive, int level)
    {
        List<string> commentList = new List<string>();

        for(int i = 0; i < GetMainReviewList(isPositive).commentList.Count; i++)
        {
            string commentString = "";
            if (level == 1)
            {
                commentString = GetMainReviewList(isPositive).commentList[i].commentString;
            }
            else if (level == 2)
            {
                commentString = GetMainReviewList(isPositive).commentList[i].commentString2;
            }
            else if (level == 3)
            {
                commentString = GetMainReviewList(isPositive).commentList[i].commentString3;
            }
            commentList.Add(commentString);
        }

        return commentList;
    }
    //随机一个主评论
    public string GetRandomMainReviewString(bool isPositive, int level)
    {
        string commentS = "";

        int maxR = GetMainReviewStringList(isPositive, level).Count;
        int ranR = Random.Range(0, maxR);
        if(ranR >= maxR)
        {
            ranR = maxR - 1;
        }
        else if(ranR < 0)
        {
            ranR = 0;
        }

        commentS = GetMainReviewStringList(isPositive, level)[ranR];

        return commentS;
    }

    //数据灌入
    void DarkReviewDataSetup()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Json/playerReviews_Dark");
        darkReviewJson = textAsset.text;

        darkReviewData = JsonMapper.ToObject(darkReviewJson);
        //数据初始化
        if (darkReviewListList == null)
        {
            darkReviewListList = new List<DarkReviewList>();
        }
        if (darkReviewListList.Count == 0)
        {
            //列表结构
            DarkReviewList darkReviewList;
            for (int i = 0; i < gameplayEffect.gameItemEffectList.Count; i++)
            {
                //Debug.Log(gameplayEffect.gameItemEffectList.Count);
                int darkI = gameplayEffect.gameItemEffectList[i].id;

                for(int aa = 0; aa < 2; aa++)
                {
                    darkReviewList = new DarkReviewList();

                    if(darkReviewListList.Count == 0)
                    {
                        darkReviewList.id = 1;
                    }
                    else
                    {
                        darkReviewList.id = darkReviewListList[darkReviewListList.Count - 1].id + 1;
                    }

                    darkReviewList.darkID = darkI;

                    if (aa == 0)
                    {
                        darkReviewList.isPositive = true;
                    }
                    else if (aa == 1)
                    {
                        darkReviewList.isPositive = false;
                    }

                    darkReviewList.commentList = new List<DarkReviewItem>();

                    darkReviewListList.Add(darkReviewList);
                }
            }

            //item数据 灌入
            DarkReviewItem darkReviewItem;
            for (int i = 0; i < darkReviewData["playerReviews_Dark"].Count; i++)
            {
                //Debug.Log(darkReviewData["playerReviews_Dark"].Count);
                darkReviewItem = new DarkReviewItem();
                //
                darkReviewItem.id = int.Parse(darkReviewData["playerReviews_Dark"][i]["id"].ToString());
                darkReviewItem.darkID = int.Parse(darkReviewData["playerReviews_Dark"][i]["darkID"].ToString());
                if (int.Parse(darkReviewData["playerReviews_Dark"][i]["positive"].ToString()) == 1)
                {
                    darkReviewItem.isPositive = true;
                }
                else
                {
                    darkReviewItem.isPositive = false;
                }
                darkReviewItem.commentString = darkReviewData["playerReviews_Dark"][i]["comments"].ToString();
                darkReviewItem.commentString2 = darkReviewData["playerReviews_Dark"][i]["comments2"].ToString();
                darkReviewItem.commentString3 = darkReviewData["playerReviews_Dark"][i]["comments3"].ToString();

                for (int aa = 0; aa < darkReviewListList.Count; aa++)
                {
                    if(darkReviewItem.darkID == darkReviewListList[aa].darkID && darkReviewItem.isPositive == darkReviewListList[aa].isPositive)
                    {
                        darkReviewListList[aa].commentList.Add(darkReviewItem);
                        break;
                    }
                }
            }
            //Debug.Log(darkReviewListList[darkReviewListList.Count - 1].darkID + " : " + darkReviewListList[darkReviewListList.Count - 1].isPositive + " : " + darkReviewListList[darkReviewListList.Count - 1].commentList[0].commentString);
        }
        //Debug.Log(darkReviewListList[0].darkID + " - " + darkReviewListList[0].commentList.Count);
        //辞典初始化
        dic_darkReviewList = new Dictionary<int, DarkReviewList>();
        foreach (DarkReviewList def in darkReviewListList)
        {
            dic_darkReviewList[def.id] = def;
        }
    }
    public LitJson.JsonData GetDarkReviewData(int id, string dataName)
    {
        for (int i = 0; i < darkReviewData[dataName].Count; i++)
        {
            if (darkReviewData[dataName][i]["id"].ToString() == id.ToString()) return darkReviewData[dataName][i];
        }
        return null;
    }

    //获得场上dark patterns的id列表
    public List<int> GetDarkPatternsIDList()
    {
        List<int> darkIdList = new List<int>();
        for(int i = 0; i < gameplayMapping.darkGameplayItemList.Count; i++)
        {
            GameObject darkItem = gameplayMapping.darkGameplayItemList[i];
            int darkID = darkItem.GetComponent<GameplayItem>().itemID;

            darkIdList.Add(darkID);
        }

        return darkIdList;
    }
    //从场上随机一个dark patterns的ID
    public int GetRandomDarkPatternID()
    {
        int darkID = 401;
        List<float> randomDarkList = new List<float>();
        for(int i = 0; i < GetDarkPatternsIDList().Count; i++)
        {
            int id = GetDarkPatternsIDList()[i];
            float ranValue = gameplayEffect.GetGameItemEffectList(id).weight;

            if(i == 0)
            {
                //ranValue = ranValue;
            }
            else
            {
                ranValue += randomDarkList[i - 1];
            }

            randomDarkList.Add(ranValue);
        }

        float randR = Random.Range(0f, randomDarkList[randomDarkList.Count - 1]);

        for(int i = 0; i < GetDarkPatternsIDList().Count; i++)
        {
            if(randR <= randomDarkList[i])
            {
                darkID = GetDarkPatternsIDList()[i];
                break;
            }
        }

        return darkID;
    }
    //获得对应具体dark patterns的正/负评论数据结构
    public DarkReviewList GetDarkReviewList(bool isPositive, int darkID)
    {
        int id = 1;

        for(int i = 0; i < darkReviewListList.Count; i++)
        {
            if (darkReviewListList[i].isPositive == isPositive && darkReviewListList[i].darkID == darkID)
            {
                id = darkReviewListList[i].id;
                break;
            }
        }

        if (dic_darkReviewList.ContainsKey(id))
        {
            return (dic_darkReviewList[id]);
        }
        return (new DarkReviewList());
    }
    //获得对应具体dark patterns的正/负评论文字的列表
    public List<string> GetDarkReviewStringList(bool isPositive, int darkID, int level)
    {
        List<string> commentList = new List<string>();

        for (int i = 0; i < GetDarkReviewList(isPositive, darkID).commentList.Count; i++)
        {
            string commentString = "";
            if(level == 1)
            {
                commentString = GetDarkReviewList(isPositive, darkID).commentList[i].commentString;
            }
            else if (level == 2)
            {
                commentString = GetDarkReviewList(isPositive, darkID).commentList[i].commentString2;
            }
            else if (level == 3)
            {
                commentString = GetDarkReviewList(isPositive, darkID).commentList[i].commentString3;
            }
            commentList.Add(commentString);
        }

        return commentList;
    }
    //随机一个对应具体dark patterns的正/负评论
    public string GetRandomDarkReviewString(bool isPositive, int darkID, int level)
    {
        string commentS = "";

        int maxR = GetDarkReviewStringList(isPositive, darkID, level).Count;
        int ranR = Random.Range(0, maxR);
        if (ranR >= maxR)
        {
            ranR = maxR - 1;
        }
        else if (ranR < 0)
        {
            ranR = 0;
        }

        commentS = GetDarkReviewStringList(isPositive, darkID, level)[ranR];

        return commentS;
    }


    //生成评论
    public void PlayerReviewDataCreate(bool isPositive, int level, int creatNum)
    {
        if (creatNum > 0)
        {
            for (int i = 0; i < creatNum; i++)
            {
                PlayerReview playerReview;

                playerReview = new PlayerReview();

                playerReview.id = playerReviewList.Count + temPlayerReviewList.Count;

                int headOrder = Random.Range(0, playerHeadList.Count);
                playerReview.playerHead = playerHeadList[headOrder].playerHead;
                int nameOrder = Random.Range(0, playerHeadList.Count);
                playerReview.playerName = playerHeadList[nameOrder].playerName;

                playerReview.isPositive = isPositive;
                playerReview.rating = gameMode.ratingList[gameMode.ratingList.Count - 1];
                playerReview.level = level;
                playerReview.commentDay = gameMode.statisticesTimes;

                playerReview.darkID = GetRandomDarkPatternID();

                string mainC = GetRandomMainReviewString(isPositive, level);
                string linkW = "";
                string darkC = "";
                if(level >= 2)
                {
                    darkC = GetRandomDarkReviewString(isPositive, playerReview.darkID, level);
                }
                int randLinkW = Random.Range(0, 6);
                if(randLinkW == 0)
                {
                    linkW = "In addition,";
                }
                else if (randLinkW == 1)
                {
                    linkW = "Especially,";
                }
                else if (randLinkW == 2)
                {
                    linkW = "Moreover,";
                }
                else if (randLinkW == 3)
                {
                    linkW = "Furthermore,";
                }
                else if (randLinkW == 4)
                {
                    linkW = "And that,";
                }
                else if (randLinkW >= 5)
                {
                    linkW = "";
                }

                if (darkC == "")
                {
                    playerReview.level = 1;
                    linkW = "";
                }
                playerReview.comment = mainC + " " + linkW + " " + darkC;

                temPlayerReviewList.Add(playerReview);
            }

        }
    }


}
