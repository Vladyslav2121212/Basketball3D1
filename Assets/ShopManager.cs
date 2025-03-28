using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public GameObject[] ballPrefabs;
    public Button[] buyButtons;
    public Text[] priceTexts;
    public Image[] ballImages;

    public int[] ballPrices;
    private int playerPoints;
    private BallController ballController;

    void Start()
    {
        ballController = FindObjectOfType<BallController>();
        playerPoints = PlayerPrefs.GetInt("PlayerPoints", 0);

        for (int i = 0; i < buyButtons.Length; i++)
        {
            int index = i;
            buyButtons[i].onClick.AddListener(() => BuyBall(index));

            if (PlayerPrefs.GetInt("BallPurchased" + index, 0) == 1)
            {
                buyButtons[i].interactable = false;
                priceTexts[i].text = "Куплено";
            }
            else
            {
                priceTexts[i].text = "Цена: " + ballPrices[i] + " поинтов";
            }
        }
    }

    public void BuyBall(int ballIndex)
    {
        if (ballIndex < 0 || ballIndex >= ballPrefabs.Length)
        {
            Debug.LogError($"[ShopManager] Ошибка: индекс {ballIndex} выходит за границы массива.");
            return;
        }

        if (PlayerPrefs.GetInt("BallPurchased" + ballIndex, 0) == 1)
        {
            PlayerPrefs.SetInt("SelectedBall", ballIndex);
            PlayerPrefs.Save();
            ballController.ChangeBall(ballIndex); // Передаем индекс
            return;
        }

        if (playerPoints >= ballPrices[ballIndex])
        {
            playerPoints -= ballPrices[ballIndex];
            PlayerPrefs.SetInt("PlayerPoints", playerPoints);
            PlayerPrefs.SetInt("BallPurchased" + ballIndex, 1);
            PlayerPrefs.SetInt("SelectedBall", ballIndex);
            PlayerPrefs.Save();

            buyButtons[ballIndex].interactable = false;
            priceTexts[ballIndex].text = "Куплено";

            ballController.ChangeBall(ballIndex); // Передаем индекс
        }
    }



}








