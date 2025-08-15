using System;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections.Generic;

// Состояния игры
public enum GameStateType
{
    Inactive,
    Starting,
    Playing,
    GameOver
}

// Базовый класс состояния
public abstract class GameState
{
    protected readonly GameStateMachine stateMachine;
    
    protected GameState(GameStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
    
    public virtual Task Enter() => Task.CompletedTask;
    public virtual void Exit() { }
    public virtual void Update() { }
}

// Состояние "Неактивно"
public class InactiveState : GameState
{
    public InactiveState(GameStateMachine stateMachine) : base(stateMachine) { }
    
    public override Task Enter()
    {
        stateMachine.UIService.HideGamePage();
        stateMachine.UIService.HideGameOver();
        return Task.CompletedTask;
    }
}

// Состояние "Запуск игры"
public class StartingState : GameState
{
    public StartingState(GameStateMachine stateMachine) : base(stateMachine) { }
    
    public override async Task Enter()
    {
        stateMachine.UIService.ShowGamePage();
        stateMachine.SessionStatsService.ResetSession();
        
        await stateMachine.UIService.ShowEngineStartAsync();
        await stateMachine.PlayerService.PutShipAsync(stateMachine.CurrentShipData);
        
        stateMachine.LevelService.StartLevel();
        stateMachine.PlayerService.StartMovement();
        
        stateMachine.ChangeState(GameStateType.Playing);
    }
}

// Состояние "Игра идет"
public class PlayingState : GameState
{
    public PlayingState(GameStateMachine stateMachine) : base(stateMachine) { }
    
    public override Task Enter()
    {
        // Подписываемся на события
        stateMachine.PlayerService.ShipDestroyed += OnShipDestroyed;
        return Task.CompletedTask;
    }
    
    public override void Exit()
    {
        // Отписываемся от событий
        stateMachine.PlayerService.ShipDestroyed -= OnShipDestroyed;
    }
    
    private void OnShipDestroyed()
    {
        stateMachine.ChangeState(GameStateType.GameOver);
    }
}

// Состояние "Игра окончена"
public class GameOverState : GameState
{
    public GameOverState(GameStateMachine stateMachine) : base(stateMachine) { }
    
    public override async Task Enter()
    {
        var sessionStats = stateMachine.SessionStatsService;
        
        // Добавляем деньги в банк
        stateMachine.BankService.AddCoins(sessionStats.SessionMoney);
        
        // Показываем экран окончания игры
        var gameOverData = new GameOverData(
            sessionStats.SessionMoney, 
            sessionStats.TraveledDistance
        );
        
        await stateMachine.UIService.ShowGameOverAsync(gameOverData);
        
        // Останавливаем игровые системы
        stateMachine.PlayerService.StopMovement();
        stateMachine.LevelService.StopLevel();
        
        // Вызываем событие окончания игры
        stateMachine.OnGameEnded();
        
        stateMachine.ChangeState(GameStateType.Inactive);
    }
}

// State Machine для управления состояниями
public class GameStateMachine
{
    private readonly Dictionary<GameStateType, GameState> states;
    private GameState currentState;
    
    public event Action GameEnded;
    
    // Сервисы
    public IUIService UIService { get; }
    public IPlayerService PlayerService { get; }
    public ILevelService LevelService { get; }
    public IBankService BankService { get; }
    public ISessionStatsService SessionStatsService { get; }
    
    // Текущие данные корабля
    public ShipPlayDataDto CurrentShipData { get; private set; }
    
    public GameStateMachine(
        IUIService uiService,
        IPlayerService playerService,
        ILevelService levelService,
        IBankService bankService,
        ISessionStatsService sessionStatsService)
    {
        UIService = uiService;
        PlayerService = playerService;
        LevelService = levelService;
        BankService = bankService;
        SessionStatsService = sessionStatsService;
        
        // Инициализируем состояния
        states = new Dictionary<GameStateType, GameState>
        {
            { GameStateType.Inactive, new InactiveState(this) },
            { GameStateType.Starting, new StartingState(this) },
            { GameStateType.Playing, new PlayingState(this) },
            { GameStateType.GameOver, new GameOverState(this) }
        };
        
        currentState = states[GameStateType.Inactive];
    }
    
    public async Task StartGameAsync(ShipPlayDataDto shipData)
    {
        CurrentShipData = shipData;
        await ChangeState(GameStateType.Starting);
    }
    
    public void EndGame()
    {
        ChangeState(GameStateType.GameOver);
    }
    
    public async Task ChangeState(GameStateType newStateType)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        
        currentState = states[newStateType];
        await currentState.Enter();
    }
    
    public void OnGameEnded()
    {
        GameEnded?.Invoke();
    }
    
    public void Update()
    {
        currentState?.Update();
    }
}