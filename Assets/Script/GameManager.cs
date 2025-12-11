using System;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get;private set;}
    public event EventHandler<OnClickedOnGridPositionEventArgs>OnClickedOnGridPosition;
    public class OnClickedOnGridPositionEventArgs : EventArgs
    {
        public int x;
        public int y;
    }
    private void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("More than one Instance");
        }
        Instance = this;
    }
    public void ClickedOnGridPosition(int x, int y)
    {
        Debug.Log("ClickedOnGridPosition"+ x +"," + y);
        OnClickedOnGridPosition?.Invoke(this,new OnClickedOnGridPositionEventArgs
        {
            x = x,
            y = y,
        });
    }
}
