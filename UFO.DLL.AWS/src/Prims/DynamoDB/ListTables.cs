using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

using UFO.Types;
using UFO.Prims;
using UFO.Types.Literal;

namespace UFO.DLL.AWS.DynamoDB;

[PrimName("aws", "ddb", "listTables")]
public class ListTables : Primitive
{
    public ListTables(string longName) : base(longName)
    {
        ParamTypes = [
            [TypeId.Z_CUSTOM]
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
        try
        {
            ListTablesResponse response = ListTablesAsync(client).GetAwaiter().GetResult();
            if (response == null || response.TableNames == null)
            {
                return Nil.NIL;
            }

            Types.Data.Array tablesArray = Types.Data.Array.Create();
            foreach (var tableName in response.TableNames)
            {
                tablesArray.Append(Types.Literal.String.Create(tableName));
            }
            return tablesArray;
        }
        catch (Exception exn)
        {
            throw new UFOException("DynamoDB", [
                ("Message", Types.Literal.String.Create(exn.Message)),
                ("Primitive", this),
                ("Client", clientObj)
            ]);
        }
    }

    public static async Task<ListTablesResponse> ListTablesAsync(AmazonDynamoDBClient client)
    {
        var request = new ListTablesRequest();
        return await client.ListTablesAsync(request);
    }
}
