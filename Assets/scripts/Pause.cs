using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

    public float timer;
    public bool ispuse;
    public bool guipuse;
	
	// Обновление состаяния паузы при нажатии
	void Update () {
        Time.timeScale = timer;
        if (Input.GetKeyDown(KeyCode.Escape) && ispuse == false)
        {
            ispuse = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && ispuse == true)
        {
            ispuse = false;
        }
        if (ispuse == true)
        {
            timer = 0;
            guipuse = true;

        }
        else if (ispuse == false)
        {
            timer = 1f;
            guipuse = false;

        }
    }

    // состояние вида паузы при активации и функции при нажатии
    public void OnGUI()
    {
        if (guipuse == true)
        {
            Cursor.visible = true;
            if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) - 150f, 150f, 45f), "Продолжить"))
            {
                ispuse = false;
                timer = 0;
                Cursor.visible = false;
            }
            if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) - 100f, 150f, 45f), "Рестарт"))
            {
                Application.LoadLevel(1);         
            }
            if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) - 50f, 150f, 45f), "Главное меню"))
            {
                Application.LoadLevel(0);
            }
            if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2), 150f, 45f), "Выход"))
            {
                Application.Quit();

            }
        }
    }
}
