using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishGenerator : MonoBehaviour
{
    public GameObject fishPrefab;

    void Start()
    {
        StartCoroutine(createFish());
    }

    void Update()
    {

    }

    private IEnumerator createFish()
    {
        yield return new WaitForSeconds(Random.Range(1f, 4f));

        GameObject fish = Instantiate(fishPrefab);

        bool rightFish = Random.Range(0, 2) == 1;

        float y = Random.Range(-4.59f, -0.3f);
        float x;

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
