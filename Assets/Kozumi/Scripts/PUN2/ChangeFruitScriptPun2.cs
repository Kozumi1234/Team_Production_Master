using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFruitScriptPun2 : Photon.Pun.MonoBehaviourPun
{
    public static int fruitsNum;

    public static int difference;


    [SerializeField] private int evolutionGrapeCount;
    [SerializeField] private int evolutionDekoponCount;
    [SerializeField] private int evolutionPersimmonCount;
    [SerializeField] private int evolutionAppleCount;
    [SerializeField] private int evolutionPearCount;
    [SerializeField] private int evolutionPeachCount;
    [SerializeField] private int evolutionPineappleCount;
    [SerializeField] private int evolutionMelonCount;
    [SerializeField] private int evolutionWatermelonCount;
    [SerializeField] private float StrDistance;
    [SerializeField] private float GraDistance;
    [SerializeField] private float OrnDistance;
    [SerializeField] private float PerDistance;
    [SerializeField] private float AppDistance;
    [SerializeField] private float PeaDistance;
    [SerializeField] private float PecDistance;
    [SerializeField] private float PinDistance;
    [SerializeField] private float MelDistance;
    [SerializeField] private float WatDistance;
    [SerializeField] private float changeSizeSpeed = 0.1f;


    int cherryCount;
    int lastFruitNum;
    bool is_Changed = false;


    void Start()
    {
        fruitsNum = 0;
        difference = 0;


    }

    void Update()
    {

        cherryCount = PlayerPun2.cherryCount;

        if (cherryCount < evolutionGrapeCount)
        {
            fruitsNum = 0;
        }
        else if (cherryCount < evolutionDekoponCount)
        {
            fruitsNum = 1;
        }
        else if (cherryCount < evolutionPersimmonCount)
        {
            fruitsNum = 2;
        }
        else if (cherryCount < evolutionAppleCount)
        {
            fruitsNum = 3;
        }
        else if (cherryCount < evolutionPearCount)
        {
            fruitsNum = 4;
        }
        else if (cherryCount < evolutionPeachCount)
        {
            fruitsNum = 5;
        }
        else if (cherryCount < evolutionPineappleCount)
        {
            fruitsNum = 6;
        }
        else if (cherryCount < evolutionMelonCount)
        {
            fruitsNum = 7;
        }
        else if (cherryCount < evolutionWatermelonCount)
        {
            fruitsNum = 8;
        }
        else if (evolutionWatermelonCount <= cherryCount)
        {
            fruitsNum = 9;
        }

        PlayerPun2.fruitState = PlayerPun2.fruitsNameList[fruitsNum];

        if (lastFruitNum != fruitsNum)
        {
            if (lastFruitNum < fruitsNum)
            {

                difference = fruitsNum - lastFruitNum;
            }
            else if (lastFruitNum > fruitsNum)
            {               
                difference = lastFruitNum - fruitsNum;
            }
            is_Changed = true;
        }

        lastFruitNum = fruitsNum;

        if (is_Changed)
        {
            Vector3 lastFruitPos = RandomMatchMaker.currentFruits.transform.position;
            Vector3 lastFruitScale = RandomMatchMaker.currentFruits.transform.localScale;
            Vector3 lastFruitVelocity = RandomMatchMaker.currentFruits.GetComponent<Rigidbody>().velocity;
            PhotonNetwork.Destroy(RandomMatchMaker.currentFruits);
            GameObject newFruits = PhotonNetwork.Instantiate(PlayerPun2.fruitsNameList[fruitsNum],lastFruitPos,Quaternion.identity);
            newFruits.transform.localScale = lastFruitScale;
            newFruits.GetComponent<Rigidbody>().velocity = lastFruitVelocity;
            RandomMatchMaker.currentFruits = newFruits;
            GameObject cam = GameObject.Find("MainCamera");
            newFruits.GetComponent<PlayerPun2>().refCamera = cam.GetComponent<PlayerFollowCameraPun2>();
            cam.GetComponent<PlayerFollowCameraPun2>().player = newFruits;
            is_Changed = false;
            float distance = 0f;
            switch (PlayerPun2.fruitState)
            {
                case "Strawberry":
                    distance = StrDistance;
                    break;
                case "Grape":
                    distance = GraDistance;
                    break;
                case "Orange":
                    distance = OrnDistance;
                    break;
                case "Persimmon":
                    distance = PerDistance;
                    break;
                case "Apple":
                    distance = AppDistance;
                    break;
                case "Pear":
                    distance = PeaDistance;
                    break;
                case "Peach":
                    distance = PecDistance;
                    break;
                case "Pineapple":
                    distance = PinDistance;
                    break;
                case "Melon":
                    distance = MelDistance;
                    break;
                case "Watermelon":
                    distance = WatDistance;
                    break;
            }
            cam.GetComponent<PlayerFollowCameraPun2>().distance = distance;
            //StartCoroutine(SizeChange(newFruits,lastFruitScale));

        }
        

    }

    IEnumerator SizeChange(GameObject newFruits, Vector3 lastFruitsScale)
    {
        Vector3 targetScale = newFruits.transform.localScale;
        newFruits.transform.localScale = lastFruitsScale;
        float time = 0f;
        while(time < 5)
        {
            newFruits.transform.localScale = Vector3.Lerp(newFruits.transform.localScale, targetScale, 0.1f);
            time += Time.deltaTime;
            yield return null;
        }
        
    }
}

    
