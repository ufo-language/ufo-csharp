using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace UFO.Types.Literal;

public class String : Literal
{
    private static readonly Dictionary<string, String> _INTERNED_STRINGS = [];
    private static readonly Lock _DICTIONARY_LOCK = new();

    public string Name { get; private set; }
    private readonly int _hashCode;

    private static readonly int _HASH_SEED = typeof(String).GetHashCode();

    private String(string name)
    {
        Name = name;
        _hashCode = Utils.Hash.CombineHash(_HASH_SEED, Name.GetHashCode());
    }

    public static String Create(string name)
    {
        lock (_DICTIONARY_LOCK)
        {
            if (!_INTERNED_STRINGS.TryGetValue(name, out String? value))
            {
                value = new String(name);
                _INTERNED_STRINGS[name] = value;
            }
            return value;
        }
    }

    public override bool BoolValue => Name.Length > 0;

    public override void DisplayOn(TextWriter writer)
    {
        writer.Write(Name);
    }

    public override bool EqualsAux(UFOObject other)
    {
        String otherString = (String)other;
        return Name == otherString.Name;
    }

    public override int GetHashCode()
    {
        return _hashCode;
    }

    public override void ShowOn(TextWriter writer)
    {
        writer.Write('"');
        writer.Write(Name);
        writer.Write('"');
    }

}
