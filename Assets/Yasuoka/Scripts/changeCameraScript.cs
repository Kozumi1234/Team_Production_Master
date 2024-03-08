using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeCameraScript : MonoBehaviour
{
    int fruitsNum;
    int cameras;
    public GameObject[] cameraChildObject;

    // Start is called before the first frame update
    void Start()
    {
        fruitsNum = changeFruitsScript.fruitsNum;
        cameras = this.transform.childCount;//子オブジェクトの個数取得
        cameraChildObject = new GameObject[cameras];//インスタンス作成

        for (int i = 0; i < cameras; i++)
        {
            cameraChildObject[i] = transform.GetChild(i).gameObject;//すべての子オブジェクト取得
        }
        //すべての子オブジェクトを非アクティブ
        foreach (GameObject camObj in cameraChildObject)
        {
            camObj.SetActive(false);
        }
        //最初はひとつだけアクティブ化しておく
        cameraChildObject[fruitsNum].SetActive(true);
    }

    public void CameraChange()
    {
        //現在のアクティブな子オブジェクトを非アクティブ
        cameraChildObject[fruitsNum].SetActive(false);
        fruitsNum++;
        cameraChildObject[fruitsNum].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
