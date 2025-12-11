using UnityEngine;

public class Gridposition : MonoBehaviour
{
    [SerializeField] private int x;
    [SerializeField] private int y;
    private void OnMouseDown() {
        Debug.Log("clicked" +x+","+y);
        GameManager.Instance.ClickedOnGridPosition(x,y);
    }
}
