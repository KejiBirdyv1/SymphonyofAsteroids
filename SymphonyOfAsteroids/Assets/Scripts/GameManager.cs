using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class GameManager : MonoBehaviour
{
    public AudioSource track;
    public bool startPlaying;
    public BeatScroller theBS;

    public static GameManager instance;

    public int currentScore;
    public int scorePerNote = 100;
    public int scorePerGoodNote = 125;
    public int scorePerPerfectNote = 150;

    public int currentCombo;
    public int comboTracker;
    public int[] comboThresholds; 

    public TMP_Text scoreText; 
    public TMP_Text comboText; 


    void Start()
    {
        instance = this;

        scoreText.text = "Score: 0";
        currentCombo = 1;
    }

    void Update()
    {
        if(!startPlaying)
        {
            if(Input.anyKeyDown)
            {
                startPlaying = true;
                theBS.hasStarted = true;

                track.Play();
            }
        }
    }

    public void NoteHit()
    {
        Debug.Log("Hit on Time");

        if(currentCombo - 1 < comboThresholds.Length)
        {
            comboTracker++;

            if(comboThresholds[currentCombo - 1] <= comboTracker)
            {
                comboTracker = 0;
                currentCombo++;
            }
        }

        comboText.text = "Combo: x" + currentCombo;
     
        // currentScore += scorePerNote * currentCombo;
        scoreText.text = "Score: " + currentScore; 
    }

    public void NormalHit()
    {
        currentScore += scorePerNote * currentCombo;
        NoteHit();
    }

    public void GoodHit()
    {
        currentScore += scorePerGoodNote * currentCombo;
        NoteHit();
    }

    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote * currentCombo;
        NoteHit();
    }

    public void NoteMissed()
    {
        Debug.Log("Missed Note");

        currentCombo = 1;
        comboTracker = 0; 

        comboText.text = "Combo: x" + currentCombo;
    }
}
