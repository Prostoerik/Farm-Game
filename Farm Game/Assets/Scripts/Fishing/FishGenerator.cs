using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishGenerator : MonoBehaviour
{
    public GameObject[] fishPrefab;

    void Start()
    {
        StartCoroutine(createFish());
    }

    private IEnumerator createFish()
    {
        yield return new WaitForSeconds(Random.Range(1f, 4f));

        int fishIndex = Random.Range(0, fishPrefab.Length);
        GameObject fish = Instantiate(fishPrefab[fishIndex]);

        bool line = Random.Range(0, 2) == 1;
        fish.GetComponent<Fish>().MoveLine = line;
        bool rightFish = Random.Range(0, 2) == 1;

        float y;
        float x;

        if (line)
        {
            y = Random.Range(-4.59f, -0.3f);
        }
        else
        {
            y = Random.Range(-3.59f, -1.3f);
        }

        if (rightFish)
        {
            x = 11;
            fish.GetComponent<Fish>().movement.x = -0.4f;
            fish.GetComponent<Transform>().Rotate(180f, 0, -90f);
        }
        else
        {
            x = -11;
            fish.GetComponent<Fish>().movement.x = 0.4f;
        }


        fish.GetComponent<Transform>().position = new Vector3(x, y, 1);

        StartCoroutine(createFish());
    }
}
