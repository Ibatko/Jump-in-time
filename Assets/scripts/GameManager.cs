using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    private static GameManager instance;
    [SerializeField]
    private GameObject coinPrefab;
    [SerializeField]
    private Text coinTxt;
    [SerializeField]
    private Text crystalTxt;
    [SerializeField]
    private Text healthTxt;
    [SerializeField]
    private Image healthImage;
    private int helthBar;
    private int collectedCoin;
    private int collectedCrystal;

    // приписывание мэнеджера к переменой
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }

    //возвращение правды при появление монеты
    public GameObject CoinPrefab
    {
        get
        {
            return coinPrefab;
        }
    }

    // запись в текст число колекционированных монет
    public int CollectedCoin
    {
        get
        {
            return collectedCoin;
        }

        set
        {
            coinTxt.text = value.ToString();
            collectedCoin = value;
            
        }
    }

    // запись в текст число колекционированных кристалов
    public int CollectedCrystal
    {
        get
        {
            return collectedCrystal;
        }

        set
        {
            crystalTxt.text =value.ToString();
            collectedCrystal = value;
        }
    }

    public int HelthBar
    {
        get
        {
            return helthBar;
        }

        set
        {
            
            helthBar = value;
            if (value == 50)
            {
                healthImage.transform.localScale = new Vector3(1f, 1, 1);
                healthTxt.text = 100 + "%";
            }
            if (value == 40)
            {
                healthImage.transform.localScale = new Vector3(0.8f, 1, 1);
                healthTxt.text = 80 + "%";
            }
            if (value == 30)
            {
                healthImage.transform.localScale = new Vector3(0.6f, 1, 1);
                healthTxt.text = 60 + "%";
            }
            if (value == 20)
            {
                healthImage.transform.localScale = new Vector3(0.4f, 1, 1);
                healthTxt.text = 40 + "%";
            }
            if (value == 10)
            {
                healthImage.transform.localScale = new Vector3(0.2f, 1, 1);
                healthTxt.text = 20 + "%";
            }
            if (value <= 0)
            {
                healthImage.transform.localScale = new Vector3(0f, 1, 1);
                healthTxt.text = 0 + "%";
            }
        }
    }
}
