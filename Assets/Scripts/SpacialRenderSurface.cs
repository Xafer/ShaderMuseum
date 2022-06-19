using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpacialRenderSurface : MonoBehaviour
{
    //This is a class to attach unto a model to generate a portal or mirror effect.
    //It generates a plane with specified dimension and a camera to render the projection
    //and assigns it a new rendertexture with proportional detail

    protected RenderTexture _rt;
    [SerializeField]protected GameObject _rc;
    protected Material _spacialRenderMaterial;

    [SerializeField] protected GameObject _mirrorObject;
    //protected Transform _targetObject;

    [SerializeField] protected Camera _sourceCamera;
    [SerializeField] protected int _textureSize;

    protected bool loaded = false;

    protected virtual void GenerateMesh(float width, float height, Vector3 offset)
    {
        //Generate the mirror object and the meshfilter and renderer component to render it
        _mirrorObject = new GameObject("Mirror");
        MeshFilter mf = _mirrorObject.AddComponent<MeshFilter>();
        MeshRenderer mr = _mirrorObject.AddComponent<MeshRenderer>();

        //Halves of width and height
        Vector2 h = new Vector2(width / 2, height / 2);

        Vector3[] newVertices = new Vector3[] { new Vector3(-h.x,-h.y,0),  //bottom left
                                                new Vector3(h.x,-h.y,0),  //bottom right
                                                new Vector3(-h.x,h.y,0),  //top left
                                                new Vector3(h.x,h.y,0)}; //top right

        Vector2[] newUV = new Vector2[] {   new Vector2(0,0),
                                            new Vector2(1,0),
                                            new Vector2(0,1),
                                            new Vector2(1,1) };

        int[] newTriangles = new int[] { 0, 1, 2, 2, 1, 3 };

        Mesh mesh = new Mesh();

        mf.mesh = mesh;

        mesh.vertices = newVertices;
        mesh.uv = newUV;
        mesh.triangles = newTriangles;

        _mirrorObject.transform.parent = transform;
        _mirrorObject.transform.localPosition = offset;
    }

    //Used to generate a gameobject with all the components required to make a mirror, as well as a rendertexture and its camera
    protected virtual void GenerateMirrorObject(float width, float height, Vector3 offset, string shaderName)
    {
        if (_mirrorObject == null)
            GenerateMesh(width, height, offset);

        //Generate render texture and camera

        Camera camera = null;

        if (_rc == null)
        {
            _rc = new GameObject("SpacialRenderCamera");
            camera = _rc.AddComponent<Camera>();
        }
        else
            camera = _rc.GetComponent<Camera>();


        camera.clearFlags = CameraClearFlags.SolidColor;
        camera.backgroundColor = Color.black;

        _rc.transform.parent = _mirrorObject.transform;

        //Create the renter texture with the same aspect ratio as the generate plane mesh
        _rt = new RenderTexture(_textureSize, Mathf.FloorToInt(_textureSize/ _sourceCamera.aspect),16);
        _rt.filterMode = FilterMode.Point;

        _rt.Create();

        camera.targetTexture = _rt;

        _spacialRenderMaterial = new Material(Shader.Find(shaderName));

        _spacialRenderMaterial.SetTexture("_MainTex", _rt);

        MeshRenderer mr = _mirrorObject.GetComponent<MeshRenderer>();

        mr.material = _spacialRenderMaterial;

        loaded = true;
    }

    public void SetSourceCamera(Camera cam)
    {
        _sourceCamera = cam;
    }
}
