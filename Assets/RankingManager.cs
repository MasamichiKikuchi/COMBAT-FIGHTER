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

        for (int i = 0; i < rankingList.rankers.Count; i++)
        {
            Debug.Log(rankingList.rankers[i].score);
        }



        foreach (var ranker in rankingList.rankers)
        {

            ranking.Add(ranker.score);
        }

        ranking.Add(Result.Instance.totalScore);
        
        //�ۑ���̊m�F
        //Debug.Log("�ۑ���:" + Application.persistentDataPath);
        // QuickSaveWriter�̃C���X�^���X���쐬
        QuickSaveWriter writer = QuickSaveWriter.Create("Ranking");
        // �f�[�^����������
        writer.Write("RankingList", ranking);

        // �ύX�𔽉f
        writer.Commit();

        rankingDialog.ShowRanking();
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
       
        ranking.Add(75);
        ranking.Add(88);
        ranking.Add(20);


        ranking.Add(10);

        foreach (var ranker in ranking.Rankers)
        {
            Debug.Log($"�|�C���g�F{ranker.score}");
        }
    }

    public void PlayerSave(QuickSaveSettings set)
    {
        Debug.Log("�ۑ���:" + Application.persistentDataPath);
        // QuickSaveWriter�̃C���X�^���X���쐬
        QuickSaveWriter writer = QuickSaveWriter.Create("Ranking", set);
        // �f�[�^����������
        //writer.Write("RankingList",;
       
        // �ύX�𔽉f
        writer.Commit();
    }

    public void PlayerLoad(QuickSaveSettings set)
    {
        // QuickSaveReader�̃C���X�^���X���쐬
        QuickSaveReader reader = QuickSaveReader.Create("Player", set);
        // �f�[�^��ǂݍ���
        string name = reader.Read<string>("Name");
        Vector3 position = reader.Read<Vector3>("Position");
        int level = reader.Read<int>("Level");

        Debug.Log("name:" + name + ", position:" + position + ",�@level:" + level);
    }
}