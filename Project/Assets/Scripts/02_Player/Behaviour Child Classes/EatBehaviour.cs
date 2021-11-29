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
        Debug.Log("EatBehaviour Start called - press A to test update");
        UIManager.UIManagerInstance.CurrentBehaviour = this;
        SetUI("kitchen");
        base.StartBehaviour();
    }

    public override void RunBehaviour()
    {
        //Debug.Log("EatBehaviour Update called");

        if (Input.GetKeyDown(KeyCode.A))
            Debug.Log("key A has been pressed");

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
        PlayerController.PlayerAnimations.Eat();
        
        
        uint hoursSinceLastMeal = CalculateHoursPerLastMeal();
        //Debug.Log(hoursSinceLastMeal);

        if (timeLastEaten == -1 || (hoursSinceLastMeal > 3 && timesEatenToday < 4)) EatSuccessful("food");
        else Debug.Log("You've eaten in the last 4 hours OR you've already eaten 3 meals today!");
    }

    public override void EatJunkFood()
    {
        //PlayerController.PlayerAnimations.EatJunkFood();

        uint hoursSinceLastMeal = CalculateHoursPerLastMeal();
       
        if (timeLastEaten == -1 || (hoursSinceLastMeal > 3 && timesEatenToday < 4)) EatSuccessful("junkfood");
        else Debug.Log("You've eaten in the last 4 hours OR you've already eaten 3 meals today!");
    }

    private void EatSuccessful(string foodType)
    {
        if (foodType == "junkfood") PlayerController.PlayerStatistics.JunkFoodEatenStatsImpact();
        else PlayerController.PlayerStatistics.FoodEatenStatsImpact();
        
        timeLastEaten = PlayerController.TimeController.GetGameTime();
        timesEatenToday += 1;
    }

    private uint CalculateHoursPerLastMeal()
    {
        return (uint)(PlayerController.TimeController.GetGameTime() - timeLastEaten);
    }
}