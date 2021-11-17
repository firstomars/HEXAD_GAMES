using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sandbox.Omar.SliderTesting
{
    
    
    public class SliderController : MonoBehaviour
    {
        private float currentHealth = 100;
        private float maxHealth = 100;
        [SerializeField] private Slider spiritSlider;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                currentHealth -= 3;


            if (Input.GetKeyDown(KeyCode.A))
                currentHealth += 10;

            spiritSlider.value = CalculateHealth();
        }

        private float CalculateHealth()
        {
            return currentHealth / maxHealth;
        }
    }

}
