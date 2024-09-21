### Client and Server Evaluation


* Client evaluation happens when using LINQ queries with AsEnumarable() method.
    
        The wrong usage can cause significant performance issues. The IEnumerable interface is a standard .NET interface for collection of objects in System.Collections namespace. It will perfomance actions in memory.

* Server evaluation happens when using LINQ queries AsQueryable() method.
    
        Server evaluation is preferable option and EF core tries to run this kind of operation as much as possible. IQueryble is in System.Linq namespace and it inherits from the IEnumerable interface. It will translate the manipulation into a specific query language, such as SQL, and execute d against the data source when call the ToList().


Some LINQ methods can cause the query to be executed immediately. Operations that executes the query immediately:

    * For or Foreach loop to iterate the items in the collection
    * ToList(), ToArray(), Single(), SingleOrDefault(), Frst(), FirstOrDefault(), or Count() methods (async options of them as well).



Each time new Util().RandomBasedOnAgeMemoryLeak(cat.Age) is called, it creates a new Random instance.

This is a poor use of Random, as it's meant to be used across multiple calls rather than instantiated every time. If many requests come in or the dataset is large, it could create unnecessary memory pressure.


Thread-Safety:

https://www.youtube.com/watch?v=bGUKSm_Ewrw

https://www.youtube.com/watch?v=J-jNcUhi9xw