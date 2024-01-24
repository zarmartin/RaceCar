using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarSelection : MonoBehaviour
{
    [Header("Buttons and Canvas")]
    public Button nextButton;
    public Button previosButton;

    private int currentCar;
    private GameObject[] carList;

    private void Awake()
    {
        chooseCar(0);
    }

    private void Start()
    {
        currentCar = PlayerPrefs.GetInt("CarSelected");

        carList = new GameObject[transform.childCount];

        for(int i = 0; i < transform.childCount; i++)
        {
            carList[i] = transform.GetChild(i).gameObject;
        }

        foreach(GameObject _go in carList)
        {
            _go.SetActive(false);
        }

        if (carList[currentCar])
        {
            carList[currentCar].SetActive(true);
        }
    }
    private void chooseCar(int _index)
    {
        previosButton.interactable = (currentCar != 0);
        nextButton.interactable = (currentCar != (transform.childCount - 1));

        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == _index);
        }
    }

    public void switchCar(int _switchCars)
    {
        currentCar += _switchCars;
        chooseCar(currentCar);
    }

    public void playGame()
    {
        PlayerPrefs.SetInt("CarSelected", currentCar);
        SceneManager.LoadScene("Track1");
    }
}
