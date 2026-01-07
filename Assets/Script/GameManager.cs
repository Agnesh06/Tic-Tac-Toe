using System;
using UnityEngine;
using Unity.Netcode;
using Unity.VisualScripting;

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
    public event EventHandler GameStarted;
    public event EventHandler OnCurrentPlayablePlayerTypeChanged;

    public enum PlayerType
    {
        None,
        Cross,
        Circle,
    }

    private PlayerType localPlayerType;
    private NetworkVariable<PlayerType> currentPlayableType = new NetworkVariable<PlayerType>();
    

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
            NetworkManager.Singleton.OnClientConnectedCallback += Network_OnClientConnectedCallback;
        }
        currentPlayableType.OnValueChanged+=(PlayerType oldPlayerType , PlayerType newPlayerType) =>
        {
            OnCurrentPlayablePlayerTypeChanged?.Invoke(this,EventArgs.Empty);
        };

    }

    private void Network_OnClientConnectedCallback(ulong obj)
    {
        if(NetworkManager.Singleton.ConnectedClientsList.Count == 2){
        currentPlayableType.Value = PlayerType.Cross;
        TriggerGameStartedRpc();
        }
    }

    [Rpc(SendTo.ClientsAndHost)]
    private  void TriggerGameStartedRpc()
    {
        GameStarted?.Invoke(this,EventArgs.Empty);
    }

    [Rpc(SendTo.Server)]
    public void ClickedOnGridPositionRpc(int x, int y,PlayerType playerType)
    {
        Debug.Log("ClickedOnGridPosition"+ x +"," + y);
        if (playerType!= currentPlayableType.Value)
        {
            return;
        }
        OnClickedOnGridPosition?.Invoke(this,new OnClickedOnGridPositionEventArgs
        {
            x = x,
            y = y,
            PlayerType = playerType,
        });
        switch (currentPlayableType.Value)
        {
            default:
            case PlayerType.Cross:
                currentPlayableType.Value = PlayerType.Circle;
                break;
            case PlayerType.Circle:
                currentPlayableType.Value = PlayerType.Cross;
                break;
        }  
    }
    public PlayerType GetLocalPlayerType()
    {
        return localPlayerType;
    }
    public PlayerType GetCurrentPlayerType()
    {
        return currentPlayableType.Value;
    }
}
