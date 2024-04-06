using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayItemUIItem : MonoBehaviour
{
    public GameObject gameplayItemBtn;
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
    public int itemLevel = 1;
    public bool itemCanLevelUp = true;
    public bool isDark = false;
    public string unlockLevelListString;
    private List<int> unlockLevelList;

    public GameObject levelUpBtn;
    private GameObject itemNameItem;
    public string itemName = "";
    public GameObject levelContent;
    public GameObject levelNum;
    public int levelPrice = 1;

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
        levelUpBtn = this.gameObject.transform.Find("LevelButton").gameObject;
        itemNameItem = levelUpBtn.transform.Find("ItemName").gameObject;
        itemName = itemNameItem.GetComponent<Text>().text;
        levelContent = this.gameObject.transform.Find("LevelContent").gameObject;
        levelNum = levelContent.transform.Find("ItemNum").gameObject;

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

        levelUpBtn.GetComponent<Button>().onClick.AddListener(delegate
        {
            designPanel.DesignItemLevelPanel(this.gameObject);
        });

        UnlockListen();

    }

    // Update is called once per frame
    void Update()
    {
        if(designPanel.designItemBtnState == true)
        {
            //界面为打开时
            itemNumText.GetComponent<Text>().text = itemNum.ToString();
            levelNum.GetComponent<Text>().text = itemLevel.ToString();

            if (hasUnlock == true)
            {
                unlockContent.transform.localScale = new Vector3(0, 0, 0);
                itemNumContent.transform.localScale = new Vector3(1, 1, 1);
                levelContent.transform.localScale = new Vector3(1, 1, 1);
                levelUpBtn.GetComponent<Button>().interactable = true;
                levelUpBtn.GetComponent<Image>().color = new Color(115 / 255f, 255 / 255f, 60 / 255f, 255 / 255f);
                itemNameItem.transform.localPosition = new Vector3(8f, 0, 0);

                if (itemCanLevelUp == false)
                {
                    levelUpBtn.GetComponent<Image>().color = new Color(119f / 255f, 201f / 255f, 225f / 255f, 1f);
                    levelUpBtn.transform.localPosition = new Vector3(0, -101, 0);
                    itemNameItem.transform.localPosition = new Vector3(0, 0, 0);
                }

                if (itemNum <= 0 && hasUnlock == true)
                {
                    gameplayItemBtn.transform.localScale = new Vector3(0, 0, 0);
                    itemNumText.GetComponent<Text>().color = Color.white;
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
                levelContent.transform.localScale = new Vector3(0, 0, 0);
                levelUpBtn.GetComponent<Button>().interactable = false;
                levelUpBtn.GetComponent<Image>().color = new Color(115 / 255f, 255 / 255f, 60 / 255f, 0 / 255f);
                itemNameItem.transform.localPosition = new Vector3(-8, 0, 0);
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
                        if(designPanel.designItemNewAnime == false)
                        {
                            designPanel.designItemNewAnime = true;
                        }
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

            //新道具提示
            if (gameMode.playerLevel > 1)
            {
                if (newItemShow == false)
                {
                    newItemShow = true;
                    if (designPanel.designItemNewAnime == false)
                    {
                        designPanel.designItemNewAnime = true;
                    }
                }
            }
        }        
    }
}
