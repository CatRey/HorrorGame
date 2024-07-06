using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
public class DoubleSidedCollision : MonoBehaviour
{
    MeshCollider toFlip;
    public MeshCollider flipped;
    
    private void Start()
    {
        toFlip = GetComponent<MeshCollider>();
        var copy = Instantiate(gameObject, transform, true);
        copy.transform.SetAsFirstSibling();
        flipped = copy.GetComponent<MeshCollider>();
        flipped.enabled = true;

        var copiedMesh = new Mesh();
        copiedMesh.SetVertices(flipped.sharedMesh.vertices);
        copiedMesh.SetUVs(0, flipped.sharedMesh.uv);
        copiedMesh.SetUVs(1, flipped.sharedMesh.uv2);
        copiedMesh.SetUVs(2, flipped.sharedMesh.uv3);
        copiedMesh.SetUVs(3, flipped.sharedMesh.uv4);
        copiedMesh.SetUVs(4, flipped.sharedMesh.uv5);
        copiedMesh.SetUVs(5, flipped.sharedMesh.uv6);
        copiedMesh.SetUVs(6, flipped.sharedMesh.uv7);
        copiedMesh.SetUVs(7, flipped.sharedMesh.uv8);
        copiedMesh.SetTriangles(flipped.sharedMesh.triangles, 0);
        copiedMesh.SetTangents(flipped.sharedMesh.tangents);
        copiedMesh.SetNormals(flipped.sharedMesh.normals);
        copiedMesh.SetColors(flipped.sharedMesh.colors);

        copiedMesh.SetIndices(copiedMesh.GetIndices(0).Concat(copiedMesh.GetIndices(0).Reverse()).ToArray(), MeshTopology.Triangles, 0);

        flipped.sharedMesh = copiedMesh;

        foreach (var item in copy.GetComponents<Component>())
        {
            if (item != flipped)
            {
                Destroy(item);
            }
        }

        foreach (Transform child in copy.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
