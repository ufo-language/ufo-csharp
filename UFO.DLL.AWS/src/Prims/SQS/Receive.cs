using Amazon.SQS.Model;

using UFO.Types;
using UFO.Types.Data;
using UFO.Types.Literal;

namespace UFO.DLL.AWS.SQS;

public class Receive : Primitive
{
    public Receive() : base("receive")
    {
        ParamTypes = [
            [TypeId.Z_CUSTOM,  // client (AmazonSQSClient wrapper)
             TypeId.STRING     // queue URL
            ]
        ];
    }

    public override UFOObject Call(Evaluator.Evaluator etor, List<UFOObject> args)
    {
        UFOObject clientObj = args[0];
        if (clientObj is not SQSClient)
        {
            throw new Exception($"Expected SQSClient, found {clientObj} :: {clientObj.TypeSymbol()}");
        }
        SQSClient sqsClient = (SQSClient)clientObj;
        string queueUrl = args[1].ToDisplayString();
        ReceiveMessageResponse response = ReceiveMessageAsync(sqsClient, queueUrl).GetAwaiter().GetResult();
        Symbol statusCodeSymbol = Symbol.Create(response.HttpStatusCode.ToString());
        if (response.HttpStatusCode == System.Net.HttpStatusCode.OK && response.Messages.Count > 0)
        {
            Console.WriteLine($"SQS Receive got status {response.HttpStatusCode}, message count {response.Messages.Count}");
            string messageBody = response.Messages[0].Body;
            // You might want to delete the message after receiving it or handle receipt handle separately.
            return Term.Create(statusCodeSymbol, Types.Literal.String.Create(messageBody));
        }
        return statusCodeSymbol;
    }

    static async Task<ReceiveMessageResponse> ReceiveMessageAsync(SQSClient sqsClient, string queueUrl)
    {
        ReceiveMessageRequest request = new()
        {
            QueueUrl = queueUrl,
            MaxNumberOfMessages = 1,
            WaitTimeSeconds = 5  // optional long polling
        };
        return await sqsClient.Client.ReceiveMessageAsync(request);
    }
}
