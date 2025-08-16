# Ответы на вопросы: ASP.NET Core разработчик
## Блок: Асинхронность и многопоточность

### Базовые концепции

#### 1. Что такое async/await в C# и как они работают?

**Ответ:**
- `async/await` - это синтаксический сахар для работы с асинхронными операциями в C#
- `async` - модификатор метода, указывающий что метод содержит асинхронные операции
- `await` - оператор, который приостанавливает выполнение метода до завершения асинхронной операции

**Как работает:**
```csharp
public async Task<string> GetDataAsync()
{
    using var client = new HttpClient();
    string result = await client.GetStringAsync("https://api.example.com/data");
    return result;
}
```

Компилятор преобразует async метод в state machine:
- Создается структура, реализующая IAsyncStateMachine
- Код разбивается на части между await операторами
- При каждом await поток может быть освобожден
- После завершения операции выполнение продолжается с места остановки

#### 2. В чем разница между Task и Thread?

**Ответ:**

**Thread:**
- Низкоуровневая абстракция ОС
- Тяжелый ресурс (1MB стека по умолчанию)
- Прямое управление потоком
- Блокирующий подход

**Task:**
- Высокоуровневая абстракция над работой
- Легковесный (использует Thread Pool)
- Представляет единицу работы, а не поток
- Асинхронный подход

```csharp
// Thread - тяжелый ресурс
var thread = new Thread(() => DoWork());
thread.Start();
thread.Join();

// Task - легковесная абстракция
var task = Task.Run(() => DoWork());
await task;

// Task Scheduler распределяет задачи по потокам Thread Pool
```

#### 3. Что такое ConfigureAwait(false) и когда его использовать?

**Ответ:**

`ConfigureAwait(false)` указывает, что продолжение выполнения после await не обязательно должно происходить в том же контексте синхронизации.

```csharp
// В библиотечном коде
public async Task<string> LibraryMethodAsync()
{
    var result = await SomeAsyncOperation().ConfigureAwait(false);
    // Продолжение может выполниться в любом потоке
    return ProcessResult(result);
}

// В UI коде
public async void Button_Click(object sender, EventArgs e)
{
    var result = await GetDataAsync(); // ConfigureAwait(true) по умолчанию
    // Продолжение в UI потоке для обновления интерфейса
    textBox.Text = result;
}
```

**Когда использовать:**
- В библиотечном коде - всегда `ConfigureAwait(false)`
- В ASP.NET Core - обычно не нужно (нет SynchronizationContext)
- В UI приложениях - когда не нужно возвращаться в UI поток

#### 4. Объясните понятие "deadlock" в контексте async/await

**Ответ:**

Deadlock возникает когда синхронный код блокируется в ожидании асинхронной операции, которая пытается вернуться в заблокированный контекст.

```csharp
// ПЛОХО - вызывает deadlock в UI приложениях
public void SyncMethod()
{
    var result = AsyncMethod().Result; // Блокирует UI поток
}

public async Task<string> AsyncMethod()
{
    await Task.Delay(1000); // Пытается вернуться в UI поток, но он заблокирован
    return "Result";
}

// ХОРОШО - асинхронность до конца
public async Task SyncMethodAsync()
{
    var result = await AsyncMethod();
}
```

**Как избежать:**
- Async all the way up
- Использовать ConfigureAwait(false) в библиотеках
- Никогда не блокировать асинхронный код (.Result, .Wait())

### Task и Task<T>

#### 5. Какие способы создания Task вы знаете?

**Ответ:**

```csharp
// Task.Run() - самый простой способ
var task1 = Task.Run(() => DoWork());

// Task.Factory.StartNew() - больше контроля
var task2 = Task.Factory.StartNew(() => DoWork(), 
    CancellationToken.None, 
    TaskCreationOptions.LongRunning, 
    TaskScheduler.Default);

// new Task() - создание без запуска
var task3 = new Task(() => DoWork());
task3.Start();

// Task.FromResult() - для уже готовых значений
var task4 = Task.FromResult("immediate result");

// TaskCompletionSource - ручное управление
var tcs = new TaskCompletionSource<string>();
var task5 = tcs.Task;
tcs.SetResult("manual result");
```

**Различия:**
- `Task.Run()` - сразу ставит в очередь Thread Pool
- `Task.Factory.StartNew()` - больше опций, но сложнее
- `new Task()` - создает без запуска, нужен явный Start()

#### 6. Что такое Task.WhenAll() и Task.WhenAny()?

**Ответ:**

**Task.WhenAll()** - ждет завершения всех задач:
```csharp
var tasks = new[]
{
    GetDataAsync("url1"),
    GetDataAsync("url2"),
    GetDataAsync("url3")
};

// Ждем все задачи
string[] results = await Task.WhenAll(tasks);

// Или обрабатываем исключения
try
{
    await Task.WhenAll(tasks);
}
catch (Exception ex)
{
    // Получим только первое исключение
    // Для всех исключений нужно проверить task.Exception
}
```

**Task.WhenAny()** - ждет завершения первой задачи:
```csharp
var tasks = new[]
{
    GetDataAsync("url1"),
    GetDataAsync("url2"),
    GetDataAsync("url3")
};

Task<string> completedTask = await Task.WhenAny(tasks);
string result = await completedTask; // Получаем результат первой завершенной
```

#### 7. Как обрабатывать исключения в асинхронных методах?

**Ответ:**

```csharp
// Простая обработка
public async Task<string> SimpleHandlingAsync()
{
    try
    {
        return await SomeAsyncOperation();
    }
    catch (HttpRequestException ex)
    {
        // Специфичная обработка
        return "fallback";
    }
    catch (Exception ex)
    {
        // Общая обработка
        throw new ApplicationException("Operation failed", ex);
    }
}

// Task.WhenAll с исключениями
public async Task HandleMultipleTasksAsync()
{
    var tasks = new[]
    {
        GetDataAsync("url1"),
        GetDataAsync("url2"),
        GetDataAsync("url3")
    };

    try
    {
        await Task.WhenAll(tasks);
    }
    catch (Exception)
    {
        // Получим только первое исключение
        // Для всех исключений:
        foreach (var task in tasks)
        {
            if (task.IsFaulted)
            {
                var ex = task.Exception?.InnerException;
                // Обработка каждого исключения
            }
        }
    }
}

// AggregateException при синхронном вызове
public void SyncCall()
{
    try
    {
        var task = Task.WhenAll(GetDataAsync("url1"), GetDataAsync("url2"));
        task.Wait();
    }
    catch (AggregateException ex)
    {
        foreach (var innerEx in ex.InnerExceptions)
        {
            // Обработка каждого исключения
        }
    }
}
```

#### 8. Что такое TaskCompletionSource<T>?

**Ответ:**

`TaskCompletionSource<T>` позволяет вручную управлять завершением Task.

```csharp
public class AsyncQueue<T>
{
    private readonly Queue<T> _queue = new();
    private readonly Queue<TaskCompletionSource<T>> _waiters = new();
    private readonly object _lock = new();

    public void Enqueue(T item)
    {
        TaskCompletionSource<T>? waiter = null;
        
        lock (_lock)
        {
            if (_waiters.Count > 0)
            {
                waiter = _waiters.Dequeue();
            }
            else
            {
                _queue.Enqueue(item);
            }
        }
        
        waiter?.SetResult(item);
    }

    public Task<T> DequeueAsync()
    {
        lock (_lock)
        {
            if (_queue.Count > 0)
            {
                return Task.FromResult(_queue.Dequeue());
            }
            
            var tcs = new TaskCompletionSource<T>();
            _waiters.Enqueue(tcs);
            return tcs.Task;
        }
    }
}

// Использование для timeout
public async Task<T> WithTimeout<T>(Task<T> task, TimeSpan timeout)
{
    var tcs = new TaskCompletionSource<T>();
    var timer = new Timer(_ => tcs.SetException(new TimeoutException()), 
                         null, timeout, Timeout.InfiniteTimeSpan);
    
    var completedTask = await Task.WhenAny(task, tcs.Task);
    timer.Dispose();
    
    return await completedTask;
}
```

**Методы TaskCompletionSource:**
- `SetResult(T)` - устанавливает результат
- `SetException(Exception)` - устанавливает исключение  
- `SetCanceled()` - отменяет задачу
- `TrySetResult()` - безопасные версии методов