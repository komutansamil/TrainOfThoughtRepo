using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Manager : MonoBehaviour
{
    public GameObject[] managertargets;
    [SerializeField] Camera kamera;
    [SerializeField] TextMeshProUGUI time_txt;
    [SerializeField] TextMeshProUGUI correctTrainCount_txt;
    float time_Second = 1f;
    int time_Minute = 2;
    int train_Count;
    bool isGameFinished = false;
    bool isActionCompleted = false;
    public bool isFree = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);

        if (hit.collider != null)
        {
            if (hit.collider.tag == "Göbek")
            {
                if (Input.GetMouseButtonDown(0))
                {
                   
                        if (!isActionCompleted)
                        {
                            if (hit.collider.gameObject.GetComponent<CrossRoads>().isPass == false)
                            {
                                Debug.Log("Oldu!");
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

        correctTrainCount_txt.text = train_Count.ToString();

        if (time_Minute != 0 && time_Second <= 0)
        {
            time_Minute--;
            time_Second = 60f;
        }
        if (time_Second <= 1 && time_Second <= 1)
        {
            isGameFinished = true;
        }

        time_Second -= Time.deltaTime;
        int time_Second_Index = (int)time_Second;
        time_txt.text = time_Minute.ToString("00") + ":" + time_Second_Index.ToString("00");
    }
}
