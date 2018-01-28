using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinGUI : MonoBehaviour {

    public Text nameText;
	public int n = 10;

    public void GoToMenu() {
        float time = Player.instance.playTime;
        string name = nameText.text;

		for (int i = 1; i <= n; i++) {
            float oldTime = PlayerPrefs.GetFloat("Time" + i, Mathf.Infinity);
            if (oldTime > time) {
                string oldName = PlayerPrefs.GetString("Name" + i, null);
                PlayerPrefs.SetFloat("Time" + i, time);
                PlayerPrefs.SetString("Name" + i, name);
				if (oldName == null)
					break;
                time = oldTime;
                name = oldName;
            }
        }
        SceneManager.LoadScene("Menu");
    }

}
