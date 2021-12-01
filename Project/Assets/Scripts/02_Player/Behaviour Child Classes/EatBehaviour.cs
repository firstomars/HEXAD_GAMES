using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatBehaviour : Behaviour
{
    private int timeLastEaten = -1;
    private int timesEatenToday = 0;

    public EatBehaviour(PlayerController playerController) : base(playerController)
    {
        PlayerController = playerController;
    }

    public override void StartBehaviour()
    {
        Debug.Log("EatBehaviour Start called");

        UIManager = UIManager.UIManagerInstance;
        DialogueManager = DialogueManager.DialogueManagerInstance;
        PlayerAnimations = PlayerController.PlayerAnimations;
        PlayerStatistics = PlayerController.PlayerStatistics;
        //TimeController = PlayerController.TimeController; - currently unused

        UIManager.CurrentBehaviour = this;
        DialogueManager.CurrentBehaviour = this;

        SetUI("kitchen");

        base.StartBehaviour();
    }

    public override void RunBehaviour()
    {
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

        if (timeLastEaten == -1 || (hoursSinceLastMeal > 3 && timesEatenToday < 4)) EatSuccessful("food");
        else AlreadyEaten();
    }

    public override void EatJunkFood()
    {
        uint hoursSinceLastMeal = CalculateHoursPerLastMeal();

        if (timeLastEaten == -1 || (hoursSinceLastMeal > 3 && timesEatenToday < 4)) EatSuccessful("junkfood");
        else AlreadyEaten();
    }

    private void EatSuccessful(string foodType)
    {
        if (foodType == "junkfood")
        {
            PlayerController.ActivateFoodObj("unhealthy", true);
            PlayerAnimations.EatJunkFood();
            PlayerStatistics.JunkFoodEatenStatsImpact();
        }
        else
        {
            PlayerController.ActivateFoodObj("healthy", true);
            PlayerAnimations.Eat();
            PlayerStatistics.FoodEatenStatsImpact();
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
        DialogueManager.PetConversation("Kitchen");
    }

    private void AlreadyEaten()
    {
        Debug.Log("You've eaten in the last 4 hours OR you've already eaten 3 meals today!");
        DialogueManager.PetConversation("AlreadyEaten");
    }
}