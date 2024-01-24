using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [Header("Timer")]
    public float countDownTimer = 5f;

    [Header("Things to stop")]
    public CarContr playerCarController;
    public CarContr playerCarController1;
    public OpponentCar opponentCar;
    public OpponentCar opponentCar1;
    public OpponentCar opponentCar2;

    public Text countDownText;
    
    void Start()
    {
        StartCoroutine(TimeCount());
    }
    void Update()
    {
        if(countDownTimer > 1)
        {
            playerCarController.motorTorque = 0f;
            playerCarController1.motorTorque = 0f;
            opponentCar.movingSpeed = 0f;
            opponentCar1.movingSpeed = 0f;
            opponentCar2.movingSpeed = 0f;
        }
        else if(countDownTimer == 0)
        {
            playerCarController.motorTorque = 4500f;
            playerCarController1.motorTorque = 4500f;
            opponentCar.movingSpeed = 22f;
            opponentCar1.movingSpeed = 25f;
            opponentCar2.movingSpeed = 28f;
        }
    }

    IEnumerator TimeCount()
    {
        while(countDownTimer > 0)
        {
            countDownText.text = countDownTimer.ToString();
            yield return new WaitForSeconds(1f);
            countDownTimer--;
        }

        countDownText.text = "GO";
        yield return new WaitForSeconds(1f);
        countDownText.gameObject.SetActive(false);
    }
}
