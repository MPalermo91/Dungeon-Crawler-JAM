using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomOut : MonoBehaviour
{

    [SerializeField] private GameObject npc1;
    [SerializeField] private GameObject npc2;
    [SerializeField] private GameObject npc3;
    [SerializeField] private GameObject npc4;
    [SerializeField] private GameObject npc5; 

    [SerializeField] private Camera cam;

    void Update()
    {

        if(npc1.activeSelf || npc2.activeSelf || npc3.activeSelf || npc4.activeSelf || npc5.activeSelf)
        {
            //tamaño normal 5.5

            cam.orthographicSize = 5.0f;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            cam.orthographicSize = 5.5f;
        }




        }


    }
}
