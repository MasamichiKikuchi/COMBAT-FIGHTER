using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�L�����N�^�[�̃X�e�[�^�X�Ɋւ���N���X
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

    public virtual void Damage(int damage)
    {
        //life�Ƀ_���[�W���󂯂�
        life -= damage;
    }
}
