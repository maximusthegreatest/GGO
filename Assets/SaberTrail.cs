using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaberTrail : MonoBehaviour
{
    public Transform bladeTip;
    public Transform bladeBase;
    public Material trailMat;
    public bool hasTrail;

    [SerializeField]
    private GameObject _trailMesh;
    [SerializeField]
    private int _trailFrameLength;

    private Mesh _weaponTrailMesh;
    [SerializeField]
    private Vector3[] _weaponTrailVerts;
    [SerializeField]
    private int[] _weaponTrailTriangles;
    private int _frameCount;
    private Vector3 _previousTipPosition;
    private Vector3 _previousBasePosition;
    private const int NUM_VERTICES = 12;
    private Renderer _rend;

    private enum Facing { Up, Forward, Right };


    // Start is called before the first frame update
    void Start()
    {
        //weapon trail
        _weaponTrailMesh = new Mesh();
        _trailMesh.GetComponent<MeshFilter>().mesh = _weaponTrailMesh;


        //4 triangles at 3 verts a piece so 12
        _weaponTrailVerts = new Vector3[_trailFrameLength * NUM_VERTICES];
        _weaponTrailTriangles = new int[_weaponTrailVerts.Length];

        _rend = _trailMesh.GetComponent<Renderer>();

        //this is just the list of vertices in the triangles in an array of ints

        _previousTipPosition = bladeTip.transform.position;
        _previousBasePosition = bladeBase.transform.position;

        //Debug.Log("previous tip position " + _previousTipPosition);
        //Debug.Log("previous base position " + _previousBasePosition);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetStartPos(bool setStart)
    {
        if(setStart)
        {
            Debug.Log("Setting start pos");
            _previousTipPosition = bladeTip.transform.position;
            _previousBasePosition = bladeBase.transform.position;
        } else
        {
            Array.Clear(_weaponTrailTriangles, 0, _weaponTrailTriangles.Length);
            Array.Clear(_weaponTrailVerts, 0, _weaponTrailVerts.Length);
        }
        
    }


    private void LateUpdate()
    {
        
        if(hasTrail)
        {
            Debug.Log("Enabling trail");
            if (_frameCount == (_trailFrameLength * NUM_VERTICES))
            {
                _frameCount = 0;
                //wipe materials
            }


            //we need to create 4 triangles because the camera only renders meshes whose normals face it 
            //we need to create 2 winding directions per triangle

            _weaponTrailVerts[_frameCount] = bladeBase.transform.position;
            _weaponTrailVerts[_frameCount + 1] = bladeTip.transform.position;
            _weaponTrailVerts[_frameCount + 2] = _previousTipPosition;

            _weaponTrailVerts[_frameCount + 3] = bladeBase.transform.position;
            _weaponTrailVerts[_frameCount + 4] = _previousTipPosition;
            _weaponTrailVerts[_frameCount + 5] = bladeTip.transform.position;

            _weaponTrailVerts[_frameCount + 6] = _previousTipPosition;
            _weaponTrailVerts[_frameCount + 7] = bladeBase.transform.position;
            _weaponTrailVerts[_frameCount + 8] = _previousBasePosition;

            _weaponTrailVerts[_frameCount + 9] = _previousTipPosition;
            _weaponTrailVerts[_frameCount + 10] = _previousBasePosition;
            _weaponTrailVerts[_frameCount + 11] = bladeBase.transform.position;

            //_weaponTrailMesh.vertices = _weaponTrailVerts;

            _weaponTrailTriangles[_frameCount] = _frameCount;
            _weaponTrailTriangles[_frameCount + 1] = _frameCount + 1;
            _weaponTrailTriangles[_frameCount + 2] = _frameCount + 2;


            _weaponTrailTriangles[_frameCount + 3] = _frameCount + 3;
            _weaponTrailTriangles[_frameCount + 4] = _frameCount + 4;
            _weaponTrailTriangles[_frameCount + 5] = _frameCount + 5;
            _weaponTrailTriangles[_frameCount + 6] = _frameCount + 6;
            _weaponTrailTriangles[_frameCount + 7] = _frameCount + 7;
            _weaponTrailTriangles[_frameCount + 8] = _frameCount + 8;
            _weaponTrailTriangles[_frameCount + 9] = _frameCount + 9;
            _weaponTrailTriangles[_frameCount + 10] = _frameCount + 10;
            _weaponTrailTriangles[_frameCount + 11] = _frameCount + 11;

            

            //_weaponTrailMesh.SetTriangles(_weaponTrailTriangles, true)

            _weaponTrailMesh.vertices = _weaponTrailVerts;
            _weaponTrailMesh.triangles = _weaponTrailTriangles;

            Vector2[] uvs = CalculateUVs(_weaponTrailVerts, 1);

            _weaponTrailMesh.uv = uvs;

            //apply a material to the triangles that fades to alpha
            _rend.material = trailMat;

            _previousTipPosition = bladeTip.transform.position;
            _previousBasePosition = bladeBase.transform.position;

            _weaponTrailMesh.RecalculateBounds();
            _frameCount += NUM_VERTICES;

            

            //Debug.Log("previous tip position " + _previousTipPosition);
            //Debug.Log("previous base position " + _previousBasePosition);

            //Debug.Log("difference " + (_previousTipPosition.y - _previousBasePosition.y));
        } else
        {
            _weaponTrailMesh.Clear();
        }


    }


    public static Vector2[] CalculateUVs(Vector3[] v/*vertices*/, float scale)
    {
        var uvs = new Vector2[v.Length];

        for (int i = 0; i < uvs.Length; i += 3)
        {
            int i0 = i;
            int i1 = i + 1;
            int i2 = i + 2;

            Vector3 v0 = v[i0];
            Vector3 v1 = v[i1];
            Vector3 v2 = v[i2];

            Vector3 side1 = v1 - v0;
            Vector3 side2 = v2 - v0;
            var direction = Vector3.Cross(side1, side2);
            var facing = FacingDirection(direction);
            switch (facing)
            {
                case Facing.Forward:
                    uvs[i0] = ScaledUV(v0.x, v0.y, scale);
                    uvs[i1] = ScaledUV(v1.x, v1.y, scale);
                    uvs[i2] = ScaledUV(v2.x, v2.y, scale);
                    break;
                case Facing.Up:
                    uvs[i0] = ScaledUV(v0.x, v0.z, scale);
                    uvs[i1] = ScaledUV(v1.x, v1.z, scale);
                    uvs[i2] = ScaledUV(v2.x, v2.z, scale);
                    break;
                case Facing.Right:
                    uvs[i0] = ScaledUV(v0.y, v0.z, scale);
                    uvs[i1] = ScaledUV(v1.y, v1.z, scale);
                    uvs[i2] = ScaledUV(v2.y, v2.z, scale);
                    break;
            }
        }
        return uvs;
    }


    private static bool FacesThisWay(Vector3 v, Vector3 dir, Facing p, ref float maxDot, ref Facing ret)
    {
        float t = Vector3.Dot(v, dir);
        if (t > maxDot)
        {
            ret = p;
            maxDot = t;
            return true;
        }
        return false;
    }

    private static Facing FacingDirection(Vector3 v)
    {
        var ret = Facing.Up;
        float maxDot = Mathf.NegativeInfinity;

        if (!FacesThisWay(v, Vector3.right, Facing.Right, ref maxDot, ref ret))
            FacesThisWay(v, Vector3.left, Facing.Right, ref maxDot, ref ret);

        if (!FacesThisWay(v, Vector3.forward, Facing.Forward, ref maxDot, ref ret))
            FacesThisWay(v, Vector3.back, Facing.Forward, ref maxDot, ref ret);

        if (!FacesThisWay(v, Vector3.up, Facing.Up, ref maxDot, ref ret))
            FacesThisWay(v, Vector3.down, Facing.Up, ref maxDot, ref ret);

        return ret;
    }

    private static Vector2 ScaledUV(float uv1, float uv2, float scale)
    {
        return new Vector2(uv1 / scale, uv2 / scale);
    }
}
