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
    private PlayerType currentPlayableType;
    

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
        if (IsServer)
        {
            currentPlayableType = PlayerType.Cross;
        }
    }
    [Rpc(SendTo.Server)]
    public void ClickedOnGridPositionRpc(int x, int y,PlayerType playerType)
    {
        Debug.Log("ClickedOnGridPosition"+ x +"," + y);
        if (playerType!= currentPlayableType)
        {
            return;
        }
        OnClickedOnGridPosition?.Invoke(this,new OnClickedOnGridPositionEventArgs
        {
            x = x,
            y = y,
            PlayerType = playerType,
        });
        switch (currentPlayableType)
        {
            default:
            case PlayerType.Cross:
                currentPlayableType = PlayerType.Circle;
                break;
            case PlayerType.Circle:
                currentPlayableType = PlayerType.Cross;
                break;
        }
    }
    public PlayerType GetLocalPlayerType()
    {
        return localPlayerType;
    }
}
