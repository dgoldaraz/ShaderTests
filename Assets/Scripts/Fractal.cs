using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class creates a fractal figure based on the MaxDepth defines
 * Duplicate and scale different level children recursively.
 */
public class Fractal : MonoBehaviour {

    public Mesh[] Meshes;
    public Material Material;
    public int MaxDepth = 4;
    public float ChildScale = 0.5f;
    public float MaxTimeSeconds = 0.5f;
    public float ChanceToAppear = 0.7f;
    public bool ChildRotate = false;
    public Vector3 AnimationRotationEuler;
    public string ColorId = "_Color";
    public Color BaseColor = Color.red;
    public Color FinalColor = Color.yellow;

    protected int Depth = 0;
    private static Vector3[] Directions = 
    {
        Vector3.up,
        Vector3.right,
        Vector3.left,
        Vector3.forward,
        Vector3.back
    };
    private static Quaternion[] Rotations =
    {
        Quaternion.identity,
        Quaternion.Euler(0.0f, 0.0f, -90.0f),
        Quaternion.Euler(0.0f, 0.0f, 90.0f),
        Quaternion.Euler(90.0f, 0.0f, 0.0f),
        Quaternion.Euler(-90.0f, 0.0f, 0.0f)
    };

	// Use this for initialization
	void Start ()
    {
        gameObject.AddComponent<MeshFilter>().mesh = Meshes[Random.Range(0, Meshes.Length)];
        MeshRenderer CurrentMeshR = gameObject.AddComponent<MeshRenderer>();
        CurrentMeshR.material = Material;
        CurrentMeshR.material.SetColor(ColorId, Color.Lerp(BaseColor, FinalColor, (float)Depth / (float)MaxDepth));
        //Create childs and apply mesh/material
        if (Depth < MaxDepth)
        {
            StartCoroutine(CreateChild());
        }

        if(Depth == 0)
        {
            //Add down child only to the root
            new GameObject("Fractal Child").AddComponent<Fractal>().Initialize(this, Vector3.down, Quaternion.identity);
        }
	}
	
	// Update is called once per frame
	private void Update ()
    {
	    if(ChildRotate || Depth == 0)
        {
            transform.Rotate(AnimationRotationEuler.x * Time.deltaTime, AnimationRotationEuler.y * Time.deltaTime, AnimationRotationEuler.z * Time.deltaTime);
        }
	}

    void Initialize(Fractal Parent, Vector3 Direction, Quaternion Rotation)
    {
        Meshes = Parent.Meshes;
        Material = Parent.Material;
        MaxDepth = Parent.MaxDepth;
        Depth = Parent.Depth + 1;
        ChanceToAppear = Parent.ChanceToAppear;
        ChildScale = Parent.ChildScale;
        ChildRotate = Parent.ChildRotate;
        AnimationRotationEuler = Parent.AnimationRotationEuler;
        BaseColor = Parent.BaseColor;
        FinalColor = Parent.FinalColor;
        ColorId = Parent.ColorId;
        //Transform changes
        transform.parent = Parent.transform;
        transform.localScale = Vector3.one * ChildScale;
        transform.localPosition = Direction * (0.5f + 0.5f * ChildScale);
        transform.localRotation = Rotation;
    }

    private IEnumerator CreateChild()
    {
        for(int i = 0; i < Directions.Length; i++)
        {
            if(Random.value < ChanceToAppear)
            {
                yield return new WaitForSeconds(Random.Range(0.1f, MaxTimeSeconds));
                new GameObject("Fractal Child").AddComponent<Fractal>().Initialize(this, Directions[i], Rotations[i]);
            }
        }
    }

}
