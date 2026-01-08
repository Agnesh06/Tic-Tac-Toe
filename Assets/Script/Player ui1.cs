using UnityEngine;
using Unity.Netcode;
using System;
using Unity.VisualScripting;
public class Playerui1 : MonoBehaviour
{

    [SerializeField] private GameObject crossyoutext;
    [SerializeField] private GameObject circleyoutext;
    [SerializeField] private GameObject crossarrow;
    [SerializeField] private GameObject circlearrow;
    

    private void Awake()
    {   
        GameManager.Instance.GameStarted+=Gamemanager_GameStarted;
        GameManager.Instance.OnCurrentPlayablePlayerTypeChanged+=GameManger_OnCurrentPlayablePlayerTypeChanged;
        crossyoutext.SetActive(false);
        circleyoutext.SetActive(false);
        crossarrow.SetActive(false);
        circlearrow.SetActive(false);

    }
    public void Start()
    {

        
    }
    public void GameManger_OnCurrentPlayablePlayerTypeChanged(object sender,EventArgs e)
    {
        UpdateCurrentArrow();
    }
    public void Gamemanager_GameStarted(object sender,EventArgs s)
    {
        if (GameManager.Instance.GetLocalPlayerType() == GameManager.PlayerType.Cross)
        {
            crossyoutext.SetActive(true);
            
        }
        else
        {
            circleyoutext.SetActive(true);
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
