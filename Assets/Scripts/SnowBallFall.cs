using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBallFall : MonoBehaviour
{
    
    public Transform charac;
    public GameObject snowBall;

    void Update()
    {
        if(charac.position.z > 100)
        {
            snowBall.SetActive(true);
   
        }    
    }
}
