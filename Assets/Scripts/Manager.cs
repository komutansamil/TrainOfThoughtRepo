using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Manager : MonoBehaviour
{
    public GameObject[] managertargets;
    [SerializeField] Camera kamera;
    [SerializeField] TextMeshProUGUI time_txt;
    [SerializeField] TextMeshProUGUI correctTrainCount_txt;
    [SerializeField] TextMeshProUGUI score_txt;
    [SerializeField] TextMeshProUGUI level_txt;
    [SerializeField] TextMeshProUGUI finishPanelCorrect_txt;
    float time_Second = 0f;
    int time_Minute = 2;
    int train_Count;
    bool isGameFinished = false;
    bool isActionCompleted = true;
    public bool isFree = true;
    public int earnedTrainCount;
    public int allTrainCount;
    [SerializeField] Transform trainInstantiatePos;
    [SerializeField] GameObject[] trains;
    [SerializeField] GameObject finishPanel;
    [SerializeField] GameObject startPanel;
    int score;
    int scoreProductCount = 100;
    [SerializeField] int levelCount;
    [SerializeField] GameObject[] levels;
    [SerializeField] bool[] islevels;
    bool isGameStarted = false;
    public bool isGameOver = false;
    public bool isTouchedCrossRoad = false;
    public GameObject[] stations;
    bool isFirstTime = false;
    float waitingTime = 1f;

    // Start is called before the first frame update
    void Awake()
    {
        levelCount = PlayerPrefs.GetInt("levelCount");
    }

    void Start()
    {
        for (int i = 0; i < islevels.Length; i++)
        {
            levels[i].SetActive(false);
        }
        levels[levelCount].SetActive(true);
        isFree = true;
        startPanel.SetActive(true);
        CallAgain();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            Physics.Raycast(ray, out hit);

            if (hit.collider != null)
            {
                if (hit.collider.tag == "Göbek")
                {
                    if(touch.phase == TouchPhase.Began)
                    {
                        if (isFree)
                        {

                            if (!isActionCompleted)
                            {
                                if (hit.collider.gameObject.GetComponent<CrossRoads>().isPass == false)
                                {
                                    hit.collider.gameObject.GetComponent<CrossRoads>().isPass = true;
                                }
                            }
                            if (isActionCompleted)
                            {
                                if (hit.collider.gameObject.GetComponent<CrossRoads>().isPass == true)
                                {
                                    hit.collider.gameObject.GetComponent<CrossRoads>().isPass = false;
                                }
                            }

                            isActionCompleted = !isActionCompleted;

                        }
                    }
                }
            }
        }



        correctTrainCount_txt.text = "Correct " + earnedTrainCount.ToString() + " of " + earnedTrainCount.ToString();

        if (isGameStarted && !isGameOver)
        {
            if (time_Minute != 0 && time_Second <= 0)
            {
                time_Minute--;
                time_Second = 60f;
            }
            if (!isGameOver)
            {
                if (time_Second <= 0 && time_Minute <= 0)
                {
                    isGameFinished = true;
                }
            }

            time_Second -= Time.deltaTime;
            int time_Second_Index = (int)time_Second;
            time_txt.text = time_Minute.ToString("00") + ":" + time_Second_Index.ToString("00");
        }

        if (isGameFinished)
        {
            finishPanel.SetActive(true);
            score = earnedTrainCount * scoreProductCount;

            finishPanelCorrect_txt.text = "Correct          " + earnedTrainCount.ToString();
            level_txt.text = "Level          " + "x" + levelCount.ToString();
            score_txt.text = "Score          " + score.ToString();

            if(levelCount != 4)
            {
                levelCount++;
            }
         
            PlayerPrefs.SetInt("levelCount", levelCount);
            isGameFinished = false;
            isGameOver = true;
        }
    }

    public void PlayButton()
    {
        startPanel.SetActive(false);
        isGameStarted = true;
    }
    public void ContinueButton()
    {
        finishPanel.SetActive(false);
        SceneManager.LoadScene("GameScene");
    }

    void CallAgain()
    {
        StartCoroutine(Timing());
    }

    IEnumerator Timing()
    {
        yield return new WaitForSeconds(waitingTime);
        if (isGameStarted && !isGameFinished)
        {
            if (levelCount == 0)
            {
                int random = Random.Range(0, 2);
                GameObject train_Instantiated = Instantiate(trains[random],
                    trainInstantiatePos.position, trainInstantiatePos.rotation);
            }
            if (levelCount == 1)
            {
                int random = Random.Range(0, 4);
                GameObject train_Instantiated = Instantiate(trains[random],
                    trainInstantiatePos.position, trainInstantiatePos.rotation);
            }
            if (levelCount == 2)
            {
                int random = Random.Range(0, 6);
                GameObject train_Instantiated = Instantiate(trains[random],
                    trainInstantiatePos.position, trainInstantiatePos.rotation);
            }
            if (levelCount == 3)
            {
                int random = Random.Range(0, 8);
                GameObject train_Instantiated = Instantiate(trains[random],
                    trainInstantiatePos.position, trainInstantiatePos.rotation);
            }
            if (levelCount == 4)
            {
                int random = Random.Range(0, 10);
                GameObject train_Instantiated = Instantiate(trains[random],
                    trainInstantiatePos.position, trainInstantiatePos.rotation);
            }

            if (!isFirstTime)
            {
                waitingTime = 7f;
                isFirstTime = true;
            }
        }
        CallAgain();
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("levelCount", levelCount);
    }
}
