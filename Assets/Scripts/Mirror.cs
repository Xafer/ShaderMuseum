using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    /*private Camera mirrorCamera;
    private Camera playerCamera;

    private RenderTexture renderTexture;
    private Material renderTextureMaterial;

    [SerializeField] private Transform pfPlayerCamera;
    // Start is called before the first frame update
    void Start()
    {
        mirrorCamera = Instantiate(pfPlayerCamera).GetComponent<Camera>();
        mirrorCamera.transform.parent = transform;
        mirrorCamera.depth = 0;
        Destroy(mirrorCamera.GetComponent<PostProcessLayer>());
        Destroy(mirrorCamera.GetComponent<PostProcessVolume>());
        Destroy(mirrorCamera.GetComponent<PlayerCamera>());

        CreateRenderTexture();

    }
    Vector3[] newVertices;
    Vector2[] newUV;
    int[] newTriangles;

    void Start()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.vertices = newVertices;
        mesh.uv = newUV;
        mesh.triangles = newTriangles;
    }

    private void CreateMesh

    private void CreateRenderTexture()
    {
        renderTexture = new RenderTexture(768,364,16);
        renderTexture.Create();

        renderTextureMaterial = new Material(Shader.Find("Unlit/Mirror"));
        renderTextureMaterial.mainTexture = renderTexture;

        mirrorCamera.targetTexture = renderTexture;

        transform.Find("MirrorPlane").GetComponent<MeshRenderer>().material = renderTextureMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerCamera == null)playerCamera = Player.Instance.camera.GetComponent<Camera>();

        mirrorCamera.transform.position = playerCamera.transform.position;
        mirrorCamera.transform.localPosition = new Vector3(mirrorCamera.transform.localPosition.x, mirrorCamera.transform.localPosition.y, mirrorCamera.transform.localPosition.z * -1);
        mirrorCamera.transform.eulerAngles = new Vector3(playerCamera.transform.eulerAngles.x, 180- Player.Instance.transform.eulerAngles.y + transform.eulerAngles.y, playerCamera.transform.eulerAngles.z);

        SetNearClipPlane();
    }

    public void SetNearClipPlane()
    {
        Transform clipPlane = transform;
        float dot = Mathf.Sign(Vector3.Dot(transform.forward, transform.position - mirrorCamera.transform.position));

        Vector3 cameraSpacePosition = mirrorCamera.worldToCameraMatrix.MultiplyPoint(clipPlane.position);
        Vector3 cameraSpaceNormal = mirrorCamera.worldToCameraMatrix.MultiplyVector(clipPlane.forward) * dot;
        float cameraSpaceDistance = -Vector3.Dot(cameraSpacePosition, cameraSpaceNormal);
        Vector4 clipPlaneCameraSpace = new Vector4(cameraSpaceNormal.x, cameraSpaceNormal.y, cameraSpaceNormal.z, cameraSpaceDistance);

        mirrorCamera.projectionMatrix = playerCamera.CalculateObliqueMatrix(clipPlaneCameraSpace);
    }*/
}
