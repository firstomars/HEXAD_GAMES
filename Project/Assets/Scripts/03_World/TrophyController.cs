using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrophyController : MonoBehaviour
{
    [SerializeField] private Transform petTrophyPos;
    private PlayerController PlayerController;

    [System.Serializable]
    public class Trophy
    {
        [SerializeField] public string trophyName;
        [SerializeField] private string trophyConditions;
        [SerializeField] public bool isTrophyCreated;
        [SerializeField] public bool isConditionMet;
        [SerializeField] public GameObject trophyPrefab;
        [SerializeField] public Transform trophyPosition;
    }

    [SerializeField] Trophy[] trophies;

    // Start is called before the first frame update
    void Start()
    {
        //get data from player prefs

        PlayerController = GameManager.Instance.player.GetComponent<PlayerController>();
        PlayerController.trophyCabinetPosition = petTrophyPos;
    }

    // Update is called once per frame
    void Update()
    {
        //condition check
        //TrophyConditionCheck();

        ////instantiate
        //if (Input.GetKeyDown(KeyCode.Space)) InstantiateTrophy();
    }

    public void TrophyConditionCheck()
    {
        //replace with actual trophy conditions
        if (!trophies[0].isConditionMet && IsTrophyOneConditionMet())
            trophies[0].isConditionMet = true;

        if (!trophies[1].isConditionMet && Input.GetKeyDown(KeyCode.Alpha2))
            trophies[1].isConditionMet = true;

        if (!trophies[2].isConditionMet && Input.GetKeyDown(KeyCode.Alpha3))
            trophies[2].isConditionMet = true;

        if (!trophies[3].isConditionMet && Input.GetKeyDown(KeyCode.Alpha4))
            trophies[3].isConditionMet = true;

        if (!trophies[4].isConditionMet && Input.GetKeyDown(KeyCode.Alpha5))
            trophies[4].isConditionMet = true;
    }

    public void InstantiateTrophy()
    {
        foreach (Trophy trophy in trophies)
        {
            if (trophy.isConditionMet && !trophy.isTrophyCreated)
            {
                Instantiate(trophy.trophyPrefab, trophy.trophyPosition);
                trophy.isTrophyCreated = true;
            }
        }
        //store data in player prefs
    }

    private bool IsTrophyOneConditionMet()
    {
        if (PlayerController.PlayerStatistics.GetHrsSleptNightOne() > 7) return true;
        
        return false;
    }
}
