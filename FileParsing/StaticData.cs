namespace FileParsing
{
    static class StaticData
    {
        public const char MacroSeparator = '#';
        public const char VariableSeparator = '$';
        public static char[] CharactersToIgnore = {' ', '\t', '\r', '\n'};

        public static bool IsAllowedCharacterForVariable(char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9');
        }
        public static bool IsAllowedFirstCharacterForVariable(char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
        }
    }
}
