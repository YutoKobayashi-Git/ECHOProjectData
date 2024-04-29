using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObjectPool : MonoBehaviour
{
    // ������(string)��name�ƁA���ۂ̃I�u�W�F�N�g�̃y�A���Ǘ�����N���X
    [System.Serializable]
    public class PoolData
    {
        public string name;
        [Header("�q�G�����L�[��Ő�������ꏊ(�e)���w��")]
        public Transform parentGameObject;
        [Header("��������v���t�@�u")]
        public GameObject prefabObj;
        [Header("��������v���t�@�u��RectTransform(�X�N���[�����W�̂ݎg�p)")]
        public RectTransform prefabRectTransform;
        // ���߂Ă����v�[��
        [System.NonSerialized]
        public List<GameObject> pool;
        [System.NonSerialized]
        public List<RectTransform> rectpool;
    }

    [SerializeField]
    private PoolData[] poolDatas;

    //�ʖ�(name)���L�[�Ƃ����Ǘ��pDictionary
    private Dictionary<string, PoolData> poolDictionary = new Dictionary<string, PoolData>();

    List<GameObject> obj = null;

    public virtual void Awake()
    {
        //poolDictionary�ɃZ�b�g
        foreach (var poolData in poolDatas)
        {
            poolDictionary.Add(poolData.name, poolData);
        }
    }

    /// <summary>
    /// �I�u�W�F�N�g�v�[���쐬
    /// </summary>
    /// <param name="maxCount"></param>
    public virtual void Create_Pool(int maxCount, string name)
    {
        if (poolDictionary.TryGetValue(name, out var poolData)) //�Ǘ��pDictionary ����A�ʖ��ŒT��
        {
            // ������
            poolData.pool = new List<GameObject>();

            for (int i = 0; i < maxCount; ++i)
            {
                // �I�u�W�F�N�g����
                GameObject obj = Instantiate(poolData.prefabObj, poolData.parentGameObject);
                obj.name = name + i;
                obj.SetActive(false);
                poolData.pool.Add(obj);
            }
        }
        else
        {
            Debug.LogWarning($"���̕ʖ��͓o�^����Ă��܂���:{name}");
        }
    }

    /// objectPool�̋K�͊��ɂ��܂����AsetActive�͏d���̂�(�����I��GetComponent���Ă܂�)color�Ɠ����~�p�̃��\�b�h������Ƃ悢�ł��ˁB
    /// <summary>
    /// �I�[�o�[���C�h�֐�
    /// �g���Ƃ��ɏꏊ���w�肵�ĕ\������:pool�̒������\���̃I�u�W�F�N�g��T���Ă���
    /// </summary>
    /// <param name="rect">�X�N���[�����W�̃I�u�W�F�N�g</param>
    /// <param name="name">�y�A�̖��O</param>
    /// <returns></returns>
    public GameObject Get_Obj(RectTransform rect, string name)
    {
        if (poolDictionary.TryGetValue(name, out var poolData)) //�Ǘ��pDictionary ����A�ʖ��ŒT��
        {
            // �g���ĂȂ����̂�T���Ă���
            for (int i = 0; i < poolData.pool.Count; ++i)
            {
                if (poolData.pool[i].activeSelf == false)
                {
                    GameObject obj = poolData.pool[i];
                    // RectTransform���
                    obj.GetComponent<RectTransform>().anchoredPosition3D = rect.anchoredPosition3D;
                    obj.SetActive(true);
                    return obj;
                }
            }

            // pool�̒��̂��̂�S���g���Ă�����V���ɐ�������
            GameObject newObj = Instantiate(poolData.prefabObj, poolData.prefabRectTransform.anchoredPosition3D, Quaternion.identity, poolData.parentGameObject);
            newObj.SetActive(false);
            poolData.pool.Add(newObj);
            return newObj;
        }
        else
        {
            Debug.LogWarning($"���̕ʖ��͓o�^����Ă��܂���:{name}");
            return null;
        }
    }

    /// <summary>
    /// �I�[�o�[���C�h�֐�
    /// �g���Ƃ��ɏꏊ���w�肵�ĕ\������:pool�̒������\���̃I�u�W�F�N�g��T���Ă���
    /// </summary>
    /// <param name="rect">���[���h���W�̃I�u�W�F�N�g</param>
    /// <param name="name">�y�A�̖��O</param>
    /// <returns></returns>
    public GameObject Get_Obj(Vector3 position, string name)
    {
        if (poolDictionary.TryGetValue(name, out var poolData)) //�Ǘ��pDictionary ����A�ʖ��ŒT��
        {
            // �g���ĂȂ����̂�T���Ă���
            for (int i = 0; i < poolData.pool.Count; ++i)
            {
                if (poolData.pool[i].activeSelf == false)
                {
                    GameObject obj = poolData.pool[i];
                    obj.transform.position = position;
                    obj.SetActive(true);
                    return obj;
                }
            }

            // pool�̒��̂��̂�S���g���Ă�����V���ɐ�������
            GameObject newObj = Instantiate(poolData.prefabObj, position, Quaternion.identity, poolData.parentGameObject);
            newObj.SetActive(false);
            poolData.pool.Add(newObj);
            return newObj;
        }
        else
        {
            Debug.LogWarning($"���̕ʖ��͓o�^����Ă��܂���:{name}");
            return null;
        }
    }

    public GameObject SetObjActiveFalse(string name)
    {
        if (poolDictionary.TryGetValue(name, out var poolData)) //�Ǘ��pDictionary ����A�ʖ��ŒT��
        {
            for (int i = 0; i < poolData.pool.Count; ++i)
            {
                if (poolData.pool[i].activeSelf == true)
                {
                    GameObject obj = poolData.pool[i];
                    obj.SetActive(false);
                    return obj;
                }
            }
        }
        else
        {
            Debug.LogWarning($"���̕ʖ��͓o�^����Ă��܂���:{name}");
            return null;
        }
        return null;
    }
}

