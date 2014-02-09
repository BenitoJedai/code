using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.Extensions;
using System;
using System.Linq.Expressions;

namespace TestFieldExpression
{
    public sealed class ApplicationSprite : Sprite
    {
        class xRow
        {
            public long Key;
        }

        public ApplicationSprite()
        {
            Expression<Func<xRow, long>> f = e => e.Key;

            new TextField
            {
                text = new { f }.ToString(),
                autoSize = TextFieldAutoSize.LEFT
            }.AttachTo(this);

            //{ f = { Body = MemberExpression { expression = ParameterExpression { type = <type name="TestFieldExpression::ApplicationSprite_xRow" base="Class" isDynamic="true" isFinal="true" isStatic="true">
            //  <extendsClass type="Class"/>
            //  <extendsClass type="Object"/>
            //  <accessor name="prototype" access="readonly" type="*" declaredBy="Class"/>
            //  <factory type="TestFieldExpression::ApplicationSprite_xRow">
            //    <extendsClass type="Object"/>
            //    <variable name="Key" type="Number">
            //      <metadata name="__go_to_definition_help">
            //        <arg key="pos" value="357"/>
            //      </metadata>
            //    </variable>
            //    <metadata name="SHA1c3084f98f6782106fc675f9738f7049047234cb4_"/>
            //    <metadata name="__go_to_ctor_definition_help">
            //      <arg key="pos" value="396"/>
            //    </metadata>
            //    <metadata name="__go_to_definition_help">
            //      <arg key="pos" value="307"/>
            //    </metadata>
            //  </factory>
            //</type>, name = e }, field = <variable name="Key" type="Number">
            //  <metadata name="__go_to_definition_help">
            //    <arg key="pos" value="357"/>
            //  </metadata>
            //</variable> }, parameters = ParameterExpression { type = <type name="TestFieldExpression::ApplicationSprite_xRow" base="Class" isDynamic="true" isFinal="true" isStatic="true">
            //  <extendsClass type="Class"/>
            //  <extendsClass type="Object"/>
            //  <accessor name="prototype" access="readonly" type="*" declaredBy="Class"/>
            //  <factory type="TestFieldExpression::ApplicationSprite_xRow">
            //    <extendsClass type="Object"/>
            //    <variable name="Key" type="Number">
            //      <metadata name="__go_to_definition_help">
            //        <arg key="pos" value="357"/>
            //      </metadata>
            //    </variable>
            //    <metadata name="SHA1c3084f98f6782106fc675f9738f7049047234cb4_"/>
            //    <metadata name="__go_to_ctor_definition_help">
            //      <arg key="pos" value="396"/>
            //    </metadata>
            //    <metadata name="__go_to_definition_help">
            //      <arg key="pos" value="307"/>
            //    </metadata>
            //  </factory>
            //</type>, name = e } } }

        }

    }
}
