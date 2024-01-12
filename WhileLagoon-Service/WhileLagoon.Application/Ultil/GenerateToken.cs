

namespace WhileLagoon.Application.Ultil
{
    public class GenerateToken
    {
        public static string Generate(int length = 20)
        {
            const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new char[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = allowedChars[random.Next(allowedChars.Length)];
            }

            return new string(result);
        }
    }
}