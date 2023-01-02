using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using AndroidNativeCore;

public class GameController : MonoBehaviour
{
    private bool game = false;
    private int cur = 0;
    private int score = 0;
    private float vel = 4;
    private float accel = 0.05f;
    private float perfAccel = 0.025f;
    private float backSpeed = 0.01f;
    private int perfCount = 0;
    private int adsCounter = 0;

    public GameObject mainMenu;
    public GameObject gameOver;
    public GameObject vibButton1;
    public GameObject vibButton2;
    public TMP_Text scoreText;
    public TMP_Text coinText1;
    public TMP_Text coinText2;
    public Character character;

    public Animator score1;
    public Animator score2;
    public Animator perfect1;
    public Animator perfect2;
    public Animator perfect3;
    public Animator perfect4;

    private bool vib = true;

    public GameObject obstacle;
    private ObstacleMan [] obsMan = new ObstacleMan[5];
    private bool obsRight = true;
    public AdsMan adsMan;

    public Material mat;
    private int matState = 0;
    private float matProgresss = 0;

    private Color color1 = new Color(0, 0, .5f);
    private Color color2 = new Color(0, .5f, 0);
    private Color color3 = new Color(.5f, 0, 0);
    private Color color4 = new Color(0, .5f, .5f);
    private Color color5 = new Color(.5f, .5f, 0);
    private Color color6 = new Color(.5f, 0, .5f);

    private void Start()
    {
        if (PlayerPrefs.GetInt("vib") == 0)
        {
            vib = true;
            vibButton1.SetActive(true);
            vibButton2.SetActive(false);
        }
        else
        {
            vib = false;
            vibButton1.SetActive(false);
            vibButton2.SetActive(true);
        }

        resetGame();
        matState = PlayerPrefs.GetInt("material");
        matProgresss = PlayerPrefs.GetFloat("progress");
        updateBackground();
        perfCount = 0;
    }

    private void Update()
    {
        if(game)
        {
            if(obsMan[cur].gameObject.transform.position.y < -0.5f)
            {
                if (Mathf.Abs(character.getX() - obsMan[cur].gameObject.transform.position.x) < 0.1f)
                {
                    score += 2;
                    perfCount++;
                    character.playPerfect(perfCount);

                    if (perfCount == 1)
                    {
                        vel += perfAccel;
                        perfect1.Play("fadeText");
                    }
                    else if (perfCount == 2)
                    {
                        vel += perfAccel;
                        perfect2.Play("fadeText");
                    }
                    if (perfCount == 3)
                    {
                        vel += perfAccel;
                        perfect3.Play("fadeText");
                    }
                    if (perfCount == 4)
                    {
                        perfect4.Play("fadeText");
                        perfCount = 0;
                        vel -= accel;
                    }

                    score2.Play("fadeText");

                }
                else
                {
                    vel += accel;
                    perfCount = 0;
                    score++;
                    score1.Play("fadeText");
                }

                character.vel = vel;
                scoreText.text = score.ToString();

                obsMan[cur].gravity();
                obsMan[cur] = Instantiate(obstacle, new Vector2(obsRight ? Random.Range(0.5f, 1.25f) : Random.Range(-1.25f, -0.5f), 14.5f), Quaternion.Euler(0, 0, 0)).GetComponent<ObstacleMan>();
                obsMan[cur].move(vel);
                obsRight = !obsRight;

                cur++;
                if (cur == obsMan.Length)
                    cur = 0;

                updateBackground();

                if(vib)
                    Vibrator.Vibrate(50);
            }
        }
    }

    private void updateBackground()
    {
        if (matState == 0)
        {
            mat.SetColor("_Top", Color.Lerp(color1, color4, matProgresss));
            mat.SetColor("_Mid", Color.Lerp(color2, color5, matProgresss));
            mat.SetColor("_Bot", Color.Lerp(color3, color6, matProgresss));

            matProgresss += backSpeed;

            if (matProgresss >= 1)
            {
                matState = 1;
                matProgresss = 0;
                PlayerPrefs.SetInt("material", matState);
            }
        }
        else if (matState == 1)
        {
            mat.SetColor("_Top", Color.Lerp(color4, color2, matProgresss));
            mat.SetColor("_Mid", Color.Lerp(color5, color3, matProgresss));
            mat.SetColor("_Bot", Color.Lerp(color6, color1, matProgresss));

            matProgresss += backSpeed;

            if (matProgresss >= 1)
            {
                matState = 2;
                matProgresss = 0;
                PlayerPrefs.SetInt("material", matState);
            }
        }
        else if (matState == 2)
        {
            mat.SetColor("_Top", Color.Lerp(color2, color5, matProgresss));
            mat.SetColor("_Mid", Color.Lerp(color3, color6, matProgresss));
            mat.SetColor("_Bot", Color.Lerp(color1, color4, matProgresss));

            matProgresss += backSpeed;

            if (matProgresss >= 1)
            {
                matState = 3;
                matProgresss = 0;
                PlayerPrefs.SetInt("material", matState);
            }
        }
        else if (matState == 3)
        {
            mat.SetColor("_Top", Color.Lerp(color5, color3, matProgresss));
            mat.SetColor("_Mid", Color.Lerp(color6, color1, matProgresss));
            mat.SetColor("_Bot", Color.Lerp(color4, color2, matProgresss));

            matProgresss += backSpeed;

            if (matProgresss >= 1)
            {
                matState = 4;
                matProgresss = 0;
                PlayerPrefs.SetInt("material", matState);
            }
        }
        else if (matState == 4)
        {
            mat.SetColor("_Top", Color.Lerp(color3, color6, matProgresss));
            mat.SetColor("_Mid", Color.Lerp(color1, color4, matProgresss));
            mat.SetColor("_Bot", Color.Lerp(color2, color5, matProgresss));

            matProgresss += backSpeed;

            if (matProgresss >= 1)
            {
                matState = 5;
                matProgresss = 0;
                PlayerPrefs.SetInt("material", matState);
            }
        }
        else if (matState == 5)
        {
            mat.SetColor("_Top", Color.Lerp(color6, color1, matProgresss));
            mat.SetColor("_Mid", Color.Lerp(color4, color2, matProgresss));
            mat.SetColor("_Bot", Color.Lerp(color5, color3, matProgresss));

            matProgresss += backSpeed;

            if (matProgresss >= 1)
            {
                matState = 0;
                matProgresss = 0;
                PlayerPrefs.SetInt("material", matState);
            }
        }

        PlayerPrefs.SetFloat("progress", matProgresss);
    }

    public bool isGame()
    {
        return game;
    }

    public void startGame()
    {
        scoreText.text = "0";
        game = true;
        mainMenu.SetActive(false);
        character.startCharacter();
    }

    public void stopGame()
    {
        obsMan[cur].move(0);
        game = false;
        stopObstacle();
        gameOver.SetActive(true);
        if (score > PlayerPrefs.GetInt("best"))
            PlayerPrefs.SetInt("best", score);

        int aux = score / 6;
        coinText2.text = "+ " + aux.ToString();
        aux += PlayerPrefs.GetInt("coin"); 
        PlayerPrefs.SetInt("coin", aux);

        if (vib)
            Vibrator.Vibrate(100);

        adsCounter++;
        if (adsCounter >= 3)
        {
            if(adsMan.isReady())
                adsMan.ShowNonRewardedAd();
            adsCounter = 0;
        }
    }

    public void resetGame()
    {
        obsRight = true;
        perfCount = 0;

        for (int i = 0; i < obsMan.Length; i++)
        {
            if (obsMan[i] != null)
                obsMan[i].open();
            obsMan[i] = Instantiate(obstacle, new Vector2(obsRight ? Random.Range(0.5f, 1.25f) : Random.Range(-1.25f, -0.5f), i * 3 + 1.5f), Quaternion.Euler(0, 0, 0)).GetComponent<ObstacleMan>();
            obsRight = !obsRight;
        }

        cur = 0;
        score = 0;
        vel = 3;
        character.vel = vel;
        scoreText.text = PlayerPrefs.GetInt("best").ToString();
        coinText1.text = PlayerPrefs.GetInt("coin").ToString();
        character.resetCharacter();
        gameOver.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void moveObstacle()
    {
        foreach (ObstacleMan om in obsMan)
            om.move(vel);
    }

    public void stopObstacle()
    {
        foreach (ObstacleMan om in obsMan)
            om.stop();
    }

    public void addScore()
    {
        score++;
    }

    public void setVib(bool x)
    {
        vib = x;
        PlayerPrefs.SetInt("vib", x ? 0 : 1);
    }
}
