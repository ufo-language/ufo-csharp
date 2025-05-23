using Amazon.SQS.Model;

using UFO.Prims;
using UFO.Types;
using UFO.Types.Literal;

namespace UFO.DLL.AWS.SQS;

public class Send : Primitive
{
    public Send()
    {
        ParamTypes = [
            [TypeId.Z_CUSTOM,  // client (AmazonSQSClient wrapper)
             TypeId.STRING,    // queue name
             TypeId.STRING     // message body
            ]
        ];
    }

    public override UFOObject Call(Evaluator.Evaluator etor, List<UFOObject> args)
    {
        if (args[0] is not SQSClient sqsClient)
        {
            throw new UFOException("SQSClient", [
                ("Message", Types.Literal.String.Create("Expected an SQSClient instance")),
                ("Actual", args[0]),
                ("Type", args[0].TypeSymbol())
            ]);
        }
        string queueUrl = args[1].ToDisplayString();
        string messageBody = args[2].ToDisplayString();
        SendMessageResponse response = SendMessageAsync(sqsClient, queueUrl, messageBody).GetAwaiter().GetResult();
        Symbol statusCodeSymbol = Symbol.Create(response.HttpStatusCode.ToString());
        return statusCodeSymbol;
    }

    static async Task<SendMessageResponse> SendMessageAsync(SQSClient sqsClient, string queueUrl, string messageBody)
    {
        var request = new SendMessageRequest
        {
            QueueUrl = queueUrl,
            MessageBody = messageBody
        };
        return await sqsClient.Client.SendMessageAsync(request);
    }
}
