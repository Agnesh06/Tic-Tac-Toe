using System.Data;
using UnityEngine;

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
        Instantiate(crossprefab,GetGridWorldPosition(e.x,e.y),Quaternion.identity);
    }
    private Vector2 GetGridWorldPosition(int x ,int y)
    {
        return new Vector2(-GRID_SIZE + x * GRID_SIZE,-GRID_SIZE + y * GRID_SIZE);
    }
}
