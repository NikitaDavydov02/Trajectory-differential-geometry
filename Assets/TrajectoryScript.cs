using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryScript : MonoBehaviour
{
    // Start is called before the first frame update
    private float t;
    public float Scale;
    private bool vectorsDrawn = false;
    void Start()
    {
        t = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float x = 3 * t - t * t * t;
        float y = 3 * t * t;
        float z = 3 * t + t * t * t;
        transform.position = new Vector3(x, y, z);
        GameObject g = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Sphere));
        g.transform.localScale = new Vector3(Scale, Scale, Scale);
        g.transform.position = transform.position;
        t += Time.deltaTime;

        //Velocity
        float vx = (1 - t * t) / (1 + t * t);
        float vy = 2*t / (1 + t * t);
        float vz = (1 + t * t) / (1 + t * t);
        Vector3 v = new Vector3(vx, vy, vz);
        v *= 1/Mathf.Sqrt(2);
        Debug.DrawRay(transform.position, v, Color.blue, 10f);

        //Acceleration
        float ax = -2*t;
        float ay = 1-t*t;
        float az = 0;
        Vector3 a = new Vector3(ax, ay, az);
        a *= 1 / (1 + t * t);
        Debug.DrawRay(transform.position, a, Color.red, 10f);

        //Binormal
        float bx = t*t*t*t-1;
        float by = -2*t*(1+t*t);
        float bz = (1+t*t)* (1 + t * t);
        Vector3 b = new Vector3(bx, by, bz);
        b *= 1 / ((1 + t * t) * (1 + t * t) * Mathf.Sqrt(2));
        Debug.DrawRay(transform.position, b, Color.green, 10f);

        Debug.Log(v.magnitude + " " + a.magnitude + " " + b.magnitude + " " + Vector3.Dot(a, v) + " " + Vector3.Dot(a, b) + " " + Vector3.Dot(b, v));

    }
}
