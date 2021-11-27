using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderWaypoints : MonoBehaviour
{
    private PlayerController PlayerController;
    [SerializeField] public Transform[] wanderWaypoints;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerController = GameManager.Instance.player.GetComponent<PlayerController>();
        PlayerController.WanderWaypoints = this;
    }

    public void SetWanderWaypoints()
    {
        Debug.Log("Set waypoints called");
        foreach (Transform waypoint in wanderWaypoints)
        {
            var collider = waypoint.GetComponent<SphereCollider>();
            collider.enabled = !collider.enabled;
        }
    }
}
