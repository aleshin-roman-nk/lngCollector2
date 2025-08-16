# Вопросы для собеседования: ASP.NET Core разработчик
## Блок: Асинхронность и многопоточность

### Базовые концепции

1. **Что такое async/await в C# и как они работают?**
   - Объясните разницу между синхронным и асинхронным выполнением
   - Как компилятор преобразует async/await методы?

2. **В чем разница между Task и Thread?**
   - Когда использовать Task, а когда Thread?
   - Что такое Task Scheduler?

3. **Что такое ConfigureAwait(false) и когда его использовать?**
   - В чем разница между ConfigureAwait(true) и ConfigureAwait(false)?
   - Почему это важно в библиотечном коде?

4. **Объясните понятие "deadlock" в контексте async/await**
   - Как возникают deadlock'и с async/await?
   - Как их избежать?

### Task и Task<T>

5. **Какие способы создания Task вы знаете?**
   - Task.Run(), Task.Factory.StartNew(), new Task()
   - В чем разница между ними?

6. **Что такое Task.WhenAll() и Task.WhenAny()?**
   - Приведите примеры использования
   - В чем разница в поведении?

7. **Как обрабатывать исключения в асинхронных методах?**
   - AggregateException vs обычные исключения
   - Особенности обработки в Task.WhenAll()

8. **Что такое TaskCompletionSource<T>?**
   - Когда и зачем его использовать?
   - Приведите пример использования

### Параллелизм и конкурентность

9. **В чем разница между параллелизмом и конкурентностью?**
   - Concurrency vs Parallelism
   - Как это применимо к веб-приложениям?

10. **Что такое Thread Pool и как он работает?**
    - Как ASP.NET Core использует Thread Pool?
    - Что происходит при нехватке потоков?

11. **Объясните концепцию Thread Safety**
    - Какие типы данных thread-safe в .NET?
    - Как обеспечить потокобезопасность?

12. **Что такое race condition и как их избежать?**
    - Приведите примеры race condition
    - Механизмы синхронизации в .NET

### Синхронизация

13. **Какие примитивы синхронизации вы знаете?**
    - lock, Monitor, Mutex, Semaphore, ReaderWriterLock
    - Когда какой использовать?

14. **Что такое SemaphoreSlim и чем он отличается от Semaphore?**
    - Асинхронные версии примитивов синхронизации
    - Примеры использования в веб-приложениях

15. **Объясните работу ConcurrentCollection'ов**
    - ConcurrentDictionary, ConcurrentQueue, ConcurrentBag
    - Когда их использовать вместо обычных коллекций?

### ASP.NET Core специфика

16. **Как работает модель выполнения запросов в ASP.NET Core?**
    - Request pipeline и асинхронность
    - Thread per request vs async/await

17. **Что такое SynchronizationContext в ASP.NET Core?**
    - Как это влияет на выполнение async методов?
    - Почему в ASP.NET Core нет SynchronizationContext?

18. **Как правильно вызывать асинхронные методы из синхронных?**
    - Проблемы с .Result и .Wait()
    - GetAwaiter().GetResult() vs Task.Run()

19. **Что такое async void и почему его избегать?**
    - Исключения: event handlers
    - Проблемы с обработкой исключений

### Производительность и оптимизация

20. **Что такое ValueTask и когда его использовать?**
    - Разница между Task и ValueTask
    - Проблемы с boxing/unboxing

21. **Как измерить производительность асинхронного кода?**
    - Метрики для мониторинга
    - Tools для профилирования

22. **Что такое async state machine?**
    - Как компилятор генерирует код для async методов?
    - Overhead асинхронности

23. **Как оптимизировать количество allocations в async коде?**
    - Pooling объектов
    - Использование Memory<T> и Span<T>

### Практические сценарии

24. **Как реализовать timeout для асинхронной операции?**
    - CancellationToken и CancellationTokenSource
    - Task.Delay() для timeout

25. **Как правильно обрабатывать отмену операций?**
    - OperationCanceledException
    - Graceful shutdown в ASP.NET Core

26. **Как реализовать retry logic для асинхронных операций?**
    - Exponential backoff
    - Circuit breaker pattern

27. **Как организовать background tasks в ASP.NET Core?**
    - IHostedService и BackgroundService
    - Жизненный цикл background services

### Сложные сценарии

28. **Как реализовать producer-consumer pattern асинхронно?**
    - Channel<T> в .NET
    - AsyncEnumerable (IAsyncEnumerable<T>)

29. **Что такое async streams (IAsyncEnumerable)?**
    - await foreach
    - Когда использовать вместо Task<IEnumerable<T>>?

30. **Как тестировать асинхронный код?**
    - Mocking асинхронных методов
    - Testing race conditions

### Анти-паттерны и распространенные ошибки

31. **Назовите основные анти-паттерны в асинхронном коде**
    - Sync over async
    - Async all the way up
    - Fire and forget

32. **Как избежать memory leaks в асинхронном коде?**
    - Proper disposal of resources
    - CancellationToken usage

33. **Что такое "async void" анти-паттерн?**
    - Почему это проблема?
    - Исключения из правила

### Debugging и мониторинг

34. **Как отлаживать асинхронный код?**
    - Call stack в async методах
    - Task debugging в Visual Studio

35. **Какие метрики важны для мониторинга асинхронных приложений?**
    - Thread pool metrics
    - Task completion rates
    - Response times

---

## Дополнительные темы для углубленного изучения

- **Channels и Pipelines** в .NET
- **Memory management** в асинхронном коде
- **Low-level async** (unsafe code, memory barriers)
- **Distributed systems** и async patterns
- **gRPC streaming** и асинхронность
- **SignalR** real-time communication patterns

---

*Примечание: Для каждого вопроса рекомендуется подготовить не только теоретический ответ, но и практические примеры кода, демонстрирующие понимание концепций.*