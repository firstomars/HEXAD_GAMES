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
    [SerializeField] private GameObject flyoutButtonPanel;
    [SerializeField] private GameObject mainFlyoutPanel;
    [SerializeField] private GameObject walkFlyoutPanel;

    [Header("Flyout Button Images")]
    [SerializeField] private Sprite activateFlyoutImage;
    [SerializeField] private Sprite deactivateFlyoutImage;

    [Header("UIButton Prefabs")]
    [SerializeField] private GameObject playerResponseButtonPrefab;
    [SerializeField] private GameObject colourSwatchButtonPrefab;

    [Header("Rooms List")]
    [SerializeField] private string[] rooms;

    [Header("Pet Colours")]
    [SerializeField] private string[] petColours;

    // Local class variables
    private bool mainFlyoutActivated = false;
    private bool walkFlyoutActivated = false;

    // Main flyout button pressed
    public void ActivateMainFlyoutMenu()
    {
        if (!mainFlyoutActivated)
        {
            mainFlyoutPanel.SetActive(true);
            mainFlyoutActivated = true;
            flyoutButtonPanel.transform.GetChild(0).GetComponentInChildren<Image>().sprite = deactivateFlyoutImage;
        }
        else
        {
            CloseAllFlyouts();
        }
    }

    // Walk flyout button pressed
    public void ActivateWalkFlyoutMenu()
    {
        if (!walkFlyoutActivated)
        {
            walkFlyoutPanel.SetActive(true);
            walkFlyoutActivated = true;
        }
        else
        {
            walkFlyoutPanel.SetActive(false);
            walkFlyoutActivated = false;
        }
    }

    private void CloseAllFlyouts()
    {
        if (walkFlyoutActivated)
        {
            walkFlyoutPanel.SetActive(false);
            walkFlyoutActivated = false;
        }
        mainFlyoutPanel.SetActive(false);
        mainFlyoutActivated = false;
        flyoutButtonPanel.transform.GetChild(0).GetComponentInChildren<Image>().sprite = activateFlyoutImage;
    }

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

        // This is not UI related
        playerMaterial.color = clickedButton.transform.GetChild(0).gameObject.GetComponent<Image>().color;

        //UI related
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

    /* Old funtion
    public void RoomButtonClicked()
    {
        string clickedButton = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().text;
        Debug.Log("Player selected " + clickedButton);

        // Not UI related
        MovePet(clickedButton);

        // UI related
        DestroyUIButtons(playerResponsePanel);
        playerResponsePanel.SetActive(false);
    }
    */
    public void RoomButtonClicked()
    {
        string clickedButton = EventSystem.current.currentSelectedGameObject.name;
        string selectedRoom = clickedButton.Substring(4);
        Debug.Log("Player selected " + clickedButton);

        // Not UI related
        MovePet(selectedRoom);

        // UI related
        CloseAllFlyouts();
    }

    // Not UI related
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
