using System;
using System.Threading.Tasks;

// Интерфейсы для абстракции зависимостей
public interface IGameService
{
    Task StartGameAsync(ShipPlayDataDto shipData);
    void EndGame();
    event Action GameEnded;
}

public interface IUIService
{
    void ShowGamePage();
    void HideGamePage();
    Task ShowGameOverAsync(GameOverData data);
    void HideGameOver();
    Task ShowEngineStartAsync();
}

public interface IPlayerService
{
    Task PutShipAsync(ShipPlayDataDto data);
    void StartMovement();
    void StopMovement();
    event Action ShipDestroyed;
}

public interface ILevelService
{
    void StartLevel();
    void StopLevel();
}

public interface IBankService
{
    void AddCoins(int amount);
}

public interface ISessionStatsService
{
    void ResetSession();
    int SessionMoney { get; }
    float TraveledDistance { get; }
}

public interface IAdvertisementService
{
    void ShowInterstitial(Action onComplete = null);
}

// DTO для передачи данных
public struct GameOverData
{
    public int Money;
    public float Distance;
    
    public GameOverData(int money, float distance)
    {
        Money = money;
        Distance = distance;
    }
}