using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

using UFO.Types;
using UFO.Prims;
using UFO.Types.Literal;
using UFO.Types.Data;

namespace UFO.DLL.AWS.DynamoDB;

public class PutItem : Primitive
{
    public PutItem()
    {
        ParamTypes = [
            [TypeId.Z_CUSTOM /*Client*/, TypeId.STRING /*table*/, TypeId.HASH_TABLE /*value*/]
        ];
    }

    public override UFOObject Call(Evaluator.Evaluator etor, List<UFOObject> args)
    {
        if (args[0] is not DynamoDBClient clientObj)
        {
            throw new UFOException("DynamoDB", [
                ("Message", Types.Literal.String.Create("Expected a DynamoDBClient instance")),
                ("Actual", args[0]),
                ("Type", args[0].TypeSymbol())
            ]);
        }
        AmazonDynamoDBClient client = clientObj.Client;
        string tableName = ((Types.Literal.String)args[1]).Value;
        HashTable record = (HashTable)args[2];
        try
        {
            PutItemAsync(client, tableName, record).GetAwaiter().GetResult();
            return Nil.NIL;
        }
        catch (Exception exn)
        {
            throw new UFOException("DynamoDB", [
                ("Message", Types.Literal.String.Create(exn.Message)),
                ("Primitive", this),
                ("Client", clientObj),
                ("TableName", Types.Literal.String.Create(tableName)),
                ("Record", record)
            ]);
        }
    }

    public static async Task PutItemAsync(AmazonDynamoDBClient client, string tableName, HashTable record)
    {
        var item = new Dictionary<string, AttributeValue>();
        foreach (KeyValuePair<UFOObject, UFOObject> pair in record.EachElem())
        {
            UFOObject key = pair.Key;
            UFOObject value = pair.Value;
            item[key.ToString()] = new AttributeValue { S = value.ToString() };
        }
        var request = new PutItemRequest
        {
            TableName = tableName,
            Item = item
        };
        await client.PutItemAsync(request);
    }
}
