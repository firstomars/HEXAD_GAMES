using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.AssignPlayer();
        GameManager.Instance.AssignWorld();
    }
}