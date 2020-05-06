namespace HackThePlanet
{
    using System.Text;


    public static class StringExtensions
    {
        public static string RepeatCharacter(char character, int count)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                stringBuilder.Append(character);
            }

            return stringBuilder.ToString();
        }
    }
}