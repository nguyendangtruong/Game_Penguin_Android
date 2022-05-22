
using UnityEngine;
using System.Collections;

public class PlayerColision : MonoBehaviour
{
    // Start is called before the first frame update
    public Character movement;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag== "Vatcan")
        {
            //Debug.Log("wwww");
            movement.enabled = false;
            FindObjectOfType<GameManager>().EndGame();
        }
        
    }
    
}
