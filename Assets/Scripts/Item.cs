using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int id;
    [SerializeField] GameObject explosionPrefab;

    public void Explosion()
    {
        Destroy(gameObject);
        //爆破エフェクトを生成して破壊
        GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(explosion, 0.2f);
    }
}
