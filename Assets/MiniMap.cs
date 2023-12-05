using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{
    public Camera miniMapCamera;
    public RawImage miniMapImage;
    RenderTexture renderTexture;
    public GameObject playerIconPrefab;  // �v���C���[�A�C�R����Prefab
    public GameObject enemyIconPrefab;   // �G�A�C�R����Prefab
    public Transform playerTransform;    // �v���C���[��Transform
    public Transform enemyParent;        // �G�̐e�I�u�W�F�N�g
    public Transform enemyIconsParent;   // �G�A�C�R����z�u����e�I�u�W�F�N�g

    private Image playerIcon;            // �v���C���[�A�C�R��
    private Dictionary<Transform, Image> enemyIcons; // �e�G�ɑΉ�����A�C�R��

    void Start()
    {
        renderTexture = new RenderTexture(miniMapCamera.pixelWidth, miniMapCamera.pixelHeight, 24);
        miniMapCamera.targetTexture = renderTexture;

        playerIcon = Instantiate(playerIconPrefab, transform).GetComponent<Image>();
        //enemyIcons = new Dictionary<Transform, Image>();
    }

    void LateUpdate()
    {
        Texture2D texture2D = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        RenderTexture.active = renderTexture;
        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();
        RenderTexture.active = null;

        miniMapImage.texture = texture2D;

        UpdatePlayerIcon();
        //UpdateEnemyIcons();
    }
    
    void UpdatePlayerIcon()
    {
        Vector3 playerPos = miniMapCamera.WorldToViewportPoint(playerTransform.position);
        playerPos.z = 0f;  // �~�j�}�b�v��ł̐[�x��0�ɐݒ�

        // �v���C���[�A�C�R���̕\���ʒu���X�V
        playerIcon.rectTransform.anchoredPosition = new Vector2(playerPos.x * miniMapImage.rectTransform.rect.width, playerPos.y * miniMapImage.rectTransform.rect.height);
    }

    void UpdateEnemyIcons()
    {
        foreach (Transform enemy in enemyParent.transform)
        {
            Vector3 enemyPos = miniMapCamera.WorldToViewportPoint(enemy.position);
            enemyPos.z = 0f;  // �~�j�}�b�v��ł̐[�x��0�ɐݒ�

            // �G�A�C�R���̕\���ʒu���X�V
            Image enemyIcon = GetEnemyIcon(enemy);
            enemyIcon.rectTransform.anchoredPosition = new Vector2(enemyPos.x * miniMapImage.rectTransform.rect.width, enemyPos.y * miniMapImage.rectTransform.rect.height);
        }
    }

    Image GetEnemyIcon(Transform enemy)
    {
        // �e�G�ɑΉ�����A�C�R�������݂��Ȃ��ꍇ�A�V���ɍ쐬
        if (!enemyIcons.ContainsKey(enemy))
        {
            GameObject enemyIcon = Instantiate(enemyIconPrefab, enemyIconsParent);
            enemyIcons[enemy] = enemyIcon.GetComponent<Image>();
        }

        return enemyIcons[enemy];
    }
    
}