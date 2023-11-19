using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int hp;
    public int currentLevel;
    bool hasWon;
    public List<string> levels;
    float targetTransitionScale;
    public Transform transition;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        var target = Vector3.one * targetTransitionScale;
        transition.localScale = Vector3.MoveTowards(transition.localScale, target, 60 * Time.deltaTime);
    }

    public void Win()
    {
        if (hasWon) return;
        hasWon = true;
        currentLevel++;
        targetTransitionScale = 40;
        Invoke("LoadNextScene", 1f);
    }

    void LoadNextScene()
    {
        var levelName = levels[currentLevel];
        SceneManager.LoadScene(levelName);
        hasWon = false;
        targetTransitionScale = 0;
    }

    public void Lose()
    {
        //hp--;
        //SceneManager.LoadScene(currentLevel);
        //if (hp == 0) 
        //{
        //    SceneManager.LoadScene(levels[0]);
        //}
    }
}
