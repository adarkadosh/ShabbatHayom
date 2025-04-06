using System;
using General_Scripts;

public class GameEvents
{
    public static Action<Products> OnProductCollected;
    public static Action OnObstacleHit;
}