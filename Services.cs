using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

// Сервисы-обертки для Unity компонентов
public class UIService : IUIService
{
    private readonly GamePage gamePage;
    private readonly GameOverPage gameOverPage;
    private readonly LoaderView engineStartPage;
    
    public UIService(GamePage gamePage, GameOverPage gameOverPage, LoaderView engineStartPage)
    {
        this.gamePage = gamePage;
        this.gameOverPage = gameOverPage;
        this.engineStartPage = engineStartPage;
    }
    
    public void ShowGamePage()
    {
        gamePage.Show();
    }
    
    public void HideGamePage()
    {
        gamePage.Hide();
    }
    
    public async Task ShowGameOverAsync(GameOverData data)
    {
        gameOverPage.UpdateTraveledScores(data.Distance);
        gameOverPage.UpdateMoney(data.Money);
        gameOverPage.UpdateRankReward(data.Distance);
        
        var tcs = new TaskCompletionSource<bool>();
        var coroutineRunner = gamePage.GetComponent<MonoBehaviour>();
        
        coroutineRunner.StartCoroutine(ShowGameOverCoroutine(tcs));
        await tcs.Task;
    }
    
    private IEnumerator ShowGameOverCoroutine(TaskCompletionSource<bool> tcs)
    {
        yield return gameOverPage.ShowModalRoutine();
        tcs.SetResult(true);
    }
    
    public void HideGameOver()
    {
        gameOverPage.ForceHide();
    }
    
    public async Task ShowEngineStartAsync()
    {
        var tcs = new TaskCompletionSource<bool>();
        var coroutineRunner = engineStartPage.GetComponent<MonoBehaviour>();
        
        coroutineRunner.StartCoroutine(StartEngineCoroutine(tcs));
        await tcs.Task;
    }
    
    private IEnumerator StartEngineCoroutine(TaskCompletionSource<bool> tcs)
    {
        engineStartPage.gameObject.SetActive(true);
        yield return engineStartPage.StartEngine();
        engineStartPage.gameObject.SetActive(false);
        tcs.SetResult(true);
    }
}

public class PlayerService : IPlayerService
{
    private readonly Player player;
    
    public event Action ShipDestroyed;
    
    public PlayerService(Player player)
    {
        this.player = player;
        this.player.ShipDestoyed += OnShipDestroyed;
    }
    
    public async Task PutShipAsync(ShipPlayDataDto data)
    {
        await player.PutShip(data);
    }
    
    public void StartMovement()
    {
        player.Go();
    }
    
    public void StopMovement()
    {
        player.StopShip();
    }
    
    private void OnShipDestroyed()
    {
        ShipDestroyed?.Invoke();
    }
    
    public void Dispose()
    {
        if (player != null)
        {
            player.ShipDestoyed -= OnShipDestroyed;
        }
    }
}

public class LevelService : ILevelService
{
    private readonly LevelGenerator levelGenerator;
    
    public LevelService(LevelGenerator levelGenerator)
    {
        this.levelGenerator = levelGenerator;
    }
    
    public void StartLevel()
    {
        levelGenerator.StartGame();
    }
    
    public void StopLevel()
    {
        levelGenerator.StopGame();
    }
}

public class BankService : IBankService
{
    private readonly Bank bank;
    
    public BankService(Bank bank)
    {
        this.bank = bank;
    }
    
    public void AddCoins(int amount)
    {
        bank.AddCoins(amount);
    }
}

public class SessionStatsService : ISessionStatsService
{
    private readonly PlayerSessionStats playerSessionStats;
    
    public SessionStatsService(PlayerSessionStats playerSessionStats)
    {
        this.playerSessionStats = playerSessionStats;
    }
    
    public void ResetSession()
    {
        playerSessionStats.ResetSession();
    }
    
    public int SessionMoney => playerSessionStats.SessionMoney;
    public float TraveledDistance => playerSessionStats.TraveledDistance;
}

public class AdvertisementService : IAdvertisementService
{
    private readonly AdvController advController;
    
    public AdvertisementService(AdvController advController)
    {
        this.advController = advController;
    }
    
    public void ShowInterstitial(Action onComplete = null)
    {
        advController.ShowAdvInterstitial(onComplete);
    }
}