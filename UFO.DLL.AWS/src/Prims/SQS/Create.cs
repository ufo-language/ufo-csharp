using System.Net;
using Amazon.SQS.Model;

using UFO.Prims;
using UFO.Types;
using UFO.Types.Data;
using UFO.Types.Literal;

namespace UFO.DLL.AWS.SQS;

[PrimName("aws", "sqs", "create")]
public class Create : Primitive
{
    public Create(string longName) : base(longName)
    {
        ParamTypes = [
            [TypeId.Z_CUSTOM, // client (AmazonSQSClient wrapper)
             TypeId.STRING    // queue name
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
        string queueName = args[1].ToDisplayString();
        CreateQueueResponse response = CreateQueueAsync(sqsClient, queueName).GetAwaiter().GetResult();
        Symbol statusCodeSymbol = Symbol.Create(response.HttpStatusCode.ToString());
        if (response.HttpStatusCode == HttpStatusCode.OK)
        {
            return Term.Create(statusCodeSymbol, Types.Literal.String.Create(response.QueueUrl));
        }
        return statusCodeSymbol;
    }

    static async Task<CreateQueueResponse> CreateQueueAsync(SQSClient sqsClient, string queueName)
    {
        var request = new CreateQueueRequest
        {
            QueueName = queueName
        };
        return await sqsClient.Client.CreateQueueAsync(request);
    }
}
