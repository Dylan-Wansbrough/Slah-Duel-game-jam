using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRenderer : MonoBehaviour
{
    public GameObject[] Player1Hearts, Player2Hearts;
    public GameObject handleObj;

    handleGameFile handleScript;
    

    // Start is called before the first frame update
    void Start()
    {
        handleScript = handleObj.GetComponent<handleGameFile>();
    }

    // Update is called once per frame
    void Update()
    {
        Player1Health();
        Player2Health();
    }

    void Player1Health()
    {
        switch(handleScript.scoring[0])
        {
            case 1:
                Player1Hearts[4].SetActive(false);
                break;

            case 2:
                Player1Hearts[3].SetActive(false);
                break;

            case 3:
                Player1Hearts[2].SetActive(false);
                break;

            case 4:
                Player1Hearts[1].SetActive(false);
                break;

            case 5:
                Player1Hearts[0].SetActive(false);
                break;
        }
    }

    void Player2Health()
    {
        switch (handleScript.scoring[1])
        {
            case 1:
                Player2Hearts[0].SetActive(false);
                break;

            case 2:
                Player2Hearts[1].SetActive(false);
                break;

            case 3:
                Player2Hearts[2].SetActive(false);
                break;

            case 4:
                Player2Hearts[3].SetActive(false);
                break;

            case 5:
                Player2Hearts[4].SetActive(false);
                break;
        }
    }
}