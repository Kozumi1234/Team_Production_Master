using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerPun2 : MonoBehaviour
{
    [SerializeField] public static int FRUITS;//??????????
    [SerializeField] public PlayerFollowCameraPun2[] PlayerFollowCameraScripts;
    int scripts;
    int cameraNum;

    // Start is called before the first frame update
    void Start()
    {
        scripts = PlayerFollowCameraScripts.Length;
        cameraNum = ChangeFruitScriptPun2.fruitsNum;

        //foreach (MonoBehaviour scr in PlayerFollowCameraScripts)
        //{
            //scr.enabled = false;
        //}
        //?????????????????A?N?e?B?u??????????
       // PlayerFollowCameraScripts[cameraNum].enabled = true;
    }

    public void EvolutionChangeCamera()
    {
        cameraNum = ChangeFruitScriptPun2.fruitsNum;
        PlayerFollowCameraScripts[(cameraNum - ChangeFruitScriptPun2.difference)].enabled = false;
        PlayerFollowCameraScripts[cameraNum].enabled = true;
    }

    public void DegenerationChangeCamera()
    {
        cameraNum = ChangeFruitScriptPun2.fruitsNum;
        PlayerFollowCameraScripts[(cameraNum + ChangeFruitScriptPun2.difference)].enabled = false;
        PlayerFollowCameraScripts[cameraNum].enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
