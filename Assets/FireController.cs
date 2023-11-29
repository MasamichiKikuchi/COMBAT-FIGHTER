using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FireController : MonoBehaviour
{
    public GameObject player;
    private void OnTriggerEnter(Collider other)
    {
        

        player.GetComponent<PlayerController>().tagetObject = other;
       
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
