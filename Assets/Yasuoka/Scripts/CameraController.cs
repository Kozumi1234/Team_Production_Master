using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] public static int FRUITS;//�ʕ��̎��
    [SerializeField] public MonoBehaviour[] PlayerFollowCameraScripts;
    int scripts;
    int cameraNum;

    // Start is called before the first frame update
    void Start()
    {
        scripts = PlayerFollowCameraScripts.Length;
        cameraNum = changeFruitsScript.fruitsNum;

        foreach (MonoBehaviour scr in PlayerFollowCameraScripts)
        {
            scr.enabled = false;
        }
        //�ŏ��͂ЂƂ����A�N�e�B�u�����Ă���
        PlayerFollowCameraScripts[cameraNum].enabled = true;
    }

    public void EvolutionChangeCamera()
    {
        cameraNum = changeFruitsScript.fruitsNum;
        //�J�����̊p�x��ێ�����
        //var CameraRotate = transform.rotation;
        PlayerFollowCameraScripts[(cameraNum - changeFruitsScript.difference)].enabled = false;
        //�J�����̊p�x�������n��
           PlayerFollowCameraScripts[cameraNum].enabled = true;
        //PlayerFollowCameraScripts[cameraNum].transform.rotation = CameraRotate;
    }

    public void DegenerationChangeCamera()
    {
        cameraNum = changeFruitsScript.fruitsNum;
        //�J�����̊p�x��ێ�����
        //var CameraRotate = transform.rotation;
        PlayerFollowCameraScripts[(cameraNum + changeFruitsScript.difference)].enabled = false;
        //�J�����̊p�x�������n��
        PlayerFollowCameraScripts[cameraNum].enabled = true;
        //PlayerFollowCameraScripts[cameraNum].transform.rotation = CameraRotate;
    }

    // Update is called once per frame
    void Update()
    {

    }
}