using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodecahedron : MonoBehaviour {


    public Transform sommet;
    private Vector3[] vertices;
    private Vector3[] positions;
    private GameObject[] sommets;

    private LineRenderer[] lineRenderers;

    private Vector3[] verticesD;
    private int[] triangle;
    private Vector3[] normals;

    private Mesh mesh;

    // Use this for initialization
    void Start () {

        GetComponent<MeshFilter>().mesh = mesh = new Mesh();

        sommets = new GameObject[20];
        positions = new Vector3[20];
        lineRenderers = new LineRenderer[20];

        verticesD = new Vector3[12*5*2];
        normals = new Vector3[12 * 5];
        triangle = new int[3*12*5];

        // Size parameter: This is distance of each vector from origin
        var r = Mathf.Sqrt(3);

        Debug.Log("Generating a dodecahedron with enclosing sphere radius: " + r);

        // Make the vertices
        MakeDodecahedron(r);

        // Print them out
        Debug.Log("       X        Y        Z");
        Debug.Log("   ==========================");
        for (var i = 0; i < vertices.Length; i++)
        {
            var vertex = vertices[i];
            Debug.Log("{0,2}:" + vertex + " " + (i + 1));
        }

        Debug.Log("\nDone!");

        // affichage des sommets
        /*for (int i = 0; i < 20; i++)
        {
            Instantiate(sommet, new Vector3(
                vertices[i].x,
                vertices[i].y,
                vertices[i].z), Quaternion.identity);
        }*/

        /*for (int i = 0; i < 20; i++)
        {
            sommets[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sommets[i].transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            sommets[i].transform.position = positions[i];
        }*/

       
        // 0 3 2 8 10
        // 0 10 18 12 13
        /*DrawPentagon(0, 0, 3, 2, 8, 10);
        DrawPentagon(1, 0, 10, 18, 12, 13);
        DrawPentagon(2, 6, 9, 15, 19, 16);
        DrawPentagon(3, 2, 7, 9, 6, 8);
        DrawPentagon(4, 6, 16, 18, 10, 8);
        DrawPentagon(5, 1, 4, 7, 2, 3);
        DrawPentagon(5, 12, 18, 16, 19, 17);
        DrawPentagon(6, 4, 5, 15, 9, 7);
        DrawPentagon(7, 1, 4, 7, 2, 3);
        DrawPentagon(8, 1, 3, 0, 13, 11);
        DrawPentagon(9, 5, 14, 17, 19, 15);
        DrawPentagon(10, 1, 11, 14, 5, 4);
        DrawPentagon(11, 11, 13, 12, 17, 14);*/

        DrawStar(0, 0, 3, 2, 8, 10);
        DrawStar(1, 0, 10, 18, 12, 13);
        DrawStar(2, 6, 9, 15, 19, 16);
        DrawStar(3, 2, 7, 9, 6, 8);
        DrawStar(4, 6, 16, 18, 10, 8);
        DrawStar(5, 1, 4, 7, 2, 3);
        DrawStar(5, 12, 18, 16, 19, 17);
        DrawStar(6, 4, 5, 15, 9, 7);
        DrawStar(7, 1, 4, 7, 2, 3);
        DrawStar(8, 1, 3, 0, 13, 11);
        DrawStar(9, 5, 14, 17, 19, 15);
        DrawStar(10, 1, 11, 14, 5, 4);
        DrawStar(11, 11, 13, 12, 17, 14);

        mesh.vertices = verticesD;
        mesh.triangles = triangle;
        mesh.RecalculateNormals();
    }

    void DrawPentagon(int n, int p1, int p2, int p3, int p4, int p5)
    {
        verticesD[n * 5] = vertices[p1];
        verticesD[n * 5 + 1] = vertices[p2];
        verticesD[n * 5 + 2] = vertices[p3];
        verticesD[n * 5 + 3] = vertices[p4];
        verticesD[n * 5 + 4] = vertices[p5];
        
        DrawTriangle(n * 3    , n * 5, n * 5 + 1, n * 5 + 2);
        DrawTriangle(n * 3 + 1, n * 5, n * 5 + 2, n * 5 + 3);
        DrawTriangle(n * 3 + 2, n * 5, n * 5 + 3, n * 5 + 4);
    }

    void DrawStar(int n, int p1, int p2, int p3, int p4, int p5)
    {
        verticesD[n * 10] = vertices[p1];
        verticesD[n * 10 + 2] = vertices[p2];
        verticesD[n * 10 + 4] = vertices[p3];
        verticesD[n * 10 + 6] = vertices[p4];
        verticesD[n * 10 + 8] = vertices[p5];

        verticesD[n * 10 + 1] = GetIntersection(
            verticesD[n * 10 + 8],
            verticesD[n * 10 ],
            verticesD[n * 10 + 2],
            verticesD[n * 10 + 4]);
        verticesD[n * 10 + 3] = GetIntersection(
            verticesD[n * 10],
            verticesD[n * 10 + 2],
            verticesD[n * 10 + 4],
            verticesD[n * 10 + 6]);
        verticesD[n * 10 + 5] = GetIntersection(
            verticesD[n * 10 + 2],
            verticesD[n * 10 + 4],
            verticesD[n * 10 + 6],
            verticesD[n * 10 + 8]);
        verticesD[n * 10 + 7] = GetIntersection(
            verticesD[n * 10 + 4],
            verticesD[n * 10 + 6],
            verticesD[n * 10 + 8],
            verticesD[n * 10]);
        verticesD[n * 10 + 9] = GetIntersection(
            verticesD[n * 10 + 6],
            verticesD[n * 10 + 8],
            verticesD[n * 10],
            verticesD[n * 10 + 2]);
        //normals[n * 5   ] = ;
        DrawTriangle(n * 5    , n * 10    , n * 10 + 1, n * 10 + 2);
        DrawTriangle(n * 5 + 1, n * 10 + 2, n * 10 + 3, n * 10 + 4);
        DrawTriangle(n * 5 + 2, n * 10 + 4, n * 10 + 5, n * 10 + 6);
        DrawTriangle(n * 5 + 3, n * 10 + 6, n * 10 + 7, n * 10 + 8);
        DrawTriangle(n * 5 + 4, n * 10 + 8, n * 10 + 9, n * 10    );
    }

    Vector3 GetIntersection(Vector3 a1, Vector3 a2, Vector3 b1, Vector3 b2) {
        float phi = (float)(Mathf.Sqrt(5) - 1) / 2; // The golden ratio
        return a2 + ( a2 - a1 ) / phi;
    }

    void DrawTriangle(int n, int p1, int p2, int p3)
    {
        triangle[n * 3] = p1;
        triangle[n * 3 + 1] = p2;
        triangle[n * 3 + 2] = p3;
    }
    void DrawTriangleStar(int n, int p1, int p2, int p3)
    {
        triangle[n * 3] = p1;
        triangle[n * 3 + 1] = p2;
        triangle[n * 3 + 2] = p3;
    }

    void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.SetColors(color, color);
        lr.SetWidth(0.1f, 0.1f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        GameObject.Destroy(myLine, duration);
    }
    // Generates a list of vertices (in arbitrary order) for a tetrahedron centered on the origin.
    //name="r"> The distance of each vertex from origin
    //
    private void MakeDodecahedron(float r)
    {
        // Calculate constants that will be used to generate vertices
        float phi = (float)(Mathf.Sqrt(5) - 1) / 2; // The golden ratio

        float a = 1 / Mathf.Sqrt(3);
        float b = a / phi;
        float c = a * phi;

        // Generate each vertex
        var v = 0;
        vertices = new Vector3[20];
        foreach (var i in new[] { -1, 1 })
        {
            foreach (var j in new[] { -1, 1 })
            {
                vertices[v++] = new Vector3(
                                    0,
                                    i * c * r,
                                    j * b * r);
                vertices[v++] = new Vector3(
                                    i * c * r,
                                    j * b * r,
                                    0);
                vertices[v++] = new Vector3(
                                    i * b * r,
                                    0,
                                    j * c * r);

                foreach (var k in new[] { -1, 1 })
                    vertices[v++] = new Vector3(
                                        i * a * r,
                                        j * a * r,
                                        k * a * r);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        /* for (int i = 0; i < 20; i++) {
            
            positions[i] = new Vector3(0f, 6.0f, 0f) + Quaternion.Euler(
                    Time.time * 2.0f + 2.0f * Mathf.PI * 5.0f,
                    Time.time + 20.0f * Mathf.PI * 5.0f,
                    Time.time * 8.0f + 2.0f * Mathf.PI * 5.0f
                    ) * vertices[i];
            sommets[i].transform.position = positions[i];
        } */


        transform.SetPositionAndRotation(new Vector3(0f, 6.0f, 0f),
            Quaternion.Euler(
                new Vector3(
                    Time.time * 10.0f + 2.0f * Mathf.PI * 5.0f,
                    Time.time + 20.0f * Mathf.PI * 5.0f,
                    Time.time * 8.0f + 2.0f * Mathf.PI * 5.0f
                    )
                )
            );

        //DrawLine(positions[0], positions[1], Color.red, 3.0f);

    }

    private GUIStyle guiStyle = new GUIStyle(); //create a new variable

    void OnGUI()
    {
        /*guiStyle.fontSize = 40; //change the font size
        GUI.color = Color.magenta;
        for (int i = 0; i < 20; i++)
        {
            Vector2 screenPos = Camera.main.WorldToScreenPoint(positions[i]);
            GUI.Label(new Rect(screenPos.x - 10, Screen.height - screenPos.y - 10, screenPos.x + 10, Screen.height - screenPos.y + 10), i.ToString(), guiStyle);
        }*/
    }

    /*  private void OnDrawGizmos()
      {
             if (vertices == null) return;
             Gizmos.color = Color.black;
             for (int i = 0; i < vertices.Length; i++)
             {
                 Gizmos.DrawSphere(vertices[i], 0.1f);
             }
      }*/
}
