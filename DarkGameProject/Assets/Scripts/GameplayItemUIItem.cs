using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayItemUIItem : MonoBehaviour
{
    private GameObject gameplayItemBtn;

    public GameObject itemNumText;
    public int itemNum;
    public int itemNumMax;

    private DesignPanel designPanel;

    public int itemID;
    public bool isDark = false;


    private void Awake()
    {
        gameplayItemBtn = this.gameObject.transform.Find("GameplayItemBtn").gameObject;
        //itemText = this.gameObject.transform.Find("ItemName").gameObject;
        GameObject itemNumContent = this.gameObject.transform.Find("ItemNumContent").gameObject;
        itemNumText = itemNumContent.transform.Find("ItemNum").gameObject;

        designPanel = GameObject.Find("Canvas").gameObject.GetComponent<DesignPanel>();

        itemNum = 1;
        itemNumMax = 1;
    }

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        if(designPanel.designItemBtnState == true)
        {
            //界面为打开时
            itemNumText.GetComponent<Text>().text = itemNum.ToString();

            if(itemNum <= 0)
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
        
    }

    public void SetItemID(int id)  //废弃
    {
        //itemID = id;
        //itemText.GetComponent<Text>().text = id.ToString();
    }

}
