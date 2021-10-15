using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndicatorController : MonoBehaviour
{
    public GameObject pointer = null;
    public float pointerY = 0;

    private void OnMouseEnter()
    {
        pointer.transform.position = new Vector3(-84.8f, pointerY, 1f);
    }

}