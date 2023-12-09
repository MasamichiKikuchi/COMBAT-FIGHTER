using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
   
         public GameObject player;
         public GameObject enemyPrefab;

        
        void Start()
        {
            StartCoroutine(SpawnLoop());
        }

        private IEnumerator SpawnLoop()
        {
            while (true)
            {
                var distanceVector = new Vector3(100, 0, 0);

                var spawnPositionFromPlayer = Quaternion.Euler(0, Random.Range(0, 360f), 0) * distanceVector;

                var spawnPosition = player.transform.position + spawnPositionFromPlayer + distanceVector;

                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                
                yield return new WaitForSeconds(10);

                if (player == null)
                {
                    break;
                }
            }
        }



   

}
