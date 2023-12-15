using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CI.QuickSave;  

public class RankingManager : MonoBehaviour
{

    [SerializeField]
    RankingDialog rankingDialog;

    void Start()
    {
        Ranking ranking = Ranking.GetInstance;

        // QuickSaveReader�̃C���X�^���X���쐬
        QuickSaveReader reader = QuickSaveReader.Create("Ranking");
        // �f�[�^��ǂݍ���
        Ranking rankingList = reader.Read<Ranking>("RankingList");

        foreach (var ranker in rankingList.rankers)
        {

            ranking.Add(ranker.score);
        }

        ranking.Add(Result.Instance.totalScore);

        rankingDialog.ShowRanking();

        ranking.Remove();

        // QuickSaveWriter�̃C���X�^���X���쐬
        QuickSaveWriter writer = QuickSaveWriter.Create("Ranking");
        // �f�[�^����������
        writer.Write("RankingList", ranking);

        // �ύX�𔽉f
        writer.Commit();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdateRanking();

            rankingDialog.ShowRanking();
        }
    }

    private void UpdateRanking()
    {
        Ranking ranking = Ranking.GetInstance;
       
        

        foreach (var ranker in ranking.Rankers)
        {
            Debug.Log($"�|�C���g�F{ranker.score}");
        }
    }

}