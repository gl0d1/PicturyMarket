using System.Text;

namespace PicturyMarket.Domain.Extensions
{
    public static class StringExtension
    {
        public static string Join(this List<string> words) 
        {
            var stringBilder = new StringBuilder();

            for(int i = 0 ; i < words.Count; i++)
            {
                stringBilder.Append($"{i + 1}: {words[i]} ");
            }

            return stringBilder.ToString();
        }
    }
}
