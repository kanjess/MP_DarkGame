using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayItemUIItem : MonoBehaviour
{
    private GameObject gameplayItemBtn;

    private void Awake()
    {
        gameplayItemBtn = this.gameObject.transform.Find("GameplayItemBtn").gameObject;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
