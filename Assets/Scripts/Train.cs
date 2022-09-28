using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    public GameObject[] targets;
    [SerializeField] GameObject target;
    [SerializeField] Manager manager;
    public bool isTouched = false;
    bool isReachedCrossRoad = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 1f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Target")
        {
            if (!isTouched)
            {




                //Buralarýn düzeltilmesi lazým lenght kýsmýnýn ve isFree ise
                if (!isReachedCrossRoad)
                {
                    int index = col.gameObject.GetComponent<Target>().index;
                    int newIndex = index + 1;
                    if (manager.managertargets.Length <= newIndex)
                        target = manager.managertargets[newIndex];
                }
                else
                {
                    int index = col.gameObject.GetComponent<Target>().index;
                    int newIndex = index + 1;
                    if (targets.Length <= newIndex)
                        target = targets[newIndex];
                }
                isTouched = true;
                manager.isFree = false;
            }
        }
        if (col.gameObject.tag == "Göbek")
        {
            manager.isFree = false;
            isReachedCrossRoad = true;
            if (col.gameObject.GetComponent<CrossRoads>().isPass)
            {
                targets = col.gameObject.GetComponent<CrossRoads>().crossRoadTargets_Turned;
            }
            else
            {
                targets = col.gameObject.GetComponent<CrossRoads>().crossRoadTargets_Straight;
            }
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Target")
        {
            isTouched = false;
        }
        if (col.gameObject.tag == "Göbek")
        {
            manager.isFree = true;
        }
    }
}
