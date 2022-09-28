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
        manager = GameObject.Find("Manager").GetComponent<Manager>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == stationColor)
        {
            Debug.Log("Oldu!!!!");
            if (!isTouchedTheTrain)
            {
                Debug.Log("Oldu!!!!");
                manager.earnedTrainCount++;
                Destroy(col.gameObject);
                StartCoroutine(Waiting());
                isTouchedTheTrain = true;
            }
        }

        if (col.gameObject.tag == "Train")
        {
            Destroy(col.gameObject);
        }
    }

    void CallAgain()
    {
        StartCoroutine(Waiting());
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(1f);
        isTouchedTheTrain = false;
        CallAgain();
    }
}
