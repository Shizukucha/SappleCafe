using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SappleGenerator : MonoBehaviour
{
    [SerializeField] GameObject sapplePrefab = default;
    [SerializeField] GameObject[] itemPrefabs = default;

    [SerializeField] Sprite[] sappleSprites = default;

    public IEnumerator Spawns(int count)
    {
        for(int i = 0; i < count; i++)
        {
            if(Random.Range(0, 100) < 5) //5%の確率でアイテムが出る
            {
                Vector2 pos = new Vector2(Random.Range(7.8f, 8.6f), 13f);
                GameObject item = Instantiate(itemPrefabs[Random.Range(0, itemPrefabs.Length)], pos, Quaternion.identity);
                yield return new WaitForSeconds(0.06f);
            }
            else
            {
                Vector2 pos = new Vector2(Random.Range(7.8f, 8.6f), 13f);
                GameObject sapple = Instantiate(sapplePrefab, pos, Quaternion.identity);
                int sappleID = Random.Range(0, sappleSprites.Length);
                sapple.GetComponent<SpriteRenderer>().sprite = sappleSprites[sappleID];
                sapple.GetComponent<Sapple>().id = sappleID;
                yield return new WaitForSeconds(0.06f);
            }
        }
    }

}
