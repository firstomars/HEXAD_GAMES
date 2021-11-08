using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIScript : MonoBehaviour
{
    [SerializeField] private Transform loungeTransform;
    [SerializeField] private Transform gymTransform;
    [SerializeField] private Transform kitchenTransform;
    [SerializeField] private Transform bedroomTransform;
    [SerializeField] string Rooms;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Player clicked walked

    public void SeekRoomBehaviour()
    {

    }

    // Player colour selection
    public void DisplayColourSelections()
    {

    }

    // Player response selection
    public void DisplayPlayerResponseSelection()
    {
        Debug.Log("We are going to walk");
    }
}
