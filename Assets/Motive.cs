using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motive : MonoBehaviour
{
    public Vector3 v; // m/s velocity
    public Vector3 a; // m/s^2 acceleration
    public GameObject trail_prefab;

    private newton_trail trail; // trail
    private float start_time;

    // Start is called before the first frame update
    void Start()
    {
        a = new Vector3(.1f, .1f, .1f);
        v = new Vector3(0, 0, 0);
        GameObject trail_obj = Instantiate(trail_prefab, new Vector3(0, 0, 0), Quaternion.identity);
        trail_obj.transform.parent = transform;
        trail = trail_obj.GetComponent<newton_trail>();
        start_time = Time.time;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time - start_time < 5f)
        {
            a += (.02f)*Random.onUnitSphere;
            v += a * Time.deltaTime;
            trail.Push(v - 0.5f * a, transform.position);
        } else
        {
            trail.End(v - 0.5f * a, transform.position);
        }
        transform.position += v * Time.deltaTime;

    }

}
