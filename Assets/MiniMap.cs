using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{
    public Camera miniMapCamera;//�~�j�}�b�v�p�̃J����
    public RawImage miniMapImage;//�~�j�}�b�v��\������C���[�W
    RenderTexture renderTexture;
    public GameObject playerIconPrefab;  // �v���C���[�A�C�R����Prefab
    public GameObject enemyIconPrefab;   // �G�A�C�R����Prefab
    public Transform playerTransform;    // �v���C���[��Transform
    public static List<GameObject> enemies;
    public Transform enemyIconsParent;   // �G�A�C�R����z�u����e�I�u�W�F�N�g

    private Image playerIcon;            // �v���C���[�A�C�R��
    private Dictionary<Transform, Image> enemyIcons; // �e�G�ɑΉ�����A�C�R��

    public RectTransform miniMapRect;  // �~�j�}�b�v��RectTransform�iInspector�Őݒ�j

    private void Awake()
    {
        renderTexture = new RenderTexture(miniMapCamera.pixelWidth, miniMapCamera.pixelHeight, 24);
        enemyIcons = new Dictionary<Transform, Image>();
        enemies = new List<GameObject>();
    }
    void Start()
    {   
        miniMapCamera.targetTexture = renderTexture;
        playerIcon = Instantiate(playerIconPrefab, transform).GetComponent<Image>();   
    }

    void Update()
    {
        Texture2D texture2D = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        RenderTexture.active = renderTexture;
        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();
        RenderTexture.active = null;

        miniMapImage.texture = texture2D;

        UpdatePlayerIcon();
        UpdateEnemyIcons();
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
        foreach (GameObject enemy in enemies)
        {
            Vector3 enemyPos = miniMapCamera.WorldToViewportPoint(enemy.transform.position);
            enemyPos.z = 0f;  // �~�j�}�b�v��ł̐[�x��0�ɐݒ�

            // �G�A�C�R���̕\���ʒu���X�V
            Image enemyIcon = GetEnemyIcon(enemy.transform);
            enemyIcon.rectTransform.anchoredPosition = new Vector2(enemyPos.x * miniMapImage.rectTransform.rect.width, enemyPos.y * miniMapImage.rectTransform.rect.height);

            if (IsInMiniMapBounds(enemyIcon.rectTransform.anchoredPosition))
            {
                // �~�j�}�b�v�͈͓̔��ɂ���ꍇ�A�A�C�R����\�����邩�ĕ\������
                SetIconVisibility(enemy.transform, true);
            }

            else 
            {
                // �~�j�}�b�v�͈̔͊O�ɂ���ꍇ�A�A�C�R�����\���ɂ���
                SetIconVisibility(enemy.transform, false);
                //continue; // ���̓G�ɐi��
            }        
        }    
    }

    bool IsInMiniMapBounds(Vector2 mapPosition)
    {
        // �~�j�}�b�v�͈̔͂𔻒肷�鏈��
        return mapPosition.x >= 0 && mapPosition.x <= miniMapRect.rect.width
            && mapPosition.y >= 0 && mapPosition.y <= miniMapRect.rect.height;
    }

    void SetIconVisibility(Transform enemyTransform, bool isVisible)
    {
        // �A�C�R���̕\���E��\����؂�ւ��鏈��
        if (enemyIcons.TryGetValue(enemyTransform, out Image enemyIcon))
        {
            enemyIcon.gameObject.SetActive(isVisible);
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

    public void RemoveEnemyIcon(GameObject enemy)
    {
        if (enemyIcons.TryGetValue(enemy.transform, out Image enemyIcon))
        {
            Destroy(enemyIcon.gameObject);
            enemyIcons.Remove(enemy.transform);           
        }
    }

}