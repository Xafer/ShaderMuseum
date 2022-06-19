using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTunnel : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;

    [SerializeField] private Camera _aCamera;
    [SerializeField] private Camera _bCamera;

    [SerializeField] private PortalRenderSurface _rsa;
    [SerializeField] private PortalRenderSurface _rsb;

    [SerializeField] private Camera _cCamera;
    [SerializeField] private Camera _dCamera;

    [SerializeField] private PortalRenderSurface _rsc;
    [SerializeField] private PortalRenderSurface _rsd;

    private void OnTriggerEnter(Collider other)
    {
        _rsa.SetSourceCamera(_mainCamera);
        _rsb.SetSourceCamera(_mainCamera);

        _rsc.SetSourceCamera(_dCamera);
        _rsd.SetSourceCamera(_cCamera);
    }

    private void OnTriggerExit(Collider other)
    {
        _rsa.SetSourceCamera(_bCamera);
        _rsb.SetSourceCamera(_aCamera);

        _rsc.SetSourceCamera(_mainCamera);
        _rsd.SetSourceCamera(_mainCamera);
    }
}
