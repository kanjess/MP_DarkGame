using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayItemUIItem : MonoBehaviour
{
    private GameObject gameplayItemBtn;

    private GameObject itemText;

    public int itemID;
    public bool isDark = false;


    private void Awake()
    {
        gameplayItemBtn = this.gameObject.transform.Find("GameplayItemBtn").gameObject;
        itemText = gameplayItemBtn.transform.Find("Text").gameObject;

    }

    // Start is called before the first frame update
    void Start()
    {
        if(itemID > 400 && itemID <= 499)
        {
            isDark = true;
        }
        else
        {
            isDark = false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetItemID(int id)
    {
        itemID = id;
        itemText.GetComponent<Text>().text = id.ToString();
    }

}
