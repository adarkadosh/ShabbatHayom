using System;

public class GameEvents
{
    public static Action<Products> OnProductCollected;
    public static Action OnObstacleHit;
    public static Action OnSpeedUp;
    public static Action TetrisSet;
    public static Action GameOver;
    public static Action OnRowCleared;
    public static Action<int> OnScoreChanged;
    public static Action OnGameRestart;
    public static Action OnPauseGame;
}