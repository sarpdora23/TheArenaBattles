using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuButton : MonoBehaviour
{
   public void PlayButton()
   {
      SceneManager.LoadScene("Level1");
   }

   public void CreditButton()
   {
      SceneManager.LoadScene("Credits");
   }

   public void BackButton()
   {
      SceneManager.LoadScene("MainMenu");
   }
}
