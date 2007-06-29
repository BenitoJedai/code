    function $ctor$(/* base, null */ p, /* string, null*/ b, /* object */ x)
    {
        var f = null;
        var z = x[b];
        
        
        // 'this' is only valid due to 'new' keyword
        if (p) // defined ctor is wrapped, inheritance is applied
        {
            f = function ()
            {
                var a = this;
                var n = new p();

                for (var i in n) 
                     if (a[i] == void(0)) a[i] = n[i];
                    
                var i = 'toString';
                    if (a[i] == void(0)) a[i] = n[i];
                
                if (z)
                    z.apply(a, arguments);        
            };
        }
        else if (z) // defined ctor is reused
        {
            f = z;
        }
        else // a default ctor will be created
        {
            f = function () { };
        }

        f.prototype = x;

        return f;
    }