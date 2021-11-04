using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathroomTrigger : RoomTrigger
{
    // Start is called before the first frame update
    void Start()
    {
        CameraManager = camMgrObj.GetComponent<CameraManager>();
        PlayerController = GameManager.Instance.player.GetComponent<PlayerController>();
        renderer = GetComponent<Renderer>();
        IsInRoom(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            IsInRoom(true);
            CameraManager.SetPlayerPosition("bathroom");
            PlayerController.SetPlayerPosition("bathroom");
            Debug.Log("I am in the bathroom");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            IsInRoom(false);
            CameraManager.SetPlayerPosition();
            PlayerController.SetPlayerPosition();
            Debug.Log("I am no longer in the bathroom");
        }
    }
}