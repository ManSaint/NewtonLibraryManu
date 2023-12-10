namespace NewtonLibraryManu;

internal static class ConsoleExtentions
{
    public static int GetInt(this string input, string fieldName)
    {
        int num;
        while (true)
        {
            if (int.TryParse(input, out num) == true)
                break;

            Console.WriteLine("Only numbers!");
            Console.Write(fieldName + ": ");

            input = Console.ReadLine()!;
        }
        return num;
    }

    public static int GetRequiredNumbers(this int input, string fieldName, List<int> requiredNumbers)
    {

        while (true)
        {
            if (requiredNumbers.Contains(input) || input == 0)
                break;

            Console.WriteLine("Try again\n");
            Console.Write(fieldName + ": ");

            input = Console.ReadLine()!.GetInt(fieldName);
        }
        return input;
    }
}