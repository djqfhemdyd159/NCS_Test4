using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieSystem : MonoBehaviour
{
    int maxPushBackBuutton = 3;
    int currentPushBackButton = 0;

    public GameObject gameOverUI;

    private void Start()
    {
        currentPushBackButton = maxPushBackBuutton;
    }

    public void PlayerDie()
    {
        gameOverUI.SetActive(true);
        Destroy(gameObject);
    }

    public void BackButtonChecker()
    {
        if (currentPushBackButton >= maxPushBackBuutton)
        {
            PlayerDie();
        }
    }
}
