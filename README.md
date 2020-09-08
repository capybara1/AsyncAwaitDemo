# Async/Await Demo

Demo code with examples for educational purpose

## Motivation

In [preemptive multitasking](https://en.wikipedia.org/wiki/Preemption_(computing)#Preemptive_multitasking)
a context switch may happen at any point in time.
On the one hand this guarantees fair use of the available compute resources
on the other hand the execution of code wich is not thread-safe must be protected using
sychnonization primitives.
Also the threads are not considered expensive in terms of their memory footprint and
the scheduling overhead.

![preemptive](./preemptive.svg)

In [cooperative multitasking](https://en.wikipedia.org/wiki/Cooperative_multitasking)
however the executing code must voluntarily _yield_ control to the scheduler.
This allows a reduction of complexity since no sychnonization primitives
are required to cope with an uncontrolled context change.
On the other hand since the responsibility to size the blocks of code
is left to the programmer, this is more error prone.

A variant of this approach is the enforcement of a single thread.
With this model the usage of synchronization primitives may be alltogether omitted.
However programming/design errors may lead to unresposive user interfaces
and other side effects.
If the provided libraries rely heavily on the _synchroneous_ execution of functions
coding for cooperative multitasking is demanding, especially if I/O operations
are designed to block the executing thread.

![cooperative, synchroneous](./cooperative_synchroneous.svg)

A shift towards a [asynchroneous](https://en.wikipedia.org/wiki/Asynchrony_(computer_programming))
(e.g. non-blocking) library along with language elements which
ease the design of these can mitigate the risks mentioned above.
This usually leads to the advent of callbacks or [promises/futures](https://en.wikipedia.org/wiki/Futures_and_promises)
(proposed in 1976).
Another element that eases the design of code are [coroutines](https://en.wikipedia.org/wiki/Coroutine)
(concept from early 1960's)
which are functions that can suspend their execution and be resumed later on.

Since a single threaded model may not capable to take advantage of todays multi code processors
or cluster architectures most runtime environments allow asynchroneous execution of long running
processes on additional threads.
However to avoid the necessity of synchronization primitives sharing of resources is
not encouraged or disallowed.
In case any computation results are required, they need to be passed to the main thread
as message (promise/future) after the computation has been completed.

![cooperative, asynchroneous](./cooperative_asynchroneous.svg)

Coroutines in .NET can be defined by using the [async/await pattern](https://en.wikipedia.org/wiki/Async/await).
Since the .NET runtime environment does not enforce a particular model
the scheduler must be defined by the application.
By default the execution is resumed on a thread pool thread, thus taking
advantage of parallelism provided by the TPL while attempting to avoid costs
usually associated with individual threads.
If the application context instead relies on a single threaded model (e.g. WPF, WindowsForms)
the behavior changes and the execution is resumed by the scheduler (e.g. Dispatcher)
of the specialized context.

## Concepts

- Awaitable and Awaiters
- State-Machine compilation
- Synchronization Context
- `async void` vs `async Task`/`async Task<TResult>`
- Exception Handling
- `Task.Run(Action)` vs. `Task.Factory.StartNew(Action)`
- Pitfalls
  - Comprehension of code execution
  - Making assumptions to run on a particular thread
  - Accidentally switching from a long running thread to a
    task pool thread
    - Thread Pool Behavior
  - Continuing on a captured Synchronization Context
- Unit-Testing

## Further Reading

- Async/await
  - [Async Programming : Introduction to Async/Await on ASP.NET](https://msdn.microsoft.com/en-us/magazine/dn802603.aspx)
  - [Understanding C# async / await (1) Compilation](https://weblogs.asp.net/dixin/understanding-c-sharp-async-await-1-compilation)
  - [Understanding C# async / await (2) The Awaitable-Awaiter Pattern](https://weblogs.asp.net/dixin/understanding-c-sharp-async-await-2-awaitable-awaiter-pattern)
  - [Understanding C# async / await (3) Runtime Context](https://weblogs.asp.net/dixin/understanding-c-sharp-async-await-3-runtime-context)
- Thread Pool
  - [The Managed Thread Pool](https://docs.microsoft.com/en-us/dotnet/standard/threading/the-managed-thread-pool)
  - [Understand the .Net CLR thread pool](https://www.infoworld.com/article/3201030/application-development/understand-the-net-clr-thread-pool.html)
- Misc
  - [I/O Completion Ports](https://msdn.microsoft.com/en-us/library/windows/desktop/aa365198.aspx)