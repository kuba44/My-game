using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;

    [SerializeField] float timeToLoad = 2f;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator LoadingNextScene(string nextLevel)
    {
        Time.timeScale = 0.5f;

        yield return new WaitForSecondsRealtime(timeToLoad);

        Time.timeScale = 1f;

        SceneManager.LoadScene(nextLevel);
    }

}
