using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameSystem : MonoBehaviour
{
    // のこり
    // ・スコア
    // ・ドラッグの時
    // 　・Ballを少し大きくする
    // 　・色をかえる
    // ・弾けるエフェクト
    // ・パラメータ調節

    [SerializeField] SappleGenerator sappleGenerator = default;
    bool isDragging;
    [SerializeField] List<Sapple> removeSapples = new List<Sapple>(); //なんでこれSerializeFieldが必要なんだ？
    Sapple currentDraggingSapple = default;
    int score;
    [SerializeField] TextMeshProUGUI scoreText = default;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(sappleGenerator.Spawns(ParamsSO.Entity.initSappleCount));
        score = 0;
        AddScore(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            OnDragBegin();
        }
        else if(Input.GetMouseButtonUp(0))
        {
            OnDragEnd();
        }
        if(isDragging)
        {
            OnDragging();
        }
    }


    void AddScore(int point)
    {
        score += point;
        scoreText.text = score.ToString();
    }


    void OnDragBegin()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if(hit && hit.collider.GetComponent<Sapple>()) //何かしらにヒットしてそれがサップルのコンポーネントを持っていれば
        {
            Sapple sapple = hit.collider.GetComponent<Sapple>();
            AddRemoveSapple(sapple);
            isDragging = true;
        }
    }


    void OnDragging()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit && hit.collider.GetComponent<Sapple>()) 
        {
            Sapple sapple = hit.collider.GetComponent<Sapple>();

            //同じ種類
            if(sapple.id == currentDraggingSapple.id)
            {
                float distance = Vector2.Distance(sapple.transform.position, currentDraggingSapple.transform.position);

                if (distance < ParamsSO.Entity.sappleDistance)
                {
                    AddRemoveSapple(sapple);
                }
            }
        }
    }

    void OnDragEnd()
    {
        int removeCount = removeSapples.Count;

        if(removeCount >= 3)
        {
            for (int i = 0; i < removeCount; i++)
            {
                removeSapples[i].Explosion();
            }

            StartCoroutine(sappleGenerator.Spawns(removeCount));

            AddScore(removeCount * ParamsSO.Entity.scorePoint);
        }

        for (int i = 0; i < removeCount; i++)
        {
            removeSapples[i].transform.localScale = Vector3.one * 1.3f;
        }

        removeSapples.Clear();

        isDragging = false;
    }

    void AddRemoveSapple(Sapple sapple)
    {
        currentDraggingSapple = sapple;

        if(removeSapples.Contains(sapple) == false) //リストにそのサップルが追加されていない場合、リストに追加する
        {
            sapple.transform.localScale = Vector3.one * 1.65f;
            removeSapples.Add(sapple);
        }
    }

}
