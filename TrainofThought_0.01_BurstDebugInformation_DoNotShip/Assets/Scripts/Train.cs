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
    public string colorName;
    // Start is called before the first frame update
    void Start()
    {
        colorName = this.gameObject.name;
        target = GameObject.Find("Target (1)");
        manager = GameObject.Find("Manager").GetComponent<Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!manager.isGameOver)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 1f * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Target")
        {
            if (!isTouched)
            {
                if (!isReachedCrossRoad)
                {
                    int index = col.gameObject.GetComponent<Target>().index;
                    if (index < manager.managertargets.Length - 1)
                    {
                        int newIndex = index + 1;
                        target = manager.managertargets[newIndex];
                    }
                }
                else
                {
                    int index = col.gameObject.GetComponent<Target>().index;
                    if (index < targets.Length - 1)
                    {
                        int newIndex = index + 1;
                        target = targets[newIndex];
                    }
                }
                StartCoroutine(Timing());
                isTouched = true;
            }
        }
        if (col.gameObject.tag == "Göbek")
        {
            isReachedCrossRoad = true;
            if (col.gameObject.GetComponent<CrossRoads>().isPass)
            {
                targets = col.gameObject.GetComponent<CrossRoads>().crossRoadTargets_Turned;
            }
            else
            {
                targets = col.gameObject.GetComponent<CrossRoads>().crossRoadTargets_Straight;
            }

            if(isReachedCrossRoad)
            {
                int index = 0;
                if (index < targets.Length - 1)
                {
                    int newIndex = index + 1;
                    target = targets[newIndex];
                }
            }
        }
    }

    IEnumerator Timing()
    {
        yield return new WaitForSeconds(1f);
        isTouched = false;
    }
}
