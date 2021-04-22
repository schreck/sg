using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newton_trail : MonoBehaviour
{
    public List<Vector3> X; // positions
    public List<Vector3> V; // velocities
    public List<float> T; // start times
    public float last_vertex;
    public float ttl=10f;
    private LineRenderer trail;
    private bool drifting;

    void Start()
    {
        trail = gameObject.GetComponent<LineRenderer>();
        trail.positionCount = 1;
        X = new List<Vector3> { transform.position };
        V = new List<Vector3> { Vector3.zero };
        T = new List<float> { Time.time };
        last_vertex = Time.time - 0.5f;
        drifting = false;
    }

    void Update()
    {
        if(Time.time - T[trail.positionCount - 1] > ttl)
        {
            DropVerex();
        }
        for(int i=0; i<trail.positionCount; i++)
        {
            X[i] += V[i] * Time.deltaTime;
        }
        if (!drifting)
        {
            X[0] = transform.parent.position;
        }
        trail.SetPositions(X.ToArray());
    }

    void AddVertex(Vector3 v, Vector3 x)
    {
        V.Insert(1,v);
        X.Insert(1,x);
        T.Insert(1, Time.time);
        trail.positionCount++;
    }

    void DropVerex()
    {
        trail.positionCount--;
        int n = trail.positionCount;
        X.RemoveAt(n);
        V.RemoveAt(n);
        T.RemoveAt(n);
        if (drifting && n == 1)
        {
            Destroy(gameObject);
        }
    }

    public void Push(Vector3 v, Vector3 x)
    {
        if (Time.time - last_vertex > 0.5f)
        {
            AddVertex(v, x);
            last_vertex = Time.time;
        }
    }

    public void End(Vector3 v, Vector3 x)
    {
        V[0] = v;
        drifting = true;
    }
}
