using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class UIScript : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] private Transform loungeTransform;
    [SerializeField] private Transform gymTransform;
    [SerializeField] private Transform kitchenTransform;
    [SerializeField] private Transform bedroomTransform;
    [SerializeField] private NavMeshAgent playerObject;
    [SerializeField] private Material playerMaterial;

    [Header("UI Panels")]
    [SerializeField] private GameObject playerResponsePanel;
    [SerializeField] private GameObject petColourPanel;

    [Header("UIButton Prefabs")]
    [SerializeField] private GameObject playerResponseButtonPrefab;
    [SerializeField] private GameObject colourSwatchButtonPrefab;

    [Header("Rooms List")]
    [SerializeField] private string[] rooms;

    [Header("Pet Colours")]
    [SerializeField] private string[] petColours;



    // Player colour selection
    public void DisplayColourSelections()
    {
        Debug.Log("Lets pick a colour");
        // Display pet colour swatch selection
        petColourPanel.SetActive(true);
        foreach(string colour in petColours)
        {
            GameObject newButton = Instantiate(colourSwatchButtonPrefab, petColourPanel.transform);
            ColorUtility.TryParseHtmlString(colour, out Color col);
            newButton.transform.GetChild(0).GetComponentInChildren<Image>().color = col;
            newButton.GetComponent<Button>().onClick.AddListener(PetColourSelection);
        }
    }

    private void PetColourSelection()
    {
        GameObject clickedButton = EventSystem.current.currentSelectedGameObject;
        playerMaterial.color = clickedButton.transform.GetChild(0).gameObject.GetComponent<Image>().color;
        DestroyUIButtons(petColourPanel);
        petColourPanel.SetActive(false);
    }

    // Player text response selection
    public void DisplayPlayerResponseSelection()
    {
        Debug.Log("Player clicked walk button");
        // Display the room selection UI
        playerResponsePanel.SetActive(true);
        foreach(string room in rooms)
        {
            GameObject newButton = Instantiate(playerResponseButtonPrefab, playerResponsePanel.transform);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = room;
            newButton.GetComponent<Button>().onClick.AddListener(RoomButtonClicked);
        }
    }

    private void RoomButtonClicked()
    {
        string clickedButton = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().text;
        Debug.Log("Player selected " + clickedButton);
        MovePet(clickedButton);
        DestroyUIButtons(playerResponsePanel);
        playerResponsePanel.SetActive(false);
    }

    private void MovePet(string destination)
    {
        Vector3 petDestination = playerObject.transform.position;
        switch (destination)
        {
            case "Bedroom":
                Debug.Log("Pet moving to " + destination);
                petDestination = new Vector3(bedroomTransform.position.x, bedroomTransform.position.y, bedroomTransform.position.z);
                break;
            case "Lounge":
                Debug.Log("Pet moving to " + destination);
                petDestination = new Vector3(loungeTransform.position.x, loungeTransform.position.y, loungeTransform.position.z);
                break;
            case "Kitchen":
                Debug.Log("Pet moving to " + destination);
                petDestination = new Vector3(kitchenTransform.position.x, kitchenTransform.position.y, kitchenTransform.position.z);
                break;
            case "Gym":
                Debug.Log("Pet moving to " + destination);
                petDestination = new Vector3(gymTransform.position.x, gymTransform.position.y, gymTransform.position.z);
                break;
        }
        playerObject.SetDestination(petDestination);
    }

    private void DestroyUIButtons(GameObject UIParent)
    {
        foreach (Transform child in UIParent.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
