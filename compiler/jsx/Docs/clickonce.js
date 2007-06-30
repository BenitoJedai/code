 ((function (_7, _8){
            var 
                _1 = 0,
                _2='onreadystatechange',
                _3=document.getElementsByTagName('HEAD')[0],
                _4,
                _5;
    
            for (_4 in _7)
            {
                _5=document.createElement('SCRIPT');
                _5.src=_7[_4];
                _3.appendChild(_5);
                    
                _5[_2 in _5 ? _2 : 'onload'] =
                    function ()
                    {
                        var _6=_5.readyState;
                        if(_6==null||_6=='loaded'||_6=='complete')
                            if (++_1 == _7.length) _8();
                    };
            }
       }) 
            (
                [
                    'http://jsc.sourceforge.net/examples/web/SpaceInvaders/ScriptCoreLib.dll.js.packed.js',
                    'http://jsc.sourceforge.net/examples/web/SpaceInvaders/ScriptCoreLib.Query.dll.js.packed.js',
                    'http://jsc.sourceforge.net/examples/web/SpaceInvaders/SpaceInvaders.dll.js.packed.js'
                ], 
                function () { SpawnSpaceInvaders('http://jsc.sourceforge.net/examples/web/SpaceInvaders/'); }
            )
       );