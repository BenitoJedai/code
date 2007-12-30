((function(h,i){
    var a=0,
    b='onreadystatechange',
    c=document.getElementsByTagName('HEAD')[0],
    d,
    e,
    f,
    g=c.childNodes;


    next: for(d in h)
    {
        
        for (f=0;f<g.length;f++)
        {
            var v = g[f];
            var w = h[d];
            
            if (v.nodeName == 'SCRIPT')
            if (v.src == w || v.src.substr(v.src.length - w.length - 1, w.length + 1) == '/' + w)
            {
                a++;
                continue next;
            }
        }
                    
        e=document.createElement('SCRIPT');
        e.src=h[d];

        c.appendChild(e);
        e[b in e?b:'onload']=
            function()
            {
            
                var f=e.readyState;
                if(f==null||f=='loaded'||f=='complete')
                {
                    a++;
                    
                    if(a==h.length) i();
                }
            };
        
        
    }
    
    if(a==h.length) i();
    
})([$references$],function(){$done$}))