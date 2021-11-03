using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourDebugUI : MonoBehaviour
{
    [SerializeField] private GameObject gymDebugUI;
    [SerializeField] private GameObject kitchenDebugUI;
    //add more

    // Start is called before the first frame update
    void Start()
    {
        gymDebugUI.SetActive(false);
        kitchenDebugUI.SetActive(false);
    }

    public void SetDebugUI(string room = default)
    {
        switch (room)
        {
            case "gym":
                Debug.Log("player in gym");
                gymDebugUI.SetActive(true);
                kitchenDebugUI.SetActive(false);
                break;

            case "kitchen":
                Debug.Log("player in kitchen");
                gymDebugUI.SetActive(false);
                kitchenDebugUI.SetActive(true);
                break;

            default:
                Debug.Log("player in house");
                gymDebugUI.SetActive(false);
                kitchenDebugUI.SetActive(false);
                break;
        }
    }

    public void SetGymDebugUI(bool value)
    {
        gymDebugUI.SetActive(value);
    }
}
