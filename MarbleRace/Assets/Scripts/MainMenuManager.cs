using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

	public void LoadScene (string scene) {
		Time.timeScale = 1;
		SceneManager.LoadScene(scene);
	}

	public void RestartScene(){
		Time.timeScale = 1;
		Scene current = SceneManager.GetActiveScene();
		SceneManager.LoadScene(current.name);
	}
}
