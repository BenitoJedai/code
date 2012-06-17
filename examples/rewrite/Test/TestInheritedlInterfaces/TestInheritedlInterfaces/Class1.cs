using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.google.appengine.repackaged.com.google.protobuf
{
    public interface Message_Builder
    {
        Message_Builder clearField();
    }

 

    public abstract class AbstractMessage_Builder : Message_Builder
    {
        public virtual Message_Builder clearField()
        {
            throw new NotImplementedException();
        }
    }

    public interface Message_Builder2
    {
        GeneratedMessage_Builder clearField();
    }

    public abstract class GeneratedMessage_Builder : AbstractMessage_Builder, Message_Builder2
    {
        public override Message_Builder clearField()
        {
            return base.clearField();
        }



        GeneratedMessage_Builder Message_Builder2.clearField()
        {
            throw new NotImplementedException();
        }
    }

    public class DescriptorProtos_UninterpretedOption_NamePart_Builder : GeneratedMessage_Builder
    {

    }
}
