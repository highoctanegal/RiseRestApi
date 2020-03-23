using System;

namespace RiseRestApi.Util
{
    public static class CodeGen
    {
        public static string Generate()
        {
            var code = new char[4];
            for(int i = 0; i < 4; i++)
            {
                code[i] = GetNextAscii();
            }
            return code.ToString();
        }

        private static char GetNextAscii()
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            var num = 0;
            while (num < 48 || (num > 57 && num < 65))
            {
                num = random.Next(48, 90);
            }
            return (char)num;
        }
    }
}
