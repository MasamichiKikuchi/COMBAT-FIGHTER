using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

//�~�j�}�b�v�Ɋւ���N���X
public class MiniMap : MonoBehaviour
{
    //�~�j�}�b�v�p�̃J����
    public Camera miniMapCamera;
    //�~�j�}�b�v�𓊉e����C���[�W
    public RawImage miniMapImage;
    //�~�j�}�b�v��\������e�N�X�`��
    Texture2D texture2D;
    //�~�j�}�b�v�J��������̉f�����摜�ɂ��郌���_�[�e�N�X�`��
    RenderTexture renderTexture;
    // �v���C���[�A�C�R����Prefab
    public GameObject playerIconPrefab;
    // �v���C���[�A�C�R���̃C���[�W
    private Image playerIcon;            
    // �v���C���[��Transform
    public Transform playerTransform;   
    // �G�A�C�R����Prefab
    public GameObject enemyIconPrefab;
    // �G�A�C�R����z�u����e�I�u�W�F�N�g
    public Transform enemyIconsParent;   
    //�Q�[����ɑ��݂���G�̃��X�g
    public static List<GameObject> enemies;
    // �e�G�ɑΉ�����A�C�R�����Ǘ�����f�B�N�V���i���[
    private Dictionary<Transform, Image> enemyIcons;
    // �~�j�}�b�v��RectTransform
    public RectTransform miniMapRect;  
    
    private void Awake()
    {
        renderTexture = new RenderTexture(miniMapCamera.pixelWidth, miniMapCamera.pixelHeight, 24);
        enemyIcons = new Dictionary<Transform, Image>();
        enemies = new List<GameObject>();
    }
    void Start()
    {   
        //�~�j�}�b�v�J�����̉f�����摜�Ƃ��ă����_�[�e�N�X�`���ɑ��
        miniMapCamera.targetTexture = renderTexture;
       //�~�j�}�b�v�\���p�̃e�N�X�`���쐬
        texture2D = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        //�v���C���[�A�C�R���쐬
        playerIcon = Instantiate(playerIconPrefab, transform).GetComponent<Image>();
    }

    void Update()
    {
        //�~�j�}�b�v�J�����̉摜���~�j�}�b�v�ɕ\��
        RenderTexture.active = renderTexture;
        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();
        miniMapImage.texture = texture2D;

        //�v���C���[�A�C�R���̕\��
        UpdatePlayerIcon();
        //�G�A�C�R���̕\��
        UpdateEnemyIcons();    

    }
    
    void UpdatePlayerIcon()
    {
        //�~�j�}�b�v�J�������̃v���C���[�ʒu���擾
        Vector3 playerPos = miniMapCamera.WorldToViewportPoint(playerTransform.position);
        // �~�j�}�b�v��ł̐[�x��0�ɐݒ�
        playerPos.z = 0f; 
        // �v���C���[�A�C�R���̕\���ʒu���X�V
        playerIcon.rectTransform.anchoredPosition = new Vector2(playerPos.x * miniMapImage.rectTransform.rect.width, playerPos.y * miniMapImage.rectTransform.rect.height);
    }

    void UpdateEnemyIcons()
    {
        foreach (GameObject enemy in enemies)
        {
            //�~�j�}�b�v�J�������̓G�ʒu���擾
            Vector3 enemyPos = miniMapCamera.WorldToViewportPoint(enemy.transform.position);
            // �~�j�}�b�v��ł̐[�x��0�ɐݒ�
            enemyPos.z = 0f;  
           
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
            }

            //�G�̌������A�C�R���ɔ��f������
            Quaternion worldRotation = enemy.transform.rotation;
            Matrix4x4 inverseTransformMatrix = miniMapCamera.transform.worldToLocalMatrix;
            Vector3 localForward = inverseTransformMatrix.MultiplyVector(worldRotation * Vector3.forward);
            float iconRotationAngle = Mathf.Atan2(localForward.x, localForward.z) * Mathf.Rad2Deg;
            enemyIcon.rectTransform.localEulerAngles = new Vector3(0f, 0f, -iconRotationAngle);
           
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
        //�Ή�����G�����݂��Ȃ��ꍇ�A�A�C�R�����폜
        if (enemyIcons.TryGetValue(enemy.transform, out Image enemyIcon))
        {
            Destroy(enemyIcon.gameObject);
            enemyIcons.Remove(enemy.transform);           
        }
    }

}