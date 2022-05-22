
using UnityEngine;

public class EventCameraTouchWater : MonoBehaviour
{
   
    public GameObject WaterSea;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            WaterSea.SetActive(true);

        }

    }
   
}
