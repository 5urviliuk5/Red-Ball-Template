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

    AudioSource source;
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip gameOverSound;

    void Start()
    {
        Application.targetFrameRate = 60;

        source = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);

        if (FindObjectsOfType<GameManager>().Length > 1)
        {
            Destroy(gameObject);
        }
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

        source.PlayOneShot(winSound);
    }

    void LoadNextScene()
    {
        var levelName = levels[currentLevel];
        SceneManager.LoadScene(levelName);
        hasWon = false;
        targetTransitionScale = 0;
        if (hp <= 0)
        {
            SceneManager.LoadScene(levels[0]);
        }
    }

    public void Lose()
    {
        hp--;
        if (hp > 0)
        {
            source.PlayOneShot(loseSound);
            Invoke("LoadNextScene", 1f);
        }
        else 
        {
            source.PlayOneShot(gameOverSound);
            currentLevel = 0;
            hp = 3;
            Invoke("LoadNextScene", 1f);
        }
    }
}
