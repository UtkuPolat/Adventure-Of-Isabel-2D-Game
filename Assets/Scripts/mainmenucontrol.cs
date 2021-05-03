using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mainmenucontrol : MonoBehaviour
{

    GameObject levels, locks;
    void Start()
    {
        //PlayerPrefs.DeleteAll();

        levels = GameObject.Find("Levels");
        locks = GameObject.Find("Locks");

        for(int i = 0; i < levels.transform.childCount; i++)
        {
            levels.transform.GetChild(i).gameObject.SetActive(false);
        }


        for (int i = 0; i < locks.transform.childCount; i++)
        {
            locks.transform.GetChild(i).gameObject.SetActive(false);
        }





        for (int i=0; i < PlayerPrefs.GetInt("whichlevel"); i++)
        {
            levels.transform.GetChild(i).GetComponent<Button>().interactable = true;
        }
    }
    public void buttonselect(int selectbutton)
    {
        if (selectbutton == 1)
        {
            SceneManager.LoadScene(1);
        }
        else if (selectbutton == 2)
        {

            for (int i = 0; i < locks.transform.childCount; i++)
            {
                locks.transform.GetChild(i).gameObject.SetActive(true);
            }
            for (int i = 0; i < levels.transform.childCount; i++)
            {
                levels.transform.GetChild(i).gameObject.SetActive(true);
            }

            for (int i = 0; i < PlayerPrefs.GetInt("whichlevel"); i++)
            {
                locks.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        else if (selectbutton == 3)
        {
            Application.Quit();
        }

    }
    public void levelsbutton(int newlevel)
    {
        SceneManager.LoadScene(newlevel);
    }


}
