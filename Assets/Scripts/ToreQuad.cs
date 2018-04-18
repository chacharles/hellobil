using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToreQuad : MonoBehaviour {

    public int xSlices;
    public int ySlices;
    public float toreL1;
    public float toreL2;
    public float toreMazeHeight;

    private AudioSource audioSource;

    private void Awake()
    {
        InitMaze();
        BuildMaze(5, 5);
        //Generate();
    }

    private Vector3[] vertices;
    private Vector3[] vertices2;
    private Vector3[] normals;
    private Vector2[] uvs;
    //private Vector4[] tangents;

    private int[] maze;

    private Mesh mesh;

    private void Generate()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";

        vertices = new Vector3[(xSlices + 1) * (ySlices + 1) * 2 ];
        normals = new Vector3[(xSlices + 1) * (ySlices + 1) * 2 ];

        uvs = new Vector2[(xSlices + 1) * (ySlices + 1) * 2];

        for (int i=0, y = 0; y <= ySlices; y++)
        {
            for (int x = 0; x <= xSlices; x++, i++)
            {
                uvs[i] = new Vector2((float)x / xSlices, (float)y / ySlices);
                uvs[i + (xSlices + 1) * (ySlices + 1)] = new Vector2((float)x / xSlices, (float)y / ySlices);
            }
        }

        //    Vector2[] uv = new Vector2[vertices.Length];
        for (int i = 0, y = 0; y <= ySlices; y++)
        {
            for (int x = 0; x <= xSlices; x++, i++)
            {
                vertices[i] = new Vector3(toreL2, 0.0f, 0.0f);
                vertices[i] = Quaternion.Euler(0.0f, 0.0f, 360.0f * x / xSlices) * vertices[i];
                vertices[i] += new Vector3(toreL1, 0.0f, 0.0f);
                vertices[i] = Quaternion.Euler(0.0f, 360.0f * y / ySlices, 0.0f) * vertices[i];

                //vertices[i] = new Vector3(toreL2, 6+x, y);

                vertices[i + (xSlices + 1) * (ySlices + 1)] = new Vector3(toreL2 + toreMazeHeight, 0.0f, 0.0f);
                vertices[i + (xSlices + 1) * (ySlices + 1)] = Quaternion.Euler(0.0f, 0.0f, 360.0f * x / xSlices) * vertices[i + (xSlices + 1) * (ySlices + 1)];
                vertices[i + (xSlices + 1) * (ySlices + 1)] += new Vector3(toreL1, 0.0f, 0.0f);
                vertices[i + (xSlices + 1) * (ySlices + 1)] = Quaternion.Euler(0.0f, 360.0f * y / ySlices, 0.0f) * vertices[i + (xSlices + 1) * (ySlices + 1)];

                normals[i] = new Vector3(1.0f, 0.0f, 0.0f);
                normals[i] = Quaternion.Euler(0.0f, 0.0f, 360.0f * x / xSlices) * normals[i];
                normals[i] = Quaternion.Euler(0.0f, 360.0f * y / ySlices, 0.0f) * normals[i];

                normals[i + (xSlices + 1) * (ySlices + 1)] = new Vector3(1.0f, 0.0f, 0.0f);
                normals[i + (xSlices + 1) * (ySlices + 1)] = Quaternion.Euler(0.0f, 0.0f, 360.0f * x / xSlices) * normals[i + (xSlices + 1) * (ySlices + 1)];
                normals[i + (xSlices + 1) * (ySlices + 1)] = Quaternion.Euler(0.0f, 360.0f * y / ySlices, 0.0f) * normals[i + (xSlices + 1) * (ySlices + 1)];
                
                /* tangents[i] = new Vector4();
                 tangents[i] = Quaternion.Euler(0.0f, 0.0f, 360.0f * x / xSlices) * tangents[i];
                 tangents[i] += new Vector3(toreL1, 0.0f, 0.0f);
                 tangents[i] = Quaternion.Euler(0.0f, 360.0f * y / ySlices, 0.0f) * tangents[i];*/

                //             uv[i] = new Vector2((float)x / xSlices, (float)y / ySlices);

                //Debug.Log("x=" + x + " y=" + y + " v=" + vertices[i]);
            }
        }

        //     mesh.uv = uv;

        int[] triangles = new int[xSlices * ySlices * 6 /*+ nbWall * 3*/];
        for (int ti = 0, vi = 0, mi = 0, y = 0; y < ySlices; y++, vi++)
        {
            for (int x = 0; x < xSlices; x++, ti += 6, vi++, mi++)
            {
                if (maze[mi] == 0)
                {
                    triangles[ti] = vi;
                    triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                    triangles[ti + 4] = triangles[ti + 1] = vi + xSlices + 1;
                    triangles[ti + 5] = vi + xSlices + 2;
                }
                else
                {
                    triangles[ti] = vi + (xSlices + 1) * (ySlices + 1);
                    triangles[ti + 3] = triangles[ti + 2] = vi + 1 + (xSlices + 1) * (ySlices + 1);
                    triangles[ti + 4] = triangles[ti + 1] = vi + xSlices + 1 + (xSlices + 1) * (ySlices + 1);
                    triangles[ti + 5] = vi + xSlices + 2 + (xSlices + 1) * (ySlices + 1);
                }
                //Debug.Log("x=" + x + " y=" + y + " nb=" + ti/6 +  " tri=" + triangles[ti]);
            }
        }

        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;
        mesh.normals = normals;
        //mesh.tangents = tangents;

        //mesh.RecalculateNormals();
        mesh.RecalculateTangents();
    }

    /*    private void OnDrawGizmos()
         {
             if (vertices == null) return;
             Gizmos.color = Color.black;
             for (int i = 0; i < vertices.Length; i++)
             {
                 Gizmos.DrawSphere(vertices[i], 0.1f);
             }
         }*/

    private int nbWall = 0;

    void InitMaze() {

        maze = new int[xSlices * ySlices];
        for (int i = 0; i < xSlices; i++)
            for (int j = 0; j < ySlices; j++) {
                maze[i + j * xSlices] = 0;// (Random.Range(0.0f, 1.0f) < 0.5f?1:0);
            }
    }
    
    public void Shuffle(int[] array)
    {
        int n = array.GetLength(0);
        while (n > 1)
        {
            n--;
            int i = Random.Range(0,n + 1);
            int temp = array[i];
            array[i] = array[n];
            array[n] = temp;
        }
    }
    void BuildMaze(int x, int y) {

        if (x < 0) x += xSlices;
        if (x >= xSlices) x -= xSlices;
        if (y < 0) y += ySlices;
        if (y >= ySlices) y -= ySlices;
        //Debug.Log("BuildMaze(" + x + "," + y + ")");
        SetMaze(x, y, 1);

        int[] numbers = new int[] { 0, 1, 2, 3 };
        Shuffle(numbers);

        //Debug.Log("BuildMaze(" + x + "," + y + ")");
        //Debug.Log(numbers[0] + " " + numbers[1] + " " + numbers[2] + " " + numbers[3]);
        for (int i = 0; i < 4; i++)
        {
            switch (numbers[i])
            {
                case 0:
                    // Right
                    if ((GetMaze(x + 1, y) == 0) &&
                        (GetMaze(x + 1, y + 1) == 0) &&
                        (GetMaze(x + 1, y - 1) == 0) &&
                        (GetMaze(x + 2, y) == 0) &&
                        (GetMaze(x + 2, y + 1) == 0) &&
                        (GetMaze(x + 2, y - 1) == 0))
                    {
                        BuildMaze(x + 1, y);
                    }
                    break;
                case 1:
                    // Left
                    if ((GetMaze(x - 1, y) == 0) &&
                        (GetMaze(x - 1, y + 1) == 0) &&
                        (GetMaze(x - 1, y - 1) == 0) &&
                        (GetMaze(x - 2, y) == 0) &&
                        (GetMaze(x - 2, y + 1) == 0) &&
                        (GetMaze(x - 2, y - 1) == 0))

                    {
                        BuildMaze(x - 1, y);
                    }
                    break;
                case 2:
                    // Up
                    if ((GetMaze(x, y + 1) == 0) &&
                        (GetMaze(x + 1, y + 1) == 0) &&
                        (GetMaze(x - 1, y + 1) == 0) &&
                        (GetMaze(x, y + 2) == 0) &&
                        (GetMaze(x + 1, y + 2) == 0) &&
                        (GetMaze(x - 1, y + 2) == 0))
                    {
                        BuildMaze(x, y + 1);
                    }

                    break;
                case 3:
                    // Down
                    if ((GetMaze(x, y - 1) == 0) &&
                        (GetMaze(x + 1, y - 1) == 0) &&
                        (GetMaze(x - 1, y - 1) == 0) &&
                        (GetMaze(x, y - 2) == 0) &&
                        (GetMaze(x + 1, y - 2) == 0) &&
                        (GetMaze(x - 1, y - 2) == 0))
                    {
                        BuildMaze(x, y - 1);
                    }
                    break;
            }
        }

        for (int i = 0; i < xSlices; i++)
            for (int j = 0; j < ySlices; j++)
            {
                if (GetMaze(i, j) == 0)
                {
                    if (GetMaze(i, j + 1) == 1) nbWall += 2;
                    if (GetMaze(i + 1, j) == 1) nbWall += 2;
                    if (GetMaze(i - 1, j) == 1) nbWall += 2;
                    if (GetMaze(i, j - 1) == 1) nbWall += 2;
                }
            }
    }


    int GetMaze(int x, int y)
    {
        // Debug.Log("Before x=" + x + " y=" + y);
        if (x < 0) x += xSlices;
        if (x >= xSlices) x -= xSlices;
        if (y < 0) y += ySlices;
        if (y >= ySlices) y -= ySlices;
        // Debug.Log("After x=" + x + " y=" + y + " index=" + (int) (x + (y * xSlices)));
        return maze[x + (y * xSlices)];
    }

    void SetMaze(int x, int y, int v)
    {
        maze[(x % xSlices) + (y % ySlices) * xSlices] = v;
    }

    // Use this for initialization
    void Start () {
        Generate();
    }
	
	// Update is called once per frame
	void Update () {
        transform.SetPositionAndRotation(transform.position,
            Quaternion.Euler(
                new Vector3(
                    Time.time * 6.0f + 2.0f * Mathf.PI * 5.0f,
                    Time.time + 2.0f * Mathf.PI * 5.0f,
                    Time.time * 20.0f + 2.0f * Mathf.PI * 5.0f
                    )
                )
            );
    }
}
