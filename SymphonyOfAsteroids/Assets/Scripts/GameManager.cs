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
    
    [SerializeField] List<Spawner> asteroidSpawners;

    // I turned it into a list
    
    // [SerializeField] Spawner A;
    // [SerializeField] Spawner J;
    // [SerializeField] Spawner S;
    // [SerializeField] Spawner K;
    // [SerializeField] Spawner D;
    // [SerializeField] Spawner L;

    [System.Serializable]
    public class AsteroidTemplate
    {
        public float spawnTime;
        public int asteroidIndex; // 0 = A | 1 = S | 2 = D | J = 3 | K = 4 | L = 5
    }

    public List<AsteroidTemplate> asteroidTemplates;

    private float timeSinceLastSpawn;              // Time elapsed since the last asteroid spawn
    private int currentAsteroidTemplateIndex = 0;  // Index of the next asteroid template to use

    public float SONGTIME = 0;
    public float RUNTIME = 0;
    public float _songBpm = 60f;
    public float _secPerBeat;
    public float _songPosition;
    public float _songPositionInBeats;
    public float _dspSongTime;
    public float _firstBeatOffset = 1.5f;

    public int currentScore;
    public int scorePerNote = 100;
    public int scorePerGoodNote = 125;
    public int scorePerPerfectNote = 150;

    public int currentCombo;
    public int comboTracker;
    public int[] comboThresholds; 

    public TMP_Text scoreText; 
    public TMP_Text comboText; 

    public float totalNotes;
    public float normalHits;
    public float goodHits;
    public float perfectHits;
    public float missedHits;

    public GameObject resultsScreen;
    public TMP_Text finalScoreText, percentHitText, rankText, normalsText, goodsText, perfectsText, missesText;

    void Start()
    {
        instance = this;

        scoreText.text = "Score: 0";
        currentCombo = 1;

        totalNotes = asteroidTemplates.Count;

        _secPerBeat = 60f / _songBpm;
        _dspSongTime = (float)AudioSettings.dspTime;

        startPlaying = true;
        theBS.hasStarted = true;

        track.Play();

        StartSpawningAsteroids();

        // A.SpawnAsteroid();
        // J.SpawnAsteroid();
    }

    void Update()
    {
        _songPosition = (float)(AudioSettings.dspTime - _dspSongTime - _firstBeatOffset);
        _songPositionInBeats = _songPosition / _secPerBeat;

        //counting down to end of track
        RUNTIME = SONGTIME - ((int)_songPosition);

        //A.SpawnAsteroid();
        //S.SpawnAsteroid();
        //D.SpawnAsteroid();
        //J.SpawnAsteroid();
        //K.SpawnAsteroid();
        //L.SpawnAsteroid();
            //everything you need to call to spawn asteroids

        // if (RUNTIME % 5 == 0)
        // {
        //     K.SpawnAsteroid(); //chain ? note (discovered by accident)
        // }

        // Update timeSinceLastSpawn
        timeSinceLastSpawn += Time.deltaTime;

        // Check if it's time to spawn the next asteroid based on the current template
        if (currentAsteroidTemplateIndex < asteroidTemplates.Count)
        {
            if (timeSinceLastSpawn >= asteroidTemplates[currentAsteroidTemplateIndex].spawnTime)
            {
                // Spawn the asteroid using the selected spawner
                int asteroidIndex = asteroidTemplates[currentAsteroidTemplateIndex].asteroidIndex;
                if (asteroidIndex >= 0 && asteroidIndex < asteroidSpawners.Count)
                {
                    asteroidSpawners[asteroidIndex].SpawnAsteroid();
                }

                // Move to the next template
                currentAsteroidTemplateIndex++;
                timeSinceLastSpawn = 0f;
            }
        }

        // Results Screen
        if(RUNTIME == 0 && !resultsScreen.activeInHierarchy)
        {
            resultsScreen.SetActive(true);

            normalsText.text = "" + normalHits;
            goodsText.text = goodHits.ToString();
            perfectsText.text = perfectHits.ToString();
            missesText.text = missedHits.ToString();

            float totalHit = normalHits + goodHits + perfectHits;
            float percentHit = (totalHit / totalNotes) * 100f;

            percentHitText.text = percentHit.ToString("F1") + "%";

            string rankVal = "F";

            if(percentHit > 40)
            {
                rankVal = "D";
            }
            if(percentHit > 55)
            {
                rankVal = "C";
            }
            if(percentHit > 70)
            {
                rankVal = "B";
            }
            if(percentHit > 85)
            {
                rankVal = "A";
            }
            if(percentHit > 95)
            {
                rankVal = "S";
            }
            if(percentHit > 100)
            {
                rankVal = "SS";
            }

            rankText.text = rankVal;

            finalScoreText.text = currentScore.ToString();

        }
    }

    void StartSpawningAsteroids()
    {
        // Make sure the asteroid spawners list is not empty
        if (asteroidSpawners.Count == 0)
        {
            Debug.LogError("No asteroid spawners assigned");
            return;
        }

        // Reset the currentAsteroidTemplateIndex and timeSinceLastSpawn
        currentAsteroidTemplateIndex = 0;
        timeSinceLastSpawn = 0f;

        // Start spawning asteroids based on their order in the list
        if (asteroidTemplates.Count > 0)
        {
            if (asteroidTemplates[currentAsteroidTemplateIndex].asteroidIndex >= 0 &&
                asteroidTemplates[currentAsteroidTemplateIndex].asteroidIndex < asteroidSpawners.Count)
            {
                asteroidSpawners[asteroidTemplates[currentAsteroidTemplateIndex].asteroidIndex].SpawnAsteroid();
            }
            currentAsteroidTemplateIndex = 1;
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

        normalHits++;
    }

    public void GoodHit()
    {
        currentScore += scorePerGoodNote * currentCombo;
        NoteHit();

        goodHits++;
    }

    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote * currentCombo;
        NoteHit();

        perfectHits++;
    }

    public void NoteMissed()
    {
        Debug.Log("Missed Note");

        currentCombo = 1;
        comboTracker = 0; 

        comboText.text = "Combo: x" + currentCombo;

        missedHits++;
    }
}