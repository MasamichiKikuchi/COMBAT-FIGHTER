using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobStatus : MonoBehaviour
{
    //�I�u�W�F�N�g��life
    public int life;
    //life�̍ő�l
    public int maxLife;

    // Start is called before the first frame update
    void Start()
    {
        //life�̒l���ő�l�ɐݒ�
        life = maxLife;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Damage(int damage)
    {
        //life�Ƀ_���[�W���󂯂�
        life -= damage;
    }
}
