
using System.Collections.ObjectModel;
using UFO.Types.Literal;

namespace UFO.Types.Expression;

public class Subscript : Expression
{
    private UFOObject _collection;
    private UFOObject _index;

    private Subscript(UFOObject collection, UFOObject index)
    {
        _collection = collection;
        _index = index;
    }

    public static Subscript Create(Parser.List parts)
    {
        UFOObject collection = (UFOObject)parts[0];
        UFOObject index = (UFOObject)parts[1];
        return new Subscript(collection, index);
    }

    public override UFOObject Eval(Evaluator.Evaluator etor)
    {
        UFOObject collectionVal = _collection.Eval(etor);
        UFOObject indexVal = _index.Eval(etor);
        if (collectionVal.Get(indexVal, out UFOObject value))
        {
            return value;
        }
        throw new Exception($"Illegal index {indexVal} :: {indexVal.GetType().Name} for collection {collectionVal} :: {collectionVal.GetType().Name}");
    }

    public override void ShowOn(TextWriter writer)
    {
        if (_collection is Identifier || _collection is Literal.Literal)
        {
            _collection.ShowOn(writer);
        }
        else
        {
            writer.Write('(');
            _collection.ShowOn(writer);
            writer.Write(')');
        }
        writer.Write('[');
        _index.ShowOn(writer);
        writer.Write(']');
    }
}
