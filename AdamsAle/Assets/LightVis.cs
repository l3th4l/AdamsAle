using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightVis : MonoBehaviour
{
    public float rad;
    public float res;
    public float angle;
    public float offset;

    public bool isCam;
    public LayerMask CamMask;

    public bool SeenPlayer;

    public MeshFilter visMeshFilter;
    private Mesh visMesh;

    void Start()
    {
        visMesh = new Mesh();
        visMesh.name = "VisMesh";
        visMeshFilter.mesh = visMesh;
    }

    void Update()
    {
        float radAngle = Mathf.Deg2Rad * angle;
        SeenPlayer = false;

        List<Vector3> Points = new List<Vector3>(); 

        for (int i = 0; i <= Mathf.Round(res * radAngle); i++)
        {
            Vector3 direction = (transform.forward * Mathf.Cos(i / res + offset * Mathf.Deg2Rad) + transform.up * Mathf.Sin(i / res + offset * Mathf.Deg2Rad));
            


            RaycastHit RHit;
            if (Physics.Raycast(transform.position, direction, out RHit, rad, CamMask))
            {
                Debug.DrawRay(transform.position, RHit.point-transform.position);

                if (isCam)
                {
                    if (RHit.collider.CompareTag("Player"))
                    {
                        SeenPlayer = true;
                    }
                }
                Points.Add(RHit.point);
            }
            else
            {
                Debug.DrawRay(transform.position, direction*rad);
                Points.Add(transform.position+direction*rad);
            }
        }

        int VertCount = Points.Count + 1;
        Vector3[] Vertices = new Vector3[VertCount];
        int[] Triangles = new int[(VertCount - 2) * 3];

        Vertices[0] = Vector3.zero;

        for (int i = 0; i < VertCount - 1; i++)
        {
            Vertices[i + 1] = transform.InverseTransformPoint(Points[i]);

            if (i < VertCount - 2)
            {
                Triangles[i * 3] = 0;
                Triangles[i * 3 + 1] = i + 1;
                Triangles[i * 3 + 2] = i + 2;
            }
        }
        visMesh.Clear();
        visMesh.vertices = Vertices;
        visMesh.triangles = Triangles;
        visMesh.RecalculateNormals();
    }
}
