using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashSE : MonoBehaviour
{
    private DesignPanel designPanel;

    private AudioSource audioSource;

    private AudioClip seClip;

    private float cashSEInterval = 0.25f;
    private float cashSETiming = 0f;
    private bool cashSEStarted = false;

    private void Awake()
    {
        designPanel = this.gameObject.GetComponent<DesignPanel>();

        audioSource = this.gameObject.transform.Find("CashSoundEffect").gameObject.GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(cashSEStarted == true)
        {
            cashSETiming += Time.deltaTime;

            if(cashSETiming >= cashSEInterval)
            {
                cashSEStarted = false;
                cashSETiming = 0f;
            }
        }
        
    }

    public void CashStart()
    {
        if(cashSEStarted == false)
        {
            seClip = Resources.Load<AudioClip>("SoundEffect/" + "cash");
            if (seClip != null)
            {
                audioSource.PlayOneShot(seClip);
                cashSEStarted = true;
            }
        }
    }
}
