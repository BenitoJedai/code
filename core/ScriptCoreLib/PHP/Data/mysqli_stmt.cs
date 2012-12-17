using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.PHP.Data
{
    // http://php.net/manual/en/class.mysqli-stmt.php
    [Script(IsNative = true)]
    class mysqli_stmt
    {
        // http://php.net/manual/en/mysqli-stmt.bind-param.php
        public bool bind_param(string types, object var1)
        {
            return default(bool);
        }

        public bool close()
        {
            return default(bool);
        }

        public bool execute()
        {
            return default(bool);
        }

        public bool fetch()
        {
            return default(bool);
        }

        public object get_result()
        {
            return default(object);
        }

        public int field_count;
        public int num_rows;

        public int insert_id;

        public int errno;
        public string error;
    }

    [Script]
    static class __mysqli_stmt
    {
        // http://stackoverflow.com/questions/793471/use-one-bind-param-with-variable-number-of-input-vars

        // http://stackoverflow.com/questions/2045875/pass-by-reference-problem-with-php-5-3-1

        [Script(OptimizedCode = @"
$refs = array(); 
$c = 0;
foreach($args as $key => $value) 
{
    if ($c == 0)
        $refs[$key] = $args[$key]; 
    else
        $refs[$key] = &$args[$key]; 

    $c++;
}

return call_user_func_array(array(&$stmt, 'bind_param'), $refs);")]
        static void __bind_param(object stmt, object[] args)
        {
        }

        public static void bind_param_array(this mysqli_stmt that, string types, params object[] e)
        {
            var a = new List<object>();

            a.Add(types);

            for (int i = 0; i < e.Length; i++)
            {
                var k = e[i];

                //Console.WriteLine("will call bind_param " + new { i, k });

                a.Add(k);
            }

            var aa = a.ToArray();

            //Console.WriteLine("will call bind_param " + new { aa.Length });

            __bind_param(that, aa);
        }
    }

}
