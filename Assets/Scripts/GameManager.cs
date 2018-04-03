using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private Transform startPoint;
    private Transform initPoint;
    private Pin curPin;
    public GameObject pinPrefabs;
    private bool isOver = false;
    private int score = 0;
    public Text scoreText;
    private Camera main;
    public GameObject retry;
    public GameObject exit;

	// Use this for initialization
	void Start () {
        startPoint = GameObject.Find("StartPoint").transform;
        initPoint = GameObject.Find("InitPoint").transform;
        main = Camera.main;
        retry.SetActive(false);
        exit.SetActive(false);
        InitPin();
	}
	
	// Update is called once per frame
	void Update () {
        if (isOver)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            if (curPin.isReach)
            {
                curPin.StartFly();
                InitPin();
                score++;
                scoreText.text = score.ToString();
                if (score % 3 == 0)
                    GameObject.Find("Circle").GetComponent<Rotate>().speed *= 1.2f;
            }
        }
	}

    void InitPin()
    {
        curPin = GameObject.Instantiate(pinPrefabs, initPoint.position, pinPrefabs.transform.rotation).GetComponent<Pin>();
    }

    public void GameOver()
    {
        if(isOver == false)
        {
            GameObject.Find("Circle").GetComponent<Rotate>().enabled = false;
            isOver = true;
            StartCoroutine(GameOverAni());
        }
    }

    IEnumerator GameOverAni()
    {
        while (true)
        {
            main.backgroundColor = Color.Lerp(main.backgroundColor, new Vector4(255/255,160.0f/255,160.0f/255,1), 3 * Time.deltaTime);
            main.orthographicSize = Mathf.Lerp(main.orthographicSize, 4, 3 * Time.deltaTime);
            if (Mathf.Abs(main.orthographicSize - 4) < 0.01f)
                break;
            yield return 0;
        }
        retry.SetActive(true);
        exit.SetActive(true);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
