namespace UFO.Types;

public enum TypeId
{
    // Data
    ARRAY, BINDING, HASH_TABLE, PAIR, PROTO_HASH, QUEUE, SET, TERM,
    // Expression
    APPLY, ASSIGN, FUNCTION, IDENTIFIER, IF_THEN, METHOD_CALL, QUOTE, SCOPE_RESOLUTION, SEQ, SUBSCRIPT,
     // Literal
    BOOLEAN, CLOSURE, INTEGER, NIL, PRIMITIVE, REAL, STRING, SYMBOL,
    // Other
    Z_CUSTOM, Z_ANY, Z_MANY
}

public static class TypeIdExtensions
{
    public static string AsString(this TypeId typeId) =>
        typeId switch
        {
            // Data
            TypeId.ARRAY => "Array",
            TypeId.BINDING => "Binding",
            TypeId.HASH_TABLE => "HashTable",
            TypeId.PAIR => "Pair",
            TypeId.PROTO_HASH => "HashTable",
            TypeId.QUEUE => "Queue",
            TypeId.SET => "Set",
            TypeId.TERM => "Term",
            // Expression
            TypeId.APPLY => "Apply",
            TypeId.ASSIGN => "Assign",
            TypeId.FUNCTION => "Function",
            TypeId.IDENTIFIER => "Identifier",
            TypeId.IF_THEN => "IfThen",
            TypeId.METHOD_CALL => "IfThen",
            TypeId.QUOTE => "Quote",
            TypeId.SCOPE_RESOLUTION => "ScopeResolution",
            TypeId.SEQ => "Seq",
            TypeId.SUBSCRIPT => "Subscript",
            // Literal
            TypeId.BOOLEAN => "Boolean",
            TypeId.CLOSURE => "Closure",
            TypeId.INTEGER => "Integer",
            TypeId.NIL => "Nil",
            TypeId.PRIMITIVE => "Primitive",
            TypeId.REAL => "Real",
            TypeId.STRING => "String",
            TypeId.SYMBOL => "Symbol",
            // Other
            TypeId.Z_CUSTOM => "Custom",
            _ => typeId.ToString()
        };
}
