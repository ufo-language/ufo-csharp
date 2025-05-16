namespace UFO.Types;

public enum TypeId
{
    // Data
    ARRAY,
    BINDING,
    HASH_TABLE,
    PAIR,
    QUEUE,
    SET,
    // Expression
    APPLY,
    ASSIGN,
    FUNCTION,
    IDENTIFIER,
    IF_THEN,
    SEQ,
    // Literal
    BOOLEAN,
    CLOSURE,
    INTEGER,
    NIL,
    PRIMITIVE,
    REAL,
    STRING,
    SYMBOL,
    // Other
    Z_ANY,
    Z_MANY
}
