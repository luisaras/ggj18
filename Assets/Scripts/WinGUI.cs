using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinGUI : MonoBehaviour {

    public Text nameText;

    public void GoToMenu() {
        float time = Player.instance.playTime;
        string name = nameText.text;

        for (int i = 1; i <= 10; ++i) {
            float oldTime = PlayerPrefs.GetFloat("Time" + i, Mathf.Infinity);
            if (oldTime > time) {
                string oldName = PlayerPrefs.GetString("Name");
                PlayerPrefs.SetFloat("Time", time);
                PlayerPrefs.SetString("Name", name);
                time = oldTime;
                name = oldName;
            }
        }

        SceneManager.LoadScene("Menu");
    }

}
