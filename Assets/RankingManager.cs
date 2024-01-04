using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CI.QuickSave;  

//�����L���O��ʂ̓�����f�[�^�����Ǘ�����N���X
public class RankingManager : MonoBehaviour
{
    //�����L���O�\���p�̃p�l��
    public RankingDialog rankingDialog;

    void Start()
    {
        //�����L���O�̃V���O���g���C���X�^���X���擾
        Ranking ranking = Ranking.GetInstance;

        // QuickSaveReader(�f�[�^�Z�[�u�̃A�Z�b�g)�̃C���X�^���X���쐬
        QuickSaveReader reader = QuickSaveReader.Create("Ranking");
        // �Z�[�u����Ă���f�[�^��ǂݍ���
        Ranking rankingList = reader.Read<Ranking>("RankingList");
   
        foreach (var ranker in rankingList.rankers)
        {
       �@�@ //�A�Z�b�g�ɃZ�[�u����Ă������J�[�������L���O���X�g�ɓ����
            ranking.Add(ranker.totalScore);
        }

        //����̃v���C���[�X�R�A
        ranking.Add(Score.Instance.totalScore);

        //��ʂɃ����L���O�\��
        rankingDialog.ShowRanking();

        //�����L���O5�ʈȉ��̃f�[�^�������L���O����폜
        ranking.Remove();

        //�����L���O�f�[�^�S�����@���Z�[�u����Ă��f�[�^���S�ď�����̂Œ���
        //ranking.rankers.Clear();

        // QuickSaveWriter(�f�[�^�Z�[�u�̃A�Z�b�g)�̃C���X�^���X���쐬
        QuickSaveWriter writer = QuickSaveWriter.Create("Ranking");
        // �f�[�^����������
        writer.Write("RankingList", ranking);
        // �ύX�𔽉f
        writer.Commit();

        //�f�[�^���d�����Ȃ��悤�ɕۑ��ς݂̃����L���O�f�[�^�����Z�b�g����
       ranking.rankers.Clear();

    }



}