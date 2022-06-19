using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalRenderSurface : SpacialRenderSurface
{
    [SerializeField] private Transform _targetObject;
    private Transform _portalDestination;
    // Start is called before the first frame update
    void Start()
    {
        GenerateMirrorObject(2, 2, new Vector3(0, 0, 0), "Unlit/Portal");

        GameObject go = new GameObject("PortalTarget");
        _portalDestination = go.transform;

        if (_targetObject != null)
        {
            _portalDestination.parent = _targetObject;
            _portalDestination.localPosition = Vector3.zero;
            _portalDestination.localEulerAngles = Vector3.zero;
            //_portalDestination.SetPositionAndRotation(_targetObject.position, _targetObject.rotation);
            _portalDestination.rotation *= Quaternion.Euler(0, 180, 0);
            //go.transform.position += go.transform.forward * 0.1f;
        }
        else
            _portalDestination.transform.SetPositionAndRotation(transform.position, transform.rotation);

        _rc.transform.parent = _portalDestination;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 localSourceCameraPos = _mirrorObject.transform.InverseTransformPoint(_sourceCamera.transform.position);

        _rc.transform.localPosition = localSourceCameraPos;
        _rc.transform.localRotation = (Quaternion.Inverse(_mirrorObject.transform.rotation)*_sourceCamera.transform.rotation);
        /*
        float diff = (_mirrorObject.transform.position - _sourceCamera.transform.position).magnitude;

        if (diff < 0.3)
        {
            _sourceCamera.transform.position = _rc.transform.position;
            _sourceCamera.transform.rotation = _rc.transform.rotation;
        }*/

        //_rc.transform.rotation = _sourceCamera.transform.rotation * _targetObject.transform.rotation;

        SetNearClipPlane();
    }
    public void SetNearClipPlane()
    {
        Camera mirrorCamera = _rc.GetComponent<Camera>();

        Transform clipPlane = _portalDestination.transform;
        float dot = Mathf.Sign(Vector3.Dot(clipPlane.forward, clipPlane.position - mirrorCamera.transform.position));

        Vector3 cameraSpacePosition = mirrorCamera.worldToCameraMatrix.MultiplyPoint(clipPlane.position);
        Vector3 cameraSpaceNormal = mirrorCamera.worldToCameraMatrix.MultiplyVector(clipPlane.forward) * dot;
        float cameraSpaceDistance = -Vector3.Dot(cameraSpacePosition, cameraSpaceNormal);
        Vector4 clipPlaneCameraSpace = new Vector4(cameraSpaceNormal.x, cameraSpaceNormal.y, cameraSpaceNormal.z, cameraSpaceDistance);

        mirrorCamera.projectionMatrix = _sourceCamera.CalculateObliqueMatrix(clipPlaneCameraSpace);
    }
}
