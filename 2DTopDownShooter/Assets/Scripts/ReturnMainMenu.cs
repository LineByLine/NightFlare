using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnMainMenu : MonoBehaviour {

    public GameObject MainMenuContainer;
    public GameObject CreditsContainer;

    public void returnToMenu()
    {
        MainMenuContainer.SetActive(true);
        CreditsContainer.SetActive(false);
    }
}
