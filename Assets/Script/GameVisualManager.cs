using System.Data;
using UnityEngine;
using Unity.Netcode;

public class GameVisualManager : MonoBehaviour
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
        Transform SpawnedCrossTransform = Instantiate(crossprefab);
        SpawnedCrossTransform.GetComponent<NetworkObject>().Spawn(true);
        SpawnedCrossTransform.position = GetGridWorldPosition(e.x,e.y);
    }
    private Vector2 GetGridWorldPosition(int x ,int y)
    {
        return new Vector2(-GRID_SIZE + x * GRID_SIZE,-GRID_SIZE + y * GRID_SIZE);
    }
}
