using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalZone : MonoBehaviour
{
    [SerializeField] private Transform _portalTarget;

    private void OnTriggerEnter(Collider other)
    {
        TeleportableObject to = other.GetComponent<TeleportableObject>();

        if (to != null)
        {
            if (to.Teleporting)
                to.Teleporting = false;
            else
            {
                to.Teleporting = true;
                Vector3 localColliderPos = transform.InverseTransformPoint(other.transform.position);
                localColliderPos = _portalTarget.rotation * localColliderPos;

                localColliderPos = new Vector3(-localColliderPos.x, localColliderPos.y, -localColliderPos.z);

                other.transform.position = _portalTarget.position + localColliderPos;
                other.transform.rotation = _portalTarget.rotation * Quaternion.Euler(0, 180, 0) * (Quaternion.Inverse(transform.rotation) * other.transform.rotation);

                Debug.Log("WARP");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawLine(transform.position, _portalTarget.position);
    }
}
