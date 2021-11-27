using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseWaypoints : MonoBehaviour
{
    private PlayerController PlayerController;
    [SerializeField] public Transform[] waypoints;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerController = GameManager.Instance.player.GetComponent<PlayerController>();
        PlayerController.HouseWaypoints = this;
    }
}
