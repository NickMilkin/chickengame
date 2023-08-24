using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public string nextScene;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            SceneManager.LoadScene(nextScene);
        }
    }
}
