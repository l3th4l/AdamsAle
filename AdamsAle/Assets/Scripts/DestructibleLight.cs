using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleLight : MonoBehaviour
{
    public float LightRad;
    public float Angle;

    public float Resolution;

    public float visInt;

    [SerializeField]
    LayerMask LightMask;


    [SerializeField]
    LayerMask PlayerMask;

    public MeshFilter visMeshFilter;
    private Mesh visMesh;

    void FindPlayer()
    {
        Collider[] PlayerCheck = Physics.OverlapSphere(transform.position, LightRad,PlayerMask);

        foreach(Collider pl in PlayerCheck)
        {
            Vector3 dir = (pl.transform.position - transform.position).normalized;

            Debug.DrawRay(transform.position,DirToAngle( Vector3.Angle(-Vector3.up, dir),true));

            if (Vector3.Angle(-Vector3.up, dir) <= Angle / 2)
                if (!isCamera)
                    pl.GetComponent<PlayerMovement>().lit = true;
                else
                    print("KYS");
            else
                pl.GetComponent<PlayerMovement>().lit = false;
        }

    }
    [SerializeField]
    bool isCamera;
    private void Start()
    {
        visMesh = new Mesh();
        visMesh.name = "VisMesh";
        visMeshFilter.mesh = visMesh;
    }

    private void LateUpdate()
    {
        DrawLOS();
        FindPlayer();
    }
    void DrawLOS()
    {
        int count = Mathf.RoundToInt(Angle * Resolution);
        float UnitAngle = Angle / count;

        List<Vector3> VPoints = new List<Vector3>();
        CastInfo OldCast = new CastInfo();
        for (int i=0; i <= count; i++)
        {
            float ObjAngle = transform.eulerAngles.x - Angle / 2 + i * UnitAngle;
            CastInfo LCast= LightCast(ObjAngle);

            if(i>0)
            {
                if(OldCast.hit != LCast.hit)
                {
                    EdgeInfo edge = GetEdge(LCast, OldCast);

                    if (edge.PointA != Vector3.zero)
                        VPoints.Add(edge.PointA);

                    if (edge.PointB != Vector3.zero)
                        VPoints.Add(edge.PointB);
                }
            }

            VPoints.Add(LCast.point);
            OldCast = LCast;
        }
        int VertCount = VPoints.Count + 1;
        Vector3[] Vertices = new Vector3[VertCount];
        int[] Triangles = new int[(VertCount - 2) * 3];

        Vertices[0] = Vector3.zero;

        for(int i = 0; i < VertCount -1; i++)
        {
            Vertices[i + 1] = transform.InverseTransformPoint(VPoints[i]);

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
    [SerializeField]
    int EdgePercision;

    public EdgeInfo GetEdge(CastInfo min, CastInfo max)
    {
        float minAngle = min.angle;
        float maxAngle = max.angle;
        Vector3 minpoint = Vector3.zero;
        Vector3 maxpoint = Vector3.zero;
        for(int i = 0; i < EdgePercision; i++)
        {
            float angle = (minAngle + maxAngle) / 2;
            CastInfo newCast = LightCast(angle);

            if(newCast.hit == min.hit)
            {
                minAngle = angle;
                minpoint = newCast.point;
            }
            else
            {
                maxAngle = angle;
                maxpoint = newCast.point;
            }
        }

        return new EdgeInfo(minpoint, maxpoint);

    }

    CastInfo LightCast(float GlobalAngle)
    {
        Vector3 direction = DirToAngle(GlobalAngle, true);
        RaycastHit HInfo;

        if (Physics.Raycast(transform.position, direction, out HInfo, LightRad, LightMask))
            return new CastInfo(true, HInfo.point, HInfo.distance, GlobalAngle);
        else
            return new CastInfo(false, transform.position + direction * LightRad, LightRad, GlobalAngle);
    }

    public Vector3 DirToAngle(float angle , bool Global)
    {
        if (!Global)
            angle += transform.eulerAngles.x;

        return (new Vector3(-Mathf.Sin(angle * Mathf.Deg2Rad), -Mathf.Cos(angle * Mathf.Deg2Rad), 0.0f));
    }

    public struct CastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dist;
        public float angle;

        public CastInfo(bool _hit, Vector3 _point, float _dist, float _angle)
        {
            hit = _hit;
            point = _point;
            dist = _dist;
            angle = _angle;
        }
    }

    public struct EdgeInfo
    {
        public Vector3 PointA;
        public Vector3 PointB;

        public EdgeInfo(Vector3 A, Vector3 B)
        {
            PointA = A;
            PointB = B;
        }
    }
}
