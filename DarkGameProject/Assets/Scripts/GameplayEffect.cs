using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

//数据
[System.Serializable]
public class GameItemEffect
{
    public int id;
    public float retention;
    public float socialBound;
    public float payingRate;
    public float payingAmount;
    public float mood;
    public float triggerRate;
}


public class GameplayEffect : MonoBehaviour
{
    private string gameItemEffectJson;
    public LitJson.JsonData gameItemEffectData;
    public List<GameItemEffect> gameItemEffectList;
    public Dictionary<int, GameItemEffect> dic_gameItemEffect;

    
    private void Awake()
    {
        GameItemEffectDataSetup();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public float GameItemEffect(int itemID, string type)
    {
        float effectValue = 0f;

        if (type == "retention")
        {
            effectValue = GetGameItemEffectList(itemID).retention;
        }
        else if (type == "payingRate")
        {
            effectValue = GetGameItemEffectList(itemID).payingRate;
        }
        else if (type == "payingAmount")
        {
            effectValue = GetGameItemEffectList(itemID).payingAmount;
        }
        else if (type == "mood")
        {
            effectValue = GetGameItemEffectList(itemID).mood;
        }
        else if (type == "socialBound")
        {
            effectValue = GetGameItemEffectList(itemID).socialBound;
        }
        else if (type == "triggerRate")
        {
            effectValue = GetGameItemEffectList(itemID).triggerRate;
        }

        return effectValue;
    }

    //数据灌入
    void GameItemEffectDataSetup()
    {
        gameItemEffectJson = File.ReadAllText(Application.streamingAssetsPath + "/Json/gameplayPatterns.json");
        gameItemEffectData = JsonMapper.ToObject(gameItemEffectJson);
        //数据初始化
        if (gameItemEffectList == null)
        {
            gameItemEffectList = new List<GameItemEffect>();
        }
        if (gameItemEffectList.Count == 0)
        {
            GameItemEffect gameItemEffect;
            for (int i = 0; i < gameItemEffectData["gameplayPatterns"].Count; i++)
            {
                gameItemEffect = new GameItemEffect();
                //
                gameItemEffect.id = int.Parse(gameItemEffectData["gameplayPatterns"][i]["id"].ToString());
                gameItemEffect.retention = float.Parse(gameItemEffectData["gameplayPatterns"][i]["retention"].ToString());
                gameItemEffect.socialBound = float.Parse(gameItemEffectData["gameplayPatterns"][i]["socialBound"].ToString());
                gameItemEffect.payingRate = float.Parse(gameItemEffectData["gameplayPatterns"][i]["payingRate"].ToString());
                gameItemEffect.payingAmount = float.Parse(gameItemEffectData["gameplayPatterns"][i]["payingAmount"].ToString());
                gameItemEffect.mood = float.Parse(gameItemEffectData["gameplayPatterns"][i]["mood"].ToString());
                gameItemEffect.triggerRate = float.Parse(gameItemEffectData["gameplayPatterns"][i]["triggerRate"].ToString());
                //
                gameItemEffectList.Add(gameItemEffect);
            }
        }
        //辞典初始化
        dic_gameItemEffect = new Dictionary<int, GameItemEffect>();
        foreach (GameItemEffect def in gameItemEffectList)
        {
            dic_gameItemEffect[def.id] = def;
        }

    }

    public LitJson.JsonData GetGameItemEffectData(int id, string dataName)
    {
        for (int i = 0; i < gameItemEffectData[dataName].Count; i++)
        {
            if (gameItemEffectData[dataName][i]["id"].ToString() == id.ToString()) return gameItemEffectData[dataName][i];
        }
        return null;
    }

    public GameItemEffect GetGameItemEffectList(int id)
    {
        if (dic_gameItemEffect.ContainsKey(id))
        {
            return (dic_gameItemEffect[id]);
        }
        return (new GameItemEffect());
    }

}
