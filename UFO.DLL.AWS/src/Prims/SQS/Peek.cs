using Amazon.SQS.Model;

using UFO.Prims;
using UFO.Types;
using UFO.Types.Data;
using UFO.Types.Literal;

namespace UFO.DLL.AWS.SQS;

public class Peek : Primitive
{
    public Peek()
    {
        ParamTypes = [
            [TypeId.Z_CUSTOM,  // client (AmazonSQSClient wrapper)
             TypeId.STRING     // queue name
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
        ReceiveMessageResponse response = ReceiveMessageAsync(sqsClient, queueUrl).GetAwaiter().GetResult();
        Symbol statusCodeSymbol = Symbol.Create(response.HttpStatusCode.ToString());
        if (response.HttpStatusCode == System.Net.HttpStatusCode.OK && response.Messages.Count > 0)
        {
            string messageBody = response.Messages[0].Body;
            string receiptHandle = response.Messages[0].ReceiptHandle;
            // You might want to delete the message after receiving it or handle receipt handle separately.
            Term term = Term.Create(statusCodeSymbol, Types.Literal.String.Create(messageBody));
            term.Attrib = Types.Literal.String.Create(receiptHandle);
            return term;
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
