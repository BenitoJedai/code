
    // iterator states
    // -2 uninitialzied
    // -1 error
    
    // 0 first yield: init 
    // 1 first yield: move next
    // 2 first yield: return
    // 3 second yield: move next
    // 4 second yield: return
        
    /* extension method */ 
    
    var ExtensionMethod = function (targets, methods)
    {
        for (var i in targets)
        for (var j in methods)
        {
            targets[i].prototype[j] = methods[j];
        }
    }
    
    ExtensionMethod(
        [
            Array, 
            SelectIterator, 
            SelectManyIterator, 
            WhereIterator, 
            ConcatIterator, 
            FuzzyIterator,
            MixedIterator
        ],
        {

//            SequenceMixed: function (a, b)
//            {
//                var w = new MixedIterator(-2);

//                w._3_a = a;
//                w._3_b = b;    
//                
//                
//                return w;
//            },        
            SequenceSelect: function (_selector)
            {
                var w = new SelectIterator(-2);

                w._3_source = this;
                w._3_selector = _selector;    
                
                
                return w;
            },
            SequenceSelectMany: function (_selector)
            {
                var w = new SelectManyIterator(-2);

                w._3_source = this;
                w._3_selector = _selector;    
                
                
                return w;
            },            
            SequenceConcat: function (_second)
            {
                var w = new ConcatIterator(-2);

                w._3_first = this;
                w._3_second = _second;    
                
                
                return w;
            },
            SequenceWhere: function (predicate)
            {
                var w = new WhereIterator(-2);

                w._3_source = this;
                w._3_predicate = predicate;    
                
                return w;
            },
            SequenceForEach: function (_action)
            {
                var e = this.GetEnumerator();
                while (e.MoveNext())
                {
                    var v = e.get_Current();

                    _action(v);
                }    
            },
            SequenceCount: function ()
            {
                
                if (this instanceof Array)
                    return this.length;
                
                var c = -2;
                var e = this.GetEnumerator();

                while (e.MoveNext()) c++;
                
                return c;
            },
            SequenceToConsole: function ()
            {
                this.SequenceForEach(
                    function (v)
                    {
                        Console.WriteLine(v);
                    }
                );     
            }       
        }
    );
    
    Array.prototype.GetEnumerator = function ()
    {
        return new SZArrayEnumerator(this);
    }
    
    SequenceMixed = function (a, b)
    {
        var v = new MixedIterator(-2);
        
        v._3_a = a;
        v._3_b = b;
        
        return v;
    }