using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PromotingPoint : MonoBehaviour
{
    private bool moveAnime = false;

    private DesignPanel designPanel;
    private GameMode gameMode;

    private GameObject point0;
    private GameObject point1;

    private Tweener moveAnimation;

    private float coolDown;

    //private SoundEffect soundEffect;

    private void Awake()
    {
        coolDown = Random.Range(0.1f, 1f);

        designPanel = GameObject.Find("Canvas").gameObject.GetComponent<DesignPanel>();
        //soundEffect = GameObject.Find("Canvas").gameObject.GetComponent<SoundEffect>();
        gameMode = GameObject.Find("Main Camera").gameObject.GetComponent<GameMode>();

        point1 = this.gameObject.transform.parent.gameObject;
        point0 = point1.transform.Find("Point").gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameMode.promotionMode == 1 && designPanel.gamePromotionPanelPointAnime == true)
        {
            this.gameObject.transform.localScale = new Vector3(1, 1, 1);

            if(moveAnime == false)
            {
                coolDown -= Time.deltaTime;

                if(coolDown <= 0)
                {
                    MoveAnime();
                }
                
            }
            

        }
        else
        {
            this.gameObject.transform.localScale = new Vector3(0, 0, 0);

            moveAnime = false;

            if (moveAnimation != null)
            {
                moveAnimation.Kill();
            }
        }

        
    }

    void MoveAnime()
    {
        moveAnime = true;
        coolDown = Random.Range(0.1f, 1f);

        this.gameObject.transform.position = point0.transform.position;

        moveAnimation = this.gameObject.transform.DOMove(point1.transform.position, 3f).SetEase(Ease.Linear);

        moveAnimation.OnComplete(() => moveAnime = false);
    }

}
