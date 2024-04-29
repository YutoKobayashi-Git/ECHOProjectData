using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObjectPool : MonoBehaviour
{
    // 文字列(string)→nameと、実際のオブジェクトのペアを管理するクラス
    [System.Serializable]
    public class PoolData
    {
        public string name;
        [Header("ヒエラルキー上で生成する場所(親)を指定")]
        public Transform parentGameObject;
        [Header("生成するプレファブ")]
        public GameObject prefabObj;
        [Header("生成するプレファブのRectTransform(スクリーン座標のみ使用)")]
        public RectTransform prefabRectTransform;
        // 貯めておくプール
        [System.NonSerialized]
        public List<GameObject> pool;
        [System.NonSerialized]
        public List<RectTransform> rectpool;
    }

    [SerializeField]
    private PoolData[] poolDatas;

    //別名(name)をキーとした管理用Dictionary
    private Dictionary<string, PoolData> poolDictionary = new Dictionary<string, PoolData>();

    List<GameObject> obj = null;

    public virtual void Awake()
    {
        //poolDictionaryにセット
        foreach (var poolData in poolDatas)
        {
            poolDictionary.Add(poolData.name, poolData);
        }
    }

    /// <summary>
    /// オブジェクトプール作成
    /// </summary>
    /// <param name="maxCount"></param>
    public virtual void Create_Pool(int maxCount, string name)
    {
        if (poolDictionary.TryGetValue(name, out var poolData)) //管理用Dictionary から、別名で探索
        {
            // 初期化
            poolData.pool = new List<GameObject>();

            for (int i = 0; i < maxCount; ++i)
            {
                // オブジェクト生成
                GameObject obj = Instantiate(poolData.prefabObj, poolData.parentGameObject);
                obj.name = name + i;
                obj.SetActive(false);
                poolData.pool.Add(obj);
            }
        }
        else
        {
            Debug.LogWarning($"その別名は登録されていません:{name}");
        }
    }

    /// objectPoolの規模感によりますが、setActiveは重いので(内部的にGetComponentしてます)colorと動作停止用のメソッドがあるとよいですね。
    /// <summary>
    /// オーバーライド関数
    /// 使うときに場所を指定して表示する:poolの中から非表示のオブジェクトを探してくる
    /// </summary>
    /// <param name="rect">スクリーン座標のオブジェクト</param>
    /// <param name="name">ペアの名前</param>
    /// <returns></returns>
    public GameObject Get_Obj(RectTransform rect, string name)
    {
        if (poolDictionary.TryGetValue(name, out var poolData)) //管理用Dictionary から、別名で探索
        {
            // 使ってないものを探してくる
            for (int i = 0; i < poolData.pool.Count; ++i)
            {
                if (poolData.pool[i].activeSelf == false)
                {
                    GameObject obj = poolData.pool[i];
                    // RectTransform代入
                    obj.GetComponent<RectTransform>().anchoredPosition3D = rect.anchoredPosition3D;
                    obj.SetActive(true);
                    return obj;
                }
            }

            // poolの中のものを全部使っていたら新たに生成する
            GameObject newObj = Instantiate(poolData.prefabObj, poolData.prefabRectTransform.anchoredPosition3D, Quaternion.identity, poolData.parentGameObject);
            newObj.SetActive(false);
            poolData.pool.Add(newObj);
            return newObj;
        }
        else
        {
            Debug.LogWarning($"その別名は登録されていません:{name}");
            return null;
        }
    }

    /// <summary>
    /// オーバーライド関数
    /// 使うときに場所を指定して表示する:poolの中から非表示のオブジェクトを探してくる
    /// </summary>
    /// <param name="rect">ワールド座標のオブジェクト</param>
    /// <param name="name">ペアの名前</param>
    /// <returns></returns>
    public GameObject Get_Obj(Vector3 position, string name)
    {
        if (poolDictionary.TryGetValue(name, out var poolData)) //管理用Dictionary から、別名で探索
        {
            // 使ってないものを探してくる
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

            // poolの中のものを全部使っていたら新たに生成する
            GameObject newObj = Instantiate(poolData.prefabObj, position, Quaternion.identity, poolData.parentGameObject);
            newObj.SetActive(false);
            poolData.pool.Add(newObj);
            return newObj;
        }
        else
        {
            Debug.LogWarning($"その別名は登録されていません:{name}");
            return null;
        }
    }

    public GameObject SetObjActiveFalse(string name)
    {
        if (poolDictionary.TryGetValue(name, out var poolData)) //管理用Dictionary から、別名で探索
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
            Debug.LogWarning($"その別名は登録されていません:{name}");
            return null;
        }
        return null;
    }
}

