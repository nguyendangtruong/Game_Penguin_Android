using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public Transform tf;
    private int objGo=0;
    public float limitMarginX=4;

    void Update()
    {


        if(tf.position.x < -limitMarginX)
        {
            objGo = 1;
        }
        if (tf.position.x > limitMarginX)
        {
            objGo = 0;
        }
        if (objGo==0)
        {
            rb.AddForce(-1000 * Time.deltaTime, 0, 0);

        }
        else
        {
            rb.AddForce(1000 * Time.deltaTime, 0, 0);
        }
       
    }
}
