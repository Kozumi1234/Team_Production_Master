using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeFruitsScript : MonoBehaviour
{
    public static int fruitsNum;

    public static int difference;//変化する階級差

    [SerializeField] private int o_max;

    [SerializeField] private int evolutionGrapeCount;//ブドウに進化するために必要なサクランボの個数
    [SerializeField] private int evolutionDekoponCount;
    [SerializeField] private int evolutionPersimmonCount;
    [SerializeField] private int evolutionAppleCount;
    [SerializeField] private int evolutionPearCount;
    [SerializeField] private int evolutionPeachCount;
    [SerializeField] private int evolutionPineappleCount;
    [SerializeField] private int evolutionMelonCount;
    [SerializeField] private int evolutionWatermelonCount;
    int cherryCount;
    int currentFruitNum;
    bool is_Evolution = false;//進化したときtrue
    bool is_Degeneration = false;//退化したときtrue

    public static GameObject[] childObject;

    void Start()
    {
        fruitsNum = 0;
        difference = 0;
        o_max = this.transform.childCount;//子オブジェクトの個数取得
        childObject = new GameObject[o_max];//インスタンス作成

        for (int i = 0; i < o_max; i++)
        {
            childObject[i] = transform.GetChild(i).gameObject;//すべての子オブジェクト取得
        }
        //すべての子オブジェクトを非アクティブ
        foreach (GameObject gamObj in childObject)
        {
            gamObj.SetActive(false);
        }
        //最初はひとつだけアクティブ化しておく
        childObject[fruitsNum].SetActive(true);
    }

    void Update()
    {
        cherryCount = Player.cherryCount;

        if (cherryCount < evolutionGrapeCount)
        {
            fruitsNum = 0;
        }
        else if (evolutionGrapeCount <= cherryCount && cherryCount < evolutionDekoponCount)
        {
            fruitsNum = 1;
        }
        else if (evolutionDekoponCount <= cherryCount && cherryCount < evolutionPersimmonCount)
        {
            fruitsNum = 2;
        }
        else if (evolutionPersimmonCount <= cherryCount && cherryCount < evolutionAppleCount)
        {
            fruitsNum = 3;
        }
        else if (evolutionAppleCount <= cherryCount && cherryCount < evolutionPearCount)
        {
            fruitsNum = 4;
        }
        else if (evolutionPearCount <= cherryCount && cherryCount < evolutionPeachCount)
        {
            fruitsNum = 5;
        }
        else if (evolutionPeachCount <= cherryCount && cherryCount < evolutionPineappleCount)
        {
            fruitsNum = 6;
        }
        else if (evolutionPineappleCount <= cherryCount && cherryCount < evolutionMelonCount)
        {
            fruitsNum = 7;
        }
        else if (evolutionMelonCount <= cherryCount && cherryCount < evolutionWatermelonCount)
        {
            fruitsNum = 8;
        }
        else if (evolutionWatermelonCount <= cherryCount)
        {
            fruitsNum = 9;
        }

        if(currentFruitNum != fruitsNum)
        {
            if (currentFruitNum < fruitsNum)
            {
                is_Evolution = true;
                difference = (fruitsNum - currentFruitNum);
}
            else if(currentFruitNum > fruitsNum)
            {
                is_Degeneration = true;
                difference = (currentFruitNum - fruitsNum);
}
        }

        currentFruitNum = fruitsNum;

        if(is_Evolution)
        {
            //現在のアクティブな子オブジェクトの座標を取得
            Vector3 pos = childObject[(fruitsNum - difference)].transform.localPosition;
            //childObject[fruitsNum].transform.localScale = new Vector3(x, y, z);

            //現在のアクティブな子オブジェクトを非アクティブ
            childObject[(fruitsNum - difference)].SetActive(false);

            //fruitsNum++;
            childObject[fruitsNum].GetComponent<Player>().ChangePlayerState();

            GameObject MainCamera = GameObject.Find("MainCamera");
            MainCamera.GetComponent<CameraController>().EvolutionChangeCamera();

            //string cameraObjectName = (Player.playerState + "Camera");
            //GameObject cameraObject = GameObject.Find(cameraObjectName);
            //cameraObject.GetComponent<changeCameraScript>().CameraChange();

            //子オブジェクトをすべて切り替えたらまた最初のオブジェクトに戻る
            //if (fruitsNum == o_max) { fruitsNum = 0; }

            //次のオブジェクトのコライダーサイズを取得し、大きい分だけy座標を上に移動してからアクティブ化する
            //var col = childObject[fruitsNum].GetComponent<Player>().sphCol_2;
            //pos.y += col.radius;

            //次のオブジェクトをアクティブ化
            childObject[fruitsNum].transform.position = pos + new Vector3(0,8,0);
            childObject[fruitsNum].GetComponent<Rigidbody>().velocity = Vector3.zero;
            childObject[fruitsNum].SetActive(true);

            is_Evolution = false;
        }
        else if (is_Degeneration)
        {
            //現在のアクティブな子オブジェクトの座標を取得
            Vector3 pos = childObject[(fruitsNum + difference)].transform.localPosition;
            //childObject[fruitsNum].transform.localScale = new Vector3(x, y, z);

            //現在のアクティブな子オブジェクトを非アクティブ
            childObject[(fruitsNum + difference)].SetActive(false);

            //fruitsNum++;
            childObject[fruitsNum].GetComponent<Player>().ChangePlayerState();

            GameObject MainCamera = GameObject.Find("MainCamera");
            MainCamera.GetComponent<CameraController>().DegenerationChangeCamera();

            //string cameraObjectName = (Player.playerState + "Camera");
            //GameObject cameraObject = GameObject.Find(cameraObjectName);
            //cameraObject.GetComponent<changeCameraScript>().CameraChange();

            //子オブジェクトをすべて切り替えたらまた最初のオブジェクトに戻る
            //if (fruitsNum == o_max) { fruitsNum = 0; }

            //次のオブジェクトのコライダーサイズを取得し、大きい分だけy座標を上に移動してからアクティブ化する
            //var col = childObject[fruitsNum].GetComponent<Player>().sphCol_2;
            //pos.y += col.radius;

            //次のオブジェクトをアクティブ化
            childObject[fruitsNum].transform.position = pos + new Vector3(0, 8, 0);
            childObject[fruitsNum].GetComponent<Rigidbody>().velocity = Vector3.zero;
            childObject[fruitsNum].SetActive(true);

            is_Degeneration = false;
        }
    }
}
