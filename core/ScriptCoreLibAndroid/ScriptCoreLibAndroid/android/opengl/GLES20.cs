using java.nio;
using ScriptCoreLib;


namespace android.opengl
{
    // http://developer.android.com/reference/android/opengl/GLES20.html
    // X:\jsc.svn\core\ScriptCoreLibAndroidNDK\ScriptCoreLibAndroidNDK\SystemHeaders\GLES2\gl2.cs

    [Script(IsNative = true)]
    public class GLES20
    {
        // tested by?

        // chicken and egg problem

        public const int GL_ACTIVE_ATTRIBUTE_MAX_LENGTH = 35722;
        public const int GL_ACTIVE_ATTRIBUTES = 35721;
        public const int GL_ACTIVE_TEXTURE = 34016;
        public const int GL_ACTIVE_UNIFORM_MAX_LENGTH = 35719;
        public const int GL_ACTIVE_UNIFORMS = 35718;
        public const int GL_ALIASED_LINE_WIDTH_RANGE = 33902;
        public const int GL_ALIASED_POINT_SIZE_RANGE = 33901;
        public const int GL_ALPHA = 6406;
        public const int GL_ALPHA_BITS = 3413;
        public const int GL_ALWAYS = 519;
        public const int GL_ARRAY_BUFFER = 34962;
        public const int GL_ARRAY_BUFFER_BINDING = 34964;
        public const int GL_ATTACHED_SHADERS = 35717;
        public const int GL_BACK = 1029;
        public const int GL_BLEND = 3042;
        public const int GL_BLEND_COLOR = 32773;
        public const int GL_BLEND_DST_ALPHA = 32970;
        public const int GL_BLEND_DST_RGB = 32968;
        public const int GL_BLEND_EQUATION = 32777;
        public const int GL_BLEND_EQUATION_ALPHA = 34877;
        public const int GL_BLEND_EQUATION_RGB = 32777;
        public const int GL_BLEND_SRC_ALPHA = 32971;
        public const int GL_BLEND_SRC_RGB = 32969;
        public const int GL_BLUE_BITS = 3412;
        public const int GL_BOOL = 35670;
        public const int GL_BOOL_VEC2 = 35671;
        public const int GL_BOOL_VEC3 = 35672;
        public const int GL_BOOL_VEC4 = 35673;
        public const int GL_BUFFER_SIZE = 34660;
        public const int GL_BUFFER_USAGE = 34661;
        public const int GL_BYTE = 5120;
        public const int GL_CCW = 2305;
        public const int GL_CLAMP_TO_EDGE = 33071;
        public const int GL_COLOR_ATTACHMENT0 = 36064;
        public const int GL_COLOR_BUFFER_BIT = 16384;
        public const int GL_COLOR_CLEAR_VALUE = 3106;
        public const int GL_COLOR_WRITEMASK = 3107;
        public const int GL_COMPILE_STATUS = 35713;
        public const int GL_COMPRESSED_TEXTURE_FORMATS = 34467;
        public const int GL_CONSTANT_ALPHA = 32771;
        public const int GL_CONSTANT_COLOR = 32769;
        public const int GL_CULL_FACE = 2884;
        public const int GL_CULL_FACE_MODE = 2885;
        public const int GL_CURRENT_PROGRAM = 35725;
        public const int GL_CURRENT_VERTEX_ATTRIB = 34342;
        public const int GL_CW = 2304;
        public const int GL_DECR = 7683;
        public const int GL_DECR_WRAP = 34056;
        public const int GL_DELETE_STATUS = 35712;
        public const int GL_DEPTH_ATTACHMENT = 36096;
        public const int GL_DEPTH_BITS = 3414;
        public const int GL_DEPTH_BUFFER_BIT = 256;
        public const int GL_DEPTH_CLEAR_VALUE = 2931;
        public const int GL_DEPTH_COMPONENT = 6402;
        public const int GL_DEPTH_COMPONENT16 = 33189;
        public const int GL_DEPTH_FUNC = 2932;
        public const int GL_DEPTH_RANGE = 2928;
        public const int GL_DEPTH_TEST = 2929;
        public const int GL_DEPTH_WRITEMASK = 2930;
        public const int GL_DITHER = 3024;
        public const int GL_DONT_CARE = 4352;
        public const int GL_DST_ALPHA = 772;
        public const int GL_DST_COLOR = 774;
        public const int GL_DYNAMIC_DRAW = 35048;
        public const int GL_ELEMENT_ARRAY_BUFFER = 34963;
        public const int GL_ELEMENT_ARRAY_BUFFER_BINDING = 34965;
        public const int GL_EQUAL = 514;
        public const int GL_EXTENSIONS = 7939;
        public const int GL_FALSE = 0;
        public const int GL_FASTEST = 4353;
        public const int GL_FIXED = 5132;
        public const int GL_FLOAT = 5126;
        public const int GL_FLOAT_MAT2 = 35674;
        public const int GL_FLOAT_MAT3 = 35675;
        public const int GL_FLOAT_MAT4 = 35676;
        public const int GL_FLOAT_VEC2 = 35664;
        public const int GL_FLOAT_VEC3 = 35665;
        public const int GL_FLOAT_VEC4 = 35666;
        public const int GL_FRAGMENT_SHADER = 35632;
        public const int GL_FRAMEBUFFER = 36160;
        public const int GL_FRAMEBUFFER_ATTACHMENT_OBJECT_NAME = 36049;
        public const int GL_FRAMEBUFFER_ATTACHMENT_OBJECT_TYPE = 36048;
        public const int GL_FRAMEBUFFER_ATTACHMENT_TEXTURE_CUBE_MAP_FACE = 36051;
        public const int GL_FRAMEBUFFER_ATTACHMENT_TEXTURE_LEVEL = 36050;
        public const int GL_FRAMEBUFFER_BINDING = 36006;
        public const int GL_FRAMEBUFFER_COMPLETE = 36053;
        public const int GL_FRAMEBUFFER_INCOMPLETE_ATTACHMENT = 36054;
        public const int GL_FRAMEBUFFER_INCOMPLETE_DIMENSIONS = 36057;
        public const int GL_FRAMEBUFFER_INCOMPLETE_MISSING_ATTACHMENT = 36055;
        public const int GL_FRAMEBUFFER_UNSUPPORTED = 36061;
        public const int GL_FRONT = 1028;
        public const int GL_FRONT_AND_BACK = 1032;
        public const int GL_FRONT_FACE = 2886;
        public const int GL_FUNC_ADD = 32774;
        public const int GL_FUNC_REVERSE_SUBTRACT = 32779;
        public const int GL_FUNC_SUBTRACT = 32778;
        public const int GL_GENERATE_MIPMAP_HINT = 33170;
        public const int GL_GEQUAL = 518;
        public const int GL_GREATER = 516;
        public const int GL_GREEN_BITS = 3411;
        public const int GL_HIGH_FLOAT = 36338;
        public const int GL_HIGH_INT = 36341;
        public const int GL_IMPLEMENTATION_COLOR_READ_FORMAT = 35739;
        public const int GL_IMPLEMENTATION_COLOR_READ_TYPE = 35738;
        public const int GL_INCR = 7682;
        public const int GL_INCR_WRAP = 34055;
        public const int GL_INFO_LOG_LENGTH = 35716;
        public const int GL_INT = 5124;
        public const int GL_INT_VEC2 = 35667;
        public const int GL_INT_VEC3 = 35668;
        public const int GL_INT_VEC4 = 35669;
        public const int GL_INVALID_ENUM = 1280;
        public const int GL_INVALID_FRAMEBUFFER_OPERATION = 1286;
        public const int GL_INVALID_OPERATION = 1282;
        public const int GL_INVALID_VALUE = 1281;
        public const int GL_INVERT = 5386;
        public const int GL_KEEP = 7680;
        public const int GL_LEQUAL = 515;
        public const int GL_LESS = 513;
        public const int GL_LINE_LOOP = 2;
        public const int GL_LINE_STRIP = 3;
        public const int GL_LINE_WIDTH = 2849;
        public const int GL_LINEAR = 9729;
        public const int GL_LINEAR_MIPMAP_LINEAR = 9987;
        public const int GL_LINEAR_MIPMAP_NEAREST = 9985;
        public const int GL_LINES = 1;
        public const int GL_LINK_STATUS = 35714;
        public const int GL_LOW_FLOAT = 36336;
        public const int GL_LOW_INT = 36339;
        public const int GL_LUMINANCE = 6409;
        public const int GL_LUMINANCE_ALPHA = 6410;
        public const int GL_MAX_COMBINED_TEXTURE_IMAGE_UNITS = 35661;
        public const int GL_MAX_CUBE_MAP_TEXTURE_SIZE = 34076;
        public const int GL_MAX_FRAGMENT_UNIFORM_VECTORS = 36349;
        public const int GL_MAX_RENDERBUFFER_SIZE = 34024;
        public const int GL_MAX_TEXTURE_IMAGE_UNITS = 34930;
        public const int GL_MAX_TEXTURE_SIZE = 3379;
        public const int GL_MAX_VARYING_VECTORS = 36348;
        public const int GL_MAX_VERTEX_ATTRIBS = 34921;
        public const int GL_MAX_VERTEX_TEXTURE_IMAGE_UNITS = 35660;
        public const int GL_MAX_VERTEX_UNIFORM_VECTORS = 36347;
        public const int GL_MAX_VIEWPORT_DIMS = 3386;
        public const int GL_MEDIUM_FLOAT = 36337;
        public const int GL_MEDIUM_INT = 36340;
        public const int GL_MIRRORED_REPEAT = 33648;
        public const int GL_NEAREST = 9728;
        public const int GL_NEAREST_MIPMAP_LINEAR = 9986;
        public const int GL_NEAREST_MIPMAP_NEAREST = 9984;
        public const int GL_NEVER = 512;
        public const int GL_NICEST = 4354;
        public const int GL_NO_ERROR = 0;
        public const int GL_NONE = 0;
        public const int GL_NOTEQUAL = 517;
        public const int GL_NUM_COMPRESSED_TEXTURE_FORMATS = 34466;
        public const int GL_NUM_SHADER_BINARY_FORMATS = 36345;
        public const int GL_ONE = 1;
        public const int GL_ONE_MINUS_CONSTANT_ALPHA = 32772;
        public const int GL_ONE_MINUS_CONSTANT_COLOR = 32770;
        public const int GL_ONE_MINUS_DST_ALPHA = 773;
        public const int GL_ONE_MINUS_DST_COLOR = 775;
        public const int GL_ONE_MINUS_SRC_ALPHA = 771;
        public const int GL_ONE_MINUS_SRC_COLOR = 769;
        public const int GL_OUT_OF_MEMORY = 1285;
        public const int GL_PACK_ALIGNMENT = 3333;
        public const int GL_POINTS = 0;
        public const int GL_POLYGON_OFFSET_FACTOR = 32824;
        public const int GL_POLYGON_OFFSET_FILL = 32823;
        public const int GL_POLYGON_OFFSET_UNITS = 10752;
        public const int GL_RED_BITS = 3410;
        public const int GL_RENDERBUFFER = 36161;
        public const int GL_RENDERBUFFER_ALPHA_SIZE = 36179;
        public const int GL_RENDERBUFFER_BINDING = 36007;
        public const int GL_RENDERBUFFER_BLUE_SIZE = 36178;
        public const int GL_RENDERBUFFER_DEPTH_SIZE = 36180;
        public const int GL_RENDERBUFFER_GREEN_SIZE = 36177;
        public const int GL_RENDERBUFFER_HEIGHT = 36163;
        public const int GL_RENDERBUFFER_INTERNAL_FORMAT = 36164;
        public const int GL_RENDERBUFFER_RED_SIZE = 36176;
        public const int GL_RENDERBUFFER_STENCIL_SIZE = 36181;
        public const int GL_RENDERBUFFER_WIDTH = 36162;
        public const int GL_RENDERER = 7937;
        public const int GL_REPEAT = 10497;
        public const int GL_REPLACE = 7681;
        public const int GL_RGB = 6407;
        public const int GL_RGB5_A1 = 32855;
        public const int GL_RGB565 = 36194;
        public const int GL_RGBA = 6408;
        public const int GL_RGBA4 = 32854;
        public const int GL_SAMPLE_ALPHA_TO_COVERAGE = 32926;
        public const int GL_SAMPLE_BUFFERS = 32936;
        public const int GL_SAMPLE_COVERAGE = 32928;
        public const int GL_SAMPLE_COVERAGE_INVERT = 32939;
        public const int GL_SAMPLE_COVERAGE_VALUE = 32938;
        public const int GL_SAMPLER_2D = 35678;
        public const int GL_SAMPLER_CUBE = 35680;
        public const int GL_SAMPLES = 32937;
        public const int GL_SCISSOR_BOX = 3088;
        public const int GL_SCISSOR_TEST = 3089;
        public const int GL_SHADER_BINARY_FORMATS = 36344;
        public const int GL_SHADER_COMPILER = 36346;
        public const int GL_SHADER_SOURCE_LENGTH = 35720;
        public const int GL_SHADER_TYPE = 35663;
        public const int GL_SHADING_LANGUAGE_VERSION = 35724;
        public const int GL_SHORT = 5122;
        public const int GL_SRC_ALPHA = 770;
        public const int GL_SRC_ALPHA_SATURATE = 776;
        public const int GL_SRC_COLOR = 768;
        public const int GL_STATIC_DRAW = 35044;
        public const int GL_STENCIL_ATTACHMENT = 36128;
        public const int GL_STENCIL_BACK_FAIL = 34817;
        public const int GL_STENCIL_BACK_FUNC = 34816;
        public const int GL_STENCIL_BACK_PASS_DEPTH_FAIL = 34818;
        public const int GL_STENCIL_BACK_PASS_DEPTH_PASS = 34819;
        public const int GL_STENCIL_BACK_REF = 36003;
        public const int GL_STENCIL_BACK_VALUE_MASK = 36004;
        public const int GL_STENCIL_BACK_WRITEMASK = 36005;
        public const int GL_STENCIL_BITS = 3415;
        public const int GL_STENCIL_BUFFER_BIT = 1024;
        public const int GL_STENCIL_CLEAR_VALUE = 2961;
        public const int GL_STENCIL_FAIL = 2964;
        public const int GL_STENCIL_FUNC = 2962;
        public const int GL_STENCIL_INDEX = 6401;
        public const int GL_STENCIL_INDEX8 = 36168;
        public const int GL_STENCIL_PASS_DEPTH_FAIL = 2965;
        public const int GL_STENCIL_PASS_DEPTH_PASS = 2966;
        public const int GL_STENCIL_REF = 2967;
        public const int GL_STENCIL_TEST = 2960;
        public const int GL_STENCIL_VALUE_MASK = 2963;
        public const int GL_STENCIL_WRITEMASK = 2968;
        public const int GL_STREAM_DRAW = 35040;
        public const int GL_SUBPIXEL_BITS = 3408;
        public const int GL_TEXTURE = 5890;
        public const int GL_TEXTURE_2D = 3553;
        public const int GL_TEXTURE_BINDING_2D = 32873;
        public const int GL_TEXTURE_BINDING_CUBE_MAP = 34068;
        public const int GL_TEXTURE_CUBE_MAP = 34067;
        public const int GL_TEXTURE_CUBE_MAP_NEGATIVE_X = 34070;
        public const int GL_TEXTURE_CUBE_MAP_NEGATIVE_Y = 34072;
        public const int GL_TEXTURE_CUBE_MAP_NEGATIVE_Z = 34074;
        public const int GL_TEXTURE_CUBE_MAP_POSITIVE_X = 34069;
        public const int GL_TEXTURE_CUBE_MAP_POSITIVE_Y = 34071;
        public const int GL_TEXTURE_CUBE_MAP_POSITIVE_Z = 34073;
        public const int GL_TEXTURE_MAG_FILTER = 10240;
        public const int GL_TEXTURE_MIN_FILTER = 10241;
        public const int GL_TEXTURE_WRAP_S = 10242;
        public const int GL_TEXTURE_WRAP_T = 10243;
        public const int GL_TEXTURE0 = 33984;
        public const int GL_TEXTURE1 = 33985;
        public const int GL_TEXTURE10 = 33994;
        public const int GL_TEXTURE11 = 33995;
        public const int GL_TEXTURE12 = 33996;
        public const int GL_TEXTURE13 = 33997;
        public const int GL_TEXTURE14 = 33998;
        public const int GL_TEXTURE15 = 33999;
        public const int GL_TEXTURE16 = 34000;
        public const int GL_TEXTURE17 = 34001;
        public const int GL_TEXTURE18 = 34002;
        public const int GL_TEXTURE19 = 34003;
        public const int GL_TEXTURE2 = 33986;
        public const int GL_TEXTURE20 = 34004;
        public const int GL_TEXTURE21 = 34005;
        public const int GL_TEXTURE22 = 34006;
        public const int GL_TEXTURE23 = 34007;
        public const int GL_TEXTURE24 = 34008;
        public const int GL_TEXTURE25 = 34009;
        public const int GL_TEXTURE26 = 34010;
        public const int GL_TEXTURE27 = 34011;
        public const int GL_TEXTURE28 = 34012;
        public const int GL_TEXTURE29 = 34013;
        public const int GL_TEXTURE3 = 33987;
        public const int GL_TEXTURE30 = 34014;
        public const int GL_TEXTURE31 = 34015;
        public const int GL_TEXTURE4 = 33988;
        public const int GL_TEXTURE5 = 33989;
        public const int GL_TEXTURE6 = 33990;
        public const int GL_TEXTURE7 = 33991;
        public const int GL_TEXTURE8 = 33992;
        public const int GL_TEXTURE9 = 33993;
        public const int GL_TRIANGLE_FAN = 6;
        public const int GL_TRIANGLE_STRIP = 5;
        public const int GL_TRIANGLES = 4;
        public const int GL_TRUE = 1;
        public const int GL_UNPACK_ALIGNMENT = 3317;
        public const int GL_UNSIGNED_BYTE = 5121;
        public const int GL_UNSIGNED_INT = 5125;
        public const int GL_UNSIGNED_SHORT = 5123;
        public const int GL_UNSIGNED_SHORT_4_4_4_4 = 32819;
        public const int GL_UNSIGNED_SHORT_5_5_5_1 = 32820;
        public const int GL_UNSIGNED_SHORT_5_6_5 = 33635;
        public const int GL_VALIDATE_STATUS = 35715;
        public const int GL_VENDOR = 7936;
        public const int GL_VERSION = 7938;
        public const int GL_VERTEX_ATTRIB_ARRAY_BUFFER_BINDING = 34975;
        public const int GL_VERTEX_ATTRIB_ARRAY_ENABLED = 34338;
        public const int GL_VERTEX_ATTRIB_ARRAY_NORMALIZED = 34922;
        public const int GL_VERTEX_ATTRIB_ARRAY_POINTER = 34373;
        public const int GL_VERTEX_ATTRIB_ARRAY_SIZE = 34339;
        public const int GL_VERTEX_ATTRIB_ARRAY_STRIDE = 34340;
        public const int GL_VERTEX_ATTRIB_ARRAY_TYPE = 34341;
        public const int GL_VERTEX_SHADER = 35633;
        public const int GL_VIEWPORT = 2978;
        public const int GL_ZERO = 0;

        public GLES20() { }

        public static void glActiveTexture(int value) { }
        public static void glAttachShader(int arg0, int arg1) { }
        public static void glBindAttribLocation(int arg0, int arg1, string arg2) { }
        public static void glBindBuffer(int arg0, int arg1) { }
        public static void glBindFramebuffer(int arg0, int arg1) { }
        public static void glBindRenderbuffer(int arg0, int arg1) { }
        public static void glBindTexture(int arg0, int arg1) { }
        public static void glBlendColor(float arg0, float arg1, float arg2, float arg3) { }
        public static void glBlendEquation(int value) { }
        public static void glBlendEquationSeparate(int arg0, int arg1) { }
        public static void glBlendFunc(int arg0, int arg1) { }
        public static void glBlendFuncSeparate(int arg0, int arg1, int arg2, int arg3) { }
        public static void glBufferData(int arg0, int arg1, Buffer arg2, int arg3) { }
        public static void glBufferSubData(int arg0, int arg1, int arg2, Buffer arg3) { }
        public static int glCheckFramebufferStatus(int value) { return default(int); }
        public static void glClear(int value) { }
        public static void glClearColor(float arg0, float arg1, float arg2, float arg3) { }
        public static void glClearDepthf(float value) { }
        public static void glClearStencil(int value) { }
        public static void glColorMask(bool arg0, bool arg1, bool arg2, bool arg3) { }
        public static void glCompileShader(int value) { }
        public static void glCompressedTexImage2D(int arg0, int arg1, int arg2, int arg3, int arg4, int arg5, int arg6, Buffer arg7) { }
        public static void glCompressedTexSubImage2D(int arg0, int arg1, int arg2, int arg3, int arg4, int arg5, int arg6, int arg7, Buffer arg8) { }
        public static void glCopyTexImage2D(int arg0, int arg1, int arg2, int arg3, int arg4, int arg5, int arg6, int arg7) { }
        public static void glCopyTexSubImage2D(int arg0, int arg1, int arg2, int arg3, int arg4, int arg5, int arg6, int arg7) { }
        public static int glCreateProgram() { return default(int); }
        public static int glCreateShader(int value) { return default(int); }
        public static void glCullFace(int value) { }
        public static void glDeleteBuffers(int arg0, IntBuffer arg1) { }
        public static void glDeleteBuffers(int arg0, int[] arg1, int arg2) { }
        public static void glDeleteFramebuffers(int arg0, IntBuffer arg1) { }
        public static void glDeleteFramebuffers(int arg0, int[] arg1, int arg2) { }
        public static void glDeleteProgram(int value) { }
        public static void glDeleteRenderbuffers(int arg0, IntBuffer arg1) { }
        public static void glDeleteRenderbuffers(int arg0, int[] arg1, int arg2) { }
        public static void glDeleteShader(int value) { }
        public static void glDeleteTextures(int arg0, IntBuffer arg1) { }
        public static void glDeleteTextures(int arg0, int[] arg1, int arg2) { }
        public static void glDepthFunc(int value) { }
        public static void glDepthMask(bool value) { }
        public static void glDepthRangef(float arg0, float arg1) { }
        public static void glDetachShader(int arg0, int arg1) { }
        public static void glDisable(int value) { }
        public static void glDisableVertexAttribArray(int value) { }
        public static void glDrawArrays(int arg0, int arg1, int arg2) { }
        public static void glDrawElements(int arg0, int arg1, int arg2, Buffer arg3) { }
        public static void glDrawElements(int arg0, int arg1, int arg2, int arg3) { }
        public static void glEnable(int value) { }
        public static void glEnableVertexAttribArray(int value) { }
        public static void glFinish() { }
        public static void glFlush() { }
        public static void glFramebufferRenderbuffer(int arg0, int arg1, int arg2, int arg3) { }
        public static void glFramebufferTexture2D(int arg0, int arg1, int arg2, int arg3, int arg4) { }
        public static void glFrontFace(int value) { }
        public static void glGenBuffers(int arg0, IntBuffer arg1) { }
        public static void glGenBuffers(int arg0, int[] arg1, int arg2) { }
        public static void glGenerateMipmap(int value) { }
        public static void glGenFramebuffers(int arg0, IntBuffer arg1) { }
        public static void glGenFramebuffers(int arg0, int[] arg1, int arg2) { }
        public static void glGenRenderbuffers(int arg0, IntBuffer arg1) { }
        public static void glGenRenderbuffers(int arg0, int[] arg1, int arg2) { }
        public static void glGenTextures(int arg0, IntBuffer arg1) { }
        public static void glGenTextures(int arg0, int[] arg1, int arg2) { }
        public static string glGetActiveAttrib(int arg0, int arg1, IntBuffer arg2, IntBuffer arg3) { return default(string); }
        public static string glGetActiveAttrib(int arg0, int arg1, int[] arg2, int arg3, int[] arg4, int arg5) { return default(string); }
        public static void glGetActiveAttrib(int arg0, int arg1, int arg2, IntBuffer arg3, IntBuffer arg4, IntBuffer arg5, sbyte arg6) { }
        public static void glGetActiveAttrib(int arg0, int arg1, int arg2, int[] arg3, int arg4, int[] arg5, int arg6, int[] arg7, int arg8, sbyte[] arg9, int arg10) { }
        public static string glGetActiveUniform(int arg0, int arg1, IntBuffer arg2, IntBuffer arg3) { return default(string); }
        public static string glGetActiveUniform(int arg0, int arg1, int[] arg2, int arg3, int[] arg4, int arg5) { return default(string); }
        public static void glGetActiveUniform(int arg0, int arg1, int arg2, IntBuffer arg3, IntBuffer arg4, IntBuffer arg5, sbyte arg6) { }
        public static void glGetActiveUniform(int arg0, int arg1, int arg2, int[] arg3, int arg4, int[] arg5, int arg6, int[] arg7, int arg8, sbyte[] arg9, int arg10) { }
        public static void glGetAttachedShaders(int arg0, int arg1, IntBuffer arg2, IntBuffer arg3) { }
        public static void glGetAttachedShaders(int arg0, int arg1, int[] arg2, int arg3, int[] arg4, int arg5) { }
        public static int glGetAttribLocation(int arg0, string arg1) { return default(int); }
        public static void glGetBooleanv(int arg0, IntBuffer arg1) { }
        public static void glGetBooleanv(int arg0, bool[] arg1, int arg2) { }
        public static void glGetBufferParameteriv(int arg0, int arg1, IntBuffer arg2) { }
        public static void glGetBufferParameteriv(int arg0, int arg1, int[] arg2, int arg3) { }
        public static int glGetError() { return default(int); }
        public static void glGetFloatv(int arg0, FloatBuffer arg1) { }
        public static void glGetFloatv(int arg0, float[] arg1, int arg2) { }
        public static void glGetFramebufferAttachmentParameteriv(int arg0, int arg1, int arg2, IntBuffer arg3) { }
        public static void glGetFramebufferAttachmentParameteriv(int arg0, int arg1, int arg2, int[] arg3, int arg4) { }
        public static void glGetIntegerv(int arg0, IntBuffer arg1) { }
        public static void glGetIntegerv(int arg0, int[] arg1, int arg2) { }
        public static string glGetProgramInfoLog(int value) { return default(string); }
        public static void glGetProgramiv(int arg0, int arg1, IntBuffer arg2) { }
        public static void glGetProgramiv(int arg0, int arg1, int[] arg2, int arg3) { }
        public static void glGetRenderbufferParameteriv(int arg0, int arg1, IntBuffer arg2) { }
        public static void glGetRenderbufferParameteriv(int arg0, int arg1, int[] arg2, int arg3) { }
        public static string glGetShaderInfoLog(int value) { return default(string); }
        public static void glGetShaderiv(int arg0, int arg1, IntBuffer arg2) { }
        public static void glGetShaderiv(int arg0, int arg1, int[] arg2, int arg3) { }
        public static void glGetShaderPrecisionFormat(int arg0, int arg1, IntBuffer arg2, IntBuffer arg3) { }
        public static void glGetShaderPrecisionFormat(int arg0, int arg1, int[] arg2, int arg3, int[] arg4, int arg5) { }
        public static string glGetShaderSource(int value) { return default(string); }
        public static void glGetShaderSource(int arg0, int arg1, IntBuffer arg2, sbyte arg3) { }
        public static void glGetShaderSource(int arg0, int arg1, int[] arg2, int arg3, sbyte[] arg4, int arg5) { }
        public static string glGetString(int value) { return default(string); }
        public static void glGetTexParameterfv(int arg0, int arg1, FloatBuffer arg2) { }
        public static void glGetTexParameterfv(int arg0, int arg1, float[] arg2, int arg3) { }
        public static void glGetTexParameteriv(int arg0, int arg1, IntBuffer arg2) { }
        public static void glGetTexParameteriv(int arg0, int arg1, int[] arg2, int arg3) { }
        public static void glGetUniformfv(int arg0, int arg1, FloatBuffer arg2) { }
        public static void glGetUniformfv(int arg0, int arg1, float[] arg2, int arg3) { }
        public static void glGetUniformiv(int arg0, int arg1, IntBuffer arg2) { }
        public static void glGetUniformiv(int arg0, int arg1, int[] arg2, int arg3) { }
        public static int glGetUniformLocation(int arg0, string arg1) { return default(int); }
        public static void glGetVertexAttribfv(int arg0, int arg1, FloatBuffer arg2) { }
        public static void glGetVertexAttribfv(int arg0, int arg1, float[] arg2, int arg3) { }
        public static void glGetVertexAttribiv(int arg0, int arg1, IntBuffer arg2) { }
        public static void glGetVertexAttribiv(int arg0, int arg1, int[] arg2, int arg3) { }
        public static void glHint(int arg0, int arg1) { }
        public static bool glIsBuffer(int value) { return default(bool); }
        public static bool glIsEnabled(int value) { return default(bool); }
        public static bool glIsFramebuffer(int value) { return default(bool); }
        public static bool glIsProgram(int value) { return default(bool); }
        public static bool glIsRenderbuffer(int value) { return default(bool); }
        public static bool glIsShader(int value) { return default(bool); }
        public static bool glIsTexture(int value) { return default(bool); }
        public static void glLineWidth(float value) { }
        public static void glLinkProgram(int value) { }
        public static void glPixelStorei(int arg0, int arg1) { }
        public static void glPolygonOffset(float arg0, float arg1) { }
        public static void glReadPixels(int arg0, int arg1, int arg2, int arg3, int arg4, int arg5, Buffer arg6) { }
        public static void glReleaseShaderCompiler() { }
        public static void glRenderbufferStorage(int arg0, int arg1, int arg2, int arg3) { }
        public static void glSampleCoverage(float arg0, bool arg1) { }
        public static void glScissor(int arg0, int arg1, int arg2, int arg3) { }
        public static void glShaderBinary(int arg0, IntBuffer arg1, int arg2, Buffer arg3, int arg4) { }
        public static void glShaderBinary(int arg0, int[] arg1, int arg2, int arg3, Buffer arg4, int arg5) { }
        public static void glShaderSource(int arg0, string arg1) { }
        public static void glStencilFunc(int arg0, int arg1, int arg2) { }
        public static void glStencilFuncSeparate(int arg0, int arg1, int arg2, int arg3) { }
        public static void glStencilMask(int value) { }
        public static void glStencilMaskSeparate(int arg0, int arg1) { }
        public static void glStencilOp(int arg0, int arg1, int arg2) { }
        public static void glStencilOpSeparate(int arg0, int arg1, int arg2, int arg3) { }
        public static void glTexImage2D(int arg0, int arg1, int arg2, int arg3, int arg4, int arg5, int arg6, int arg7, Buffer arg8) { }
        public static void glTexParameterf(int arg0, int arg1, float arg2) { }
        public static void glTexParameterfv(int arg0, int arg1, FloatBuffer arg2) { }
        public static void glTexParameterfv(int arg0, int arg1, float[] arg2, int arg3) { }
        public static void glTexParameteri(int arg0, int arg1, int arg2) { }
        public static void glTexParameteriv(int arg0, int arg1, IntBuffer arg2) { }
        public static void glTexParameteriv(int arg0, int arg1, int[] arg2, int arg3) { }
        public static void glTexSubImage2D(int arg0, int arg1, int arg2, int arg3, int arg4, int arg5, int arg6, int arg7, Buffer arg8) { }
        public static void glUniform1f(int arg0, float arg1) { }
        public static void glUniform1fv(int arg0, int arg1, FloatBuffer arg2) { }
        public static void glUniform1fv(int arg0, int arg1, float[] arg2, int arg3) { }
        public static void glUniform1i(int arg0, int arg1) { }
        public static void glUniform1iv(int arg0, int arg1, IntBuffer arg2) { }
        public static void glUniform1iv(int arg0, int arg1, int[] arg2, int arg3) { }
        public static void glUniform2f(int arg0, float arg1, float arg2) { }
        public static void glUniform2fv(int arg0, int arg1, FloatBuffer arg2) { }
        public static void glUniform2fv(int arg0, int arg1, float[] arg2, int arg3) { }
        public static void glUniform2i(int arg0, int arg1, int arg2) { }
        public static void glUniform2iv(int arg0, int arg1, IntBuffer arg2) { }
        public static void glUniform2iv(int arg0, int arg1, int[] arg2, int arg3) { }
        public static void glUniform3f(int arg0, float arg1, float arg2, float arg3) { }
        public static void glUniform3fv(int arg0, int arg1, FloatBuffer arg2) { }
        public static void glUniform3fv(int arg0, int arg1, float[] arg2, int arg3) { }
        public static void glUniform3i(int arg0, int arg1, int arg2, int arg3) { }
        public static void glUniform3iv(int arg0, int arg1, IntBuffer arg2) { }
        public static void glUniform3iv(int arg0, int arg1, int[] arg2, int arg3) { }
        public static void glUniform4f(int arg0, float arg1, float arg2, float arg3, float arg4) { }
        public static void glUniform4fv(int arg0, int arg1, FloatBuffer arg2) { }
        public static void glUniform4fv(int arg0, int arg1, float[] arg2, int arg3) { }
        public static void glUniform4i(int arg0, int arg1, int arg2, int arg3, int arg4) { }
        public static void glUniform4iv(int arg0, int arg1, IntBuffer arg2) { }
        public static void glUniform4iv(int arg0, int arg1, int[] arg2, int arg3) { }
        public static void glUniformMatrix2fv(int arg0, int arg1, bool arg2, FloatBuffer arg3) { }
        public static void glUniformMatrix2fv(int arg0, int arg1, bool arg2, float[] arg3, int arg4) { }
        public static void glUniformMatrix3fv(int arg0, int arg1, bool arg2, FloatBuffer arg3) { }
        public static void glUniformMatrix3fv(int arg0, int arg1, bool arg2, float[] arg3, int arg4) { }
        public static void glUniformMatrix4fv(int arg0, int arg1, bool arg2, FloatBuffer arg3) { }
        public static void glUniformMatrix4fv(int arg0, int arg1, bool arg2, float[] arg3, int arg4) { }
        public static void glUseProgram(int value) { }
        public static void glValidateProgram(int value) { }
        public static void glVertexAttrib1f(int arg0, float arg1) { }
        public static void glVertexAttrib1fv(int arg0, FloatBuffer arg1) { }
        public static void glVertexAttrib1fv(int arg0, float[] arg1, int arg2) { }
        public static void glVertexAttrib2f(int arg0, float arg1, float arg2) { }
        public static void glVertexAttrib2fv(int arg0, FloatBuffer arg1) { }
        public static void glVertexAttrib2fv(int arg0, float[] arg1, int arg2) { }
        public static void glVertexAttrib3f(int arg0, float arg1, float arg2, float arg3) { }
        public static void glVertexAttrib3fv(int arg0, FloatBuffer arg1) { }
        public static void glVertexAttrib3fv(int arg0, float[] arg1, int arg2) { }
        public static void glVertexAttrib4f(int arg0, float arg1, float arg2, float arg3, float arg4) { }
        public static void glVertexAttrib4fv(int arg0, FloatBuffer arg1) { }
        public static void glVertexAttrib4fv(int arg0, float[] arg1, int arg2) { }
        public static void glVertexAttribPointer(int arg0, int arg1, int arg2, bool arg3, int arg4, Buffer arg5) { }
        public static void glVertexAttribPointer(int arg0, int arg1, int arg2, bool arg3, int arg4, int arg5) { }
        public static void glViewport(int arg0, int arg1, int arg2, int arg3) { }
    }
}
