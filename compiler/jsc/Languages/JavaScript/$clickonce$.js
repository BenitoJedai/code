((function(h,i){
    var a=-1,
    b='onreadystatechange',
    c=document.getElementsByTagName('HEAD')[0],
    d,
    e,
    f,
    g=c.childNodes,
    d;


    d = function ()
    {
        next: while (1)
        {
            a++;
            
            if (a == h.length)
            {
                i();
                return;
            }
            
            for (f=0;f<g.length;f++)
            {
                var v = g[f];
                var w = h[a];
                
                if (v.nodeName == 'SCRIPT')
                if (v.src == w || v.src.substr(v.src.length - w.length - 1, w.length + 1) == '/' + w)
                    continue next;
            }
                        
            e=document.createElement('SCRIPT');
            e.src=h[a];

            
            e[b in e?b:'onload']=
                function()
                {
                
                    var f=e.readyState;
                    if(f==null||f=='loaded'||f=='complete')
                        d();
                };
            c.appendChild(e);
            
            return;
        }
    };
    
    d();
})([$references$],function(){$done$}))