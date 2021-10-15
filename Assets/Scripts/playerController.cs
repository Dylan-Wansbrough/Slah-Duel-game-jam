using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    //player 
    public int playerNumber;
    public GameObject OtherPlayer;

    //Players state
    public bool isAttacking;
    public bool isBlocking;
    public bool isFaking;
    public bool isStunned;
    public bool isHit;
    public bool isFinished;

    //Player animation states
    public Sprite[] sprites;
    private SpriteRenderer m_SpriteRenderer;
    private float time;
    private float aniamtionTime; //how long the anaimation lasts for

    //timings
    private float timeSincePress;
    private float pressWindow = 0.3f; //time to double tap attack
    private float cooldown;
    private float blockWindow; //the time the other player is allowed to block

    private string[] keypressed = new string[2];

    //player life
    public float timesHit;
    public bool isDead;


    public AudioSource[] aSource;

    bool playSheathOnce;

    public Animation anim;



    // Start is called before the first frame update
    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();

        if(playerNumber == 1)
        {
            keypressed[0] = "d";
            keypressed[1] = "a";
        }
        else
        {
            keypressed[0] = "left";
            keypressed[1] = "right";
        }
    }

    // Update is called once per frame
    void Update()
    {

        time += Time.deltaTime;

        if (!isDead)
        {
            if (time > cooldown) //recently attacked
            {
                if (Input.GetKeyDown(keypressed[1]))
                {
                    Debug.Log("blocking");
                    m_SpriteRenderer.sprite = sprites[3];
                    aniamtionTime = time + 0.3f; //block animation lasts for a small period
                    isBlocking = true;
                    playSheathOnce = true;
                    cooldown = time + 0.7f;
                    aSource[5].Play();
                }
                else if (Input.GetKeyDown(keypressed[0]))
                {
                    if (time > (timeSincePress + pressWindow))
                    {
                        Debug.Log("psych");
                        m_SpriteRenderer.sprite = sprites[1];
                        aniamtionTime = time + 0.3f;
                        isFaking = true;
                        playSheathOnce = true;
                        timeSincePress = time; //last time the player pressed attack key
                        aSource[4].Play();
                    }
                    else
                    {
                        aSource[1].Play();
                        Debug.Log("attack");
                        isAttacking = true;
                        isFaking = false;
                        playSheathOnce = true;
                        m_SpriteRenderer.sprite = sprites[2];
                        aniamtionTime = time + 0.5f;
                        cooldown = time + 1f;
                        blockWindow = time + 0.2f;
                    }
                }
            }

            //Attacking the other player
            if (isAttacking)
            {
                if (time > blockWindow)
                {
                    if (OtherPlayer.GetComponent<playerController>().isBlocking)
                    {
                        isAttacking = false;
                        isStunned = true;
                        m_SpriteRenderer.sprite = sprites[4];
                        cooldown = time + 0.7f; //time penalty for blocking them
                        aniamtionTime = time + 0.8f;
                        aSource[2].Play();
                    }
                    else
                    {
                        isAttacking = false;
                        cooldown = time + 1f;
                        OtherPlayer.GetComponent<playerController>().isHit = true;
                        OtherPlayer.GetComponent<playerController>().isAttacking = false;
                        OtherPlayer.GetComponent<playerController>().cooldown = cooldown;
                        OtherPlayer.GetComponent<playerController>().timesHit += 1;
                        OtherPlayer.GetComponent<playerController>().aniamtionTime = time + 0.9f;
                        //sound effects
                        anim.Play("screenshake");
                        aSource[0].Play();
                        if (OtherPlayer.GetComponent<playerController>().isDead)
                        {
                            OtherPlayer.GetComponent<playerController>().isFinished = true;
                        }
                    }
                }
            }

            //if the other player is stunned all CD are reset to allow for counter attack
            if (OtherPlayer.GetComponent<playerController>().isStunned)
            {
                cooldown = 0;
            }

            if (isHit)
            {
                m_SpriteRenderer.sprite = sprites[5];
            }

            //resetting
            if (time > aniamtionTime)
            {
                m_SpriteRenderer.sprite = sprites[0];
                isBlocking = false;
                isFaking = false;
                isAttacking = false;
                isStunned = false;
                isHit = false;
            }

            if(timesHit > 4)
            {
                isDead = true;
                m_SpriteRenderer.sprite = sprites[6];
            }


        }
        else
        {
            if (isFinished)
            {
                m_SpriteRenderer.sprite = sprites[7];
            }
        }
    }
            
}
