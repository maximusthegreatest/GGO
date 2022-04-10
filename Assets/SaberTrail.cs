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
    private const int NUM_VERTICES = 6;
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
        //Debug.Log("wep tris" + _weaponTrailTriangles);
        _rend = _trailMesh.GetComponent<Renderer>();
        _rend.material = trailMat;
        //_rend.sharedMaterial = trailMat;


        //this is just the list of vertices in the triangles in an array of ints

        _previousTipPosition = bladeTip.transform.position;
        _previousBasePosition = bladeBase.transform.position;

        //Debug.Log(" start previous tip position " + _previousTipPosition);
        //Debug.Log("start previous base position " + _previousBasePosition);
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
            if (_frameCount == (_trailFrameLength * NUM_VERTICES))
            {
                

                _frameCount = 0;
                //wipe materials
            }


            //we need to create 4 triangles because the camera only renders meshes whose normals face it 
            //we need to create 2 winding directions per triangle

            /*
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
            */

            _weaponTrailVerts[_frameCount] = bladeBase.transform.position;
            _weaponTrailVerts[_frameCount + 1] = _previousTipPosition;
            _weaponTrailVerts[_frameCount + 2] = bladeTip.transform.position;

            _weaponTrailVerts[_frameCount + 3] = _previousTipPosition;
            _weaponTrailVerts[_frameCount + 4] = bladeBase.transform.position;
            _weaponTrailVerts[_frameCount + 5] = _previousBasePosition;


            //Debug.Log("Blade vert test " + _weaponTrailVerts[0]);
            //_weaponTrailMesh.vertices = _weaponTrailVerts;

            _weaponTrailTriangles[_frameCount] = _frameCount;
            _weaponTrailTriangles[_frameCount + 1] = _frameCount + 1;
            _weaponTrailTriangles[_frameCount + 2] = _frameCount + 2;


            _weaponTrailTriangles[_frameCount + 3] = _frameCount + 3;
            _weaponTrailTriangles[_frameCount + 4] = _frameCount + 4;
            _weaponTrailTriangles[_frameCount + 5] = _frameCount + 5;
            /*
            _weaponTrailTriangles[_frameCount + 6] = _frameCount + 6;
            _weaponTrailTriangles[_frameCount + 7] = _frameCount + 7;
            _weaponTrailTriangles[_frameCount + 8] = _frameCount + 8;
            _weaponTrailTriangles[_frameCount + 9] = _frameCount + 9;
            _weaponTrailTriangles[_frameCount + 10] = _frameCount + 10;
            _weaponTrailTriangles[_frameCount + 11] = _frameCount + 11;
            */


            //_weaponTrailMesh.SetTriangles(_weaponTrailTriangles, true)
            //Debug.Log("wep trail verts " + _weaponTrailVerts);
            //Debug.Log("wep trail tris " + _weaponTrailTriangles);

            _weaponTrailMesh.MarkDynamic();

            Debug.Log("Wep Trail verts");
            foreach(Vector3 wepVert in _weaponTrailVerts)
            {
                Debug.Log(wepVert);
            }
            
            _weaponTrailMesh.vertices = _weaponTrailVerts;
            _weaponTrailMesh.triangles = _weaponTrailTriangles;

            //Vector2[] uvs = new Vector2[_weaponTrailVerts.Length];

            //get width and height of plane

            _weaponTrailMesh.RecalculateBounds();

            Vector2[] uvs = CalculateUVs(_weaponTrailVerts, 1);
            Debug.Log("uvs");
            foreach (Vector2 uv in uvs)
            {
                Debug.Log("uv: " + uv);
            }
            
            _weaponTrailMesh.uv = uvs;

            //apply a material to the triangles that fades to alpha
            //_rend.material = trailMat;

            _previousTipPosition = bladeTip.transform.position;
            _previousBasePosition = bladeBase.transform.position;

            _weaponTrailMesh.RecalculateNormals();
            //_weaponTrailMesh.RecalculateBounds();
            _frameCount += NUM_VERTICES;

            

            //Debug.Log("previous tip position " + _previousTipPosition);
            //Debug.Log("previous base position " + _previousBasePosition);

            //Debug.Log("difference " + (_previousTipPosition.y - _previousBasePosition.y));
        } else
        {
            _weaponTrailMesh.Clear();
        }


    }


    public Vector2[] CalculateUVs(Vector3[] v/*vertices*/, float scale)
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

            float scale1, scale2;

            if (_rend.bounds.size.x == 0)
            {
                scale1 = 1;
            }
            else
            {
                scale1 = _rend.bounds.size.x;
            }

            if (_rend.bounds.size.y == 0)
            {
                scale2 = 1;
            }
            else
            {
                scale2 = _rend.bounds.size.y;
            }

            Debug.Log("wep rend size " + _rend.bounds.size);

            //Debug.Log("mesh sizes " + _weaponTrailMesh.bounds.size.y + " " + _weaponTrailMesh.bounds.size.z);

            switch (facing)
            {
                case Facing.Forward:

                    uvs[i0] = ScaledUV(v0.x, v0.y, scale1, scale2);
                    uvs[i1] = ScaledUV(v1.x, v1.y, scale1, scale2);
                    uvs[i2] = ScaledUV(v2.x, v2.y, scale1, scale2);
                    
                    Debug.Log("forward uvs");
                    Debug.Log(uvs[i0] + " " + uvs[i1] + " " + uvs[i2]);

                    break;
                case Facing.Up:

                    uvs[i0] = ScaledUV(v0.x, v0.z, scale1, scale2);
                    uvs[i1] = ScaledUV(v1.x, v1.z, scale1, scale2);
                    uvs[i2] = ScaledUV(v2.x, v2.z, scale1, scale2);
                    Debug.Log("up uvs");
                    //Debug.Log("mesh sizes " + _weaponTrailMesh.bounds.size.x + " " + _weaponTrailMesh.bounds.size.z);
                    Debug.Log(uvs[i0] + " " + uvs[i1] + " " + uvs[i2]);
                    break;
                case Facing.Right:

                    uvs[i0] = ScaledUV(v0.y, v0.z, scale1, scale2);
                    uvs[i1] = ScaledUV(v1.y, v1.z, scale1, scale2);
                    uvs[i2] = ScaledUV(v2.y, v2.z, scale1, scale2);
                    Debug.Log("right uvs");                    
                    Debug.Log(uvs[i0] + " " + uvs[i1] + " " + uvs[i2]);
                    break;
            }

            /*
            switch (facing)
            {
                case Facing.Forward:
                    Debug.Log("facing forward");
                    uvs[i0] = ScaledUV(v0.x, v0.y, scale);
                    uvs[i1] = ScaledUV(v1.x, v1.y, scale);
                    uvs[i2] = ScaledUV(v2.x, v2.y, scale);
                    break;
                case Facing.Up:
                    Debug.Log("facing up");
                    uvs[i0] = ScaledUV(v0.x, v0.z, scale);
                    uvs[i1] = ScaledUV(v1.x, v1.z, scale);
                    uvs[i2] = ScaledUV(v2.x, v2.z, scale);
                    break;
                case Facing.Right:
                    Debug.Log("facing right");
                    uvs[i0] = ScaledUV(v0.y, v0.z, scale);
                    uvs[i1] = ScaledUV(v1.y, v1.z, scale);
                    uvs[i2] = ScaledUV(v2.y, v2.z, scale);
                    break;
            }
            */
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
        //float maxDot = 0;

        if (!FacesThisWay(v, Vector3.right, Facing.Right, ref maxDot, ref ret))
            FacesThisWay(v, Vector3.left, Facing.Right, ref maxDot, ref ret);

        if (!FacesThisWay(v, Vector3.forward, Facing.Forward, ref maxDot, ref ret))
            FacesThisWay(v, Vector3.back, Facing.Forward, ref maxDot, ref ret);

        if (!FacesThisWay(v, Vector3.up, Facing.Up, ref maxDot, ref ret))
            FacesThisWay(v, Vector3.down, Facing.Up, ref maxDot, ref ret);

        return ret;
    }
      
    private Vector2 ScaledUV(float uv1, float uv2, float scale1, float scale2)
    {
        //return new Vector2(uv1 / scale, uv2 / scale);
        //return new Vector2(uv1 / _weaponTrailMesh.bounds.size.y, uv2 / _weaponTrailMesh.bounds.size.z);

        //Debug.Log("uv1: " + uv1 + " uv2: " + uv2);

        Vector2 uvRet = new Vector2(uv1 / scale1, uv2 / scale2);
        if(uvRet[0] > 1 || uvRet[0] < 0  || uvRet[1] > 1 || uvRet[1] < 0 )
        {
            Debug.Log("Problem: " + uv1 + " " + uv2 + " " + scale1 + " " + scale2);
        }

        return uvRet;
    }
}
