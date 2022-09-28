using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossRoads : MonoBehaviour
{
    public GameObject straight_CrossRoad;
    public GameObject turned_CrossRoad;
    public bool isPass = false;
    public int crosRoadNumber;
    public GameObject[] crossRoadTargets_Straight;
    public GameObject[] crossRoadTargets_Turned;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isPass)
        {
            straight_CrossRoad.SetActive(false);
            turned_CrossRoad.SetActive(true);
        }
        else
        {
            turned_CrossRoad.SetActive(false);
            straight_CrossRoad.SetActive(true);
        }
    }
}
