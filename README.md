# Async/Await Demo

Demo code with examples for educational purpose

## Motivation

![preemptive](./preemptive.svg)
![cooperative, synchroneous](./cooperative_synchroneous.svg)
![cooperative, asynchroneous](./cooperative_asynchroneous.svg)

- STA Applications
- Thread Pool Behavior

## Async/Await

 *  Synchronization Context
     *  None (e.g. ASP.NET CORE)
     *  WPF
     *  Windows Forms
 *  `async void` vs `async Task`/`async Task<TResult>`
 *  Exception Handling
 *  Pitfalls
     *  `Task.Run(Action)` vs. `Task.Factory.StartNew(Action)`
     *  Continuing on a captured Synchronization Context
 *  Unit-Testing



## Further Reading

 *  [Async Programming : Introduction to Async/Await on ASP.NET](https://msdn.microsoft.com/en-us/magazine/dn802603.aspx)
 *  [The Managed Thread Pool](https://docs.microsoft.com/en-us/dotnet/standard/threading/the-managed-thread-pool)
 *  [Understand the .Net CLR thread pool](https://www.infoworld.com/article/3201030/application-development/understand-the-net-clr-thread-pool.html)
 *  [I/O Completion Ports](https://msdn.microsoft.com/en-us/library/windows/desktop/aa365198.aspx)