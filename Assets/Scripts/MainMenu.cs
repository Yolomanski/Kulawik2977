using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
      public void Settings()
   {
    SceneManager.LoadSceneAsync(1);
   }

      public void LevelSelect()
   {
    SceneManager.LoadSceneAsync(2);
   }
         public void Level1()
   {
    SceneManager.LoadSceneAsync(3);
   }
   public void Exit()
   {
    Application.Quit();
   }
    public void Menu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
