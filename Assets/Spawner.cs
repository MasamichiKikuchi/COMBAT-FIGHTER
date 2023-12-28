using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//敵の出現を管理するクラス
public class Spawner : MonoBehaviour
{     
    //プレイヤーの位置
    public GameObject player; 
    //敵のプレハブ
    public GameObject enemyPrefab;
        
    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

       
    private IEnumerator SpawnLoop()
    {
        　　
        //一定時間ごとにプレイヤーの周囲のランダムな位置に敵を出現         
        while (true)           
        {               
            var distanceVector = new Vector3(0, 0, Random.Range(200, 500));              
            var spawnPositionFromPlayer = Quaternion.Euler(0, Random.Range(0, 360f), 0) * distanceVector;            　
            var spawnPosition = player.transform.position + spawnPositionFromPlayer + distanceVector;             
            yield return new WaitForSeconds(5);               
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);         
            
            
            if (player == null)
            {
                //プレイヤーが撃墜されたら終了  
                break;
            }
        }
    }



   

}
