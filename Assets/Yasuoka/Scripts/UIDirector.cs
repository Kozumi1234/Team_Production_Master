using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDirector : MonoBehaviour
{
    public GameObject cherryCountObject; // Textオブジェクト
    int cherryCount = 0;

    [SerializeField] public float timeRemaining = 10;
    public static bool timerIsRunning = false;
    public Text timeText;

    [SerializeField] public GameObject timeUp;


    // Start is called before the first frame update
    void Start()
    {
        timeUp.SetActive(false);
        cherryCount = Player.cherryCount;
        // Starts the timer automatically
        timerIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        cherryCount = Player.cherryCount;
        //string cherryCountView = cherryCount.ToString("00");
        //cherryCountText.text = cherryCountView;
        Text Count = cherryCountObject.GetComponent<Text>();
        // テキストの表示を入れ替える
        Count.text = "さくらんぼ:" + cherryCount;

        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time Up!!");
                timeUp.SetActive(true);
                timeRemaining = 0;
                timerIsRunning = false;

                GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("cherry");

                foreach (GameObject gameObj in gameObjects)
                {
                    Destroy(gameObj.gameObject);
                }
            }
        }

    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
