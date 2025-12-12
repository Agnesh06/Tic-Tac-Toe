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
        SpawnedObjjectRpc(e.x , e.y);
    } 
    [Rpc(SendTo.Server)]
    private void SpawnedObjjectRpc(int x ,int y)
    {
        Debug.Log("'Object Spawned");
        Transform SpawnedCrossTransform = Instantiate(crossprefab,GetGridWorldPosition(x,y),Quaternion.identity);
        SpawnedCrossTransform.GetComponent<NetworkObject>().Spawn(true);
    }
    
    private Vector2 GetGridWorldPosition(int x ,int y)
    {
        return new Vector2(-GRID_SIZE + x * GRID_SIZE,-GRID_SIZE + y * GRID_SIZE);
    }
}
