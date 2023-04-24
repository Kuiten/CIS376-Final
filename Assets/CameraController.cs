using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private int totalCollected;
    private int playerScore;
    private int AIScore;
    private Vector3 offset;
    public TMP_Text ScoreText;
    private GameObject Stage1;
    protected AIController AI;
    // Start is called before the first frame update
    void Start()
    {
        totalCollected = 0;
        playerScore = 0;
        AIScore = 0;
        offset = transform.position - player.transform.position;
        ScoreText.text = "Player Score: " + playerScore.ToString() + " \t\tTotal Collected: " + totalCollected.ToString() + " \t\tAI Score: " + AIScore.ToString();
        Stage1 = GameObject.Find("Stage1");
        AI = GameObject.Find("AI").GetComponent<AIController>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
        if(totalCollected >= 8 && Stage1.activeSelf)
        {
            Stage1.SetActive(false);
            AI.setStageSwitch();
        }else if(totalCollected >= 16){
            if(AIScore > playerScore){
                //load defeat screen
                SceneManager.LoadScene("Defeat", LoadSceneMode.Single);
            }else{
                //load victory screen
                SceneManager.LoadScene("Victory", LoadSceneMode.Single);
            }
        }
    }

    public void IncrementScore(bool isPlayer)
    {
        totalCollected += 1;
        if(isPlayer)
        {
            playerScore += 1;
        }
        else 
        {
            AIScore += 1;
        }
        ScoreText.text = "Player Score: " + playerScore.ToString() + " \t\tTotal Collected: " + totalCollected.ToString() + " \t\tAI Score: " + AIScore.ToString();
    }
    public int[] getScore(){
        int[] score = {playerScore, AIScore};
        // score[0] = playerScore;
        // score[1] = AIScore;
        return score;
    }
}
