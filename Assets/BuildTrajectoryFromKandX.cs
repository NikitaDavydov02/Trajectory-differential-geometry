using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTrajectoryFromKandX : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 n;
    public Vector3 v;
    public Vector3 b;
    private float time;
    public float Scale = 0.1f;
    private Vector3 lastdNdl;
    void Start()
    {
        v = new Vector3(1, 0, 0);
        n = new Vector3(0, 1, 0);
        b = new Vector3(0, 0, 1);
        time = 0;
        lastdNdl = -Curvature(0) * v - Torsion(0) * b;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += v * Time.deltaTime;
        v += Curvature(time) * n * Time.deltaTime;
        n -= (Curvature(time) * v + Torsion(time) * b) * Time.deltaTime;
        b += Torsion(time) * n * Time.deltaTime;
        time += Time.deltaTime;
        Vector3 SecondDerivative = (-(Curvature(time) * v + Torsion(time) * b) - lastdNdl) / Time.deltaTime;
        lastdNdl = -((Curvature(time) * v + Torsion(time) * b));

        Debug.Log(SecondDerivative + (Curvature(time) * Curvature(time) + Torsion(time) * Torsion(time)) * n);
        GameObject g = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Sphere));
        g.transform.localScale = new Vector3(Scale, Scale, Scale);
        g.transform.position = transform.position;
        if (time % 1 < 0.05)
        {
            Debug.DrawRay(transform.position, v, Color.blue, 10f);
            Debug.DrawRay(transform.position, n, Color.red, 10f);
            Debug.DrawRay(transform.position, b, Color.green, 10f);
            Debug.DrawRay(transform.position, SecondDerivative, Color.yellow, 10f);
        }
    }
    private float Curvature(float l)
    {
        return 0.5f;
    }
    private float Torsion(float l)
    {
        return 0.5f;
    }
}
