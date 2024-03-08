using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
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
        PlayerFollowCameraScripts[(cameraNum - 1)].enabled = false;
        PlayerFollowCameraScripts[cameraNum].enabled = true;
    }

    public void DegenerationChangeCamera()
    {
        cameraNum = changeFruitsScript.fruitsNum;
        PlayerFollowCameraScripts[(cameraNum + 1)].enabled = false;
        PlayerFollowCameraScripts[cameraNum].enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}