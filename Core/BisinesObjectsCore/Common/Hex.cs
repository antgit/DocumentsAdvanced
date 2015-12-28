namespace BusinessObjects
{
    /// <summary>
    /// Двоичные данные
    /// </summary>
    public static class Hex
    {
        private static readonly char[] HexDigits = {'0', '1', '2', '3', '4', '5', '6', '7','8', '9', 'A', 'B', 'C', 'D', 'E', 'F'};
        /// <summary>Строковое представление двоичного значения</summary>
        /// <param name="values">Значение</param>
        /// <returns></returns>
        public static string ToHexString(byte[] values)
        {
            int j = values.Length;

            char[] chars = new char[j * 2];

            for (int i = 0; i < j; i++)
            {
                int b = values[i];
                chars[i * 2] = HexDigits[b >> 4];
                chars[i * 2 + 1] = HexDigits[b & 0xF];
            }
            return new string(chars);
        }
        /// <summary>Строковое представление двоичного значения</summary>
        /// <param name="prefix">Префикс</param>
        /// <param name="value">Значение</param>
        /// <returns></returns>
        public static string ToHexString(string prefix, byte[] value)
        {
            int k = prefix.Length;
            int j = value.Length;
            char[] chars = new char[k + (j * 2)];
            prefix.CopyTo(0, chars, 0, k);
            for (int i = 0; i < j; i++)
            {
                int b = value[i];
                int c = (i * 2) + k;
                chars[c] = HexDigits[b >> 4];
                chars[c + 1] = HexDigits[b & 0xF];
            }
            return new string(chars);
        }
        /// <summary>Строковое представление двоичного значения</summary>
        /// <param name="prefix">Префикс</param>
        /// <param name="value">Значение</param>
        /// <param name="suffix">Суффикс</param>
        /// <returns></returns>
        public static string ToHexString(string prefix, byte[] value, string suffix)
        {
            int k = prefix.Length;
            int m = suffix.Length;
            int j = value.Length;
            char[] chars = new char[k + (j * 2) + m];
            prefix.CopyTo(0, chars, 0, k);
            for (int i = 0; i < j; i++)
            {
                int b = value[i];
                int c = (i * 2) + k;
                chars[c] = HexDigits[b >> 4];
                chars[c + 1] = HexDigits[b & 0xF];
            }
            suffix.CopyTo(0, chars, k + (j * 2), m);
            return new string(chars);
        }
    }
}
