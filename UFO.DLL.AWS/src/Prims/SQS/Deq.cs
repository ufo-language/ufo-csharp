using Amazon.SQS.Model;

using UFO.Prims;
using UFO.Types;
using UFO.Types.Data;
using UFO.Types.Literal;

namespace UFO.DLL.AWS.SQS;

[PrimName("aws", "sqs", "deq")]
public class Deq : Primitive
{
    private static int _DEFAULT_WAIT_SECONDS = 5;
    public Deq(string longName) : base(longName)
    {
        ParamTypes = [
            [TypeId.Z_CUSTOM /*client*/, TypeId.STRING /*queue name*/],
            [TypeId.Z_CUSTOM /*client*/, TypeId.STRING /*queue name*/, TypeId.INTEGER /*timeout*/]
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
        int waitSeconds = _DEFAULT_WAIT_SECONDS;
        if (args.Count == 3)
        {
            waitSeconds = ((Integer)args[2]).Value;
        }
        ReceiveMessageResponse response = ReceiveMessageAsync(sqsClient, queueUrl, waitSeconds).GetAwaiter().GetResult();
        Symbol statusCodeSymbol = Symbol.Create(response.HttpStatusCode.ToString());
        if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
        {
            if (response.Messages == null)
            {
                return Term.Create(Symbol.Create("Timeout"), Binding.Create(Symbol.Create("Seconds"), Integer.Create(waitSeconds)));
            }
            else if (response.Messages.Count > 0)
            {
                Message msg = response.Messages[0];
                string messageBody = msg.Body;
                string receiptHandle = msg.ReceiptHandle;
                // Delete the message after receiving it
                DeleteMessageAsync(sqsClient, queueUrl, receiptHandle).GetAwaiter().GetResult();
                return Term.Create(statusCodeSymbol, Types.Literal.String.Create(messageBody));
            }
        }
        Console.WriteLine("Deq got here 7");
        return statusCodeSymbol;
    }

    static async Task<ReceiveMessageResponse> ReceiveMessageAsync(SQSClient sqsClient, string queueUrl, int waitSeconds)
    {
        ReceiveMessageRequest request = new()
        {
            QueueUrl = queueUrl,
            MaxNumberOfMessages = 1,
            WaitTimeSeconds = waitSeconds  // optional long polling, 20 seconds max allowed
        };
        return await sqsClient.Client.ReceiveMessageAsync(request);
    }

    static async Task DeleteMessageAsync(SQSClient sqsClient, string queueUrl, string receiptHandle)
    {
        var request = new DeleteMessageRequest
        {
            QueueUrl = queueUrl,
            ReceiptHandle = receiptHandle
        };
        await sqsClient.Client.DeleteMessageAsync(request);
    }
}
