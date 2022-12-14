    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMan : MonoBehaviour
{
    public GameObject obstacle;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void disableObstacle()
    {
        obstacle.SetActive(false);
    }

    public void enableObstacle()
    {
        obstacle.SetActive(true);
        obstacle.transform.localPosition = new Vector2(Random.Range(-1.25f, 1.25f), 0);
    }

    public void stopAnim()
    {
        anim.speed = 0;
    }

    public void startAnim()
    {
        anim.speed = 1;
    }

    public bool reset()
    {
        return gameObject.transform.position.y > 6.5f;
    }
}
