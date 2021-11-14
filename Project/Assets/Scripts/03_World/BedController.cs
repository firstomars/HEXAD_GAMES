using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedController : MonoBehaviour
{
    [SerializeField] private Transform petBedPos;

    // Start is called before the first frame update
    void Start()
    {
        PlayerController PlayerController = GameManager.Instance.player.GetComponent<PlayerController>();
        PlayerController.bed = petBedPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
