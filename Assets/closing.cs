using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closing : MonoBehaviour
{

    public Vector3 v = new Vector3(-4f,4f,2f);
    public float a_max=10f;
    public GameObject target;
    public GameObject trail_prefab;
    public GameObject explorer_prefab;
    public float rad_ratio;

    private Vector3 xt;
    private Vector3 vt;
    private newton_trail trail;
    private bool tracking;

    // Start is called before the first frame update
    void Start()
    {

        GameObject trail_obj = Instantiate(trail_prefab, new Vector3(0, 0, 0), Quaternion.identity);
        trail_obj.transform.parent = transform;
        trail = trail_obj.GetComponent<newton_trail>();
        tracking = true;
        a_max = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (tracking)
        {
            xt = target.transform.position - transform.position;
            vt = target.GetComponent<Motive>().v - v;
            Vector3 v_radial = Vector3.Project(vt, xt.normalized) -2f*xt.normalized;
            Vector3 v_tangent = vt - v_radial;

            // had rad_ratio means radial burn off
            // low mean closing
            rad_ratio = v_radial.magnitude / (v_tangent.magnitude + v_radial.magnitude);
            Vector3 a_radial = rad_ratio * a_max * xt.normalized;
            Vector3 a_tangent = (1 - rad_ratio) * a_max * v_tangent.normalized;
            Vector3 a = a_radial + a_tangent;
            transform.rotation = Quaternion.LookRotation(a, transform.rotation * Vector3.up);
            v += a * Time.deltaTime;
            if (xt.magnitude < 2)
            {
                tracking = false;
                trail.End(v - 2f * a, transform.position);
                GameObject exploder = Instantiate(explorer_prefab, new Vector3(0, 0, 0), Quaternion.identity);
                exploder.transform.position = transform.position;
                gameObject.GetComponent<Renderer>().enabled = false;
            } else
            {
                trail.Push(v - 2f * a, transform.position);
            }
        }
        transform.position += v * Time.deltaTime;
    }
}
