

enum ConversionKind
{ x, y, z }

enum ConversionKind3
{ x, y, z }


class Class1
{
    static ConversionKind[,] convkind = 
     {
        {ConversionKind.x, ConversionKind.y},
        {ConversionKind.x, ConversionKind.y},
     };

    static ConversionKind[, ,] convkind2 = 
     {
        {{ConversionKind.x, ConversionKind.y, ConversionKind.z}},
     };

    static ConversionKind3[, ,] convkind3 = 
     {
        {{ConversionKind3.x, ConversionKind3.y, ConversionKind3.z}},
     };
}
