using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AddOne : MonoBehaviour
{
   public Text numberText;
   public int number = 0;

   public void ButtonCLicked()
   {
      number++;
      numberText.text = number.ToString();
   }

}
