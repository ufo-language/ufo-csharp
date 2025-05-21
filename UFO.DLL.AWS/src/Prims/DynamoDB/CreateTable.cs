using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

using UFO.Types;
using UFO.Prims;
using UFO.Types.Literal;

namespace UFO.DLL.AWS.DynamoDB;

[PrimName("aws", "ddb", "createTable")]
public class CreateTable : Primitive
{
    public CreateTable(string longName) : base(longName)
    {
        ParamTypes = [
            [TypeId.Z_CUSTOM, TypeId.STRING]
        ];
    }

    public override UFOObject Call(Evaluator.Evaluator etor, List<UFOObject> args)
    {
        if (args[0] is not DynamoDBClient clientObj)
        {
            throw new Exception("Expected LambdaClient");
        }
        Types.Literal.String tableName = (Types.Literal.String)args[1];
        AmazonDynamoDBClient client = clientObj.Client;
        try
        {
            Amazon.DynamoDBv2.Model.CreateTableResponse response = CreateTableAsync(client, tableName.Value).GetAwaiter().GetResult();
            if (response == null)
            {
                return Nil.NIL;
            }
            Types.Data.Array responseParts = Types.Data.Array.Create();
            responseParts.Append(Types.Literal.String.Create(response.TableDescription.TableName));
            responseParts.Append(Types.Literal.String.Create(response.TableDescription.TableStatus));
            responseParts.Append(Types.Literal.String.Create(response.TableDescription.CreationDateTime?.ToString() ?? "NoDate"));
            return responseParts;
        }
        catch (Exception exn)
        {
            throw new UFOException("DynamoDB", [
                ("Message", Types.Literal.String.Create(exn.Message)),
                ("Primitive", this),
                ("Client", clientObj),
                ("TableName", tableName)
            ]);
        }
    }

    public static async Task<CreateTableResponse> CreateTableAsync(AmazonDynamoDBClient client, string tableName)
    {
        var request = new CreateTableRequest
        {
            TableName = tableName,
            AttributeDefinitions =
                [
                    new AttributeDefinition
                    {
                        AttributeName = "id",
                        AttributeType = "S" // String
                    }
                ],
            KeySchema =
                [
                    new KeySchemaElement
                    {
                        AttributeName = "id",
                        KeyType = "HASH" // Partition key
                    }
                ],
            ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 5,
                    WriteCapacityUnits = 5
                }
        };
        return await client.CreateTableAsync(request);
    }
}
