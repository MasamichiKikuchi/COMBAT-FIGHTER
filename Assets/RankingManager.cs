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
        Ranking ranking = Ranking.GetInstance;

        // QuickSaveReader�̃C���X�^���X���쐬
        QuickSaveReader reader = QuickSaveReader.Create("Ranking");
        // �Z�[�u����Ă���f�[�^��ǂݍ���
        Ranking rankingList = reader.Read<Ranking>("RankingList");
   
        foreach (var ranker in rankingList.rankers)
        {
            ranking.Add(ranker.score);
        }

        //����̃X�R�A
        ranking.Add(Result.Instance.totalScore);

        rankingDialog.ShowRanking();

        ranking.Remove();

        //�����L���O�f�[�^�S����
        //ranking.rankers.Clear();
       
        // QuickSaveWriter�̃C���X�^���X���쐬
        QuickSaveWriter writer = QuickSaveWriter.Create("Ranking");
        // �f�[�^����������
        writer.Write("RankingList", ranking);
        // �ύX�𔽉f
        writer.Commit();
    }

}