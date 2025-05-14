namespace UFO.Utils;

public class Hash
{
    private static readonly int _MAGIC_HASH_PRIME = 397;

    public static int CombineHash(int a, int b)
    {
        unchecked
        {
            return (a * _MAGIC_HASH_PRIME) ^ b;
        }
    }

}
