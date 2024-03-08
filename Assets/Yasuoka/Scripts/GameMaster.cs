using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public List<string> fruitsNameList = new List<string> { "cherry", "strawberry", "grape", "dekopon", "persimmon", "apple", "pear", "peach", "pineapple", "melon", "watermelon" };
    public GameObject[] fruitsObject = new GameObject[11];

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(fruitsObject[Random.Range(0, 5)], new Vector3(0.5f, 15f, 10f), Quaternion.identity);
    }

    void CreateFruit()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
