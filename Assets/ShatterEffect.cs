using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Accessibility;

public class ShatterEffect : MonoBehaviour
{

    private float startLerpTime;
    private GameObject objectBeingShattered;
    
    public void StartShatter(GameObject objectToShatter, Material shatterMaterial, Material shatterMaterialInitial = null)
    {
        objectBeingShattered = objectToShatter;
        MeshFilter mf = objectToShatter.GetComponent<MeshFilter>();
        MeshRenderer mr = objectToShatter.GetComponent<MeshRenderer>();
        Mesh m = mf.mesh;

        Vector3[] verts = m.vertices;
        Vector3[] normals = m.normals;

        Vector2[] uvs = m.uv;

        for(int submesh = 0; submesh < m.subMeshCount; submesh++)
        {
            int[] indices = m.GetTriangles(submesh);
            //gets a triangle, then stores the coordinate of each vertex
               //so like 10, 5, 2

            
            for(int i = 0; i < indices.Length; i += 9)
            {
                //incrementing by three because theres 3 verti in a triangle
                Vector3[] newVerts = new Vector3[3];
                Vector3[] newNormals = new Vector3[3];
                Vector2[] newUvs = new Vector2[3];

                for(int n = 0; n < 3; n++)
                {
                    
                    int index = indices[i + n];
                    newVerts[n] = verts[index];
                    newUvs[n] = uvs[index];
                    newNormals[n] = normals[index];
                }

                Mesh mesh = new Mesh();
                mesh.vertices = newVerts;
                //mesh.normals = newNormals;
                mesh.uv = newUvs;

                mesh.triangles = new int[] { 0, 1, 2, 2, 1, 0 };

                GameObject GO = new GameObject("Triangle" + (i / 3));
                GO.layer = 19;
                //give it a layer
                GO.transform.position = objectToShatter.transform.position;
                GO.transform.rotation = objectToShatter.transform.rotation;

                //GO.AddComponent<MeshRenderer>().material = mr.materials[submesh];
                if(shatterMaterialInitial)
                {
                    //GO.AddComponent<MeshRenderer>().material = shatterMaterialInitial;
                    //for now just do the blue mat
                    GO.AddComponent<MeshRenderer>().material = shatterMaterial;
                    startLerpTime = Time.time;
                    
                    //Right now this doesn't seem to work. Could potentially convert this to a particle effect
                    /*
                    ShatterEffectTransition shatterEffectTransition = GO.AddComponent<ShatterEffectTransition>();
                    shatterEffectTransition.shatterMaterialInitial = shatterMaterialInitial;
                    shatterEffectTransition.shatterMaterialTransition = shatterMaterial;
                    */
                    
                } else
                {
                    GO.AddComponent<MeshRenderer>().material = shatterMaterial;
                }
                

                //

                GO.AddComponent<MeshFilter>().mesh = mesh;
                GO.AddComponent<BoxCollider>();
                Rigidbody rb = GO.AddComponent<Rigidbody>();
                rb.useGravity = false;
                //StartCoroutine(LerpMaterial(GO.GetComponent<MeshRenderer>().material, shatterMaterial, shatterMaterialInitial));
                Debug.Log("Calling coroutine!");
                //StartCoroutine("FuckYou");
                
                //StartCoroutine("LerpMaterial", GO.GetComponent<MeshRenderer>().material, shatterMaterial, shatterMaterialInitial);
                rb.AddExplosionForce(50, objectToShatter.transform.position, 5);
                
                Destroy(GO, 1);

            }
            

        }

        mr.enabled = false;                        
        Destroy(objectToShatter);



    }

    



}