using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperiorArrays;


[ExecuteAlways]
public class CustomObject : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] vertices;

    private Mesh selfMesh;
    private void Awake()
    {
        selfMesh = new Mesh();
        selfMesh.name = "mesh";
        GetComponent<MeshFilter>().mesh = selfMesh;
        GetComponent<MeshCollider>().sharedMesh = selfMesh;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        
        //selfMesh.vertices = GetVerticies(true);
        Vector3[] vertsA = GetVerticies(true);
        Vector3[] vertsB = GetVerticies(false);
        
        int[] pointList = new int[0];
        int[] pointListA = new int[0];

        for (int i = 0; i < vertices.Length - 1; i++)
        {

            pointListA = Sray.AppendArray(pointListA, i + 1);
            pointListA = Sray.AppendArray(pointListA, i);
            pointListA = Sray.AppendArray(pointListA, 0);



        }


        pointListA = Sray.AppendArray(pointListA, 0);
        pointListA = Sray.AppendArray(pointListA, vertices.Length - 1);
        pointListA = Sray.AppendArray(pointListA, 1);


        //for (int i = pointListA.Length-2; i > 0; i--)
        //{
        //    pointListA = Sray.AppendArray(pointListA, i + 1);
        //    pointListA = Sray.AppendArray(pointListA, i);
        //    pointListA = Sray.AppendArray(pointListA, 0);
        //}


        //////////////////////////////////////////////////////////////////////////////////
        //Outer Faces

        int[] pointListB = new int[0];

        for (int i = 2; i < vertsB.Length; i++)
        {
            if (i % 2 == 1) //Full Squares
            {
                pointListB = Sray.AppendArray(pointListB, vertsA.Length + i - 2);
                pointListB = Sray.AppendArray(pointListB, vertsA.Length + i - 1);
                pointListB = Sray.AppendArray(pointListB, vertsA.Length + i);
            }
            else
            {
                pointListB = Sray.AppendArray(pointListB, vertsA.Length + i);
                pointListB = Sray.AppendArray(pointListB, vertsA.Length + i - 1);
                pointListB = Sray.AppendArray(pointListB, vertsA.Length + i - 2);
            }
        }
        pointListB = Sray.AppendArray(pointListB, vertsA.Length + 0);
        pointListB = Sray.AppendArray(pointListB, vertsA.Length + vertsB.Length - 1);
        pointListB = Sray.AppendArray(pointListB, vertsA.Length + vertsB.Length - 2);

        pointListB = Sray.AppendArray(pointListB, vertsA.Length + vertsB.Length - 2);
        pointListB = Sray.AppendArray(pointListB, vertsA.Length + vertsB.Length - 1);
        pointListB = Sray.AppendArray(pointListB, vertsA.Length + 0);


        /////////////////////////////////////////////////////////////////////////////////////


        for (int i = 0; i < pointListA.Length; i++)
        {
            pointList = Sray.AppendArray(pointList, pointListA[i]);
        }
        for (int i = 0; i < pointListB.Length; i++)
        {
            pointList = Sray.AppendArray(pointList, pointListB[i]);
        }
        Vector3[] verts = new Vector3[0];
        for (int i = 0; i < vertsA.Length; i++)
        {
            verts = Sray.AppendArray(verts, vertsA[i]);
        }
        for (int i = 0; i < vertsB.Length; i++)
        {
            verts = Sray.AppendArray(verts,vertsB[i]);
        }

        //pointList
        selfMesh.vertices = verts;
        selfMesh.triangles = pointList;

    }


    public Vector3[] GetVerticies(bool box)
    {
        Vector3[] vects = new Vector3[0];
        if (box)
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                vects = Sray.AppendArray(vects, vertices[i].transform.localPosition);
            }
        }
        else
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                vects = Sray.AppendArray(vects, new Vector3(vertices[i].transform.localPosition.x, vertices[i].transform.localPosition.y, vertices[i].transform.localPosition.z + 1));
                vects = Sray.AppendArray(vects, new Vector3(vertices[i].transform.localPosition.x, vertices[i].transform.localPosition.y, vertices[i].transform.localPosition.z - 1));
            }
        }
        return vects;
    }

}
