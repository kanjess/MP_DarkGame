using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using LitJson;
using System.Linq;

//数据
[System.Serializable]
public class TaskItem
{
    public int id;
    public string desc;
    public bool hasDone;
    public int function;
    public string funcImage;
    public int functionTarget;
    public float functionValue;
    public int nextID;
    public int reward;
    public List<int> guideList;
    public int guidePanelOpenNum;
}

[System.Serializable]
public class GameGuide
{
    public int id;
    public string desc;
    public string image;
}


public class GameTask : MonoBehaviour
{
    public bool allTaskFinish = false;
    private DesignPanel designPanel;

    private string taskItemJson;
    public LitJson.JsonData taskItemData;
    public List<TaskItem> taskItemList;
    public Dictionary<int, TaskItem> dic_taskItem;

    private string gameGuideJson;
    public LitJson.JsonData gameGuideData;
    public List<GameGuide> gameGuideList;
    public Dictionary<int, GameGuide> dic_gameGuide;

    public int currentTaskID;
    public TaskItem currentTask;
    public float currentTaskProcess;

    private GameMode gameMode;
    private GameplayMapping gameplayMapping;
    private GameplayEffect gameplayEffect;


    private void Awake()
    {
        TaskItemDataSetup();
        GameGuideDataSetup();

        currentTaskID = taskItemList[0].id;
        currentTask = taskItemList[0];

        designPanel = GameObject.Find("Canvas").gameObject.GetComponent<DesignPanel>();
        gameMode = this.gameObject.GetComponent<GameMode>();
        gameplayMapping = this.gameObject.GetComponent<GameplayMapping>();
        gameplayEffect = this.gameObject.GetComponent<GameplayEffect>();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TaskCompleteListen();
    }

    void TaskItemDataSetup()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Json/gameTask");
        taskItemJson = textAsset.text;

        taskItemData = JsonMapper.ToObject(taskItemJson);
        //数据初始化
        if (taskItemList == null)
        {
            taskItemList = new List<TaskItem>();
        }
        if (taskItemList.Count == 0)
        {
            TaskItem taskItem;
            for (int i = 0; i < taskItemData["gameTask"].Count; i++)
            {
                taskItem = new TaskItem();
                //
                taskItem.id = int.Parse(taskItemData["gameTask"][i]["id"].ToString());
                taskItem.desc = taskItemData["gameTask"][i]["desc"].ToString();
                taskItem.hasDone = false;
                taskItem.function = int.Parse(taskItemData["gameTask"][i]["func"].ToString());
                taskItem.funcImage = taskItemData["gameTask"][i]["image"].ToString();
                if (taskItemData["gameTask"][i]["funcTarget"].ToString() != "")
                {
                    taskItem.functionTarget = int.Parse(taskItemData["gameTask"][i]["funcTarget"].ToString());
                }
                taskItem.functionValue = float.Parse(taskItemData["gameTask"][i]["funcValue"].ToString());
                taskItem.nextID = int.Parse(taskItemData["gameTask"][i]["next"].ToString());
                taskItem.reward = int.Parse(taskItemData["gameTask"][i]["reward"].ToString());
                taskItem.guideList = new List<int>();
                string newGuideL = taskItemData["gameTask"][i]["newGuide"].ToString();
                if(newGuideL != "")
                {
                    string[] newGuideLA = newGuideL.Split(',');
                    int[] newGuideLAI = newGuideLA.Select(int.Parse).ToArray();
                    taskItem.guideList.AddRange(newGuideLAI);
                }
                taskItem.guidePanelOpenNum = 0;
                //
                taskItemList.Add(taskItem);
            }
        }
        //辞典初始化
        dic_taskItem = new Dictionary<int, TaskItem>();
        foreach (TaskItem def in taskItemList)
        {
            dic_taskItem[def.id] = def;
        }
    }
    public TaskItem GetTaskItem(int id)
    {
        if (dic_taskItem.ContainsKey(id))
        {
            return (dic_taskItem[id]);
        }
        return (new TaskItem());
    }


    void GameGuideDataSetup()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Json/gameGuide");
        gameGuideJson = textAsset.text;

        gameGuideData = JsonMapper.ToObject(gameGuideJson);
        //数据初始化
        if (gameGuideList == null)
        {
            gameGuideList = new List<GameGuide>();
        }
        if (gameGuideList.Count == 0)
        {
            GameGuide gameGuide;
            for (int i = 0; i < gameGuideData["gameGuide"].Count; i++)
            {
                gameGuide = new GameGuide();
                //
                gameGuide.id = int.Parse(gameGuideData["gameGuide"][i]["id"].ToString());
                gameGuide.desc = gameGuideData["gameGuide"][i]["desc"].ToString();
                gameGuide.image = gameGuideData["gameGuide"][i]["image"].ToString();

                //
                gameGuideList.Add(gameGuide);
            }
        }
        //辞典初始化
        dic_gameGuide = new Dictionary<int, GameGuide>();
        foreach (GameGuide def in gameGuideList)
        {
            dic_gameGuide[def.id] = def;
        }
    }
    public GameGuide GetGameGuide(int id)
    {
        if (dic_gameGuide.ContainsKey(id))
        {
            return (dic_gameGuide[id]);
        }
        return (new GameGuide());
    }

    //任务监听
    void TaskCompleteListen()
    {
        if(designPanel.hasTaskShow == true && currentTask.hasDone == false)
        {
            if(currentTask.function == 401) //401 达到x等级
            {
                currentTaskProcess = gameMode.playerLevel;
            }
            else if (currentTask.function == 402) //402 场上拥有某dark pattern X个
            {
                currentTaskProcess = 0;
                for (int i = 0; i < gameplayMapping.darkGameplayItemList.Count; i++)
                {
                    int darkID = gameplayMapping.darkGameplayItemList[i].gameObject.GetComponent<GameplayItem>().itemID;
                    if(darkID == currentTask.functionTarget)
                    {
                        currentTaskProcess++;
                    }
                }
            }
            else if (currentTask.function == 403) //403 场上共计有dark patterns X个
            {
                currentTaskProcess = gameplayMapping.darkGameplayItemList.Count;
            }
            else if (currentTask.function == 404) //404 提高留存到x
            {
                currentTaskProcess = gameMode.retentionRateList.Max();
            }
            else if (currentTask.function == 405) //405 提高付费率到x
            {
                currentTaskProcess = gameMode.purchaseRateList.Max();
            }
            else if (currentTask.function == 406) //406 提高clv到x
            {
                currentTaskProcess = float.Parse(gameMode.clvList.Max().ToString("0.0"));
            }
            else if (currentTask.function == 407) //407 放置基础funtion到x个
            {
                currentTaskProcess = gameplayMapping.mainGameplayItemList.Count - 1;
            }
            else if (currentTask.function == 408) //408 进行promotion x次
            {
                currentTaskProcess = gameMode.promotionCount;
            }
            else if (currentTask.function == 409) //409 升级dark pattern x次
            {
                currentTaskProcess = 0;
                for (int i = 0; i < gameplayEffect.gameItemEffectList.Count; i++)
                {
                    currentTaskProcess += gameplayEffect.gameItemEffectList[i].level - 1;
                }
            }
            else if (currentTask.function == 410) //410 查看x次运营报告
            {
                currentTaskProcess = designPanel.operationPanelCheckNum;
            }
            else if (currentTask.function == 411) //411 查看x次玩家报告
            {
                currentTaskProcess = designPanel.ratingPanelCheckNum;
            }
            else
            {
                currentTaskProcess = 0f;
            }

            //
            if(currentTaskProcess >= currentTask.functionValue)
            {
                //完成
                currentTask.hasDone = true;

            }
        }
    }

}
