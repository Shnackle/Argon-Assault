using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    private int score = 0;
    TMP_Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TMP_Text>();
        scoreText.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ModifyScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
    }
}
