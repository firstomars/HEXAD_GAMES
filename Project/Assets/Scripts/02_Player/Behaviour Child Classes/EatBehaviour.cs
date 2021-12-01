using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatBehaviour : Behaviour
{
    private int timeLastEaten = -1;
    private int timesEatenToday = 0;

    private bool hasBeenInKitchen = false;
    private bool isRoomUISet = false;

    public EatBehaviour(PlayerController playerController) : base(playerController)
    {
        PlayerController = playerController;
    }

    public override void StartBehaviour()
    {
        Debug.Log("EatBehaviour Start called - press A to test update");
        UIManager.UIManagerInstance.CurrentBehaviour = this;
        DialogueManager.DialogueManagerInstance.CurrentBehaviour = this;

        SetUI("kitchen");


        //if (!hasBeenInKitchen) DialogueManager.DialogueManagerInstance.PetConversation("Kitchen");
        //else SetUI("kitchen");


        base.StartBehaviour();
    }

    public override void RunBehaviour()
    {
        //if (DialogueManager.DialogueManagerInstance.currentConversationComplete) hasBeenInKitchen = true;

        //if (!isRoomUISet && hasBeenInKitchen)
        //{
        //    SetUI("kitchen");
        //    isRoomUISet = true;
        //}

        base.RunBehaviour();
    }

    public override void EndBehaviour()
    {
        Debug.Log("EatBehaviour End called");
        SetUI();
        base.EndBehaviour();
    }



    public override void EatFood()
    {      
        uint hoursSinceLastMeal = CalculateHoursPerLastMeal();
        //Debug.Log(hoursSinceLastMeal);

        if (timeLastEaten == -1 || (hoursSinceLastMeal > 3 && timesEatenToday < 4)) EatSuccessful("food");
        else
        {
            Debug.Log("You've eaten in the last 4 hours OR you've already eaten 3 meals today!");
            DialogueManager.DialogueManagerInstance.PetConversation("AlreadyEaten");
        }
    }

    public override void EatJunkFood()
    {
        uint hoursSinceLastMeal = CalculateHoursPerLastMeal();
       
        if (timeLastEaten == -1 || (hoursSinceLastMeal > 3 && timesEatenToday < 4)) EatSuccessful("junkfood");
        else
        {
            Debug.Log("You've eaten in the last 4 hours OR you've already eaten 3 meals today!");
            DialogueManager.DialogueManagerInstance.PetConversation("AlreadyEaten");
        }
    }

    private void EatSuccessful(string foodType)
    {
        if (foodType == "junkfood")
        {
            //PlayerController.PlayerAnimations.EatJunkFood();
            PlayerController.PlayerStatistics.JunkFoodEatenStatsImpact();
        }
        else
        {
            PlayerController.PlayerAnimations.Eat();
            PlayerController.PlayerStatistics.FoodEatenStatsImpact();
        }
        
        timeLastEaten = PlayerController.TimeController.GetGameTime();
        timesEatenToday += 1;
    }

    private uint CalculateHoursPerLastMeal()
    {
        return (uint)(PlayerController.TimeController.GetGameTime() - timeLastEaten);
    }

    public override void StartConversation()
    {
        DialogueManager.DialogueManagerInstance.PetConversation("Kitchen");
    }
}