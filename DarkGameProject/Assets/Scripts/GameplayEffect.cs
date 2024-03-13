using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameplayEffect : MonoBehaviour
{
    //101
    private float retention_101;
    private float socialBound_101;
    private float payingRate_101;
    private float payingAmount_101;
    private float mood_101;
    //102  pve
    private float retention_102;
    private float socialBound_102;
    private float payingRate_102;
    private float payingAmount_102;
    private float mood_102;
    //103  pvp
    private float retention_103;
    private float socialBound_103;
    private float payingRate_103;
    private float payingAmount_103;
    private float mood_103;
    //104  运营
    private float retention_104;
    private float socialBound_104;
    private float payingRate_104;
    private float payingAmount_104;
    private float mood_104;

    //401  pay to skip
    private float retention_401;
    private float socialBound_401;
    private float payingRate_401;
    private float payingAmount_401;
    private float mood_401;
    //402  grinding
    private float retention_402;
    private float socialBound_402;
    private float payingRate_402;
    private float payingAmount_402;
    private float mood_402;
    //411  ranking
    private float retention_411;
    private float socialBound_411;
    private float payingRate_411;
    private float payingAmount_411;
    private float mood_411;
    //421  货币
    private float retention_421;
    private float socialBound_421;
    private float payingRate_421;
    private float payingAmount_421;
    private float mood_421;
    //431  每日登陆奖励
    private float retention_431;
    private float socialBound_431;
    private float payingRate_431;
    private float payingAmount_431;
    private float mood_431;

    private void Awake()
    {
        retention_102 = 0.05f;

        payingRate_103 = 0.05f;

        retention_104 = 0.05f;
        payingRate_104 = 0.05f;

        payingRate_401 = 0.50f;  //401触发事件

        retention_402 = 0.1f;  //402触发性数值
        mood_402 = -2;

        socialBound_411 = 0.1f;    //411触发性数值
        payingRate_411 = 0.05f;
        mood_411 = -1;

        payingRate_421 = 0.10f;  //421全局
        mood_421 = -5;

        retention_431 = 0.2f;  //431全局

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

        if(itemID == 101)
        {
            if(type == "retention")
            {
                effectValue = retention_101;
            }
            else if (type == "payingRate")
            {
                effectValue = payingRate_101;
            }
            else if (type == "payingAmount")
            {
                effectValue = payingAmount_101;
            }
            else if (type == "mood")
            {
                effectValue = mood_101;
            }
            else if (type == "socialBound")
            {
                effectValue = socialBound_101;
            }
        }
        else if(itemID == 102)  //pve
        {
            if (type == "retention")
            {
                effectValue = retention_102;
            }
            else if (type == "payingRate")
            {
                effectValue = payingRate_102;
            }
            else if (type == "payingAmount")
            {
                effectValue = payingAmount_102;
            }
            else if (type == "mood")
            {
                effectValue = mood_102;
            }
            else if (type == "socialBound")
            {
                effectValue = socialBound_102;
            }
        }
        else if (itemID == 103)  //pvp
        {
            if (type == "retention")
            {
                effectValue = retention_103;
            }
            else if (type == "payingRate")
            {
                effectValue = payingRate_103;
            }
            else if (type == "payingAmount")
            {
                effectValue = payingAmount_103;
            }
            else if (type == "mood")
            {
                effectValue = mood_103;
            }
            else if (type == "socialBound")
            {
                effectValue = socialBound_103;
            }
        }
        else if (itemID == 104)  //运营
        {
            if (type == "retention")
            {
                effectValue = retention_104;
            }
            else if (type == "payingRate")
            {
                effectValue = payingRate_104;
            }
            else if (type == "payingAmount")
            {
                effectValue = payingAmount_104;
            }
            else if (type == "mood")
            {
                effectValue = mood_104;
            }
            else if (type == "socialBound")
            {
                effectValue = socialBound_104;
            }
        }
        else if (itemID == 401)  
        {
            if (type == "retention")
            {
                effectValue = retention_401;
            }
            else if (type == "payingRate")
            {
                effectValue = payingRate_401;
            }
            else if (type == "payingAmount")
            {
                effectValue = payingAmount_401;
            }
            else if (type == "mood")
            {
                effectValue = mood_401;
            }
            else if (type == "socialBound")
            {
                effectValue = socialBound_401;
            }
        }
        else if (itemID == 402)
        {
            if (type == "retention")
            {
                effectValue = retention_402;
            }
            else if (type == "payingRate")
            {
                effectValue = payingRate_402;
            }
            else if (type == "payingAmount")
            {
                effectValue = payingAmount_402;
            }
            else if (type == "mood")
            {
                effectValue = mood_402;
            }
            else if (type == "socialBound")
            {
                effectValue = socialBound_402;
            }
        }
        else if (itemID == 411)
        {
            if (type == "retention")
            {
                effectValue = retention_411;
            }
            else if (type == "payingRate")
            {
                effectValue = payingRate_411;
            }
            else if (type == "payingAmount")
            {
                effectValue = payingAmount_411;
            }
            else if (type == "mood")
            {
                effectValue = mood_411;
            }
            else if (type == "socialBound")
            {
                effectValue = socialBound_411;
            }
        }
        else if (itemID == 421)
        {
            if (type == "retention")
            {
                effectValue = retention_421;
            }
            else if (type == "payingRate")
            {
                effectValue = payingRate_421;
            }
            else if (type == "payingAmount")
            {
                effectValue = payingAmount_421;
            }
            else if (type == "mood")
            {
                effectValue = mood_421;
            }
            else if (type == "socialBound")
            {
                effectValue = socialBound_421;
            }
        }
        else if (itemID == 431)
        {
            if (type == "retention")
            {
                effectValue = retention_431;
            }
            else if (type == "payingRate")
            {
                effectValue = payingRate_431;
            }
            else if (type == "payingAmount")
            {
                effectValue = payingAmount_431;
            }
            else if (type == "mood")
            {
                effectValue = mood_431;
            }
            else if (type == "socialBound")
            {
                effectValue = socialBound_431;
            }
        }
        else
        {
            Debug.Log("No Effect for " + itemID);
        }

        return effectValue;
    }


}
