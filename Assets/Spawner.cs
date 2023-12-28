using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//�G�̏o�����Ǘ�����N���X
public class Spawner : MonoBehaviour
{     
    //�v���C���[�̈ʒu
    public GameObject player; 
    //�G�̃v���n�u
    public GameObject enemyPrefab;
        
    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

       
    private IEnumerator SpawnLoop()
    {
        �@�@
        //��莞�Ԃ��ƂɃv���C���[�̎��͂̃����_���Ȉʒu�ɓG���o��         
        while (true)           
        {               
            var distanceVector = new Vector3(0, 0, Random.Range(200, 500));              
            var spawnPositionFromPlayer = Quaternion.Euler(0, Random.Range(0, 360f), 0) * distanceVector;            �@
            var spawnPosition = player.transform.position + spawnPositionFromPlayer + distanceVector;             
            yield return new WaitForSeconds(5);               
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);         
            
            
            if (player == null)
            {
                //�v���C���[�����Ă��ꂽ��I��  
                break;
            }
        }
    }



   

}
