using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private DesignPanel designPanel;

    private AudioSource audioSource;
    public bool musicStart = false;
    private float musicPlayInterval;
    private float musicPlayTiming = 0f;
    private int musicNo = 1;

    private AudioClip clip1;
    private AudioClip clip2;
    private AudioClip clip3;

    private void Awake()
    {
        designPanel = this.gameObject.GetComponent<DesignPanel>();

        musicPlayInterval = 3f;

        musicPlayTiming = musicPlayInterval;

        audioSource = this.gameObject.transform.Find("BGM").gameObject.GetComponent<AudioSource>();

        clip1 = Resources.Load<AudioClip>("BGM/" + "gameOver");
        clip2 = Resources.Load<AudioClip>("BGM/" + "bitShift");
        clip3 = Resources.Load<AudioClip>("BGM/" + "pookatori");
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("MusicStartBool", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if(musicStart == true)
        {
            if(audioSource.clip == null || audioSource.isPlaying == false)
            {
                musicPlayTiming += Time.deltaTime;
            }

            if (musicPlayTiming >= musicPlayInterval)
            {
                if (musicNo == 1)
                {
                    audioSource.clip = clip1;
                }
                else if (musicNo == 2)
                {
                    audioSource.clip = clip2;
                }
                else if (musicNo == 3)
                {
                    audioSource.clip = clip3;
                }

                audioSource.Play();
                musicPlayTiming = 0;

                if(designPanel.gameStartUI.transform.localScale == new Vector3(0, 0, 0))
                {
                    musicNo++;
                }
                else
                {
                    musicNo = 1;
                }

                if(musicNo >= 4)
                {
                    musicNo = 1;
                }
            }

        }     
    }

    void MusicStartBool()
    {
        musicStart = true;
    }
}
