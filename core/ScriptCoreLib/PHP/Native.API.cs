using ScriptCoreLib.Shared;
using ScriptCoreLib.PHP.Runtime;


namespace ScriptCoreLib.PHP
{

	public static partial class Native
	{
		public static class API
		{
			[Script(IsNative = true)]
			public static object unpack(string format, string data)
			{
				return null;
			}

			#region int rand ( [int min, int max] )

			/// <summary>
			/// If called without the optional min, max arguments rand() returns a pseudo-random integer between 0 and RAND_MAX. If you want a random number between 5 and 15 (inclusive), for example, use rand (5, 15).
			/// </summary>
			/// <param name="_undefined"></param>
			[Script(IsNative = true)]
			public static int rand() { return default(int); }

			#endregion



			#region bool ctype_digit ( string text )

			/// <summary>
			/// Checks if all of the characters in the provided string, text, are numerical. 
			/// </summary>
			/// <param name="_text">string text</param>
			[Script(IsNative = true)]
			public static bool ctype_digit(string _text) { return default(bool); }

			#endregion


			#region bool is_int ( mixed var )

			/// <summary>
			/// Finds whether the given variable is an integer.
			/// </summary>
			/// <param name="_var">mixed var</param>
			[Script(IsNative = true)]
			public static bool is_int(object _var) { return default(bool); }

			#endregion


			#region int fpassthru ( resource handle )

			/// <summary>
			/// Reads to EOF on the given file pointer from the current position and writes the results to the output buffer.
			/// </summary>
			/// <param name="_handle">resource handle</param>
			[Script(IsNative = true)]
			public static int fpassthru(object _handle) { return default(int); }

			#endregion


			#region bool move_uploaded_file ( string filename, string destination )

			/// <summary>
			/// This function checks to ensure that the file designated by filename is a valid upload file (meaning that it was uploaded via PHP's HTTP POST upload mechanism). If the file is valid, it will be moved to the filename given by destination. 
			/// </summary>
			/// <param name="_filename">string filename</param>
			/// <param name="_destination">string destination</param>
			[Script(IsNative = true)]
			public static bool move_uploaded_file(string _filename, string _destination) { return default(bool); }

			#endregion


			#region number pow ( number base, number exp )

			/// <summary>
			/// Returns base raised to the power of exp. If possible, this function will return an integer. 
			/// </summary>
			/// <param name="_base">number base</param>
			/// <param name="_exp">number exp</param>
			[Script(IsNative = true)]
			public static int pow(int _base, int _exp) { return default(int); }

			#endregion


			#region bool touch ( string filename [, int time [, int atime]] )

			/// <summary>
			/// Attempts to set the access and modification times of the file named in the filename parameter to the value given in time. If time is not supplied, the current system time is used. If the third parameter, is present, the access time of the given filename is set to the value of atime. Note that the access time is always modified, regardless of the number of parameters. 
			/// </summary>
			/// <param name="_filename">string filename</param>
			[Script(IsNative = true)]
			public static bool touch(string _filename) { return default(bool); }

			#endregion


			#region array scandir ( string directory [, int sorting_order [, resource context]] )

			/// <summary>
			/// Returns an array of files and directories from the directory. 
			/// </summary>
			/// <param name="_directory">string directory</param>
			[Script(IsNative = true)]
			public static string[] scandir(string _directory) { return default(string[]); }

			#endregion


			#region int filesize ( string filename )

			/// <summary>
			/// Returns the size of the file in bytes, or FALSE (and generates an error of level E_WARNING) in case of an error. 
			/// </summary>
			/// <param name="_filename">string filename</param>
			[Script(IsNative = true)]
			public static int filesize(string _filename) { return default(int); }

			#endregion


			#region string addslashes ( string str )

			/// <summary>
			/// Returns a string with backslashes before characters that need to be quoted in database queries etc. These characters are single quote ('), double quote ("), backslash (\) and NUL (the NULL byte). 
			/// </summary>
			/// <param name="_str">string str</param>
			[Script(IsNative = true)]
			public static string addslashes(string _str) { return default(string); }

			#endregion


			#region float floatval ( mixed var )

			/// <summary>
			/// Gets the float value of var. 
			/// </summary>
			/// <param name="_var">mixed var</param>
			[Script(IsNative = true)]
			public static object floatval(object _var) { return default(object); }

			#endregion

			#region string session_id ( [string id] )

			/// <summary>
			/// session_id() is used to get or set the session id for the current session. 
			/// </summary>
			/// <param name="_undefined"></param>
			[Script(IsNative = true)]
			public static string session_id() { return default(string); }

			#endregion

			[Script(IsNative = true)]
			public static string sleep(int seconds) { return default(string); }
			[Script(IsNative = true)]
			public static string usleep(int nanoseconds) { return default(string); }

			#region string gmdate ( string format , int timestamp )

			/// <summary>
			/// Identical to the date() function except that the time returned is Greenwich Mean Time (GMT). For example, when run in Finland (GMT +0200), the first line below prints "Jan 01 1998 00:00:00", while the second prints "Dec 31 1997 22:00:00".
			/// </summary>
			/// <param name="_format">string format</param>
			/// <param name="_timestamp">int timestamp</param>
			[Script(IsNative = true)]
			public static string gmdate(string _format, int _timestamp) { return default(string); }

			#endregion


			#region string gmdate ( string format [, int timestamp] )

			/// <summary>
			/// Identical to the date() function except that the time returned is Greenwich Mean Time (GMT). For example, when run in Finland (GMT +0200), the first line below prints "Jan 01 1998 00:00:00", while the second prints "Dec 31 1997 22:00:00".
			/// </summary>
			/// <param name="_format">string format</param>
			[Script(IsNative = true)]
			public static string gmdate(string _format) { return default(string); }

			#endregion


			#region string nl2br ( string string )

			/// <summary>
			/// Returns string with '&lt;br /&gt;' inserted before all newlines. 
			/// </summary>
			/// <param name="_string">string string</param>
			[Script(IsNative = true)]
			public static string nl2br(string _string) { return default(string); }

			#endregion

			#region bool ob_end_flush ( void )

			/// <summary>
			/// This function will send the contents of the topmost output buffer (if any) and turn this output buffer off. If you want to further process the buffer's contents you have to call ob_get_contents() before ob_end_flush() as the buffer contents are discarded after ob_end_flush() is called. The function returns TRUE when it successfully discarded one buffer and FALSE otherwise. Reasons for failure are first that you called the function without an active buffer or that for some reason a buffer could not be deleted (possible for special buffer). 
			/// </summary>
			/// <param name="_undefined">void</param>
			[Script(IsNative = true)]
			public static bool ob_end_flush() { return default(bool); }

			#endregion


			#region bool ob_end_clean ( void )

			/// <summary>
			/// This function discards the contents of the topmost output buffer and turns off this output buffering. If you want to further process the buffer's contents you have to call ob_get_contents() before ob_end_clean() as the buffer contents are discarded when ob_end_clean() is called. The function returns TRUE when it successfully discarded one buffer and FALSE otherwise. Reasons for failure are first that you called the function without an active buffer or that for some reason a buffer could not be deleted (possible for special buffer). 
			/// </summary>
			/// <param name="_undefined">void</param>
			[Script(IsNative = true)]
			public static bool ob_end_clean() { return default(bool); }

			#endregion


			#region void ob_clean ( void )

			/// <summary>
			/// This function discards the contents of the output buffer. 
			/// </summary>
			/// <param name="_undefined">void</param>
			[Script(IsNative = true)]
			public static void ob_clean() { }

			#endregion


			#region void clearstatcache ( void )

			/// <summary>
			/// When you use stat(), lstat(), or any of the other functions listed in the affected functions list (below), PHP caches the information those functions return in order to provide faster performance. However, in certain cases, you may want to clear the cached information. For instance, if the same file is being checked multiple times within a single script, and that file is in danger of being removed or changed during that script's operation, you may elect to clear the status cache. In these cases, you can use the clearstatcache() function to clear the information that PHP caches about a file. 
			/// </summary>
			/// <param name="_undefined">void</param>
			[Script(IsNative = true)]
			public static void clearstatcache() { }

			#endregion


			#region int filemtime ( string filename )

			/// <summary>
			/// Returns the time the file was last modified, or FALSE in case of an error. The time is returned as a Unix timestamp, which is suitable for the date() function. 
			/// </summary>
			/// <param name="_filename">string filename</param>
			[Script(IsNative = true)]
			public static int filemtime(string _filename) { return default(int); }

			#endregion


			#region string dirname ( string path )

			/// <summary>
			/// Given a string containing a path to a file, this function will return the name of the directory. 
			/// </summary>
			/// <param name="_path">string path</param>
			[Script(IsNative = true)]
			public static string dirname(string _path) { return default(string); }

			#endregion

			#region string ob_get_contents ( void )

			/// <summary>
			/// This will return the contents of the output buffer without clearing it or FALSE, if output buffering isn't active. 
			/// </summary>
			/// <param name="_undefined">void</param>
			[Script(IsNative = true)]
			public static string ob_get_contents() { return default(string); }

			#endregion


			#region nt time ( void )

			/// <summary>
			/// Returns the current time measured in the number of seconds since the Unix Epoch (January 1 1970 00:00:00 GMT). 
			/// </summary>
			/// <param name="_undefined">void</param>
			[Script(IsNative = true)]
			public static int time() { return default(int); }

			#endregion

			[Script(IsNative = true)]
			public static double microtime(bool _get_as_float) { return default(int); }

			#region mixed preg_replace ( mixed pattern, mixed replacement, mixed subject [, int limit [, int &count]] )

			/// <summary>
			/// Searches subject for matches to pattern and replaces them with replacement. 
			/// </summary>
			/// <param name="_pattern">mixed pattern</param>
			/// <param name="_replacement">mixed replacement</param>
			/// <param name="_subject">mixed subject</param>
			[Script(IsNative = true)]
			public static string preg_replace(object _pattern, object _replacement, object _subject) { return default(string); }

			#endregion



			#region string htmlspecialchars_decode ( string string [, int quote_style] )

			/// <summary>
			/// This function is the opposite of htmlspecialchars(). It converts special HTML entities back to characters. 
			/// </summary>
			/// <param name="_string">string string</param>
			[Script(OptimizedCode = @"return strtr($_string, array_flip(get_html_translation_table(HTML_ENTITIES, ENT_QUOTES)));")]
			public static string htmlspecialchars_decode(string _string) { return default(string); }

			#endregion


			#region int ob_get_length ( void )

			/// <summary>
			/// This will return the length of the contents in the output buffer or FALSE, if output buffering isn't active. 
			/// </summary>
			/// <param name="_undefined">void</param>
			[Script(IsNative = true)]
			public static int ob_get_length() { return default(int); }

			#endregion


			#region bool in_array ( mixed needle, array haystack [, bool strict] )

			/// <summary>
			/// Searches haystack for needle and returns TRUE if it is found in the array, FALSE otherwise. 
			/// </summary>
			/// <param name="_needle">mixed needle</param>
			/// <param name="_haystack">array haystack</param>
			[Script(IsNative = true)]
			public static bool in_array(object _needle, object _haystack) { return default(bool); }

			#endregion


			#region string stripslashes ( string str )

			/// <summary>
			/// Returns a string with backslashes stripped off. (\' becomes ' and so on.) Double backslashes (\\) are made into a single backslash (\). 
			/// </summary>
			/// <param name="_str">string str</param>
			[Script(IsNative = true)]
			public static string stripslashes(string _str) { return default(string); }

			#endregion


			[Script(IsNative = true)]
			public static int strpos(string haystack, string needle)
			{
				return default(int);
			}

			#region string implode ( string glue, array pieces )

			/// <summary>
			/// Returns a string containing a string representation of all the array elements in the same order, with the glue string between each element.
			/// </summary>
			/// <param name="_glue">string glue</param>
			/// <param name="_pieces">array pieces</param>
			[Script(IsNative = true)]
			public static string implode(string _glue, object _pieces) { return default(string); }

			#endregion


			#region bool is_readable ( string filename )

			/// <summary>
			/// Returns TRUE if the file or directory specified by filename exists and is readable. 
			/// </summary>
			/// <param name="_filename">string filename</param>
			[Script(IsNative = true)]
			public static bool is_readable(string _filename) { return default(bool); }

			#endregion


			#region bool is_file ( string filename )

			/// <summary>
			/// Returns TRUE if the filename exists and is a regular file. 
			/// </summary>
			/// <param name="_filename">string filename</param>
			[Script(IsNative = true)]
			public static bool is_file(string _filename) { return default(bool); }

			#endregion

			[Script(IsNative = true)]
			public static string getcwd() { return default(string); }




			#region bool mkdir ( string pathname [, int mode [, bool recursive [, resource context]]] )

			/// <summary>
			/// Attempts to create the directory specified by pathname. 
			/// </summary>
			/// <param name="_pathname">string pathname</param>
			[Script(IsNative = true)]
			public static bool mkdir(string _pathname) { return default(bool); }

			#endregion


			#region bool imagedestroy ( resource image )

			/// <summary>
			/// imagedestroy() frees any memory associated with image image. image is the image identifier returned by one of the image create functions, such as imagecreatetruecolor(). 
			/// </summary>
			/// <param name="_image">resource image</param>
			[Script(IsNative = true)]
			public static bool imagedestroy(object _image) { return default(bool); }

			#endregion


			#region bool imagejpeg ( resource image , string filename , int quality )

			/// <summary>
			/// imagejpeg() creates the JPEG file in filename from the image image. The image argument is the return from the imagecreatetruecolor() function. 
			/// </summary>
			/// <param name="_image">resource image</param>
			/// <param name="_filename">string filename</param>
			/// <param name="_quality">int quality</param>
			[Script(IsNative = true)]
			public static bool imagejpeg(object _image, string _filename, int _quality) { return default(bool); }

			#endregion


			#region bool imagejpeg ( resource image [, string filename [, int quality]] )

			/// <summary>
			/// imagejpeg() creates the JPEG file in filename from the image image. The image argument is the return from the imagecreatetruecolor() function. 
			/// </summary>
			/// <param name="_image">resource image</param>
			[Script(IsNative = true)]
			public static bool imagejpeg(object _image) { return default(bool); }

			#endregion


			#region bool imagecopyresampled ( resource dst_image, resource src_image, int dst_x, int dst_y, int src_x, int src_y, int dst_w, int dst_h, int src_w, int src_h )

			/// <summary>
			/// imagecopyresampled() copies a rectangular portion of one image to another image, smoothly interpolating pixel values so that, in particular, reducing the size of an image still retains a great deal of clarity. Returns TRUE on success or FALSE on failure. 
			/// </summary>
			/// <param name="_dst_image">resource dst_image</param>
			/// <param name="_src_image">resource src_image</param>
			/// <param name="_dst_x">int dst_x</param>
			/// <param name="_dst_y">int dst_y</param>
			/// <param name="_src_x">int src_x</param>
			/// <param name="_src_y">int src_y</param>
			/// <param name="_dst_w">int dst_w</param>
			/// <param name="_dst_h">int dst_h</param>
			/// <param name="_src_w">int src_w</param>
			/// <param name="_src_h">int src_h</param>
			[Script(IsNative = true)]
			public static bool imagecopyresampled(object _dst_image, object _src_image, int _dst_x, int _dst_y, int _src_x, int _src_y, int _dst_w, int _dst_h, int _src_w, int _src_h) { return default(bool); }

			#endregion


			#region resource imagecreatetruecolor ( int x_size, int y_size )

			/// <summary>
			/// imagecreatetruecolor() returns an image identifier representing a black image of size x_size by y_size. 
			/// </summary>
			/// <param name="_x_size">int x_size</param>
			/// <param name="_y_size">int y_size</param>
			[Script(IsNative = true)]
			public static object imagecreatetruecolor(int _x_size, int _y_size) { return default(object); }

			#endregion


			#region resource imagecreatefromjpeg ( string filename )

			/// <summary>
			/// imagecreatefromjpeg() returns an image identifier representing the image obtained from the given filename. 
			/// </summary>
			/// <param name="_filename">string filename</param>
			[Script(IsNative = true)]
			public static object imagecreatefromjpeg(string _filename) { return default(object); }

			#endregion


			#region array getimagesize ( string filename [, array &imageinfo] )

			/// <summary>
			/// The getimagesize() function will determine the size of any GIF, JPG, PNG, SWF, SWC, PSD, TIFF, BMP, IFF, JP2, JPX, JB2, JPC, XBM, or WBMP image file and return the dimensions along with the file type and a height/width text string to be used inside a normal HTML &lt;IMG&gt; tag. 
			/// </summary>
			/// <param name="_filename">string filename</param>
			[Script(IsNative = true)]
			public static object[] getimagesize(string _filename) { return default(object[]); }

			#endregion


			#region string md5_file ( string filename [, bool raw_output] )

			/// <summary>
			/// Calculates the MD5 hash of the file specified by the filename parameter using the RSA Data Security, Inc. MD5 Message-Digest Algorithm, and returns that hash. The hash is a 32-character hexadecimal number. 
			/// </summary>
			/// <param name="_filename">string filename</param>
			[Script(IsNative = true)]
			public static string md5_file(string _filename) { return default(string); }

			#endregion

			#region string md5 ( string str [, bool raw_output] )

			/// <summary>
			/// Calculates the MD5 hash of str using the RSA Data Security, Inc. MD5 Message-Digest Algorithm, and returns that hash. The hash is a 32-character hexadecimal number. If the optional raw_output is set to TRUE, then the md5 digest is instead returned in raw binary format with a length of 16. 
			/// </summary>
			/// <param name="_str">string str</param>
			[Script(IsNative = true)]
			public static string md5(string _str) { return default(string); }

			#endregion


			#region string basename ( string path [, string suffix] )

			/// <summary>
			/// 
			/// </summary>
			/// <param name="_path">string path</param>
			[Script(IsNative = true)]
			public static string basename(string _path) { return default(string); }

			#endregion


			#region void closedir ( resource dir_handle )

			/// <summary>
			/// Closes the directory stream indicated by dir_handle. The stream must have previously been opened by opendir(). 
			/// </summary>
			/// <param name="_dir_handle">resource dir_handle</param>
			[Script(IsNative = true)]
			public static void closedir(object _dir_handle) { }

			#endregion


			#region bool is_dir ( string filename )

			/// <summary>
			/// Returns TRUE if the filename exists and is a directory. If filename is a relative filename, it will be checked relative to the current working directory. 
			/// </summary>
			/// <param name="_filename">string filename</param>
			[Script(IsNative = true)]
			public static bool is_dir(string _filename) { return default(bool); }

			#endregion


			#region string readdir ( resource dir_handle )

			/// <summary>
			/// Returns the filename of the next file from the directory. The filenames are returned in the order in which they are stored by the filesystem. 
			/// </summary>
			/// <param name="_dir_handle">resource dir_handle</param>
			[Script(IsNative = true)]
			public static string readdir(object _dir_handle) { return default(string); }

			#endregion

			[Script(IsNative = true)]
			public static string realpath(string path) { return default(string); }

			#region resource opendir ( string path [, resource context] )

			/// <summary>
			/// Opens up a directory handle to be used in subsequent closedir(), readdir(), and rewinddir() calls. 
			/// </summary>
			/// <param name="_path">string path</param>
			[Script(IsNative = true)]
			public static object opendir(string _path) { return default(object); }

			#endregion


			#region array get_object_vars ( object obj )

			/// <summary>
			/// 
			/// </summary>
			/// <param name="_obj">object obj</param>
			[Script(IsNative = true)]
			public static IArray<string> get_object_vars(object _obj) { return default(IArray<string>); }

			#endregion


			#region string chr ( int ascii )

			/// <summary>
			/// Returns a one-character string containing the character specified by ascii.
			/// </summary>
			/// <param name="_ascii">int ascii</param>
			[Script(IsNative = true)]
			public static string chr(int _ascii) { return default(string); }

			#endregion


			#region string date ( string format, int timestamp )

			/// <summary>
			/// Returns a string formatted according to the given format string using the given integer timestamp or the current local time if no timestamp is given. In other words, timestamp is optional and defaults to the value of time(). 
			/// </summary>
			/// <param name="_format">string format</param>
			[Script(IsNative = true)]
			public static string date(string _format, int timestamp) { return default(string); }

			#endregion

			#region string date ( string format [, int timestamp] )

			/// <summary>
			/// Returns a string formatted according to the given format string using the given integer timestamp or the current local time if no timestamp is given. In other words, timestamp is optional and defaults to the value of time(). 
			/// </summary>
			/// <param name="_format">string format</param>
			[Script(IsNative = true)]
			public static string date(string _format) { return default(string); }

			#endregion


			#region bool fclose ( resource handle )

			/// <summary>
			/// The file pointed to by handle is closed. 
			/// </summary>
			/// <param name="_handle">resource handle</param>
			[Script(IsNative = true)]
			public static bool fclose(object _handle) { return default(bool); }

			#endregion

			[Script(IsNative = true)]
			public static bool ftruncate(object _handle, int _size) { return default(bool); }

			[Script(IsNative = true)]
			public static string fgets(object _handle, int _length) { return default(string); }

			[Script(IsNative = true)]
			public static string fread(object _handle, int _length) { return default(string); }


			[Script(IsNative = true)]
			public static bool feof(object _handle) { return default(bool); }

			[Script(IsNative = true)]
			public static object fsockopen(string _hostname, int _port) { return default(object); }

			#region resource fopen ( string filename, string mode [, bool use_include_path [, resource zcontext]] )

			/// <summary>
			/// fopen() binds a named resource, specified by filename, to a stream. If filename is of the form "scheme://...", it is assumed to be a URL and PHP will search for a protocol handler (also known as a wrapper) for that scheme. If no wrappers for that protocol are registered, PHP will emit a notice to help you track potential problems in your script and then continue as though filename specifies a regular file. 
			/// </summary>
			/// <param name="_filename">string filename</param>
			/// <param name="_mode">string mode</param>
			[Script(IsNative = true)]
			public static object fopen(string _filename, string _mode) { return default(object); }

			[Script(IsNative = true)]
			public static int fseek(object _handle, int _offset, int _whence) { return default(int); }


			#endregion

			#region int fwrite ( resource handle, string string [, int length] )

			/// <summary>
			/// fwrite() writes the contents of string to the file stream pointed to by handle. If the length argument is given, writing will stop after length bytes have been written or the end of string is reached, whichever comes first. 
			/// </summary>
			/// <param name="_handle">resource handle</param>
			/// <param name="_string">string string</param>
			[Script(IsNative = true)]
			public static int fwrite(object _handle, string _string) { return default(int); }

			#endregion


			#region array explode ( string separator, string string [, int limit] )

			/// <summary>
			/// Returns an array of strings, each of which is a substring of string formed by splitting it on boundaries formed by the string separator. If limit is set, the returned array will contain a maximum of limit elements with the last element containing the rest of string. 
			/// </summary>
			/// <param name="_separator">string separator</param>
			/// <param name="_string">string string</param>
			[Script(IsNative = true)]
			public static string[] explode(object _separator, object _string) { return default(string[]); }

			#endregion

			#region int intval ( mixed var [, int base] )

			/// <summary>
			/// Returns the integer value of var, using the specified base for the conversion (the default is base 10). 
			/// </summary>
			/// <param name="_var">mixed var</param>
			[Script(IsNative = true)]
			public static int intval(object _var) { return default(int); }

			#endregion


			#region bool phpinfo ( [int what] )

			/// <summary>
			/// Outputs a large amount of information about the current state of PHP. This includes information about PHP compilation options and extensions, the PHP version, server information and environment (if compiled as a module), the PHP environment, OS version information, paths, master and local values of configuration options, HTTP headers, and the PHP License. 
			/// </summary>
			[Script(IsNative = true)]
			public static bool phpinfo() { return default(bool); }

			#endregion


			#region mixed highlight_file ( string filename [, bool return] )

			/// <summary>  
			/// The highlight_file() function prints out a syntax highlighted version of the code contained in filename using the colors defined in the built-in syntax highlighter for PHP.   
			/// </summary>  
			/// <param name="_filename">string filename</param>  
			[Script(IsNative = true)]
			public static bool highlight_file(string _filename) { return default(bool); }

			#endregion

			#region int preg_match_all ( string pattern, string subject, array &matches [, int flags [, int offset]] )

			/// <summary>  
			/// Searches subject for all matches to the regular expression given in pattern and puts them in matches in the order specified by flags.   
			/// </summary>  
			/// <param name="_pattern">string pattern</param>  
			/// <param name="_subject">string subject</param>  
			/// <param name="_&matches">array &amp;matches</param>  
			[Script(IsNative = true)]
			public static int preg_match_all(string _pattern, string _subject, object[] _matches) { return default(int); }

			#endregion


			#region int preg_match ( string pattern, string subject, array &matches [, int flags [, int offset]]] )

			/// <summary>  
			/// Searches subject for a match to the regular expression given in pattern.   
			/// </summary>  
			/// <param name="_pattern">string pattern</param>  
			/// <param name="_subject">string subject</param>  
			/// <param name="_&matches">array &amp;matches</param>  
			[Script(IsNative = true)]
			public static int preg_match(string _pattern, string _subject, [ScriptParameterByRef] object[] _matches) { return default(int); }

			#endregion


			#region int ignore_user_abort ( bool setting )

			/// <summary>  
			/// This function sets whether a client disconnect should cause a script to be aborted. It will return the previous setting and can be called without an argument to not change the current setting and only return the current setting. See the Connection Handling section in the Features chapter for a complete description of connection handling in PHP.   
			/// </summary>  
			/// <param name="_setting">bool setting</param>  
			[Script(IsNative = true)]
			public static int ignore_user_abort(bool _setting) { return default(int); }

			#endregion



			#region void set_time_limit ( int seconds )

			/// <summary>  
			/// Set the number of seconds a script is allowed to run. If this is reached, the script returns a fatal error. The default limit is 30 seconds or, if it exists, the max_execution_time value defined in the php.ini. If seconds is set to zero, no time limit is imposed.   
			/// </summary>  
			/// <param name="_seconds">int seconds</param>  
			[Script(IsNative = true)]
			public static void set_time_limit(int _seconds) { }

			#endregion


			#region string sprintf ( string format [, mixed args [, mixed ...]] )

			/// <summary>  
			/// Returns a string produced according to the formatting string format.   
			/// </summary>  
			/// <param name="_format">string format</param>  
			[Script(IsNative = true)]
			public static string sprintf(string _format) { return default(string); }

			#endregion


			#region string iconv ( string in_charset, string out_charset, string str )

			/// <summary>  
			/// Performs a character set conversion on the string str from in_charset to out_charset. Returns the converted string or FALSE on failure.   
			/// </summary>  
			/// <param name="_in_charset">string in_charset</param>  
			/// <param name="_out_charset">string out_charset</param>  
			/// <param name="_str">string str</param>  
			[Script(IsNative = true)]
			public static string iconv(string _in_charset, string _out_charset, object _str) { return default(string); }

			#endregion


			#region bool function_exists ( string function_name )

			/// <summary>  
			/// Checks the list of defined functions, both built-in (internal) and user-defined, for function_name. Returns TRUE on success or FALSE on failure.   
			/// </summary>  
			/// <param name="_function_name">string function_name</param>  
			[Script(IsNative = true)]
			public static bool function_exists(string _function_name) { return default(bool); }

			#endregion





			#region void flush ( void )

			/// <summary>  
			/// Flushes the output buffers of PHP and whatever backend PHP is using (CGI, a web server, etc). This effectively tries to push all the output so far to the user's browser.   
			/// </summary>  
			[Script(IsNative = true)]
			public static void flush() { }

			#endregion


			#region string utf8_encode ( string data )

			/// <summary>  
			/// This function encodes the string data to UTF-8, and returns the encoded version. UTF-8 is a standard mechanism used by Unicode for encoding wide character values into a byte stream. UTF-8 is transparent to plain ASCII characters, is self-synchronized (meaning it is possible for a program to figure out where in the bytestream characters start) and can be used with normal string comparison functions for sorting and such. PHP encodes UTF-8 characters in up to four bytes, like this:   
			/// </summary>  
			/// <param name="_data">string data</param>  
			[Script(IsNative = true)]
			public static string utf8_encode(object _data) { return default(string); }

			#endregion

			#region string utf8_decode ( string data )

			/// <summary>  
			/// This function decodes data, assumed to be UTF-8 encoded, to ISO-8859-1.   
			/// </summary>  
			/// <param name="_data">string data</param>  
			[Script(IsNative = true)]
			public static string utf8_decode(object _data) { return default(string); }

			#endregion

			#region int error_reporting ( int level )

			/// <summary>  
			/// The error_reporting() function sets the error_reporting directive at runtime. PHP has many levels of errors, using this function sets that level for the duration (runtime) of your script.   
			/// </summary>  
			/// <param name="_level">int level</param>  
			[Script(IsNative = true)]
			public static int error_reporting(int _level) { return default(int); }

			#endregion

			#region mixed set_error_handler ( callback error_handler [, int error_types] )

			/// <summary>
			/// Sets a user function (error_handler) to handle errors in a script.
			/// </summary>
			/// <param name="_error_handler">callback error_handler</param>
			[Script(IsNative = true)]
			public static string set_error_handler(string _error_handler) { return default(string); }

			#endregion



			#region void header ( string string [, bool replace [, int http_response_code]] )

			/// <summary>  
			/// header() is used to send raw HTTP headers. See the HTTP/1.1 specification for more information on HTTP headers.   
			/// </summary>  
			/// <param name="_string">string string</param>  
			[Script(IsNative = true)]
			public static void header(string _string) { }

			#endregion


			#region string urldecode ( string str )

			/// <summary>  
			/// Decodes any %## encoding in the given string. The decoded string is returned.  
			/// </summary>  
			/// <param name="_str">string str</param>  
			[Script(IsNative = true)]
			public static string urldecode(string _str) { return default(string); }

			#endregion


			#region string urlencode ( string str )

			/// <summary>  
			/// Returns a string in which all non-alphanumeric characters except -_. have been replaced with a percent (%) sign followed by two hex digits and spaces encoded as plus (+) signs. It is encoded the same way that the posted data from a WWW form is encoded, that is the same way as in application/x-www-form-urlencoded media type. This differs from the RFC1738 encoding (see rawurlencode()) in that for historical reasons, spaces are encoded as plus (+) signs. This function is convenient when encoding a string to be used in a query part of a URL, as a convenient way to pass variables to the next page:  
			/// </summary>  
			/// <param name="_str">string str</param>  
			[Script(IsNative = true)]
			public static string urlencode(string _str) { return default(string); }

			#endregion


			#region string rawurlencode ( string str )

			/// <summary>  
			/// Returns a string in which all non-alphanumeric characters except -_. have been replaced with a percent (%) sign followed by two hex digits. This is the encoding described in RFC 1738 for protecting literal characters from being interpreted as special URL delimiters, and for protecting URLs from being mangled by transmission media with character conversions (like some email systems). For example, if you want to include a password in an FTP URL:   
			/// </summary>  
			/// <param name="_str">string str</param>  
			[Script(IsNative = true)]
			public static string rawurlencode(string _str) { return default(string); }

			#endregion


			#region string rawurldecode ( string str )

			/// <summary>  
			/// Returns a string in which the sequences with percent (%) signs followed by two hex digits have been replaced with literal characters.   
			/// </summary>  
			/// <param name="_str">string str</param>  
			[Script(IsNative = true)]
			public static string rawurldecode(string _str) { return default(string); }

			#endregion


			#region string base64_encode ( string data )

			/// <summary>  
			/// base64_encode() returns data encoded with base64. This encoding is designed to make binary data survive transport through transport layers that are not 8-bit clean, such as mail bodies.   
			/// </summary>  
			/// <param name="_data">string data</param>  
			[Script(IsNative = true)]
			public static string base64_encode([ScriptParameterByRef] string _data) { return default(string); }

			#endregion


			#region string base64_decode ( string encoded_data )

			/// <summary>  
			/// base64_decode() decodes encoded_data and returns the original data or FALSE on failure. The returned data may be binary.   
			/// </summary>  
			/// <param name="_encoded_data">string encoded_data</param>  
			[Script(IsNative = true)]
			public static string base64_decode([ScriptParameterByRef] string _encoded_data) { return default(string); }

			#endregion


			#region mixed highlight_string ( string str [, bool return] )

			/// <summary>  
			/// The highlight_string() function outputs a syntax highlighted version of str using the colors defined in the built-in syntax highlighter for PHP.   
			/// </summary>  
			/// <param name="_str">string str</param>  
			[Script(IsNative = true)]
			public static object highlight_string([ScriptParameterByRef] string _str) { return default(object); }

			#endregion


			#region string file_get_contents ( string filename [, bool use_include_path [, resource context [, int offset [, int maxlen]]]] )

			/// <summary>  
			/// Identical to file(), except that file_get_contents() returns the file in a string, starting at the specified offset up to maxlen bytes. On failure, file_get_contents() will return FALSE.   
			/// </summary>  
			/// <param name="_filename">string filename</param>  
			[Script(IsNative = true)]
			public static string file_get_contents(string _filename) { return default(string); }

			#endregion

			[Script(IsNative = true)]
			public static int file_put_contents(string _filename, string data) { return default(int); }


			#region string serialize ( mixed value )

			/// <summary>  
			/// serialize() returns a string containing a byte-stream representation of value that can be stored anywhere.   
			/// </summary>  
			/// <param name="_value">mixed value</param>  
			[Script(IsNative = true)]
			public static string serialize(object _value) { return default(string); }

			#endregion

			#region mixed unserialize ( string str )

			/// <summary>  
			/// unserialize() takes a single serialized variable (see serialize()) and converts it back into a PHP value. The converted value is returned, and can be a boolean, integer, float, string, array or object. In case the passed string is not unserializeable, FALSE is returned and E_NOTICE is issued.   
			/// </summary>  
			/// <param name="_str">string str</param>  
			[Script(IsNative = true)]
			public static object unserialize(string _str) { return default(object); }

			#endregion

			#region bool ob_start

			/// <summary>
			/// This function will turn output buffering on. While output buffering is active no output is sent from the script (other than headers), instead the output is stored in an internal buffer. 
			/// </summary>

			[Script(IsNative = true)]
			public static bool ob_start() { return default(bool); }

			#endregion

			#region string ob_get_clean

			/// <summary>
			/// This will return the contents of the output buffer and end output buffering. If output buffering isn't active then FALSE is returned. ob_get_clean() essentially executes both ob_get_contents() and ob_end_clean(). 
			/// </summary>
			[Script(IsNative = true)]
			public static string ob_get_clean() { return default(string); }

			#endregion

			#region void exit ( )

			/// <summary>  
			/// The exit() function terminates execution of the script. It prints status just before exiting.   
			/// </summary>  
			[Script(IsNative = true)]
			public static void exit() { }

			#endregion


			#region void var_dump

			/// <summary>
			/// This function displays structured information about one or more expressions that includes its type and value. Arrays and objects are explored recursively with values indented to show structure. 
			/// </summary>
			/// <param name="_mixed">mixed expression </param>
			[Script(IsNative = true)]
			public static void var_dump(object _mixed) { }

			#endregion

			#region int ord

			/// <summary>
			/// Returns the ASCII value of the first character of string. This function complements chr().
			/// </summary>
			/// <param name="_string">string string</param>
			[Script(IsNative = true)]
			public static char ord(string _string) { return default(char); }

			#endregion

			#region string dechex ( int number )

			/// <summary>  
			/// Returns a string containing a hexadecimal representation of the given number argument. The largest number that can be converted is 4294967295 in decimal resulting to "ffffffff".   
			/// </summary>  
			/// <param name="_number">int number</param>  
			[Script(IsNative = true)]
			public static string dechex(int _number) { return default(string); }


			#endregion


			#region number hexdec ( string hex_string )

			/// <summary>  
			/// Returns the decimal equivalent of the hexadecimal number represented by the hex_string argument. hexdec() converts a hexadecimal string to a decimal number. The largest number that can be converted is 7fffffff or 2147483647 in decimal. As of PHP 4.1.0, this function can also convert larger numbers. It returns float in that case.   
			/// </summary>  
			/// <param name="_hex_string">string hex_string</param>  
			[Script(IsNative = true)]
			public static int hexdec(string _hex_string) { return default(int); }


			#endregion


			#region string htmlspecialchars ( string string [, int quote_style [, string charset]] )

			/// <summary>  
			/// Certain characters have special significance in HTML, and should be represented by HTML entities if they are to preserve their meanings. This function returns a string with some of these conversions made; the translations made are those most useful for everyday web programming. If you require all HTML character entities to be translated, use htmlentities() instead.   
			/// </summary>  
			/// <param name="_string">string string</param>  
			[Script(IsNative = true)]
			public static string htmlspecialchars(string _string) { return default(string); }

			#endregion

			#region string htmlentities ( string string [, int quote_style [, string charset]] )

			/// <summary>  
			/// This function is identical to htmlspecialchars() in all ways, except with htmlentities(), all characters which have HTML character entity equivalents are translated into these entities.   
			/// </summary>  
			/// <param name="_string">string string</param>  
			[Script(IsNative = true)]
			public static string htmlentities(string _string) { return default(string); }

			#endregion


			[Script(IsNative = true)]
			public static void session_start()
			{
			}

			[Script(IsNative = true)]
			public static void session_write_close()
			{
			}

			[Script(IsNative = true)]
			public static void unlink(string path)
			{

			}

			[Script(IsNative = true)]
			public static void rmdir(string path)
			{

			}
		}



		public static void WebApplicationPlaceholder(string p)
		{
			echo("<div class='" + p + "' ></div>");
		}

		internal static void Break(string p)
		{
			Native.Error(p);

			Native.exit();
		}

		public static string LogFileName;
		public static LogLevelEnum LogLevel;

		public enum LogLevelEnum
		{
			None,
			Low,
			Medium,
			High
		}

		public static void Trace(string p)
		{
			Log("trace: " + p);
		}

		public static void Log(string p)
		{
			Log(p, LogLevelEnum.Medium);
		}

		public static string DateTimeFormat = "d.m.Y H:i:s";

		public static string DateTime
		{
			get
			{
				return Native.API.date(DateTimeFormat);
			}
		}

		public static void Log(string p, LogLevelEnum level)
		{
			string x = LogFileName;

			if (x == null)
				x = Native.ScriptFileName + ".log";


			if (level <= LogLevel)
			{
				object f = Native.API.fopen(x, "a");

				Native.API.fwrite(f,
					DateTime + "\t" + p + "\r\n");

				Native.API.fclose(f);
			}
		}

		public static void Redirect(string p)
		{
			Native.Log("Redirect from {" + Native.SuperGlobals.Server[SuperGlobals.ServerVariables.REQUEST_URI] + "} to {" + p + "}");

			Native.header("Location: " + p);
			Native.exit();
		}

		public static void Redirect()
		{
			Redirect("?" + QueryString);
		}



		public static void DumpToLog(object e)
		{
			Log(DumpToString(e));
		}

		public static void Terminate()
		{
			Native.Log("Terminate: No Content");

			Native.header("HTTP/1.1 204 No Content");
			Native.exit();
		}

		public static void WriteFormTemplate()
		{
			Native.echo("<form id='" + Helper.FormTemplateID + "' method='post' enctype='multipart/form-data'></form>");
		}

		public static void WebApplicationPlaceholder(string p, string content)
		{
			Native.echo("<div class='" + p + "' >" + content + "</div>");

		}

		public static void SetContentType(string p, string p_2)
		{
			Native.API.header("Content-type: " + p + "; charset=" + p_2);

		}

		public static void SetContentType(string p)
		{
			Native.API.header("Content-type: " + p);

		}

		private static void SetDefaultTimezone()
		{
			MethodInfo m = MethodInfo.Of("date_default_timezone_set");

			if (m.Exists)
			{
				m.Invoke<bool, string>("UTC");
			}
		}

		/// <summary>
		/// defines script import for *.dll.js or *.dll.js.packed.js
		/// </summary>
		/// <param name="p"></param>
		/// <param name="b"></param>
		public static void Script(string p, bool b)
		{
			if (b)
				Script(p + ".dll.js.packed.js");
			else
				Script(p + ".dll.js");
		}
	}
}
