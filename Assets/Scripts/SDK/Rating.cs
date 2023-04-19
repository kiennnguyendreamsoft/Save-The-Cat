using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Rating : MonoBehaviour
{
    public Button star_1, star_2, star_3, star_4, star_5;
    public Button btnAccept;
    public GameObject[] imgStars;
    private int starNumber = 5;

    private void OnEnable()
    {
        ClickStar(5);
    }

    void Start()
    {
        star_1.onClick.AddListener(delegate{
            ClickStar(1);
        });
        star_2.onClick.AddListener(delegate{
            ClickStar(2);
        });
        star_3.onClick.AddListener(delegate{
            ClickStar(3);
        });
        star_4.onClick.AddListener(delegate{
            ClickStar(4);
        });
        star_5.onClick.AddListener(delegate{
            ClickStar(5);
        });

        btnAccept.onClick.AddListener(AcceptRating);
    }

    public void ClickStar(int number){
        starNumber = number;
        for (int i = 0; i < imgStars.Length; i++)
        {
            imgStars[i].SetActive(i < number);
        }
    }

    public void AcceptRating(){
        if(starNumber < 4){
            gameObject.SetActive(false);
        }
        else
        {
            AppReview.Instance.OpenRating();
            gameObject.SetActive(false);
        }
    }
}
