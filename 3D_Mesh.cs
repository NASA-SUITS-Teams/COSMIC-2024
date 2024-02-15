//Need to used unity AR Foundation Plug-in this to work
//Place in Asset's portion of unity VR file
using System.Collections.Generic; 
using UnityEngine; //C# Unity Engine
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

//Unity Requires certain functions to work 
public class MartianEnvironmentAR : MonoBehaviour
{
    public ARPointCloudManager pointCloudManager;
    public ARPlaneManager planeManager;
    public Material terrainMaterial;

    void Start()
    {
        pointCloudManager.pointCloudPrefab.SetActive(false); // Disable default point cloud visualization
        CreateMartianTerrain();
    }

    void CreateMartianTerrain()
    {
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        // Create a simple grid for terrain
        int width = 10;
        int length = 10;

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < length; z++)
            {
                float height = Mathf.PerlinNoise(x * 0.1f, z * 0.1f) * 2; // Adjust the multiplier for terrain height
                vertices.Add(new Vector3(x, height, z));
            }
        }

        // Create triangles based on the grid again need Foundation Support
        for (int x = 0; x < width - 1; x++)
        {
            for (int z = 0; z < length - 1; z++)
            {
                int index = x * length + z;
                triangles.Add(index);
                triangles.Add(index + 1);
                triangles.Add(index + length);

                triangles.Add(index + 1);
                triangles.Add(index + length + 1);
                triangles.Add(index + length);
            }
        }
        //Should Update through time

        // Create and assign mesh
        Mesh terrainMesh = new Mesh();
        terrainMesh.vertices = vertices.ToArray();
        terrainMesh.triangles = triangles.ToArray();
        terrainMesh.RecalculateNormals();

        GameObject terrainObject = new GameObject("MartianTerrain");
        terrainObject.transform.position = Vector3.zero;

        MeshFilter meshFilter = terrainObject.AddComponent<MeshFilter>();
        meshFilter.mesh = terrainMesh;

        MeshRenderer meshRenderer = terrainObject.AddComponent<MeshRenderer>();
        meshRenderer.material = terrainMaterial;
    }
}
