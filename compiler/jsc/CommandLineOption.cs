using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace jsc
{



    [global::System.AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public  sealed class CommandLineOptionAttribute : Attribute
    {
        // See the attribute guidelines at 
        //  http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconusingattributeclasses.asp

        public int Offset = -1;

        public string Flag;

        public string Description;

        public bool IsRequired;
    }

    public abstract class CommandLineOptionsBase<T>
        where T : CommandLineOptionsBase<T>
    {
        [CommandLineOption(
            Flag = "?", 
            Description="This help screen.")
        ]
        public bool IsHelp;

        public void ShowHelp()
        {
            foreach (CommandLineOptionsBaseOptionField v in OptionFields)
            {
                string h = "";

                if (v.Attribute.Flag == null)
                    h += ((v.Attribute.Offset + 1) + ". argument" );
                else
                    h += ("/" + v.Attribute.Flag);

                Console.Write(h.PadRight(32));

                Console.Write(" ");

                if (v.Attribute.IsRequired)
                    Console.Write("(required) ");

                if (v.Attribute.Description != null)
                {
                    
                    Console.Write(v.Attribute.Description);
                }

                Console.WriteLine();
            }
        }

        readonly CommandLineOptionsBaseOptionField[] OptionFields;

        protected CommandLineOptionsBase()
        {
        }

        protected CommandLineOptionsBase(string[] e)
        {
            OptionFields = CommandLineOptionsBaseOptionField.Of((T)this);

            if (e.Length == 0)
            {
                IsHelp = true;
                return;
            }

            foreach (CommandLineOptionsBaseOptionField v in OptionFields)
            {
                string value = v[e];

                if (value != null)
                {
                    v[typeof(FileInfo)] = delegate
                    {
                        v.Typed<FileInfo>().Value = new FileInfo(value);
                    };

                    v[typeof(bool)] = delegate
                    {
                        v.Typed<bool>().Value = true;
                    };
                }
            }
        }



 
    }

    public class CommandLineOptionsBaseOptionField
    {
        public FieldInfo Field;
        public CommandLineOptionAttribute Attribute;
        public object Target;

        public Action this[Type e]
        {
            set
            {
                if (e == Field.FieldType)
                    value();
            }
        }

        string FlagName(string e)
        {
            if (e.Length < 2)
                return null;

            if (!e.StartsWith("-") && !e.StartsWith("/"))
                return null;


            string u = e.Substring(1);

            if (u == this.Attribute.Flag)
                return u;

            return null;
        }

        public string this[string[] args]
        {
            get
            {
                if (Attribute.Flag == null)
                {
                    if (Attribute.Offset > -1 && Attribute.Offset < args.Length)
                    {
                        return args[Attribute.Offset];
                    }
                }
                else
                {
                    foreach (string v in args)
                    {
                        string flag = FlagName(v);

                        if (flag != null)
                            return flag;
                    }
                }

                return null;
            }
        }

        public class GenericFieldInfo<TF>
        {
            FieldInfo field;
            object target;

            public GenericFieldInfo(FieldInfo f, object t)
            {
                field = f;
                target = t;
            }

            public TF Value
            {
                get
                {
                    return (TF)field.GetValue(target);
                }
                set
                {
                    field.SetValue(target, value);
                }
            }
        }

        public GenericFieldInfo<TF> Typed<TF>()
        {
            return new GenericFieldInfo<TF>(Field, Target);
        }

        public static CommandLineOptionsBaseOptionField[] Of<TArg>(TArg target)
        {
            return Of(target, typeof(TArg).GetFields());
        }

        public static CommandLineOptionsBaseOptionField[] Of(object target, FieldInfo[] e)
        {
            List<CommandLineOptionsBaseOptionField> a = new List<CommandLineOptionsBaseOptionField>();

            foreach (FieldInfo v in e)
            {
                CommandLineOptionAttribute o = Helper.AttributeOf<CommandLineOptionAttribute>(v);

                if (o != null)
                {
                    CommandLineOptionsBaseOptionField f = new CommandLineOptionsBaseOptionField();

                    f.Target = target;
                    f.Attribute = o;
                    f.Field = v;

                    a.Add(f);
                }
            }

            return a.ToArray();
        }
    }
}
