using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeFruitsScript : MonoBehaviour
{
    public static int fruitsNum;

    public static int difference;//�ω�����K����

    [SerializeField] private int o_max;

    [SerializeField] private int evolutionGrapeCount;//�u�h�E�ɐi�����邽�߂ɕK�v�ȃT�N�����{�̌�
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
    bool is_Evolution = false;//�i�������Ƃ�true
    bool is_Degeneration = false;//�މ������Ƃ�true

    public static GameObject[] childObject;

    void Start()
    {
        fruitsNum = 0;
        difference = 0;
        o_max = this.transform.childCount;//�q�I�u�W�F�N�g�̌��擾
        childObject = new GameObject[o_max];//�C���X�^���X�쐬

        for (int i = 0; i < o_max; i++)
        {
            childObject[i] = transform.GetChild(i).gameObject;//���ׂĂ̎q�I�u�W�F�N�g�擾
        }
        //���ׂĂ̎q�I�u�W�F�N�g���A�N�e�B�u
        foreach (GameObject gamObj in childObject)
        {
            gamObj.SetActive(false);
        }
        //�ŏ��͂ЂƂ����A�N�e�B�u�����Ă���
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
            //���݂̃A�N�e�B�u�Ȏq�I�u�W�F�N�g�̍��W���擾
            Vector3 pos = childObject[(fruitsNum - difference)].transform.localPosition;
            //childObject[fruitsNum].transform.localScale = new Vector3(x, y, z);

            //���݂̃A�N�e�B�u�Ȏq�I�u�W�F�N�g���A�N�e�B�u
            childObject[(fruitsNum - difference)].SetActive(false);

            //fruitsNum++;
            childObject[fruitsNum].GetComponent<Player>().ChangePlayerState();

            GameObject MainCamera = GameObject.Find("MainCamera");
            MainCamera.GetComponent<CameraController>().EvolutionChangeCamera();

            //string cameraObjectName = (Player.playerState + "Camera");
            //GameObject cameraObject = GameObject.Find(cameraObjectName);
            //cameraObject.GetComponent<changeCameraScript>().CameraChange();

            //�q�I�u�W�F�N�g�����ׂĐ؂�ւ�����܂��ŏ��̃I�u�W�F�N�g�ɖ߂�
            //if (fruitsNum == o_max) { fruitsNum = 0; }

            //���̃I�u�W�F�N�g�̃R���C�_�[�T�C�Y���擾���A�傫��������y���W����Ɉړ����Ă���A�N�e�B�u������
            //var col = childObject[fruitsNum].GetComponent<Player>().sphCol_2;
            //pos.y += col.radius;

            //���̃I�u�W�F�N�g���A�N�e�B�u��
            childObject[fruitsNum].transform.position = pos + new Vector3(0,8,0);
            childObject[fruitsNum].GetComponent<Rigidbody>().velocity = Vector3.zero;
            childObject[fruitsNum].SetActive(true);

            is_Evolution = false;
        }
        else if (is_Degeneration)
        {
            //���݂̃A�N�e�B�u�Ȏq�I�u�W�F�N�g�̍��W���擾
            Vector3 pos = childObject[(fruitsNum + difference)].transform.localPosition;
            //childObject[fruitsNum].transform.localScale = new Vector3(x, y, z);

            //���݂̃A�N�e�B�u�Ȏq�I�u�W�F�N�g���A�N�e�B�u
            childObject[(fruitsNum + difference)].SetActive(false);

            //fruitsNum++;
            childObject[fruitsNum].GetComponent<Player>().ChangePlayerState();

            GameObject MainCamera = GameObject.Find("MainCamera");
            MainCamera.GetComponent<CameraController>().DegenerationChangeCamera();

            //string cameraObjectName = (Player.playerState + "Camera");
            //GameObject cameraObject = GameObject.Find(cameraObjectName);
            //cameraObject.GetComponent<changeCameraScript>().CameraChange();

            //�q�I�u�W�F�N�g�����ׂĐ؂�ւ�����܂��ŏ��̃I�u�W�F�N�g�ɖ߂�
            //if (fruitsNum == o_max) { fruitsNum = 0; }

            //���̃I�u�W�F�N�g�̃R���C�_�[�T�C�Y���擾���A�傫��������y���W����Ɉړ����Ă���A�N�e�B�u������
            //var col = childObject[fruitsNum].GetComponent<Player>().sphCol_2;
            //pos.y += col.radius;

            //���̃I�u�W�F�N�g���A�N�e�B�u��
            childObject[fruitsNum].transform.position = pos + new Vector3(0, 8, 0);
            childObject[fruitsNum].GetComponent<Rigidbody>().velocity = Vector3.zero;
            childObject[fruitsNum].SetActive(true);

            is_Degeneration = false;
        }
    }
}
