using Amazon.SQS.Model;

using UFO.Prims;
using UFO.Types;
using UFO.Types.Literal;

namespace UFO.DLL.AWS.SQS;

[PrimName("aws", "sqs", "send")]
public class Send : Primitive
{
    public Send(string longName) : base(longName)
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
        UFOObject clientObj = args[0];
        if (clientObj is not SQSClient)
        {
            throw new Exception($"Expected AmazonSQSClient, found {clientObj} :: {clientObj.TypeSymbol()}");
        }
        SQSClient sqsClient = (SQSClient)clientObj;
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
