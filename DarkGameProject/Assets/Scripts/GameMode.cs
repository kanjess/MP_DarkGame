using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    public int maxPlayer;

    public bool gameDynamicProcess = false;
    public bool gameProcessPause = false;

    public GameObject playerBornPoint;
    public GameObject playerStartObject; //GameplayItem_999

    public GameObject playerItem;

    private GameObject playerItemLayer;

    private float playerCreateInterval;
    private float playerCreateTiming = 0f;

    public float companyMoney = 0f;


    private void Awake()
    {
        maxPlayer = 10;

        playerCreateInterval = 1f;

        playerItemLayer = GameObject.Find("PlayerObject").gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameDynamicProcess == true && gameProcessPause == false)
        {
            GameDynamicProcessPlay();
        }
    }

    void GameDynamicProcessPlay()
    {
        playerCreateTiming += Time.deltaTime;

        if(playerCreateTiming >= playerCreateInterval)
        {
            playerCreateTiming = 0f;

            if (playerItemLayer.transform.childCount < maxPlayer)
            {
                GameObject pItem = Instantiate(playerItem) as GameObject;
                pItem.transform.SetParent(playerItemLayer.transform);
                pItem.transform.position = playerBornPoint.transform.position;
                pItem.GetComponent<PlayerItem>().PlayerDetailSet(playerStartObject);
            }    
        }
    }
}
