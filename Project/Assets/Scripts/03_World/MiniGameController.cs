using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameController : MonoBehaviour
{
    [SerializeField] private Transform miniGamePos;

    // Start is called before the first frame update
    void Start()
    {
        PlayerController PlayerController = GameManager.Instance.player.GetComponent<PlayerController>();
        PlayerController.miniGamePos = miniGamePos;
    }
}
