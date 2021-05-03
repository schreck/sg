using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missle_field : MonoBehaviour
{

    public GameObject target;
    public GameObject missle_prefab;

    void Start()
    {
        int N = 4;
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                GameObject missle = Instantiate(
                    missle_prefab,
                    new Vector3(Random.Range(-50f,50f), 10*(i-N/2), 10*(j-N/2)),
                    Quaternion.identity);
                missle.name = string.Format("missle({0},{1})", i, j);
                missle.transform.parent = transform;
                closing c = missle.GetComponent<closing>();
                c.target = target;
                c.v = 5f*Random.onUnitSphere;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
