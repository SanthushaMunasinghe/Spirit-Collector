using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BasePool : MonoBehaviour
{
    private static BasePool _instance;

    public ObjectPool<GameObject> playerBulletPool;

    [SerializeField] private GameObject playerBulletPrefab;

    public int bulletCount = 0;

    public static BasePool Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        playerBulletPool = NewPool(playerBulletPrefab, 30, 100);
    }

    private ObjectPool<GameObject> NewPool(GameObject prefab, int minValue = 241, int maxValue = 300, bool check = false)
    {
        return new ObjectPool<GameObject>(() =>
        {
            return Instantiate(prefab);
        },
            (GameObject obj) => obj.gameObject.SetActive(true),
            (GameObject obj) => obj.gameObject.SetActive(false),
            (GameObject obj) => Destroy(obj),
            check,
            minValue,
            maxValue);
    }
}
