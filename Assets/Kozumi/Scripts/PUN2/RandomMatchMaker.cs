using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; // ??????
using Photon.Realtime;
using System.Runtime.CompilerServices; // ??????

public class RandomMatchMaker : MonoBehaviourPunCallbacks
{

    // ?C???X?y?N?^?[????????
    public GameObject PhotonObject;
    public GameObject[] fruits;
    public static GameObject currentFruits;

    [SerializeField] PlayerFollowCameraPun2[] PlayerFollowCamraScripts;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4; // ????4?l???????????\
        PhotonNetwork.CreateRoom(null, roomOptions); //?????????????[????
    }

    public override void OnJoinedRoom()
    {
        GameObject player = PhotonNetwork.Instantiate(
            PhotonObject.name,
            new Vector3(0f, 150f, 0f),    //?|?W?V????
            Quaternion.identity,    //???]
            0
        );
        currentFruits = player;
        
        Cursor.visible = false;
        GameObject mainCamera = GameObject.FindWithTag("MainCamera");

        player.GetComponent<PlayerPun2>().refCamera = mainCamera.GetComponent<PlayerFollowCameraPun2>();
        player.GetComponent<PlayerPun2>().enabled = true;
        mainCamera.GetComponent<PlayerFollowCameraPun2>().player = player;
        mainCamera.GetComponent<PlayerFollowCameraPun2>().enabled = true;
        
    }
        
       
        
       
   
        
    }

