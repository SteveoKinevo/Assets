using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    public static LevelManager Instance { get; set; }

    public float timer = 10f;
    public Vector3 spawnPosition;
    public Transform playerTransform;
    int hitpoint = 3;
    int score = 0;
    bool paused = false;

    public Text hitpoint_text;
    public Text timer_text;
    public Text score_text;

    void Awake()
    {
        Instance = this;
        hitpoint_text.text = "Lives: " + hitpoint.ToString();
        timer_text.text = ((int)timer).ToString() + " seconds";
        score_text.text = "Score: " + score.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        if (playerTransform.position.y < -15)
        {
            playerTransform.position = spawnPosition;
            hitpoint--;
            hitpoint_text.text = "Lives: " + hitpoint.ToString();
            if (hitpoint <= 0)
            {
                Debug.Log("dead");
                SceneManager.LoadScene("menu");
            }
        }

        if (!paused)
        {
            //check timer for end
            this.timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer_text.text = "Game Over!";
                timer = 0;
                paused = true;
                StartCoroutine(pauseIt(1));
                //KillPlayer();
                //Time.timeScale = 0f;       //interferes with coroutine          

            }
            else
                timer_text.text = timer.ToString("f0") + " seconds";
        }
            
    }

    IEnumerator pauseIt(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene("menu");
    }

    public void CoinUp()
    {
        score+=1;
        score_text.text = "Score: " + score.ToString();
    }

    public void KillPlayer()
    {
        Destroy(playerTransform.gameObject);
    }

    public void Win()
    {
        //obj.SetActive(false);
        Debug.Log("Victory");
        SceneManager.LoadScene("menu");
                

    }
}
