using System;
using System.Threading.Tasks;
using UnityEngine;

public class ImprovedGame : MonoBehaviour, IGameService
{
    [Header("Dependencies")]
    [SerializeField] private Bank bank;
    [SerializeField] private GamePage gamePage;
    [SerializeField] private GameOverPage gameOverPage;
    [SerializeField] private LoaderView engineStartPage;
    [SerializeField] private LevelGenerator levelGenerator;
    [SerializeField] private Player player;
    [SerializeField] private PlayerSessionStats playerSessionStats;
    [SerializeField] private AdvController advController;
    
    // Основная зависимость - только State Machine
    private GameStateMachine stateMachine;
    private PlayerService playerService;
    
    public event Action GameEnded;
    
    private void Awake()
    {
        InitializeServices();
    }
    
    private void InitializeServices()
    {
        // Создаем сервисы
        var uiService = new UIService(gamePage, gameOverPage, engineStartPage);
        playerService = new PlayerService(player);
        var levelService = new LevelService(levelGenerator);
        var bankService = new BankService(bank);
        var sessionStatsService = new SessionStatsService(playerSessionStats);
        
        // Создаем State Machine
        stateMachine = new GameStateMachine(
            uiService,
            playerService,
            levelService,
            bankService,
            sessionStatsService
        );
        
        // Подписываемся на события
        stateMachine.GameEnded += OnGameEnded;
    }
    
    public async Task StartGameAsync(ShipPlayDataDto shipData)
    {
        gameObject.SetActive(true);
        await stateMachine.StartGameAsync(shipData);
    }
    
    public void EndGame()
    {
        stateMachine.EndGame();
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    
    private void OnGameEnded()
    {
        GameEnded?.Invoke();
    }
    
    private void Update()
    {
        stateMachine?.Update();
    }
    
    private void OnDestroy()
    {
        // Правильная очистка ресурсов
        if (stateMachine != null)
        {
            stateMachine.GameEnded -= OnGameEnded;
        }
        
        playerService?.Dispose();
    }
    
    // Для совместимости с оригинальным API
    public async Task Enter(ShipPlayDataDto obj)
    {
        await StartGameAsync(obj);
    }
}