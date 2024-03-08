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
        cameras = this.transform.childCount;//�q�I�u�W�F�N�g�̌��擾
        cameraChildObject = new GameObject[cameras];//�C���X�^���X�쐬

        for (int i = 0; i < cameras; i++)
        {
            cameraChildObject[i] = transform.GetChild(i).gameObject;//���ׂĂ̎q�I�u�W�F�N�g�擾
        }
        //���ׂĂ̎q�I�u�W�F�N�g���A�N�e�B�u
        foreach (GameObject camObj in cameraChildObject)
        {
            camObj.SetActive(false);
        }
        //�ŏ��͂ЂƂ����A�N�e�B�u�����Ă���
        cameraChildObject[fruitsNum].SetActive(true);
    }

    public void CameraChange()
    {
        //���݂̃A�N�e�B�u�Ȏq�I�u�W�F�N�g���A�N�e�B�u
        cameraChildObject[fruitsNum].SetActive(false);
        fruitsNum++;
        cameraChildObject[fruitsNum].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
