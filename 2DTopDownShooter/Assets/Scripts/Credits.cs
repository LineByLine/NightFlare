using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour {

    public GameObject MainMenuContainer;
    public GameObject CreditsContainer;

    public void goToCredits()
    {
        MainMenuContainer.SetActive(false);
        CreditsContainer.SetActive(true);
    }
}
