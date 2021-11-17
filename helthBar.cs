using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class helthBar : MonoBehaviour
{
   
    public Slider slider;

    public void SetHealth(int playerHealth) {
        slider.value = playerHealth; //healthbar slider fucntionality 
    }
}
