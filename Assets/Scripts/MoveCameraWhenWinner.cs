using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraWhenWinner : MonoBehaviour
{
    public Transform endMarker = null; // create an empty gameobject and assign in inspector
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, endMarker.position, Time.deltaTime);
        transform.rotation = Quaternion.Euler(25, 0, 0);


    }
}
