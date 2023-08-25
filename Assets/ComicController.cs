using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComicController : MonoBehaviour
{
    public GameObject[] images;
    public string nextScene;
    int activeIndex = 0;

    void Start() {
        images[0].SetActive(true);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            activeIndex += 1;
            if (activeIndex == images.Length) {
                SceneManager.LoadScene(nextScene);
            } else {
                images[activeIndex].SetActive(true);
            }
        }
    }
}
