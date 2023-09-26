using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public Animator transition;
    public float transitionTime = 1f;
    #region Singleton
    public static LevelLoader instance;

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel(string levelName)
    {
        StartCoroutine(LoadNamedLevel(levelName));
    }

    IEnumerator LoadNamedLevel(string levelName)
    {
        transition.SetTrigger("FadeIn");
        // Start transition animation
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelName);
    }

}
