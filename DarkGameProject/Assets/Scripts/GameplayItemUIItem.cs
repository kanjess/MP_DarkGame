using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayItemUIItem : MonoBehaviour
{
    private GameObject gameplayItemBtn;
    private GameMode gameMode;

    private GameObject itemNumContent;
    public GameObject itemNumText;
    public int itemNum;
    public int itemNumMax;

    private GameObject newItemContent;
    private GameObject unlockContent;
    private GameObject unlockText;

    private bool hasUnlock = false;
    public bool newItemShow = false;

    private DesignPanel designPanel;

    public int itemID;
    public bool isDark = false;
    public string unlockLevelListString;
    private List<int> unlockLevelList;

    private void Awake()
    {
        gameMode = GameObject.Find("Main Camera").GetComponent<GameMode>();

        gameplayItemBtn = this.gameObject.transform.Find("GameplayItemBtn").gameObject;
        //itemText = this.gameObject.transform.Find("ItemName").gameObject;
        itemNumContent = this.gameObject.transform.Find("ItemNumContent").gameObject;
        itemNumText = itemNumContent.transform.Find("ItemNum").gameObject;
        newItemContent = this.gameObject.transform.Find("NewItemContent").gameObject;
        unlockContent = this.gameObject.transform.Find("UnlockContent").gameObject;
        unlockText = unlockContent.transform.Find("UnlockText").gameObject;

        designPanel = GameObject.Find("Canvas").gameObject.GetComponent<DesignPanel>();

        itemNum = 1;
        itemNumMax = 1;

        unlockLevelList = new List<int>();
        if(unlockLevelListString != "")
        {
            string[] unlistS = unlockLevelListString.Split(',');
            foreach(string us in unlistS)
            {
                if(int.TryParse(us, out int unlistInt))
                {
                    unlockLevelList.Add(unlistInt);
                }
            }
        }

        
    }

    // Start is called before the first frame update
    void Start()
    {
        gameMode.gameItemList.Add(this.gameObject);

        UnlockListen();


    }

    // Update is called once per frame
    void Update()
    {
        if(designPanel.designItemBtnState == true)
        {
            //界面为打开时
            itemNumText.GetComponent<Text>().text = itemNum.ToString();

            if(hasUnlock == true)
            {
                unlockContent.transform.localScale = new Vector3(0, 0, 0);
                itemNumContent.transform.localScale = new Vector3(1, 1, 1);

                if (itemNum <= 0 && hasUnlock == true)
                {
                    gameplayItemBtn.transform.localScale = new Vector3(0, 0, 0);
                    itemNumText.GetComponent<Text>().color = Color.red;
                }
                else
                {
                    gameplayItemBtn.transform.localScale = new Vector3(1, 1, 1);
                    itemNumText.GetComponent<Text>().color = Color.yellow;
                }
            }
            else if (hasUnlock == false)
            {
                unlockContent.transform.localScale = new Vector3(1, 1, 1);
                unlockText.GetComponent<Text>().text = "Level " + unlockLevelList[0];
                gameplayItemBtn.transform.localScale = new Vector3(0, 0, 0);
                itemNumContent.transform.localScale = new Vector3(0, 0, 0);
            }

            if(newItemShow == false)
            {
                newItemContent.transform.localScale = new Vector3(0, 0, 0);
            }
            else
            {
                newItemContent.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        
    }

    public void SetItemID(int id)  //废弃
    {
        //itemID = id;
        //itemText.GetComponent<Text>().text = id.ToString();
    }

    public void UnlockListen()
    {
        //解锁
        if(gameMode.playerLevel >= unlockLevelList[0])
        {
            if(hasUnlock == false)
            {
                hasUnlock = true;

                //新道具提示
                if (gameMode.playerLevel > 1)
                {
                    if (newItemShow == false)
                    {
                        newItemShow = true;
                    }
                }
            }
        }

        //数量增加
        int shouldNum = 0;
        for(int i = 0; i < unlockLevelList.Count; i++)
        {
            if(gameMode.playerLevel >= unlockLevelList[i])
            {
                shouldNum += 1;
            }else if(gameMode.playerLevel < unlockLevelList[i])
            {
                break;
            }
        }
        if(shouldNum > itemNumMax)
        {
            int addChange = shouldNum - itemNumMax;
            itemNumMax += addChange;
            itemNum += addChange;
        }        
    }
}
