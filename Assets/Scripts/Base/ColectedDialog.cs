using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ColectedDialog : MonoBehaviour
{
    // Start is called before the first frame update
    public static ColectedDialog Instance;
    public TextMeshProUGUI AmountTxt;

    private void Start()
    {
        Instance = this;
    }
    private void OnDestroy()
    {
        Instance = null;
    }

    public void ShowDialog(int amount)
    {
        transform.GetChild(0).gameObject.SetActive(true);
        if (amount == -1)
        {
            AmountTxt.text = "";
        }
        else
        {

            AmountTxt.text = "+" + amount.ToString();
        }

    }
    public void ShowRandomDiamon(int Min, int Max)
    {
        int value;
        transform.GetChild(0).gameObject.SetActive(true);
        if (Min > Max || Min < 0 || Max < 0)
        {
            Debug.LogError("Wrong Input Value");
        }
        else
        {
            value = Random.Range(Min, Max);
            AmountTxt.text = "+" + value.ToString();
            GameController.Instance.ShowRewardDiamond(value);
        }

    }


}
