using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsObjectCreateScript : MonoBehaviour
{
    //public List<string> fruitsNameList = new List<string> { "cherry", "strawberry", "grape", "dekopon", "persimmon", "apple", "pear", "peach", "pineapple", "melon", "watermelon" };
    //public GameObject[] fruitsObject = new GameObject[11];

    [Header("ステージ円の設定")]
    // 円の半径
    [SerializeField] private float _radius = 1.0f;
    // 円の中心点
    [SerializeField] private Vector3 _center;

    [Header("配置するPrefab")]
    [SerializeField] private int Prefab_num = 30;
    [SerializeField] private GameObject _prefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (UIDirector.timerIsRunning)
        {
            GameObject[] allCherry = GameObject.FindGameObjectsWithTag("cherry");
            int remainCherry = allCherry.Length;
            // 所定の個数よりも少なくなったら
            if (remainCherry < Prefab_num)
            {
                // 指定された半径の円内のランダム位置を取得
                var circlePos = _radius * Random.insideUnitCircle;

                // XZ平面で指定された半径、中心点の円内のランダム位置を計算
                var spawnPos = new Vector3(circlePos.x, 5, circlePos.y) + _center;

                // Prefabを追加
                Instantiate(_prefab, spawnPos, Quaternion.identity);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_center, _radius);
    }

    void OnBecameInvisible()
    {
        GameObject.Destroy(this.gameObject);
    }
}
