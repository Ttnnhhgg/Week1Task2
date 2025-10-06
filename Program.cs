using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
class AsyncDemo
{
    static void Main()
    {
        Console.WriteLine("Сравнение синхронной и асинхронной обработки\n");
        RunSyncDemo();
        RunAsyncDemo().Wait();
        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
    static void RunSyncDemo()
    {
        Console.WriteLine("СИНХРОННАЯ ОБРАБОТКА:");
        var stopwatch = Stopwatch.StartNew();
        string result1 = ProcessData("Файл 1");
        string result2 = ProcessData("Файл 2");
        string result3 = ProcessData("Файл 3");    
        Console.WriteLine(result1);
        Console.WriteLine(result2);
        Console.WriteLine(result3);
        stopwatch.Stop();
        Console.WriteLine($"Общее время синхронной обработки: {stopwatch.Elapsed.TotalSeconds:F2} секунд\n");
    }
    static async Task RunAsyncDemo()
    {
        Console.WriteLine("АСИНХРОННАЯ ОБРАБОТКА:");
        var stopwatch = Stopwatch.StartNew();
        await RunAsyncOperations();
        stopwatch.Stop();
        Console.WriteLine($"Общее время асинхронной обработки: {stopwatch.Elapsed.TotalSeconds:F2} секунд");
    }
    static string ProcessData(string dataName)
    {       
        Thread.Sleep(3000);
        return $"Обработка '{dataName}' завершена за 3 секунды";
    }
    static async Task<string> ProcessDataAsync(string dataName)
    {     
        await Task.Delay(3000);
        return $"Асинхронная обработка '{dataName}' завершена за 3 секунды";
    }
    static async Task RunAsyncOperations()
    {
        Task<string> task1 = ProcessDataAsync("Файл 1");
        Task<string> task2 = ProcessDataAsync("Файл 2");
        Task<string> task3 = ProcessDataAsync("Файл 3");
        Console.WriteLine("Все асинхронные задачи запущены...");    
        Task<string>[] tasks = { task1, task2, task3 };
        string[] results = await Task.WhenAll(tasks);        
        foreach (string result in results)
        {
            Console.WriteLine(result);
        }
    }
}