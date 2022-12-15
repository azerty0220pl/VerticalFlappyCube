using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float vel = 4f;
    public Rigidbody2D rb;
    public GameController gc;
    public GameObject skin;
    public SpriteRenderer skinSprite;
    public ParticleSystem ps;
    public ParticleSystem perf1;
    public ParticleSystem perf2;
    public ParticleSystem perf3;
    public ParticleSystem perf4;
    private int state = 0;

    public Sprite[] skinSprites;

    private void Start()
    {
        skinSprite.sprite = skinSprites[PlayerPrefs.GetInt("skin")];
    }

    private void Update()
    {
        if (gc.isGame() && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            switch(state)
            {
                case 1:
                    gc.stopObstacle();
                    rb.velocity = transform.right * vel;
                    state = 2;
                    break;
                case 2:
                    gc.moveObstacle();
                    rb.velocity = Vector2.zero;
                    state = 3;
                    break;
                case 3:
                    gc.stopObstacle();
                    rb.velocity = -transform.right * vel;
                    state = 4;
                    break;
                case 4:
                    gc.moveObstacle();
                    rb.velocity = Vector2.zero;
                    state = 1;
                    break;
            }
        }
    }

    public void startCharacter()
    {
        rb.velocity = transform.right * vel;
        state = 2;
    }

    public void resetCharacter()
    {
        skin.SetActive(true);
        transform.position = Vector2.zero;
        rb.velocity = Vector2.zero;
        state = 0;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "obstacle" && gc.isGame())
        {
            skin.SetActive(false);
            ps.Play();
            gc.stopGame();
            rb.velocity = Vector2.zero;
            state = 0;
        }
    }

    public float getX()
    {
        return transform.position.x;
    }

    public void playPerfect(int x)
    {
        if(x == 4)
        {
            perf4.Play();
            return;
        }

        perf1.Play();
        if (x >= 2)
            perf2.Play();
        if (x > 2)
            perf3.Play();
    }
}
