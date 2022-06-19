using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorRenderSurface : SpacialRenderSurface
{
    // Start is called before the first frame update
    void Start()
    {
        GenerateMirrorObject(2, 2, new Vector3(0, 0, 0), "Unlit/Mirror");
    }

    // Update is called once per frame
    void Update()
    {
        if(loaded)
        {
            Vector3 localSourceCameraPos = _mirrorObject.transform.InverseTransformPoint(_sourceCamera.transform.position);

            _rc.transform.localPosition = new Vector3(localSourceCameraPos.x, localSourceCameraPos.y, -localSourceCameraPos.z);

            _rc.transform.rotation = _sourceCamera.transform.rotation;
            Quaternion q = Quaternion.Euler(0, 180, 0)*_rc.transform.localRotation;
            _rc.transform.localRotation = new Quaternion(q.x, -q.y, -q.z, q.w);

            SetNearClipPlane();

            transform.rotation *= Quaternion.Euler(Time.deltaTime * 12, Time.deltaTime * 16, 0);
        } 
    }
    public void SetNearClipPlane()
    {
        Camera mirrorCamera = _rc.GetComponent<Camera>();

        Transform clipPlane = _mirrorObject.transform;
        float dot = Mathf.Sign(Vector3.Dot(clipPlane.forward, clipPlane.position - mirrorCamera.transform.position));

        Vector3 cameraSpacePosition = mirrorCamera.worldToCameraMatrix.MultiplyPoint(clipPlane.position);
        Vector3 cameraSpaceNormal = mirrorCamera.worldToCameraMatrix.MultiplyVector(clipPlane.forward) * dot;
        float cameraSpaceDistance = -Vector3.Dot(cameraSpacePosition, cameraSpaceNormal);
        Vector4 clipPlaneCameraSpace = new Vector4(cameraSpaceNormal.x, cameraSpaceNormal.y, cameraSpaceNormal.z, cameraSpaceDistance);

        mirrorCamera.projectionMatrix = _sourceCamera.CalculateObliqueMatrix(clipPlaneCameraSpace);
    }
}
