namespace DecalSystem {
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Rendering;

    static class DecalUtils {


        public static MeshFilter[] GetAffectedObjects(Decal decal) {
            var bounds = GetBounds( decal );
            var isOnlyStatic = decal.gameObject.isStatic;

            var a = GameObject.FindObjectsOfType<MeshRenderer>();
            var b = a.Where(i => i.GetComponent<Decal>() == null); // ignore another decals
            var c = b.Where(i => i.gameObject.isStatic || !isOnlyStatic);
            var d = c.Where(i => HasLayer(decal.LayerMask, i.gameObject.layer));
            var e = d.Where(i => bounds.Intersects(i.bounds));

            var f = e.Select(i => i.GetComponent<MeshFilter>());
            var g = f.Where(i => i);


            return g
                
                
                
                

                
                
                
                
                .ToArray();
        }

        public static Terrain[] GetAffectedTerrains(Decal decal) {
            var bounds = GetBounds( decal );
            var isOnlyStatic = decal.gameObject.isStatic;

            return Terrain.activeTerrains
                .Where( i => i.gameObject.isStatic || !isOnlyStatic )
                .Where( i => HasLayer( decal.LayerMask, i.gameObject.layer ) )
                .Where( i => bounds.Intersects( i.GetBounds() ) )
                .ToArray();
        }


        public static Bounds GetBounds(Decal decal) {
            var transform = decal.transform;
            var size = transform.lossyScale;
            var min = -size / 2f;
            var max = size / 2f;

            var vts = new Vector3[] {
                new Vector3( min.x, min.y, min.z ),
                new Vector3( max.x, min.y, min.z ),
                new Vector3( min.x, max.y, min.z ),
                new Vector3( max.x, max.y, min.z ),

                new Vector3( min.x, min.y, max.z ),
                new Vector3( max.x, min.y, max.z ),
                new Vector3( min.x, max.y, max.z ),
                new Vector3( max.x, max.y, max.z ),
            };

            vts = vts.Select( transform.TransformDirection ).ToArray();
            min = vts.Aggregate( Vector3.Min );
            max = vts.Aggregate( Vector3.Max );

            return new Bounds( transform.position, max - min );
        }

        private static Bounds GetBounds(this Terrain terrain) {
            var bounds = terrain.terrainData.bounds;
            bounds.center += terrain.transform.position;
            return bounds;
        }


        public static void SetDirty(Decal decal) {
/*            if (decal.gameObject.scene.IsValid()) {
                if (!EditorApplication.isPlaying) EditorSceneManager.MarkSceneDirty( decal.gameObject.scene );
            } else {
                EditorUtility.SetDirty( decal.gameObject );
            }*/
        }


        public static void FixRatio(Decal decal, ref Vector3 oldScale) {
            var transform = decal.transform;
            var rect = decal.Sprite.rect;
            var ratio = rect.width / rect.height;

            var scale = transform.localScale;
            FixRatio( ref scale, ref oldScale, ratio );

            var hasChanged = transform.hasChanged;
            transform.localScale = scale;
            transform.hasChanged = hasChanged;
        }

        private static void FixRatio(ref Vector3 scale, ref Vector3 oldScale, float ratio) {
            if (!Mathf.Approximately( oldScale.x, scale.x )) scale.y = scale.x / ratio; // if scale.x was changed then fix scale.y
            else
            if (!Mathf.Approximately( oldScale.y, scale.y )) scale.x = scale.y * ratio;
            else
            if (!Mathf.Approximately( scale.x / scale.y, ratio )) scale.x = scale.y * ratio;

            oldScale = scale;
        }


        // Helpers
        private static bool HasLayer(LayerMask mask, int layer) {
            return (mask.value & 1 << layer) != 0;
        }

        public static Mesh MakeReadableMeshCopy(Mesh nonReadableMesh)
        {
            Mesh meshCopy = new Mesh();
            meshCopy.indexFormat = nonReadableMesh.indexFormat;

            // Handle vertices
            GraphicsBuffer verticesBuffer = nonReadableMesh.GetVertexBuffer(0);
            int totalSize = verticesBuffer.stride * verticesBuffer.count;
            byte[] data = new byte[totalSize];
            verticesBuffer.GetData(data);
            meshCopy.SetVertexBufferParams(nonReadableMesh.vertexCount, nonReadableMesh.GetVertexAttributes());
            meshCopy.SetVertexBufferData(data, 0, 0, totalSize);
            verticesBuffer.Release();

            // Handle triangles
            meshCopy.subMeshCount = nonReadableMesh.subMeshCount;
            GraphicsBuffer indexesBuffer = nonReadableMesh.GetIndexBuffer();
            int tot = indexesBuffer.stride * indexesBuffer.count;
            byte[] indexesData = new byte[tot];
            indexesBuffer.GetData(indexesData);
            meshCopy.SetIndexBufferParams(indexesBuffer.count, nonReadableMesh.indexFormat);
            meshCopy.SetIndexBufferData(indexesData, 0, 0, tot);
            indexesBuffer.Release();

            // Restore submesh structure
            uint currentIndexOffset = 0;
            for (int i = 0; i < meshCopy.subMeshCount; i++)
            {
                uint subMeshIndexCount = nonReadableMesh.GetIndexCount(i);
                meshCopy.SetSubMesh(i, new SubMeshDescriptor((int)currentIndexOffset, (int)subMeshIndexCount));
                currentIndexOffset += subMeshIndexCount;
            }

            // Recalculate normals and bounds
            meshCopy.RecalculateNormals();
            meshCopy.RecalculateBounds();

            return meshCopy;
        }

    }
}