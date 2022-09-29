using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    [SerializeField] Manager manager;
    bool isTouchedTheTrain = false;
    [SerializeField] string stationColor;

    // Start is called before the first frame update
    void Start()
    {
        isTouchedTheTrain = false;
        manager = GameObject.Find("Manager").GetComponent<Manager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == stationColor)
        {
            if (!isTouchedTheTrain)
            {
                Debug.Log("Oldu!!!!");
                manager.earnedTrainCount++;
                Destroy(col.gameObject);
                StartCoroutine(Waiting());
                isTouchedTheTrain = true;
            }
        }

        if(col.gameObject.transform.childCount > 0)
        {
            if (col.gameObject.transform.GetChild(0).tag == "Train")
            {
                Destroy(col.gameObject);
            }
        }
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(2f);
        isTouchedTheTrain = false;
    }
}
