using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] public static int FRUITS;//果物の種類
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
        //最初はひとつだけアクティブ化しておく
        PlayerFollowCameraScripts[cameraNum].enabled = true;
    }

    public void EvolutionChangeCamera()
    {
        cameraNum = changeFruitsScript.fruitsNum;
        //カメラの角度を保持する
        //var CameraRotate = transform.rotation;
        PlayerFollowCameraScripts[(cameraNum - changeFruitsScript.difference)].enabled = false;
        //カメラの角度を引き渡す
           PlayerFollowCameraScripts[cameraNum].enabled = true;
        //PlayerFollowCameraScripts[cameraNum].transform.rotation = CameraRotate;
    }

    public void DegenerationChangeCamera()
    {
        cameraNum = changeFruitsScript.fruitsNum;
        //カメラの角度を保持する
        //var CameraRotate = transform.rotation;
        PlayerFollowCameraScripts[(cameraNum + changeFruitsScript.difference)].enabled = false;
        //カメラの角度を引き渡す
        PlayerFollowCameraScripts[cameraNum].enabled = true;
        //PlayerFollowCameraScripts[cameraNum].transform.rotation = CameraRotate;
    }

    // Update is called once per frame
    void Update()
    {

    }
}