
# Домашняя работа №6

## Теория

 1. [TPL](https://docs.microsoft.com/en-us/dotnet/standard/parallel-programming/task-parallel-library-tpl)
 2. [Async/await и механизм реализации в C# 5.0](https://habr.com/ru/post/260217/) (*обратите внимание на рекомендованные в комментариях видео докладов: они крутые, в идеале - посмотрите и законспектируйте все до конца семестра*)
 3. [Async/await в C#: подводные камни](https://habr.com/ru/post/257221/)
 4. [Async programming in .NET: Best practices](https://www.youtube.com/watch?v=wM-h6P1BJRk), ([расшифровка](https://habr.com/ru/company/jugru/blog/491236/)). *В этом докладе подсказка как выполнить одно
из предстоящих практических заданий*
 5. [Jeffrey Richter — Building responsive and scalable applications](https://www.youtube.com/watch?v=xGSabgBo-S8)
 6. [Get started with ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/getting-started/?view=aspnetcore-3.1&amp%3Btabs=windows&tabs=windows)
 7. [Middleware в ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-3.1)

 *Факультатив:*
 

 8. [Asynchronous C# and F# (II.): How do they differ?](http://tomasp.net/blog/async-csharp-differences.aspx/)
 9. [Роман Елизаров — Корутины в Kotlin](https://www.youtube.com/watch?v=rB5Q3y73FTo)
 10. [Андрей Бреслав — Асинхронно, но понятно. Сопрограммы в Kotlin](https://www.youtube.com/watch?v=ffIVVWHpups)

## Вопросы к семинару
 1. Асинхронно и параллельно - это одно и то же или нет? Почему?
 2. В библиотечном коде всегда нужно писать .ConfigureAwait(false). Да или нет? Почему?
 3. Где можно использовать Task.Result, а где нельзя? Почему?
 4. Запишите цепочку выполнения Middleware в виде сигнатур функций
 5. Это одно и то же или нет?
	var r1 = await Task.Run(() =&gt; File.ReadAll(fileName));
	var r2 = await File.ReadAllAsync(fileName);
 6. В каких случаях следует использовать TaskCompletionSourсe?
 7. Какой планировщик задач выполняется по умолчанию? Как его подменить?
 8. Для чего используется CancellationToken?
 9. Как организовать deadlock с тасками?
 10. В чем отличие IO-bound операций от CPU-bound операций?

## Практика
Практика
1. Перенести калькулятор в ASP.NET Core приложение с использованием [Giraffe](https://github.com/giraffe-fsharp/Giraffe) на F#
2. Написать интеграционные тесты для калькулятора на C# используя [WebApplicationFactory](https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-5.0#basic-tests-with-the-default-webapplicationfactory). Все вызовы
методов сервера должны быть выполнены с помощью async/await
3. Используя [Reflector](https://ru.wikipedia.org/wiki/.NET_Reflector) или [DotPeek](https://www.jetbrains.com/decompiler/) декомпилировать F# и C# части программы, изучить полученный после декомпиляции код, используя настройку “не использовать конструкции async/await” при декомпиляции.