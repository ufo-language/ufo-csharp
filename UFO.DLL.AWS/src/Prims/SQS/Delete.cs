using Amazon.SQS.Model;

using UFO.Prims;
using UFO.Types;
using UFO.Types.Literal;

namespace UFO.DLL.AWS.SQS;

[PrimName("aws", "sqs", "delete")]
public class Delete : Primitive
{
    public Delete(string longName) : base(longName)
    {
        ParamTypes = [
            [TypeId.Z_CUSTOM,  // client (AmazonSQSClient wrapper)
             TypeId.STRING,    // queue name
             TypeId.STRING     // receipt handle
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
        string receiptHandle = args[2].ToDisplayString();
        DeleteMessageResponse response = DeleteMessageAsync(sqsClient, queueUrl, receiptHandle).GetAwaiter().GetResult();
        return Symbol.Create(response.HttpStatusCode.ToString());
    }

    static async Task<DeleteMessageResponse> DeleteMessageAsync(SQSClient sqsClient, string queueUrl, string receiptHandle)
    {
        DeleteMessageRequest request = new()
        {
            QueueUrl = queueUrl,
            ReceiptHandle = receiptHandle
        };
        return await sqsClient.Client.DeleteMessageAsync(request);
    }
}
