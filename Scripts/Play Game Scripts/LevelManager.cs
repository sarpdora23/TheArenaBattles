using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int enemy_count_l1, enemy_count_l2, enemy_count_l3, enemy_count_l4;
    public int enemy_count_l5, enemy_count_l6, enemy_count_l7, enemy_count_l8,enemy_count_l9,enemy_count_l10;
    private static LevelManager levelManager = null;

    public static LevelManager levelManager_instance
    {
        get
        {
            if (levelManager == null)
            {
                levelManager = new LevelManager();
            }

            return levelManager;
        }
    }

    private void OnEnable()
    {
        levelManager = this;
    }

    public void KilledEnemy()
    {
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            enemy_count_l1--;
        }
        else if (SceneManager.GetActiveScene().name == "Level2")
        {
            enemy_count_l2--;
        }
        else if (SceneManager.GetActiveScene().name == "Level3")
        {
            enemy_count_l3--;
        }
        else if (SceneManager.GetActiveScene().name == "Level4")
        {
            enemy_count_l4--;
        }
        else if (SceneManager.GetActiveScene().name == "Level5")
        {
            enemy_count_l5--;
        }
        else if (SceneManager.GetActiveScene().name == "Level6")
        {
            enemy_count_l6--;
        }
        else if (SceneManager.GetActiveScene().name == "Level7")
        {
            enemy_count_l7--;
        }
        else if (SceneManager.GetActiveScene().name == "Level8")
        {
            enemy_count_l8--;
        }
        else if (SceneManager.GetActiveScene().name == "Level9")
        {
            enemy_count_l9--;
        }
        else if (SceneManager.GetActiveScene().name == "Level10")
        {
            enemy_count_l10--;
        }
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            if (enemy_count_l1 == 0)
            {
                SceneManager.LoadScene("Level2");
            }
        }
        else if (SceneManager.GetActiveScene().name == "Level2")
        {
            if (enemy_count_l2 == 0)
            {
                SceneManager.LoadScene("Level3");
            }
        }
        else if (SceneManager.GetActiveScene().name == "Level3")
        {
            if (enemy_count_l3 == 0)
            {
                SceneManager.LoadScene("Level4");
            }
        }
        else if (SceneManager.GetActiveScene().name == "Level4")
        {
            if (enemy_count_l4 == 0)
            {
                SceneManager.LoadScene("Level5");
            }
        }
        else if (SceneManager.GetActiveScene().name == "Level5")
        {
            if (enemy_count_l5 == 0)
            {
                SceneManager.LoadScene("Level6");
            }
        }
        else if (SceneManager.GetActiveScene().name == "Level6")
        {
            if (enemy_count_l6 == 0)
            {
                SceneManager.LoadScene("Level7");
            }
        }
        else if (SceneManager.GetActiveScene().name == "Level7")
        {
            if (enemy_count_l7 == 0)
            {
                SceneManager.LoadScene("Level8");
            }
        }
        else if (SceneManager.GetActiveScene().name == "Level8")
        {
            if (enemy_count_l8 == 0)
            {
                SceneManager.LoadScene("Level9");
            }
        }
        else if (SceneManager.GetActiveScene().name == "Level9")
        {
            if (enemy_count_l9 == 0)
            {
                SceneManager.LoadScene("Level10");
            }
        }
        else if (SceneManager.GetActiveScene().name == "Level10")
        {
            if (enemy_count_l10 == 0)
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }

    public void Lose()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
