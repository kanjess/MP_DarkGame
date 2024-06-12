using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    private DesignPanel designPanel;

    private AudioSource audioSource;

    private AudioClip seClip;

    private void Awake()
    {
        designPanel = this.gameObject.GetComponent<DesignPanel>();

        audioSource = this.gameObject.transform.Find("SoundEffect").gameObject.GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonClick()
    {
        seClip = Resources.Load<AudioClip>("SoundEffect/" + "buttonClick");
        if(seClip != null)
        {
            audioSource.PlayOneShot(seClip);
        }   
    }

    public void SwitchClick()
    {
        seClip = Resources.Load<AudioClip>("SoundEffect/" + "switchButton");
        if (seClip != null)
        {
            audioSource.PlayOneShot(seClip);
        }
    }

    public void ClickSingle()
    {
        seClip = Resources.Load<AudioClip>("SoundEffect/" + "clickSingle");
        if (seClip != null)
        {
            audioSource.PlayOneShot(seClip);
        }
    }

    public void GameStartSE()
    {
        seClip = Resources.Load<AudioClip>("SoundEffect/" + "gameStart");
        if (seClip != null)
        {
            audioSource.PlayOneShot(seClip);
        }
    }

    public void FuncIn()
    {
        seClip = Resources.Load<AudioClip>("SoundEffect/" + "funcIn");
        if (seClip != null)
        {
            audioSource.PlayOneShot(seClip);
        }
    }
    public void FuncInDark()
    {
        seClip = Resources.Load<AudioClip>("SoundEffect/" + "funcInDark");
        if (seClip != null)
        {
            audioSource.PlayOneShot(seClip);
        }
    }
    public void FuncBack()
    {
        seClip = Resources.Load<AudioClip>("SoundEffect/" + "funcBack");
        if (seClip != null)
        {
            audioSource.PlayOneShot(seClip);
        }
    }
    public void FuncLink()
    {
        seClip = Resources.Load<AudioClip>("SoundEffect/" + "moduleLink");
        if (seClip != null)
        {
            audioSource.PlayOneShot(seClip);
        }
    }
    public void LoopStart()
    {
        seClip = Resources.Load<AudioClip>("SoundEffect/" + "loopConnect");
        if (seClip != null)
        {
            audioSource.PlayOneShot(seClip);
        }
    }
    public void PromotionStart()
    {
        seClip = Resources.Load<AudioClip>("SoundEffect/" + "promotion");
        if (seClip != null)
        {
            audioSource.PlayOneShot(seClip);
        }
    }
}
