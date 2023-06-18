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
}
