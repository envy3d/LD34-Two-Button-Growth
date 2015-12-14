<<<<<<< HEAD
﻿using UnityEngine;
=======
using UnityEngine;
>>>>>>> refs/remotes/origin/dev

[RequireComponent(typeof(SpriteRenderer), typeof(MeshCollider))]
public class PolyColToMeshCol : MonoBehaviour
{

    void Start()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        MeshCollider meshCollider = GetComponent<MeshCollider>();
        Mesh collisionMesh = new Mesh();

        Vector3[] verts = new Vector3[sprite.vertices.Length];
        for (int i = 0; i < verts.Length - 1; i++)
        {
            verts[i] = sprite.vertices[i];
        }

        int[] tris = new int[sprite.triangles.Length];
        for (int i = 0; i < tris.Length - 1; i++)
        {
            tris[i] = sprite.triangles[i];
        }

        collisionMesh.vertices = verts;
        collisionMesh.triangles = tris;
        collisionMesh.RecalculateBounds();

        meshCollider.sharedMesh = collisionMesh;
	}
}
