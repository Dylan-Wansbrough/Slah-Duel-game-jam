using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class handleGameFile : MonoBehaviour
{

    public GameObject[] players = new GameObject[2];

    public float[] scoring = new float[2];

    public Text winner;
    public GameObject blood;

    // Start is called before the first frame update
    void Start()
    {
        winner.text = " ";
        blood.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        scoring[0] = players[0].GetComponent<playerController>().timesHit;
        scoring[1] = players[1].GetComponent<playerController>().timesHit;

        if(scoring[0] > 4)
        {
            winner.text = "Player 2 Wins";
            blood.SetActive(true);
        }
        else if(scoring[1] > 4)
        {
            winner.text = "Player 1 Wins";
            blood.SetActive(true);
        }
    }
}
