using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MeshCollider)), RequireComponent(typeof(MeshFilter))]
public class DoubleSidedCollision : MonoBehaviour
{
    
    private void Start()
    {
        var toFlip = GetComponent<MeshCollider>();
        toFlip.sharedMesh = DecalSystem.DecalUtils.MakeReadableMeshCopy(toFlip.sharedMesh);
        toFlip.sharedMesh.SetIndices(toFlip.sharedMesh.GetIndices(0).Concat(toFlip.sharedMesh.GetIndices(0).Reverse()).ToArray(), MeshTopology.Triangles, 0);
        var toFlipVisual = GetComponent<MeshFilter>();
        toFlipVisual.mesh = DecalSystem.DecalUtils.MakeReadableMeshCopy(toFlipVisual.mesh);
        toFlipVisual.mesh.SetIndices(toFlipVisual.mesh.GetIndices(0).Concat(toFlipVisual.mesh.GetIndices(0).Reverse()).ToArray(), MeshTopology.Triangles, 0);
    }
}
