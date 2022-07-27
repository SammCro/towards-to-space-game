using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MapCanvas : MonoBehaviour
{

    [SerializeField] private List<GameObject> Levels;
    [SerializeField] GameObject gameController;

 

        
    void OnEnable()
    {

        Levels[1].GetComponent<Button>().interactable = true;

        try
        {
            switch (PlayerPrefs.GetInt("Level2"))
            {
                case 0:
                    Levels[2].GetComponent<Button>().interactable = false;
                    break;
                case 1:
                    Levels[2].GetComponent<Button>().interactable = true;

                    break;
            }
            switch (PlayerPrefs.GetInt("Level3"))
            {
                case 0:
                    Levels[3].GetComponent<Button>().interactable = false;
                    break;
                case 1:
                    Levels[3].GetComponent<Button>().interactable = true;

                    break;
            }
            switch (PlayerPrefs.GetInt("Level4"))
            {
                case 0:
                    Levels[4].GetComponent<Button>().interactable = false;
                    break;
                case 1:
                    Levels[4].GetComponent<Button>().interactable = true;

                    break;
            }
            switch (PlayerPrefs.GetInt("Level5"))
            {
                case 0:
                    Levels[5].GetComponent<Button>().interactable = false;
                    break;
                case 1:
                    Levels[5].GetComponent<Button>().interactable = true;

                    break;
            }
            switch (PlayerPrefs.GetInt("Level6"))
            {
                case 0:
                    Levels[6].GetComponent<Button>().interactable = false;
                    break;
                case 1:
                    Levels[6].GetComponent<Button>().interactable = true;

                    break;
            }
            switch (PlayerPrefs.GetInt("Level7"))
            {
                case 0:
                    Levels[7].GetComponent<Button>().interactable = false;
                    break;
                case 1:
                    Levels[7].GetComponent<Button>().interactable = true;

                    break;
            }
            switch (PlayerPrefs.GetInt("Level8"))
            {
                case 0:
                    Levels[8].GetComponent<Button>().interactable = false;
                    break;
                case 1:
                    Levels[8].GetComponent<Button>().interactable = true;

                    break;
            }
            switch (PlayerPrefs.GetInt("Level9"))
            {
                case 0:
                    Levels[9].GetComponent<Button>().interactable = false;
                    break;
                case 1:
                    Levels[9].GetComponent<Button>().interactable = true;

                    break;
            }
            switch (PlayerPrefs.GetInt("Level10"))
            {
                case 0:
                    Levels[10].GetComponent<Button>().interactable = false;
                    break;
                case 1:
                    Levels[10].GetComponent<Button>().interactable = true;

                    break;
            }
            switch (PlayerPrefs.GetInt("Level11"))
            {
                case 0:
                    Levels[11].GetComponent<Button>().interactable = false;
                    break;
                case 1:
                    Levels[11].GetComponent<Button>().interactable = true;

                    break;
            }
            switch (PlayerPrefs.GetInt("Level12"))
            {
                case 0:
                    Levels[12].GetComponent<Button>().interactable = false;
                    break;
                case 1:
                    Levels[12].GetComponent<Button>().interactable = true;

                    break;
            }
            switch (PlayerPrefs.GetInt("Level3"))
            {
                case 0:
                    Levels[13].GetComponent<Button>().interactable = false;
                    break;
                case 1:
                    Levels[13].GetComponent<Button>().interactable = true;

                    break;
            }
            switch (PlayerPrefs.GetInt("Level14"))
            {
                case 0:
                    Levels[14].GetComponent<Button>().interactable = false;
                    break;
                case 1:
                    Levels[14].GetComponent<Button>().interactable = true;

                    break;
            }
            switch (PlayerPrefs.GetInt("Level15"))
            {
                case 0:
                    Levels[15].GetComponent<Button>().interactable = false;
                    break;
                case 1:
                    Levels[15].GetComponent<Button>().interactable = true;

                    break;
            }


        }
        catch (Exception e)
        {

           

        }
    }

    

    public void LoadScener(int index)
    {
        SceneManager.LoadScene(index);
        if (gameController!=null)
        {
            gameController.GetComponent<BannerScript>().Destroyer();
        }
    }
}
