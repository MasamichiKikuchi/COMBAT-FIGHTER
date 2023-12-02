using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   
    int hp;
    int maxHp =1;
   
    public BoxCollider enemyEscapeArea; //�v���C���[�����m���������s�����Ƃ�͈͂̃{�b�N�X�R���C�_�[�R���|�[�l���g
    public float avoidanceSpeed = 200f;    // ����s�����̑��x
    private Vector3 avoidanceDirection; // ����s���̕���
    
    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, 0.1f); 

    }

    public void Damage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        { 
          Destroy(gameObject);
          FireController.enemies.Remove(gameObject);     
        }
    }
    public void AvoidMove(Collider collider)
    {          
           // ����s���̕������v�Z
            avoidanceDirection = transform.position - collider.transform.position;
            avoidanceDirection.y = 0f; // Y�������̕ω��𖳎�
      
        // ����s���̕����Ɍ������Ĉړ�
        transform.Translate(avoidanceDirection.normalized * avoidanceSpeed * Time.deltaTime);
    }


}
