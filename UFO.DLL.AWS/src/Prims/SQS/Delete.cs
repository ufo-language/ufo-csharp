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
        UFOObject clientObj = args[0];
        if (clientObj is not SQSClient)
        {
            throw new Exception($"Expected SQSClient, found {clientObj} :: {clientObj.TypeSymbol()}");
        }
        SQSClient sqsClient = (SQSClient)clientObj;
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
