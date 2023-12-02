using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject enemy;
    Enemy _enemy;
    // Start is called before the first frame update
    void Start()
    {
        _enemy = enemy.GetComponent<Enemy>();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
       _enemy.AvoidMove(other);
    }

}
