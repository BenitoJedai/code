package Foo;

public class Bar
{
// 0000 TestJavaFinalIntegerField.AssetsLibrary.merge Foo.Bar
// InternalSetConstant { FullName = Foo.Bar, Name = XFloat, RawConstantValueType = java.lang.Float, FieldType = float }
// System.InvalidOperationException: SetConstant { SourceTypeName = Foo.Bar, FieldName = XInteger, FieldType = java.lang.Integer }

	// error: variable XInteger might not have been initialized
    public static final Integer XInteger = 6;

    public static final int BATTERY_HEALTH_COLD = 7;
    public static final long XLong = 77;
    public static final short XShort = 77;
    
	public static final byte XSByte = 77;


	public static final float XFloat = 77;
}
