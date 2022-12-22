using System.Text;

namespace SharedCode.Comm.Extensions
{
    public static class Extentions
    {
        public static string FillAlignLeft(this string str, int value, char fillWith) 
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(str);
            for (int i = 0; i < value; i++)
            {
                builder.Append(fillWith);
            }
            return builder.ToString();
        }
        public static string FillAlignRight(this string str, int value, char fillWith)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < value; i++)
            {
                builder.Append(fillWith);
            }
            builder.Append(str);
            return builder.ToString();
        }
    }
}
