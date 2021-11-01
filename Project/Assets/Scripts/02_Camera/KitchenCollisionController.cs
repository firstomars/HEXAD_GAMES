using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenCollisionController : MonoBehaviour
{
    CameraManager CameraManager;

    // Start is called before the first frame update
    private void Start()
    {
        CameraManager = GetComponentInParent<CameraManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            CameraManager.SetPlayerPosition("kitchen");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            CameraManager.SetPlayerPosition();
    }
}