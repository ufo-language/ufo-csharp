

namespace UFO.Types.Expression;

public class MethodCall : Expression
{
    private readonly UFOObject _receiver;
    private readonly UFOObject _methodName;

    private MethodCall(UFOObject receiver, UFOObject methodName)
        : base(TypeId.METHOD_CALL)
    {
        _receiver = receiver;
        _methodName = methodName;
    }

    public static MethodCall Create(Parser.List parts)
    {
        UFOObject receiver = (UFOObject)parts[0];
        UFOObject methodName = (UFOObject)parts[1];
        return new MethodCall(receiver, methodName);
    }

    public override UFOObject Eval(Evaluator.Evaluator etor)
    {
        throw new NotImplementedException();
    }

    public override void ShowOn(TextWriter writer)
    {
        _receiver.ShowOn(writer);
        writer.Write('.');
        _methodName.ShowOn(writer);
    }
}
