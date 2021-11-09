using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Transform playerStartPosition;

    private void Start()
    {
        GameManager.Instance.AssignPlayer(playerStartPosition);
        GameManager.Instance.AssignWorld();
    }
}