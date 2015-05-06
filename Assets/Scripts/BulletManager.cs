using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletManager : MonoBehaviour
{
    // "Consts"

    public int BulletPoolStart;
    public float BulletMaxTime;
    public float BulletSpeed;

    public GameObject Bullet;  
    public static BulletManager current;

    private List<GameObject> bullets;

    void Awake()
    {
        current = this;
    }

    // Use this for initialization
    void Start()
    {
        bullets = new List<GameObject>(BulletPoolStart);
        for (int i = 0; i < BulletPoolStart; i++)
        {
            var b = (GameObject)Instantiate(Bullet);
            b.SetActive(false);
            bullets.Add(b);
        }
    }

    public GameObject GetBullet()
    {
        for (int i = 0; i < bullets.Count; i++)
            if (!bullets[i].activeInHierarchy)
                return bullets[i];

        var b = (GameObject)Instantiate(Bullet);
        b.SetActive(false);
        bullets.Add(b);
        return b;
    }
}
