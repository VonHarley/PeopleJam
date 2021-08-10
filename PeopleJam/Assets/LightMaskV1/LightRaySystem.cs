using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperiorArrays;

public class LightRaySystem : MonoBehaviour
{ 
 // Start is called before the first frame update



private Vector3[] vertices;



public GameObject[] hitObjects;

private Mesh meshMask;

public LayerMask collisionObjects;

public float offHit = 1;


void Start()
{
    meshMask = new Mesh();
    meshMask.name = "meshMask";
    vertices = new Vector3[0];

}

// Update is called once per frame
void Update()
{





}

    public void CycleLights(GameObject source, Mesh sourceMesh)
    {
        ClearArrays();
        GetBounds(source);
        GrabVerts(source);
        CreateMesh(source,sourceMesh);

    }



/// <summary>
/// Gets the four corner angles of the enviornment
/// </summary>
public void GetBounds(GameObject source)
{
        RaycastHit hit;
        Camera cam = GetComponent<Camera>();
        var bottomLeft = (Vector2)cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        var topLeft = (Vector2)cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight, cam.nearClipPlane));
        var topRight = (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, cam.nearClipPlane));
        var bottomRight = (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0, cam.nearClipPlane));

        Physics.Raycast(source.transform.position, GetAngleDirection(source.transform.position, bottomLeft, 0), out hit);
        vertices = Sray.AppendArray(vertices, hit.point);
        Debug.DrawLine(source.transform.position, hit.point);

        Physics.Raycast(source.transform.position, GetAngleDirection(source.transform.position, bottomRight, 0), out hit);
        vertices = Sray.AppendArray(vertices, hit.point);
        Debug.DrawLine(source.transform.position, hit.point);

        Physics.Raycast(source.transform.position, GetAngleDirection(source.transform.position, topRight, 0), out hit);
        vertices = Sray.AppendArray(vertices, hit.point);
        Debug.DrawLine(source.transform.position, hit.point);

        Physics.Raycast(source.transform.position, GetAngleDirection(source.transform.position, topLeft, 0), out hit);
        vertices = Sray.AppendArray(vertices, hit.point);
        Debug.DrawLine(source.transform.position, hit.point);
}

/// <summary>
/// Resets The Arrays to Their Default
/// </summary>
public void ClearArrays()
{
    hitObjects = new GameObject[0];
    vertices = new Vector3[0];
}



/// <summary>
/// Shoots out arrays to Grab All Custom Objects that are hit
/// </summary>
public void GrabVerts(GameObject source)
{
    for (int i = 0; i < 37; i++)
    {
            
        RaycastHit hit;
        Physics.Raycast(source.transform.position, Quaternion.AngleAxis(i * 10, Vector3.forward) * Vector3.right, out hit);
            if (hit.transform != null)
                if (!Sray.CheckInArray(hitObjects, hit.transform.gameObject))
                {
                    //HACK To lower layer requirment, should probably custom scripts this to the custom objects
                    //TASK need to seperate this into its own function to call again when secondary ray hits another object
                    if (hit.transform.gameObject.GetComponent<CustomObject>())
                    {
                        ObjectVerts(hit.transform,source);
                        hitObjects = Sray.AppendArray(hitObjects, hit.transform.gameObject);
                    }
        }
    }
}

public void ObjectVerts(Transform obj,GameObject source)
{
    for (int i = 0; i < obj.gameObject.GetComponent<ObsticleHit>().points.Length; i++)
    {
        RaycastHit hit;

        Ray rayDir;
        Vector3 hitPoint = (obj.gameObject.GetComponent<ObsticleHit>().points[i].transform.position);
        Physics.Raycast(source.transform.position, hitPoint, out hit);



        Physics.Raycast(source.transform.position, GetAngleDirection(source.transform.localPosition, hitPoint, offHit), out hit);
        vertices = Sray.AppendArray(vertices, hit.point);
        Debug.DrawLine(source.transform.position, hit.point, Color.green);



        Physics.Raycast(source.transform.position, GetAngleDirection(source.transform.localPosition, hitPoint, 0), out hit);
        if (hit.distance > GetDistance(source.transform.position, obj.gameObject.GetComponent<ObsticleHit>().points[i].transform.position))
        {
            vertices = Sray.AppendArray(vertices, obj.gameObject.GetComponent<ObsticleHit>().points[i].transform.position);
            Debug.DrawLine(source.transform.position, obj.gameObject.GetComponent<ObsticleHit>().points[i].transform.position, Color.cyan);
            Debug.DrawLine(source.transform.position, hit.point, Color.red);
        }
        else
        {
            vertices = Sray.AppendArray(vertices, hit.point);
            Debug.DrawLine(source.transform.position, obj.gameObject.GetComponent<ObsticleHit>().points[i].transform.position, Color.cyan);
            Debug.DrawLine(source.transform.position, hit.point, Color.red);
        }



        Physics.Raycast(source.transform.position, GetAngleDirection(source.transform.localPosition, hitPoint, -offHit), out hit);
        vertices = Sray.AppendArray(vertices, hit.point);
        Debug.DrawLine(source.transform.position, hit.point, Color.blue);
    }
}

public float GetDistance(Vector3 start, Vector3 end)
{
    float distance;

    distance = Mathf.Pow(end.x - start.x, 2) + Mathf.Pow(end.y - start.y, 2);

    distance = Mathf.Sqrt(distance);
    return distance;

}




public float GetAngle(Vector3 origin, Vector3 newPoint)
{
    //https://www.triangle-calculator.com/?what=vc&a=-3&a1=-4&3dd=3D&a2=0&b=0&b1=0&b2=0&c=0&c1=1&c2=0&submit=Solve&3d=0
    Vector2 A = newPoint;
    Vector2 B = origin;
    Vector2 C = new Vector2(origin.x, origin.y + 1);

    float a = Mathf.Sqrt(Mathf.Pow(B.x - C.x, 2) + Mathf.Pow(B.y - C.y, 2));
    float b = Mathf.Sqrt(Mathf.Pow(A.x - C.x, 2) + Mathf.Pow(A.y - C.y, 2));
    float c = Mathf.Sqrt(Mathf.Pow(B.x - A.x, 2) + Mathf.Pow(B.y - A.y, 2));


    //Angle of B
    float beta = (a * a) + (c * c);
    beta = beta - (b * b);


    beta = beta / (2 * a * c);

    beta = Mathf.Acos(beta);


    beta = Mathf.Rad2Deg * beta;

    if (B.x > A.x)
    {

        beta = 360 - beta;
    }
    return beta;
}

public Vector3 GetAngleDirection(Vector3 origin, Vector3 newPoint, float offHit)
{

    Vector3 dist = (new Vector3((newPoint.x - origin.x), (newPoint.y - origin.y), 0).normalized);

    dist = Quaternion.Euler(0, 0, offHit) * dist;

    return dist;
}





public void CreateMesh(GameObject source,Mesh sourceMesh)
{
    vertices = Sray.CleanArray(vertices);

    Vector3[] positiveVerts = new Vector3[0];
    Vector3[] negativeVerts = new Vector3[0];



    for (int i = 0; i < vertices.Length; i++)
    {

        positiveVerts = Sray.AppendArray(positiveVerts, vertices[i]);


    }

    int origVal = vertices.Length;
    vertices = new Vector3[1];

    vertices[0] = source.transform.position;




    positiveVerts = Sray.CleanArray(positiveVerts);
    negativeVerts = Sray.CleanArray(negativeVerts);
    //1
    for (int i = 0; i < origVal; i++)
    {
        if (positiveVerts.Length != 0)
        {
            float angle = GetAngle(source.transform.position, positiveVerts[0]);
            int curPos = 0;
            for (int f = 0; f < positiveVerts.Length; f++)
            {


                if (angle < GetAngle(source.transform.position, positiveVerts[f]))
                {
                    curPos = f;
                    angle = GetAngle(source.transform.position, positiveVerts[f]);

                }

            }
            vertices = Sray.AppendArray(vertices, positiveVerts[curPos]);
            positiveVerts = Sray.RemoveInArray(positiveVerts, positiveVerts[curPos]);
            positiveVerts = Sray.CleanArray(positiveVerts);
        }
    }






    vertices = Sray.CleanArray(vertices);






    sourceMesh.vertices = vertices;
    int[] pointList = new int[0];

    for (int i = 0; i < vertices.Length - 1; i++)
    {

        pointList = Sray.AppendArray(pointList, 0);
        pointList = Sray.AppendArray(pointList, i);
        pointList = Sray.AppendArray(pointList, i + 1);

    }


    pointList = Sray.AppendArray(pointList, 0);
    pointList = Sray.AppendArray(pointList, vertices.Length - 1);
    pointList = Sray.AppendArray(pointList, 1);

        for (int i = vertices.Length - 2; i > 0; i--)
        {

            pointList = Sray.AppendArray(pointList, i + 1);
            pointList = Sray.AppendArray(pointList, i);
            pointList = Sray.AppendArray(pointList, 0);

        }
        pointList = Sray.AppendArray(pointList, 1);
        pointList = Sray.AppendArray(pointList, vertices.Length - 1);
        pointList = Sray.AppendArray(pointList, 0);
        sourceMesh.triangles = pointList;

}






private void OnDrawGizmos()
{


    Gizmos.color = Color.red;

    for (int i = 0; i < vertices.Length; i++)
    {
        Gizmos.DrawSphere(vertices[i], .1f);
    }





}



}
