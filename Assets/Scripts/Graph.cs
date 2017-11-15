using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class shows functions like y = f(x) using prefabs
 * Also animates in time the function 
 */
public class Graph : MonoBehaviour {

    //Prefab used for each point in the Graph
    public Transform PointPrefab;
    [Range(10,100)]
    public int PointNumber = 10;
    public bool bShowSin = true;
    public bool bShowCos = false;

    //Initial formulation
    // f(x) = Multiplier * x^Pow + Additive
    public float Multiplier = 1.0f;
    public int Pow = 1;
    public float Additive = 0.0f;

    private Transform[] Points;

    void InitPoints()
    {
        Points = new Transform[PointNumber];
        float Step = (2.0f / PointNumber);
        Vector3 Scale = Vector3.one * Step;
        Vector3 Position = Vector3.zero;
        for (int i = 0; i < PointNumber; i++)
        {
            Transform Point = Instantiate(PointPrefab);
            Position.x = (i + 0.5f) * Step - 1f;
            Point.localPosition = Position;
            Point.localScale = Scale;
            Point.SetParent(transform, false);
            Points[i] = Point;
        }
    }

    void Awake()
    {
        InitPoints();
    }
    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < Points.Length; i++)
        {
            Transform Point = Points[i];
            Vector3 Position = Point.localPosition;
            if (bShowSin)
            {
                Position.y = Mathf.Sin(Mathf.PI * (Position.x + Time.time));
            }
            else if (bShowCos)
            {
                Position.y = Mathf.Cos(Mathf.PI * (Position.x + Time.time));
            }
            else
            {
                Position.y = Function(Position.x);
            }
            Point.localPosition = Position;
        }
    }

    float Function(float x)
    {
        //Optimistic way to optimize pow calculation
        float PowValue = 1.0f;
        for(int i = 0; i < Pow; i++)
        {
            PowValue *= x;
        }
        //f(x) = Multiplier * x ^ Pow + Additive
        return Multiplier * PowValue + Additive;
    }
}
