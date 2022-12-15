using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopMan : MonoBehaviour
{
    public Sprite [] skinSprites;

    public Image [] skinImage;

    public SpriteRenderer character;

    public TMP_Text noMoney;

    public GameController gc;

    private int coin;

    private void OnEnable()
    {
        PlayerPrefs.SetInt("skin0", 1);
        for(int i = 0; i < skinImage.Length; i++)
        {
            if (PlayerPrefs.GetInt("skin" + (i + 1)) == 1)
                skinImage[i].sprite = skinSprites[i + 1];

            coin = PlayerPrefs.GetInt("coin");
            if (coin >= 200)
            {
                int aux = coin / 200;
                if (aux == 1)
                    noMoney.text = "You can buy " + aux + " skin";
                else
                    noMoney.text = "You can buy " + aux + " skins";
            }
            else
                noMoney.text = "You lack " + (200 - coin) + " coins";
        }
    }

    public void changeSkin(int x)
    {
        coin = PlayerPrefs.GetInt("coin");
        if (PlayerPrefs.GetInt("skin" + x) == 1)
        {
            character.sprite = skinSprites[x];
            PlayerPrefs.SetInt("skin", x);
            this.gameObject.SetActive(false);
            gc.resetGame();
        }
        else
        {
            Debug.Log("buying...");
            if (coin >= 200)
            {
                Debug.Log("we have the money...");
                coin -= 200;
                character.sprite = skinSprites[x];
                PlayerPrefs.SetInt("skin", x);
                PlayerPrefs.SetInt("skin" + x, 1);
                PlayerPrefs.SetInt("coin", coin);
                this.gameObject.SetActive(false);
                gc.resetGame();
            }
        }
    }
}
