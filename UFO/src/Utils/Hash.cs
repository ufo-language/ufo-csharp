namespace UFO.Utils;

public class Hash
{
    private static readonly int MAGIC_HASH_PRIME = 397;

    public static int CombineHash(int a, int b)
    {
        unchecked
        {
            return (a * MAGIC_HASH_PRIME) ^ b;
        }
    }

}
