using System;

namespace ClientServerEvaluation;

public class Util
{

    public static int RandomBasedOnAge(int age)
    {
        return Random.Shared.Next(1, 2) * age;
    } 

    public int RandomBasedOnAgeMemoryLeakAndNoThreadSatefy(int age)
    {
        return new Random().Next(1, 2) * age;
    } 
}
