using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AI;
using UnityEngine.EventSystems;

//===
//OMAR FEEDBACK - namespace
//Add namespace to ensure that this class is not accessed globally while in sandbox mode
//e.g. namespace Sandbox.Craig.UITesting { //script in here }


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
    [SerializeField] private GameObject timeEntryPanel;
    [SerializeField] private GameObject flyoutButtonPanel;
    [SerializeField] private GameObject mainFlyoutPanel;
    [SerializeField] private GameObject walkFlyoutPanel;

    [Header("Pet Dialogue Text Panel")]
    [SerializeField] private GameObject petDialoguePanel;
    [SerializeField] private TextMeshProUGUI petDialogueText;

    [Header("Flyout Button Images")]
    [SerializeField] private Sprite activateFlyoutImage;
    [SerializeField] private Sprite deactivateFlyoutImage;

    [Header("UIButton Prefabs")]
    [SerializeField] private GameObject playerResponseButtonPrefab;
    [SerializeField] private GameObject colourSwatchButtonPrefab;
    [SerializeField] private GameObject timeEntryButtonPrefab;

    [Header("Rooms List")]
    [SerializeField] private string[] rooms;

    [Header("Pet Colours")]
    [SerializeField] private string[] colours;

    // Sub class to manage time inputs
    public class TimeBoxes
    {
        private System.DateTime time;
    }

    // Local class variables
    public string UIStoredPlayerResponse { get; private set; }
    public string UIStoredInputTime { get; private set; }
    private bool mainFlyoutActivated = false;
    private bool walkFlyoutActivated = false;
    private readonly List<string> hours = new List<string> { "00:00", "01:00", "02:00", "03:00", "04:00", "05:00", 
                                                            "06:00", "07:00", "08:00", "09:00", "10:00", "11:00", 
                                                            "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", 
                                                            "18:00", "19:00", "20:00", "21:00", "22:00", "23:00" };
    private int nextHoursPage = 0;

    // Main flyout button pressed
    public void ActivateMainFlyoutMenu()
    {
        if (!mainFlyoutActivated)
        {
            mainFlyoutPanel.SetActive(true);
            mainFlyoutActivated = true;
            flyoutButtonPanel.transform.GetChild(0).GetComponentInChildren<Image>().sprite = deactivateFlyoutImage;
            // Change size of flyout button to 155 x 155 to match the main flyout buttons
            RectTransform flyoutBtnImage = flyoutButtonPanel.transform.GetChild(0).GetComponentInChildren<Image>().rectTransform;
            flyoutBtnImage.sizeDelta = new Vector2(155, 155);
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

    // When the player clicks a button call the Seek behaviour to move the pet
    public void RoomButtonClicked()
    {
        // Get the object name of the clicked button
        // Buttons are named Btn_ so get the name after the prefix
        string clickedButton = EventSystem.current.currentSelectedGameObject.name.Substring(4);

        // Call seek behaviour with destination
        MovePet(clickedButton);

        // UI related
        CloseAllFlyouts();
    }

    // Closes all flyouts and resets the flyout UI
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
        // Change size of flyout button to 200 x 200 to match the main flyout outline
        RectTransform flyoutBtnImage = flyoutButtonPanel.transform.GetChild(0).GetComponentInChildren<Image>().rectTransform;
        flyoutBtnImage.sizeDelta = new Vector2(200, 200);
    }

    // Non overload method to be used for displaying pet dialogue
    public void EnablePetDialogueText(string petDialogue)
    {
        petDialoguePanel.SetActive(true);
        petDialogueText.text = petDialogue;
    }

    // Overload method to be used for displaying pet tips
    public void EnablePetDialogueText(string petDialogue, int timeToDisplay)
    {
        petDialoguePanel.SetActive(true);
        petDialogueText.text = petDialogue;
        StartCoroutine(DisableUIElementsAfterSeconds(timeToDisplay, new[] { petDialoguePanel }));
    }

    // Coroutine used to disable multiple UI elements after a set period of time
    IEnumerator DisableUIElementsAfterSeconds(int timeToDisplay, GameObject[] UIElements)
    {
        yield return new WaitForSeconds(timeToDisplay);
        foreach(GameObject UIElement in UIElements)
        {
            UIElement.SetActive(false);
        }
    }

    //===
    //OMAR FEEDBACK - display colour selections + pet colour selections functions
    //where is the best place for these functions to go?
    //separate script entirely that is called during the introduction period of the game?
    //and then accesses the UI script to activate panels etc.?

    // Player colour selection
    // Only used when changing the pets colour
    public void DisplayColourSelections()
    {
        Debug.Log("Lets pick a colour");
        // Display pet colour swatch selection
        petColourPanel.SetActive(true);
        // Create a button for each pet colour listed in the UIManager
        foreach(string colour in colours)
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

        // UI related
        DestroyUIButtons(petColourPanel);
        StartCoroutine(DisableUIElementsAfterSeconds(0, new[] { petColourPanel, petDialoguePanel }));
    }

    // Player text response selection
    // Accepts an array of strings to display text responses the player can choose
    public void DisplayPlayerResponses(string[] responses)
    {
        // If the passed in array is too long then exit
        if (responses.Length > 4)
            return;
        // Enable the player response panel
        playerResponsePanel.SetActive(true);
        // Display a button for each response the player can make
        foreach(string response in responses)
        {
            GameObject newButton = Instantiate(playerResponseButtonPrefab, playerResponsePanel.transform);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = response;
            newButton.GetComponent<Button>().onClick.AddListener(StorePlayerResponse);
        }
    }

    // When the player clicks a response button the text response is stored in a public variable
    private void StorePlayerResponse()
    {
        UIStoredPlayerResponse = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        DestroyUIButtons(playerResponsePanel);
        StartCoroutine(DisableUIElementsAfterSeconds(0, new[] { playerResponsePanel, petDialoguePanel }));
    }

    // Used to reset a UI panel button options
    private void DestroyUIButtons(GameObject UIParent)
    {
        foreach (Transform child in UIParent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    // Time input controller
    // Used to input times for bed time and wake up time
    public void DisplayTimeEntry()
    {
        Debug.Log("Lets enter some time variables");
        // Enable the time entry panel
        timeEntryPanel.SetActive(true);
        PaginateHoursList(nextHoursPage);
    }

    // Pagination function to support scrolling through pages
    private void PaginateHoursList(int pageToFetch)
    {
        Debug.Log("Fetching page" + pageToFetch);
        int pageSize = 6;
        // Set how many pages to skip based on current page
        int pageCount = pageToFetch * pageSize;
        IEnumerable<string> currentPage = hours.Skip(pageCount).Take(pageSize);
        // Get the time button panel and create the new page of buttons
        GameObject timeButtonPanel = timeEntryPanel.transform.GetChild(0).gameObject;
        DestroyUIButtons(timeButtonPanel);
        foreach (string hour in currentPage)
        {
            GameObject newButton = Instantiate(timeEntryButtonPrefab, timeButtonPanel.transform);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = hour;
            newButton.GetComponent<Button>().onClick.AddListener(StoreTimeEntry);
        }
    }

    // Helper function to set current page for pagination
    private void SetNextHoursPage(bool direction)
    {
        if (direction)
            nextHoursPage++;
        else
            nextHoursPage--;
        Debug.Log("Current page is " + nextHoursPage);
        if (nextHoursPage > 3) nextHoursPage = 0;
        if (nextHoursPage < 0) nextHoursPage = 3;
    }

    // Public button function to move forward a page
    public void NextPageHoursList()
    {
        PaginateHoursList(nextHoursPage);
        SetNextHoursPage(true);
    }

    // Public button function to move back a page
    public void PreviousPageHoursList()
    {
        PaginateHoursList(nextHoursPage);
        SetNextHoursPage(false);
    }

    //When the player clicks a time entry button the time is stored in a public variable
    public void StoreTimeEntry()
    {
        Debug.Log("Player stored time");
        UIStoredInputTime = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        Debug.Log("Player stored " + UIStoredInputTime);
    }

    //===
    //OMAR FEEDBACK - MovePet to be broken out across UI & Seek.cs connections
    //lets get some time in on Monday to break this out
    //I've got some foundation logic working at the moment which we can lean on

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

    private void Start()
    {
        DisplayTimeEntry();
    }

}
