using System.Data;
using UnityEngine;
using Unity.Netcode;

public class GameVisualManager : NetworkBehaviour
{
    private const float GRID_SIZE = 3.2f;
    [SerializeField] private Transform crossprefab;
    [SerializeField] private Transform circleprefab;

    private void Start()
    {
        GameManager.Instance.OnClickedOnGridPosition += GameManager_OnClickedOnGridPosition;
    }
    private void GameManager_OnClickedOnGridPosition(object sender,GameManager.OnClickedOnGridPositionEventArgs e)
    {
        Debug.Log("message sended to server ");
        SpawnedObjjectRpc(e.x , e.y,e.PlayerType);
    } 
    [Rpc(SendTo.Server)]
    private void SpawnedObjjectRpc(int x ,int y,GameManager.PlayerType playerType)
    {
        Debug.Log("'Object Spawned");
        Transform prefab;
        switch (playerType)
        {
            default:
            case GameManager.PlayerType.Cross :
                prefab = crossprefab;
                break;
            case GameManager.PlayerType.Circle :
            prefab = circleprefab;
            break;
        }
        Transform SpawnedCrossTransform = Instantiate(prefab,GetGridWorldPosition(x,y),Quaternion.identity);
        SpawnedCrossTransform.GetComponent<NetworkObject>().Spawn(true);
    }
    
    private Vector2 GetGridWorldPosition(int x ,int y)
    {
        return new Vector2(-GRID_SIZE + x * GRID_SIZE,-GRID_SIZE + y * GRID_SIZE);
    }
}
