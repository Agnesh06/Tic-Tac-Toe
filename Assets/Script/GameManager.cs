using System;
using UnityEngine;
using Unity.Netcode;

public class GameManager : NetworkBehaviour 
{
    public static GameManager Instance {get;private set;}
    public event EventHandler<OnClickedOnGridPositionEventArgs>OnClickedOnGridPosition;
    public class OnClickedOnGridPositionEventArgs : EventArgs
    {
        public int x;
        public int y;
        public PlayerType PlayerType;
    }

    public enum PlayerType
    {
        None,
        Cross,
        Circle,
    }

    private PlayerType localPlayerType;
    private void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("More than one Instance");
        }
        Instance = this;
    }
    public override void OnNetworkSpawn()
    {
        Debug.Log("onnetwrokspwan" + NetworkManager.Singleton.LocalClientId);
        if(NetworkManager.Singleton.LocalClientId == 0)
        {
            localPlayerType = PlayerType.Cross;
        }
        else
        {
            localPlayerType = PlayerType.Circle;
        }
    }
    public void ClickedOnGridPosition(int x, int y)
    {
        Debug.Log("ClickedOnGridPosition"+ x +"," + y);
        OnClickedOnGridPosition?.Invoke(this,new OnClickedOnGridPositionEventArgs
        {
            x = x,
            y = y,
            PlayerType = GetLocalPlayerType(),
        });
    }
    public PlayerType GetLocalPlayerType()
    {
        return localPlayerType;
    }
}
