using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

    // кнопка загрузки новой игры
    public void NewGameBtn()
    {
        Application.LoadLevel("start");
    }

    // кнопка загрузки новой игры
    public void ReastaGameBtn()
    {
            Application.LoadLevel("Lvl2"); 
    }

    // кнопка загрузки новой игры
    public void MenuGameBtn()
    {
        Application.LoadLevel("menu");
    }

    // кнопка закрытия игры
    public void CloseGame()
    {
        Application.Quit();
    }
}
