public class Numbers
{
    private static readonly double N = 25;

    public static double Formula(double z)
    {
        return Round((z + Math.Sqrt(4 + Math.Pow(z, 2))) / 2);
    }

    public static double Recursive(double z)
    {
        return Round(Recursive(z, N) / Recursive(z, N - 1));
    }

    public static double Iterative(double z)
    {
        return Round(Iterative(z, N) / Iterative(z, N - 1));
    }

    private static double Recursive(double z, double n)
    {
        if (n == 0 || n == 1) return 1;
        return z * Recursive(z, n - 1) + Recursive(z, n - 2);
    } //https://chatgpt.com/s/t_68f828b087c08191b18b4634c1aa3722

    private static double Iterative(double z, double n)
    {
        int m = (int)n;
        if (m <= 1) return 1; 
        double f0 = 1; // f(0)
        double f1 = 1; // f(1) 
        for (int i = 2; i <= m; i++)
        {
            double f = z * f1 + f0;
            f0 = f1;
            f1 = f;
        }
        return f1;
    }//https://chatgpt.com/s/t_68f82934b5188191976347111d396394

    private static double Round(double value)
    {
        return Math.Round(value, 10);
    }

    public static void Main(String[] args)
    {
        String[] metallics = [
            "Platinum", // [0]
            "Golden", // [1]
            "Silver", // [2]
            "Bronze", // [3]
            "Copper", // [4]
            "Nickel", // [5]
            "Aluminum", // [6]
            "Iron", // [7]
            "Tin", // [8]
            "Lead", // [9]
        ];
        Console.WriteLine(Recursive(2, 0)); // 1
        Console.WriteLine(Recursive(2, 1));
        Console.WriteLine(Recursive(2, 2));
        for (var z = 0; z < metallics.Length; z++)
        {
            Console.WriteLine("\n[" + z + "] " + metallics[z]);
            Console.WriteLine(" ↳ formula(" + z + ")   ≈ " + Formula(z));
            Console.WriteLine(" ↳ recursive(" + z + ") ≈ " + Recursive(z));
            Console.WriteLine(" ↳ iterative(" + z + ") ≈ " + Iterative(z));
        }
    }
}
