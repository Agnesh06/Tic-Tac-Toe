using System;
using System.ComponentModel;
using Unity.Networking.Transport.Error;
using UnityEngine;

public class PALyer : MonoBehaviour
{
    [SerializeField] private GameObject crossyoutext;
    [SerializeField] private GameObject circleyoutext;
    [SerializeField] private GameObject crossarrow;
    [SerializeField] private GameObject circlearrow;
    

    private void Awake()
    {
        crossyoutext.SetActive(false);
        circleyoutext.SetActive(false);
        crossarrow.SetActive(false);
        circlearrow.SetActive(false);
    }
    public void Start()
    {
        GameManager.Instance.GameStarted+=Gamemanager_GameStarted;
        
    }
    public void Gamemanager_GameStarted(object sender,EventArgs s)
    {
        if (GameManager.Instance.GetLocalPlayerType() == GameManager.PlayerType.Cross)
        {
            crossyoutext.SetActive(true);
            
        }
        else
        {
            circleyoutext.SetActive(false);
        }
        UpdateCurrentArrow();
    }
    

    private void UpdateCurrentArrow()
    {
        if(GameManager.Instance.GetCurrentPlayerType()== GameManager.PlayerType.Cross)
        {
            crossarrow.SetActive(true);
            circlearrow.SetActive(false);
        }
        else
        {
            crossarrow.SetActive(false);
            circlearrow.SetActive(true);
        }
    }

}