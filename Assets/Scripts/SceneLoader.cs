using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public Animator transition;
    public float transitionTime = 1;

    public string nextScene;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.tag == "Player") {
                StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));

                IEnumerator LoadLevel(int levelIndex){
                transition.SetTrigger("Start"); //Scene change causes trigger of transistion.
                yield return new WaitForSeconds(transitionTime);
                
                SceneManager.LoadScene(nextScene);
            }
        }
    }
}
