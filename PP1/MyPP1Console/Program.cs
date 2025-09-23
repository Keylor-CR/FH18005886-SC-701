class Sum
{
    static int SumFor(int n)
    {
        var sum = n * (n + 1) / 2;
        return sum;
    }

    static int SumIte(int n)
    {
        int sum = 0;
        for (int i = 1; i <= n; i++)
        {
            sum += i;
        }
        return sum;
    }

    static void Main()
    {
        const int Max = int.MaxValue;

        Console.WriteLine();
        Console.WriteLine("• SumFor:");
        //Calculo SumFor Asc

        int lastN_For = 0;
        int lastSumN_For = 0;
        for (int n = 1; n <= Max; n++)
        {
            int cur = SumFor(n);
            if (cur > 0)
            {
                lastN_For = n;
                lastSumN_For = cur;
            }
            else break;
        }

        Console.WriteLine($"        ◦ From 1 to Max → n: {lastN_For} → sum: {lastSumN_For}");

        //Calculo SumFor Desc

        int firstN_For = 0;
        int firsSumN_For = 0;
        for (int n = Max; n >= 1; n--)
        {
            int cur = SumFor(n);
            if (cur > 0)
            {
                firstN_For = n;
                firsSumN_For = cur;
                break;
            }
        }

        Console.WriteLine($"        ◦ From Max to 1 → n: {firstN_For} → sum: {firsSumN_For}");


        Console.WriteLine("• SumIte:");
        Console.WriteLine();

        //Calculo SumIte Asc

        int lastN_Ite = 0;
        int lastSumN_Ite = 0;
        for (int n = 1; n <= Max; n++)
        {
            int cur = SumIte(n);
            if (cur > 0)
            {
                lastN_Ite = n;
                lastSumN_Ite = cur;
            }
            else break;
        }

        Console.WriteLine($"        ◦ From 1 to Max → n: {lastN_Ite} → sum: {lastSumN_Ite}");

        //Calculo SumIte Desc 

        // int firstN_Ite = 0;
        // int firstSumN_Ite = 0;
        // for (int n = Max; n >= 1; n--)
        // {
        //     int cur = SumIte(n);
        //     if (cur > 0)
        //     {
        //         firstN_Ite = n;
        //         firstSumN_Ite = cur;
        //         break;
        //     }
        // }

        // Console.WriteLine($"        ◦ From Max to 1 → n: {firstN_Ite} → sum: {firstSumN_Ite}");
        
        //This last one is commented out due to it being computationally infiasible. Source : https://chatgpt.com/share/68d06f3b-6284-8007-8596-0c1f8447a505

    }
}