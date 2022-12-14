using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMan : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;

    public void move(float vel)
    {
        rb.velocity = -transform.up * vel;
    }

    public void stop()
    {
        rb.velocity = Vector2.zero;
    }

    public void gravity()
    {
        rb.gravityScale = 1;
        Destroy(this.gameObject, 1);
    }

    public void open()
    {
        anim.Play("obsOpen");
        Destroy(this.gameObject, 0.5f);
    }

    private void vib()
    {
        Handheld.Vibrate();
    }
}
