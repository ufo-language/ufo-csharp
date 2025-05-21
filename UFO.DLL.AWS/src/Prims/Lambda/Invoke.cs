using Amazon.Lambda.Model;

using UFO.Prims;
using UFO.Types;
using UFO.Types.Literal;

namespace UFO.DLL.AWS.Lambda;

[PrimName("aws", "lambda", "invoke")]
public class Invoke : Primitive
{
    public Invoke(string longName) : base(longName)
    {
        ParamTypes = [
            [TypeId.Z_CUSTOM,  // client (AmazonLambdaClient wrapper)
             TypeId.STRING,    // function name
             TypeId.STRING     // payload JSON
            ]
        ];
    }

    public override UFOObject Call(Evaluator.Evaluator etor, List<UFOObject> args)
    {
        if (args[0] is not LambdaClient lambdaClient)
        {
            throw new Exception("Expected LambdaClient");
        }
        string functionName = args[1].ToDisplayString();
        string payload = args[2].ToDisplayString();

        string? response = InvokeLambda(lambdaClient, functionName, payload).GetAwaiter().GetResult();
        return Types.Literal.String.Create(response);
    }

    static async Task<string> InvokeLambda(LambdaClient lambdaClient, string functionName, string payload)
    {
        var request = new InvokeRequest
        {
            FunctionName = functionName,
            Payload = payload
        };

        var response = await lambdaClient.Client.InvokeAsync(request);
        using var reader = new StreamReader(response.Payload);
        return await reader.ReadToEndAsync();
    }
}
