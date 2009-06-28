    function $ctor$(
        p,  // base, null 
        b,  // string, null
        x   // object - new prototype
    )
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
  var W_bfCHB0Un0CgJ_aixUydYLg = {Name:{Name:"ScriptCoreLib",FullName:"ScriptCoreLib, Version\x3d3.0.3448.27305, Culture\x3dneutral, PublicKeyToken\x3dnull"}};
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Object
  function FBMUy2aCrzG0XVBXGP7j9g(){};
  FBMUy2aCrzG0XVBXGP7j9g.TypeName = "Object";
  FBMUy2aCrzG0XVBXGP7j9g.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$FBMUy2aCrzG0XVBXGP7j9g = FBMUy2aCrzG0XVBXGP7j9g.prototype;
  type$FBMUy2aCrzG0XVBXGP7j9g.constructor = FBMUy2aCrzG0XVBXGP7j9g;
  var basector$FBMUy2aCrzG0XVBXGP7j9g = $ctor$(null, null, type$FBMUy2aCrzG0XVBXGP7j9g);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Object..ctor
  type$FBMUy2aCrzG0XVBXGP7j9g._5hEABmaCrzG0XVBXGP7j9g = function ()
  {
    var a = this;

  };
  var ctor$_5hEABmaCrzG0XVBXGP7j9g = FBMUy2aCrzG0XVBXGP7j9g.ctor = $ctor$(null, '_5hEABmaCrzG0XVBXGP7j9g', type$FBMUy2aCrzG0XVBXGP7j9g);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Object.ReferenceEquals
  var _3xEABmaCrzG0XVBXGP7j9g = function () { return _2BIABr5xMzijfM5xNYhyrw.apply(null, arguments); };
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Object.GetPrototype
  function _4BEABmaCrzG0XVBXGP7j9g(i) { return i.constructor.prototype; };
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Object.GetType
  function _4REABmaCrzG0XVBXGP7j9g(a)
  {
    var b, c;

    b = new ctor$nQMABrh_afT6gMvrmjMrwWw(_4BEABmaCrzG0XVBXGP7j9g(a));
    c = jxIABvTjTTG_aL4_ahSLPtTA(oAMABrh_afT6gMvrmjMrwWw(b));
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Object.Equals
  function _4hEABmaCrzG0XVBXGP7j9g(b, c)
  {
    var d, e;

    e = !(b == c);

    if (!e)
    {
      d = 1;
      return d;
    }

    e = (!b || (c == null));

    if (!e)
    {
      d = b._4xEABmaCrzG0XVBXGP7j9g(c);
      return d;
    }

    d = 0;
    return d;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Object.Equals
  type$FBMUy2aCrzG0XVBXGP7j9g._4xEABmaCrzG0XVBXGP7j9g = function (b)
  {
    var a = this, c;

    c = (a == b);
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Object.GetHashCode
  type$FBMUy2aCrzG0XVBXGP7j9g._5BEABmaCrzG0XVBXGP7j9g = function ()
  {
    var a = this, b;

    b = 0;
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Object.ToString
  type$FBMUy2aCrzG0XVBXGP7j9g.toString /* ScriptCoreLib.JavaScript.BCLImplementation.System.__Object.ToString */ = function ()
  {
    var a = this, b;

    b = null;
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Text.__StringBuilder
  function _4_aNPjRHFijqvYJf1nSns2Q(){};
  _4_aNPjRHFijqvYJf1nSns2Q.TypeName = "StringBuilder";
  _4_aNPjRHFijqvYJf1nSns2Q.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$_4_aNPjRHFijqvYJf1nSns2Q = _4_aNPjRHFijqvYJf1nSns2Q.prototype;
  type$_4_aNPjRHFijqvYJf1nSns2Q.constructor = _4_aNPjRHFijqvYJf1nSns2Q;
  type$_4_aNPjRHFijqvYJf1nSns2Q._Value = null;
  var basector$_4_aNPjRHFijqvYJf1nSns2Q = $ctor$(null, null, type$_4_aNPjRHFijqvYJf1nSns2Q);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.Text.__StringBuilder..ctor
  type$_4_aNPjRHFijqvYJf1nSns2Q._1REABhHFijqvYJf1nSns2Q = function ()
  {
    var a = this;

    a._Value = '';
  };
  var ctor$_1REABhHFijqvYJf1nSns2Q = _4_aNPjRHFijqvYJf1nSns2Q.ctor = $ctor$(null, '_1REABhHFijqvYJf1nSns2Q', type$_4_aNPjRHFijqvYJf1nSns2Q);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Text.__StringBuilder.Append
  type$_4_aNPjRHFijqvYJf1nSns2Q._1hEABhHFijqvYJf1nSns2Q = function (b)
  {
    var a = this, c;

    a._Value = fhMABjDeCj_aRJzaBmU9SJg(a._Value, new Boolean(b));
    c = a;
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Text.__StringBuilder.Append
  type$_4_aNPjRHFijqvYJf1nSns2Q._1xEABhHFijqvYJf1nSns2Q = function (b)
  {
    var a = this, c;

    a._Value = fhMABjDeCj_aRJzaBmU9SJg(a._Value, b);
    c = a;
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Text.__StringBuilder.Append
  type$_4_aNPjRHFijqvYJf1nSns2Q._2BEABhHFijqvYJf1nSns2Q = function (b)
  {
    var a = this, c;

    a._Value = fhMABjDeCj_aRJzaBmU9SJg(a._Value, b);
    c = a;
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Text.__StringBuilder.Append
  type$_4_aNPjRHFijqvYJf1nSns2Q._2REABhHFijqvYJf1nSns2Q = function (b)
  {
    var a = this, c;

    a._Value = fhMABjDeCj_aRJzaBmU9SJg(a._Value, new Number(b));
    c = a;
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Text.__StringBuilder.Append
  type$_4_aNPjRHFijqvYJf1nSns2Q._2hEABhHFijqvYJf1nSns2Q = function (b)
  {
    var a = this, c;

    a._Value = gBMABjDeCj_aRJzaBmU9SJg(a._Value, b);
    c = a;
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Text.__StringBuilder.Append
  type$_4_aNPjRHFijqvYJf1nSns2Q._2xEABhHFijqvYJf1nSns2Q = function (b)
  {
    var a = this, c, d;

    d = (b == null);

    if (!d)
    {
      a._Value = gBMABjDeCj_aRJzaBmU9SJg(a._Value, (b+''));
    }

    c = a;
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Text.__StringBuilder.AppendLine
  type$_4_aNPjRHFijqvYJf1nSns2Q._3BEABhHFijqvYJf1nSns2Q = function ()
  {
    var a = this, b;

    b = a._2hEABhHFijqvYJf1nSns2Q(CgYABg2CEzqoZ_bNTAiczEw());
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Text.__StringBuilder.AppendLine
  type$_4_aNPjRHFijqvYJf1nSns2Q._3REABhHFijqvYJf1nSns2Q = function (b)
  {
    var a = this, c;

    c = a._2hEABhHFijqvYJf1nSns2Q(b)._3BEABhHFijqvYJf1nSns2Q();
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Text.__StringBuilder.ToString
  type$_4_aNPjRHFijqvYJf1nSns2Q.toString /* ScriptCoreLib.JavaScript.BCLImplementation.System.Text.__StringBuilder.ToString */ = function ()
  {
    var a = this, b;

    b = a._Value;
    return b;
  };
    _4_aNPjRHFijqvYJf1nSns2Q.prototype.toString /* System.Object.ToString */ = _4_aNPjRHFijqvYJf1nSns2Q.prototype.toString /* ScriptCoreLib.JavaScript.BCLImplementation.System.Text.__StringBuilder.ToString */;

  // ScriptCoreLib.JavaScript.Controls.SpawnControlBase
  function CGA9Rijy7DqW6zC1OmUAYA(){};
  CGA9Rijy7DqW6zC1OmUAYA.TypeName = "SpawnControlBase";
  CGA9Rijy7DqW6zC1OmUAYA.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$CGA9Rijy7DqW6zC1OmUAYA = CGA9Rijy7DqW6zC1OmUAYA.prototype;
  type$CGA9Rijy7DqW6zC1OmUAYA.constructor = CGA9Rijy7DqW6zC1OmUAYA;
  type$CGA9Rijy7DqW6zC1OmUAYA.SpawnControl = null;
  var basector$CGA9Rijy7DqW6zC1OmUAYA = $ctor$(null, null, type$CGA9Rijy7DqW6zC1OmUAYA);
  // ScriptCoreLib.JavaScript.Controls.SpawnControlBase..ctor
  type$CGA9Rijy7DqW6zC1OmUAYA.xxEABijy7DqW6zC1OmUAYA = function (b)
  {
    var a = this;

    a.SpawnControl = b;
  };
  var ctor$xxEABijy7DqW6zC1OmUAYA = $ctor$(null, 'xxEABijy7DqW6zC1OmUAYA', type$CGA9Rijy7DqW6zC1OmUAYA);

  // ScriptCoreLib.JavaScript.Controls.SpawnControlBase.get_SpawnString
  type$CGA9Rijy7DqW6zC1OmUAYA.xhEABijy7DqW6zC1OmUAYA = function ()
  {
    var a = this, b;

    b = YQgABl6xZjKlcaFaQuMTTA(a.SpawnControl.value);
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.IDOMImplementation.hasFeature
  // ScriptCoreLib.JavaScript.DOM.IWindow+Confirmation
  function vF5X5UKpEzGpiLUgRF75_bg(){};
  vF5X5UKpEzGpiLUgRF75_bg.TypeName = "Confirmation";
  vF5X5UKpEzGpiLUgRF75_bg.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$vF5X5UKpEzGpiLUgRF75_bg = vF5X5UKpEzGpiLUgRF75_bg.prototype;
  type$vF5X5UKpEzGpiLUgRF75_bg.constructor = vF5X5UKpEzGpiLUgRF75_bg;
  type$vF5X5UKpEzGpiLUgRF75_bg.Text = null;
  var basector$vF5X5UKpEzGpiLUgRF75_bg = $ctor$(null, null, type$vF5X5UKpEzGpiLUgRF75_bg);
  // ScriptCoreLib.JavaScript.DOM.IWindow+Confirmation..ctor
  type$vF5X5UKpEzGpiLUgRF75_bg.YhEABkKpEzGpiLUgRF75_bg = function ()
  {
    var a = this;

  };
  var ctor$YhEABkKpEzGpiLUgRF75_bg = vF5X5UKpEzGpiLUgRF75_bg.ctor = $ctor$(null, 'YhEABkKpEzGpiLUgRF75_bg', type$vF5X5UKpEzGpiLUgRF75_bg);

  // ScriptCoreLib.JavaScript.DOM.IWindow+NavigatorInfo+MimeTypeInfo
  function G2TFCdcYOj6koLXQMjq6PA(){};
  G2TFCdcYOj6koLXQMjq6PA.TypeName = "MimeTypeInfo";
  G2TFCdcYOj6koLXQMjq6PA.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$G2TFCdcYOj6koLXQMjq6PA = G2TFCdcYOj6koLXQMjq6PA.prototype;
  type$G2TFCdcYOj6koLXQMjq6PA.constructor = G2TFCdcYOj6koLXQMjq6PA;
  type$G2TFCdcYOj6koLXQMjq6PA.description = null;
  type$G2TFCdcYOj6koLXQMjq6PA.type = null;
  var basector$G2TFCdcYOj6koLXQMjq6PA = $ctor$(null, null, type$G2TFCdcYOj6koLXQMjq6PA);
  // ScriptCoreLib.JavaScript.DOM.IWindow+NavigatorInfo+MimeTypeInfo..ctor
  type$G2TFCdcYOj6koLXQMjq6PA.YREABtcYOj6koLXQMjq6PA = function ()
  {
    var a = this;

  };
  var ctor$YREABtcYOj6koLXQMjq6PA = G2TFCdcYOj6koLXQMjq6PA.ctor = $ctor$(null, 'YREABtcYOj6koLXQMjq6PA', type$G2TFCdcYOj6koLXQMjq6PA);

  // ScriptCoreLib.JavaScript.DOM.IWindow+NavigatorInfo+PluginInfo
  function Yxho2_b1_afzqTeXth6kRKyA(){};
  Yxho2_b1_afzqTeXth6kRKyA.TypeName = "PluginInfo";
  Yxho2_b1_afzqTeXth6kRKyA.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$Yxho2_b1_afzqTeXth6kRKyA = Yxho2_b1_afzqTeXth6kRKyA.prototype;
  type$Yxho2_b1_afzqTeXth6kRKyA.constructor = Yxho2_b1_afzqTeXth6kRKyA;
  type$Yxho2_b1_afzqTeXth6kRKyA.description = null;
  var basector$Yxho2_b1_afzqTeXth6kRKyA = $ctor$(null, null, type$Yxho2_b1_afzqTeXth6kRKyA);
  // ScriptCoreLib.JavaScript.DOM.IWindow+NavigatorInfo+PluginInfo..ctor
  type$Yxho2_b1_afzqTeXth6kRKyA.YBEABv1_afzqTeXth6kRKyA = function ()
  {
    var a = this;

  };
  var ctor$YBEABv1_afzqTeXth6kRKyA = Yxho2_b1_afzqTeXth6kRKyA.ctor = $ctor$(null, 'YBEABv1_afzqTeXth6kRKyA', type$Yxho2_b1_afzqTeXth6kRKyA);

  // ScriptCoreLib.JavaScript.DOM.IWindow+NavigatorInfo
  function _1n6ypcVTDjm0iPhLuf0mGg(){};
  _1n6ypcVTDjm0iPhLuf0mGg.TypeName = "NavigatorInfo";
  _1n6ypcVTDjm0iPhLuf0mGg.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$_1n6ypcVTDjm0iPhLuf0mGg = _1n6ypcVTDjm0iPhLuf0mGg.prototype;
  type$_1n6ypcVTDjm0iPhLuf0mGg.constructor = _1n6ypcVTDjm0iPhLuf0mGg;
  type$_1n6ypcVTDjm0iPhLuf0mGg.userAgent = null;
  type$_1n6ypcVTDjm0iPhLuf0mGg.appVersion = null;
  type$_1n6ypcVTDjm0iPhLuf0mGg.mimeTypes = null;
  type$_1n6ypcVTDjm0iPhLuf0mGg.plugins = null;
  var basector$_1n6ypcVTDjm0iPhLuf0mGg = $ctor$(null, null, type$_1n6ypcVTDjm0iPhLuf0mGg);
  // ScriptCoreLib.JavaScript.DOM.IWindow+NavigatorInfo..ctor
  type$_1n6ypcVTDjm0iPhLuf0mGg.XxEABsVTDjm0iPhLuf0mGg = function ()
  {
    var a = this;

  };
  var ctor$XxEABsVTDjm0iPhLuf0mGg = _1n6ypcVTDjm0iPhLuf0mGg.ctor = $ctor$(null, 'XxEABsVTDjm0iPhLuf0mGg', type$_1n6ypcVTDjm0iPhLuf0mGg);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Double
  function _9_aEMqNzT7Ti0XThSh5FnRg(){};
  _9_aEMqNzT7Ti0XThSh5FnRg.TypeName = "Double";
  _9_aEMqNzT7Ti0XThSh5FnRg.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$_9_aEMqNzT7Ti0XThSh5FnRg = _9_aEMqNzT7Ti0XThSh5FnRg.prototype;
  type$_9_aEMqNzT7Ti0XThSh5FnRg.constructor = _9_aEMqNzT7Ti0XThSh5FnRg;
  var basector$_9_aEMqNzT7Ti0XThSh5FnRg = $ctor$(null, null, type$_9_aEMqNzT7Ti0XThSh5FnRg);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Double..ctor
  type$_9_aEMqNzT7Ti0XThSh5FnRg.ixAABtzT7Ti0XThSh5FnRg = function ()
  {
    var a = this;

  };
  var ctor$ixAABtzT7Ti0XThSh5FnRg = _9_aEMqNzT7Ti0XThSh5FnRg.ctor = $ctor$(null, 'ixAABtzT7Ti0XThSh5FnRg', type$_9_aEMqNzT7Ti0XThSh5FnRg);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Double.Parse
  function iRAABtzT7Ti0XThSh5FnRg(e) { return parseFloat(e); };
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Double.CompareTo
  function ihAABtzT7Ti0XThSh5FnRg(a, b)
  {
    var c;

    c = _4RIABr5xMzijfM5xNYhyrw(a, b);
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Monitor
  function JhBtWXL_bGTm30vFuPKB5rQ(){};
  JhBtWXL_bGTm30vFuPKB5rQ.TypeName = "Monitor";
  JhBtWXL_bGTm30vFuPKB5rQ.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$JhBtWXL_bGTm30vFuPKB5rQ = JhBtWXL_bGTm30vFuPKB5rQ.prototype;
  type$JhBtWXL_bGTm30vFuPKB5rQ.constructor = JhBtWXL_bGTm30vFuPKB5rQ;
  var basector$JhBtWXL_bGTm30vFuPKB5rQ = $ctor$(null, null, type$JhBtWXL_bGTm30vFuPKB5rQ);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Monitor..ctor
  type$JhBtWXL_bGTm30vFuPKB5rQ.hBAABnL_bGTm30vFuPKB5rQ = function ()
  {
    var a = this;

  };
  var ctor$hBAABnL_bGTm30vFuPKB5rQ = JhBtWXL_bGTm30vFuPKB5rQ.ctor = $ctor$(null, 'hBAABnL_bGTm30vFuPKB5rQ', type$JhBtWXL_bGTm30vFuPKB5rQ);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Monitor.Enter
  function ghAABnL_bGTm30vFuPKB5rQ(b)
  {
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Monitor.Exit
  function gxAABnL_bGTm30vFuPKB5rQ(b)
  {
  };

  // ScriptCoreLib.JavaScript.Runtime.TweenData`1
  function EOQvqcOtIzm5NAIKAKvPQg(){};
  EOQvqcOtIzm5NAIKAKvPQg.TypeName = "TweenData_1";
  EOQvqcOtIzm5NAIKAKvPQg.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$EOQvqcOtIzm5NAIKAKvPQg = EOQvqcOtIzm5NAIKAKvPQg.prototype;
  type$EOQvqcOtIzm5NAIKAKvPQg.constructor = EOQvqcOtIzm5NAIKAKvPQg;
  type$EOQvqcOtIzm5NAIKAKvPQg.Dirty = false;
  type$EOQvqcOtIzm5NAIKAKvPQg.CurrentValue = null;
  type$EOQvqcOtIzm5NAIKAKvPQg.FutureValue = null;
  type$EOQvqcOtIzm5NAIKAKvPQg.SyncTimer = null;
  type$EOQvqcOtIzm5NAIKAKvPQg.Tick = null;
  type$EOQvqcOtIzm5NAIKAKvPQg.Done = null;
  type$EOQvqcOtIzm5NAIKAKvPQg.IsCloseEnoughHandler = null;
  type$EOQvqcOtIzm5NAIKAKvPQg.FutureValueChanged = null;
  type$EOQvqcOtIzm5NAIKAKvPQg.ValueChanged = null;
  type$EOQvqcOtIzm5NAIKAKvPQg.Speed = 0;
  var basector$EOQvqcOtIzm5NAIKAKvPQg = $ctor$(null, null, type$EOQvqcOtIzm5NAIKAKvPQg);
  // ScriptCoreLib.JavaScript.Runtime.TweenData`1..ctor
  type$EOQvqcOtIzm5NAIKAKvPQg.dBAABsOtIzm5NAIKAKvPQg = function ()
  {
    var a = this, b;

    b = null;
    a.Speed = 50;

    if (!b)
    {
      b = new ctor$KQsABoVjnjetV_aXG4GFkBQ(a, 'gRAABsOtIzm5NAIKAKvPQg');
    }

    a.SyncTimer = new ctor$_1gIABilzizCnsZekUeVmgA(b);
  };
  var ctor$dBAABsOtIzm5NAIKAKvPQg = EOQvqcOtIzm5NAIKAKvPQg.ctor = $ctor$(null, 'dBAABsOtIzm5NAIKAKvPQg', type$EOQvqcOtIzm5NAIKAKvPQg);

  // ScriptCoreLib.JavaScript.Runtime.TweenData`1.add_Tick
  type$EOQvqcOtIzm5NAIKAKvPQg.dRAABsOtIzm5NAIKAKvPQg = function (b)
  {
    var a = this;

    a.Tick = fwsABqgPxjmDnjkmC_a5nbw(a.Tick, b);
  };

  // ScriptCoreLib.JavaScript.Runtime.TweenData`1.remove_Tick
  type$EOQvqcOtIzm5NAIKAKvPQg.dhAABsOtIzm5NAIKAKvPQg = function (b)
  {
    var a = this;

    a.Tick = gQsABqgPxjmDnjkmC_a5nbw(a.Tick, b);
  };

  // ScriptCoreLib.JavaScript.Runtime.TweenData`1.add_Done
  type$EOQvqcOtIzm5NAIKAKvPQg.dxAABsOtIzm5NAIKAKvPQg = function (b)
  {
    var a = this;

    a.Done = fwsABqgPxjmDnjkmC_a5nbw(a.Done, b);
  };

  // ScriptCoreLib.JavaScript.Runtime.TweenData`1.remove_Done
  type$EOQvqcOtIzm5NAIKAKvPQg.eBAABsOtIzm5NAIKAKvPQg = function (b)
  {
    var a = this;

    a.Done = gQsABqgPxjmDnjkmC_a5nbw(a.Done, b);
  };

  // ScriptCoreLib.JavaScript.Runtime.TweenData`1.get_IsCloseEnough
  type$EOQvqcOtIzm5NAIKAKvPQg.eRAABsOtIzm5NAIKAKvPQg = function ()
  {
    var a = this, b;

    b = RwsABgF8xDasrw7uIe6gQA(a.IsCloseEnoughHandler, 0);
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.TweenData`1.add_FutureValueChanged
  type$EOQvqcOtIzm5NAIKAKvPQg.ehAABsOtIzm5NAIKAKvPQg = function (b)
  {
    var a = this;

    a.FutureValueChanged = fwsABqgPxjmDnjkmC_a5nbw(a.FutureValueChanged, b);
  };

  // ScriptCoreLib.JavaScript.Runtime.TweenData`1.remove_FutureValueChanged
  type$EOQvqcOtIzm5NAIKAKvPQg.exAABsOtIzm5NAIKAKvPQg = function (b)
  {
    var a = this;

    a.FutureValueChanged = gQsABqgPxjmDnjkmC_a5nbw(a.FutureValueChanged, b);
  };

  // ScriptCoreLib.JavaScript.Runtime.TweenData`1.add_ValueChanged
  type$EOQvqcOtIzm5NAIKAKvPQg.fBAABsOtIzm5NAIKAKvPQg = function (b)
  {
    var a = this;

    a.ValueChanged = fwsABqgPxjmDnjkmC_a5nbw(a.ValueChanged, b);
  };

  // ScriptCoreLib.JavaScript.Runtime.TweenData`1.remove_ValueChanged
  type$EOQvqcOtIzm5NAIKAKvPQg.fRAABsOtIzm5NAIKAKvPQg = function (b)
  {
    var a = this;

    a.ValueChanged = gQsABqgPxjmDnjkmC_a5nbw(a.ValueChanged, b);
  };

  // ScriptCoreLib.JavaScript.Runtime.TweenData`1.get_Value
  type$EOQvqcOtIzm5NAIKAKvPQg.fhAABsOtIzm5NAIKAKvPQg = function ()
  {
    var a = this, b;

    b = a.CurrentValue;
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.TweenData`1.set_Value
  type$EOQvqcOtIzm5NAIKAKvPQg.fxAABsOtIzm5NAIKAKvPQg = function (b)
  {
    var a = this, c;

    c = !a.Dirty;

    if (!c)
    {
      a.FutureValue = b;
      sggABrDT9zCTidQoMa3Dig(a.FutureValueChanged);
      c = a.eRAABsOtIzm5NAIKAKvPQg();

      if (!c)
      {
        a.SyncTimer._3QIABilzizCnsZekUeVmgA(a.Speed);
      }

      return;
    }

    a.FutureValue = b;
    sggABrDT9zCTidQoMa3Dig(a.FutureValueChanged);
    a.CurrentValue = a.FutureValue;
    a.Dirty = 1;
    a.gBAABsOtIzm5NAIKAKvPQg();
  };

  // ScriptCoreLib.JavaScript.Runtime.TweenData`1.RaiseValueChanged
  type$EOQvqcOtIzm5NAIKAKvPQg.gBAABsOtIzm5NAIKAKvPQg = function ()
  {
    var a = this, b;

    b = !a.eRAABsOtIzm5NAIKAKvPQg();

    if (!b)
    {
      a.CurrentValue = a.FutureValue;
    }

    sggABrDT9zCTidQoMa3Dig(a.ValueChanged);
  };

  // ScriptCoreLib.JavaScript.Runtime.TweenData`1.<.ctor>b__0
  type$EOQvqcOtIzm5NAIKAKvPQg.gRAABsOtIzm5NAIKAKvPQg = function (b)
  {
    var a = this, c;

    c = !a.eRAABsOtIzm5NAIKAKvPQg();

    if (!c)
    {
      a.SyncTimer._4QIABilzizCnsZekUeVmgA();
      sggABrDT9zCTidQoMa3Dig(a.Done);
      return;
    }

    sggABrDT9zCTidQoMa3Dig(a.Tick);
  };

  // ScriptCoreLib.JavaScript.Controls.Effects.TweenDataDouble
  function wOR0Dn55WzWtTxnfDBbnkQ(){};
  wOR0Dn55WzWtTxnfDBbnkQ.TypeName = "TweenDataDouble";
  wOR0Dn55WzWtTxnfDBbnkQ.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$wOR0Dn55WzWtTxnfDBbnkQ = wOR0Dn55WzWtTxnfDBbnkQ.prototype = new EOQvqcOtIzm5NAIKAKvPQg();
  type$wOR0Dn55WzWtTxnfDBbnkQ.constructor = wOR0Dn55WzWtTxnfDBbnkQ;
  var basector$wOR0Dn55WzWtTxnfDBbnkQ = $ctor$(basector$EOQvqcOtIzm5NAIKAKvPQg, null, type$wOR0Dn55WzWtTxnfDBbnkQ);
  // ScriptCoreLib.JavaScript.Controls.Effects.TweenDataDouble..ctor
  type$wOR0Dn55WzWtTxnfDBbnkQ.zxEABn55WzWtTxnfDBbnkQ = function (b)
  {
    var a = this;

    a._0BEABn55WzWtTxnfDBbnkQ();
    a.fBAABsOtIzm5NAIKAKvPQg(b);
  };
  var ctor$zxEABn55WzWtTxnfDBbnkQ = $ctor$(basector$EOQvqcOtIzm5NAIKAKvPQg, 'zxEABn55WzWtTxnfDBbnkQ', type$wOR0Dn55WzWtTxnfDBbnkQ);

  // ScriptCoreLib.JavaScript.Controls.Effects.TweenDataDouble..ctor
  type$wOR0Dn55WzWtTxnfDBbnkQ._0BEABn55WzWtTxnfDBbnkQ = function ()
  {
    var a = this, b, c, d;

    b = null;
    c = null;
    d = null;
    a.dBAABsOtIzm5NAIKAKvPQg();

    if (!b)
    {
      b = new ctor$LQsABharvz6dEOdzk7hI5Q(a, '_0hEABn55WzWtTxnfDBbnkQ');
    }

    a.dRAABsOtIzm5NAIKAKvPQg(b);

    if (!c)
    {
      c = new ctor$LQsABharvz6dEOdzk7hI5Q(a, '_0xEABn55WzWtTxnfDBbnkQ');
    }

    a.ehAABsOtIzm5NAIKAKvPQg(c);

    if (!d)
    {
      d = new ctor$KQsABoVjnjetV_aXG4GFkBQ(a, '_1BEABn55WzWtTxnfDBbnkQ');
    }

    a.IsCloseEnoughHandler = fwsABqgPxjmDnjkmC_a5nbw(a.IsCloseEnoughHandler, d);
  };
  var ctor$_0BEABn55WzWtTxnfDBbnkQ = wOR0Dn55WzWtTxnfDBbnkQ.ctor = $ctor$(basector$EOQvqcOtIzm5NAIKAKvPQg, '_0BEABn55WzWtTxnfDBbnkQ', type$wOR0Dn55WzWtTxnfDBbnkQ);

  // ScriptCoreLib.JavaScript.Controls.Effects.TweenDataDouble.round
  type$wOR0Dn55WzWtTxnfDBbnkQ._0REABn55WzWtTxnfDBbnkQ = function (b)
  {
    var a = this, c;

    b = (b * 100);
    b = Math.round(b);
    b = (b / 100);
    c = b;
    return c;
  };

  // ScriptCoreLib.JavaScript.Controls.Effects.TweenDataDouble.<.ctor>b__0
  type$wOR0Dn55WzWtTxnfDBbnkQ._0hEABn55WzWtTxnfDBbnkQ = function ()
  {
    var a = this, b;

    b = ((a.CurrentValue + a.FutureValue) / 2);
    a.CurrentValue = a._0REABn55WzWtTxnfDBbnkQ(b);
    a.gBAABsOtIzm5NAIKAKvPQg();
  };

  // ScriptCoreLib.JavaScript.Controls.Effects.TweenDataDouble.<.ctor>b__1
  type$wOR0Dn55WzWtTxnfDBbnkQ._0xEABn55WzWtTxnfDBbnkQ = function ()
  {
    var a = this;

    a.FutureValue = a._0REABn55WzWtTxnfDBbnkQ(a.FutureValue);
  };

  // ScriptCoreLib.JavaScript.Controls.Effects.TweenDataDouble.<.ctor>b__2
  type$wOR0Dn55WzWtTxnfDBbnkQ._1BEABn55WzWtTxnfDBbnkQ = function (b)
  {
    var a = this;

    b.Value = (Math.abs((a.CurrentValue - a.FutureValue)) < 0.05);
  };

  // ScriptCoreLib.JavaScript.Controls.Effects.TweenDataPoint
  function zf0v_aGuINTivHicv_aPWjJw(){};
  zf0v_aGuINTivHicv_aPWjJw.TypeName = "TweenDataPoint";
  zf0v_aGuINTivHicv_aPWjJw.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$zf0v_aGuINTivHicv_aPWjJw = zf0v_aGuINTivHicv_aPWjJw.prototype = new EOQvqcOtIzm5NAIKAKvPQg();
  type$zf0v_aGuINTivHicv_aPWjJw.constructor = zf0v_aGuINTivHicv_aPWjJw;
  var basector$zf0v_aGuINTivHicv_aPWjJw = $ctor$(basector$EOQvqcOtIzm5NAIKAKvPQg, null, type$zf0v_aGuINTivHicv_aPWjJw);
  // ScriptCoreLib.JavaScript.Controls.Effects.TweenDataPoint..ctor
  type$zf0v_aGuINTivHicv_aPWjJw.yBEABmuINTivHicv_aPWjJw = function (b)
  {
    var a = this;

    a.yREABmuINTivHicv_aPWjJw();
    a.fBAABsOtIzm5NAIKAKvPQg(b);
  };
  var ctor$yBEABmuINTivHicv_aPWjJw = $ctor$(basector$EOQvqcOtIzm5NAIKAKvPQg, 'yBEABmuINTivHicv_aPWjJw', type$zf0v_aGuINTivHicv_aPWjJw);

  // ScriptCoreLib.JavaScript.Controls.Effects.TweenDataPoint..ctor
  type$zf0v_aGuINTivHicv_aPWjJw.yREABmuINTivHicv_aPWjJw = function ()
  {
    var a = this, b, c, d;

    b = null;
    c = null;
    d = null;
    a.dBAABsOtIzm5NAIKAKvPQg();

    if (!b)
    {
      b = new ctor$LQsABharvz6dEOdzk7hI5Q(a, 'zBEABmuINTivHicv_aPWjJw');
    }

    a.dRAABsOtIzm5NAIKAKvPQg(b);

    if (!c)
    {
      c = new ctor$LQsABharvz6dEOdzk7hI5Q(a, 'zREABmuINTivHicv_aPWjJw');
    }

    a.ehAABsOtIzm5NAIKAKvPQg(c);

    if (!d)
    {
      d = new ctor$KQsABoVjnjetV_aXG4GFkBQ(a, 'zhEABmuINTivHicv_aPWjJw');
    }

    a.IsCloseEnoughHandler = fwsABqgPxjmDnjkmC_a5nbw(a.IsCloseEnoughHandler, d);
  };
  var ctor$yREABmuINTivHicv_aPWjJw = zf0v_aGuINTivHicv_aPWjJw.ctor = $ctor$(basector$EOQvqcOtIzm5NAIKAKvPQg, 'yREABmuINTivHicv_aPWjJw', type$zf0v_aGuINTivHicv_aPWjJw);

  // ScriptCoreLib.JavaScript.Controls.Effects.TweenDataPoint.round
  type$zf0v_aGuINTivHicv_aPWjJw.yhEABmuINTivHicv_aPWjJw = function (b)
  {
    var a = this, c;

    c = new ctor$xhQABoqVzDa8F5yDSGjsZA(a.yxEABmuINTivHicv_aPWjJw(b.X), a.yxEABmuINTivHicv_aPWjJw(b.Y));
    return c;
  };

  // ScriptCoreLib.JavaScript.Controls.Effects.TweenDataPoint.round
  type$zf0v_aGuINTivHicv_aPWjJw.yxEABmuINTivHicv_aPWjJw = function (b)
  {
    var a = this, c;

    c = Math.round(b);
    return c;
  };

  // ScriptCoreLib.JavaScript.Controls.Effects.TweenDataPoint.<.ctor>b__0
  type$zf0v_aGuINTivHicv_aPWjJw.zBEABmuINTivHicv_aPWjJw = function ()
  {
    var a = this, b;

    b = zRQABoqVzDa8F5yDSGjsZA(zBQABoqVzDa8F5yDSGjsZA(a.CurrentValue, a.FutureValue), 2);
    a.CurrentValue = a.yhEABmuINTivHicv_aPWjJw(b);
    a.gBAABsOtIzm5NAIKAKvPQg();
  };

  // ScriptCoreLib.JavaScript.Controls.Effects.TweenDataPoint.<.ctor>b__1
  type$zf0v_aGuINTivHicv_aPWjJw.zREABmuINTivHicv_aPWjJw = function ()
  {
    var a = this;

    a.FutureValue = a.yhEABmuINTivHicv_aPWjJw(a.FutureValue);
  };

  // ScriptCoreLib.JavaScript.Controls.Effects.TweenDataPoint.<.ctor>b__2
  type$zf0v_aGuINTivHicv_aPWjJw.zhEABmuINTivHicv_aPWjJw = function (b)
  {
    var a = this, c, d;

    c = yxQABoqVzDa8F5yDSGjsZA(a.CurrentValue, a.FutureValue);
    d = !(Math.abs(c.X) > 1);

    if (!d)
    {
      return;
    }

    d = !(Math.abs(c.Y) > 1);

    if (!d)
    {
      return;
    }

    b.Value = 1;
  };

  // ScriptCoreLib.Shared.Serialized.SimpleEmailTag
  function vp1c5CVSVjSzSGzRnERclQ(){};
  vp1c5CVSVjSzSGzRnERclQ.TypeName = "SimpleEmailTag";
  vp1c5CVSVjSzSGzRnERclQ.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$vp1c5CVSVjSzSGzRnERclQ = vp1c5CVSVjSzSGzRnERclQ.prototype;
  type$vp1c5CVSVjSzSGzRnERclQ.constructor = vp1c5CVSVjSzSGzRnERclQ;
  type$vp1c5CVSVjSzSGzRnERclQ.from = null;
  type$vp1c5CVSVjSzSGzRnERclQ.to = null;
  type$vp1c5CVSVjSzSGzRnERclQ.subject = null;
  type$vp1c5CVSVjSzSGzRnERclQ.body = null;
  var basector$vp1c5CVSVjSzSGzRnERclQ = $ctor$(null, null, type$vp1c5CVSVjSzSGzRnERclQ);
  // ScriptCoreLib.Shared.Serialized.SimpleEmailTag..ctor
  type$vp1c5CVSVjSzSGzRnERclQ.JBAABiVSVjSzSGzRnERclQ = function ()
  {
    var a = this;

  };
  var ctor$JBAABiVSVjSzSGzRnERclQ = vp1c5CVSVjSzSGzRnERclQ.ctor = $ctor$(null, 'JBAABiVSVjSzSGzRnERclQ', type$vp1c5CVSVjSzSGzRnERclQ);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Console+__BrowserConsole
  function hj_aCuhbA4D2OPLnwMhJAvw(){};
  hj_aCuhbA4D2OPLnwMhJAvw.TypeName = "__BrowserConsole";
  hj_aCuhbA4D2OPLnwMhJAvw.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$hj_aCuhbA4D2OPLnwMhJAvw = hj_aCuhbA4D2OPLnwMhJAvw.prototype;
  type$hj_aCuhbA4D2OPLnwMhJAvw.constructor = hj_aCuhbA4D2OPLnwMhJAvw;
  var rgUABBbA4D2OPLnwMhJAvw = 0;
  var rwUABBbA4D2OPLnwMhJAvw = null;
  var sQUABBbA4D2OPLnwMhJAvw = false;
  type$hj_aCuhbA4D2OPLnwMhJAvw._task = null;
  type$hj_aCuhbA4D2OPLnwMhJAvw.StartTime = null;
  var basector$hj_aCuhbA4D2OPLnwMhJAvw = $ctor$(null, null, type$hj_aCuhbA4D2OPLnwMhJAvw);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Console+__BrowserConsole..ctor
  type$hj_aCuhbA4D2OPLnwMhJAvw.KA8ABhbA4D2OPLnwMhJAvw = function (b)
  {
    var a = this;

    a._task = b;
    a.StartTime = Zw0ABijAJDClltROMHFY_bw(Zg0ABijAJDClltROMHFY_bw());
    a.KQ8ABhbA4D2OPLnwMhJAvw();
    LQ8ABhbA4D2OPLnwMhJAvw(gRMABjDeCj_aRJzaBmU9SJg('<', a._task, '>'));
    rgUABBbA4D2OPLnwMhJAvw = (rgUABBbA4D2OPLnwMhJAvw + 1);
  };
  var ctor$KA8ABhbA4D2OPLnwMhJAvw = $ctor$(null, 'KA8ABhbA4D2OPLnwMhJAvw', type$hj_aCuhbA4D2OPLnwMhJAvw);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Console+__BrowserConsole.EnableActiveXConsole
  function Jw8ABhbA4D2OPLnwMhJAvw()
  {
    var b, c;

    b = !(rwUABBbA4D2OPLnwMhJAvw == null);

    if (!b)
    {
      c = [
        'ActiveXConsole.Console'
      ];
      rwUABBbA4D2OPLnwMhJAvw = Ng8ABmnHcTKxHThhFfFi6A(c);
      b = (rwUABBbA4D2OPLnwMhJAvw == null);

      if (!b)
      {
        rwUABBbA4D2OPLnwMhJAvw.OpenConsole();
      }

    }

  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Console+__BrowserConsole.WriteIdent
  type$hj_aCuhbA4D2OPLnwMhJAvw.KQ8ABhbA4D2OPLnwMhJAvw = function ()
  {
    var a = this, b, c;

    b = rgUABBbA4D2OPLnwMhJAvw;
    while ((b-- > 0))
    {
      LA8ABhbA4D2OPLnwMhJAvw(' ');
    }
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Console+__BrowserConsole.InternalDump
  function Kg8ABhbA4D2OPLnwMhJAvw(w0, e0) { 
            if (w0['dump'] != void(0))
                w0.dump(e0);
             };
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Console+__BrowserConsole.Dump
  function Kw8ABhbA4D2OPLnwMhJAvw(b)
  {
    Kg8ABhbA4D2OPLnwMhJAvw(window, b);
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Console+__BrowserConsole.Write
  function LA8ABhbA4D2OPLnwMhJAvw(b)
  {
    var c;

    c = !(rwUABBbA4D2OPLnwMhJAvw == null);

    if (!c)
    {
      Kw8ABhbA4D2OPLnwMhJAvw(b);
      return;
    }

    rwUABBbA4D2OPLnwMhJAvw.WriteString(fRMABjDeCj_aRJzaBmU9SJg(b));
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Console+__BrowserConsole.WriteLine
  function LQ8ABhbA4D2OPLnwMhJAvw(b)
  {
    LA8ABhbA4D2OPLnwMhJAvw(b);
    LA8ABhbA4D2OPLnwMhJAvw('\u000a');
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Console+__BrowserConsole.Dispose
  type$hj_aCuhbA4D2OPLnwMhJAvw.Lg8ABhbA4D2OPLnwMhJAvw = function ()
  {
    var a = this, b, c;

    rgUABBbA4D2OPLnwMhJAvw = (rgUABBbA4D2OPLnwMhJAvw - 1);
    b = (Zw0ABijAJDClltROMHFY_bw(Zg0ABijAJDClltROMHFY_bw()) - a.StartTime);
    a.KQ8ABhbA4D2OPLnwMhJAvw();
    c = [
      '<\u002f',
      a._task,
      ' - ',
      b,
      'ms >'
    ];
    LQ8ABhbA4D2OPLnwMhJAvw(fBMABjDeCj_aRJzaBmU9SJg(c));
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Console+__BrowserConsole.Log
  function Lw8ABhbA4D2OPLnwMhJAvw(b)
  {
    var c;

    c = !(document == null);

    if (!c)
    {
      return;
    }

    c = !sQUABBbA4D2OPLnwMhJAvw;

    if (!c)
    {
      window.status = b;
    }

    LQ8ABhbA4D2OPLnwMhJAvw(gRMABjDeCj_aRJzaBmU9SJg(Zg0ABijAJDClltROMHFY_bw().toLocaleString(), ' ', b));
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Console+__BrowserConsole.LogError
  function MA8ABhbA4D2OPLnwMhJAvw(b)
  {
    Lw8ABhbA4D2OPLnwMhJAvw(gBMABjDeCj_aRJzaBmU9SJg('\u002a\u002a\u002a ', b));
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Console+__BrowserConsole.LogError
  function MQ8ABhbA4D2OPLnwMhJAvw(b)
  {
    Lw8ABhbA4D2OPLnwMhJAvw(gBMABjDeCj_aRJzaBmU9SJg('\u002a\u002a\u002a ', (b+'')));
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Console+__BrowserConsole.WriteLine
  function Mg8ABhbA4D2OPLnwMhJAvw()
  {
    LQ8ABhbA4D2OPLnwMhJAvw('');
  };

  // System.IDisposable
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Console+__BrowserConsole
  (function (i)  {
    i.xAAABq_bUDz_aWf_aXPRTEtLA = i.Lg8ABhbA4D2OPLnwMhJAvw;
  }
  )(type$hj_aCuhbA4D2OPLnwMhJAvw);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Console
  function T4NSm6iGvTSwHMGw_bObBeQ(){};
  T4NSm6iGvTSwHMGw_bObBeQ.TypeName = "Console";
  T4NSm6iGvTSwHMGw_bObBeQ.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$T4NSm6iGvTSwHMGw_bObBeQ = T4NSm6iGvTSwHMGw_bObBeQ.prototype;
  type$T4NSm6iGvTSwHMGw_bObBeQ.constructor = T4NSm6iGvTSwHMGw_bObBeQ;
  var basector$T4NSm6iGvTSwHMGw_bObBeQ = $ctor$(null, null, type$T4NSm6iGvTSwHMGw_bObBeQ);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Console..ctor
  type$T4NSm6iGvTSwHMGw_bObBeQ.JQ8ABqiGvTSwHMGw_bObBeQ = function ()
  {
    var a = this;

  };
  var ctor$JQ8ABqiGvTSwHMGw_bObBeQ = T4NSm6iGvTSwHMGw_bObBeQ.ctor = $ctor$(null, 'JQ8ABqiGvTSwHMGw_bObBeQ', type$T4NSm6iGvTSwHMGw_bObBeQ);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Console.WriteLine
  function Hw8ABqiGvTSwHMGw_bObBeQ(b)
  {
    LQ8ABhbA4D2OPLnwMhJAvw((b+''));
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Console.WriteLine
  function IA8ABqiGvTSwHMGw_bObBeQ(b)
  {
    LQ8ABhbA4D2OPLnwMhJAvw(b);
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Console.WriteLine
  function IQ8ABqiGvTSwHMGw_bObBeQ()
  {
    Mg8ABhbA4D2OPLnwMhJAvw();
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Console.WriteLine
  function Ig8ABqiGvTSwHMGw_bObBeQ(b, c)
  {
    LQ8ABhbA4D2OPLnwMhJAvw(aBMABjDeCj_aRJzaBmU9SJg(b, c));
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Console.Write
  function Iw8ABqiGvTSwHMGw_bObBeQ(b)
  {
    LA8ABhbA4D2OPLnwMhJAvw(b);
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Console.Write
  function JA8ABqiGvTSwHMGw_bObBeQ(b)
  {
    LA8ABhbA4D2OPLnwMhJAvw((b+''));
  };

  // ScriptCoreLib.JavaScript.DOM.IMath.minmax
  function Rw4ABi5cVD2wLrHYrXm6uQ(a, b, c, d)
  {
    var e;

    e = a.max(a.min(b, c), d);
    return e;
  };

  // ScriptCoreLib.JavaScript.DOM.IMath.abs
  // ScriptCoreLib.JavaScript.DOM.IMath.acos
  // ScriptCoreLib.JavaScript.DOM.IMath.asin
  // ScriptCoreLib.JavaScript.DOM.IMath.atan
  // ScriptCoreLib.JavaScript.DOM.IMath.atan2
  // ScriptCoreLib.JavaScript.DOM.IMath.ceil
  // ScriptCoreLib.JavaScript.DOM.IMath.floor
  // ScriptCoreLib.JavaScript.DOM.IMath.cos
  // ScriptCoreLib.JavaScript.DOM.IMath.exp
  // ScriptCoreLib.JavaScript.DOM.IMath.log
  // ScriptCoreLib.JavaScript.DOM.IMath.sin
  // ScriptCoreLib.JavaScript.DOM.IMath.sqrt
  // ScriptCoreLib.JavaScript.DOM.IMath.round
  // ScriptCoreLib.JavaScript.DOM.IMath.tan
  // ScriptCoreLib.JavaScript.DOM.IMath.random
  // ScriptCoreLib.JavaScript.DOM.IMath.max
  // ScriptCoreLib.JavaScript.DOM.IMath.max
  // ScriptCoreLib.JavaScript.DOM.IMath.min
  // ScriptCoreLib.JavaScript.DOM.IMath.min
  // ScriptCoreLib.JavaScript.DOM.IMath.pow
  // ScriptCoreLib.JavaScript.DOM.IFunction.CreateType
  function Og4ABlsV3T_aPxZ1ZRmsarQ(f) { return new f(); };
  // ScriptCoreLib.JavaScript.DOM.IFunction.CreateType
  function Ow4ABlsV3T_aPxZ1ZRmsarQ(a)
  {
    var b;

    b = Og4ABlsV3T_aPxZ1ZRmsarQ(a);
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.IFunction.Invoke
  function PA4ABlsV3T_aPxZ1ZRmsarQ(f, a0) { return f(a0) };
  // ScriptCoreLib.JavaScript.DOM.IFunction.Invoke
  function PQ4ABlsV3T_aPxZ1ZRmsarQ(a, b)
  {
    var c;

    c = PA4ABlsV3T_aPxZ1ZRmsarQ(a, b);
    return c;
  };

  // ScriptCoreLib.JavaScript.DOM.IFunction.Invoke
  function Pg4ABlsV3T_aPxZ1ZRmsarQ(f, a0, a1, a2) { return f(a0, a1, a2); };
  // ScriptCoreLib.JavaScript.DOM.IFunction.apply
  // ScriptCoreLib.JavaScript.DOM.IFunction.Invoke
  function QA4ABlsV3T_aPxZ1ZRmsarQ(a, b, c, d)
  {
    var e;

    e = Pg4ABlsV3T_aPxZ1ZRmsarQ(a, b, c, d);
    return e;
  };

  // ScriptCoreLib.JavaScript.DOM.IFunction.Of
  function QQ4ABlsV3T_aPxZ1ZRmsarQ(b)
  {
    var c;

    c = CBMABr5xMzijfM5xNYhyrw(BBMABr5xMzijfM5xNYhyrw(window), b);
    return c;
  };

  // ScriptCoreLib.JavaScript.DOM.IFunction.Of
  function Qg4ABlsV3T_aPxZ1ZRmsarQ(b)
  {
    var c;

    c = b.fAsABqgPxjmDnjkmC_a5nbw();
    return c;
  };

  // ScriptCoreLib.JavaScript.DOM.IFunction.Of
  function Qw4ABlsV3T_aPxZ1ZRmsarQ(b)
  {
    var c;

    c = b.fAsABqgPxjmDnjkmC_a5nbw();
    return c;
  };

  // ScriptCoreLib.JavaScript.DOM.IFunction.OfDelegate
  function RA4ABlsV3T_aPxZ1ZRmsarQ(b)
  {
    var c;

    c = b.fAsABqgPxjmDnjkmC_a5nbw();
    return c;
  };

  // ScriptCoreLib.JavaScript.DOM.IFunction.Export
  function RQ4ABlsV3T_aPxZ1ZRmsarQ(a, b)
  {
    EhMABr5xMzijfM5xNYhyrw(b, a);
  };

  // ScriptCoreLib.JavaScript.DOM.IFunction.Export
  function Rg4ABlsV3T_aPxZ1ZRmsarQ(b, c)
  {
    RQ4ABlsV3T_aPxZ1ZRmsarQ(Qg4ABlsV3T_aPxZ1ZRmsarQ(c), b);
  };

  // ScriptCoreLib.JavaScript.DOM.IArray`1+IncludeArgs
  function LgFommn_bwTyJHmssIjkNiQ(){};
  LgFommn_bwTyJHmssIjkNiQ.TypeName = "IncludeArgs";
  LgFommn_bwTyJHmssIjkNiQ.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$LgFommn_bwTyJHmssIjkNiQ = LgFommn_bwTyJHmssIjkNiQ.prototype;
  type$LgFommn_bwTyJHmssIjkNiQ.constructor = LgFommn_bwTyJHmssIjkNiQ;
  type$LgFommn_bwTyJHmssIjkNiQ.Include = false;
  type$LgFommn_bwTyJHmssIjkNiQ.Item = null;
  var basector$LgFommn_bwTyJHmssIjkNiQ = $ctor$(null, null, type$LgFommn_bwTyJHmssIjkNiQ);
  // ScriptCoreLib.JavaScript.DOM.IArray`1+IncludeArgs..ctor
  type$LgFommn_bwTyJHmssIjkNiQ.Ng4ABmn_bwTyJHmssIjkNiQ = function ()
  {
    var a = this;

    a.Include = 0;
  };
  var ctor$Ng4ABmn_bwTyJHmssIjkNiQ = LgFommn_bwTyJHmssIjkNiQ.ctor = $ctor$(null, 'Ng4ABmn_bwTyJHmssIjkNiQ', type$LgFommn_bwTyJHmssIjkNiQ);

  // ScriptCoreLib.JavaScript.DOM.IArray`1.Find
  function Gw4ABqPHMD_aEoYb_aoNHL9w(a, b)
  {
    var c, d, e, f;

    c = HA4ABqPHMD_aEoYb_aoNHL9w(a, b);
    e = !(c == null);

    if (!e)
    {
      f = void(0);
      d = f;
      return d;
    }

    d = c.Item;
    return d;
  };

  // ScriptCoreLib.JavaScript.DOM.IArray`1.FindMember
  function HA4ABqPHMD_aEoYb_aoNHL9w(a, b)
  {
    var c;

    c = _6xIABr5xMzijfM5xNYhyrw(BBMABr5xMzijfM5xNYhyrw(a), b);
    return c;
  };

  // ScriptCoreLib.JavaScript.DOM.IArray`1.InternalConstructor
  function Hw4ABqPHMD_aEoYb_aoNHL9w() { return []; };
  // ScriptCoreLib.JavaScript.DOM.IArray`1.InternalConstructor
  function IA4ABqPHMD_aEoYb_aoNHL9w(b, c)
  {
    var d, e, f, g, h, i, j;

    d = Hw4ABqPHMD_aEoYb_aoNHL9w();
    h = b;

    for (i = 0; (i < h.length); i++)
    {
      e = h[i];
      f = new ctor$Ng4ABmn_bwTyJHmssIjkNiQ();
      f.Item = e;
      c.Invoke(f);
      j = !f.Include;

      if (!j)
      {
        d = IQ4ABqPHMD_aEoYb_aoNHL9w(d, e);
      }

    }

    g = d;
    return g;
  };

  // ScriptCoreLib.JavaScript.DOM.IArray`1.op_Addition
  function IQ4ABqPHMD_aEoYb_aoNHL9w(b, c)
  {
    var d;

    b.push(c);
    d = b;
    return d;
  };

  // ScriptCoreLib.JavaScript.DOM.IArray`1.ForEach
  function Ig4ABqPHMD_aEoYb_aoNHL9w(a, b)
  {
    var c, d, e, f;

    d = Lw4ABqPHMD_aEoYb_aoNHL9w(a);

    for (e = 0; (e < d.length); e++)
    {
      c = d[e];
      b.Invoke(c);
    }

  };

  // ScriptCoreLib.JavaScript.DOM.IArray`1.push
  // ScriptCoreLib.JavaScript.DOM.IArray`1.shift
  // ScriptCoreLib.JavaScript.DOM.IArray`1.unshift
  // ScriptCoreLib.JavaScript.DOM.IArray`1.pop
  // ScriptCoreLib.JavaScript.DOM.IArray`1.splice
  // ScriptCoreLib.JavaScript.DOM.IArray`1.splice
  // ScriptCoreLib.JavaScript.DOM.IArray`1.slice
  // ScriptCoreLib.JavaScript.DOM.IArray`1.join
  // ScriptCoreLib.JavaScript.DOM.IArray`1.join
  // ScriptCoreLib.JavaScript.DOM.IArray`1.get_Item
  function LA4ABqPHMD_aEoYb_aoNHL9w(a, b)
  {
    var c;

    c = BhMABr5xMzijfM5xNYhyrw(a, new Number(b));
    return c;
  };

  // ScriptCoreLib.JavaScript.DOM.IArray`1.set_Item
  function LQ4ABqPHMD_aEoYb_aoNHL9w(a, b, c)
  {
    BxMABr5xMzijfM5xNYhyrw(a, new Number(b), c);
  };

  // ScriptCoreLib.JavaScript.DOM.IArray`1.indexOf
  function Lg4ABqPHMD_aEoYb_aoNHL9w(a, b)
  {
    var c, d, e, f;

    c = -1;

    for (d = 0; (d < a.length); d++)
    {
      f = !_2BIABr5xMzijfM5xNYhyrw(LA4ABqPHMD_aEoYb_aoNHL9w(a, d), b);

      if (!f)
      {
        c = d;
        break;
      }

    }

    e = c;
    return e;
  };

  // ScriptCoreLib.JavaScript.DOM.IArray`1.ToArray
  function Lw4ABqPHMD_aEoYb_aoNHL9w(a)
  {
    var b;

    b = a;
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.IArray`1.op_Implicit
  function MA4ABqPHMD_aEoYb_aoNHL9w(b)
  {
    var c;

    c = Lw4ABqPHMD_aEoYb_aoNHL9w(b);
    return c;
  };

  // ScriptCoreLib.JavaScript.DOM.IArray`1.Split
  function MQ4ABqPHMD_aEoYb_aoNHL9w(e, d) { return e.split(d); };
  // ScriptCoreLib.JavaScript.DOM.IArray`1.sort
  // ScriptCoreLib.JavaScript.DOM.IArray`1.sort
  function Mw4ABqPHMD_aEoYb_aoNHL9w(a, b)
  {
    a.sort(b.fAsABqgPxjmDnjkmC_a5nbw());
  };

  // ScriptCoreLib.JavaScript.DOM.IArray`1.get_IsArray
  function NA4ABqPHMD_aEoYb_aoNHL9w(a)
  {
    var b;

    b = __aBIABr5xMzijfM5xNYhyrw(BBMABr5xMzijfM5xNYhyrw(a));
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.IArray`1.SplitLines
  function NQ4ABqPHMD_aEoYb_aoNHL9w(b)
  {
    var c, d, e;

    c = MQ4ABqPHMD_aEoYb_aoNHL9w(b, '\u000d\u000a');
    d = MQ4ABqPHMD_aEoYb_aoNHL9w(b, '\u000a');
    e = ((c.length >= d.length) ? c : d);
    return e;
  };

  // ScriptCoreLib.JavaScript.Runtime.StringWriter
  function wffJRvXKHjSzDZNs7b2S4g(){};
  wffJRvXKHjSzDZNs7b2S4g.TypeName = "StringWriter";
  wffJRvXKHjSzDZNs7b2S4g.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$wffJRvXKHjSzDZNs7b2S4g = wffJRvXKHjSzDZNs7b2S4g.prototype;
  type$wffJRvXKHjSzDZNs7b2S4g.constructor = wffJRvXKHjSzDZNs7b2S4g;
  type$wffJRvXKHjSzDZNs7b2S4g.Buffer = null;
  type$wffJRvXKHjSzDZNs7b2S4g.NewLineString = null;
  var basector$wffJRvXKHjSzDZNs7b2S4g = $ctor$(null, null, type$wffJRvXKHjSzDZNs7b2S4g);
  // ScriptCoreLib.JavaScript.Runtime.StringWriter..ctor
  type$wffJRvXKHjSzDZNs7b2S4g.Gg4ABvXKHjSzDZNs7b2S4g = function ()
  {
    var a = this;

    a.Buffer = Hw4ABqPHMD_aEoYb_aoNHL9w();
    a.NewLineString = '\u000d\u000a';
  };
  var ctor$Gg4ABvXKHjSzDZNs7b2S4g = wffJRvXKHjSzDZNs7b2S4g.ctor = $ctor$(null, 'Gg4ABvXKHjSzDZNs7b2S4g', type$wffJRvXKHjSzDZNs7b2S4g);

  // ScriptCoreLib.JavaScript.Runtime.StringWriter.Write
  type$wffJRvXKHjSzDZNs7b2S4g.EA4ABvXKHjSzDZNs7b2S4g = function (b)
  {
    var a = this;

    a.Buffer.push(b.Fw4ABvXKHjSzDZNs7b2S4g());
  };

  // ScriptCoreLib.JavaScript.Runtime.StringWriter.Write
  type$wffJRvXKHjSzDZNs7b2S4g.EQ4ABvXKHjSzDZNs7b2S4g = function ()
  {
    var a = this;

    a.Eg4ABvXKHjSzDZNs7b2S4g('');
  };

  // ScriptCoreLib.JavaScript.Runtime.StringWriter.Write
  type$wffJRvXKHjSzDZNs7b2S4g.Eg4ABvXKHjSzDZNs7b2S4g = function (b)
  {
    var a = this, c, d, e, f, g;

    c = a.Buffer.length;
    g = !(c > 0);

    if (!g)
    {
      d = (c - 1);
      e = a.Buffer;
      f = LA4ABqPHMD_aEoYb_aoNHL9w(e, d);
      LQ4ABqPHMD_aEoYb_aoNHL9w(e, d, fhMABjDeCj_aRJzaBmU9SJg(f, b));
      return;
    }

    a.Buffer.push(fRMABjDeCj_aRJzaBmU9SJg(b));
  };

  // ScriptCoreLib.JavaScript.Runtime.StringWriter.WriteLine
  type$wffJRvXKHjSzDZNs7b2S4g.Ew4ABvXKHjSzDZNs7b2S4g = function ()
  {
    var a = this;

    a.Buffer.push(a.NewLineString);
  };

  // ScriptCoreLib.JavaScript.Runtime.StringWriter.WriteLine
  type$wffJRvXKHjSzDZNs7b2S4g.FA4ABvXKHjSzDZNs7b2S4g = function (b)
  {
    var a = this;

    a.Eg4ABvXKHjSzDZNs7b2S4g(b);
    a.Ew4ABvXKHjSzDZNs7b2S4g();
  };

  // ScriptCoreLib.JavaScript.Runtime.StringWriter.Prefix
  type$wffJRvXKHjSzDZNs7b2S4g.FQ4ABvXKHjSzDZNs7b2S4g = function (b, c)
  {
    var a = this;

    a.Fg4ABvXKHjSzDZNs7b2S4g(b, c, (a.Buffer.length - 1));
  };

  // ScriptCoreLib.JavaScript.Runtime.StringWriter.Prefix
  type$wffJRvXKHjSzDZNs7b2S4g.Fg4ABvXKHjSzDZNs7b2S4g = function (b, c, d)
  {
    var a = this, e, f;


    for (e = c; !(e > d); e++)
    {
      f = !mRMABjDeCj_aRJzaBmU9SJg(LA4ABqPHMD_aEoYb_aoNHL9w(a.Buffer, e), a.NewLineString);

      if (!f)
      {
        LQ4ABqPHMD_aEoYb_aoNHL9w(a.Buffer, e, gBMABjDeCj_aRJzaBmU9SJg(b, LA4ABqPHMD_aEoYb_aoNHL9w(a.Buffer, e)));
      }

    }

  };

  // ScriptCoreLib.JavaScript.Runtime.StringWriter.GetString
  type$wffJRvXKHjSzDZNs7b2S4g.Fw4ABvXKHjSzDZNs7b2S4g = function ()
  {
    var a = this, b;

    b = a.Buffer.join('');
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.StringWriter.GetString
  type$wffJRvXKHjSzDZNs7b2S4g.GA4ABvXKHjSzDZNs7b2S4g = function (b)
  {
    var a = this, c;

    c = a.Buffer.join(b);
    return c;
  };

  // ScriptCoreLib.JavaScript.Runtime.StringWriter.Clear
  type$wffJRvXKHjSzDZNs7b2S4g.GQ4ABvXKHjSzDZNs7b2S4g = function ()
  {
    var a = this;

    a.Buffer.splice(0, a.Buffer.length);
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Diagnostics.__Debug
  function Dhmwyss5uDO5UHO4wbYV7Q(){};
  Dhmwyss5uDO5UHO4wbYV7Q.TypeName = "Debug";
  Dhmwyss5uDO5UHO4wbYV7Q.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$Dhmwyss5uDO5UHO4wbYV7Q = Dhmwyss5uDO5UHO4wbYV7Q.prototype;
  type$Dhmwyss5uDO5UHO4wbYV7Q.constructor = Dhmwyss5uDO5UHO4wbYV7Q;
  var basector$Dhmwyss5uDO5UHO4wbYV7Q = $ctor$(null, null, type$Dhmwyss5uDO5UHO4wbYV7Q);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.Diagnostics.__Debug..ctor
  type$Dhmwyss5uDO5UHO4wbYV7Q.gA0ABss5uDO5UHO4wbYV7Q = function ()
  {
    var a = this;

  };
  var ctor$gA0ABss5uDO5UHO4wbYV7Q = Dhmwyss5uDO5UHO4wbYV7Q.ctor = $ctor$(null, 'gA0ABss5uDO5UHO4wbYV7Q', type$Dhmwyss5uDO5UHO4wbYV7Q);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Diagnostics.__Debug.Assert
  function fg0ABss5uDO5UHO4wbYV7Q(b)
  {
    var c;

    c = b;

    if (!c)
    {
      throw lwAABq9OGjCe3bHElJJ0LA('Assert failed');
    }

  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Diagnostics.__Debug.Assert
  function fw0ABss5uDO5UHO4wbYV7Q(b, c)
  {
    var d;

    d = b;

    if (!d)
    {
      throw lwAABq9OGjCe3bHElJJ0LA(gBMABjDeCj_aRJzaBmU9SJg('Assert failed: ', c));
    }

  };

  // ScriptCoreLib.JavaScript.Runtime.TimeFilter
  function __amoPTqpAQj_apuj6uPJmsSA(){};
  __amoPTqpAQj_apuj6uPJmsSA.TypeName = "TimeFilter";
  __amoPTqpAQj_apuj6uPJmsSA.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$__amoPTqpAQj_apuj6uPJmsSA = __amoPTqpAQj_apuj6uPJmsSA.prototype;
  type$__amoPTqpAQj_apuj6uPJmsSA.constructor = __amoPTqpAQj_apuj6uPJmsSA;
  type$__amoPTqpAQj_apuj6uPJmsSA.Value = null;
  type$__amoPTqpAQj_apuj6uPJmsSA.Window = 0;
  var basector$__amoPTqpAQj_apuj6uPJmsSA = $ctor$(null, null, type$__amoPTqpAQj_apuj6uPJmsSA);
  // ScriptCoreLib.JavaScript.Runtime.TimeFilter..ctor
  type$__amoPTqpAQj_apuj6uPJmsSA.ew0ABqpAQj_apuj6uPJmsSA = function (b)
  {
    var a = this;

    a.Window = b;
  };
  var ctor$ew0ABqpAQj_apuj6uPJmsSA = $ctor$(null, 'ew0ABqpAQj_apuj6uPJmsSA', type$__amoPTqpAQj_apuj6uPJmsSA);

  // ScriptCoreLib.JavaScript.Runtime.TimeFilter.get_IsValid
  type$__amoPTqpAQj_apuj6uPJmsSA.eg0ABqpAQj_apuj6uPJmsSA = function ()
  {
    var a = this, b;

    b = (Math.abs((a.Value - Zw0ABijAJDClltROMHFY_bw(Zg0ABijAJDClltROMHFY_bw()))) > a.Window);
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.TimeFilter.Update
  type$__amoPTqpAQj_apuj6uPJmsSA.fA0ABqpAQj_apuj6uPJmsSA = function ()
  {
    var a = this;

    a.Value = Zw0ABijAJDClltROMHFY_bw(Zg0ABijAJDClltROMHFY_bw());
  };

  // ScriptCoreLib.JavaScript.Runtime.TimeFilter.Invoke
  type$__amoPTqpAQj_apuj6uPJmsSA.fQ0ABqpAQj_apuj6uPJmsSA = function (b)
  {
    var a = this, c;

    c = a.eg0ABqpAQj_apuj6uPJmsSA();

    if (!c)
    {
      return;
    }

    sggABrDT9zCTidQoMa3Dig(b);
    a.fA0ABqpAQj_apuj6uPJmsSA();
  };

  // ScriptCoreLib.JavaScript.DOM.IDate.setFullYear
  // ScriptCoreLib.JavaScript.DOM.IDate.setMonth
  // ScriptCoreLib.JavaScript.DOM.IDate.setDate
  // ScriptCoreLib.JavaScript.DOM.IDate.getMilliseconds
  // ScriptCoreLib.JavaScript.DOM.IDate.getSeconds
  // ScriptCoreLib.JavaScript.DOM.IDate.getMinutes
  // ScriptCoreLib.JavaScript.DOM.IDate.getHours
  // ScriptCoreLib.JavaScript.DOM.IDate.getDate
  // ScriptCoreLib.JavaScript.DOM.IDate.getDay
  // ScriptCoreLib.JavaScript.DOM.IDate.getMonth
  // ScriptCoreLib.JavaScript.DOM.IDate.getFullYear
  // ScriptCoreLib.JavaScript.DOM.IDate.getTime
  // ScriptCoreLib.JavaScript.DOM.IDate.toGMTString
  // ScriptCoreLib.JavaScript.DOM.IDate.toLocaleString
  // ScriptCoreLib.JavaScript.DOM.IDate.get_Now
  function Zg0ABijAJDClltROMHFY_bw()
  {
    var b;

    b = new Date();
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.IDate.op_Implicit
  function Zw0ABijAJDClltROMHFY_bw(b)
  {
    var c;

    c = b.getTime();
    return c;
  };

  // ScriptCoreLib.JavaScript.DOM.IStyle.Apply
  function SA0ABoNSrD_aFizz5n6sJfw(a, b)
  {
    b.Invoke(a);
  };

  // ScriptCoreLib.JavaScript.DOM.IStyle.ToCenter
  function SQ0ABoNSrD_aFizz5n6sJfw(a, b, c, d)
  {
    a.position = 'absolute';
    Sw0ABoNSrD_aFizz5n6sJfw(a, ((b.clientWidth - c) / 2), ((b.clientHeight - d) / 2), c, d);
  };

  // ScriptCoreLib.JavaScript.DOM.IStyle.SetLocation
  function Sg0ABoNSrD_aFizz5n6sJfw(a, b, c)
  {
    a.position = 'absolute';
    a.left = fhMABjDeCj_aRJzaBmU9SJg(new Number(b), 'px');
    a.top = fhMABjDeCj_aRJzaBmU9SJg(new Number(c), 'px');
  };

  // ScriptCoreLib.JavaScript.DOM.IStyle.SetLocation
  function Sw0ABoNSrD_aFizz5n6sJfw(a, b, c, d, e)
  {
    Sg0ABoNSrD_aFizz5n6sJfw(a, b, c);
    TQ0ABoNSrD_aFizz5n6sJfw(a, d, e);
  };

  // ScriptCoreLib.JavaScript.DOM.IStyle.SetLocation
  function TA0ABoNSrD_aFizz5n6sJfw(a, b, c, d)
  {
    Sg0ABoNSrD_aFizz5n6sJfw(a, (b.offsetLeft - c), (b.offsetTop - d));
    TQ0ABoNSrD_aFizz5n6sJfw(a, (b.clientWidth + (c * 2)), (b.clientHeight + (d * 2)));
  };

  // ScriptCoreLib.JavaScript.DOM.IStyle.SetSize
  function TQ0ABoNSrD_aFizz5n6sJfw(a, b, c)
  {
    a.width = fhMABjDeCj_aRJzaBmU9SJg(new Number(b), 'px');
    a.height = fhMABjDeCj_aRJzaBmU9SJg(new Number(c), 'px');
  };

  // ScriptCoreLib.JavaScript.DOM.IStyle.SetSize
  function Tg0ABoNSrD_aFizz5n6sJfw(a, b)
  {
    TQ0ABoNSrD_aFizz5n6sJfw(a, b.clientWidth, b.clientHeight);
  };

  // ScriptCoreLib.JavaScript.DOM.IStyle.__opacity_internal
  function Tw0ABoNSrD_aFizz5n6sJfw(a0, a1) { 
            a0.filter = 'Alpha(Opacity=' + (a1 * 100) + ')';
            a0.opacity = a1;
         };
  // ScriptCoreLib.JavaScript.DOM.IStyle.set_Opacity
  function UA0ABoNSrD_aFizz5n6sJfw(a, b)
  {
    Tw0ABoNSrD_aFizz5n6sJfw(a, b);
  };

  // ScriptCoreLib.JavaScript.DOM.IStyle.__float_internal
  function UQ0ABoNSrD_aFizz5n6sJfw(a0, a1) { 
            a0.cssFloat = a1;
            a0.styleFloat = a1;
         };
  // ScriptCoreLib.JavaScript.DOM.IStyle.set_Float
  function Ug0ABoNSrD_aFizz5n6sJfw(a, b)
  {
    UQ0ABoNSrD_aFizz5n6sJfw(a, b);
  };

  // ScriptCoreLib.JavaScript.DOM.IStyle.SetLocation
  function Uw0ABoNSrD_aFizz5n6sJfw(a, b)
  {
    Sw0ABoNSrD_aFizz5n6sJfw(a, b.Left, b.Top, b.Width, b.Height);
  };

  // ScriptCoreLib.JavaScript.DOM.IStyle.SetBackground
  function VA0ABoNSrD_aFizz5n6sJfw(a, b, c)
  {
    var d;

    a.backgroundImage = gRMABjDeCj_aRJzaBmU9SJg('url(', b, ')');
    d = !c;

    if (!d)
    {
      a.backgroundRepeat = '';
      return;
    }

    a.backgroundRepeat = 'no-repeat';
  };

  var xwQABDlKHDqtgaJhGt6KBg = null;
  // ScriptCoreLib.JavaScript.DOM.IStyleSheet.get_Default
  function PQ0ABjlKHDqtgaJhGt6KBg()
  {
    var b, c;

    c = !(xwQABDlKHDqtgaJhGt6KBg == null);

    if (!c)
    {
      xwQABDlKHDqtgaJhGt6KBg = QA0ABjlKHDqtgaJhGt6KBg();
    }

    b = xwQABDlKHDqtgaJhGt6KBg;
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.IStyleSheet.get_Rules
  function Pg0ABjlKHDqtgaJhGt6KBg(a)
  {
    var b, c;

    c = !BRMABr5xMzijfM5xNYhyrw(a, 'cssRules');

    if (!c)
    {
      b = a.cssRules;
      return b;
    }

    c = !BRMABr5xMzijfM5xNYhyrw(a, 'rules');

    if (!c)
    {
      b = a.rules;
      return b;
    }

    throw lwAABq9OGjCe3bHElJJ0LA('member IStyleSheet.Rules not found');
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.IStyleSheet.InternalConstructor
  function QA0ABjlKHDqtgaJhGt6KBg()
  {
    var b, c, d, e;

    b = xREABtKyTTiMSMom97AAzA();
    c = document.getElementsByTagName('head');
    e = !(c.length > 0);

    if (!e)
    {
      c[0].appendChild(b);
    }
    else
    {
      cwsABt0jLD6yDQ0X6wt5_aw(b);
    }

    d = wxEABtKyTTiMSMom97AAzA(b);
    return d;
  };

  // ScriptCoreLib.JavaScript.DOM.IStyleSheet.addRule
  // ScriptCoreLib.JavaScript.DOM.IStyleSheet.insertRule
  // ScriptCoreLib.JavaScript.DOM.IStyleSheet.AddRule
  function Qw0ABjlKHDqtgaJhGt6KBg(a, b, c, d)
  {
    var e, f;

    f = !BRMABr5xMzijfM5xNYhyrw(a, 'insertRule');

    if (!f)
    {
      a.insertRule(ghMABjDeCj_aRJzaBmU9SJg(b, '{', c, '}'), d);
    }
    else
    {
      f = !BRMABr5xMzijfM5xNYhyrw(a, 'addRule');

      if (!f)
      {
        a.addRule(b, c, d);
      }
      else
      {
        throw lwAABq9OGjCe3bHElJJ0LA('fault at IStyleSheetRule.AddRule');
      }

    }

    e = Pg0ABjlKHDqtgaJhGt6KBg(a)[d];
    return e;
  };

  // ScriptCoreLib.JavaScript.DOM.IStyleSheet.AddRule
  function RA0ABjlKHDqtgaJhGt6KBg(a, b)
  {
    var c;

    c = Qw0ABjlKHDqtgaJhGt6KBg(a, b, '\u002f\u002a\u002a\u002f', Pg0ABjlKHDqtgaJhGt6KBg(a).length);
    return c;
  };

  // ScriptCoreLib.JavaScript.DOM.IStyleSheet.AddRule
  function RQ0ABjlKHDqtgaJhGt6KBg(a, b)
  {
    var c;

    c = Rg0ABjlKHDqtgaJhGt6KBg(a, b.rAMABsP0KzS_bdRyy1rdx_bg(), b.rgMABsP0KzS_bdRyy1rdx_bg());
    return c;
  };

  // ScriptCoreLib.JavaScript.DOM.IStyleSheet.AddRule
  function Rg0ABjlKHDqtgaJhGt6KBg(a, b, c)
  {
    var d, e;

    d = RA0ABjlKHDqtgaJhGt6KBg(a, b);
    c.Invoke(d);
    e = d;
    return e;
  };

  // ScriptCoreLib.JavaScript.DOM.IStyleSheet.get_Owner
  function Rw0ABjlKHDqtgaJhGt6KBg(a)
  {
    var b, c;

    c = !BRMABr5xMzijfM5xNYhyrw(a, 'ownerNode');

    if (!c)
    {
      b = a.ownerNode;
      return b;
    }

    c = !BRMABr5xMzijfM5xNYhyrw(a, 'owningElement');

    if (!c)
    {
      b = a.owningElement;
      return b;
    }

    throw lwAABq9OGjCe3bHElJJ0LA('fault at IStyleSheet.Owner');
    return b;
  };

  // ScriptCoreLib.Shared.AssemblyInfo
  function PZ9ITdF_bMzODN61n2IR6Jw(){};
  PZ9ITdF_bMzODN61n2IR6Jw.TypeName = "AssemblyInfo";
  PZ9ITdF_bMzODN61n2IR6Jw.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$PZ9ITdF_bMzODN61n2IR6Jw = PZ9ITdF_bMzODN61n2IR6Jw.prototype;
  type$PZ9ITdF_bMzODN61n2IR6Jw.constructor = PZ9ITdF_bMzODN61n2IR6Jw;
  var xAQABNF_bMzODN61n2IR6Jw = null;
  var basector$PZ9ITdF_bMzODN61n2IR6Jw = $ctor$(null, null, type$PZ9ITdF_bMzODN61n2IR6Jw);
  // ScriptCoreLib.Shared.AssemblyInfo..ctor
  type$PZ9ITdF_bMzODN61n2IR6Jw.Og0ABtF_bMzODN61n2IR6Jw = function ()
  {
    var a = this;

  };
  var ctor$Og0ABtF_bMzODN61n2IR6Jw = PZ9ITdF_bMzODN61n2IR6Jw.ctor = $ctor$(null, 'Og0ABtF_bMzODN61n2IR6Jw', type$PZ9ITdF_bMzODN61n2IR6Jw);

  // ScriptCoreLib.Shared.AssemblyInfo.get_BuildDateTimeString
  type$PZ9ITdF_bMzODN61n2IR6Jw.OA0ABtF_bMzODN61n2IR6Jw = function () { return '18.06.2009 19:33:33 UTC'; };
  // ScriptCoreLib.Shared.AssemblyInfo.get_ModuleName
  type$PZ9ITdF_bMzODN61n2IR6Jw.OQ0ABtF_bMzODN61n2IR6Jw = function () { return 'ScriptCoreLib.dll'; };
  // ScriptCoreLib.Shared.IAssemblyInfo
  // ScriptCoreLib.Shared.AssemblyInfo
  (function (i)  {
    i.mgMABpv81zGcdvtIbfyHsA = i.OA0ABtF_bMzODN61n2IR6Jw;
    i.mwMABpv81zGcdvtIbfyHsA = i.OQ0ABtF_bMzODN61n2IR6Jw;
  }
  )(type$PZ9ITdF_bMzODN61n2IR6Jw);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Char
  function ulZ6nbrjrzGhrKOv71Rbfw(){};
  ulZ6nbrjrzGhrKOv71Rbfw.TypeName = "Char";
  ulZ6nbrjrzGhrKOv71Rbfw.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$ulZ6nbrjrzGhrKOv71Rbfw = ulZ6nbrjrzGhrKOv71Rbfw.prototype;
  type$ulZ6nbrjrzGhrKOv71Rbfw.constructor = ulZ6nbrjrzGhrKOv71Rbfw;
  var basector$ulZ6nbrjrzGhrKOv71Rbfw = $ctor$(null, null, type$ulZ6nbrjrzGhrKOv71Rbfw);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Char..ctor
  type$ulZ6nbrjrzGhrKOv71Rbfw.vQwABrrjrzGhrKOv71Rbfw = function ()
  {
    var a = this;

  };
  var ctor$vQwABrrjrzGhrKOv71Rbfw = ulZ6nbrjrzGhrKOv71Rbfw.ctor = $ctor$(null, 'vQwABrrjrzGhrKOv71Rbfw', type$ulZ6nbrjrzGhrKOv71Rbfw);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Char.IsNumber
  function uwwABrrjrzGhrKOv71Rbfw(b, c)
  {
    var d;

    d = vAwABrrjrzGhrKOv71Rbfw(eRMABjDeCj_aRJzaBmU9SJg(b, c));
    return d;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Char.IsNumber
  function vAwABrrjrzGhrKOv71Rbfw(b)
  {
    var c, d;

    d = !(b == 48);

    if (!d)
    {
      c = 1;
      return c;
    }

    d = !(b == 49);

    if (!d)
    {
      c = 1;
      return c;
    }

    d = !(b == 50);

    if (!d)
    {
      c = 1;
      return c;
    }

    d = !(b == 51);

    if (!d)
    {
      c = 1;
      return c;
    }

    d = !(b == 52);

    if (!d)
    {
      c = 1;
      return c;
    }

    d = !(b == 53);

    if (!d)
    {
      c = 1;
      return c;
    }

    d = !(b == 54);

    if (!d)
    {
      c = 1;
      return c;
    }

    d = !(b == 55);

    if (!d)
    {
      c = 1;
      return c;
    }

    d = !(b == 56);

    if (!d)
    {
      c = 1;
      return c;
    }

    d = !(b == 57);

    if (!d)
    {
      c = 1;
      return c;
    }

    c = 0;
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Stack`1
  function L5pu4WOwEjOAsqGKZ0msvA(){};
  L5pu4WOwEjOAsqGKZ0msvA.TypeName = "Stack_1";
  L5pu4WOwEjOAsqGKZ0msvA.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$L5pu4WOwEjOAsqGKZ0msvA = L5pu4WOwEjOAsqGKZ0msvA.prototype;
  type$L5pu4WOwEjOAsqGKZ0msvA.constructor = L5pu4WOwEjOAsqGKZ0msvA;
  type$L5pu4WOwEjOAsqGKZ0msvA.items = null;
  var basector$L5pu4WOwEjOAsqGKZ0msvA = $ctor$(null, null, type$L5pu4WOwEjOAsqGKZ0msvA);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Stack`1..ctor
  type$L5pu4WOwEjOAsqGKZ0msvA.ugwABmOwEjOAsqGKZ0msvA = function ()
  {
    var a = this;

    a.items = Hw4ABqPHMD_aEoYb_aoNHL9w();
  };
  var ctor$ugwABmOwEjOAsqGKZ0msvA = L5pu4WOwEjOAsqGKZ0msvA.ctor = $ctor$(null, 'ugwABmOwEjOAsqGKZ0msvA', type$L5pu4WOwEjOAsqGKZ0msvA);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Stack`1.Pop
  type$L5pu4WOwEjOAsqGKZ0msvA.tAwABmOwEjOAsqGKZ0msvA = function ()
  {
    var a = this, b;

    b = a.items.pop();
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Stack`1.Push
  type$L5pu4WOwEjOAsqGKZ0msvA.tQwABmOwEjOAsqGKZ0msvA = function (b)
  {
    var a = this;

    a.items.push(b);
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Stack`1.get_Count
  type$L5pu4WOwEjOAsqGKZ0msvA.tgwABmOwEjOAsqGKZ0msvA = function ()
  {
    var a = this, b;

    b = a.items.length;
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Stack`1.Clear
  type$L5pu4WOwEjOAsqGKZ0msvA.twwABmOwEjOAsqGKZ0msvA = function ()
  {
    var a = this;

    a.items.splice(0, a.items.length);
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Stack`1.GetEnumerator
  type$L5pu4WOwEjOAsqGKZ0msvA.uAwABmOwEjOAsqGKZ0msvA = function ()
  {
    var a = this, b, c;

    b = a.items;
    c = new ctor$pggABhLuAz2cCkYgRA33LQ(b);
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Stack`1.System.Collections.IEnumerable.GetEnumerator
  type$L5pu4WOwEjOAsqGKZ0msvA.uQwABmOwEjOAsqGKZ0msvA = function ()
  {
    var a = this, b;

    b = a.uAwABmOwEjOAsqGKZ0msvA();
    return b;
  };

  // 
  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Stack`1
  (function (i)  {
    i.NgEABnMeWzaNooAKOmFm5g = i.uAwABmOwEjOAsqGKZ0msvA;
    // System.Collections.IEnumerable
    i.EQAABu7N0xGI6ACQJ1TEOg = i.uQwABmOwEjOAsqGKZ0msvA;
  }
  )(type$L5pu4WOwEjOAsqGKZ0msvA);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Delegate
  function _22cEaagPxjmDnjkmC_a5nbw(){};
  _22cEaagPxjmDnjkmC_a5nbw.TypeName = "Delegate";
  _22cEaagPxjmDnjkmC_a5nbw.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$_22cEaagPxjmDnjkmC_a5nbw = _22cEaagPxjmDnjkmC_a5nbw.prototype;
  type$_22cEaagPxjmDnjkmC_a5nbw.constructor = _22cEaagPxjmDnjkmC_a5nbw;
  type$_22cEaagPxjmDnjkmC_a5nbw.Target = null;
  type$_22cEaagPxjmDnjkmC_a5nbw.Method = null;
  type$_22cEaagPxjmDnjkmC_a5nbw.InvokePointerCache = null;
  var basector$_22cEaagPxjmDnjkmC_a5nbw = $ctor$(null, null, type$_22cEaagPxjmDnjkmC_a5nbw);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Delegate..ctor
  type$_22cEaagPxjmDnjkmC_a5nbw.fQsABqgPxjmDnjkmC_a5nbw = function (b, c)
  {
    var a = this;

    a.Target = (!(b) ? window : b);
    a.Method = c;
  };
  var ctor$fQsABqgPxjmDnjkmC_a5nbw = $ctor$(null, 'fQsABqgPxjmDnjkmC_a5nbw', type$_22cEaagPxjmDnjkmC_a5nbw);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Delegate.get_InvokePointer
  type$_22cEaagPxjmDnjkmC_a5nbw.fAsABqgPxjmDnjkmC_a5nbw = function ()
  {
    var a = this, b, c;

    c = !(a.InvokePointerCache == null);

    if (!c)
    {
      a.InvokePointerCache = fgsABqgPxjmDnjkmC_a5nbw(a.Target, a.Method);
    }

    b = a.InvokePointerCache;
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Delegate.InternalGetAsyncInvoke
  function fgsABqgPxjmDnjkmC_a5nbw(o, p) { return function(a0, a1) { return o[p](a0, a1); } };
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Delegate.Combine
  function fwsABqgPxjmDnjkmC_a5nbw(b, c)
  {
    var d, e;

    e = !(b == null);

    if (!e)
    {
      d = c;
      return d;
    }

    e = !(c == null);

    if (!e)
    {
      d = b;
      return d;
    }

    d = b.gAsABqgPxjmDnjkmC_a5nbw(c);
    return d;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Delegate.CombineImpl
  type$_22cEaagPxjmDnjkmC_a5nbw.gAsABqgPxjmDnjkmC_a5nbw = function (b)
  {
    var a = this;

    throw lwAABq9OGjCe3bHElJJ0LA('use MulticastDelegate instead');
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Delegate.Remove
  function gQsABqgPxjmDnjkmC_a5nbw(b, c)
  {
    var d, e;

    e = !(b == null);

    if (!e)
    {
      d = null;
      return d;
    }

    e = !(c == null);

    if (!e)
    {
      d = b;
      return d;
    }

    d = b.ggsABqgPxjmDnjkmC_a5nbw(c);
    return d;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Delegate.RemoveImpl
  type$_22cEaagPxjmDnjkmC_a5nbw.ggsABqgPxjmDnjkmC_a5nbw = function (b)
  {
    var a = this;

    throw lwAABq9OGjCe3bHElJJ0LA('use MulticastDelegate instead');
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Delegate.Equals
  type$_22cEaagPxjmDnjkmC_a5nbw.gwsABqgPxjmDnjkmC_a5nbw = function (b)
  {
    var a = this, c;

    c = hAsABqgPxjmDnjkmC_a5nbw(a, b);
    return c;
  };
    _22cEaagPxjmDnjkmC_a5nbw.prototype.AwAABnwCHD6Y1dqcmGKqIQ = _22cEaagPxjmDnjkmC_a5nbw.prototype.gwsABqgPxjmDnjkmC_a5nbw;

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Delegate.IsEqual
  function hAsABqgPxjmDnjkmC_a5nbw(b, c)
  {
    var d, e;

    e = !(b == null);

    if (!e)
    {
      d = 0;
      return d;
    }

    e = !(c == null);

    if (!e)
    {
      d = 0;
      return d;
    }

    d = (BAUABsfB_bTOt1XXfMkgmbQ(b.Method, c.Method) && (b.Target == c.Target));
    return d;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Delegate.GetHashCode
  type$_22cEaagPxjmDnjkmC_a5nbw.hQsABqgPxjmDnjkmC_a5nbw = function ()
  {
    var a = this, b;

    b = 0;
    return b;
  };
    _22cEaagPxjmDnjkmC_a5nbw.prototype.BwAABnwCHD6Y1dqcmGKqIQ = _22cEaagPxjmDnjkmC_a5nbw.prototype.hQsABqgPxjmDnjkmC_a5nbw;

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__MulticastDelegate
  function kgJWtah_alDaJsZAeVC_bgaA(){};
  kgJWtah_alDaJsZAeVC_bgaA.TypeName = "MulticastDelegate";
  kgJWtah_alDaJsZAeVC_bgaA.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$kgJWtah_alDaJsZAeVC_bgaA = kgJWtah_alDaJsZAeVC_bgaA.prototype = new _22cEaagPxjmDnjkmC_a5nbw();
  type$kgJWtah_alDaJsZAeVC_bgaA.constructor = kgJWtah_alDaJsZAeVC_bgaA;
  type$kgJWtah_alDaJsZAeVC_bgaA.list = null;
  var basector$kgJWtah_alDaJsZAeVC_bgaA = $ctor$(basector$_22cEaagPxjmDnjkmC_a5nbw, null, type$kgJWtah_alDaJsZAeVC_bgaA);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__MulticastDelegate..ctor
  type$kgJWtah_alDaJsZAeVC_bgaA.hgsABqh_alDaJsZAeVC_bgaA = function (b, c)
  {
    var a = this;

    a.list = Hw4ABqPHMD_aEoYb_aoNHL9w();
    a.fQsABqgPxjmDnjkmC_a5nbw(b, c);
    a.list.push(a);
  };
  var ctor$hgsABqh_alDaJsZAeVC_bgaA = $ctor$(basector$_22cEaagPxjmDnjkmC_a5nbw, 'hgsABqh_alDaJsZAeVC_bgaA', type$kgJWtah_alDaJsZAeVC_bgaA);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__MulticastDelegate.CombineImpl
  type$kgJWtah_alDaJsZAeVC_bgaA.hwsABqh_alDaJsZAeVC_bgaA = function (b)
  {
    var a = this, c;

    a.list.push(b);
    c = a;
    return c;
  };
    kgJWtah_alDaJsZAeVC_bgaA.prototype.gAsABqgPxjmDnjkmC_a5nbw = kgJWtah_alDaJsZAeVC_bgaA.prototype.hwsABqh_alDaJsZAeVC_bgaA;

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__MulticastDelegate.RemoveImpl
  type$kgJWtah_alDaJsZAeVC_bgaA.iAsABqh_alDaJsZAeVC_bgaA = function (b)
  {
    var a = this, c, d, e, f;

    c = -1;

    for (d = 0; (d < a.list.length); d++)
    {
      f = !(LA4ABqPHMD_aEoYb_aoNHL9w(a.list, d) == b);

      if (!f)
      {
        c = d;
        break;
      }

    }

    f = !(c > -1);

    if (!f)
    {
      a.list.splice(c, 1);
    }

    f = !!a.list.length;

    if (!f)
    {
      e = null;
      return e;
    }

    e = a;
    return e;
  };
    kgJWtah_alDaJsZAeVC_bgaA.prototype.ggsABqgPxjmDnjkmC_a5nbw = kgJWtah_alDaJsZAeVC_bgaA.prototype.iAsABqh_alDaJsZAeVC_bgaA;

  // delegate: (a, b, c) => T
  // ScriptCoreLib.Shared.InternalFunc`4
  function LFasTNMeczSL0Nma4X8IUw(){};
  LFasTNMeczSL0Nma4X8IUw.TypeName = "InternalFunc_4";
  LFasTNMeczSL0Nma4X8IUw.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$LFasTNMeczSL0Nma4X8IUw = LFasTNMeczSL0Nma4X8IUw.prototype = new kgJWtah_alDaJsZAeVC_bgaA();
  type$LFasTNMeczSL0Nma4X8IUw.constructor = LFasTNMeczSL0Nma4X8IUw;
  type$LFasTNMeczSL0Nma4X8IUw.IsExtensionMethod = false;
  type$LFasTNMeczSL0Nma4X8IUw.AsExtensionMethod = function ()
  {
    this.IsExtensionMethod = true;
    return this;
  };
  type$LFasTNMeczSL0Nma4X8IUw.QQsABtMeczSL0Nma4X8IUw = type$kgJWtah_alDaJsZAeVC_bgaA.hgsABqh_alDaJsZAeVC_bgaA;
  var ctor$QQsABtMeczSL0Nma4X8IUw = $ctor$(basector$kgJWtah_alDaJsZAeVC_bgaA, 'QQsABtMeczSL0Nma4X8IUw', type$LFasTNMeczSL0Nma4X8IUw);
  type$LFasTNMeczSL0Nma4X8IUw.Invoke = function (b, c, d)
  {
    var _ = void(0);
    for (var _i = 0; _i < this.list.length; _i++)
    {
      var _f = this.list[_i];
      var _arguments = Array.prototype.slice.call(arguments).slice(0);
      if(_f.IsExtensionMethod) _arguments.splice(0, 0, _f.Target);
      var _target = _f.IsExtensionMethod ? window : _f.Target;
      _ = _target[_f.Method].apply(_target, _arguments);
    }
    return _;
  };

  // delegate: (a, b) => T
  // ScriptCoreLib.Shared.InternalFunc`3
  function JMDUiknYYzqUF_bGIp4_aaGQ(){};
  JMDUiknYYzqUF_bGIp4_aaGQ.TypeName = "InternalFunc_3";
  JMDUiknYYzqUF_bGIp4_aaGQ.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$JMDUiknYYzqUF_bGIp4_aaGQ = JMDUiknYYzqUF_bGIp4_aaGQ.prototype = new kgJWtah_alDaJsZAeVC_bgaA();
  type$JMDUiknYYzqUF_bGIp4_aaGQ.constructor = JMDUiknYYzqUF_bGIp4_aaGQ;
  type$JMDUiknYYzqUF_bGIp4_aaGQ.IsExtensionMethod = false;
  type$JMDUiknYYzqUF_bGIp4_aaGQ.AsExtensionMethod = function ()
  {
    this.IsExtensionMethod = true;
    return this;
  };
  type$JMDUiknYYzqUF_bGIp4_aaGQ.PQsABknYYzqUF_bGIp4_aaGQ = type$kgJWtah_alDaJsZAeVC_bgaA.hgsABqh_alDaJsZAeVC_bgaA;
  var ctor$PQsABknYYzqUF_bGIp4_aaGQ = $ctor$(basector$kgJWtah_alDaJsZAeVC_bgaA, 'PQsABknYYzqUF_bGIp4_aaGQ', type$JMDUiknYYzqUF_bGIp4_aaGQ);
  type$JMDUiknYYzqUF_bGIp4_aaGQ.Invoke = function (b, c)
  {
    var _ = void(0);
    for (var _i = 0; _i < this.list.length; _i++)
    {
      var _f = this.list[_i];
      var _arguments = Array.prototype.slice.call(arguments).slice(0);
      if(_f.IsExtensionMethod) _arguments.splice(0, 0, _f.Target);
      var _target = _f.IsExtensionMethod ? window : _f.Target;
      _ = _target[_f.Method].apply(_target, _arguments);
    }
    return _;
  };

  // delegate: (a) => T
  // ScriptCoreLib.Shared.InternalFunc`2
  function DlKQv4vAgzaPrv33odvgUQ(){};
  DlKQv4vAgzaPrv33odvgUQ.TypeName = "InternalFunc_2";
  DlKQv4vAgzaPrv33odvgUQ.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$DlKQv4vAgzaPrv33odvgUQ = DlKQv4vAgzaPrv33odvgUQ.prototype = new kgJWtah_alDaJsZAeVC_bgaA();
  type$DlKQv4vAgzaPrv33odvgUQ.constructor = DlKQv4vAgzaPrv33odvgUQ;
  type$DlKQv4vAgzaPrv33odvgUQ.IsExtensionMethod = false;
  type$DlKQv4vAgzaPrv33odvgUQ.AsExtensionMethod = function ()
  {
    this.IsExtensionMethod = true;
    return this;
  };
  type$DlKQv4vAgzaPrv33odvgUQ.OQsABovAgzaPrv33odvgUQ = type$kgJWtah_alDaJsZAeVC_bgaA.hgsABqh_alDaJsZAeVC_bgaA;
  var ctor$OQsABovAgzaPrv33odvgUQ = $ctor$(basector$kgJWtah_alDaJsZAeVC_bgaA, 'OQsABovAgzaPrv33odvgUQ', type$DlKQv4vAgzaPrv33odvgUQ);
  type$DlKQv4vAgzaPrv33odvgUQ.Invoke = function (b)
  {
    var _ = void(0);
    for (var _i = 0; _i < this.list.length; _i++)
    {
      var _f = this.list[_i];
      var _arguments = Array.prototype.slice.call(arguments).slice(0);
      if(_f.IsExtensionMethod) _arguments.splice(0, 0, _f.Target);
      var _target = _f.IsExtensionMethod ? window : _f.Target;
      _ = _target[_f.Method].apply(_target, _arguments);
    }
    return _;
  };

  // delegate: (e) => R
  // ScriptCoreLib.Shared.FuncParams`2
  function e36bjRxQJTKxhL2I_aKPoYg(){};
  e36bjRxQJTKxhL2I_aKPoYg.TypeName = "FuncParams_2";
  e36bjRxQJTKxhL2I_aKPoYg.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$e36bjRxQJTKxhL2I_aKPoYg = e36bjRxQJTKxhL2I_aKPoYg.prototype = new kgJWtah_alDaJsZAeVC_bgaA();
  type$e36bjRxQJTKxhL2I_aKPoYg.constructor = e36bjRxQJTKxhL2I_aKPoYg;
  type$e36bjRxQJTKxhL2I_aKPoYg.IsExtensionMethod = false;
  type$e36bjRxQJTKxhL2I_aKPoYg.AsExtensionMethod = function ()
  {
    this.IsExtensionMethod = true;
    return this;
  };
  type$e36bjRxQJTKxhL2I_aKPoYg.NQsABhxQJTKxhL2I_aKPoYg = type$kgJWtah_alDaJsZAeVC_bgaA.hgsABqh_alDaJsZAeVC_bgaA;
  var ctor$NQsABhxQJTKxhL2I_aKPoYg = $ctor$(basector$kgJWtah_alDaJsZAeVC_bgaA, 'NQsABhxQJTKxhL2I_aKPoYg', type$e36bjRxQJTKxhL2I_aKPoYg);
  type$e36bjRxQJTKxhL2I_aKPoYg.Invoke = function (b)
  {
    var _ = void(0);
    for (var _i = 0; _i < this.list.length; _i++)
    {
      var _f = this.list[_i];
      var _arguments = Array.prototype.slice.call(arguments).slice(0);
      if(_f.IsExtensionMethod) _arguments.splice(0, 0, _f.Target);
      var _target = _f.IsExtensionMethod ? window : _f.Target;
      _ = _target[_f.Method].apply(_target, _arguments);
    }
    return _;
  };

  // delegate: () => Void
  // ScriptCoreLib.Shared.InternalAction
  function _014DOB_bO7jaSPMhX_aXFxFw(){};
  _014DOB_bO7jaSPMhX_aXFxFw.TypeName = "InternalAction";
  _014DOB_bO7jaSPMhX_aXFxFw.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$_014DOB_bO7jaSPMhX_aXFxFw = _014DOB_bO7jaSPMhX_aXFxFw.prototype = new kgJWtah_alDaJsZAeVC_bgaA();
  type$_014DOB_bO7jaSPMhX_aXFxFw.constructor = _014DOB_bO7jaSPMhX_aXFxFw;
  type$_014DOB_bO7jaSPMhX_aXFxFw.IsExtensionMethod = false;
  type$_014DOB_bO7jaSPMhX_aXFxFw.AsExtensionMethod = function ()
  {
    this.IsExtensionMethod = true;
    return this;
  };
  type$_014DOB_bO7jaSPMhX_aXFxFw.MQsABh_bO7jaSPMhX_aXFxFw = type$kgJWtah_alDaJsZAeVC_bgaA.hgsABqh_alDaJsZAeVC_bgaA;
  var ctor$MQsABh_bO7jaSPMhX_aXFxFw = $ctor$(basector$kgJWtah_alDaJsZAeVC_bgaA, 'MQsABh_bO7jaSPMhX_aXFxFw', type$_014DOB_bO7jaSPMhX_aXFxFw);
  type$_014DOB_bO7jaSPMhX_aXFxFw.Invoke = function ()
  {
    for (var _i = 0; _i < this.list.length; _i++)
    {
      var _f = this.list[_i];
      var _arguments = Array.prototype.slice.call(arguments).slice(0);
      if(_f.IsExtensionMethod) _arguments.splice(0, 0, _f.Target);
      var _target = _f.IsExtensionMethod ? window : _f.Target;
      _target[_f.Method].apply(_target, _arguments);
    }
  };

  // delegate: () => Void
  // ScriptCoreLib.Shared.EventHandler
  function iXl4fBarvz6dEOdzk7hI5Q(){};
  iXl4fBarvz6dEOdzk7hI5Q.TypeName = "EventHandler";
  iXl4fBarvz6dEOdzk7hI5Q.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$iXl4fBarvz6dEOdzk7hI5Q = iXl4fBarvz6dEOdzk7hI5Q.prototype = new kgJWtah_alDaJsZAeVC_bgaA();
  type$iXl4fBarvz6dEOdzk7hI5Q.constructor = iXl4fBarvz6dEOdzk7hI5Q;
  type$iXl4fBarvz6dEOdzk7hI5Q.IsExtensionMethod = false;
  type$iXl4fBarvz6dEOdzk7hI5Q.AsExtensionMethod = function ()
  {
    this.IsExtensionMethod = true;
    return this;
  };
  type$iXl4fBarvz6dEOdzk7hI5Q.LQsABharvz6dEOdzk7hI5Q = type$kgJWtah_alDaJsZAeVC_bgaA.hgsABqh_alDaJsZAeVC_bgaA;
  var ctor$LQsABharvz6dEOdzk7hI5Q = $ctor$(basector$kgJWtah_alDaJsZAeVC_bgaA, 'LQsABharvz6dEOdzk7hI5Q', type$iXl4fBarvz6dEOdzk7hI5Q);
  type$iXl4fBarvz6dEOdzk7hI5Q.Invoke = function ()
  {
    for (var _i = 0; _i < this.list.length; _i++)
    {
      var _f = this.list[_i];
      var _arguments = Array.prototype.slice.call(arguments).slice(0);
      if(_f.IsExtensionMethod) _arguments.splice(0, 0, _f.Target);
      var _target = _f.IsExtensionMethod ? window : _f.Target;
      _target[_f.Method].apply(_target, _arguments);
    }
  };

  // delegate: (e) => Void
  // ScriptCoreLib.Shared.EventHandler`1
  function L1nMQoVjnjetV_aXG4GFkBQ(){};
  L1nMQoVjnjetV_aXG4GFkBQ.TypeName = "EventHandler_1";
  L1nMQoVjnjetV_aXG4GFkBQ.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$L1nMQoVjnjetV_aXG4GFkBQ = L1nMQoVjnjetV_aXG4GFkBQ.prototype = new kgJWtah_alDaJsZAeVC_bgaA();
  type$L1nMQoVjnjetV_aXG4GFkBQ.constructor = L1nMQoVjnjetV_aXG4GFkBQ;
  type$L1nMQoVjnjetV_aXG4GFkBQ.IsExtensionMethod = false;
  type$L1nMQoVjnjetV_aXG4GFkBQ.AsExtensionMethod = function ()
  {
    this.IsExtensionMethod = true;
    return this;
  };
  type$L1nMQoVjnjetV_aXG4GFkBQ.KQsABoVjnjetV_aXG4GFkBQ = type$kgJWtah_alDaJsZAeVC_bgaA.hgsABqh_alDaJsZAeVC_bgaA;
  var ctor$KQsABoVjnjetV_aXG4GFkBQ = $ctor$(basector$kgJWtah_alDaJsZAeVC_bgaA, 'KQsABoVjnjetV_aXG4GFkBQ', type$L1nMQoVjnjetV_aXG4GFkBQ);
  type$L1nMQoVjnjetV_aXG4GFkBQ.Invoke = function (b)
  {
    for (var _i = 0; _i < this.list.length; _i++)
    {
      var _f = this.list[_i];
      var _arguments = Array.prototype.slice.call(arguments).slice(0);
      if(_f.IsExtensionMethod) _arguments.splice(0, 0, _f.Target);
      var _target = _f.IsExtensionMethod ? window : _f.Target;
      _target[_f.Method].apply(_target, _arguments);
    }
  };

  // delegate: (e, p) => Void
  // ScriptCoreLib.Shared.EventHandler`2
  function tEhRuyiBODO0UmkEsz3uYQ(){};
  tEhRuyiBODO0UmkEsz3uYQ.TypeName = "EventHandler_2";
  tEhRuyiBODO0UmkEsz3uYQ.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$tEhRuyiBODO0UmkEsz3uYQ = tEhRuyiBODO0UmkEsz3uYQ.prototype = new kgJWtah_alDaJsZAeVC_bgaA();
  type$tEhRuyiBODO0UmkEsz3uYQ.constructor = tEhRuyiBODO0UmkEsz3uYQ;
  type$tEhRuyiBODO0UmkEsz3uYQ.IsExtensionMethod = false;
  type$tEhRuyiBODO0UmkEsz3uYQ.AsExtensionMethod = function ()
  {
    this.IsExtensionMethod = true;
    return this;
  };
  type$tEhRuyiBODO0UmkEsz3uYQ.JQsABiiBODO0UmkEsz3uYQ = type$kgJWtah_alDaJsZAeVC_bgaA.hgsABqh_alDaJsZAeVC_bgaA;
  var ctor$JQsABiiBODO0UmkEsz3uYQ = $ctor$(basector$kgJWtah_alDaJsZAeVC_bgaA, 'JQsABiiBODO0UmkEsz3uYQ', type$tEhRuyiBODO0UmkEsz3uYQ);
  type$tEhRuyiBODO0UmkEsz3uYQ.Invoke = function (b, c)
  {
    for (var _i = 0; _i < this.list.length; _i++)
    {
      var _f = this.list[_i];
      var _arguments = Array.prototype.slice.call(arguments).slice(0);
      if(_f.IsExtensionMethod) _arguments.splice(0, 0, _f.Target);
      var _target = _f.IsExtensionMethod ? window : _f.Target;
      _target[_f.Method].apply(_target, _arguments);
    }
  };

  // delegate: (input) => TOutput
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Converter`2
  function VeOC2hfi2TSuNTV3iROB_bw(){};
  VeOC2hfi2TSuNTV3iROB_bw.TypeName = "Converter_2";
  VeOC2hfi2TSuNTV3iROB_bw.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$VeOC2hfi2TSuNTV3iROB_bw = VeOC2hfi2TSuNTV3iROB_bw.prototype = new kgJWtah_alDaJsZAeVC_bgaA();
  type$VeOC2hfi2TSuNTV3iROB_bw.constructor = VeOC2hfi2TSuNTV3iROB_bw;
  type$VeOC2hfi2TSuNTV3iROB_bw.IsExtensionMethod = false;
  type$VeOC2hfi2TSuNTV3iROB_bw.AsExtensionMethod = function ()
  {
    this.IsExtensionMethod = true;
    return this;
  };
  type$VeOC2hfi2TSuNTV3iROB_bw.AgsABhfi2TSuNTV3iROB_bw = type$kgJWtah_alDaJsZAeVC_bgaA.hgsABqh_alDaJsZAeVC_bgaA;
  var ctor$AgsABhfi2TSuNTV3iROB_bw = $ctor$(basector$kgJWtah_alDaJsZAeVC_bgaA, 'AgsABhfi2TSuNTV3iROB_bw', type$VeOC2hfi2TSuNTV3iROB_bw);
  type$VeOC2hfi2TSuNTV3iROB_bw.Invoke = function (b)
  {
    var _ = void(0);
    for (var _i = 0; _i < this.list.length; _i++)
    {
      var _f = this.list[_i];
      var _arguments = Array.prototype.slice.call(arguments).slice(0);
      if(_f.IsExtensionMethod) _arguments.splice(0, 0, _f.Target);
      var _target = _f.IsExtensionMethod ? window : _f.Target;
      _ = _target[_f.Method].apply(_target, _arguments);
    }
    return _;
  };

  // delegate: (arg0, arg1, arg2, arg3) => Void
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Action`4
  function qTU18YokIjaQxqtHcBhVoQ(){};
  qTU18YokIjaQxqtHcBhVoQ.TypeName = "Action_4";
  qTU18YokIjaQxqtHcBhVoQ.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$qTU18YokIjaQxqtHcBhVoQ = qTU18YokIjaQxqtHcBhVoQ.prototype = new kgJWtah_alDaJsZAeVC_bgaA();
  type$qTU18YokIjaQxqtHcBhVoQ.constructor = qTU18YokIjaQxqtHcBhVoQ;
  type$qTU18YokIjaQxqtHcBhVoQ.IsExtensionMethod = false;
  type$qTU18YokIjaQxqtHcBhVoQ.AsExtensionMethod = function ()
  {
    this.IsExtensionMethod = true;
    return this;
  };
  type$qTU18YokIjaQxqtHcBhVoQ.GwYABookIjaQxqtHcBhVoQ = type$kgJWtah_alDaJsZAeVC_bgaA.hgsABqh_alDaJsZAeVC_bgaA;
  var ctor$GwYABookIjaQxqtHcBhVoQ = $ctor$(basector$kgJWtah_alDaJsZAeVC_bgaA, 'GwYABookIjaQxqtHcBhVoQ', type$qTU18YokIjaQxqtHcBhVoQ);
  type$qTU18YokIjaQxqtHcBhVoQ.Invoke = function (b, c, d, e)
  {
    for (var _i = 0; _i < this.list.length; _i++)
    {
      var _f = this.list[_i];
      var _arguments = Array.prototype.slice.call(arguments).slice(0);
      if(_f.IsExtensionMethod) _arguments.splice(0, 0, _f.Target);
      var _target = _f.IsExtensionMethod ? window : _f.Target;
      _target[_f.Method].apply(_target, _arguments);
    }
  };

  // delegate: (arg0, arg1, arg2) => Void
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Action`3
  function _5TH1DSr07DG96Cs6JXqPaw(){};
  _5TH1DSr07DG96Cs6JXqPaw.TypeName = "Action_3";
  _5TH1DSr07DG96Cs6JXqPaw.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$_5TH1DSr07DG96Cs6JXqPaw = _5TH1DSr07DG96Cs6JXqPaw.prototype = new kgJWtah_alDaJsZAeVC_bgaA();
  type$_5TH1DSr07DG96Cs6JXqPaw.constructor = _5TH1DSr07DG96Cs6JXqPaw;
  type$_5TH1DSr07DG96Cs6JXqPaw.IsExtensionMethod = false;
  type$_5TH1DSr07DG96Cs6JXqPaw.AsExtensionMethod = function ()
  {
    this.IsExtensionMethod = true;
    return this;
  };
  type$_5TH1DSr07DG96Cs6JXqPaw.FwYABir07DG96Cs6JXqPaw = type$kgJWtah_alDaJsZAeVC_bgaA.hgsABqh_alDaJsZAeVC_bgaA;
  var ctor$FwYABir07DG96Cs6JXqPaw = $ctor$(basector$kgJWtah_alDaJsZAeVC_bgaA, 'FwYABir07DG96Cs6JXqPaw', type$_5TH1DSr07DG96Cs6JXqPaw);
  type$_5TH1DSr07DG96Cs6JXqPaw.Invoke = function (b, c, d)
  {
    for (var _i = 0; _i < this.list.length; _i++)
    {
      var _f = this.list[_i];
      var _arguments = Array.prototype.slice.call(arguments).slice(0);
      if(_f.IsExtensionMethod) _arguments.splice(0, 0, _f.Target);
      var _target = _f.IsExtensionMethod ? window : _f.Target;
      _target[_f.Method].apply(_target, _arguments);
    }
  };

  // delegate: (arg0, arg1) => Void
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Action`2
  function HQVp6ScseTyEC3C_acJNezQ(){};
  HQVp6ScseTyEC3C_acJNezQ.TypeName = "Action_2";
  HQVp6ScseTyEC3C_acJNezQ.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$HQVp6ScseTyEC3C_acJNezQ = HQVp6ScseTyEC3C_acJNezQ.prototype = new kgJWtah_alDaJsZAeVC_bgaA();
  type$HQVp6ScseTyEC3C_acJNezQ.constructor = HQVp6ScseTyEC3C_acJNezQ;
  type$HQVp6ScseTyEC3C_acJNezQ.IsExtensionMethod = false;
  type$HQVp6ScseTyEC3C_acJNezQ.AsExtensionMethod = function ()
  {
    this.IsExtensionMethod = true;
    return this;
  };
  type$HQVp6ScseTyEC3C_acJNezQ.EwYABicseTyEC3C_acJNezQ = type$kgJWtah_alDaJsZAeVC_bgaA.hgsABqh_alDaJsZAeVC_bgaA;
  var ctor$EwYABicseTyEC3C_acJNezQ = $ctor$(basector$kgJWtah_alDaJsZAeVC_bgaA, 'EwYABicseTyEC3C_acJNezQ', type$HQVp6ScseTyEC3C_acJNezQ);
  type$HQVp6ScseTyEC3C_acJNezQ.Invoke = function (b, c)
  {
    for (var _i = 0; _i < this.list.length; _i++)
    {
      var _f = this.list[_i];
      var _arguments = Array.prototype.slice.call(arguments).slice(0);
      if(_f.IsExtensionMethod) _arguments.splice(0, 0, _f.Target);
      var _target = _f.IsExtensionMethod ? window : _f.Target;
      _target[_f.Method].apply(_target, _arguments);
    }
  };

  // delegate: () => Void
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Action
  function __bJQ43mBkATis_auW8hsdm0Q(){};
  __bJQ43mBkATis_auW8hsdm0Q.TypeName = "Action";
  __bJQ43mBkATis_auW8hsdm0Q.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$__bJQ43mBkATis_auW8hsdm0Q = __bJQ43mBkATis_auW8hsdm0Q.prototype = new kgJWtah_alDaJsZAeVC_bgaA();
  type$__bJQ43mBkATis_auW8hsdm0Q.constructor = __bJQ43mBkATis_auW8hsdm0Q;
  type$__bJQ43mBkATis_auW8hsdm0Q.IsExtensionMethod = false;
  type$__bJQ43mBkATis_auW8hsdm0Q.AsExtensionMethod = function ()
  {
    this.IsExtensionMethod = true;
    return this;
  };
  type$__bJQ43mBkATis_auW8hsdm0Q.DwYABmBkATis_auW8hsdm0Q = type$kgJWtah_alDaJsZAeVC_bgaA.hgsABqh_alDaJsZAeVC_bgaA;
  var ctor$DwYABmBkATis_auW8hsdm0Q = $ctor$(basector$kgJWtah_alDaJsZAeVC_bgaA, 'DwYABmBkATis_auW8hsdm0Q', type$__bJQ43mBkATis_auW8hsdm0Q);
  type$__bJQ43mBkATis_auW8hsdm0Q.Invoke = function ()
  {
    for (var _i = 0; _i < this.list.length; _i++)
    {
      var _f = this.list[_i];
      var _arguments = Array.prototype.slice.call(arguments).slice(0);
      if(_f.IsExtensionMethod) _arguments.splice(0, 0, _f.Target);
      var _target = _f.IsExtensionMethod ? window : _f.Target;
      _target[_f.Method].apply(_target, _arguments);
    }
  };

  // delegate: (t) => Void
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Action`1
  function skdzdrHL2Te9uAFMbXAqvw(){};
  skdzdrHL2Te9uAFMbXAqvw.TypeName = "Action_1";
  skdzdrHL2Te9uAFMbXAqvw.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$skdzdrHL2Te9uAFMbXAqvw = skdzdrHL2Te9uAFMbXAqvw.prototype = new kgJWtah_alDaJsZAeVC_bgaA();
  type$skdzdrHL2Te9uAFMbXAqvw.constructor = skdzdrHL2Te9uAFMbXAqvw;
  type$skdzdrHL2Te9uAFMbXAqvw.IsExtensionMethod = false;
  type$skdzdrHL2Te9uAFMbXAqvw.AsExtensionMethod = function ()
  {
    this.IsExtensionMethod = true;
    return this;
  };
  type$skdzdrHL2Te9uAFMbXAqvw.CwYABrHL2Te9uAFMbXAqvw = type$kgJWtah_alDaJsZAeVC_bgaA.hgsABqh_alDaJsZAeVC_bgaA;
  var ctor$CwYABrHL2Te9uAFMbXAqvw = $ctor$(basector$kgJWtah_alDaJsZAeVC_bgaA, 'CwYABrHL2Te9uAFMbXAqvw', type$skdzdrHL2Te9uAFMbXAqvw);
  type$skdzdrHL2Te9uAFMbXAqvw.Invoke = function (b)
  {
    for (var _i = 0; _i < this.list.length; _i++)
    {
      var _f = this.list[_i];
      var _arguments = Array.prototype.slice.call(arguments).slice(0);
      if(_f.IsExtensionMethod) _arguments.splice(0, 0, _f.Target);
      var _target = _f.IsExtensionMethod ? window : _f.Target;
      _target[_f.Method].apply(_target, _arguments);
    }
  };

  // delegate: (x, y) => Int32
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Comparison`1
  function m907BpPfLjS_aPoO7xO4TjA(){};
  m907BpPfLjS_aPoO7xO4TjA.TypeName = "Comparison_1";
  m907BpPfLjS_aPoO7xO4TjA.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$m907BpPfLjS_aPoO7xO4TjA = m907BpPfLjS_aPoO7xO4TjA.prototype = new kgJWtah_alDaJsZAeVC_bgaA();
  type$m907BpPfLjS_aPoO7xO4TjA.constructor = m907BpPfLjS_aPoO7xO4TjA;
  type$m907BpPfLjS_aPoO7xO4TjA.IsExtensionMethod = false;
  type$m907BpPfLjS_aPoO7xO4TjA.AsExtensionMethod = function ()
  {
    this.IsExtensionMethod = true;
    return this;
  };
  type$m907BpPfLjS_aPoO7xO4TjA.jBAABpPfLjS_aPoO7xO4TjA = type$kgJWtah_alDaJsZAeVC_bgaA.hgsABqh_alDaJsZAeVC_bgaA;
  var ctor$jBAABpPfLjS_aPoO7xO4TjA = $ctor$(basector$kgJWtah_alDaJsZAeVC_bgaA, 'jBAABpPfLjS_aPoO7xO4TjA', type$m907BpPfLjS_aPoO7xO4TjA);
  type$m907BpPfLjS_aPoO7xO4TjA.Invoke = function (b, c)
  {
    var _ = void(0);
    for (var _i = 0; _i < this.list.length; _i++)
    {
      var _f = this.list[_i];
      var _arguments = Array.prototype.slice.call(arguments).slice(0);
      if(_f.IsExtensionMethod) _arguments.splice(0, 0, _f.Target);
      var _target = _f.IsExtensionMethod ? window : _f.Target;
      _ = _target[_f.Method].apply(_target, _arguments);
    }
    return _;
  };

  // delegate: (e) => Boolean
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Predicate`1
  function yRvOKinIHzuOQ9HytTgcwQ(){};
  yRvOKinIHzuOQ9HytTgcwQ.TypeName = "Predicate_1";
  yRvOKinIHzuOQ9HytTgcwQ.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$yRvOKinIHzuOQ9HytTgcwQ = yRvOKinIHzuOQ9HytTgcwQ.prototype = new kgJWtah_alDaJsZAeVC_bgaA();
  type$yRvOKinIHzuOQ9HytTgcwQ.constructor = yRvOKinIHzuOQ9HytTgcwQ;
  type$yRvOKinIHzuOQ9HytTgcwQ.IsExtensionMethod = false;
  type$yRvOKinIHzuOQ9HytTgcwQ.AsExtensionMethod = function ()
  {
    this.IsExtensionMethod = true;
    return this;
  };
  type$yRvOKinIHzuOQ9HytTgcwQ.hRAABinIHzuOQ9HytTgcwQ = type$kgJWtah_alDaJsZAeVC_bgaA.hgsABqh_alDaJsZAeVC_bgaA;
  var ctor$hRAABinIHzuOQ9HytTgcwQ = $ctor$(basector$kgJWtah_alDaJsZAeVC_bgaA, 'hRAABinIHzuOQ9HytTgcwQ', type$yRvOKinIHzuOQ9HytTgcwQ);
  type$yRvOKinIHzuOQ9HytTgcwQ.Invoke = function (b)
  {
    var _ = void(0);
    for (var _i = 0; _i < this.list.length; _i++)
    {
      var _f = this.list[_i];
      var _arguments = Array.prototype.slice.call(arguments).slice(0);
      if(_f.IsExtensionMethod) _arguments.splice(0, 0, _f.Target);
      var _target = _f.IsExtensionMethod ? window : _f.Target;
      _ = _target[_f.Method].apply(_target, _arguments);
    }
    return _;
  };

  // ScriptCoreLib.JavaScript.Extensions.Extensions.AttachToDocument
  function cwsABt0jLD6yDQ0X6wt5_aw(b)
  {
    var c;

    c = dAsABt0jLD6yDQ0X6wt5_aw(b, document.body);
    return c;
  };

  // ScriptCoreLib.JavaScript.Extensions.Extensions.Show
  function bgsABt0jLD6yDQ0X6wt5_aw(b)
  {
    var c;

    b.style.display = '';
    UA0ABoNSrD_aFizz5n6sJfw(b.style, 1);
    c = b;
    return c;
  };

  // ScriptCoreLib.JavaScript.Extensions.Extensions.Show
  function bwsABt0jLD6yDQ0X6wt5_aw(b, c)
  {
    var d, e;

    e = !c;

    if (!e)
    {
      d = bgsABt0jLD6yDQ0X6wt5_aw(b);
      return d;
    }

    d = cAsABt0jLD6yDQ0X6wt5_aw(b);
    return d;
  };

  // ScriptCoreLib.JavaScript.Extensions.Extensions.Hide
  function cAsABt0jLD6yDQ0X6wt5_aw(b)
  {
    var c;

    b.style.display = 'none';
    c = b;
    return c;
  };

  // ScriptCoreLib.JavaScript.Extensions.Extensions.ToggleVisible
  function cQsABt0jLD6yDQ0X6wt5_aw(b)
  {
    var c, d, e;

    c = '';
    e = !(b.style.display == c);

    if (!e)
    {
      cAsABt0jLD6yDQ0X6wt5_aw(b);
      d = 0;
      return d;
    }

    bgsABt0jLD6yDQ0X6wt5_aw(b);
    d = 1;
    return d;
  };

  // ScriptCoreLib.JavaScript.Extensions.Extensions.Dispose
  function cgsABt0jLD6yDQ0X6wt5_aw(b)
  {
    var c, d, e;

    e = !(b == null);

    if (!e)
    {
      throw /* DOMCreateType */new Ldtxf6AerjyAhyYJX1IV5g();
    }

    c = b.parentNode;
    e = (c == null);

    if (!e)
    {
      c.removeChild(b);
    }

    d = b;
    return d;
  };

  // ScriptCoreLib.JavaScript.Extensions.Extensions.AttachTo
  function dAsABt0jLD6yDQ0X6wt5_aw(b, c)
  {
    var d;

    c.appendChild(b);
    d = b;
    return d;
  };

  // ScriptCoreLib.JavaScript.Extensions.Extensions.Deserialize
  function dQsABt0jLD6yDQ0X6wt5_aw(b, c)
  {
    var d, e;

    e = !(c == null);

    if (!e)
    {
      throw lwAABq9OGjCe3bHElJJ0LA('Deserialize: k is null');
    }

    d = new ctor$BggABh3T7DmrLa5jZ1qOVA(c).CggABh3T7DmrLa5jZ1qOVA(b);
    return d;
  };

  // ScriptCoreLib.JavaScript.Extensions.Extensions.Spawn
  function dgsABt0jLD6yDQ0X6wt5_aw(b)
  {
    var c;

    c = /* DOMCreateType */new jFOh6mqSAjClHaxCM7KWRQ();
    c.alias = b;
    ggcABm1g3jqH_atPTeraONg(c.alias.oQMABhv6ETyTub5rOKSvRA(), new ctor$KQsABoVjnjetV_aXG4GFkBQ(c, '_Spawn_b__0'));
  };

  // ScriptCoreLib.JavaScript.Extensions.Extensions.SpawnTo
  function dwsABt0jLD6yDQ0X6wt5_aw(b, c)
  {
    var d;

    d = /* DOMCreateType */new rqen1WoNYTG_bP1KupTgpsw();
    d.h = c;
    ggcABm1g3jqH_atPTeraONg(b.oQMABhv6ETyTub5rOKSvRA(), new ctor$KQsABoVjnjetV_aXG4GFkBQ(d, '_SpawnTo_b__3'));
  };

  // ScriptCoreLib.JavaScript.Extensions.Extensions.SpawnTo
  function eAsABt0jLD6yDQ0X6wt5_aw(b, c, d)
  {
    var e;

    e = /* DOMCreateType */new DSCszieMWziaVWaR7SimNg();
    e.h = d;
    eQsABt0jLD6yDQ0X6wt5_aw(b, c, new ctor$EwYABicseTyEC3C_acJNezQ(e, '_SpawnTo_b__6'));
  };

  // ScriptCoreLib.JavaScript.Extensions.Extensions.SpawnTo
  function eQsABt0jLD6yDQ0X6wt5_aw(b, c, d)
  {
    var e, f;

    e = /* DOMCreateType */new llDStxfsVjK2NM94Dy3f_bw();
    e.KnownTypes = c;
    e.h = d;
    f = !(e.KnownTypes == null);

    if (!f)
    {
      throw lwAABq9OGjCe3bHElJJ0LA('GetKnownTypes is null');
    }

    ggcABm1g3jqH_atPTeraONg(b.oQMABhv6ETyTub5rOKSvRA(), new ctor$KQsABoVjnjetV_aXG4GFkBQ(e, '_SpawnTo_b__9'));
  };

  // ScriptCoreLib.Shared.Pair`1
  function p1D1pilS0Dabl5_a552x63g(){};
  p1D1pilS0Dabl5_a552x63g.TypeName = "Pair_1";
  p1D1pilS0Dabl5_a552x63g.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$p1D1pilS0Dabl5_a552x63g = p1D1pilS0Dabl5_a552x63g.prototype;
  type$p1D1pilS0Dabl5_a552x63g.constructor = p1D1pilS0Dabl5_a552x63g;
  type$p1D1pilS0Dabl5_a552x63g.A = null;
  type$p1D1pilS0Dabl5_a552x63g.B = null;
  var basector$p1D1pilS0Dabl5_a552x63g = $ctor$(null, null, type$p1D1pilS0Dabl5_a552x63g);
  // ScriptCoreLib.Shared.Pair`1..ctor
  type$p1D1pilS0Dabl5_a552x63g.WQsABilS0Dabl5_a552x63g = function ()
  {
    var a = this;

  };
  var ctor$WQsABilS0Dabl5_a552x63g = p1D1pilS0Dabl5_a552x63g.ctor = $ctor$(null, 'WQsABilS0Dabl5_a552x63g', type$p1D1pilS0Dabl5_a552x63g);

  // ScriptCoreLib.Shared.Pair`2
  function QdvYmrjuJTGprs5OFzkcPA(){};
  QdvYmrjuJTGprs5OFzkcPA.TypeName = "Pair_2";
  QdvYmrjuJTGprs5OFzkcPA.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$QdvYmrjuJTGprs5OFzkcPA = QdvYmrjuJTGprs5OFzkcPA.prototype;
  type$QdvYmrjuJTGprs5OFzkcPA.constructor = QdvYmrjuJTGprs5OFzkcPA;
  type$QdvYmrjuJTGprs5OFzkcPA.A = null;
  type$QdvYmrjuJTGprs5OFzkcPA.B = null;
  var basector$QdvYmrjuJTGprs5OFzkcPA = $ctor$(null, null, type$QdvYmrjuJTGprs5OFzkcPA);
  // ScriptCoreLib.Shared.Pair`2..ctor
  type$QdvYmrjuJTGprs5OFzkcPA.WAsABrjuJTGprs5OFzkcPA = function (b, c)
  {
    var a = this;

    a.A = b;
    a.B = c;
  };
  var ctor$WAsABrjuJTGprs5OFzkcPA = $ctor$(null, 'WAsABrjuJTGprs5OFzkcPA', type$QdvYmrjuJTGprs5OFzkcPA);

  // ScriptCoreLib.Shared.JSONBase
  function cXhOvPhugDmzVMjtbtSocg(){};
  cXhOvPhugDmzVMjtbtSocg.TypeName = "JSONBase";
  cXhOvPhugDmzVMjtbtSocg.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$cXhOvPhugDmzVMjtbtSocg = cXhOvPhugDmzVMjtbtSocg.prototype;
  type$cXhOvPhugDmzVMjtbtSocg.constructor = cXhOvPhugDmzVMjtbtSocg;
  var basector$cXhOvPhugDmzVMjtbtSocg = $ctor$(null, null, type$cXhOvPhugDmzVMjtbtSocg);
  // ScriptCoreLib.Shared.JSONBase..ctor
  type$cXhOvPhugDmzVMjtbtSocg.VwsABvhugDmzVMjtbtSocg = function ()
  {
    var a = this;

  };
  var ctor$VwsABvhugDmzVMjtbtSocg = cXhOvPhugDmzVMjtbtSocg.ctor = $ctor$(null, 'VwsABvhugDmzVMjtbtSocg', type$cXhOvPhugDmzVMjtbtSocg);

  // ScriptCoreLib.Shared.MyTransportDescriptor`1
  function K72XfZMh5j_aGre4JjPD2dQ(){};
  K72XfZMh5j_aGre4JjPD2dQ.TypeName = "MyTransportDescriptor_1";
  K72XfZMh5j_aGre4JjPD2dQ.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$K72XfZMh5j_aGre4JjPD2dQ = K72XfZMh5j_aGre4JjPD2dQ.prototype = new cXhOvPhugDmzVMjtbtSocg();
  type$K72XfZMh5j_aGre4JjPD2dQ.constructor = K72XfZMh5j_aGre4JjPD2dQ;
  type$K72XfZMh5j_aGre4JjPD2dQ.Callback = null;
  type$K72XfZMh5j_aGre4JjPD2dQ.Description = null;
  type$K72XfZMh5j_aGre4JjPD2dQ.Data = null;
  type$K72XfZMh5j_aGre4JjPD2dQ.$0 = {};
  type$K72XfZMh5j_aGre4JjPD2dQ.$0.$0 = 'MyTransportDescriptor`1';
  type$K72XfZMh5j_aGre4JjPD2dQ.$0.$1 = 'WgsABpMh5j_aGre4JjPD2dQ';

  var basector$K72XfZMh5j_aGre4JjPD2dQ = $ctor$(basector$cXhOvPhugDmzVMjtbtSocg, null, type$K72XfZMh5j_aGre4JjPD2dQ);
  // ScriptCoreLib.Shared.MyTransportDescriptor`1..ctor
  type$K72XfZMh5j_aGre4JjPD2dQ.WgsABpMh5j_aGre4JjPD2dQ = function ()
  {
    var a = this;

    a.VwsABvhugDmzVMjtbtSocg();
  };
  var ctor$WgsABpMh5j_aGre4JjPD2dQ = K72XfZMh5j_aGre4JjPD2dQ.ctor = $ctor$(basector$cXhOvPhugDmzVMjtbtSocg, 'WgsABpMh5j_aGre4JjPD2dQ', type$K72XfZMh5j_aGre4JjPD2dQ);

  // ScriptCoreLib.Shared.Predicate
  function F8DHswF8xDasrw7uIe6gQA(){};
  F8DHswF8xDasrw7uIe6gQA.TypeName = "Predicate";
  F8DHswF8xDasrw7uIe6gQA.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$F8DHswF8xDasrw7uIe6gQA = F8DHswF8xDasrw7uIe6gQA.prototype;
  type$F8DHswF8xDasrw7uIe6gQA.constructor = F8DHswF8xDasrw7uIe6gQA;
  type$F8DHswF8xDasrw7uIe6gQA.Value = false;
  var basector$F8DHswF8xDasrw7uIe6gQA = $ctor$(null, null, type$F8DHswF8xDasrw7uIe6gQA);
  // ScriptCoreLib.Shared.Predicate..ctor
  type$F8DHswF8xDasrw7uIe6gQA.SwsABgF8xDasrw7uIe6gQA = function ()
  {
    var a = this;

  };
  var ctor$SwsABgF8xDasrw7uIe6gQA = F8DHswF8xDasrw7uIe6gQA.ctor = $ctor$(null, 'SwsABgF8xDasrw7uIe6gQA', type$F8DHswF8xDasrw7uIe6gQA);

  // ScriptCoreLib.Shared.Predicate.Invoke
  type$F8DHswF8xDasrw7uIe6gQA.RQsABgF8xDasrw7uIe6gQA = function (b)
  {
    var a = this;

    sQgABrDT9zCTidQoMa3Dig(b, a);
  };

  // ScriptCoreLib.Shared.Predicate.Is
  function RgsABgF8xDasrw7uIe6gQA(b)
  {
    var c;

    c = RwsABgF8xDasrw7uIe6gQA(b, 0);
    return c;
  };

  // ScriptCoreLib.Shared.Predicate.Is
  function RwsABgF8xDasrw7uIe6gQA(b, c)
  {
    var d, e;

    d = new ctor$SwsABgF8xDasrw7uIe6gQA();
    d.Value = c;
    d.RQsABgF8xDasrw7uIe6gQA(b);
    e = d.Value;
    return e;
  };

  // ScriptCoreLib.Shared.Predicate.Invoke
  function SAsABgF8xDasrw7uIe6gQA(b, c)
  {
    var d, e;

    d = new ctor$VgsABqxcBjuvbuvUgRNVDw();
    d.Target = b;
    d.VAsABqxcBjuvbuvUgRNVDw(c);
    e = d.Value;
    return e;
  };

  // ScriptCoreLib.Shared.Predicate.Invoke
  function SQsABgF8xDasrw7uIe6gQA(b, c, d)
  {
    var e, f;

    e = TQsABiPSpzKAec_a9tCWuMA(b, c);
    e.TgsABiPSpzKAec_a9tCWuMA(d);
    f = e.Value;
    return f;
  };

  // ScriptCoreLib.Shared.Predicate.op_Implicit
  function SgsABgF8xDasrw7uIe6gQA(b)
  {
    var c;

    c = b.Value;
    return c;
  };

  // ScriptCoreLib.Shared.Predicate`1
  function gtVjaKxcBjuvbuvUgRNVDw(){};
  gtVjaKxcBjuvbuvUgRNVDw.TypeName = "Predicate_1";
  gtVjaKxcBjuvbuvUgRNVDw.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$gtVjaKxcBjuvbuvUgRNVDw = gtVjaKxcBjuvbuvUgRNVDw.prototype = new F8DHswF8xDasrw7uIe6gQA();
  type$gtVjaKxcBjuvbuvUgRNVDw.constructor = gtVjaKxcBjuvbuvUgRNVDw;
  type$gtVjaKxcBjuvbuvUgRNVDw.Target = null;
  var basector$gtVjaKxcBjuvbuvUgRNVDw = $ctor$(basector$F8DHswF8xDasrw7uIe6gQA, null, type$gtVjaKxcBjuvbuvUgRNVDw);
  // ScriptCoreLib.Shared.Predicate`1..ctor
  type$gtVjaKxcBjuvbuvUgRNVDw.VgsABqxcBjuvbuvUgRNVDw = function ()
  {
    var a = this;

    a.SwsABgF8xDasrw7uIe6gQA();
  };
  var ctor$VgsABqxcBjuvbuvUgRNVDw = gtVjaKxcBjuvbuvUgRNVDw.ctor = $ctor$(basector$F8DHswF8xDasrw7uIe6gQA, 'VgsABqxcBjuvbuvUgRNVDw', type$gtVjaKxcBjuvbuvUgRNVDw);

  // ScriptCoreLib.Shared.Predicate`1.Invoke
  type$gtVjaKxcBjuvbuvUgRNVDw.VAsABqxcBjuvbuvUgRNVDw = function (b)
  {
    var a = this;

    sQgABrDT9zCTidQoMa3Dig(b, a);
  };

  // ScriptCoreLib.Shared.Predicate`1.op_Implicit
  function VQsABqxcBjuvbuvUgRNVDw(b)
  {
    var c;

    c = b.Target;
    return c;
  };

  // ScriptCoreLib.Shared.Predicate`2
  function __bE3_bDCPSpzKAec_a9tCWuMA(){};
  __bE3_bDCPSpzKAec_a9tCWuMA.TypeName = "Predicate_2";
  __bE3_bDCPSpzKAec_a9tCWuMA.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$__bE3_bDCPSpzKAec_a9tCWuMA = __bE3_bDCPSpzKAec_a9tCWuMA.prototype = new F8DHswF8xDasrw7uIe6gQA();
  type$__bE3_bDCPSpzKAec_a9tCWuMA.constructor = __bE3_bDCPSpzKAec_a9tCWuMA;
  type$__bE3_bDCPSpzKAec_a9tCWuMA.TargetIn = null;
  type$__bE3_bDCPSpzKAec_a9tCWuMA.TargetOut = null;
  var basector$__bE3_bDCPSpzKAec_a9tCWuMA = $ctor$(basector$F8DHswF8xDasrw7uIe6gQA, null, type$__bE3_bDCPSpzKAec_a9tCWuMA);
  // ScriptCoreLib.Shared.Predicate`2..ctor
  type$__bE3_bDCPSpzKAec_a9tCWuMA.TwsABiPSpzKAec_a9tCWuMA = function ()
  {
    var a = this;

    a.SwsABgF8xDasrw7uIe6gQA();
  };
  var ctor$TwsABiPSpzKAec_a9tCWuMA = __bE3_bDCPSpzKAec_a9tCWuMA.ctor = $ctor$(basector$F8DHswF8xDasrw7uIe6gQA, 'TwsABiPSpzKAec_a9tCWuMA', type$__bE3_bDCPSpzKAec_a9tCWuMA);

  // ScriptCoreLib.Shared.Predicate`2.Invoke
  function TAsABiPSpzKAec_a9tCWuMA(b, c, d)
  {
    var e, f;

    e = TQsABiPSpzKAec_a9tCWuMA(b, c);
    e.TgsABiPSpzKAec_a9tCWuMA(d);
    f = e.Value;
    return f;
  };

  // ScriptCoreLib.Shared.Predicate`2.Of
  function TQsABiPSpzKAec_a9tCWuMA(b, c)
  {
    var d, e;

    d = new ctor$TwsABiPSpzKAec_a9tCWuMA();
    d.TargetIn = b;
    d.TargetOut = c;
    e = d;
    return e;
  };

  // ScriptCoreLib.Shared.Predicate`2.Invoke
  type$__bE3_bDCPSpzKAec_a9tCWuMA.TgsABiPSpzKAec_a9tCWuMA = function (b)
  {
    var a = this;

    sQgABrDT9zCTidQoMa3Dig(b, a);
  };

  // ScriptCoreLib.Shared.ConvertTo`2
  function PuN33GmiwjiGbtK8fLrm_bw(){};
  PuN33GmiwjiGbtK8fLrm_bw.TypeName = "ConvertTo_2";
  PuN33GmiwjiGbtK8fLrm_bw.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$PuN33GmiwjiGbtK8fLrm_bw = PuN33GmiwjiGbtK8fLrm_bw.prototype = new __bE3_bDCPSpzKAec_a9tCWuMA();
  type$PuN33GmiwjiGbtK8fLrm_bw.constructor = PuN33GmiwjiGbtK8fLrm_bw;
  type$PuN33GmiwjiGbtK8fLrm_bw.TargetInComparer = null;
  var basector$PuN33GmiwjiGbtK8fLrm_bw = $ctor$(basector$__bE3_bDCPSpzKAec_a9tCWuMA, null, type$PuN33GmiwjiGbtK8fLrm_bw);
  // ScriptCoreLib.Shared.ConvertTo`2..ctor
  type$PuN33GmiwjiGbtK8fLrm_bw.UwsABmmiwjiGbtK8fLrm_bw = function ()
  {
    var a = this;

    a.TwsABiPSpzKAec_a9tCWuMA();
  };
  var ctor$UwsABmmiwjiGbtK8fLrm_bw = PuN33GmiwjiGbtK8fLrm_bw.ctor = $ctor$(basector$__bE3_bDCPSpzKAec_a9tCWuMA, 'UwsABmmiwjiGbtK8fLrm_bw', type$PuN33GmiwjiGbtK8fLrm_bw);

  // ScriptCoreLib.Shared.ConvertTo`2.set_Item
  type$PuN33GmiwjiGbtK8fLrm_bw.UAsABmmiwjiGbtK8fLrm_bw = function (b, c)
  {
    var a = this, d;

    d = !SQsABgF8xDasrw7uIe6gQA(a.TargetIn, b, a.TargetInComparer);

    if (!d)
    {
      a.TargetOut = c;
      a.Value = 1;
    }

  };

  // ScriptCoreLib.Shared.ConvertTo`2.Invoke
  type$PuN33GmiwjiGbtK8fLrm_bw.UQsABmmiwjiGbtK8fLrm_bw = function (b)
  {
    var a = this;

    sQgABrDT9zCTidQoMa3Dig(b, a);
  };

  // ScriptCoreLib.Shared.ConvertTo`2.Convert
  function UgsABmmiwjiGbtK8fLrm_bw(b, c)
  {
    var d, e;

    d = new ctor$UwsABmmiwjiGbtK8fLrm_bw();
    d.TargetIn = b;
    d.UQsABmmiwjiGbtK8fLrm_bw(c);
    e = d.TargetOut;
    return e;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection.__AssemblyName
  function _9YB69m_aNFTqBCXCdcvj_azQ(){};
  _9YB69m_aNFTqBCXCdcvj_azQ.TypeName = "AssemblyName";
  _9YB69m_aNFTqBCXCdcvj_azQ.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$_9YB69m_aNFTqBCXCdcvj_azQ = _9YB69m_aNFTqBCXCdcvj_azQ.prototype;
  type$_9YB69m_aNFTqBCXCdcvj_azQ.constructor = _9YB69m_aNFTqBCXCdcvj_azQ;
  type$_9YB69m_aNFTqBCXCdcvj_azQ.__Value = null;
  type$_9YB69m_aNFTqBCXCdcvj_azQ.__NameValue = null;
  var basector$_9YB69m_aNFTqBCXCdcvj_azQ = $ctor$(null, null, type$_9YB69m_aNFTqBCXCdcvj_azQ);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection.__AssemblyName..ctor
  type$_9YB69m_aNFTqBCXCdcvj_azQ._2AoABm_aNFTqBCXCdcvj_azQ = function ()
  {
    var a = this;

  };
  var ctor$_2AoABm_aNFTqBCXCdcvj_azQ = _9YB69m_aNFTqBCXCdcvj_azQ.ctor = $ctor$(null, '_2AoABm_aNFTqBCXCdcvj_azQ', type$_9YB69m_aNFTqBCXCdcvj_azQ);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection.__AssemblyName.get_Name
  type$_9YB69m_aNFTqBCXCdcvj_azQ.get_Name = function ()
  {
    var a = this, b;

    b = a.__NameValue.Name;
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection.__AssemblyName.get_FullName
  type$_9YB69m_aNFTqBCXCdcvj_azQ.get_FullName = function ()
  {
    var a = this, b;

    b = a.__NameValue.FullName;
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection.__AssemblyNameValue
  function _82itIHGRWzucLr1QYx_bscw(){};
  _82itIHGRWzucLr1QYx_bscw.TypeName = "__AssemblyNameValue";
  _82itIHGRWzucLr1QYx_bscw.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$_82itIHGRWzucLr1QYx_bscw = _82itIHGRWzucLr1QYx_bscw.prototype;
  type$_82itIHGRWzucLr1QYx_bscw.constructor = _82itIHGRWzucLr1QYx_bscw;
  type$_82itIHGRWzucLr1QYx_bscw.Name = null;
  type$_82itIHGRWzucLr1QYx_bscw.FullName = null;
  var basector$_82itIHGRWzucLr1QYx_bscw = $ctor$(null, null, type$_82itIHGRWzucLr1QYx_bscw);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection.__AssemblyNameValue..ctor
  type$_82itIHGRWzucLr1QYx_bscw._1QoABnGRWzucLr1QYx_bscw = function ()
  {
    var a = this;

  };
  var ctor$_1QoABnGRWzucLr1QYx_bscw = _82itIHGRWzucLr1QYx_bscw.ctor = $ctor$(null, '_1QoABnGRWzucLr1QYx_bscw', type$_82itIHGRWzucLr1QYx_bscw);

  // ScriptCoreLib.Shared.BCLImplementation.System.__Uri
  function nag6H_b96VjiMaEh0hnX_bhg(){};
  nag6H_b96VjiMaEh0hnX_bhg.TypeName = "Uri";
  nag6H_b96VjiMaEh0hnX_bhg.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$nag6H_b96VjiMaEh0hnX_bhg = nag6H_b96VjiMaEh0hnX_bhg.prototype;
  type$nag6H_b96VjiMaEh0hnX_bhg.constructor = nag6H_b96VjiMaEh0hnX_bhg;
  type$nag6H_b96VjiMaEh0hnX_bhg._OriginalString_k__BackingField = null;
  type$nag6H_b96VjiMaEh0hnX_bhg._Scheme_k__BackingField = null;
  type$nag6H_b96VjiMaEh0hnX_bhg._PathAndQuery_k__BackingField = null;
  type$nag6H_b96VjiMaEh0hnX_bhg._Host_k__BackingField = null;
  type$nag6H_b96VjiMaEh0hnX_bhg._Query_k__BackingField = null;
  type$nag6H_b96VjiMaEh0hnX_bhg._AbsolutePath_k__BackingField = null;
  type$nag6H_b96VjiMaEh0hnX_bhg._Segments_k__BackingField = null;
  var basector$nag6H_b96VjiMaEh0hnX_bhg = $ctor$(null, null, type$nag6H_b96VjiMaEh0hnX_bhg);
  // ScriptCoreLib.Shared.BCLImplementation.System.__Uri..ctor
  type$nag6H_b96VjiMaEh0hnX_bhg.VgoABv96VjiMaEh0hnX_bhg = function (b)
  {
    var a = this, c, d, e, f, g, h, i;

    a.SQoABv96VjiMaEh0hnX_bhg(b);
    c = dhMABjDeCj_aRJzaBmU9SJg(b, ':\u002f\u002f');
    a.SwoABv96VjiMaEh0hnX_bhg(khMABjDeCj_aRJzaBmU9SJg(b, 0, c));
    d = dxMABjDeCj_aRJzaBmU9SJg(b, '\u002f', (c + eBMABjDeCj_aRJzaBmU9SJg(':\u002f\u002f')));
    a.TwoABv96VjiMaEh0hnX_bhg(khMABjDeCj_aRJzaBmU9SJg(b, (c + eBMABjDeCj_aRJzaBmU9SJg(':\u002f\u002f')), (d - (c + eBMABjDeCj_aRJzaBmU9SJg(':\u002f\u002f')))));
    a.TQoABv96VjiMaEh0hnX_bhg(kRMABjDeCj_aRJzaBmU9SJg(b, d));
    e = dhMABjDeCj_aRJzaBmU9SJg(a.TAoABv96VjiMaEh0hnX_bhg(), '?');
    i = !(e < 0);

    if (!i)
    {
      a.UQoABv96VjiMaEh0hnX_bhg('');
      a.UwoABv96VjiMaEh0hnX_bhg(a.TAoABv96VjiMaEh0hnX_bhg());
    }
    else
    {
      a.UQoABv96VjiMaEh0hnX_bhg(kRMABjDeCj_aRJzaBmU9SJg(a.TAoABv96VjiMaEh0hnX_bhg(), (e + 1)));
      a.UwoABv96VjiMaEh0hnX_bhg(khMABjDeCj_aRJzaBmU9SJg(a.TAoABv96VjiMaEh0hnX_bhg(), 0, e));
    }

    f = new ctor$swAABnGXyTaWJhb6CcyWQQ();
    g = 0;
    h = dhMABjDeCj_aRJzaBmU9SJg(a.UgoABv96VjiMaEh0hnX_bhg(), '\u002f');
    while (!(g < 0))
    {
      h = dxMABjDeCj_aRJzaBmU9SJg(a.UgoABv96VjiMaEh0hnX_bhg(), '\u002f', g);
      i = (h < 0);

      if (!i)
      {
        f.vQAABnGXyTaWJhb6CcyWQQ(khMABjDeCj_aRJzaBmU9SJg(a.UgoABv96VjiMaEh0hnX_bhg(), g, ((h - g) + 1)));
        g = (h + 1);
      }
      else
      {
        i = !(g < (eBMABjDeCj_aRJzaBmU9SJg(a.UgoABv96VjiMaEh0hnX_bhg()) - 1));

        if (!i)
        {
          f.vQAABnGXyTaWJhb6CcyWQQ(kRMABjDeCj_aRJzaBmU9SJg(a.UgoABv96VjiMaEh0hnX_bhg(), g));
        }

        g = -1;
      }

    }
    a.VQoABv96VjiMaEh0hnX_bhg(f.tgAABnGXyTaWJhb6CcyWQQ());
  };
  var ctor$VgoABv96VjiMaEh0hnX_bhg = $ctor$(null, 'VgoABv96VjiMaEh0hnX_bhg', type$nag6H_b96VjiMaEh0hnX_bhg);

  // ScriptCoreLib.Shared.BCLImplementation.System.__Uri.get_OriginalString
  type$nag6H_b96VjiMaEh0hnX_bhg.SAoABv96VjiMaEh0hnX_bhg = function ()
  {
    return this._OriginalString_k__BackingField;
  };

  // ScriptCoreLib.Shared.BCLImplementation.System.__Uri.set_OriginalString
  type$nag6H_b96VjiMaEh0hnX_bhg.SQoABv96VjiMaEh0hnX_bhg = function (b)
  {
    var a = this;

    a._OriginalString_k__BackingField = b;
  };

  // ScriptCoreLib.Shared.BCLImplementation.System.__Uri.get_Scheme
  type$nag6H_b96VjiMaEh0hnX_bhg.SgoABv96VjiMaEh0hnX_bhg = function ()
  {
    return this._Scheme_k__BackingField;
  };

  // ScriptCoreLib.Shared.BCLImplementation.System.__Uri.set_Scheme
  type$nag6H_b96VjiMaEh0hnX_bhg.SwoABv96VjiMaEh0hnX_bhg = function (b)
  {
    var a = this;

    a._Scheme_k__BackingField = b;
  };

  // ScriptCoreLib.Shared.BCLImplementation.System.__Uri.get_PathAndQuery
  type$nag6H_b96VjiMaEh0hnX_bhg.TAoABv96VjiMaEh0hnX_bhg = function ()
  {
    return this._PathAndQuery_k__BackingField;
  };

  // ScriptCoreLib.Shared.BCLImplementation.System.__Uri.set_PathAndQuery
  type$nag6H_b96VjiMaEh0hnX_bhg.TQoABv96VjiMaEh0hnX_bhg = function (b)
  {
    var a = this;

    a._PathAndQuery_k__BackingField = b;
  };

  // ScriptCoreLib.Shared.BCLImplementation.System.__Uri.get_Host
  type$nag6H_b96VjiMaEh0hnX_bhg.TgoABv96VjiMaEh0hnX_bhg = function ()
  {
    return this._Host_k__BackingField;
  };

  // ScriptCoreLib.Shared.BCLImplementation.System.__Uri.set_Host
  type$nag6H_b96VjiMaEh0hnX_bhg.TwoABv96VjiMaEh0hnX_bhg = function (b)
  {
    var a = this;

    a._Host_k__BackingField = b;
  };

  // ScriptCoreLib.Shared.BCLImplementation.System.__Uri.get_Query
  type$nag6H_b96VjiMaEh0hnX_bhg.UAoABv96VjiMaEh0hnX_bhg = function ()
  {
    return this._Query_k__BackingField;
  };

  // ScriptCoreLib.Shared.BCLImplementation.System.__Uri.set_Query
  type$nag6H_b96VjiMaEh0hnX_bhg.UQoABv96VjiMaEh0hnX_bhg = function (b)
  {
    var a = this;

    a._Query_k__BackingField = b;
  };

  // ScriptCoreLib.Shared.BCLImplementation.System.__Uri.get_AbsolutePath
  type$nag6H_b96VjiMaEh0hnX_bhg.UgoABv96VjiMaEh0hnX_bhg = function ()
  {
    return this._AbsolutePath_k__BackingField;
  };

  // ScriptCoreLib.Shared.BCLImplementation.System.__Uri.set_AbsolutePath
  type$nag6H_b96VjiMaEh0hnX_bhg.UwoABv96VjiMaEh0hnX_bhg = function (b)
  {
    var a = this;

    a._AbsolutePath_k__BackingField = b;
  };

  // ScriptCoreLib.Shared.BCLImplementation.System.__Uri.get_Segments
  type$nag6H_b96VjiMaEh0hnX_bhg.VAoABv96VjiMaEh0hnX_bhg = function ()
  {
    return this._Segments_k__BackingField;
  };

  // ScriptCoreLib.Shared.BCLImplementation.System.__Uri.set_Segments
  type$nag6H_b96VjiMaEh0hnX_bhg.VQoABv96VjiMaEh0hnX_bhg = function (b)
  {
    var a = this;

    a._Segments_k__BackingField = b;
  };

  // ScriptCoreLib.Shared.BCLImplementation.System.__Uri.op_Inequality
  function VwoABv96VjiMaEh0hnX_bhg(b, c)
  {
    var d, e, f, g;

    d = b;
    e = c;
    g = !(d == null);

    if (!g)
    {
      f = !(d == e);
      return f;
    }

    g = !(e == null);

    if (!g)
    {
      f = !(d == e);
      return f;
    }

    f = lxMABjDeCj_aRJzaBmU9SJg(b.SAoABv96VjiMaEh0hnX_bhg(), c.SAoABv96VjiMaEh0hnX_bhg());
    return f;
  };

  // ScriptCoreLib.Shared.BCLImplementation.System.__Uri.op_Equality
  function WAoABv96VjiMaEh0hnX_bhg(b, c)
  {
    var d, e, f, g;

    d = b;
    e = c;
    g = !(d == null);

    if (!g)
    {
      f = (d == e);
      return f;
    }

    g = !(e == null);

    if (!g)
    {
      f = (d == e);
      return f;
    }

    f = lxMABjDeCj_aRJzaBmU9SJg(b.SAoABv96VjiMaEh0hnX_bhg(), c.SAoABv96VjiMaEh0hnX_bhg());
    return f;
  };

  // ScriptCoreLib.Shared.BCLImplementation.System.__Uri.ToString
  type$nag6H_b96VjiMaEh0hnX_bhg.toString /* ScriptCoreLib.Shared.BCLImplementation.System.__Uri.ToString */ = function ()
  {
    var a = this, b;

    b = a.SAoABv96VjiMaEh0hnX_bhg();
    return b;
  };
    nag6H_b96VjiMaEh0hnX_bhg.prototype.toString /* System.Object.ToString */ = nag6H_b96VjiMaEh0hnX_bhg.prototype.toString /* ScriptCoreLib.Shared.BCLImplementation.System.__Uri.ToString */;

  // ScriptCoreLib.Shared.TextWriter
  function Jw8JzpJOUTu084nfg8QmiA(){};
  Jw8JzpJOUTu084nfg8QmiA.TypeName = "TextWriter";
  Jw8JzpJOUTu084nfg8QmiA.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$Jw8JzpJOUTu084nfg8QmiA = Jw8JzpJOUTu084nfg8QmiA.prototype;
  type$Jw8JzpJOUTu084nfg8QmiA.constructor = Jw8JzpJOUTu084nfg8QmiA;
  type$Jw8JzpJOUTu084nfg8QmiA._text = null;
  var basector$Jw8JzpJOUTu084nfg8QmiA = $ctor$(null, null, type$Jw8JzpJOUTu084nfg8QmiA);
  // ScriptCoreLib.Shared.TextWriter..ctor
  type$Jw8JzpJOUTu084nfg8QmiA.RwoABpJOUTu084nfg8QmiA = function ()
  {
    var a = this;

    a._text = '';
  };
  var ctor$RwoABpJOUTu084nfg8QmiA = Jw8JzpJOUTu084nfg8QmiA.ctor = $ctor$(null, 'RwoABpJOUTu084nfg8QmiA', type$Jw8JzpJOUTu084nfg8QmiA);

  // ScriptCoreLib.Shared.TextWriter.get_Text
  type$Jw8JzpJOUTu084nfg8QmiA.QgoABpJOUTu084nfg8QmiA = function ()
  {
    var a = this, b;

    b = a._text;
    return b;
  };

  // ScriptCoreLib.Shared.TextWriter.set_Text
  type$Jw8JzpJOUTu084nfg8QmiA.QwoABpJOUTu084nfg8QmiA = function (b)
  {
    var a = this;

    a._text = b;
  };

  // ScriptCoreLib.Shared.TextWriter.Write
  type$Jw8JzpJOUTu084nfg8QmiA.RAoABpJOUTu084nfg8QmiA = function (b)
  {
    var a = this;

    a._text = gBMABjDeCj_aRJzaBmU9SJg(a._text, b);
  };

  // ScriptCoreLib.Shared.TextWriter.WriteLine
  type$Jw8JzpJOUTu084nfg8QmiA.RQoABpJOUTu084nfg8QmiA = function ()
  {
    var a = this;

    a.RgoABpJOUTu084nfg8QmiA('');
  };

  // ScriptCoreLib.Shared.TextWriter.WriteLine
  type$Jw8JzpJOUTu084nfg8QmiA.RgoABpJOUTu084nfg8QmiA = function (b)
  {
    var a = this;

    a.RAoABpJOUTu084nfg8QmiA(gBMABjDeCj_aRJzaBmU9SJg(b, '\u000a'));
  };

  // ScriptCoreLib.JavaScript.DOM.ILocation.get_IsHTTP
  function LQoABpkS6TmmN8B4d0MPsg(a)
  {
    var b;

    b = lxMABjDeCj_aRJzaBmU9SJg(a.protocol, 'http:');
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.ILocation.reload
  // ScriptCoreLib.JavaScript.DOM.ILocation.get_Item
  function LwoABpkS6TmmN8B4d0MPsg(a, b)
  {
    var c, d, e, f, g, h, i, j, k;

    c = null;
    d = LA4ABqPHMD_aEoYb_aoNHL9w(MQ4ABqPHMD_aEoYb_aoNHL9w(a.search, '?'), 1);
    i = (d == null);

    if (!i)
    {
      e = MQ4ABqPHMD_aEoYb_aoNHL9w(d, '\u0026');
      j = Lw4ABqPHMD_aEoYb_aoNHL9w(e);

      for (k = 0; (k < j.length); k++)
      {
        f = j[k];
        g = MQ4ABqPHMD_aEoYb_aoNHL9w(f, '=');
        i = !(g.length > 1);

        if (!i)
        {
          i = !lxMABjDeCj_aRJzaBmU9SJg(window.unescape(LA4ABqPHMD_aEoYb_aoNHL9w(g, 0)), b);

          if (!i)
          {
            c = window.unescape(LA4ABqPHMD_aEoYb_aoNHL9w(g, 1));
            break;
          }

        }

      }

    }

    h = c;
    return h;
  };

  // ScriptCoreLib.JavaScript.DOM.ILocation.replace
  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLDocument+IMSNamespaceCollection.item
  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLDocument+IMSNamespaceCollection.item
  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLDocument+IMSNamespaceCollection.add
  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.ObjectModel.__Collection`1
  function tp0_bSnFuvjCQaUwK5cXJNA(){};
  tp0_bSnFuvjCQaUwK5cXJNA.TypeName = "Collection_1";
  tp0_bSnFuvjCQaUwK5cXJNA.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$tp0_bSnFuvjCQaUwK5cXJNA = tp0_bSnFuvjCQaUwK5cXJNA.prototype;
  type$tp0_bSnFuvjCQaUwK5cXJNA.constructor = tp0_bSnFuvjCQaUwK5cXJNA;
  type$tp0_bSnFuvjCQaUwK5cXJNA.items = null;
  var basector$tp0_bSnFuvjCQaUwK5cXJNA = $ctor$(null, null, type$tp0_bSnFuvjCQaUwK5cXJNA);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.ObjectModel.__Collection`1..ctor
  type$tp0_bSnFuvjCQaUwK5cXJNA.FAkABnFuvjCQaUwK5cXJNA = function ()
  {
    var a = this;

    a.items = new ctor$swAABnGXyTaWJhb6CcyWQQ();
  };
  var ctor$FAkABnFuvjCQaUwK5cXJNA = tp0_bSnFuvjCQaUwK5cXJNA.ctor = $ctor$(null, 'FAkABnFuvjCQaUwK5cXJNA', type$tp0_bSnFuvjCQaUwK5cXJNA);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.ObjectModel.__Collection`1.InsertItemBody
  type$tp0_bSnFuvjCQaUwK5cXJNA.FQkABnFuvjCQaUwK5cXJNA = function (b, c)
  {
    var a = this;

    a.items._1hkABiRqbTmIbxb0k2jSqw(b, c);
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.ObjectModel.__Collection`1.InsertItem
  type$tp0_bSnFuvjCQaUwK5cXJNA.FgkABnFuvjCQaUwK5cXJNA = function (b, c)
  {
    var a = this;

    a.FQkABnFuvjCQaUwK5cXJNA(b, c);
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.ObjectModel.__Collection`1.SetItemBody
  type$tp0_bSnFuvjCQaUwK5cXJNA.FwkABnFuvjCQaUwK5cXJNA = function (b, c)
  {
    var a = this;

    a.items._1BkABiRqbTmIbxb0k2jSqw(b, c);
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.ObjectModel.__Collection`1.SetItem
  type$tp0_bSnFuvjCQaUwK5cXJNA.GAkABnFuvjCQaUwK5cXJNA = function (b, c)
  {
    var a = this;

    a.FwkABnFuvjCQaUwK5cXJNA(b, c);
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.ObjectModel.__Collection`1.Add
  type$tp0_bSnFuvjCQaUwK5cXJNA.GQkABnFuvjCQaUwK5cXJNA = function (b)
  {
    var a = this, c;

    c = a.items.IBkABnTAkDm_aGe9ZbsQrAQ();
    a.FgkABnFuvjCQaUwK5cXJNA(c, b);
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.ObjectModel.__Collection`1.Clear
  type$tp0_bSnFuvjCQaUwK5cXJNA.GgkABnFuvjCQaUwK5cXJNA = function ()
  {
    var a = this;

    a.GwkABnFuvjCQaUwK5cXJNA();
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.ObjectModel.__Collection`1.ClearItems
  type$tp0_bSnFuvjCQaUwK5cXJNA.GwkABnFuvjCQaUwK5cXJNA = function ()
  {
    var a = this;

    a.items.IxkABnTAkDm_aGe9ZbsQrAQ();
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.ObjectModel.__Collection`1.Remove
  type$tp0_bSnFuvjCQaUwK5cXJNA.HAkABnFuvjCQaUwK5cXJNA = function (b)
  {
    var a = this, c, d, e;

    c = a.items._1RkABiRqbTmIbxb0k2jSqw(b);
    e = (c < 0);

    if (!e)
    {
      a.HgkABnFuvjCQaUwK5cXJNA(c);
      d = 1;
      return d;
    }

    d = 0;
    return d;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.ObjectModel.__Collection`1.RemoveItemBody
  type$tp0_bSnFuvjCQaUwK5cXJNA.HQkABnFuvjCQaUwK5cXJNA = function (b)
  {
    var a = this;

    a.items._1xkABiRqbTmIbxb0k2jSqw(b);
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.ObjectModel.__Collection`1.RemoveItem
  type$tp0_bSnFuvjCQaUwK5cXJNA.HgkABnFuvjCQaUwK5cXJNA = function (b)
  {
    var a = this;

    a.HQkABnFuvjCQaUwK5cXJNA(b);
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.ObjectModel.__Collection`1.IndexOf
  type$tp0_bSnFuvjCQaUwK5cXJNA.HwkABnFuvjCQaUwK5cXJNA = function (b)
  {
    var a = this, c;

    c = a.items._1RkABiRqbTmIbxb0k2jSqw(b);
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.ObjectModel.__Collection`1.Insert
  type$tp0_bSnFuvjCQaUwK5cXJNA.IAkABnFuvjCQaUwK5cXJNA = function (b, c)
  {
    var a = this;

    a.FgkABnFuvjCQaUwK5cXJNA(b, c);
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.ObjectModel.__Collection`1.RemoveAt
  type$tp0_bSnFuvjCQaUwK5cXJNA.IQkABnFuvjCQaUwK5cXJNA = function (b)
  {
    var a = this;

    a.HgkABnFuvjCQaUwK5cXJNA(b);
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.ObjectModel.__Collection`1.get_Item
  type$tp0_bSnFuvjCQaUwK5cXJNA.IgkABnFuvjCQaUwK5cXJNA = function (b)
  {
    var a = this, c;

    c = a.items._0xkABiRqbTmIbxb0k2jSqw(b);
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.ObjectModel.__Collection`1.set_Item
  type$tp0_bSnFuvjCQaUwK5cXJNA.IwkABnFuvjCQaUwK5cXJNA = function (b, c)
  {
    var a = this;

    a.GAkABnFuvjCQaUwK5cXJNA(b, c);
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.ObjectModel.__Collection`1.Contains
  type$tp0_bSnFuvjCQaUwK5cXJNA.JAkABnFuvjCQaUwK5cXJNA = function (b)
  {
    var a = this, c;

    c = a.items.JBkABnTAkDm_aGe9ZbsQrAQ(b);
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.ObjectModel.__Collection`1.CopyTo
  type$tp0_bSnFuvjCQaUwK5cXJNA.JQkABnFuvjCQaUwK5cXJNA = function (b, c)
  {
    var a = this;

    a.items.JRkABnTAkDm_aGe9ZbsQrAQ(b, c);
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.ObjectModel.__Collection`1.get_Count
  type$tp0_bSnFuvjCQaUwK5cXJNA.JgkABnFuvjCQaUwK5cXJNA = function ()
  {
    var a = this, b;

    b = a.items.IBkABnTAkDm_aGe9ZbsQrAQ();
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.ObjectModel.__Collection`1.get_IsReadOnly
  type$tp0_bSnFuvjCQaUwK5cXJNA.JwkABnFuvjCQaUwK5cXJNA = function ()
  {
    var a = this, b;

    b = a.items.IRkABnTAkDm_aGe9ZbsQrAQ();
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.ObjectModel.__Collection`1.GetEnumerator
  type$tp0_bSnFuvjCQaUwK5cXJNA.KAkABnFuvjCQaUwK5cXJNA = function ()
  {
    var a = this, b;

    b = a.items.NgEABnMeWzaNooAKOmFm5g();
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.ObjectModel.__Collection`1.global::System.Collections.IEnumerable.GetEnumerator
  type$tp0_bSnFuvjCQaUwK5cXJNA.KQkABnFuvjCQaUwK5cXJNA = function ()
  {
    var a = this;

    throw lwAABq9OGjCe3bHElJJ0LA('The method or operation is not implemented.');
  };

  // 
  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.ObjectModel.__Collection`1
  (function (i)  {
    i._0xkABiRqbTmIbxb0k2jSqw = i.IgkABnFuvjCQaUwK5cXJNA;
    i._1BkABiRqbTmIbxb0k2jSqw = i.IwkABnFuvjCQaUwK5cXJNA;
    i._1RkABiRqbTmIbxb0k2jSqw = i.HwkABnFuvjCQaUwK5cXJNA;
    i._1hkABiRqbTmIbxb0k2jSqw = i.IAkABnFuvjCQaUwK5cXJNA;
    i._1xkABiRqbTmIbxb0k2jSqw = i.IQkABnFuvjCQaUwK5cXJNA;
    // 
    i.IBkABnTAkDm_aGe9ZbsQrAQ = i.JgkABnFuvjCQaUwK5cXJNA;
    i.IRkABnTAkDm_aGe9ZbsQrAQ = i.JwkABnFuvjCQaUwK5cXJNA;
    i.IhkABnTAkDm_aGe9ZbsQrAQ = i.GQkABnFuvjCQaUwK5cXJNA;
    i.IxkABnTAkDm_aGe9ZbsQrAQ = i.GgkABnFuvjCQaUwK5cXJNA;
    i.JBkABnTAkDm_aGe9ZbsQrAQ = i.JAkABnFuvjCQaUwK5cXJNA;
    i.JRkABnTAkDm_aGe9ZbsQrAQ = i.JQkABnFuvjCQaUwK5cXJNA;
    i.JhkABnTAkDm_aGe9ZbsQrAQ = i.HAkABnFuvjCQaUwK5cXJNA;
    // 
    i.NgEABnMeWzaNooAKOmFm5g = i.KAkABnFuvjCQaUwK5cXJNA;
    // System.Collections.IEnumerable
    i.EQAABu7N0xGI6ACQJ1TEOg = i.KQkABnFuvjCQaUwK5cXJNA;
  }
  )(type$tp0_bSnFuvjCQaUwK5cXJNA);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.ComponentModel.__BindingList`1
  function Ut9JsUT1jDWCJ0XOWBoljg(){};
  Ut9JsUT1jDWCJ0XOWBoljg.TypeName = "BindingList_1";
  Ut9JsUT1jDWCJ0XOWBoljg.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$Ut9JsUT1jDWCJ0XOWBoljg = Ut9JsUT1jDWCJ0XOWBoljg.prototype = new tp0_bSnFuvjCQaUwK5cXJNA();
  type$Ut9JsUT1jDWCJ0XOWBoljg.constructor = Ut9JsUT1jDWCJ0XOWBoljg;
  type$Ut9JsUT1jDWCJ0XOWBoljg.ListChanged = null;
  type$Ut9JsUT1jDWCJ0XOWBoljg._RaiseListChangedEvents_k__BackingField = false;
  var basector$Ut9JsUT1jDWCJ0XOWBoljg = $ctor$(basector$tp0_bSnFuvjCQaUwK5cXJNA, null, type$Ut9JsUT1jDWCJ0XOWBoljg);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.ComponentModel.__BindingList`1..ctor
  type$Ut9JsUT1jDWCJ0XOWBoljg.LAkABkT1jDWCJ0XOWBoljg = function ()
  {
    var a = this;

    a.FAkABnFuvjCQaUwK5cXJNA();
    a.LwkABkT1jDWCJ0XOWBoljg(1);
  };
  var ctor$LAkABkT1jDWCJ0XOWBoljg = Ut9JsUT1jDWCJ0XOWBoljg.ctor = $ctor$(basector$tp0_bSnFuvjCQaUwK5cXJNA, 'LAkABkT1jDWCJ0XOWBoljg', type$Ut9JsUT1jDWCJ0XOWBoljg);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.ComponentModel.__BindingList`1.FireListChanged
  type$Ut9JsUT1jDWCJ0XOWBoljg.LQkABkT1jDWCJ0XOWBoljg = function (b, c)
  {
    var a = this, d;

    d = !a.LgkABkT1jDWCJ0XOWBoljg();

    if (!d)
    {
      a.MAkABkT1jDWCJ0XOWBoljg(new ctor$_6wEABk8ScD2xMP0_bk23JjQ(b, c));
    }

  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.ComponentModel.__BindingList`1.get_RaiseListChangedEvents
  type$Ut9JsUT1jDWCJ0XOWBoljg.LgkABkT1jDWCJ0XOWBoljg = function ()
  {
    return this._RaiseListChangedEvents_k__BackingField;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.ComponentModel.__BindingList`1.set_RaiseListChangedEvents
  type$Ut9JsUT1jDWCJ0XOWBoljg.LwkABkT1jDWCJ0XOWBoljg = function (b)
  {
    var a = this;

    a._RaiseListChangedEvents_k__BackingField = b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.ComponentModel.__BindingList`1.OnListChanged
  type$Ut9JsUT1jDWCJ0XOWBoljg.MAkABkT1jDWCJ0XOWBoljg = function (b)
  {
    var a = this, c;

    c = (a.ListChanged == null);

    if (!c)
    {
      a.ListChanged.Invoke(a, b);
    }

  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.ComponentModel.__BindingList`1.add_ListChanged
  type$Ut9JsUT1jDWCJ0XOWBoljg.MQkABkT1jDWCJ0XOWBoljg = function (b)
  {
    var a = this;

    a.ListChanged = fwsABqgPxjmDnjkmC_a5nbw(a.ListChanged, b);
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.ComponentModel.__BindingList`1.remove_ListChanged
  type$Ut9JsUT1jDWCJ0XOWBoljg.MgkABkT1jDWCJ0XOWBoljg = function (b)
  {
    var a = this;

    a.ListChanged = gQsABqgPxjmDnjkmC_a5nbw(a.ListChanged, b);
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.ComponentModel.__BindingList`1.InsertItem
  type$Ut9JsUT1jDWCJ0XOWBoljg.MwkABkT1jDWCJ0XOWBoljg = function (b, c)
  {
    var a = this;

    a.FQkABnFuvjCQaUwK5cXJNA(b, c);
    a.LQkABkT1jDWCJ0XOWBoljg(1, b);
  };
    Ut9JsUT1jDWCJ0XOWBoljg.prototype.FgkABnFuvjCQaUwK5cXJNA = Ut9JsUT1jDWCJ0XOWBoljg.prototype.MwkABkT1jDWCJ0XOWBoljg;

  // ScriptCoreLib.JavaScript.BCLImplementation.System.ComponentModel.__BindingList`1.SetItem
  type$Ut9JsUT1jDWCJ0XOWBoljg.NAkABkT1jDWCJ0XOWBoljg = function (b, c)
  {
    var a = this;

    a.FwkABnFuvjCQaUwK5cXJNA(b, c);
    a.LQkABkT1jDWCJ0XOWBoljg(4, b);
  };
    Ut9JsUT1jDWCJ0XOWBoljg.prototype.GAkABnFuvjCQaUwK5cXJNA = Ut9JsUT1jDWCJ0XOWBoljg.prototype.NAkABkT1jDWCJ0XOWBoljg;

  // ScriptCoreLib.JavaScript.BCLImplementation.System.ComponentModel.__BindingList`1.RemoveItem
  type$Ut9JsUT1jDWCJ0XOWBoljg.NQkABkT1jDWCJ0XOWBoljg = function (b)
  {
    var a = this;

    a.HQkABnFuvjCQaUwK5cXJNA(b);
    a.LQkABkT1jDWCJ0XOWBoljg(2, b);
  };
    Ut9JsUT1jDWCJ0XOWBoljg.prototype.HgkABnFuvjCQaUwK5cXJNA = Ut9JsUT1jDWCJ0XOWBoljg.prototype.NQkABkT1jDWCJ0XOWBoljg;

  // 
  // ScriptCoreLib.JavaScript.BCLImplementation.System.ComponentModel.__BindingList`1
  (function (i)  {
    i._0xkABiRqbTmIbxb0k2jSqw = i.IgkABnFuvjCQaUwK5cXJNA;
    i._1BkABiRqbTmIbxb0k2jSqw = i.IwkABnFuvjCQaUwK5cXJNA;
    i._1RkABiRqbTmIbxb0k2jSqw = i.HwkABnFuvjCQaUwK5cXJNA;
    i._1hkABiRqbTmIbxb0k2jSqw = i.IAkABnFuvjCQaUwK5cXJNA;
    i._1xkABiRqbTmIbxb0k2jSqw = i.IQkABnFuvjCQaUwK5cXJNA;
    // 
    i.IBkABnTAkDm_aGe9ZbsQrAQ = i.JgkABnFuvjCQaUwK5cXJNA;
    i.IRkABnTAkDm_aGe9ZbsQrAQ = i.JwkABnFuvjCQaUwK5cXJNA;
    i.IhkABnTAkDm_aGe9ZbsQrAQ = i.GQkABnFuvjCQaUwK5cXJNA;
    i.IxkABnTAkDm_aGe9ZbsQrAQ = i.GgkABnFuvjCQaUwK5cXJNA;
    i.JBkABnTAkDm_aGe9ZbsQrAQ = i.JAkABnFuvjCQaUwK5cXJNA;
    i.JRkABnTAkDm_aGe9ZbsQrAQ = i.JQkABnFuvjCQaUwK5cXJNA;
    i.JhkABnTAkDm_aGe9ZbsQrAQ = i.HAkABnFuvjCQaUwK5cXJNA;
    // 
    i.NgEABnMeWzaNooAKOmFm5g = i.KAkABnFuvjCQaUwK5cXJNA;
    // System.Collections.IEnumerable
    i.EQAABu7N0xGI6ACQJ1TEOg = i.KQkABnFuvjCQaUwK5cXJNA;
    // ScriptCoreLib.JavaScript.BCLImplementation.System.ComponentModel.__IBindingList
    i.KgkABlS7FDWOiDAPgzDRQw = i.MQkABkT1jDWCJ0XOWBoljg;
    i.KwkABlS7FDWOiDAPgzDRQw = i.MgkABkT1jDWCJ0XOWBoljg;
  }
  )(type$Ut9JsUT1jDWCJ0XOWBoljg);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__DateTime
  function zuYGquIWQTOzXhToksaJfw(){};
  zuYGquIWQTOzXhToksaJfw.TypeName = "DateTime";
  zuYGquIWQTOzXhToksaJfw.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$zuYGquIWQTOzXhToksaJfw = zuYGquIWQTOzXhToksaJfw.prototype;
  type$zuYGquIWQTOzXhToksaJfw.constructor = zuYGquIWQTOzXhToksaJfw;
  var cgMABOIWQTOzXhToksaJfw = null;
  var cwMABOIWQTOzXhToksaJfw = null;
  type$zuYGquIWQTOzXhToksaJfw.Value = null;
  var basector$zuYGquIWQTOzXhToksaJfw = $ctor$(null, null, type$zuYGquIWQTOzXhToksaJfw);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__DateTime..ctor
  type$zuYGquIWQTOzXhToksaJfw.AgkABuIWQTOzXhToksaJfw = function ()
  {
    var a = this;

  };
  var ctor$AgkABuIWQTOzXhToksaJfw = zuYGquIWQTOzXhToksaJfw.ctor = $ctor$(null, 'AgkABuIWQTOzXhToksaJfw', type$zuYGquIWQTOzXhToksaJfw);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__DateTime..ctor
  type$zuYGquIWQTOzXhToksaJfw.AwkABuIWQTOzXhToksaJfw = function (b)
  {
    var a = this, c, d;

    d = ((b < 0) ? 0 : !(b > 3155378975999999999));

    if (!d)
    {
      throw lwAABq9OGjCe3bHElJJ0LA('ArgumentOutOfRange_DateTimeBadTicks');
    }

    c = ((b - 621355968000000000) / 65536);
    a.Value = new Date(c);
  };
  var ctor$AwkABuIWQTOzXhToksaJfw = $ctor$(null, 'AwkABuIWQTOzXhToksaJfw', type$zuYGquIWQTOzXhToksaJfw);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__DateTime..ctor
  type$zuYGquIWQTOzXhToksaJfw.BAkABuIWQTOzXhToksaJfw = function (b, c, d)
  {
    var a = this;

    a.Value = new Date();
    a.Value.setFullYear(b);
    a.Value.setMonth((c - 1));
    a.Value.setDate(d);
  };
  var ctor$BAkABuIWQTOzXhToksaJfw = $ctor$(null, 'BAkABuIWQTOzXhToksaJfw', type$zuYGquIWQTOzXhToksaJfw);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__DateTime.get_Now
  function BQkABuIWQTOzXhToksaJfw()
  {
    var b, c;

    b = new ctor$AgkABuIWQTOzXhToksaJfw();
    b.Value = new Date();
    c = b;
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__DateTime.get_Millisecond
  type$zuYGquIWQTOzXhToksaJfw.BgkABuIWQTOzXhToksaJfw = function ()
  {
    var a = this, b;

    b = a.Value.getMilliseconds();
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__DateTime.get_Second
  type$zuYGquIWQTOzXhToksaJfw.BwkABuIWQTOzXhToksaJfw = function ()
  {
    var a = this, b;

    b = a.Value.getSeconds();
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__DateTime.get_Minute
  type$zuYGquIWQTOzXhToksaJfw.CAkABuIWQTOzXhToksaJfw = function ()
  {
    var a = this, b;

    b = a.Value.getMinutes();
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__DateTime.get_Hour
  type$zuYGquIWQTOzXhToksaJfw.CQkABuIWQTOzXhToksaJfw = function ()
  {
    var a = this, b;

    b = a.Value.getHours();
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__DateTime.get_DayOfWeek
  type$zuYGquIWQTOzXhToksaJfw.CgkABuIWQTOzXhToksaJfw = function ()
  {
    var a = this, b;

    b = a.Value.getDay();
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__DateTime.get_Day
  type$zuYGquIWQTOzXhToksaJfw.CwkABuIWQTOzXhToksaJfw = function ()
  {
    var a = this, b;

    b = a.Value.getDate();
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__DateTime.get_Month
  type$zuYGquIWQTOzXhToksaJfw.DAkABuIWQTOzXhToksaJfw = function ()
  {
    var a = this, b;

    b = (a.Value.getMonth() + 1);
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__DateTime.get_Year
  type$zuYGquIWQTOzXhToksaJfw.DQkABuIWQTOzXhToksaJfw = function ()
  {
    var a = this, b;

    b = a.Value.getFullYear();
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__DateTime.get_Ticks
  type$zuYGquIWQTOzXhToksaJfw.DgkABuIWQTOzXhToksaJfw = function ()
  {
    var a = this, b, c;

    b = a.Value.getTime();
    c = ((b * 65536) + 621355968000000000);
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__DateTime.DaysInMonth
  function DwkABuIWQTOzXhToksaJfw(b, c)
  {
    var d, e, f;

    f = !(c < 1);

    if (!f)
    {
      throw lwAABq9OGjCe3bHElJJ0LA('ArgumentOutOfRange_Month');
    }

    f = !(c > 12);

    if (!f)
    {
      throw lwAABq9OGjCe3bHElJJ0LA('ArgumentOutOfRange_Month');
    }

    d = ((EgkABuIWQTOzXhToksaJfw(b)) ? cgMABOIWQTOzXhToksaJfw : cwMABOIWQTOzXhToksaJfw);
    e = (d[c] - d[(c - 1)]);
    return e;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__DateTime.__ArrayDummy
  function EQkABuIWQTOzXhToksaJfw(b)
  {
    var c;

    c = b;
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__DateTime.IsLeapYear
  function EgkABuIWQTOzXhToksaJfw(b)
  {
    var c, d;

    d = !(b < 1);

    if (!d)
    {
      throw lwAABq9OGjCe3bHElJJ0LA('ArgumentOutOfRange_Year');
    }

    d = !(b > 9999);

    if (!d)
    {
      throw lwAABq9OGjCe3bHElJJ0LA('ArgumentOutOfRange_Year');
    }

    d = !(b % 4);

    if (!d)
    {
      c = 0;
      return c;
    }

    d = !!(b % 100);

    if (!d)
    {
      c = !(b % 400);
      return c;
    }

    c = 1;
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__DateTime.ToString
  type$zuYGquIWQTOzXhToksaJfw.toString /* ScriptCoreLib.JavaScript.BCLImplementation.System.__DateTime.ToString */ = function ()
  {
    var a = this, b, c;

    c = [
      new Number(a.CQkABuIWQTOzXhToksaJfw()),
      ':',
      new Number(a.CAkABuIWQTOzXhToksaJfw()),
      ':',
      new Number(a.BwkABuIWQTOzXhToksaJfw()),
      '.',
      new Number(a.BgkABuIWQTOzXhToksaJfw())
    ];
    b = fBMABjDeCj_aRJzaBmU9SJg(c);
    return b;
  };
    zuYGquIWQTOzXhToksaJfw.prototype.toString /* System.Object.ToString */ = zuYGquIWQTOzXhToksaJfw.prototype.toString /* ScriptCoreLib.JavaScript.BCLImplementation.System.__DateTime.ToString */;

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Dictionary`2+__Enumerator
  function F6BNHISAiTucEpWQKJMKMw(){};
  F6BNHISAiTucEpWQKJMKMw.TypeName = "Enumerator";
  F6BNHISAiTucEpWQKJMKMw.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$F6BNHISAiTucEpWQKJMKMw = F6BNHISAiTucEpWQKJMKMw.prototype;
  type$F6BNHISAiTucEpWQKJMKMw.constructor = F6BNHISAiTucEpWQKJMKMw;
  type$F6BNHISAiTucEpWQKJMKMw.list = null;
  var basector$F6BNHISAiTucEpWQKJMKMw = $ctor$(null, null, type$F6BNHISAiTucEpWQKJMKMw);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Dictionary`2+__Enumerator..ctor
  type$F6BNHISAiTucEpWQKJMKMw.__awgABoSAiTucEpWQKJMKMw = function ()
  {
    var a = this;

    a.__bAgABoSAiTucEpWQKJMKMw(null);
  };
  var ctor$__awgABoSAiTucEpWQKJMKMw = F6BNHISAiTucEpWQKJMKMw.ctor = $ctor$(null, '__awgABoSAiTucEpWQKJMKMw', type$F6BNHISAiTucEpWQKJMKMw);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Dictionary`2+__Enumerator..ctor
  type$F6BNHISAiTucEpWQKJMKMw.__bAgABoSAiTucEpWQKJMKMw = function (b)
  {
    var a = this, c, d, e, f;

    e = !(b == null);

    if (!e)
    {
      return;
    }

    c = new ctor$swAABnGXyTaWJhb6CcyWQQ();
    f = b._5wgABto2cTOZ_bfHP8FSspw().xgAABnGXyTaWJhb6CcyWQQ();
    try
    {
      while (f.zgAABuT_aVDKnn40FH1TlGg())
      {
        d = f.ywAABuT_aVDKnn40FH1TlGg();
        c.vQAABnGXyTaWJhb6CcyWQQ(new ctor$sQMABsP0KzS_bdRyy1rdx_bg(d, b._7QgABto2cTOZ_bfHP8FSspw(d)));
      }
    }
    finally
    {
      ;
      f.xAAABq_bUDz_aWf_aXPRTEtLA();
    }
    a.list = c.xgAABnGXyTaWJhb6CcyWQQ();
  };
  var ctor$__bAgABoSAiTucEpWQKJMKMw = $ctor$(null, '__bAgABoSAiTucEpWQKJMKMw', type$F6BNHISAiTucEpWQKJMKMw);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Dictionary`2+__Enumerator.get_Current
  type$F6BNHISAiTucEpWQKJMKMw.__bQgABoSAiTucEpWQKJMKMw = function ()
  {
    var a = this, b;

    b = a.list.xQAABrYmRzSu_anO2U_bk1MA();
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Dictionary`2+__Enumerator.Dispose
  type$F6BNHISAiTucEpWQKJMKMw.__bggABoSAiTucEpWQKJMKMw = function ()
  {
    var a = this;

    a.list.xAAABq_bUDz_aWf_aXPRTEtLA();
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Dictionary`2+__Enumerator.MoveNext
  type$F6BNHISAiTucEpWQKJMKMw.__bwgABoSAiTucEpWQKJMKMw = function ()
  {
    var a = this, b;

    b = a.list.qAAABu7N0xGI6ACQJ1TEOg();
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Dictionary`2+__Enumerator.System.Collections.IEnumerator.get_Current
  type$F6BNHISAiTucEpWQKJMKMw.AAkABoSAiTucEpWQKJMKMw = function ()
  {
    var a = this, b;

    b = a.__bQgABoSAiTucEpWQKJMKMw();
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Dictionary`2+__Enumerator.Reset
  type$F6BNHISAiTucEpWQKJMKMw.AQkABoSAiTucEpWQKJMKMw = function ()
  {
    var a = this;

    throw lwAABq9OGjCe3bHElJJ0LA('The method or operation is not implemented.');
  };

  // 
  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Dictionary`2+__Enumerator
  (function (i)  {
    i.xQAABrYmRzSu_anO2U_bk1MA = i.__bQgABoSAiTucEpWQKJMKMw;
    // System.IDisposable
    i.xAAABq_bUDz_aWf_aXPRTEtLA = i.__bggABoSAiTucEpWQKJMKMw;
    // System.Collections.IEnumerator
    i.qAAABu7N0xGI6ACQJ1TEOg = i.__bwgABoSAiTucEpWQKJMKMw;
    i.qQAABu7N0xGI6ACQJ1TEOg = i.AAkABoSAiTucEpWQKJMKMw;
    i.qgAABu7N0xGI6ACQJ1TEOg = i.AQkABoSAiTucEpWQKJMKMw;
  }
  )(type$F6BNHISAiTucEpWQKJMKMw);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Dictionary`2
  function PN7Sf9o2cTOZ_bfHP8FSspw(){};
  PN7Sf9o2cTOZ_bfHP8FSspw.TypeName = "Dictionary_2";
  PN7Sf9o2cTOZ_bfHP8FSspw.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$PN7Sf9o2cTOZ_bfHP8FSspw = PN7Sf9o2cTOZ_bfHP8FSspw.prototype;
  type$PN7Sf9o2cTOZ_bfHP8FSspw.constructor = PN7Sf9o2cTOZ_bfHP8FSspw;
  type$PN7Sf9o2cTOZ_bfHP8FSspw._keys = null;
  type$PN7Sf9o2cTOZ_bfHP8FSspw._values = null;
  var basector$PN7Sf9o2cTOZ_bfHP8FSspw = $ctor$(null, null, type$PN7Sf9o2cTOZ_bfHP8FSspw);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Dictionary`2..ctor
  type$PN7Sf9o2cTOZ_bfHP8FSspw._4wgABto2cTOZ_bfHP8FSspw = function ()
  {
    var a = this;

    a._keys = new ctor$__aQgABnXimjuVSFjQZEELng();
    a._values = new ctor$__aggABqUXOTWBTxfXH2TMTA();
  };
  var ctor$_4wgABto2cTOZ_bfHP8FSspw = PN7Sf9o2cTOZ_bfHP8FSspw.ctor = $ctor$(null, '_4wgABto2cTOZ_bfHP8FSspw', type$PN7Sf9o2cTOZ_bfHP8FSspw);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Dictionary`2..ctor
  type$PN7Sf9o2cTOZ_bfHP8FSspw._5AgABto2cTOZ_bfHP8FSspw = function (b)
  {
    var a = this;

    a._keys = new ctor$__aQgABnXimjuVSFjQZEELng();
    a._values = new ctor$__aggABqUXOTWBTxfXH2TMTA();
  };
  var ctor$_5AgABto2cTOZ_bfHP8FSspw = $ctor$(null, '_5AgABto2cTOZ_bfHP8FSspw', type$PN7Sf9o2cTOZ_bfHP8FSspw);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Dictionary`2.Add
  type$PN7Sf9o2cTOZ_bfHP8FSspw._5QgABto2cTOZ_bfHP8FSspw = function (b, c)
  {
    var a = this, d;

    d = !a._5ggABto2cTOZ_bfHP8FSspw(b);

    if (!d)
    {
      throw lwAABq9OGjCe3bHElJJ0LA('Argument_AddingDuplicate');
    }

    a._keys.vQAABnGXyTaWJhb6CcyWQQ(b);
    a._values.vQAABnGXyTaWJhb6CcyWQQ(c);
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Dictionary`2.ContainsKey
  type$PN7Sf9o2cTOZ_bfHP8FSspw._5ggABto2cTOZ_bfHP8FSspw = function (b)
  {
    var a = this, c;

    c = a._keys.wAAABnGXyTaWJhb6CcyWQQ(b);
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Dictionary`2.get_Keys
  type$PN7Sf9o2cTOZ_bfHP8FSspw._5wgABto2cTOZ_bfHP8FSspw = function ()
  {
    var a = this, b;

    b = a._keys;
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Dictionary`2.System.Collections.Generic.IDictionary<TKey,TValue>.get_Keys
  type$PN7Sf9o2cTOZ_bfHP8FSspw._6AgABto2cTOZ_bfHP8FSspw = function ()
  {
    var a = this, b;

    b = a._keys;
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Dictionary`2.Remove
  type$PN7Sf9o2cTOZ_bfHP8FSspw._6QgABto2cTOZ_bfHP8FSspw = function (b)
  {
    var a = this, c, d, e;

    e = a._5ggABto2cTOZ_bfHP8FSspw(b);

    if (!e)
    {
      d = 0;
      return d;
    }

    c = a._keys.twAABnGXyTaWJhb6CcyWQQ(b);
    a._keys.uQAABnGXyTaWJhb6CcyWQQ(c);
    a._values.uQAABnGXyTaWJhb6CcyWQQ(c);
    d = 1;
    return d;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Dictionary`2.TryGetValue
  type$PN7Sf9o2cTOZ_bfHP8FSspw._6ggABto2cTOZ_bfHP8FSspw = function (b, c)
  {
    var a = this;

    throw lwAABq9OGjCe3bHElJJ0LA('The method or operation is not implemented.');
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Dictionary`2.get_Values
  type$PN7Sf9o2cTOZ_bfHP8FSspw._6wgABto2cTOZ_bfHP8FSspw = function ()
  {
    var a = this, b;

    b = a._values;
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Dictionary`2.System.Collections.Generic.IDictionary<TKey,TValue>.get_Values
  type$PN7Sf9o2cTOZ_bfHP8FSspw._7AgABto2cTOZ_bfHP8FSspw = function ()
  {
    var a = this, b;

    b = a._values;
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Dictionary`2.get_Item
  type$PN7Sf9o2cTOZ_bfHP8FSspw._7QgABto2cTOZ_bfHP8FSspw = function (b)
  {
    var a = this, c, d, e;

    c = a._keys.twAABnGXyTaWJhb6CcyWQQ(b);
    e = !(c == -1);

    if (!e)
    {
      throw lwAABq9OGjCe3bHElJJ0LA('Not found.');
    }

    d = a._values.ugAABnGXyTaWJhb6CcyWQQ(c);
    return d;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Dictionary`2.set_Item
  type$PN7Sf9o2cTOZ_bfHP8FSspw._7ggABto2cTOZ_bfHP8FSspw = function (b, c)
  {
    var a = this, d, e;

    d = a._keys.twAABnGXyTaWJhb6CcyWQQ(b);
    e = !(d == -1);

    if (!e)
    {
      a._keys.vQAABnGXyTaWJhb6CcyWQQ(b);
      a._values.vQAABnGXyTaWJhb6CcyWQQ(c);
      return;
    }

    a._values.uwAABnGXyTaWJhb6CcyWQQ(d, c);
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Dictionary`2.Add
  type$PN7Sf9o2cTOZ_bfHP8FSspw._7wgABto2cTOZ_bfHP8FSspw = function (b)
  {
    var a = this;

    a._5QgABto2cTOZ_bfHP8FSspw(b.rAMABsP0KzS_bdRyy1rdx_bg(), b.rgMABsP0KzS_bdRyy1rdx_bg());
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Dictionary`2.Clear
  type$PN7Sf9o2cTOZ_bfHP8FSspw._8AgABto2cTOZ_bfHP8FSspw = function ()
  {
    var a = this;

    a._keys.vwAABnGXyTaWJhb6CcyWQQ();
    a._values.vwAABnGXyTaWJhb6CcyWQQ();
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Dictionary`2.Contains
  type$PN7Sf9o2cTOZ_bfHP8FSspw._8QgABto2cTOZ_bfHP8FSspw = function (b)
  {
    var a = this;

    throw lwAABq9OGjCe3bHElJJ0LA('The method or operation is not implemented.');
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Dictionary`2.CopyTo
  type$PN7Sf9o2cTOZ_bfHP8FSspw._8ggABto2cTOZ_bfHP8FSspw = function (b, c)
  {
    var a = this;

    throw lwAABq9OGjCe3bHElJJ0LA('The method or operation is not implemented.');
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Dictionary`2.get_Count
  type$PN7Sf9o2cTOZ_bfHP8FSspw._8wgABto2cTOZ_bfHP8FSspw = function ()
  {
    var a = this, b;

    b = a._keys.wgAABnGXyTaWJhb6CcyWQQ();
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Dictionary`2.get_IsReadOnly
  type$PN7Sf9o2cTOZ_bfHP8FSspw._9AgABto2cTOZ_bfHP8FSspw = function ()
  {
    var a = this;

    throw lwAABq9OGjCe3bHElJJ0LA('The method or operation is not implemented.');
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Dictionary`2.Remove
  type$PN7Sf9o2cTOZ_bfHP8FSspw._9QgABto2cTOZ_bfHP8FSspw = function (b)
  {
    var a = this;

    throw lwAABq9OGjCe3bHElJJ0LA('The method or operation is not implemented.');
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Dictionary`2.System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey,TValue>>.GetEnumerator
  type$PN7Sf9o2cTOZ_bfHP8FSspw._9ggABto2cTOZ_bfHP8FSspw = function ()
  {
    var a = this, b;

    b = a.__aAgABto2cTOZ_bfHP8FSspw();
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Dictionary`2.System.Collections.IEnumerable.GetEnumerator
  type$PN7Sf9o2cTOZ_bfHP8FSspw._9wgABto2cTOZ_bfHP8FSspw = function ()
  {
    var a = this, b;

    b = a.__aAgABto2cTOZ_bfHP8FSspw();
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Dictionary`2.GetEnumerator
  type$PN7Sf9o2cTOZ_bfHP8FSspw.__aAgABto2cTOZ_bfHP8FSspw = function ()
  {
    var a = this, b;

    b = new ctor$__bAgABoSAiTucEpWQKJMKMw(a);
    return b;
  };

  // 
  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Dictionary`2
  (function (i)  {
    i.JxkABm_az2jGblddb4Z0czA = i._7QgABto2cTOZ_bfHP8FSspw;
    i.KBkABm_az2jGblddb4Z0czA = i._7ggABto2cTOZ_bfHP8FSspw;
    i.KRkABm_az2jGblddb4Z0czA = i._6AgABto2cTOZ_bfHP8FSspw;
    i.KhkABm_az2jGblddb4Z0czA = i._7AgABto2cTOZ_bfHP8FSspw;
    i.KxkABm_az2jGblddb4Z0czA = i._5ggABto2cTOZ_bfHP8FSspw;
    i.LBkABm_az2jGblddb4Z0czA = i._5QgABto2cTOZ_bfHP8FSspw;
    i.LRkABm_az2jGblddb4Z0czA = i._6QgABto2cTOZ_bfHP8FSspw;
    i.LhkABm_az2jGblddb4Z0czA = i._6ggABto2cTOZ_bfHP8FSspw;
    // 
    i.IBkABnTAkDm_aGe9ZbsQrAQ = i._8wgABto2cTOZ_bfHP8FSspw;
    i.IRkABnTAkDm_aGe9ZbsQrAQ = i._9AgABto2cTOZ_bfHP8FSspw;
    i.IhkABnTAkDm_aGe9ZbsQrAQ = i._7wgABto2cTOZ_bfHP8FSspw;
    i.IxkABnTAkDm_aGe9ZbsQrAQ = i._8AgABto2cTOZ_bfHP8FSspw;
    i.JBkABnTAkDm_aGe9ZbsQrAQ = i._8QgABto2cTOZ_bfHP8FSspw;
    i.JRkABnTAkDm_aGe9ZbsQrAQ = i._8ggABto2cTOZ_bfHP8FSspw;
    i.JhkABnTAkDm_aGe9ZbsQrAQ = i._9QgABto2cTOZ_bfHP8FSspw;
    // 
    i.NgEABnMeWzaNooAKOmFm5g = i._9ggABto2cTOZ_bfHP8FSspw;
    // System.Collections.IEnumerable
    i.EQAABu7N0xGI6ACQJ1TEOg = i._9wgABto2cTOZ_bfHP8FSspw;
  }
  )(type$PN7Sf9o2cTOZ_bfHP8FSspw);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.Runtime.CompilerServices.__RuntimeHelpers
  function GlqbXKnefTmTgBvr2WI7_aA(){};
  GlqbXKnefTmTgBvr2WI7_aA.TypeName = "RuntimeHelpers";
  GlqbXKnefTmTgBvr2WI7_aA.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$GlqbXKnefTmTgBvr2WI7_aA = GlqbXKnefTmTgBvr2WI7_aA.prototype;
  type$GlqbXKnefTmTgBvr2WI7_aA.constructor = GlqbXKnefTmTgBvr2WI7_aA;
  var basector$GlqbXKnefTmTgBvr2WI7_aA = $ctor$(null, null, type$GlqbXKnefTmTgBvr2WI7_aA);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.Runtime.CompilerServices.__RuntimeHelpers..ctor
  type$GlqbXKnefTmTgBvr2WI7_aA._2wgABqnefTmTgBvr2WI7_aA = function ()
  {
    var a = this;

  };
  var ctor$_2wgABqnefTmTgBvr2WI7_aA = GlqbXKnefTmTgBvr2WI7_aA.ctor = $ctor$(null, '_2wgABqnefTmTgBvr2WI7_aA', type$GlqbXKnefTmTgBvr2WI7_aA);

  var ZgMABLDT9zCTidQoMa3Dig = null;
  var ZwMABLDT9zCTidQoMa3Dig = null;
  // ScriptCoreLib.Shared.Helper.Invoke
  function sQgABrDT9zCTidQoMa3Dig(b, c)
  {
    var d, e;

    e = (b == null);

    if (!e)
    {
      b.Invoke(c);
    }

    d = c;
    return d;
  };

  // ScriptCoreLib.Shared.Helper.get_BuildDateString
  function rwgABrDT9zCTidQoMa3Dig() { return "18.06.2009 19:33:37 UTC"; };
  // ScriptCoreLib.Shared.Helper.get_CompilerBuildDateString
  function sAgABrDT9zCTidQoMa3Dig() { return "16.06.2009 15:58:44 UTC"; };
  // ScriptCoreLib.Shared.Helper.Invoke
  function sggABrDT9zCTidQoMa3Dig(b)
  {
    var c;

    c = !(b == null);

    if (!c)
    {
      return;
    }

    b.Invoke();
  };

  // ScriptCoreLib.Shared.Helper.Join
  function swgABrDT9zCTidQoMa3Dig(b, c)
  {
    var d, e, f, g;

    d = '';

    for (e = 0; (e < c.length); e++)
    {
      g = !(e > 0);

      if (!g)
      {
        d = gBMABjDeCj_aRJzaBmU9SJg(d, b);
      }

      d = fhMABjDeCj_aRJzaBmU9SJg(d, c[e]);
    }

    f = d;
    return f;
  };

  // ScriptCoreLib.Shared.Helper.DefaultString
  function tAgABrDT9zCTidQoMa3Dig(b, c)
  {
    var d, e;

    e = !(c == null);

    if (!e)
    {
      d = b;
      return d;
    }

    e = !lxMABjDeCj_aRJzaBmU9SJg(c, '');

    if (!e)
    {
      d = b;
      return d;
    }

    d = c;
    return d;
  };

  // ScriptCoreLib.Shared.Helper.VariableEquals
  function tQgABrDT9zCTidQoMa3Dig(a, b) { return a == b; };
  // ScriptCoreLib.Shared.Helper.InvokeTry
  function tggABrDT9zCTidQoMa3Dig(b)
  {
    var c, d;

    c = 1;
    try
    {
      sggABrDT9zCTidQoMa3Dig(b);
    }
    catch (__exc)
    {
      c = 0;
    }
    d = c;
    return d;
  };

  // ScriptCoreLib.Shared.Query.SZArrayEnumerator`1
  function h4q24RLuAz2cCkYgRA33LQ(){};
  h4q24RLuAz2cCkYgRA33LQ.TypeName = "SZArrayEnumerator_1";
  h4q24RLuAz2cCkYgRA33LQ.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$h4q24RLuAz2cCkYgRA33LQ = h4q24RLuAz2cCkYgRA33LQ.prototype;
  type$h4q24RLuAz2cCkYgRA33LQ.constructor = h4q24RLuAz2cCkYgRA33LQ;
  type$h4q24RLuAz2cCkYgRA33LQ._array = null;
  type$h4q24RLuAz2cCkYgRA33LQ._index = 0;
  type$h4q24RLuAz2cCkYgRA33LQ._endIndex = 0;
  var basector$h4q24RLuAz2cCkYgRA33LQ = $ctor$(null, null, type$h4q24RLuAz2cCkYgRA33LQ);
  // ScriptCoreLib.Shared.Query.SZArrayEnumerator`1..ctor
  type$h4q24RLuAz2cCkYgRA33LQ.pggABhLuAz2cCkYgRA33LQ = function (b)
  {
    var a = this, c;

    c = !(b == null);

    if (!c)
    {
      throw lwAABq9OGjCe3bHElJJ0LA('ArgumentNullException');
    }

    a._array = b;
    a._index = -1;
    a._endIndex = b.length;
  };
  var ctor$pggABhLuAz2cCkYgRA33LQ = $ctor$(null, 'pggABhLuAz2cCkYgRA33LQ', type$h4q24RLuAz2cCkYgRA33LQ);

  // ScriptCoreLib.Shared.Query.SZArrayEnumerator`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator
  type$h4q24RLuAz2cCkYgRA33LQ.pwgABhLuAz2cCkYgRA33LQ = function ()
  {
    var a = this, b, c;

    c = !(a._index == -1);

    if (!c)
    {
      b = a;
      return b;
    }

    b = new ctor$pggABhLuAz2cCkYgRA33LQ(a._array);
    return b;
  };

  // ScriptCoreLib.Shared.Query.SZArrayEnumerator`1.System.Collections.IEnumerable.GetEnumerator
  type$h4q24RLuAz2cCkYgRA33LQ.qAgABhLuAz2cCkYgRA33LQ = function ()
  {
    var a = this, b, c;

    c = !(a._index == -1);

    if (!c)
    {
      b = a;
      return b;
    }

    b = new ctor$pggABhLuAz2cCkYgRA33LQ(a._array);
    return b;
  };

  // ScriptCoreLib.Shared.Query.SZArrayEnumerator`1.get_Current
  type$h4q24RLuAz2cCkYgRA33LQ.qQgABhLuAz2cCkYgRA33LQ = function ()
  {
    var a = this, b, c;

    c = !(a._index < 0);

    if (!c)
    {
      throw lwAABq9OGjCe3bHElJJ0LA('InvalidOperation_EnumNotStarted');
    }

    c = (a._index < a._endIndex);

    if (!c)
    {
      throw lwAABq9OGjCe3bHElJJ0LA('InvalidOperation_EnumEnded');
    }

    b = a._array[a._index];
    return b;
  };

  // ScriptCoreLib.Shared.Query.SZArrayEnumerator`1.Dispose
  type$h4q24RLuAz2cCkYgRA33LQ.qggABhLuAz2cCkYgRA33LQ = function ()
  {
    var a = this;

    a._index = -1;
  };

  // ScriptCoreLib.Shared.Query.SZArrayEnumerator`1.System.Collections.IEnumerator.get_Current
  type$h4q24RLuAz2cCkYgRA33LQ.qwgABhLuAz2cCkYgRA33LQ = function ()
  {
    var a = this, b;

    b = a.qQgABhLuAz2cCkYgRA33LQ();
    return b;
  };

  // ScriptCoreLib.Shared.Query.SZArrayEnumerator`1.MoveNext
  type$h4q24RLuAz2cCkYgRA33LQ.rAgABhLuAz2cCkYgRA33LQ = function ()
  {
    var a = this, b, c;

    c = !(a._index < a._endIndex);

    if (!c)
    {
      a._index = (a._index + 1);
      b = (a._index < a._endIndex);
      return b;
    }

    b = 0;
    return b;
  };

  // ScriptCoreLib.Shared.Query.SZArrayEnumerator`1.Reset
  type$h4q24RLuAz2cCkYgRA33LQ.rQgABhLuAz2cCkYgRA33LQ = function ()
  {
    var a = this;

    throw lwAABq9OGjCe3bHElJJ0LA('The method or operation is not implemented.');
  };

  // ScriptCoreLib.Shared.Query.SZArrayEnumerator`1.op_Implicit
  function rggABhLuAz2cCkYgRA33LQ(b)
  {
    var c, d;

    d = !(b == null);

    if (!d)
    {
      c = null;
      return c;
    }

    c = new ctor$pggABhLuAz2cCkYgRA33LQ(b);
    return c;
  };

  // 
  // ScriptCoreLib.Shared.Query.SZArrayEnumerator`1
  (function (i)  {
    i.NgEABnMeWzaNooAKOmFm5g = i.pwgABhLuAz2cCkYgRA33LQ;
    // System.Collections.IEnumerable
    i.EQAABu7N0xGI6ACQJ1TEOg = i.qAgABhLuAz2cCkYgRA33LQ;
    // 
    i.xQAABrYmRzSu_anO2U_bk1MA = i.qQgABhLuAz2cCkYgRA33LQ;
    // System.IDisposable
    i.xAAABq_bUDz_aWf_aXPRTEtLA = i.qggABhLuAz2cCkYgRA33LQ;
    // System.Collections.IEnumerator
    i.qAAABu7N0xGI6ACQJ1TEOg = i.rAgABhLuAz2cCkYgRA33LQ;
    i.qQAABu7N0xGI6ACQJ1TEOg = i.qwgABhLuAz2cCkYgRA33LQ;
    i.qgAABu7N0xGI6ACQJ1TEOg = i.rQgABhLuAz2cCkYgRA33LQ;
  }
  )(type$h4q24RLuAz2cCkYgRA33LQ);
  // ScriptCoreLib.Shared.Serialized.DualNotation`1
  function _5po7aloEVz2Ws5_aM4658JQ(){};
  _5po7aloEVz2Ws5_aM4658JQ.TypeName = "DualNotation_1";
  _5po7aloEVz2Ws5_aM4658JQ.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$_5po7aloEVz2Ws5_aM4658JQ = _5po7aloEVz2Ws5_aM4658JQ.prototype;
  type$_5po7aloEVz2Ws5_aM4658JQ.constructor = _5po7aloEVz2Ws5_aM4658JQ;
  type$_5po7aloEVz2Ws5_aM4658JQ.Stream = null;
  type$_5po7aloEVz2Ws5_aM4658JQ.IsBase64 = false;
  type$_5po7aloEVz2Ws5_aM4658JQ.Target = null;
  var basector$_5po7aloEVz2Ws5_aM4658JQ = $ctor$(null, null, type$_5po7aloEVz2Ws5_aM4658JQ);
  // ScriptCoreLib.Shared.Serialized.DualNotation`1..ctor
  type$_5po7aloEVz2Ws5_aM4658JQ.fwgABloEVz2Ws5_aM4658JQ = function ()
  {
    var a = this;

  };
  var ctor$fwgABloEVz2Ws5_aM4658JQ = _5po7aloEVz2Ws5_aM4658JQ.ctor = $ctor$(null, 'fwgABloEVz2Ws5_aM4658JQ', type$_5po7aloEVz2Ws5_aM4658JQ);

  // ScriptCoreLib.JavaScript.Runtime.Cookie
  function HcuGEoSh8ziUMprx3cOUUw(){};
  HcuGEoSh8ziUMprx3cOUUw.TypeName = "Cookie";
  HcuGEoSh8ziUMprx3cOUUw.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$HcuGEoSh8ziUMprx3cOUUw = HcuGEoSh8ziUMprx3cOUUw.prototype;
  type$HcuGEoSh8ziUMprx3cOUUw.constructor = HcuGEoSh8ziUMprx3cOUUw;
  type$HcuGEoSh8ziUMprx3cOUUw.Name = null;
  var basector$HcuGEoSh8ziUMprx3cOUUw = $ctor$(null, null, type$HcuGEoSh8ziUMprx3cOUUw);
  // ScriptCoreLib.JavaScript.Runtime.Cookie..ctor
  type$HcuGEoSh8ziUMprx3cOUUw.awgABoSh8ziUMprx3cOUUw = function (b)
  {
    var a = this;

    a.Name = b;
  };
  var ctor$awgABoSh8ziUMprx3cOUUw = $ctor$(null, 'awgABoSh8ziUMprx3cOUUw', type$HcuGEoSh8ziUMprx3cOUUw);

  // ScriptCoreLib.JavaScript.Runtime.Cookie.get_PHPSession
  function aQgABoSh8ziUMprx3cOUUw()
  {
    var b;

    b = new ctor$awgABoSh8ziUMprx3cOUUw('PHPSESSID').dAgABoSh8ziUMprx3cOUUw();
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.Cookie.get_Item
  type$HcuGEoSh8ziUMprx3cOUUw.aggABoSh8ziUMprx3cOUUw = function (b)
  {
    var a = this, c;

    c = new ctor$awgABoSh8ziUMprx3cOUUw(gRMABjDeCj_aRJzaBmU9SJg(a.Name, '$', b));
    return c;
  };

  // ScriptCoreLib.JavaScript.Runtime.Cookie.get_EscapedName
  type$HcuGEoSh8ziUMprx3cOUUw.bAgABoSh8ziUMprx3cOUUw = function ()
  {
    var a = this, b;

    b = window.escape(a.Name);
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.Cookie.Delete
  type$HcuGEoSh8ziUMprx3cOUUw.bQgABoSh8ziUMprx3cOUUw = function ()
  {
    var a = this;

    document.cookie = gRMABjDeCj_aRJzaBmU9SJg(a.bAgABoSh8ziUMprx3cOUUw(), '=;expires=', new Date(0).toGMTString());
  };

  // ScriptCoreLib.JavaScript.Runtime.Cookie.get_IntegerValue
  type$HcuGEoSh8ziUMprx3cOUUw.bggABoSh8ziUMprx3cOUUw = function ()
  {
    var a = this, b, c, d;

    b = mRIABhk97zGutkNmTIw91A(a.dAgABoSh8ziUMprx3cOUUw());
    d = !window.isNaN(b);

    if (!d)
    {
      c = 0;
      return c;
    }

    c = b;
    return c;
  };

  // ScriptCoreLib.JavaScript.Runtime.Cookie.set_IntegerValue
  type$HcuGEoSh8ziUMprx3cOUUw.bwgABoSh8ziUMprx3cOUUw = function (b)
  {
    var a = this;

    a.dQgABoSh8ziUMprx3cOUUw((b+''));
  };

  // ScriptCoreLib.JavaScript.Runtime.Cookie.get_BooleanValue
  type$HcuGEoSh8ziUMprx3cOUUw.cAgABoSh8ziUMprx3cOUUw = function ()
  {
    var a = this, b;

    b = lxMABjDeCj_aRJzaBmU9SJg(a.dAgABoSh8ziUMprx3cOUUw(), 'true');
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.Cookie.set_BooleanValue
  type$HcuGEoSh8ziUMprx3cOUUw.cQgABoSh8ziUMprx3cOUUw = function (b)
  {
    var a = this;

    a.dQgABoSh8ziUMprx3cOUUw(((b) ? 'true' : 'false'));
  };

  // ScriptCoreLib.JavaScript.Runtime.Cookie.get_ValueBase64
  type$HcuGEoSh8ziUMprx3cOUUw.cggABoSh8ziUMprx3cOUUw = function ()
  {
    var a = this, b;

    b = YQgABl6xZjKlcaFaQuMTTA(a.dAgABoSh8ziUMprx3cOUUw());
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.Cookie.set_ValueBase64
  type$HcuGEoSh8ziUMprx3cOUUw.cwgABoSh8ziUMprx3cOUUw = function (b)
  {
    var a = this;

    a.dQgABoSh8ziUMprx3cOUUw(YAgABl6xZjKlcaFaQuMTTA(b));
  };

  // ScriptCoreLib.JavaScript.Runtime.Cookie.get_Value
  type$HcuGEoSh8ziUMprx3cOUUw.dAgABoSh8ziUMprx3cOUUw = function ()
  {
    var a = this, b, c, d, e, f, g, h, i;

    g = !(document == null);

    if (!g)
    {
      f = '';
      return f;
    }

    b = MA4ABqPHMD_aEoYb_aoNHL9w(MQ4ABqPHMD_aEoYb_aoNHL9w(document.cookie, '; '));
    c = '';
    h = b;

    for (i = 0; (i < h.length); i++)
    {
      d = h[i];
      e = MA4ABqPHMD_aEoYb_aoNHL9w(MQ4ABqPHMD_aEoYb_aoNHL9w(d, '='));
      g = !lxMABjDeCj_aRJzaBmU9SJg(e[0], a.bAgABoSh8ziUMprx3cOUUw());

      if (!g)
      {
        c = e[1];
        break;
      }

    }

    g = !(c == null);

    if (!g)
    {
      c = '';
    }

    c = window.unescape(c);
    f = ihMABjDeCj_aRJzaBmU9SJg(c);
    return f;
  };

  // ScriptCoreLib.JavaScript.Runtime.Cookie.set_Value
  type$HcuGEoSh8ziUMprx3cOUUw.dQgABoSh8ziUMprx3cOUUw = function (b)
  {
    var a = this, c, d, e, f;

    c = a.dAgABoSh8ziUMprx3cOUUw();
    d = b;
    d = window.escape(ihMABjDeCj_aRJzaBmU9SJg(LA4ABqPHMD_aEoYb_aoNHL9w(NQ4ABqPHMD_aEoYb_aoNHL9w(d), 0)));
    f = !lxMABjDeCj_aRJzaBmU9SJg(c, d);

    if (!f)
    {
      return;
    }

    e = ghMABjDeCj_aRJzaBmU9SJg(a.bAgABoSh8ziUMprx3cOUUw(), '=', d, ';path=\u002f;');
    document.cookie = e;
  };

  // ScriptCoreLib.JavaScript.Runtime.Cookie`1
  function __abhl3Th1cD6tAkOjNbk7qw(){};
  __abhl3Th1cD6tAkOjNbk7qw.TypeName = "Cookie_1";
  __abhl3Th1cD6tAkOjNbk7qw.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$__abhl3Th1cD6tAkOjNbk7qw = __abhl3Th1cD6tAkOjNbk7qw.prototype = new HcuGEoSh8ziUMprx3cOUUw();
  type$__abhl3Th1cD6tAkOjNbk7qw.constructor = __abhl3Th1cD6tAkOjNbk7qw;
  type$__abhl3Th1cD6tAkOjNbk7qw._spawn_helper = null;
  var basector$__abhl3Th1cD6tAkOjNbk7qw = $ctor$(basector$HcuGEoSh8ziUMprx3cOUUw, null, type$__abhl3Th1cD6tAkOjNbk7qw);
  // ScriptCoreLib.JavaScript.Runtime.Cookie`1..ctor
  type$__abhl3Th1cD6tAkOjNbk7qw.dggABjh1cD6tAkOjNbk7qw = function (b)
  {
    var a = this;

    a.awgABoSh8ziUMprx3cOUUw(b);
  };
  var ctor$dggABjh1cD6tAkOjNbk7qw = $ctor$(basector$HcuGEoSh8ziUMprx3cOUUw, 'dggABjh1cD6tAkOjNbk7qw', type$__abhl3Th1cD6tAkOjNbk7qw);

  // ScriptCoreLib.JavaScript.Runtime.Cookie`1..ctor
  type$__abhl3Th1cD6tAkOjNbk7qw.dwgABjh1cD6tAkOjNbk7qw = function (b, c)
  {
    var a = this;

    a.awgABoSh8ziUMprx3cOUUw(b);
    a._spawn_helper = c;
  };
  var ctor$dwgABjh1cD6tAkOjNbk7qw = $ctor$(basector$HcuGEoSh8ziUMprx3cOUUw, 'dwgABjh1cD6tAkOjNbk7qw', type$__abhl3Th1cD6tAkOjNbk7qw);

  // ScriptCoreLib.JavaScript.Runtime.Cookie`1.op_Implicit
  function eAgABjh1cD6tAkOjNbk7qw(b)
  {
    var c;

    c = b.eQgABjh1cD6tAkOjNbk7qw();
    return c;
  };

  // ScriptCoreLib.JavaScript.Runtime.Cookie`1.get_Value
  type$__abhl3Th1cD6tAkOjNbk7qw.eQgABjh1cD6tAkOjNbk7qw = function ()
  {
    var a = this, b, c, d;

    b = new ctor$QhMABoYPyT_a_afjO7wLrjcQ();
    try
    {
      b.PxMABoYPyT_a_afjO7wLrjcQ(a.cggABoSh8ziUMprx3cOUUw());
    }
    catch (__exc){ }
    c = new ctor$VgsABqxcBjuvbuvUgRNVDw();
    c.Target = b.QBMABoYPyT_a_afjO7wLrjcQ();
    c.VAsABqxcBjuvbuvUgRNVDw(a._spawn_helper);
    d = c.Target;
    return d;
  };

  // ScriptCoreLib.JavaScript.Runtime.Cookie`1.set_Value
  type$__abhl3Th1cD6tAkOjNbk7qw.eggABjh1cD6tAkOjNbk7qw = function (b)
  {
    var a = this, c;

    c = new ctor$QhMABoYPyT_a_afjO7wLrjcQ();
    c.QRMABoYPyT_a_afjO7wLrjcQ(b);
    a.cwgABoSh8ziUMprx3cOUUw(c.PhMABoYPyT_a_afjO7wLrjcQ());
  };

  var WwMABF6xZjKlcaFaQuMTTA = null;
  // ScriptCoreLib.JavaScript.Runtime.Convert.DateFromMysqlDateFormatString
  function WAgABl6xZjKlcaFaQuMTTA(b)
  {
    var c, d, e, f, g;

    f = [
      32
    ];
    c = kxMABjDeCj_aRJzaBmU9SJg(b, f)[0];
    f = [
      45
    ];
    d = kxMABjDeCj_aRJzaBmU9SJg(c, f);
    g = [
      d[2],
      '.',
      d[1],
      '.',
      d[0]
    ];
    c = exMABjDeCj_aRJzaBmU9SJg(g);
    e = c;
    return e;
  };

  // ScriptCoreLib.JavaScript.Runtime.Convert.ToHtml
  function WQgABl6xZjKlcaFaQuMTTA(b)
  {
    var c, d, e;

    c = TwAABtcctzqgvmwoTNwgqg();
    e = [
      b
    ];
    QAAABlWwyD6MDQKkPtM6Fg(c, e);
    d = c.innerHTML;
    return d;
  };

  // ScriptCoreLib.JavaScript.Runtime.Convert.ToString
  function WggABl6xZjKlcaFaQuMTTA(c) { return String.fromCharCode(c); };
  // ScriptCoreLib.JavaScript.Runtime.Convert.ToCurrency
  function WwgABl6xZjKlcaFaQuMTTA(b)
  {
    var c, d, e;

    c = fRMABjDeCj_aRJzaBmU9SJg(new Number(Math.round((b * 100))));
    e = !(eBMABjDeCj_aRJzaBmU9SJg(c) > 2);

    if (!e)
    {
      d = gRMABjDeCj_aRJzaBmU9SJg(khMABjDeCj_aRJzaBmU9SJg(c, 0, (eBMABjDeCj_aRJzaBmU9SJg(c) - 2)), '.', kRMABjDeCj_aRJzaBmU9SJg(c, (eBMABjDeCj_aRJzaBmU9SJg(c) - 2)));
      return d;
    }

    e = !(eBMABjDeCj_aRJzaBmU9SJg(c) == 2);

    if (!e)
    {
      d = gBMABjDeCj_aRJzaBmU9SJg('0.', c);
      return d;
    }

    d = gRMABjDeCj_aRJzaBmU9SJg('0.', c, '0');
    return d;
  };

  // ScriptCoreLib.JavaScript.Runtime.Convert.ToRadixString
  function XAgABl6xZjKlcaFaQuMTTA(b, c)
  {
    var d, e, f, g, h, i, j;

    d = '';
    e = '0123456789ABCDEF';
    f = b;
    h = 0;
    while ((f > 0.9))
    {
      h++;
      g = f;
      d = fhMABjDeCj_aRJzaBmU9SJg(eRMABjDeCj_aRJzaBmU9SJg(e, (g % c)), d);
      f = Math.floor((g / c));
    }
    j = !((eBMABjDeCj_aRJzaBmU9SJg(d) % 2) == 1);

    if (!j)
    {
      i = gBMABjDeCj_aRJzaBmU9SJg('0', d);
      return i;
    }

    i = d;
    return i;
  };

  // ScriptCoreLib.JavaScript.Runtime.Convert.ToHexString
  function XQgABl6xZjKlcaFaQuMTTA(b)
  {
    var c, d, e, f, g, h;

    c = new ctor$Gg4ABvXKHjSzDZNs7b2S4g();
    f = b;

    for (g = 0; (g < eBMABjDeCj_aRJzaBmU9SJg(f)); g++)
    {
      d = eRMABjDeCj_aRJzaBmU9SJg(f, g);
      c.Eg4ABvXKHjSzDZNs7b2S4g(XggABl6xZjKlcaFaQuMTTA(d));
    }

    e = c.Fw4ABvXKHjSzDZNs7b2S4g();
    return e;
  };

  // ScriptCoreLib.JavaScript.Runtime.Convert.ToHexString
  function XggABl6xZjKlcaFaQuMTTA(b)
  {
    var c;

    c = XAgABl6xZjKlcaFaQuMTTA(b, 16);
    return c;
  };

  // ScriptCoreLib.JavaScript.Runtime.Convert.ToHexString
  function XwgABl6xZjKlcaFaQuMTTA(b)
  {
    var c;

    c = XAgABl6xZjKlcaFaQuMTTA(b, 16);
    return c;
  };

  // ScriptCoreLib.JavaScript.Runtime.Convert.ToBase64String
  function YAgABl6xZjKlcaFaQuMTTA(b)
  {
    var c, d, e, f, g, h, i, j, k, l, m, n;

    c = '';
    k = 0;
    l = 1;
    while (l)
    {
      d = bBMABjDeCj_aRJzaBmU9SJg(b, k++);
      e = bBMABjDeCj_aRJzaBmU9SJg(b, k++);
      f = bBMABjDeCj_aRJzaBmU9SJg(b, k++);
      g = (d >> 2);
      h = (((d & 3) << 4) | (e >> 4));
      i = (((e & 15) << 2) | (f >> 6));
      j = (f & 63);
      n = !window.isNaN(e);

      if (!n)
      {
        j = 64;
        i = 64;
      }
      else
      {
        n = !window.isNaN(f);

        if (!n)
        {
          j = 64;
        }

      }

      c = fhMABjDeCj_aRJzaBmU9SJg(c, eRMABjDeCj_aRJzaBmU9SJg(WwMABF6xZjKlcaFaQuMTTA, g));
      c = fhMABjDeCj_aRJzaBmU9SJg(c, eRMABjDeCj_aRJzaBmU9SJg(WwMABF6xZjKlcaFaQuMTTA, h));
      c = fhMABjDeCj_aRJzaBmU9SJg(c, eRMABjDeCj_aRJzaBmU9SJg(WwMABF6xZjKlcaFaQuMTTA, i));
      c = fhMABjDeCj_aRJzaBmU9SJg(c, eRMABjDeCj_aRJzaBmU9SJg(WwMABF6xZjKlcaFaQuMTTA, j));
      l = (k < eBMABjDeCj_aRJzaBmU9SJg(b));
    }
    m = c;
    return m;
  };

  // ScriptCoreLib.JavaScript.Runtime.Convert.FromBase64String
  function YQgABl6xZjKlcaFaQuMTTA(b)
  {
    var c, d, e, f, g, h, i, j, k, l, m, n;

    c = '';
    k = 0;
    l = 1;
    while (l)
    {
      g = dRMABjDeCj_aRJzaBmU9SJg(WwMABF6xZjKlcaFaQuMTTA, eRMABjDeCj_aRJzaBmU9SJg(b, k++));
      h = dRMABjDeCj_aRJzaBmU9SJg(WwMABF6xZjKlcaFaQuMTTA, eRMABjDeCj_aRJzaBmU9SJg(b, k++));
      i = dRMABjDeCj_aRJzaBmU9SJg(WwMABF6xZjKlcaFaQuMTTA, eRMABjDeCj_aRJzaBmU9SJg(b, k++));
      j = dRMABjDeCj_aRJzaBmU9SJg(WwMABF6xZjKlcaFaQuMTTA, eRMABjDeCj_aRJzaBmU9SJg(b, k++));
      d = ((g << 2) | (h >> 4));
      e = (((h & 15) << 4) | (i >> 2));
      f = (((i & 3) << 6) | j);
      c = gBMABjDeCj_aRJzaBmU9SJg(c, bRMABjDeCj_aRJzaBmU9SJg(d));
      n = (i == 64);

      if (!n)
      {
        c = gBMABjDeCj_aRJzaBmU9SJg(c, bRMABjDeCj_aRJzaBmU9SJg(e));
      }

      n = (j == 64);

      if (!n)
      {
        c = gBMABjDeCj_aRJzaBmU9SJg(c, bRMABjDeCj_aRJzaBmU9SJg(f));
      }

      l = (k < eBMABjDeCj_aRJzaBmU9SJg(b));
    }
    m = c;
    return m;
  };

  // ScriptCoreLib.JavaScript.Runtime.Convert.ToByte
  function YggABl6xZjKlcaFaQuMTTA(b)
  {
    var c;

    c = (Math.floor(b) % 256);
    return c;
  };

  // ScriptCoreLib.JavaScript.Runtime.Convert.UrlEncode
  function YwgABl6xZjKlcaFaQuMTTA(b)
  {
    var c, d, e, f, g, h;

    c = new ctor$Gg4ABvXKHjSzDZNs7b2S4g();
    d = b;

    for (e = 0; (e < eBMABjDeCj_aRJzaBmU9SJg(d)); e++)
    {
      f = bBMABjDeCj_aRJzaBmU9SJg(d, e);
      c.Eg4ABvXKHjSzDZNs7b2S4g(gBMABjDeCj_aRJzaBmU9SJg('%', XggABl6xZjKlcaFaQuMTTA(f)));
    }

    g = c.Fw4ABvXKHjSzDZNs7b2S4g();
    return g;
  };

  // ScriptCoreLib.JavaScript.Runtime.Convert.ToInteger
  function ZAgABl6xZjKlcaFaQuMTTA(b)
  {
    var c;

    c = Math.round(b);
    return c;
  };

  // ScriptCoreLib.JavaScript.Runtime.Convert.FromJSON
  function ZQgABl6xZjKlcaFaQuMTTA(b, c)
  {
    var d;

    d = _7hIABr5xMzijfM5xNYhyrw(_3xIABr5xMzijfM5xNYhyrw(b, c));
    return d;
  };

  // ScriptCoreLib.JavaScript.Runtime.Convert.ToJSON
  function ZggABl6xZjKlcaFaQuMTTA(b)
  {
    var c;

    c = _2RIABr5xMzijfM5xNYhyrw(BBMABr5xMzijfM5xNYhyrw(b));
    return c;
  };

  // ScriptCoreLib.JavaScript.Runtime.Convert.To
  function ZwgABl6xZjKlcaFaQuMTTA(b, c, d)
  {
    var e, f;

    e = new ctor$UwsABmmiwjiGbtK8fLrm_bw();
    e.TargetIn = b;
    e.TargetOut = c;
    e.UQsABmmiwjiGbtK8fLrm_bw(d);
    f = e.TargetOut;
    return f;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__BinaryReader
  function bR_ayb7e53TOpA9okUA4AYQ(){};
  bR_ayb7e53TOpA9okUA4AYQ.TypeName = "BinaryReader";
  bR_ayb7e53TOpA9okUA4AYQ.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$bR_ayb7e53TOpA9okUA4AYQ = bR_ayb7e53TOpA9okUA4AYQ.prototype;
  type$bR_ayb7e53TOpA9okUA4AYQ.constructor = bR_ayb7e53TOpA9okUA4AYQ;
  type$bR_ayb7e53TOpA9okUA4AYQ.m_stream = null;
  type$bR_ayb7e53TOpA9okUA4AYQ.m_buffer = null;
  var basector$bR_ayb7e53TOpA9okUA4AYQ = $ctor$(null, null, type$bR_ayb7e53TOpA9okUA4AYQ);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__BinaryReader..ctor
  type$bR_ayb7e53TOpA9okUA4AYQ.IQgABre53TOpA9okUA4AYQ = function (b)
  {
    var a = this, c;

    c = !(b == null);

    if (!c)
    {
      throw _0QAAButUdDuooDX2aLKa3w('input');
    }

    a.m_stream = b;
    a.m_buffer = new Array(16);
  };
  var ctor$IQgABre53TOpA9okUA4AYQ = $ctor$(null, 'IQgABre53TOpA9okUA4AYQ', type$bR_ayb7e53TOpA9okUA4AYQ);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__BinaryReader.get_BaseStream
  type$bR_ayb7e53TOpA9okUA4AYQ.IAgABre53TOpA9okUA4AYQ = function ()
  {
    var a = this, b;

    b = a.m_stream;
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__BinaryReader.ReadUInt32
  type$bR_ayb7e53TOpA9okUA4AYQ.IggABre53TOpA9okUA4AYQ = function ()
  {
    var a = this, b, c;

    a.JggABre53TOpA9okUA4AYQ(4);
    b = 0;
    b += a.m_buffer[0];
    b += (a.m_buffer[1] << 8);
    b += (a.m_buffer[2] << 16);
    b += (a.m_buffer[3] << 24);
    c = b;
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__BinaryReader.ReadBytes
  type$bR_ayb7e53TOpA9okUA4AYQ.IwgABre53TOpA9okUA4AYQ = function (b)
  {
    var a = this, c, d;

    c = new Array(b);
    a.m_stream.hgEABlMF5jCv0uAularNmw(c, 0, b);
    d = c;
    return d;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__BinaryReader.ReadInt32
  type$bR_ayb7e53TOpA9okUA4AYQ.JAgABre53TOpA9okUA4AYQ = function ()
  {
    var a = this, b, c;

    a.JggABre53TOpA9okUA4AYQ(4);
    b = 0;
    b += a.m_buffer[0];
    b += (a.m_buffer[1] << 8);
    b += (a.m_buffer[2] << 16);
    b += (a.m_buffer[3] << 24);
    c = b;
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__BinaryReader.ReadInt16
  type$bR_ayb7e53TOpA9okUA4AYQ.JQgABre53TOpA9okUA4AYQ = function ()
  {
    var a = this, b, c;

    a.JggABre53TOpA9okUA4AYQ(2);
    b = 0;
    b = (b + a.m_buffer[0]);
    b = (b + (a.m_buffer[1] << 8));
    c = b;
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__BinaryReader.FillBuffer
  type$bR_ayb7e53TOpA9okUA4AYQ.JggABre53TOpA9okUA4AYQ = function (b)
  {
    var a = this;

    a.m_stream.hgEABlMF5jCv0uAularNmw(a.m_buffer, 0, b);
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__BinaryReader.ReadByte
  type$bR_ayb7e53TOpA9okUA4AYQ.JwgABre53TOpA9okUA4AYQ = function ()
  {
    var a = this, b, c, d, e;

    e = !(a.m_stream == null);

    if (!e)
    {
      throw lwAABq9OGjCe3bHElJJ0LA('FileNotOpen');
    }

    b = a.m_stream.hwEABlMF5jCv0uAularNmw();
    e = !(b == -1);

    if (!e)
    {
      c = ( function () { var c$59 = a.m_stream; return (c$59 instanceof __buKZpkodmziAk5_bSoww_a8g ? c$59 : null); } )();
      e = (c == null);

      if (!e)
      {
        throw lwAABq9OGjCe3bHElJJ0LA(gBMABjDeCj_aRJzaBmU9SJg('MemoryStreamEndOfFile: ', (new ctor$QRUABnYBADScCNBkNEK2Qw(a.m_stream.iwEABlMF5jCv0uAularNmw(), a.m_stream.igEABlMF5jCv0uAularNmw(), b, c.lAEABkodmziAk5_bSoww_a8g())+'')));
      }

      throw lwAABq9OGjCe3bHElJJ0LA(gBMABjDeCj_aRJzaBmU9SJg('EndOfFile: ', (new ctor$SRUABpkrLDCk5CUZLQTt9w(a.m_stream.iwEABlMF5jCv0uAularNmw(), a.m_stream.igEABlMF5jCv0uAularNmw(), b)+'')));
    }

    d = b;
    return d;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__BinaryReader.ReadDouble
  type$bR_ayb7e53TOpA9okUA4AYQ.KAgABre53TOpA9okUA4AYQ = function ()
  {
    var a = this;

    throw _8REABs69FD_astU8tL1xvXQ();
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__BinaryReader.ReadString
  type$bR_ayb7e53TOpA9okUA4AYQ.KQgABre53TOpA9okUA4AYQ = function ()
  {
    var a = this, b, c, d, e, f, g, h, i, j, k, l;

    b = a.KwgABre53TOpA9okUA4AYQ();
    c = a.IwgABre53TOpA9okUA4AYQ(b);
    d = 0;
    e = Hw4ABqPHMD_aEoYb_aoNHL9w();
    while ((d < c.length))
    {
      f = c[d];
      l = !(f < 128);

      if (!l)
      {
        e.push(f);
        d++;
      }
      else
      {
        g = (f > 191);
        h = (f < 224);
        l = (!g || !h);

        if (!l)
        {
          i = c[(d + 1)];
          e.push((((f & 31) << 6) | (i & 63)));
          d += 2;
        }
        else
        {
          i = c[(d + 1)];
          j = c[(d + 2)];
          e.push(((((f & 15) << 12) | ((i & 63) << 6)) | (j & 63)));
          d += 3;
        }

      }

    }
    k = KggABre53TOpA9okUA4AYQ(MA4ABqPHMD_aEoYb_aoNHL9w(e));
    return k;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__BinaryReader.String_fromCharCode
  function KggABre53TOpA9okUA4AYQ(e) { return String.fromCharCode.apply(null, e); };
  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__BinaryReader.Read7BitEncodedInt
  type$bR_ayb7e53TOpA9okUA4AYQ.KwgABre53TOpA9okUA4AYQ = function ()
  {
    var a = this, b, c, d, e, f, g;

    c = 0;
    d = 0;
    e = 1;
    while (e)
    {
      g = !(d == 35);

      if (!g)
      {
        throw lwAABq9OGjCe3bHElJJ0LA('Format_Bad7BitInt32');
      }

      b = a.JwgABre53TOpA9okUA4AYQ();
      c = (c | ((b & 127) << (d & 31)));
      d += 7;
      e = !!(b & 128);
    }
    f = c;
    return f;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__BinaryReader.op_Implicit
  function LAgABre53TOpA9okUA4AYQ(b)
  {
    var c;

    c = b;
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__BinaryWriter
  function mdhTIqtl8TqzEhtngH6T_bw(){};
  mdhTIqtl8TqzEhtngH6T_bw.TypeName = "BinaryWriter";
  mdhTIqtl8TqzEhtngH6T_bw.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$mdhTIqtl8TqzEhtngH6T_bw = mdhTIqtl8TqzEhtngH6T_bw.prototype;
  type$mdhTIqtl8TqzEhtngH6T_bw.constructor = mdhTIqtl8TqzEhtngH6T_bw;
  type$mdhTIqtl8TqzEhtngH6T_bw.OutStream = null;
  type$mdhTIqtl8TqzEhtngH6T_bw._buffer = null;
  var basector$mdhTIqtl8TqzEhtngH6T_bw = $ctor$(null, null, type$mdhTIqtl8TqzEhtngH6T_bw);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__BinaryWriter..ctor
  type$mdhTIqtl8TqzEhtngH6T_bw.FQgABqtl8TqzEhtngH6T_bw = function (b)
  {
    var a = this, c;

    c = !(b == null);

    if (!c)
    {
      throw _0QAAButUdDuooDX2aLKa3w('output');
    }

    a.OutStream = b;
    a._buffer = new Array(16);
  };
  var ctor$FQgABqtl8TqzEhtngH6T_bw = $ctor$(null, 'FQgABqtl8TqzEhtngH6T_bw', type$mdhTIqtl8TqzEhtngH6T_bw);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__BinaryWriter.get_BaseStream
  type$mdhTIqtl8TqzEhtngH6T_bw.FAgABqtl8TqzEhtngH6T_bw = function ()
  {
    var a = this, b;

    b = a.OutStream;
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__BinaryWriter.Dispose
  type$mdhTIqtl8TqzEhtngH6T_bw.FggABqtl8TqzEhtngH6T_bw = function ()
  {
    var a = this;

  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__BinaryWriter.Write
  type$mdhTIqtl8TqzEhtngH6T_bw.FwgABqtl8TqzEhtngH6T_bw = function (b)
  {
    var a = this;

    a._buffer[0] = (b & 255);
    a._buffer[1] = ((b >> 8) & 255);
    a.OutStream.iAEABlMF5jCv0uAularNmw(a._buffer, 0, 2);
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__BinaryWriter.Write
  type$mdhTIqtl8TqzEhtngH6T_bw.GAgABqtl8TqzEhtngH6T_bw = function (b)
  {
    var a = this;

    a._buffer[0] = (b & 255);
    a._buffer[1] = ((b >> 8) & 255);
    a._buffer[2] = ((b >> 16) & 255);
    a._buffer[3] = ((b >> 24) & 255);
    a.OutStream.iAEABlMF5jCv0uAularNmw(a._buffer, 0, 4);
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__BinaryWriter.Write
  type$mdhTIqtl8TqzEhtngH6T_bw.GQgABqtl8TqzEhtngH6T_bw = function (b)
  {
    var a = this;

    a._buffer[0] = (b & 255);
    a._buffer[1] = ((b >> 8) & 255);
    a._buffer[2] = ((b >> 16) & 255);
    a._buffer[3] = ((b >> 24) & 255);
    a.OutStream.iAEABlMF5jCv0uAularNmw(a._buffer, 0, 4);
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__BinaryWriter.Write
  type$mdhTIqtl8TqzEhtngH6T_bw.GggABqtl8TqzEhtngH6T_bw = function (b)
  {
    var a = this;

    a.OutStream.iQEABlMF5jCv0uAularNmw(b);
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__BinaryWriter.Write
  type$mdhTIqtl8TqzEhtngH6T_bw.GwgABqtl8TqzEhtngH6T_bw = function (b)
  {
    var a = this;

    a.OutStream.iAEABlMF5jCv0uAularNmw(b, 0, b.length);
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__BinaryWriter.Write
  type$mdhTIqtl8TqzEhtngH6T_bw.HAgABqtl8TqzEhtngH6T_bw = function (b)
  {
    var a = this;

    throw _8REABs69FD_astU8tL1xvXQ();
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__BinaryWriter.Write
  type$mdhTIqtl8TqzEhtngH6T_bw.HQgABqtl8TqzEhtngH6T_bw = function (b)
  {
    var a = this, c, d, e, f;

    a.HwgABqtl8TqzEhtngH6T_bw(a.HggABqtl8TqzEhtngH6T_bw(b));
    d = b;

    for (e = 0; (e < eBMABjDeCj_aRJzaBmU9SJg(d)); e++)
    {
      c = eRMABjDeCj_aRJzaBmU9SJg(d, e);
      f = !(c < 128);

      if (!f)
      {
        a.FAgABqtl8TqzEhtngH6T_bw().iQEABlMF5jCv0uAularNmw(c);
      }
      else
      {
        f = !(c < 2048);

        if (!f)
        {
          a.FAgABqtl8TqzEhtngH6T_bw().iQEABlMF5jCv0uAularNmw(((c >> 6) | 192));
          a.FAgABqtl8TqzEhtngH6T_bw().iQEABlMF5jCv0uAularNmw(((c & 63) | 128));
        }
        else
        {
          a.FAgABqtl8TqzEhtngH6T_bw().iQEABlMF5jCv0uAularNmw(((c >> 12) | 224));
          a.FAgABqtl8TqzEhtngH6T_bw().iQEABlMF5jCv0uAularNmw((((c >> 6) & 63) | 128));
          a.FAgABqtl8TqzEhtngH6T_bw().iQEABlMF5jCv0uAularNmw(((c & 63) | 128));
        }

      }

    }

  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__BinaryWriter.GetByteCount
  type$mdhTIqtl8TqzEhtngH6T_bw.HggABqtl8TqzEhtngH6T_bw = function (b)
  {
    var a = this, c, d, e, f, g, h;

    c = 0;
    f = b;

    for (g = 0; (g < eBMABjDeCj_aRJzaBmU9SJg(f)); g++)
    {
      d = eRMABjDeCj_aRJzaBmU9SJg(f, g);
      c++;
      h = !(d > 127);

      if (!h)
      {
        c++;
      }

      h = !(d > 2047);

      if (!h)
      {
        c++;
      }

    }

    e = c;
    return e;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__BinaryWriter.Write7BitEncodedInt
  type$mdhTIqtl8TqzEhtngH6T_bw.HwgABqtl8TqzEhtngH6T_bw = function (b)
  {
    var a = this, c, d;

    c = b;
    while (!(c < 128))
    {
      a.GggABqtl8TqzEhtngH6T_bw((c | 128));
      c = (c >> 7);
    }
    a.GggABqtl8TqzEhtngH6T_bw(c);
  };

  // System.IDisposable
  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__BinaryWriter
  (function (i)  {
    i.xAAABq_bUDz_aWf_aXPRTEtLA = i.FggABqtl8TqzEhtngH6T_bw;
  }
  )(type$mdhTIqtl8TqzEhtngH6T_bw);
  // ScriptCoreLib.JavaScript.DOM.XML.IXMLSerializer`1
  function ZJSzmB3T7DmrLa5jZ1qOVA(){};
  ZJSzmB3T7DmrLa5jZ1qOVA.TypeName = "IXMLSerializer_1";
  ZJSzmB3T7DmrLa5jZ1qOVA.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$ZJSzmB3T7DmrLa5jZ1qOVA = ZJSzmB3T7DmrLa5jZ1qOVA.prototype;
  type$ZJSzmB3T7DmrLa5jZ1qOVA.constructor = ZJSzmB3T7DmrLa5jZ1qOVA;
  type$ZJSzmB3T7DmrLa5jZ1qOVA.KnownTypes = null;
  var basector$ZJSzmB3T7DmrLa5jZ1qOVA = $ctor$(null, null, type$ZJSzmB3T7DmrLa5jZ1qOVA);
  // ScriptCoreLib.JavaScript.DOM.XML.IXMLSerializer`1..ctor
  type$ZJSzmB3T7DmrLa5jZ1qOVA.BggABh3T7DmrLa5jZ1qOVA = function (b)
  {
    var a = this, c, d, e, f, g;

    a.KnownTypes = _6BIABr5xMzijfM5xNYhyrw();
    e = !(b == null);

    if (!e)
    {
      throw lwAABq9OGjCe3bHElJJ0LA('IXMLSerializer: k is null');
    }

    f = b;

    for (g = 0; (g < f.length); g++)
    {
      c = f[g];
      d = BBMABr5xMzijfM5xNYhyrw(c);
      CRMABr5xMzijfM5xNYhyrw(a.KnownTypes, _4hIABr5xMzijfM5xNYhyrw(d), d);
    }

  };
  var ctor$BggABh3T7DmrLa5jZ1qOVA = $ctor$(null, 'BggABh3T7DmrLa5jZ1qOVA', type$ZJSzmB3T7DmrLa5jZ1qOVA);

  // ScriptCoreLib.JavaScript.DOM.XML.IXMLSerializer`1.SerializeTo
  type$ZJSzmB3T7DmrLa5jZ1qOVA.BwgABh3T7DmrLa5jZ1qOVA = function (b, c, d)
  {
    var a = this, e, f, g, h, i, j, k, l, m, n, o;

    e = _9BIABr5xMzijfM5xNYhyrw(d);
    k = e;

    for (l = 0; (l < k.length); l++)
    {
      f = k[l];
      g = igcABpT5EDGI7bGdvAw5qA(b, f.Name, []);
      m = (!__ahIABr5xMzijfM5xNYhyrw(f._1hIABu_aiVzem_agWG9y0ZrA()) && !__bxIABr5xMzijfM5xNYhyrw(f._1hIABu_aiVzem_agWG9y0ZrA()));

      if (!m)
      {
        g.appendChild(Fg8ABqyVHzy7gPr_bdrWEIA(b, f._0RIABu_aiVzem_agWG9y0ZrA()));
      }
      else
      {
        m = !__bBIABr5xMzijfM5xNYhyrw(f._1hIABu_aiVzem_agWG9y0ZrA());

        if (!m)
        {
          g.appendChild(Fg8ABqyVHzy7gPr_bdrWEIA(b, f._0RIABu_aiVzem_agWG9y0ZrA()));
        }
        else
        {
          m = !__aBIABr5xMzijfM5xNYhyrw(f._1hIABu_aiVzem_agWG9y0ZrA());

          if (!m)
          {
            h = _7hIABr5xMzijfM5xNYhyrw(f._1hIABu_aiVzem_agWG9y0ZrA());
            n = h;

            for (o = 0; (o < n.length); o++)
            {
              i = n[o];
              j = igcABpT5EDGI7bGdvAw5qA(b, _4hIABr5xMzijfM5xNYhyrw(i), []);
              a.BwgABh3T7DmrLa5jZ1qOVA(b, j, i);
              g.appendChild(j);
            }

          }
          else
          {
            m = (!ABMABr5xMzijfM5xNYhyrw(f._1hIABu_aiVzem_agWG9y0ZrA()) || AhMABr5xMzijfM5xNYhyrw(f._1hIABu_aiVzem_agWG9y0ZrA()));

            if (!m)
            {
              a.BwgABh3T7DmrLa5jZ1qOVA(b, g, f._1hIABu_aiVzem_agWG9y0ZrA());
            }

          }

        }

      }

      c.appendChild(g);
    }

  };

  // ScriptCoreLib.JavaScript.DOM.XML.IXMLSerializer`1.Serialize
  type$ZJSzmB3T7DmrLa5jZ1qOVA.CAgABh3T7DmrLa5jZ1qOVA = function (b)
  {
    var a = this, c, d;

    c = uQUABnwZPj_az_b6m5XFzB4A(_4hIABr5xMzijfM5xNYhyrw(BBMABr5xMzijfM5xNYhyrw(b)));
    a.BwgABh3T7DmrLa5jZ1qOVA(c, c.documentElement, BBMABr5xMzijfM5xNYhyrw(b));
    d = c;
    return d;
  };

  // ScriptCoreLib.JavaScript.DOM.XML.IXMLSerializer`1.DeserializeTo
  type$ZJSzmB3T7DmrLa5jZ1qOVA.CQgABh3T7DmrLa5jZ1qOVA = function (b, c)
  {
    var a = this, d, e, f, g, h, i, j, k, l, m;

    i = !(DRMABr5xMzijfM5xNYhyrw(a.KnownTypes, c) == null);

    if (!i)
    {
      h = null;
      return h;
    }

    d = ERMABr5xMzijfM5xNYhyrw(DRMABr5xMzijfM5xNYhyrw(a.KnownTypes, c));
    j = b.childNodes;

    for (k = 0; (k < j.length); k++)
    {
      e = j[k];
      i = !(e.nodeType == 1);

      if (!i)
      {
        i = !(DRMABr5xMzijfM5xNYhyrw(_5BIABr5xMzijfM5xNYhyrw(d), e.nodeName) == null);

        if (!i)
        {
          CRMABr5xMzijfM5xNYhyrw(d, e.nodeName, jQcABpT5EDGI7bGdvAw5qA(e));
        }
        else
        {
          i = !__aBIABr5xMzijfM5xNYhyrw(DRMABr5xMzijfM5xNYhyrw(_5BIABr5xMzijfM5xNYhyrw(d), e.nodeName));

          if (!i)
          {
            f = Hw4ABqPHMD_aEoYb_aoNHL9w();
            l = e.childNodes;

            for (m = 0; (m < l.length); m++)
            {
              g = l[m];
              i = !(g.nodeType == 1);

              if (!i)
              {
                f = IQ4ABqPHMD_aEoYb_aoNHL9w(f, a.CQgABh3T7DmrLa5jZ1qOVA(g, g.nodeName));
              }

            }

            CRMABr5xMzijfM5xNYhyrw(d, e.nodeName, f);
          }
          else
          {
            CRMABr5xMzijfM5xNYhyrw(d, e.nodeName, a.CQgABh3T7DmrLa5jZ1qOVA(e, _7RIABr5xMzijfM5xNYhyrw(DRMABr5xMzijfM5xNYhyrw(_5BIABr5xMzijfM5xNYhyrw(d), e.nodeName))));
          }

        }

      }

    }

    h = d;
    return h;
  };

  // ScriptCoreLib.JavaScript.DOM.XML.IXMLSerializer`1.Deserialize
  type$ZJSzmB3T7DmrLa5jZ1qOVA.CggABh3T7DmrLa5jZ1qOVA = function (b)
  {
    var a = this, c, d, e;

    d = !(b == null);

    if (!d)
    {
      e = void(0);
      c = e;
      return c;
    }

    c = _7hIABr5xMzijfM5xNYhyrw(a.CQgABh3T7DmrLa5jZ1qOVA(b.documentElement, b.documentElement.nodeName));
    return c;
  };

  var DwMABG1g3jqH_atPTeraONg = null;
  var EAMABG1g3jqH_atPTeraONg = null;
  var EQMABG1g3jqH_atPTeraONg = null;
  var EgMABG1g3jqH_atPTeraONg = null;
  var EwMABG1g3jqH_atPTeraONg = null;
  // ScriptCoreLib.JavaScript.Native.get_DisabledEventHandler
  function fwcABm1g3jqH_atPTeraONg()
  {
    var b;


    if (!(EwMABG1g3jqH_atPTeraONg))
    {
      EwMABG1g3jqH_atPTeraONg = new ctor$KQsABoVjnjetV_aXG4GFkBQ(null, 'hwcABm1g3jqH_atPTeraONg');
    }

    b = EwMABG1g3jqH_atPTeraONg;
    return b;
  };

  // ScriptCoreLib.JavaScript.Native.Spawn
  function gQcABm1g3jqH_atPTeraONg(b)
  {
    var c, d, e, f;

    d = b;

    for (e = 0; (e < d.length); e++)
    {
      c = d[e];
      ggcABm1g3jqH_atPTeraONg(c.A, c.B);
    }

  };

  // ScriptCoreLib.JavaScript.Native.Spawn
  function ggcABm1g3jqH_atPTeraONg(b, c)
  {
    var d, e;

    d = /* DOMCreateType */new C7kJwBz59TCfTnpwidtIag();
    d.id = b;
    d.Spawn = c;
    IA8ABqiGvTSwHMGw_bObBeQ(gBMABjDeCj_aRJzaBmU9SJg('spawn on load: ', d.id));
    e = !(window == null);

    if (!e)
    {
      return;
    }

    TREABtpeljmp_berMhF6qtQ(window, new ctor$KQsABoVjnjetV_aXG4GFkBQ(d, '_Spawn_b__2'));
  };

  // ScriptCoreLib.JavaScript.Native.Spawn
  function gwcABm1g3jqH_atPTeraONg(b, c)
  {
    var d;

    d = /* DOMCreateType */new iayK3Y_bp2zi_ajOCidnWQkA();
    d.id = b;
    d.s = c;
    IA8ABqiGvTSwHMGw_bObBeQ(gBMABjDeCj_aRJzaBmU9SJg('spawn on load: ', d.id));
    TREABtpeljmp_berMhF6qtQ(window, new ctor$KQsABoVjnjetV_aXG4GFkBQ(d, '_Spawn_b__6'));
  };

  // ScriptCoreLib.JavaScript.Native.SpawnInline
  function hAcABm1g3jqH_atPTeraONg(b, c)
  {
    Ig4ABqPHMD_aEoYb_aoNHL9w(DQoABrOeqzW195CXZ74N9w(document, gBMABjDeCj_aRJzaBmU9SJg(b, ':inline')), c);
  };

  // ScriptCoreLib.JavaScript.Native.PlaySound
  function hQcABm1g3jqH_atPTeraONg(b)
  {
    var c, d;

    c = ewsABq8htz2AVt_bTi1Qy_ag();
    c.autostart = 'true';
    c.volume = '100';
    c.src = b;
    Sw0ABoNSrD_aFizz5n6sJfw(c.style, 0, 0, 0, 0);
    document.body.appendChild(c);
    d = c;
    return d;
  };

  // ScriptCoreLib.JavaScript.Native.Include
  function hgcABm1g3jqH_atPTeraONg(b)
  {
    var c;

    IA8ABqiGvTSwHMGw_bObBeQ(gBMABjDeCj_aRJzaBmU9SJg('include ', b));
    c = Hg8ABkYoYz6o6k0GE2GJ_aA();
    c.type = 'text\u002fjavascript';
    c.src = b;
    cwsABt0jLD6yDQ0X6wt5_aw(c);
  };

  // ScriptCoreLib.JavaScript.Native.<get_DisabledEventHandler>b__0
  function hwcABm1g3jqH_atPTeraONg(b)
  {
    _4hQABmvdsDi9SJKe_axnpQQ(b);
    _3RQABmvdsDi9SJKe_axnpQQ(b);
  };

  // ScriptCoreLib.JavaScript.Runtime.Fader
  function i4RNkwltTjWniv3drmFOgg(){};
  i4RNkwltTjWniv3drmFOgg.TypeName = "Fader";
  i4RNkwltTjWniv3drmFOgg.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$i4RNkwltTjWniv3drmFOgg = i4RNkwltTjWniv3drmFOgg.prototype;
  type$i4RNkwltTjWniv3drmFOgg.constructor = i4RNkwltTjWniv3drmFOgg;
  var basector$i4RNkwltTjWniv3drmFOgg = $ctor$(null, null, type$i4RNkwltTjWniv3drmFOgg);
  // ScriptCoreLib.JavaScript.Runtime.Fader..ctor
  type$i4RNkwltTjWniv3drmFOgg.fgcABgltTjWniv3drmFOgg = function ()
  {
    var a = this;

  };
  var ctor$fgcABgltTjWniv3drmFOgg = i4RNkwltTjWniv3drmFOgg.ctor = $ctor$(null, 'fgcABgltTjWniv3drmFOgg', type$i4RNkwltTjWniv3drmFOgg);

  // ScriptCoreLib.JavaScript.Runtime.Fader.FadeOut
  function eAcABgltTjWniv3drmFOgg(b)
  {
    eQcABgltTjWniv3drmFOgg(b, 0, 300);
  };

  // ScriptCoreLib.JavaScript.Runtime.Fader.FadeOut
  function eQcABgltTjWniv3drmFOgg(b, c, d)
  {
    var e;

    e = /* DOMCreateType */new rcNS0INtdzaH79GFOgRlFw();
    e.target = b;
    e.fadetime = d;
    UA0ABoNSrD_aFizz5n6sJfw(e.target.style, 1);
    new ctor$_1gIABilzizCnsZekUeVmgA(new ctor$KQsABoVjnjetV_aXG4GFkBQ(e, '_FadeOut_b__0'))._3wIABilzizCnsZekUeVmgA(c);
  };

  // ScriptCoreLib.JavaScript.Runtime.Fader.FadeAndRemove
  function egcABgltTjWniv3drmFOgg(b)
  {
    fAcABgltTjWniv3drmFOgg(b, 0, 300, []);
  };

  // ScriptCoreLib.JavaScript.Runtime.Fader.Fade
  function ewcABgltTjWniv3drmFOgg(b, c, d, e)
  {
    var f;

    f = /* DOMCreateType */new _6yZECZP9ozm5xQDaInToeg();
    f.target = b;
    f.fadetime = d;
    f.done = e;
    f.target.style.height = fhMABjDeCj_aRJzaBmU9SJg(new Number(f.target.clientHeight), 'px');
    new ctor$_1gIABilzizCnsZekUeVmgA(new ctor$KQsABoVjnjetV_aXG4GFkBQ(f, '_Fade_b__6'))._3wIABilzizCnsZekUeVmgA(c);
  };

  // ScriptCoreLib.JavaScript.Runtime.Fader.FadeAndRemove
  function fAcABgltTjWniv3drmFOgg(b, c, d, e)
  {
    var f;

    f = /* DOMCreateType */new _4f5T_a0uoNDWSVnEPOnQ_agw();
    f.target = b;
    f.fadetime = d;
    f.cotargets = e;
    f.target.style.height = fhMABjDeCj_aRJzaBmU9SJg(new Number(f.target.clientHeight), 'px');
    new ctor$_1gIABilzizCnsZekUeVmgA(new ctor$KQsABoVjnjetV_aXG4GFkBQ(f, '_FadeAndRemove_b__c'))._3wIABilzizCnsZekUeVmgA(c);
  };

  // ScriptCoreLib.JavaScript.Runtime.Fader.FlashAndFadeOut
  function fQcABgltTjWniv3drmFOgg(b, c)
  {
    var d, e, f;

    e = /* DOMCreateType */new UeC9_b_a7LCzCo_aWeeKy6C6A();
    e.e = b;
    d = new ctor$QxMABtwijzGJDK4R_a8wyvw(c);
    d = ShMABtwijzGJDK4R_a8wyvw(d, new ctor$LQsABharvz6dEOdzk7hI5Q(e, '_FlashAndFadeOut_b__12'));
    d = ShMABtwijzGJDK4R_a8wyvw(d, new ctor$LQsABharvz6dEOdzk7hI5Q(e, '_FlashAndFadeOut_b__13'));
    d = ShMABtwijzGJDK4R_a8wyvw(d, new ctor$LQsABharvz6dEOdzk7hI5Q(e, '_FlashAndFadeOut_b__14'));
    d = ShMABtwijzGJDK4R_a8wyvw(d, new ctor$LQsABharvz6dEOdzk7hI5Q(e, '_FlashAndFadeOut_b__15'));
    e.e.style.zIndex = 1000;
    f = d;
    return f;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Environment.get_NewLine
  function CgYABg2CEzqoZ_bNTAiczEw()
  {
    var b;

    b = '\u000d\u000a';
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__TextWriter
  function Wxo_bUh_bS8TGjoRdFP09sxA(){};
  Wxo_bUh_bS8TGjoRdFP09sxA.TypeName = "TextWriter";
  Wxo_bUh_bS8TGjoRdFP09sxA.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$Wxo_bUh_bS8TGjoRdFP09sxA = Wxo_bUh_bS8TGjoRdFP09sxA.prototype;
  type$Wxo_bUh_bS8TGjoRdFP09sxA.constructor = Wxo_bUh_bS8TGjoRdFP09sxA;
  var basector$Wxo_bUh_bS8TGjoRdFP09sxA = $ctor$(null, null, type$Wxo_bUh_bS8TGjoRdFP09sxA);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__TextWriter..ctor
  type$Wxo_bUh_bS8TGjoRdFP09sxA.CQYABh_bS8TGjoRdFP09sxA = function ()
  {
    var a = this;

  };
  var ctor$CQYABh_bS8TGjoRdFP09sxA = Wxo_bUh_bS8TGjoRdFP09sxA.ctor = $ctor$(null, 'CQYABh_bS8TGjoRdFP09sxA', type$Wxo_bUh_bS8TGjoRdFP09sxA);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__TextWriter.WriteLine
  type$Wxo_bUh_bS8TGjoRdFP09sxA.BwYABh_bS8TGjoRdFP09sxA = function (b)
  {
    var a = this;

  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__TextWriter.Dispose
  type$Wxo_bUh_bS8TGjoRdFP09sxA.CAYABh_bS8TGjoRdFP09sxA = function ()
  {
    var a = this;

  };

  // System.IDisposable
  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__TextWriter
  (function (i)  {
    i.xAAABq_bUDz_aWf_aXPRTEtLA = i.CAYABh_bS8TGjoRdFP09sxA;
  }
  )(type$Wxo_bUh_bS8TGjoRdFP09sxA);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__TimeSpan
  function _7TuJuSJncDag7F9nfDUbfQ(){};
  _7TuJuSJncDag7F9nfDUbfQ.TypeName = "TimeSpan";
  _7TuJuSJncDag7F9nfDUbfQ.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$_7TuJuSJncDag7F9nfDUbfQ = _7TuJuSJncDag7F9nfDUbfQ.prototype;
  type$_7TuJuSJncDag7F9nfDUbfQ.constructor = _7TuJuSJncDag7F9nfDUbfQ;
  type$_7TuJuSJncDag7F9nfDUbfQ._TotalMilliseconds_k__BackingField = null;
  var basector$_7TuJuSJncDag7F9nfDUbfQ = $ctor$(null, null, type$_7TuJuSJncDag7F9nfDUbfQ);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__TimeSpan..ctor
  type$_7TuJuSJncDag7F9nfDUbfQ.AQYABiJncDag7F9nfDUbfQ = function ()
  {
    var a = this;

  };
  var ctor$AQYABiJncDag7F9nfDUbfQ = _7TuJuSJncDag7F9nfDUbfQ.ctor = $ctor$(null, 'AQYABiJncDag7F9nfDUbfQ', type$_7TuJuSJncDag7F9nfDUbfQ);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__TimeSpan.get_TotalMilliseconds
  type$_7TuJuSJncDag7F9nfDUbfQ.AgYABiJncDag7F9nfDUbfQ = function ()
  {
    return this._TotalMilliseconds_k__BackingField;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__TimeSpan.set_TotalMilliseconds
  type$_7TuJuSJncDag7F9nfDUbfQ.AwYABiJncDag7F9nfDUbfQ = function (b)
  {
    var a = this;

    a._TotalMilliseconds_k__BackingField = b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__TimeSpan.Parse
  function BAYABiJncDag7F9nfDUbfQ(b)
  {
    var c, d;

    d = new ctor$AQYABiJncDag7F9nfDUbfQ();
    c = d;
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__TimeSpan.FromMilliseconds
  function BQYABiJncDag7F9nfDUbfQ(b)
  {
    var c, d;

    c = new ctor$AQYABiJncDag7F9nfDUbfQ();
    c.AwYABiJncDag7F9nfDUbfQ(b);
    d = BgYABiJncDag7F9nfDUbfQ(c);
    return d;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__TimeSpan.op_Implicit
  function BgYABiJncDag7F9nfDUbfQ(b)
  {
    var c;

    c = b;
    return c;
  };

  // ScriptCoreLib.Shared.Drawing.Color+System.get_ActiveBorder
  function _0QUABgd8VzK5JoeL_bgruXw()
  {
    var b;

    b = zgUABhd1cjKlJqjNj8xntA('ActiveBorder');
    return b;
  };

  // ScriptCoreLib.Shared.Drawing.Color+System.get_ActiveCaption
  function _0gUABgd8VzK5JoeL_bgruXw()
  {
    var b;

    b = zgUABhd1cjKlJqjNj8xntA('ActiveCaption');
    return b;
  };

  // ScriptCoreLib.Shared.Drawing.Color+System.get_AppWorkspace
  function _0wUABgd8VzK5JoeL_bgruXw()
  {
    var b;

    b = zgUABhd1cjKlJqjNj8xntA('AppWorkspace');
    return b;
  };

  // ScriptCoreLib.Shared.Drawing.Color+System.get_Background
  function _1AUABgd8VzK5JoeL_bgruXw()
  {
    var b;

    b = zgUABhd1cjKlJqjNj8xntA('Background');
    return b;
  };

  // ScriptCoreLib.Shared.Drawing.Color+System.get_ButtonFace
  function _1QUABgd8VzK5JoeL_bgruXw()
  {
    var b;

    b = zgUABhd1cjKlJqjNj8xntA('ButtonFace');
    return b;
  };

  // ScriptCoreLib.Shared.Drawing.Color+System.get_ButtonHighlight
  function _1gUABgd8VzK5JoeL_bgruXw()
  {
    var b;

    b = zgUABhd1cjKlJqjNj8xntA('ButtonHighlight');
    return b;
  };

  // ScriptCoreLib.Shared.Drawing.Color+System.get_ButtonShadow
  function _1wUABgd8VzK5JoeL_bgruXw()
  {
    var b;

    b = zgUABhd1cjKlJqjNj8xntA('ButtonShadow');
    return b;
  };

  // ScriptCoreLib.Shared.Drawing.Color+System.get_ButtonText
  function _2AUABgd8VzK5JoeL_bgruXw()
  {
    var b;

    b = zgUABhd1cjKlJqjNj8xntA('ButtonText');
    return b;
  };

  // ScriptCoreLib.Shared.Drawing.Color+System.get_CaptionText
  function _2QUABgd8VzK5JoeL_bgruXw()
  {
    var b;

    b = zgUABhd1cjKlJqjNj8xntA('CaptionText');
    return b;
  };

  // ScriptCoreLib.Shared.Drawing.Color+System.get_GrayText
  function _2gUABgd8VzK5JoeL_bgruXw()
  {
    var b;

    b = zgUABhd1cjKlJqjNj8xntA('GrayText');
    return b;
  };

  // ScriptCoreLib.Shared.Drawing.Color+System.get_Highlight
  function _2wUABgd8VzK5JoeL_bgruXw()
  {
    var b;

    b = zgUABhd1cjKlJqjNj8xntA('Highlight');
    return b;
  };

  // ScriptCoreLib.Shared.Drawing.Color+System.get_HighlightText
  function _3AUABgd8VzK5JoeL_bgruXw()
  {
    var b;

    b = zgUABhd1cjKlJqjNj8xntA('HighlightText');
    return b;
  };

  // ScriptCoreLib.Shared.Drawing.Color+System.get_InactiveBorder
  function _3QUABgd8VzK5JoeL_bgruXw()
  {
    var b;

    b = zgUABhd1cjKlJqjNj8xntA('InactiveBorder');
    return b;
  };

  // ScriptCoreLib.Shared.Drawing.Color+System.get_InactiveCaption
  function _3gUABgd8VzK5JoeL_bgruXw()
  {
    var b;

    b = zgUABhd1cjKlJqjNj8xntA('InactiveCaption');
    return b;
  };

  // ScriptCoreLib.Shared.Drawing.Color+System.get_InactiveCaptionText
  function _3wUABgd8VzK5JoeL_bgruXw()
  {
    var b;

    b = zgUABhd1cjKlJqjNj8xntA('InactiveCaptionText');
    return b;
  };

  // ScriptCoreLib.Shared.Drawing.Color+System.get_InfoBackground
  function _4AUABgd8VzK5JoeL_bgruXw()
  {
    var b;

    b = zgUABhd1cjKlJqjNj8xntA('InfoBackground');
    return b;
  };

  // ScriptCoreLib.Shared.Drawing.Color+System.get_InfoText
  function _4QUABgd8VzK5JoeL_bgruXw()
  {
    var b;

    b = zgUABhd1cjKlJqjNj8xntA('InfoText');
    return b;
  };

  // ScriptCoreLib.Shared.Drawing.Color+System.get_Menu
  function _4gUABgd8VzK5JoeL_bgruXw()
  {
    var b;

    b = zgUABhd1cjKlJqjNj8xntA('Menu');
    return b;
  };

  // ScriptCoreLib.Shared.Drawing.Color+System.get_MenuText
  function _4wUABgd8VzK5JoeL_bgruXw()
  {
    var b;

    b = zgUABhd1cjKlJqjNj8xntA('MenuText');
    return b;
  };

  // ScriptCoreLib.Shared.Drawing.Color+System.get_Scrollbar
  function _5AUABgd8VzK5JoeL_bgruXw()
  {
    var b;

    b = zgUABhd1cjKlJqjNj8xntA('Scrollbar');
    return b;
  };

  // ScriptCoreLib.Shared.Drawing.Color+System.get_ThreeDDarkShadow
  function _5QUABgd8VzK5JoeL_bgruXw()
  {
    var b;

    b = zgUABhd1cjKlJqjNj8xntA('ThreeDDarkShadow');
    return b;
  };

  // ScriptCoreLib.Shared.Drawing.Color+System.get_ThreeDFace
  function _5gUABgd8VzK5JoeL_bgruXw()
  {
    var b;

    b = zgUABhd1cjKlJqjNj8xntA('ThreeDFace');
    return b;
  };

  // ScriptCoreLib.Shared.Drawing.Color+System.get_ThreeDHighlight
  function _5wUABgd8VzK5JoeL_bgruXw()
  {
    var b;

    b = zgUABhd1cjKlJqjNj8xntA('ThreeDHighlight');
    return b;
  };

  // ScriptCoreLib.Shared.Drawing.Color+System.get_ThreeDLightShadow
  function _6AUABgd8VzK5JoeL_bgruXw()
  {
    var b;

    b = zgUABhd1cjKlJqjNj8xntA('ThreeDLightShadow');
    return b;
  };

  // ScriptCoreLib.Shared.Drawing.Color+System.get_ThreeDShadow
  function _6QUABgd8VzK5JoeL_bgruXw()
  {
    var b;

    b = zgUABhd1cjKlJqjNj8xntA('ThreeDShadow');
    return b;
  };

  // ScriptCoreLib.Shared.Drawing.Color+System.get_Window
  function _6gUABgd8VzK5JoeL_bgruXw()
  {
    var b;

    b = zgUABhd1cjKlJqjNj8xntA('Window');
    return b;
  };

  // ScriptCoreLib.Shared.Drawing.Color+System.get_WindowFrame
  function _6wUABgd8VzK5JoeL_bgruXw()
  {
    var b;

    b = zgUABhd1cjKlJqjNj8xntA('WindowFrame');
    return b;
  };

  // ScriptCoreLib.Shared.Drawing.Color+System.get_WindowText
  function _7AUABgd8VzK5JoeL_bgruXw()
  {
    var b;

    b = zgUABhd1cjKlJqjNj8xntA('WindowText');
    return b;
  };

  // ScriptCoreLib.Shared.Drawing.Color
  function zHA_aARd1cjKlJqjNj8xntA(){};
  zHA_aARd1cjKlJqjNj8xntA.TypeName = "Color";
  zHA_aARd1cjKlJqjNj8xntA.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$zHA_aARd1cjKlJqjNj8xntA = zHA_aARd1cjKlJqjNj8xntA.prototype;
  type$zHA_aARd1cjKlJqjNj8xntA.constructor = zHA_aARd1cjKlJqjNj8xntA;
  type$zHA_aARd1cjKlJqjNj8xntA.R = 0;
  type$zHA_aARd1cjKlJqjNj8xntA.G = 0;
  type$zHA_aARd1cjKlJqjNj8xntA.B = 0;
  type$zHA_aARd1cjKlJqjNj8xntA.KnownName = null;
  var basector$zHA_aARd1cjKlJqjNj8xntA = $ctor$(null, null, type$zHA_aARd1cjKlJqjNj8xntA);
  // ScriptCoreLib.Shared.Drawing.Color..ctor
  type$zHA_aARd1cjKlJqjNj8xntA._0AUABhd1cjKlJqjNj8xntA = function ()
  {
    var a = this;

  };
  var ctor$_0AUABhd1cjKlJqjNj8xntA = zHA_aARd1cjKlJqjNj8xntA.ctor = $ctor$(null, '_0AUABhd1cjKlJqjNj8xntA', type$zHA_aARd1cjKlJqjNj8xntA);

  // ScriptCoreLib.Shared.Drawing.Color.op_Implicit
  function wAUABhd1cjKlJqjNj8xntA(b)
  {
    var c;

    c = (b+'');
    return c;
  };

  // ScriptCoreLib.Shared.Drawing.Color.op_Implicit
  function wQUABhd1cjKlJqjNj8xntA(b)
  {
    var c;

    c = ((b.B + (b.G << 8)) + (b.R << 16));
    return c;
  };

  // ScriptCoreLib.Shared.Drawing.Color.op_Implicit
  function wgUABhd1cjKlJqjNj8xntA(b)
  {
    var c, d, e, f;

    c = (b & 255);
    d = ((b >> 8) & 255);
    e = ((b >> 16) & 255);
    f = wwUABhd1cjKlJqjNj8xntA(e, d, c);
    return f;
  };

  // ScriptCoreLib.Shared.Drawing.Color.FromRGB
  function wwUABhd1cjKlJqjNj8xntA(b, c, d)
  {
    var e, f;

    e = new ctor$_0AUABhd1cjKlJqjNj8xntA();
    e.R = b;
    e.G = c;
    e.B = d;
    f = e;
    return f;
  };

  // ScriptCoreLib.Shared.Drawing.Color.FromGray
  function xAUABhd1cjKlJqjNj8xntA(b)
  {
    var c;

    c = wwUABhd1cjKlJqjNj8xntA(b, b, b);
    return c;
  };

  // ScriptCoreLib.Shared.Drawing.Color.get_None
  function xQUABhd1cjKlJqjNj8xntA()
  {
    var b;

    b = zgUABhd1cjKlJqjNj8xntA('');
    return b;
  };

  // ScriptCoreLib.Shared.Drawing.Color.get_Transparent
  function xgUABhd1cjKlJqjNj8xntA()
  {
    var b;

    b = zgUABhd1cjKlJqjNj8xntA('transparent');
    return b;
  };

  // ScriptCoreLib.Shared.Drawing.Color.get_Black
  function xwUABhd1cjKlJqjNj8xntA()
  {
    var b;

    b = xAUABhd1cjKlJqjNj8xntA(0);
    return b;
  };

  // ScriptCoreLib.Shared.Drawing.Color.get_Gray
  function yAUABhd1cjKlJqjNj8xntA()
  {
    var b;

    b = xAUABhd1cjKlJqjNj8xntA(128);
    return b;
  };

  // ScriptCoreLib.Shared.Drawing.Color.get_White
  function yQUABhd1cjKlJqjNj8xntA()
  {
    var b;

    b = xAUABhd1cjKlJqjNj8xntA(255);
    return b;
  };

  // ScriptCoreLib.Shared.Drawing.Color.get_Red
  function ygUABhd1cjKlJqjNj8xntA()
  {
    var b;

    b = wwUABhd1cjKlJqjNj8xntA(255, 0, 0);
    return b;
  };

  // ScriptCoreLib.Shared.Drawing.Color.get_Green
  function ywUABhd1cjKlJqjNj8xntA()
  {
    var b;

    b = wwUABhd1cjKlJqjNj8xntA(0, 255, 0);
    return b;
  };

  // ScriptCoreLib.Shared.Drawing.Color.get_Blue
  function zAUABhd1cjKlJqjNj8xntA()
  {
    var b;

    b = wgUABhd1cjKlJqjNj8xntA(255);
    return b;
  };

  // ScriptCoreLib.Shared.Drawing.Color.get_Yellow
  function zQUABhd1cjKlJqjNj8xntA()
  {
    var b;

    b = wgUABhd1cjKlJqjNj8xntA(16776960);
    return b;
  };

  // ScriptCoreLib.Shared.Drawing.Color.FromKnownName
  function zgUABhd1cjKlJqjNj8xntA(b)
  {
    var c, d;

    c = new ctor$_0AUABhd1cjKlJqjNj8xntA();
    c.KnownName = b;
    d = c;
    return d;
  };

  // ScriptCoreLib.Shared.Drawing.Color.ToString
  type$zHA_aARd1cjKlJqjNj8xntA.toString /* ScriptCoreLib.Shared.Drawing.Color.ToString */ = function ()
  {
    var a = this, b, c, d, e;

    b = a;
    d = (b.KnownName == null);

    if (!d)
    {
      c = b.KnownName;
      return c;
    }

    e = [
      'RGB(',
      new Number(b.R),
      ', ',
      new Number(b.G),
      ', ',
      new Number(b.B),
      ')'
    ];
    c = fBMABjDeCj_aRJzaBmU9SJg(e);
    return c;
  };
    zHA_aARd1cjKlJqjNj8xntA.prototype.toString /* System.Object.ToString */ = zHA_aARd1cjKlJqjNj8xntA.prototype.toString /* ScriptCoreLib.Shared.Drawing.Color.ToString */;

  // ScriptCoreLib.JavaScript.DOM.XML.IXMLDocument+__IXMLDocument_Native.selectSingleNode
  // ScriptCoreLib.JavaScript.DOM.XML.IXMLDocument+__IXMLDocument_Native.selectNodes
  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__TextReader
  function aZFnb_a4m3TqP8M4PCqAT_aw(){};
  aZFnb_a4m3TqP8M4PCqAT_aw.TypeName = "TextReader";
  aZFnb_a4m3TqP8M4PCqAT_aw.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$aZFnb_a4m3TqP8M4PCqAT_aw = aZFnb_a4m3TqP8M4PCqAT_aw.prototype;
  type$aZFnb_a4m3TqP8M4PCqAT_aw.constructor = aZFnb_a4m3TqP8M4PCqAT_aw;
  var basector$aZFnb_a4m3TqP8M4PCqAT_aw = $ctor$(null, null, type$aZFnb_a4m3TqP8M4PCqAT_aw);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__TextReader..ctor
  type$aZFnb_a4m3TqP8M4PCqAT_aw.cQUABu4m3TqP8M4PCqAT_aw = function ()
  {
    var a = this;

  };
  var ctor$cQUABu4m3TqP8M4PCqAT_aw = aZFnb_a4m3TqP8M4PCqAT_aw.ctor = $ctor$(null, 'cQUABu4m3TqP8M4PCqAT_aw', type$aZFnb_a4m3TqP8M4PCqAT_aw);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__TextReader.ReadLine
  type$aZFnb_a4m3TqP8M4PCqAT_aw.bwUABu4m3TqP8M4PCqAT_aw = function ()
  {
    var a = this;

    throw nAAABq584TSQo69VDcfM9Q();
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__TextReader.Dispose
  type$aZFnb_a4m3TqP8M4PCqAT_aw.cAUABu4m3TqP8M4PCqAT_aw = function ()
  {
    var a = this;

  };

  // System.IDisposable
  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__TextReader
  (function (i)  {
    i.xAAABq_bUDz_aWf_aXPRTEtLA = i.cAUABu4m3TqP8M4PCqAT_aw;
  }
  )(type$aZFnb_a4m3TqP8M4PCqAT_aw);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Attribute
  function _4wJc_bUv8Sz_awaRYQafikfw(){};
  _4wJc_bUv8Sz_awaRYQafikfw.TypeName = "Attribute";
  _4wJc_bUv8Sz_awaRYQafikfw.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$_4wJc_bUv8Sz_awaRYQafikfw = _4wJc_bUv8Sz_awaRYQafikfw.prototype;
  type$_4wJc_bUv8Sz_awaRYQafikfw.constructor = _4wJc_bUv8Sz_awaRYQafikfw;
  var basector$_4wJc_bUv8Sz_awaRYQafikfw = $ctor$(null, null, type$_4wJc_bUv8Sz_awaRYQafikfw);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Attribute..ctor
  type$_4wJc_bUv8Sz_awaRYQafikfw.EAUABkv8Sz_awaRYQafikfw = function ()
  {
    var a = this;

  };
  var ctor$EAUABkv8Sz_awaRYQafikfw = _4wJc_bUv8Sz_awaRYQafikfw.ctor = $ctor$(null, 'EAUABkv8Sz_awaRYQafikfw', type$_4wJc_bUv8Sz_awaRYQafikfw);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.ComponentModel.__Container
  function crOifgM6eDasj50UUSMO8Q(){};
  crOifgM6eDasj50UUSMO8Q.TypeName = "Container";
  crOifgM6eDasj50UUSMO8Q.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$crOifgM6eDasj50UUSMO8Q = crOifgM6eDasj50UUSMO8Q.prototype;
  type$crOifgM6eDasj50UUSMO8Q.constructor = crOifgM6eDasj50UUSMO8Q;
  var basector$crOifgM6eDasj50UUSMO8Q = $ctor$(null, null, type$crOifgM6eDasj50UUSMO8Q);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.ComponentModel.__Container..ctor
  type$crOifgM6eDasj50UUSMO8Q.DwUABgM6eDasj50UUSMO8Q = function ()
  {
    var a = this;

  };
  var ctor$DwUABgM6eDasj50UUSMO8Q = crOifgM6eDasj50UUSMO8Q.ctor = $ctor$(null, 'DwUABgM6eDasj50UUSMO8Q', type$crOifgM6eDasj50UUSMO8Q);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.ComponentModel.__Container.Add
  type$crOifgM6eDasj50UUSMO8Q.CgUABgM6eDasj50UUSMO8Q = function (b, c)
  {
    var a = this;

    throw lwAABq9OGjCe3bHElJJ0LA('The method or operation is not implemented.');
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.ComponentModel.__Container.Add
  type$crOifgM6eDasj50UUSMO8Q.CwUABgM6eDasj50UUSMO8Q = function (b)
  {
    var a = this;

  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.ComponentModel.__Container.get_Components
  type$crOifgM6eDasj50UUSMO8Q.DAUABgM6eDasj50UUSMO8Q = function ()
  {
    var a = this;

    throw lwAABq9OGjCe3bHElJJ0LA('The method or operation is not implemented.');
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.ComponentModel.__Container.Remove
  type$crOifgM6eDasj50UUSMO8Q.DQUABgM6eDasj50UUSMO8Q = function (b)
  {
    var a = this;

    throw lwAABq9OGjCe3bHElJJ0LA('The method or operation is not implemented.');
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.ComponentModel.__Container.Dispose
  type$crOifgM6eDasj50UUSMO8Q.DgUABgM6eDasj50UUSMO8Q = function ()
  {
    var a = this;

    throw lwAABq9OGjCe3bHElJJ0LA('The method or operation is not implemented.');
  };

  // System.ComponentModel.IContainer
  // ScriptCoreLib.JavaScript.BCLImplementation.System.ComponentModel.__Container
  (function (i)  {
    i.fgYABq1KOTWvgk82wZ13yA = i.CwUABgM6eDasj50UUSMO8Q;
    i.fwYABq1KOTWvgk82wZ13yA = i.CgUABgM6eDasj50UUSMO8Q;
    i.gAYABq1KOTWvgk82wZ13yA = i.DAUABgM6eDasj50UUSMO8Q;
    i.gQYABq1KOTWvgk82wZ13yA = i.DQUABgM6eDasj50UUSMO8Q;
    // System.IDisposable
    i.xAAABq_bUDz_aWf_aXPRTEtLA = i.DgUABgM6eDasj50UUSMO8Q;
  }
  )(type$crOifgM6eDasj50UUSMO8Q);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.ComponentModel.__ComponentCollection
  function fKSRyufUEjqbrc6Q5O7ryw(){};
  fKSRyufUEjqbrc6Q5O7ryw.TypeName = "ComponentCollection";
  fKSRyufUEjqbrc6Q5O7ryw.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$fKSRyufUEjqbrc6Q5O7ryw = fKSRyufUEjqbrc6Q5O7ryw.prototype;
  type$fKSRyufUEjqbrc6Q5O7ryw.constructor = fKSRyufUEjqbrc6Q5O7ryw;
  var basector$fKSRyufUEjqbrc6Q5O7ryw = $ctor$(null, null, type$fKSRyufUEjqbrc6Q5O7ryw);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.ComponentModel.__ComponentCollection..ctor
  type$fKSRyufUEjqbrc6Q5O7ryw.CQUABufUEjqbrc6Q5O7ryw = function ()
  {
    var a = this;

  };
  var ctor$CQUABufUEjqbrc6Q5O7ryw = fKSRyufUEjqbrc6Q5O7ryw.ctor = $ctor$(null, 'CQUABufUEjqbrc6Q5O7ryw', type$fKSRyufUEjqbrc6Q5O7ryw);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__IntPtr
  function apo1IMfB_bTOt1XXfMkgmbQ(){};
  apo1IMfB_bTOt1XXfMkgmbQ.TypeName = "IntPtr";
  apo1IMfB_bTOt1XXfMkgmbQ.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$apo1IMfB_bTOt1XXfMkgmbQ = apo1IMfB_bTOt1XXfMkgmbQ.prototype;
  type$apo1IMfB_bTOt1XXfMkgmbQ.constructor = apo1IMfB_bTOt1XXfMkgmbQ;
  var basector$apo1IMfB_bTOt1XXfMkgmbQ = $ctor$(null, null, type$apo1IMfB_bTOt1XXfMkgmbQ);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__IntPtr..ctor
  type$apo1IMfB_bTOt1XXfMkgmbQ.CAUABsfB_bTOt1XXfMkgmbQ = function ()
  {
    var a = this;

  };
  var ctor$CAUABsfB_bTOt1XXfMkgmbQ = apo1IMfB_bTOt1XXfMkgmbQ.ctor = $ctor$(null, 'CAUABsfB_bTOt1XXfMkgmbQ', type$apo1IMfB_bTOt1XXfMkgmbQ);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__IntPtr.op_Equality
  function BAUABsfB_bTOt1XXfMkgmbQ(a, b) { return a==b };
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__IntPtr.op_Inequality
  function BQUABsfB_bTOt1XXfMkgmbQ(a, b) { return a!=b };
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__IntPtr.Equals
  type$apo1IMfB_bTOt1XXfMkgmbQ.BgUABsfB_bTOt1XXfMkgmbQ = function (b)
  {
    var a = this, c;

    c = BAUABsfB_bTOt1XXfMkgmbQ(a, b);
    return c;
  };
    apo1IMfB_bTOt1XXfMkgmbQ.prototype.AwAABnwCHD6Y1dqcmGKqIQ = apo1IMfB_bTOt1XXfMkgmbQ.prototype.BgUABsfB_bTOt1XXfMkgmbQ;

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__IntPtr.GetHashCode
  type$apo1IMfB_bTOt1XXfMkgmbQ.BwUABsfB_bTOt1XXfMkgmbQ = function ()
  {
    var a = this, b;

    b = 0;
    return b;
  };
    apo1IMfB_bTOt1XXfMkgmbQ.prototype.BwAABnwCHD6Y1dqcmGKqIQ = apo1IMfB_bTOt1XXfMkgmbQ.prototype.BwUABsfB_bTOt1XXfMkgmbQ;

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Random
  function AmDbum4UGD6K4rxXW7dcsg(){};
  AmDbum4UGD6K4rxXW7dcsg.TypeName = "Random";
  AmDbum4UGD6K4rxXW7dcsg.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$AmDbum4UGD6K4rxXW7dcsg = AmDbum4UGD6K4rxXW7dcsg.prototype;
  type$AmDbum4UGD6K4rxXW7dcsg.constructor = AmDbum4UGD6K4rxXW7dcsg;
  var basector$AmDbum4UGD6K4rxXW7dcsg = $ctor$(null, null, type$AmDbum4UGD6K4rxXW7dcsg);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Random..ctor
  type$AmDbum4UGD6K4rxXW7dcsg.__bwQABm4UGD6K4rxXW7dcsg = function ()
  {
    var a = this;

  };
  var ctor$__bwQABm4UGD6K4rxXW7dcsg = AmDbum4UGD6K4rxXW7dcsg.ctor = $ctor$(null, '__bwQABm4UGD6K4rxXW7dcsg', type$AmDbum4UGD6K4rxXW7dcsg);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Random.Next
  type$AmDbum4UGD6K4rxXW7dcsg.AAUABm4UGD6K4rxXW7dcsg = function ()
  {
    var a = this, b;

    b = Math.round((a.AwUABm4UGD6K4rxXW7dcsg() * 4294967295));
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Random.Next
  type$AmDbum4UGD6K4rxXW7dcsg.AQUABm4UGD6K4rxXW7dcsg = function (b)
  {
    var a = this, c, d;

    d = !(b < 0);

    if (!d)
    {
      throw lwAABq9OGjCe3bHElJJ0LA('ArgumentOutOfRange_MustBePositive');
    }

    c = Math.round((a.AwUABm4UGD6K4rxXW7dcsg() * b));
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Random.Next
  type$AmDbum4UGD6K4rxXW7dcsg.AgUABm4UGD6K4rxXW7dcsg = function (b, c)
  {
    var a = this, d, e;

    e = !(b > c);

    if (!e)
    {
      throw lwAABq9OGjCe3bHElJJ0LA('Argument_MinMaxValue');
    }

    d = (a.AQUABm4UGD6K4rxXW7dcsg((c - b)) + b);
    return d;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Random.NextDouble
  type$AmDbum4UGD6K4rxXW7dcsg.AwUABm4UGD6K4rxXW7dcsg = function ()
  {
    var a = this, b;

    b = Math.random();
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Activator
  function Y8eKzTmXVjiltWVkuVHBiA(){};
  Y8eKzTmXVjiltWVkuVHBiA.TypeName = "Activator";
  Y8eKzTmXVjiltWVkuVHBiA.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$Y8eKzTmXVjiltWVkuVHBiA = Y8eKzTmXVjiltWVkuVHBiA.prototype;
  type$Y8eKzTmXVjiltWVkuVHBiA.constructor = Y8eKzTmXVjiltWVkuVHBiA;
  var basector$Y8eKzTmXVjiltWVkuVHBiA = $ctor$(null, null, type$Y8eKzTmXVjiltWVkuVHBiA);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Activator..ctor
  type$Y8eKzTmXVjiltWVkuVHBiA.swMABjmXVjiltWVkuVHBiA = function ()
  {
    var a = this;

  };
  var ctor$swMABjmXVjiltWVkuVHBiA = Y8eKzTmXVjiltWVkuVHBiA.ctor = $ctor$(null, 'swMABjmXVjiltWVkuVHBiA', type$Y8eKzTmXVjiltWVkuVHBiA);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Activator.CreateInstance
  function sgMABjmXVjiltWVkuVHBiA(b)
  {
    var c, d, e, f, g;

    f = b.ihIABvTjTTG_aL4_ahSLPtTA();
    c = BBMABr5xMzijfM5xNYhyrw(f.get_Value());
    d = CBMABr5xMzijfM5xNYhyrw(BBMABr5xMzijfM5xNYhyrw(c.constructor), 'ctor');
    g = !(d == null);

    if (!g)
    {
      throw _8hEABs69FD_astU8tL1xvXQ(b.oQMABhv6ETyTub5rOKSvRA());
    }

    e = Ow4ABlsV3T_aPxZ1ZRmsarQ(d);
    return e;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__KeyValuePair`2
  function UkV3xMP0KzS_bdRyy1rdx_bg(){};
  UkV3xMP0KzS_bdRyy1rdx_bg.TypeName = "KeyValuePair_2";
  UkV3xMP0KzS_bdRyy1rdx_bg.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$UkV3xMP0KzS_bdRyy1rdx_bg = UkV3xMP0KzS_bdRyy1rdx_bg.prototype;
  type$UkV3xMP0KzS_bdRyy1rdx_bg.constructor = UkV3xMP0KzS_bdRyy1rdx_bg;
  type$UkV3xMP0KzS_bdRyy1rdx_bg._Key_k__BackingField = null;
  type$UkV3xMP0KzS_bdRyy1rdx_bg._Value_k__BackingField = null;
  var basector$UkV3xMP0KzS_bdRyy1rdx_bg = $ctor$(null, null, type$UkV3xMP0KzS_bdRyy1rdx_bg);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__KeyValuePair`2..ctor
  type$UkV3xMP0KzS_bdRyy1rdx_bg.sAMABsP0KzS_bdRyy1rdx_bg = function ()
  {
    var a = this;

  };
  var ctor$sAMABsP0KzS_bdRyy1rdx_bg = UkV3xMP0KzS_bdRyy1rdx_bg.ctor = $ctor$(null, 'sAMABsP0KzS_bdRyy1rdx_bg', type$UkV3xMP0KzS_bdRyy1rdx_bg);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__KeyValuePair`2..ctor
  type$UkV3xMP0KzS_bdRyy1rdx_bg.sQMABsP0KzS_bdRyy1rdx_bg = function (b, c)
  {
    var a = this;

    a.rQMABsP0KzS_bdRyy1rdx_bg(b);
    a.rwMABsP0KzS_bdRyy1rdx_bg(c);
  };
  var ctor$sQMABsP0KzS_bdRyy1rdx_bg = $ctor$(null, 'sQMABsP0KzS_bdRyy1rdx_bg', type$UkV3xMP0KzS_bdRyy1rdx_bg);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__KeyValuePair`2.get_Key
  type$UkV3xMP0KzS_bdRyy1rdx_bg.rAMABsP0KzS_bdRyy1rdx_bg = function ()
  {
    return this._Key_k__BackingField;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__KeyValuePair`2.set_Key
  type$UkV3xMP0KzS_bdRyy1rdx_bg.rQMABsP0KzS_bdRyy1rdx_bg = function (b)
  {
    var a = this;

    a._Key_k__BackingField = b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__KeyValuePair`2.get_Value
  type$UkV3xMP0KzS_bdRyy1rdx_bg.rgMABsP0KzS_bdRyy1rdx_bg = function ()
  {
    return this._Value_k__BackingField;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__KeyValuePair`2.set_Value
  type$UkV3xMP0KzS_bdRyy1rdx_bg.rwMABsP0KzS_bdRyy1rdx_bg = function (b)
  {
    var a = this;

    a._Value_k__BackingField = b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection.__Assembly
  function wfLcjvd3qD6XrPGZP0rKLA(){};
  wfLcjvd3qD6XrPGZP0rKLA.TypeName = "Assembly";
  wfLcjvd3qD6XrPGZP0rKLA.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$wfLcjvd3qD6XrPGZP0rKLA = wfLcjvd3qD6XrPGZP0rKLA.prototype;
  type$wfLcjvd3qD6XrPGZP0rKLA.constructor = wfLcjvd3qD6XrPGZP0rKLA;
  type$wfLcjvd3qD6XrPGZP0rKLA.__Value = null;
  var basector$wfLcjvd3qD6XrPGZP0rKLA = $ctor$(null, null, type$wfLcjvd3qD6XrPGZP0rKLA);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection.__Assembly..ctor
  type$wfLcjvd3qD6XrPGZP0rKLA.qwMABvd3qD6XrPGZP0rKLA = function ()
  {
    var a = this;

  };
  var ctor$qwMABvd3qD6XrPGZP0rKLA = wfLcjvd3qD6XrPGZP0rKLA.ctor = $ctor$(null, 'qwMABvd3qD6XrPGZP0rKLA', type$wfLcjvd3qD6XrPGZP0rKLA);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection.__Assembly.GetName
  type$wfLcjvd3qD6XrPGZP0rKLA.pgMABvd3qD6XrPGZP0rKLA = function ()
  {
    var a = this, b, c;

    b = new ctor$_2AoABm_aNFTqBCXCdcvj_azQ();
    b.__NameValue = a.__Value.Name;
    c = b;
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection.__Assembly.GetReferencedAssemblies
  type$wfLcjvd3qD6XrPGZP0rKLA.pwMABvd3qD6XrPGZP0rKLA = function ()
  {
    var a = this, b, c, d, e, f, g;

    b = a.__Value.References;
    c = new Array(b.length);

    for (d = 0; (d < b.length); d++)
    {
      e = new ctor$_2AoABm_aNFTqBCXCdcvj_azQ();
      e.__Value = b[d];
      c[d] = e;
    }

    f = c;
    return f;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection.__Assembly.Load
  function qAMABvd3qD6XrPGZP0rKLA(b)
  {
    var c, d, e, f;

    c = b;
    f = !(c.__Value == null);

    if (!f)
    {
      throw lwAABq9OGjCe3bHElJJ0LA('Cannot load this assembly');
    }

    d = new ctor$qwMABvd3qD6XrPGZP0rKLA();
    d.__Value = c.__Value;
    e = d;
    return e;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection.__Assembly.GetTypes
  type$wfLcjvd3qD6XrPGZP0rKLA.qQMABvd3qD6XrPGZP0rKLA = function ()
  {
    var a = this, b, c, d, e, f, g, h, i, j;

    b = a.__Value.Types;
    c = new Array(b.length);

    for (d = 0; (d < b.length); d++)
    {
      e = BBMABr5xMzijfM5xNYhyrw(b[d]);
      g = new ctor$nAMABrh_afT6gMvrmjMrwWw();
      g.set_Value(e.prototype);
      f = g;
      h = new ctor$lhIABvTjTTG_aL4_ahSLPtTA();
      h.ixIABvTjTTG_aL4_ahSLPtTA(oAMABrh_afT6gMvrmjMrwWw(f));
      c[d] = h;
    }

    i = c;
    return i;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection.__Assembly.get_FullName
  type$wfLcjvd3qD6XrPGZP0rKLA.qgMABvd3qD6XrPGZP0rKLA = function ()
  {
    var a = this, b;

    b = a.pgMABvd3qD6XrPGZP0rKLA().get_FullName();
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection.__AssemblyValue
  function GVZC9UAznTmriroAm2yWgg(){};
  GVZC9UAznTmriroAm2yWgg.TypeName = "__AssemblyValue";
  GVZC9UAznTmriroAm2yWgg.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$GVZC9UAznTmriroAm2yWgg = GVZC9UAznTmriroAm2yWgg.prototype;
  type$GVZC9UAznTmriroAm2yWgg.constructor = GVZC9UAznTmriroAm2yWgg;
  type$GVZC9UAznTmriroAm2yWgg.FullName = null;
  type$GVZC9UAznTmriroAm2yWgg.Types = null;
  type$GVZC9UAznTmriroAm2yWgg.References = null;
  type$GVZC9UAznTmriroAm2yWgg.Name = null;
  var basector$GVZC9UAznTmriroAm2yWgg = $ctor$(null, null, type$GVZC9UAznTmriroAm2yWgg);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection.__AssemblyValue..ctor
  type$GVZC9UAznTmriroAm2yWgg.pQMABkAznTmriroAm2yWgg = function ()
  {
    var a = this;

  };
  var ctor$pQMABkAznTmriroAm2yWgg = GVZC9UAznTmriroAm2yWgg.ctor = $ctor$(null, 'pQMABkAznTmriroAm2yWgg', type$GVZC9UAznTmriroAm2yWgg);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection.__MemberInfo
  function rIWe5xv6ETyTub5rOKSvRA(){};
  rIWe5xv6ETyTub5rOKSvRA.TypeName = "MemberInfo";
  rIWe5xv6ETyTub5rOKSvRA.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$rIWe5xv6ETyTub5rOKSvRA = rIWe5xv6ETyTub5rOKSvRA.prototype;
  type$rIWe5xv6ETyTub5rOKSvRA.constructor = rIWe5xv6ETyTub5rOKSvRA;
  var basector$rIWe5xv6ETyTub5rOKSvRA = $ctor$(null, null, type$rIWe5xv6ETyTub5rOKSvRA);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection.__MemberInfo..ctor
  type$rIWe5xv6ETyTub5rOKSvRA.pAMABhv6ETyTub5rOKSvRA = function ()
  {
    var a = this;

  };
  var ctor$pAMABhv6ETyTub5rOKSvRA = rIWe5xv6ETyTub5rOKSvRA.ctor = $ctor$(null, 'pAMABhv6ETyTub5rOKSvRA', type$rIWe5xv6ETyTub5rOKSvRA);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection.__MemberInfo.get_Name
  type$rIWe5xv6ETyTub5rOKSvRA.oQMABhv6ETyTub5rOKSvRA = function ()
  {
/* abstract */  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection.__MemberInfo.GetCustomAttributes
  type$rIWe5xv6ETyTub5rOKSvRA.ogMABhv6ETyTub5rOKSvRA = function (b, c)
  {
/* abstract */  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection.__MemberInfo.GetCustomAttributes
  type$rIWe5xv6ETyTub5rOKSvRA.owMABhv6ETyTub5rOKSvRA = function (b)
  {
/* abstract */  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection.__FieldInfo
  function SyVYHNtm1DmvK1RUasoEIQ(){};
  SyVYHNtm1DmvK1RUasoEIQ.TypeName = "FieldInfo";
  SyVYHNtm1DmvK1RUasoEIQ.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$SyVYHNtm1DmvK1RUasoEIQ = SyVYHNtm1DmvK1RUasoEIQ.prototype = new rIWe5xv6ETyTub5rOKSvRA();
  type$SyVYHNtm1DmvK1RUasoEIQ.constructor = SyVYHNtm1DmvK1RUasoEIQ;
  type$SyVYHNtm1DmvK1RUasoEIQ._Name = null;
  var basector$SyVYHNtm1DmvK1RUasoEIQ = $ctor$(basector$rIWe5xv6ETyTub5rOKSvRA, null, type$SyVYHNtm1DmvK1RUasoEIQ);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection.__FieldInfo..ctor
  type$SyVYHNtm1DmvK1RUasoEIQ._4ggABttm1DmvK1RUasoEIQ = function ()
  {
    var a = this;

    a.pAMABhv6ETyTub5rOKSvRA();
  };
  var ctor$_4ggABttm1DmvK1RUasoEIQ = SyVYHNtm1DmvK1RUasoEIQ.ctor = $ctor$(basector$rIWe5xv6ETyTub5rOKSvRA, '_4ggABttm1DmvK1RUasoEIQ', type$SyVYHNtm1DmvK1RUasoEIQ);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection.__FieldInfo.get_Name
  type$SyVYHNtm1DmvK1RUasoEIQ._3AgABttm1DmvK1RUasoEIQ = function ()
  {
    var a = this, b;

    b = a._Name;
    return b;
  };
    SyVYHNtm1DmvK1RUasoEIQ.prototype.oQMABhv6ETyTub5rOKSvRA = SyVYHNtm1DmvK1RUasoEIQ.prototype._3AgABttm1DmvK1RUasoEIQ;

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection.__FieldInfo.GetValue
  type$SyVYHNtm1DmvK1RUasoEIQ._3QgABttm1DmvK1RUasoEIQ = function (b)
  {
    var a = this, c;

    c = BhMABr5xMzijfM5xNYhyrw(b, a._Name);
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection.__FieldInfo.SetValue
  type$SyVYHNtm1DmvK1RUasoEIQ._3ggABttm1DmvK1RUasoEIQ = function (b, c)
  {
    var a = this;

    BxMABr5xMzijfM5xNYhyrw(b, a._Name, c);
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection.__FieldInfo.op_Implicit
  function _3wgABttm1DmvK1RUasoEIQ(b)
  {
    var c;

    c = b;
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection.__FieldInfo.GetCustomAttributes
  type$SyVYHNtm1DmvK1RUasoEIQ._4AgABttm1DmvK1RUasoEIQ = function (b)
  {
    var a = this;

    throw nAAABq584TSQo69VDcfM9Q();
  };
    SyVYHNtm1DmvK1RUasoEIQ.prototype.owMABhv6ETyTub5rOKSvRA = SyVYHNtm1DmvK1RUasoEIQ.prototype._4AgABttm1DmvK1RUasoEIQ;

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection.__FieldInfo.GetCustomAttributes
  type$SyVYHNtm1DmvK1RUasoEIQ._4QgABttm1DmvK1RUasoEIQ = function (b, c)
  {
    var a = this;

    throw nAAABq584TSQo69VDcfM9Q();
  };
    SyVYHNtm1DmvK1RUasoEIQ.prototype.ogMABhv6ETyTub5rOKSvRA = SyVYHNtm1DmvK1RUasoEIQ.prototype._4QgABttm1DmvK1RUasoEIQ;

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__RuntimeTypeHandle
  function bluEd7h_afT6gMvrmjMrwWw(){};
  bluEd7h_afT6gMvrmjMrwWw.TypeName = "RuntimeTypeHandle";
  bluEd7h_afT6gMvrmjMrwWw.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$bluEd7h_afT6gMvrmjMrwWw = bluEd7h_afT6gMvrmjMrwWw.prototype;
  type$bluEd7h_afT6gMvrmjMrwWw.constructor = bluEd7h_afT6gMvrmjMrwWw;
  type$bluEd7h_afT6gMvrmjMrwWw._Value = null;
  var basector$bluEd7h_afT6gMvrmjMrwWw = $ctor$(null, null, type$bluEd7h_afT6gMvrmjMrwWw);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__RuntimeTypeHandle..ctor
  type$bluEd7h_afT6gMvrmjMrwWw.nAMABrh_afT6gMvrmjMrwWw = function ()
  {
    var a = this;

  };
  var ctor$nAMABrh_afT6gMvrmjMrwWw = bluEd7h_afT6gMvrmjMrwWw.ctor = $ctor$(null, 'nAMABrh_afT6gMvrmjMrwWw', type$bluEd7h_afT6gMvrmjMrwWw);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__RuntimeTypeHandle..ctor
  type$bluEd7h_afT6gMvrmjMrwWw.nQMABrh_afT6gMvrmjMrwWw = function (b)
  {
    var a = this;

    a._Value = b;
  };
  var ctor$nQMABrh_afT6gMvrmjMrwWw = $ctor$(null, 'nQMABrh_afT6gMvrmjMrwWw', type$bluEd7h_afT6gMvrmjMrwWw);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__RuntimeTypeHandle.get_Value
  type$bluEd7h_afT6gMvrmjMrwWw.get_Value = function ()
  {
    var a = this, b;

    b = a._Value;
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__RuntimeTypeHandle.set_Value
  type$bluEd7h_afT6gMvrmjMrwWw.set_Value = function (b)
  {
    var a = this;

    a._Value = b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__RuntimeTypeHandle.op_Implicit
  function oAMABrh_afT6gMvrmjMrwWw(b)
  {
    var c;

    c = b;
    return c;
  };

  // ScriptCoreLib.JavaScript.Runtime.Timer
  function VqxmIClzizCnsZekUeVmgA(){};
  VqxmIClzizCnsZekUeVmgA.TypeName = "Timer";
  VqxmIClzizCnsZekUeVmgA.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$VqxmIClzizCnsZekUeVmgA = VqxmIClzizCnsZekUeVmgA.prototype;
  type$VqxmIClzizCnsZekUeVmgA.constructor = VqxmIClzizCnsZekUeVmgA;
  type$VqxmIClzizCnsZekUeVmgA.Tick = null;
  type$VqxmIClzizCnsZekUeVmgA.id = 0;
  type$VqxmIClzizCnsZekUeVmgA.isTimeout = false;
  type$VqxmIClzizCnsZekUeVmgA.isInterval = false;
  type$VqxmIClzizCnsZekUeVmgA.Counter = 0;
  type$VqxmIClzizCnsZekUeVmgA.Step = 0;
  type$VqxmIClzizCnsZekUeVmgA.TimeToLive = 0;
  type$VqxmIClzizCnsZekUeVmgA.Enabled = false;
  var basector$VqxmIClzizCnsZekUeVmgA = $ctor$(null, null, type$VqxmIClzizCnsZekUeVmgA);
  // ScriptCoreLib.JavaScript.Runtime.Timer..ctor
  type$VqxmIClzizCnsZekUeVmgA._1QIABilzizCnsZekUeVmgA = function ()
  {
    var a = this;

    a.Step = 1;
    a.TimeToLive = 0;
    a.Enabled = 1;
  };
  var ctor$_1QIABilzizCnsZekUeVmgA = VqxmIClzizCnsZekUeVmgA.ctor = $ctor$(null, '_1QIABilzizCnsZekUeVmgA', type$VqxmIClzizCnsZekUeVmgA);

  // ScriptCoreLib.JavaScript.Runtime.Timer..ctor
  type$VqxmIClzizCnsZekUeVmgA._1gIABilzizCnsZekUeVmgA = function (b)
  {
    var a = this;

    a.Step = 1;
    a.TimeToLive = 0;
    a.Enabled = 1;
    a.Tick = fwsABqgPxjmDnjkmC_a5nbw(a.Tick, b);
  };
  var ctor$_1gIABilzizCnsZekUeVmgA = $ctor$(null, '_1gIABilzizCnsZekUeVmgA', type$VqxmIClzizCnsZekUeVmgA);

  // ScriptCoreLib.JavaScript.Runtime.Timer..ctor
  type$VqxmIClzizCnsZekUeVmgA._1wIABilzizCnsZekUeVmgA = function (b, c, d)
  {
    var a = this, e, f, g;

    e = null;
    f = /* DOMCreateType */new XBYWZfIxUjayT78g3b_aPsA();
    f.interval = d;
    a.Step = 1;
    a.TimeToLive = 0;
    a.Enabled = 1;
    f.__4__this = a;
    a.Tick = fwsABqgPxjmDnjkmC_a5nbw(a.Tick, b);
    g = !(c > 0);

    if (!g)
    {

      if (!e)
      {
        e = new ctor$LQsABharvz6dEOdzk7hI5Q(f, '__ctor_b__0');
      }

      QxEABtpeljmp_berMhF6qtQ(window, e, c);
    }
    else
    {
      g = !(f.interval > 0);

      if (!g)
      {
        a._3QIABilzizCnsZekUeVmgA(f.interval);
      }
      else
      {
        a._2QIABilzizCnsZekUeVmgA();
      }

    }

  };
  var ctor$_1wIABilzizCnsZekUeVmgA = $ctor$(null, '_1wIABilzizCnsZekUeVmgA', type$VqxmIClzizCnsZekUeVmgA);

  // ScriptCoreLib.JavaScript.Runtime.Timer.add_Tick
  type$VqxmIClzizCnsZekUeVmgA._0wIABilzizCnsZekUeVmgA = function (b)
  {
    var a = this;

    a.Tick = fwsABqgPxjmDnjkmC_a5nbw(a.Tick, b);
  };

  // ScriptCoreLib.JavaScript.Runtime.Timer.remove_Tick
  type$VqxmIClzizCnsZekUeVmgA._1AIABilzizCnsZekUeVmgA = function (b)
  {
    var a = this;

    a.Tick = gQsABqgPxjmDnjkmC_a5nbw(a.Tick, b);
  };

  // ScriptCoreLib.JavaScript.Runtime.Timer.get_TimeToLiveExceeded
  type$VqxmIClzizCnsZekUeVmgA._2AIABilzizCnsZekUeVmgA = function ()
  {
    var a = this, b;

    b = ((a.TimeToLive <= 0) ? 0 : (a.Counter > a.TimeToLive));
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.Timer.Invoke
  type$VqxmIClzizCnsZekUeVmgA._2QIABilzizCnsZekUeVmgA = function ()
  {
    var a = this, b;

    b = !a.Enabled;

    if (!b)
    {
      sQgABrDT9zCTidQoMa3Dig(a.Tick, a);
      a.Counter = (a.Counter + a.Step);
      b = !a._2AIABilzizCnsZekUeVmgA();

      if (!b)
      {
        a._4QIABilzizCnsZekUeVmgA();
      }

    }

  };

  // ScriptCoreLib.JavaScript.Runtime.Timer.Interval
  function _2gIABilzizCnsZekUeVmgA(b, c)
  {
    var d, e;

    d = new ctor$_1QIABilzizCnsZekUeVmgA();
    d.Tick = fwsABqgPxjmDnjkmC_a5nbw(d.Tick, b);
    d._3QIABilzizCnsZekUeVmgA(c);
    e = d;
    return e;
  };

  // ScriptCoreLib.JavaScript.Runtime.Timer.StartInterval
  type$VqxmIClzizCnsZekUeVmgA._2wIABilzizCnsZekUeVmgA = function (b, c)
  {
    var a = this;

    a.TimeToLive = c;
    a._3QIABilzizCnsZekUeVmgA(b);
  };

  // ScriptCoreLib.JavaScript.Runtime.Timer.StartInterval
  type$VqxmIClzizCnsZekUeVmgA._3AIABilzizCnsZekUeVmgA = function ()
  {
    var a = this;

    a._3QIABilzizCnsZekUeVmgA(300);
  };

  // ScriptCoreLib.JavaScript.Runtime.Timer.StartInterval
  type$VqxmIClzizCnsZekUeVmgA._3QIABilzizCnsZekUeVmgA = function (b)
  {
    var a = this;

    a._4QIABilzizCnsZekUeVmgA();
    a.isInterval = 1;
    a.id = RhEABtpeljmp_berMhF6qtQ(window, new ctor$LQsABharvz6dEOdzk7hI5Q(a, '_2QIABilzizCnsZekUeVmgA'), b);
  };

  // ScriptCoreLib.JavaScript.Runtime.Timer.StartTimeout
  type$VqxmIClzizCnsZekUeVmgA._3gIABilzizCnsZekUeVmgA = function ()
  {
    var a = this;

    a._3wIABilzizCnsZekUeVmgA(300);
  };

  // ScriptCoreLib.JavaScript.Runtime.Timer.StartTimeout
  type$VqxmIClzizCnsZekUeVmgA._3wIABilzizCnsZekUeVmgA = function (b)
  {
    var a = this;

    a._4QIABilzizCnsZekUeVmgA();
    a.isTimeout = 1;
    a.id = QxEABtpeljmp_berMhF6qtQ(window, new ctor$LQsABharvz6dEOdzk7hI5Q(a, '_2QIABilzizCnsZekUeVmgA'), b);
  };

  // ScriptCoreLib.JavaScript.Runtime.Timer.get_IsAlive
  type$VqxmIClzizCnsZekUeVmgA._4AIABilzizCnsZekUeVmgA = function ()
  {
    var a = this, b;

    b = !!a.id;
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.Timer.Stop
  type$VqxmIClzizCnsZekUeVmgA._4QIABilzizCnsZekUeVmgA = function ()
  {
    var a = this, b;

    b = !a.isTimeout;

    if (!b)
    {
      window.clearTimeout(a.id);
    }

    b = !a.isInterval;

    if (!b)
    {
      window.clearInterval(a.id);
    }

    a.isInterval = 0;
    a.isTimeout = 0;
    a.id = 0;
    a.Counter = 0;
  };

  // ScriptCoreLib.JavaScript.Runtime.Timer.Do
  function _4gIABilzizCnsZekUeVmgA(b, c, d)
  {
    var e;

    e = /* DOMCreateType */new w9DU74gzczKRXuw3d3noSw();
    e.dx = b;
    new ctor$_1wIABilzizCnsZekUeVmgA(new ctor$KQsABoVjnjetV_aXG4GFkBQ(e, '_Do_b__4'), c, d);
  };

  // ScriptCoreLib.JavaScript.Runtime.Timer.DoAsync
  function _4wIABilzizCnsZekUeVmgA(b)
  {
    var c;

    c = /* DOMCreateType */new JeHOowsSMTebt9kMJXYf5w();
    c.h = b;
    new ctor$_1wIABilzizCnsZekUeVmgA(new ctor$KQsABoVjnjetV_aXG4GFkBQ(c, '_DoAsync_b__7'), 1, 0);
  };

  // ScriptCoreLib.JavaScript.Runtime.Timer.Trigger
  function _5AIABilzizCnsZekUeVmgA(b, c)
  {
    var d, e, f;

    e = /* DOMCreateType */new baR6i9v8HDWWH5n0EvcT5Q();
    e.p = b;
    e.h = c;
    e.timer = null;
    d = new ctor$KQsABoVjnjetV_aXG4GFkBQ(e, '_Trigger_b__a');
    e.timer = new ctor$_1wIABilzizCnsZekUeVmgA(d, 100, 100);
    f = e.timer;
    return f;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__EventArgs
  function NumX0sMIBDafRMsBisUoIQ(){};
  NumX0sMIBDafRMsBisUoIQ.TypeName = "EventArgs";
  NumX0sMIBDafRMsBisUoIQ.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$NumX0sMIBDafRMsBisUoIQ = NumX0sMIBDafRMsBisUoIQ.prototype;
  type$NumX0sMIBDafRMsBisUoIQ.constructor = NumX0sMIBDafRMsBisUoIQ;
  var mQEABMMIBDafRMsBisUoIQ = null;
  var basector$NumX0sMIBDafRMsBisUoIQ = $ctor$(null, null, type$NumX0sMIBDafRMsBisUoIQ);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__EventArgs..ctor
  type$NumX0sMIBDafRMsBisUoIQ._5wEABsMIBDafRMsBisUoIQ = function ()
  {
    var a = this;

  };
  var ctor$_5wEABsMIBDafRMsBisUoIQ = NumX0sMIBDafRMsBisUoIQ.ctor = $ctor$(null, '_5wEABsMIBDafRMsBisUoIQ', type$NumX0sMIBDafRMsBisUoIQ);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__EventArgs.op_Implicit
  function _5QEABsMIBDafRMsBisUoIQ(b)
  {
    var c;

    c = b;
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__EventArgs.op_Implicit
  function _5gEABsMIBDafRMsBisUoIQ(b)
  {
    var c;

    c = b;
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.ComponentModel.__ListChangedEventArgs
  function __b56JeU8ScD2xMP0_bk23JjQ(){};
  __b56JeU8ScD2xMP0_bk23JjQ.TypeName = "ListChangedEventArgs";
  __b56JeU8ScD2xMP0_bk23JjQ.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$__b56JeU8ScD2xMP0_bk23JjQ = __b56JeU8ScD2xMP0_bk23JjQ.prototype = new NumX0sMIBDafRMsBisUoIQ();
  type$__b56JeU8ScD2xMP0_bk23JjQ.constructor = __b56JeU8ScD2xMP0_bk23JjQ;
  type$__b56JeU8ScD2xMP0_bk23JjQ.listChangedType = 0;
  type$__b56JeU8ScD2xMP0_bk23JjQ.newIndex = 0;
  type$__b56JeU8ScD2xMP0_bk23JjQ.oldIndex = 0;
  var basector$__b56JeU8ScD2xMP0_bk23JjQ = $ctor$(basector$NumX0sMIBDafRMsBisUoIQ, null, type$__b56JeU8ScD2xMP0_bk23JjQ);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.ComponentModel.__ListChangedEventArgs..ctor
  type$__b56JeU8ScD2xMP0_bk23JjQ._6wEABk8ScD2xMP0_bk23JjQ = function (b, c)
  {
    var a = this;

    a._5wEABsMIBDafRMsBisUoIQ();
    a.listChangedType = b;
    a.newIndex = c;
    a.oldIndex = -1;
  };
  var ctor$_6wEABk8ScD2xMP0_bk23JjQ = $ctor$(basector$NumX0sMIBDafRMsBisUoIQ, '_6wEABk8ScD2xMP0_bk23JjQ', type$__b56JeU8ScD2xMP0_bk23JjQ);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.ComponentModel.__ListChangedEventArgs..ctor
  type$__b56JeU8ScD2xMP0_bk23JjQ._7AEABk8ScD2xMP0_bk23JjQ = function (b, c, d)
  {
    var a = this;

    a._5wEABsMIBDafRMsBisUoIQ();
    a.listChangedType = b;
    a.newIndex = c;
    a.oldIndex = d;
  };
  var ctor$_7AEABk8ScD2xMP0_bk23JjQ = $ctor$(basector$NumX0sMIBDafRMsBisUoIQ, '_7AEABk8ScD2xMP0_bk23JjQ', type$__b56JeU8ScD2xMP0_bk23JjQ);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.ComponentModel.__ListChangedEventArgs.get_ListChangedType
  type$__b56JeU8ScD2xMP0_bk23JjQ._6AEABk8ScD2xMP0_bk23JjQ = function ()
  {
    var a = this, b;

    b = a.listChangedType;
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.ComponentModel.__ListChangedEventArgs.get_NewIndex
  type$__b56JeU8ScD2xMP0_bk23JjQ._6QEABk8ScD2xMP0_bk23JjQ = function ()
  {
    var a = this, b;

    b = a.newIndex;
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.ComponentModel.__ListChangedEventArgs.get_OldIndex
  type$__b56JeU8ScD2xMP0_bk23JjQ._6gEABk8ScD2xMP0_bk23JjQ = function ()
  {
    var a = this, b;

    b = a.oldIndex;
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__Stream
  function LsjGOFMF5jCv0uAularNmw(){};
  LsjGOFMF5jCv0uAularNmw.TypeName = "Stream";
  LsjGOFMF5jCv0uAularNmw.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$LsjGOFMF5jCv0uAularNmw = LsjGOFMF5jCv0uAularNmw.prototype;
  type$LsjGOFMF5jCv0uAularNmw.constructor = LsjGOFMF5jCv0uAularNmw;
  var basector$LsjGOFMF5jCv0uAularNmw = $ctor$(null, null, type$LsjGOFMF5jCv0uAularNmw);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__Stream..ctor
  type$LsjGOFMF5jCv0uAularNmw.jQEABlMF5jCv0uAularNmw = function ()
  {
    var a = this;

  };
  var ctor$jQEABlMF5jCv0uAularNmw = LsjGOFMF5jCv0uAularNmw.ctor = $ctor$(null, 'jQEABlMF5jCv0uAularNmw', type$LsjGOFMF5jCv0uAularNmw);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__Stream.Dispose
  type$LsjGOFMF5jCv0uAularNmw.hQEABlMF5jCv0uAularNmw = function ()
  {
    var a = this;

  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__Stream.Read
  type$LsjGOFMF5jCv0uAularNmw.hgEABlMF5jCv0uAularNmw = function (b, c, d)
  {
/* abstract */  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__Stream.ReadByte
  type$LsjGOFMF5jCv0uAularNmw.hwEABlMF5jCv0uAularNmw = function ()
  {
    var a = this, b, c;

    b = [
      0
    ];
    a.hgEABlMF5jCv0uAularNmw(b, 0, 1);
    c = (b[0] & 255);
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__Stream.Write
  type$LsjGOFMF5jCv0uAularNmw.iAEABlMF5jCv0uAularNmw = function (b, c, d)
  {
/* abstract */  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__Stream.WriteByte
  type$LsjGOFMF5jCv0uAularNmw.iQEABlMF5jCv0uAularNmw = function (b)
  {
    var a = this, c, d;

    c = (b & 255);
    d = [
      c
    ];
    a.iAEABlMF5jCv0uAularNmw(d, 0, 1);
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__Stream.get_Length
  type$LsjGOFMF5jCv0uAularNmw.igEABlMF5jCv0uAularNmw = function ()
  {
/* abstract */  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__Stream.get_Position
  type$LsjGOFMF5jCv0uAularNmw.iwEABlMF5jCv0uAularNmw = function ()
  {
/* abstract */  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__Stream.set_Position
  type$LsjGOFMF5jCv0uAularNmw.jAEABlMF5jCv0uAularNmw = function (b)
  {
/* abstract */  };

  // System.IDisposable
  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__Stream
  (function (i)  {
    i.xAAABq_bUDz_aWf_aXPRTEtLA = i.hQEABlMF5jCv0uAularNmw;
  }
  )(type$LsjGOFMF5jCv0uAularNmw);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__MemoryStream
  function __buKZpkodmziAk5_bSoww_a8g(){};
  __buKZpkodmziAk5_bSoww_a8g.TypeName = "MemoryStream";
  __buKZpkodmziAk5_bSoww_a8g.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$__buKZpkodmziAk5_bSoww_a8g = __buKZpkodmziAk5_bSoww_a8g.prototype = new LsjGOFMF5jCv0uAularNmw();
  type$__buKZpkodmziAk5_bSoww_a8g.constructor = __buKZpkodmziAk5_bSoww_a8g;
  type$__buKZpkodmziAk5_bSoww_a8g.Buffer = null;
  type$__buKZpkodmziAk5_bSoww_a8g._Position_k__BackingField = 0;
  var basector$__buKZpkodmziAk5_bSoww_a8g = $ctor$(basector$LsjGOFMF5jCv0uAularNmw, null, type$__buKZpkodmziAk5_bSoww_a8g);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__MemoryStream..ctor
  type$__buKZpkodmziAk5_bSoww_a8g.jgEABkodmziAk5_bSoww_a8g = function ()
  {
    var a = this;

    a.jwEABkodmziAk5_bSoww_a8g(null);
  };
  var ctor$jgEABkodmziAk5_bSoww_a8g = __buKZpkodmziAk5_bSoww_a8g.ctor = $ctor$(basector$LsjGOFMF5jCv0uAularNmw, 'jgEABkodmziAk5_bSoww_a8g', type$__buKZpkodmziAk5_bSoww_a8g);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__MemoryStream..ctor
  type$__buKZpkodmziAk5_bSoww_a8g.jwEABkodmziAk5_bSoww_a8g = function (b)
  {
    var a = this, c;

    a.Buffer = '';
    a.jQEABlMF5jCv0uAularNmw();
    c = (b == null);

    if (!c)
    {
      a.iAEABlMF5jCv0uAularNmw(b, 0, b.length);
      a.jAEABlMF5jCv0uAularNmw(0);
    }

  };
  var ctor$jwEABkodmziAk5_bSoww_a8g = $ctor$(basector$LsjGOFMF5jCv0uAularNmw, 'jwEABkodmziAk5_bSoww_a8g', type$__buKZpkodmziAk5_bSoww_a8g);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__MemoryStream.ReadByte
  type$__buKZpkodmziAk5_bSoww_a8g.kAEABkodmziAk5_bSoww_a8g = function ()
  {
    var a = this, b, c, d;

    d = !(a.iwEABlMF5jCv0uAularNmw() < 0);

    if (!d)
    {
      c = -1;
      return c;
    }

    d = (a.iwEABlMF5jCv0uAularNmw() < a.igEABlMF5jCv0uAularNmw());

    if (!d)
    {
      c = -1;
      return c;
    }

    b = (eRMABjDeCj_aRJzaBmU9SJg(a.Buffer, a.iwEABlMF5jCv0uAularNmw()) & 255);
    a.jAEABlMF5jCv0uAularNmw((a.iwEABlMF5jCv0uAularNmw() + 1));
    c = b;
    return c;
  };
    __buKZpkodmziAk5_bSoww_a8g.prototype.hwEABlMF5jCv0uAularNmw = __buKZpkodmziAk5_bSoww_a8g.prototype.kAEABkodmziAk5_bSoww_a8g;

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__MemoryStream.WriteByte
  type$__buKZpkodmziAk5_bSoww_a8g.kQEABkodmziAk5_bSoww_a8g = function (b)
  {
    var a = this, c;

    c = !(a.iwEABlMF5jCv0uAularNmw() < a.igEABlMF5jCv0uAularNmw());

    if (!c)
    {
      throw nAAABq584TSQo69VDcfM9Q();
    }

    a.Buffer = gBMABjDeCj_aRJzaBmU9SJg(a.Buffer, bRMABjDeCj_aRJzaBmU9SJg((b & 255)));
    a.jAEABlMF5jCv0uAularNmw((a.iwEABlMF5jCv0uAularNmw() + 1));
  };
    __buKZpkodmziAk5_bSoww_a8g.prototype.iQEABlMF5jCv0uAularNmw = __buKZpkodmziAk5_bSoww_a8g.prototype.kQEABkodmziAk5_bSoww_a8g;

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__MemoryStream.Read
  type$__buKZpkodmziAk5_bSoww_a8g.kgEABkodmziAk5_bSoww_a8g = function (b, c, d)
  {
    var a = this, e, f, g, h, i;

    e = 0;
    f = a.iwEABlMF5jCv0uAularNmw();

    for (g = 0; (g < d); g++)
    {
      i = (g < a.igEABlMF5jCv0uAularNmw());

      if (!i)
      {
        break;
      }

      b[(g + c)] = (eRMABjDeCj_aRJzaBmU9SJg(a.Buffer, (g + f)) & 255);
      e++;
    }

    a.jAEABlMF5jCv0uAularNmw((a.iwEABlMF5jCv0uAularNmw() + e));
    h = e;
    return h;
  };
    __buKZpkodmziAk5_bSoww_a8g.prototype.hgEABlMF5jCv0uAularNmw = __buKZpkodmziAk5_bSoww_a8g.prototype.kgEABkodmziAk5_bSoww_a8g;

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__MemoryStream.Write
  type$__buKZpkodmziAk5_bSoww_a8g.kwEABkodmziAk5_bSoww_a8g = function (b, c, d)
  {
    var a = this, e, f;

    f = !(a.iwEABlMF5jCv0uAularNmw() < a.igEABlMF5jCv0uAularNmw());

    if (!f)
    {
      throw nAAABq584TSQo69VDcfM9Q();
    }


    for (e = 0; (e < d); e++)
    {
      a.Buffer = gBMABjDeCj_aRJzaBmU9SJg(a.Buffer, bRMABjDeCj_aRJzaBmU9SJg(b[(c + e)]));
    }

    a.jAEABlMF5jCv0uAularNmw((a.iwEABlMF5jCv0uAularNmw() + d));
  };
    __buKZpkodmziAk5_bSoww_a8g.prototype.iAEABlMF5jCv0uAularNmw = __buKZpkodmziAk5_bSoww_a8g.prototype.kwEABkodmziAk5_bSoww_a8g;

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__MemoryStream.ToArray
  type$__buKZpkodmziAk5_bSoww_a8g.lAEABkodmziAk5_bSoww_a8g = function ()
  {
    var a = this, b, c, d, e;

    b = new Array(a.igEABlMF5jCv0uAularNmw());

    for (c = 0; (c < a.igEABlMF5jCv0uAularNmw()); c++)
    {
      b[c] = (eRMABjDeCj_aRJzaBmU9SJg(a.Buffer, c) & 255);
    }

    d = b;
    return d;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__MemoryStream.get_Length
  type$__buKZpkodmziAk5_bSoww_a8g.lQEABkodmziAk5_bSoww_a8g = function ()
  {
    var a = this, b;

    b = eBMABjDeCj_aRJzaBmU9SJg(a.Buffer);
    return b;
  };
    __buKZpkodmziAk5_bSoww_a8g.prototype.igEABlMF5jCv0uAularNmw = __buKZpkodmziAk5_bSoww_a8g.prototype.lQEABkodmziAk5_bSoww_a8g;

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__MemoryStream.get_Position
  type$__buKZpkodmziAk5_bSoww_a8g.lgEABkodmziAk5_bSoww_a8g = function ()
  {
    return this._Position_k__BackingField;
  };
    __buKZpkodmziAk5_bSoww_a8g.prototype.iwEABlMF5jCv0uAularNmw = __buKZpkodmziAk5_bSoww_a8g.prototype.lgEABkodmziAk5_bSoww_a8g;

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__MemoryStream.set_Position
  type$__buKZpkodmziAk5_bSoww_a8g.lwEABkodmziAk5_bSoww_a8g = function (b)
  {
    var a = this;

    a._Position_k__BackingField = b;
  };
    __buKZpkodmziAk5_bSoww_a8g.prototype.jAEABlMF5jCv0uAularNmw = __buKZpkodmziAk5_bSoww_a8g.prototype.lwEABkodmziAk5_bSoww_a8g;

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__MemoryStream.WriteTo
  type$__buKZpkodmziAk5_bSoww_a8g.mAEABkodmziAk5_bSoww_a8g = function (b)
  {
    var a = this;

    throw _8REABs69FD_astU8tL1xvXQ();
  };

  // System.IDisposable
  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__MemoryStream
  (function (i)  {
    i.xAAABq_bUDz_aWf_aXPRTEtLA = i.hQEABlMF5jCv0uAularNmw;
  }
  )(type$__buKZpkodmziAk5_bSoww_a8g);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__List`1+__Enumerator
  function Eum5pOT_aVDKnn40FH1TlGg(){};
  Eum5pOT_aVDKnn40FH1TlGg.TypeName = "Enumerator";
  Eum5pOT_aVDKnn40FH1TlGg.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$Eum5pOT_aVDKnn40FH1TlGg = Eum5pOT_aVDKnn40FH1TlGg.prototype;
  type$Eum5pOT_aVDKnn40FH1TlGg.constructor = Eum5pOT_aVDKnn40FH1TlGg;
  type$Eum5pOT_aVDKnn40FH1TlGg.value = null;
  var basector$Eum5pOT_aVDKnn40FH1TlGg = $ctor$(null, null, type$Eum5pOT_aVDKnn40FH1TlGg);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__List`1+__Enumerator..ctor
  type$Eum5pOT_aVDKnn40FH1TlGg.ygAABuT_aVDKnn40FH1TlGg = function (b)
  {
    var a = this;

    a.value = ChQABi2cDDCKLtCQWJ9PkA(b.tgAABnGXyTaWJhb6CcyWQQ()).NgEABnMeWzaNooAKOmFm5g();
  };
  var ctor$ygAABuT_aVDKnn40FH1TlGg = $ctor$(null, 'ygAABuT_aVDKnn40FH1TlGg', type$Eum5pOT_aVDKnn40FH1TlGg);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__List`1+__Enumerator.get_Current
  type$Eum5pOT_aVDKnn40FH1TlGg.ywAABuT_aVDKnn40FH1TlGg = function ()
  {
    var a = this, b;

    b = a.value.xQAABrYmRzSu_anO2U_bk1MA();
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__List`1+__Enumerator.Dispose
  type$Eum5pOT_aVDKnn40FH1TlGg.zAAABuT_aVDKnn40FH1TlGg = function ()
  {
    var a = this;

    a.value.xAAABq_bUDz_aWf_aXPRTEtLA();
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__List`1+__Enumerator.System.Collections.IEnumerator.get_Current
  type$Eum5pOT_aVDKnn40FH1TlGg.zQAABuT_aVDKnn40FH1TlGg = function ()
  {
    var a = this, b;

    b = a.value.xQAABrYmRzSu_anO2U_bk1MA();
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__List`1+__Enumerator.MoveNext
  type$Eum5pOT_aVDKnn40FH1TlGg.zgAABuT_aVDKnn40FH1TlGg = function ()
  {
    var a = this, b;

    b = a.value.qAAABu7N0xGI6ACQJ1TEOg();
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__List`1+__Enumerator.Reset
  type$Eum5pOT_aVDKnn40FH1TlGg.zwAABuT_aVDKnn40FH1TlGg = function ()
  {
    var a = this;

    a.value.qgAABu7N0xGI6ACQJ1TEOg();
  };

  // 
  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__List`1+__Enumerator
  (function (i)  {
    i.xQAABrYmRzSu_anO2U_bk1MA = i.ywAABuT_aVDKnn40FH1TlGg;
    // System.IDisposable
    i.xAAABq_bUDz_aWf_aXPRTEtLA = i.zAAABuT_aVDKnn40FH1TlGg;
    // System.Collections.IEnumerator
    i.qAAABu7N0xGI6ACQJ1TEOg = i.zgAABuT_aVDKnn40FH1TlGg;
    i.qQAABu7N0xGI6ACQJ1TEOg = i.zQAABuT_aVDKnn40FH1TlGg;
    i.qgAABu7N0xGI6ACQJ1TEOg = i.zwAABuT_aVDKnn40FH1TlGg;
  }
  )(type$Eum5pOT_aVDKnn40FH1TlGg);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__List`1
  function wljBXXGXyTaWJhb6CcyWQQ(){};
  wljBXXGXyTaWJhb6CcyWQQ.TypeName = "List_1";
  wljBXXGXyTaWJhb6CcyWQQ.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$wljBXXGXyTaWJhb6CcyWQQ = wljBXXGXyTaWJhb6CcyWQQ.prototype;
  type$wljBXXGXyTaWJhb6CcyWQQ.constructor = wljBXXGXyTaWJhb6CcyWQQ;
  type$wljBXXGXyTaWJhb6CcyWQQ._items = null;
  var basector$wljBXXGXyTaWJhb6CcyWQQ = $ctor$(null, null, type$wljBXXGXyTaWJhb6CcyWQQ);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__List`1..ctor
  type$wljBXXGXyTaWJhb6CcyWQQ.swAABnGXyTaWJhb6CcyWQQ = function ()
  {
    var a = this;

    a._items = Hw4ABqPHMD_aEoYb_aoNHL9w();
  };
  var ctor$swAABnGXyTaWJhb6CcyWQQ = wljBXXGXyTaWJhb6CcyWQQ.ctor = $ctor$(null, 'swAABnGXyTaWJhb6CcyWQQ', type$wljBXXGXyTaWJhb6CcyWQQ);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__List`1..ctor
  type$wljBXXGXyTaWJhb6CcyWQQ.tAAABnGXyTaWJhb6CcyWQQ = function (b)
  {
    var a = this, c;

    a._items = Hw4ABqPHMD_aEoYb_aoNHL9w();
    c = !(b == null);

    if (!c)
    {
      throw lwAABq9OGjCe3bHElJJ0LA('collection is null');
    }

    a.vgAABnGXyTaWJhb6CcyWQQ(b);
  };
  var ctor$tAAABnGXyTaWJhb6CcyWQQ = $ctor$(null, 'tAAABnGXyTaWJhb6CcyWQQ', type$wljBXXGXyTaWJhb6CcyWQQ);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__List`1.get_ArrayReferenceCloned
  type$wljBXXGXyTaWJhb6CcyWQQ.tQAABnGXyTaWJhb6CcyWQQ = function ()
  {
    var a = this, b;

    b = a._items.slice(0);
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__List`1.ToArray
  type$wljBXXGXyTaWJhb6CcyWQQ.tgAABnGXyTaWJhb6CcyWQQ = function ()
  {
    var a = this, b;

    b = a.tQAABnGXyTaWJhb6CcyWQQ();
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__List`1.IndexOf
  type$wljBXXGXyTaWJhb6CcyWQQ.twAABnGXyTaWJhb6CcyWQQ = function (b)
  {
    var a = this, c, d, e, f;

    c = -1;

    for (d = 0; (d < a.wgAABnGXyTaWJhb6CcyWQQ()); d++)
    {
      f = !_2BIABr5xMzijfM5xNYhyrw(a.ugAABnGXyTaWJhb6CcyWQQ(d), b);

      if (!f)
      {
        c = d;
        break;
      }

    }

    e = c;
    return e;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__List`1.Insert
  type$wljBXXGXyTaWJhb6CcyWQQ.uAAABnGXyTaWJhb6CcyWQQ = function (b, c)
  {
    var a = this;

    a._items.splice(b, 0, c);
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__List`1.RemoveAt
  type$wljBXXGXyTaWJhb6CcyWQQ.uQAABnGXyTaWJhb6CcyWQQ = function (b)
  {
    var a = this, c;

    c = (b < a.wgAABnGXyTaWJhb6CcyWQQ());

    if (!c)
    {
      throw lwAABq9OGjCe3bHElJJ0LA('ArgumentOutOfRangeException');
    }

    a._items.splice(b, 1);
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__List`1.get_Item
  type$wljBXXGXyTaWJhb6CcyWQQ.ugAABnGXyTaWJhb6CcyWQQ = function (b)
  {
    var a = this, c, d;

    d = (b < a.wgAABnGXyTaWJhb6CcyWQQ());

    if (!d)
    {
      throw lwAABq9OGjCe3bHElJJ0LA('ArgumentOutOfRangeException');
    }

    c = LA4ABqPHMD_aEoYb_aoNHL9w(a._items, b);
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__List`1.set_Item
  type$wljBXXGXyTaWJhb6CcyWQQ.uwAABnGXyTaWJhb6CcyWQQ = function (b, c)
  {
    var a = this, d;

    d = (b < a.wgAABnGXyTaWJhb6CcyWQQ());

    if (!d)
    {
      throw lwAABq9OGjCe3bHElJJ0LA('ArgumentOutOfRangeException');
    }

    LQ4ABqPHMD_aEoYb_aoNHL9w(a._items, b, c);
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__List`1.ForEach
  type$wljBXXGXyTaWJhb6CcyWQQ.vAAABnGXyTaWJhb6CcyWQQ = function (b)
  {
    var a = this, c, d;

    d = !(b == null);

    if (!d)
    {
      throw lwAABq9OGjCe3bHElJJ0LA('ArgumentOutOfRangeException');
    }


    for (c = 0; (c < a.wgAABnGXyTaWJhb6CcyWQQ()); c++)
    {
      b.Invoke(LA4ABqPHMD_aEoYb_aoNHL9w(a._items, c));
    }

  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__List`1.Add
  type$wljBXXGXyTaWJhb6CcyWQQ.vQAABnGXyTaWJhb6CcyWQQ = function (b)
  {
    var a = this;

    a._items.push(b);
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__List`1.AddRange
  type$wljBXXGXyTaWJhb6CcyWQQ.vgAABnGXyTaWJhb6CcyWQQ = function (b)
  {
    var a = this, c, d, e;

    d = ChQABi2cDDCKLtCQWJ9PkA(b).NgEABnMeWzaNooAKOmFm5g();
    try
    {
      while (d.qAAABu7N0xGI6ACQJ1TEOg())
      {
        c = d.xQAABrYmRzSu_anO2U_bk1MA();
        a.vQAABnGXyTaWJhb6CcyWQQ(c);
      }
    }
    finally
    {
      e = (d == null);

      if (!e)
      {
        d.xAAABq_bUDz_aWf_aXPRTEtLA();
      }

    }
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__List`1.Clear
  type$wljBXXGXyTaWJhb6CcyWQQ.vwAABnGXyTaWJhb6CcyWQQ = function ()
  {
    var a = this;

    a._items.splice(0, a.wgAABnGXyTaWJhb6CcyWQQ());
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__List`1.Contains
  type$wljBXXGXyTaWJhb6CcyWQQ.wAAABnGXyTaWJhb6CcyWQQ = function (b)
  {
    var a = this, c, d, e, f;

    c = 0;

    for (d = 0; (d < a.wgAABnGXyTaWJhb6CcyWQQ()); d++)
    {
      f = !_2BIABr5xMzijfM5xNYhyrw(a.ugAABnGXyTaWJhb6CcyWQQ(d), b);

      if (!f)
      {
        c = 1;
        break;
      }

    }

    e = c;
    return e;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__List`1.CopyTo
  type$wljBXXGXyTaWJhb6CcyWQQ.wQAABnGXyTaWJhb6CcyWQQ = function (b, c)
  {
    var a = this;

    throw lwAABq9OGjCe3bHElJJ0LA('The method or operation is not implemented.');
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__List`1.get_Count
  type$wljBXXGXyTaWJhb6CcyWQQ.wgAABnGXyTaWJhb6CcyWQQ = function ()
  {
    var a = this, b;

    b = a._items.length;
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__List`1.get_IsReadOnly
  type$wljBXXGXyTaWJhb6CcyWQQ.wwAABnGXyTaWJhb6CcyWQQ = function ()
  {
    var a = this;

    throw lwAABq9OGjCe3bHElJJ0LA('The method or operation is not implemented.');
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__List`1.Remove
  type$wljBXXGXyTaWJhb6CcyWQQ.xAAABnGXyTaWJhb6CcyWQQ = function (b)
  {
    var a = this, c, d, e;

    c = a.twAABnGXyTaWJhb6CcyWQQ(b);
    e = !(c == -1);

    if (!e)
    {
      d = 0;
      return d;
    }

    a.uQAABnGXyTaWJhb6CcyWQQ(c);
    d = 1;
    return d;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__List`1.RemoveAll
  type$wljBXXGXyTaWJhb6CcyWQQ.xQAABnGXyTaWJhb6CcyWQQ = function (b)
  {
    var a = this, c, d, e, f;

    c = 0;

    for (d = 0; (d < a.wgAABnGXyTaWJhb6CcyWQQ()); d++)
    {
      f = !b.Invoke(a.ugAABnGXyTaWJhb6CcyWQQ(d));

      if (!f)
      {
        a.uQAABnGXyTaWJhb6CcyWQQ(c);
        c--;
      }

      c++;
    }

    e = c;
    return e;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__List`1.GetEnumerator
  type$wljBXXGXyTaWJhb6CcyWQQ.xgAABnGXyTaWJhb6CcyWQQ = function ()
  {
    var a = this, b;

    b = new ctor$ygAABuT_aVDKnn40FH1TlGg(a);
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__List`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator
  type$wljBXXGXyTaWJhb6CcyWQQ.xwAABnGXyTaWJhb6CcyWQQ = function ()
  {
    var a = this, b;

    b = a.xgAABnGXyTaWJhb6CcyWQQ();
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__List`1.System.Collections.IEnumerable.GetEnumerator
  type$wljBXXGXyTaWJhb6CcyWQQ.yAAABnGXyTaWJhb6CcyWQQ = function ()
  {
    var a = this, b;

    b = a.xgAABnGXyTaWJhb6CcyWQQ();
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__List`1.Reverse
  type$wljBXXGXyTaWJhb6CcyWQQ.yQAABnGXyTaWJhb6CcyWQQ = function ()
  {
    var a = this, b, c, d;

    b = a.tgAABnGXyTaWJhb6CcyWQQ();

    for (c = 0; (c < b.length); c++)
    {
      a.uwAABnGXyTaWJhb6CcyWQQ(((b.length - 1) - c), b[c]);
    }

  };

  // 
  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__List`1
  (function (i)  {
    i._0xkABiRqbTmIbxb0k2jSqw = i.ugAABnGXyTaWJhb6CcyWQQ;
    i._1BkABiRqbTmIbxb0k2jSqw = i.uwAABnGXyTaWJhb6CcyWQQ;
    i._1RkABiRqbTmIbxb0k2jSqw = i.twAABnGXyTaWJhb6CcyWQQ;
    i._1hkABiRqbTmIbxb0k2jSqw = i.uAAABnGXyTaWJhb6CcyWQQ;
    i._1xkABiRqbTmIbxb0k2jSqw = i.uQAABnGXyTaWJhb6CcyWQQ;
    // 
    i.IBkABnTAkDm_aGe9ZbsQrAQ = i.wgAABnGXyTaWJhb6CcyWQQ;
    i.IRkABnTAkDm_aGe9ZbsQrAQ = i.wwAABnGXyTaWJhb6CcyWQQ;
    i.IhkABnTAkDm_aGe9ZbsQrAQ = i.vQAABnGXyTaWJhb6CcyWQQ;
    i.IxkABnTAkDm_aGe9ZbsQrAQ = i.vwAABnGXyTaWJhb6CcyWQQ;
    i.JBkABnTAkDm_aGe9ZbsQrAQ = i.wAAABnGXyTaWJhb6CcyWQQ;
    i.JRkABnTAkDm_aGe9ZbsQrAQ = i.wQAABnGXyTaWJhb6CcyWQQ;
    i.JhkABnTAkDm_aGe9ZbsQrAQ = i.xAAABnGXyTaWJhb6CcyWQQ;
    // 
    i.NgEABnMeWzaNooAKOmFm5g = i.xwAABnGXyTaWJhb6CcyWQQ;
    // System.Collections.IEnumerable
    i.EQAABu7N0xGI6ACQJ1TEOg = i.yAAABnGXyTaWJhb6CcyWQQ;
  }
  )(type$wljBXXGXyTaWJhb6CcyWQQ);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Dictionary`2+ValueCollection
  function t_aoaHaUXOTWBTxfXH2TMTA(){};
  t_aoaHaUXOTWBTxfXH2TMTA.TypeName = "ValueCollection";
  t_aoaHaUXOTWBTxfXH2TMTA.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$t_aoaHaUXOTWBTxfXH2TMTA = t_aoaHaUXOTWBTxfXH2TMTA.prototype = new wljBXXGXyTaWJhb6CcyWQQ();
  type$t_aoaHaUXOTWBTxfXH2TMTA.constructor = t_aoaHaUXOTWBTxfXH2TMTA;
  var basector$t_aoaHaUXOTWBTxfXH2TMTA = $ctor$(basector$wljBXXGXyTaWJhb6CcyWQQ, null, type$t_aoaHaUXOTWBTxfXH2TMTA);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Dictionary`2+ValueCollection..ctor
  type$t_aoaHaUXOTWBTxfXH2TMTA.__aggABqUXOTWBTxfXH2TMTA = function ()
  {
    var a = this;

    a.swAABnGXyTaWJhb6CcyWQQ();
  };
  var ctor$__aggABqUXOTWBTxfXH2TMTA = t_aoaHaUXOTWBTxfXH2TMTA.ctor = $ctor$(basector$wljBXXGXyTaWJhb6CcyWQQ, '__aggABqUXOTWBTxfXH2TMTA', type$t_aoaHaUXOTWBTxfXH2TMTA);

  // 
  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Dictionary`2+ValueCollection
  (function (i)  {
    i._0xkABiRqbTmIbxb0k2jSqw = i._6xkABosEszqMzVSBNHcdOA;
    i._1BkABiRqbTmIbxb0k2jSqw = i._7BkABosEszqMzVSBNHcdOA;
    i._1RkABiRqbTmIbxb0k2jSqw = i.EBoABosEszqMzVSBNHcdOA;
    i._1hkABiRqbTmIbxb0k2jSqw = i.FBoABosEszqMzVSBNHcdOA;
    i._1xkABiRqbTmIbxb0k2jSqw = i.HRoABosEszqMzVSBNHcdOA;
    // 
    i.IBkABnTAkDm_aGe9ZbsQrAQ = i._5RkABosEszqMzVSBNHcdOA;
    i.IRkABnTAkDm_aGe9ZbsQrAQ = i._5xkABosEszqMzVSBNHcdOA;
    i.IhkABnTAkDm_aGe9ZbsQrAQ = i._8RkABosEszqMzVSBNHcdOA;
    i.IxkABnTAkDm_aGe9ZbsQrAQ = i.__aBkABosEszqMzVSBNHcdOA;
    i.JBkABnTAkDm_aGe9ZbsQrAQ = i.__aRkABosEszqMzVSBNHcdOA;
    i.JRkABnTAkDm_aGe9ZbsQrAQ = i.__bxkABosEszqMzVSBNHcdOA;
    i.JhkABnTAkDm_aGe9ZbsQrAQ = i.GhoABosEszqMzVSBNHcdOA;
    // 
    i.NgEABnMeWzaNooAKOmFm5g = i.DRoABosEszqMzVSBNHcdOA;
    // System.Collections.IEnumerable
    i.EQAABu7N0xGI6ACQJ1TEOg = i.DhoABosEszqMzVSBNHcdOA;
    // System.Collections.IList
    i.FgAABmT3EzGRQDu9EnqWuw = i._7xkABosEszqMzVSBNHcdOA;
    i.FwAABmT3EzGRQDu9EnqWuw = i._8BkABosEszqMzVSBNHcdOA;
    i.GAAABmT3EzGRQDu9EnqWuw = i._8hkABosEszqMzVSBNHcdOA;
    i.GQAABmT3EzGRQDu9EnqWuw = i.__ahkABosEszqMzVSBNHcdOA;
    i.GgAABmT3EzGRQDu9EnqWuw = i.__aBkABosEszqMzVSBNHcdOA;
    i.GwAABmT3EzGRQDu9EnqWuw = i._6BkABosEszqMzVSBNHcdOA;
    i.HAAABmT3EzGRQDu9EnqWuw = i._5hkABosEszqMzVSBNHcdOA;
    i.HQAABmT3EzGRQDu9EnqWuw = i.ERoABosEszqMzVSBNHcdOA;
    i.HgAABmT3EzGRQDu9EnqWuw = i.FRoABosEszqMzVSBNHcdOA;
    i.HwAABmT3EzGRQDu9EnqWuw = i.GxoABosEszqMzVSBNHcdOA;
    i.IAAABmT3EzGRQDu9EnqWuw = i.HRoABosEszqMzVSBNHcdOA;
    // System.Collections.ICollection
    i.EgAABgHRkjqNHOcuXxDpkg = i.__bRkABosEszqMzVSBNHcdOA;
    i.EwAABgHRkjqNHOcuXxDpkg = i._5RkABosEszqMzVSBNHcdOA;
    i.FAAABgHRkjqNHOcuXxDpkg = i._6hkABosEszqMzVSBNHcdOA;
    i.FQAABgHRkjqNHOcuXxDpkg = i._6RkABosEszqMzVSBNHcdOA;
  }
  )(type$t_aoaHaUXOTWBTxfXH2TMTA);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Dictionary`2+KeyCollection
  function KYzfe3XimjuVSFjQZEELng(){};
  KYzfe3XimjuVSFjQZEELng.TypeName = "KeyCollection";
  KYzfe3XimjuVSFjQZEELng.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$KYzfe3XimjuVSFjQZEELng = KYzfe3XimjuVSFjQZEELng.prototype = new wljBXXGXyTaWJhb6CcyWQQ();
  type$KYzfe3XimjuVSFjQZEELng.constructor = KYzfe3XimjuVSFjQZEELng;
  var basector$KYzfe3XimjuVSFjQZEELng = $ctor$(basector$wljBXXGXyTaWJhb6CcyWQQ, null, type$KYzfe3XimjuVSFjQZEELng);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Dictionary`2+KeyCollection..ctor
  type$KYzfe3XimjuVSFjQZEELng.__aQgABnXimjuVSFjQZEELng = function ()
  {
    var a = this;

    a.swAABnGXyTaWJhb6CcyWQQ();
  };
  var ctor$__aQgABnXimjuVSFjQZEELng = KYzfe3XimjuVSFjQZEELng.ctor = $ctor$(basector$wljBXXGXyTaWJhb6CcyWQQ, '__aQgABnXimjuVSFjQZEELng', type$KYzfe3XimjuVSFjQZEELng);

  // 
  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Dictionary`2+KeyCollection
  (function (i)  {
    i._0xkABiRqbTmIbxb0k2jSqw = i._6xkABosEszqMzVSBNHcdOA;
    i._1BkABiRqbTmIbxb0k2jSqw = i._7BkABosEszqMzVSBNHcdOA;
    i._1RkABiRqbTmIbxb0k2jSqw = i.EBoABosEszqMzVSBNHcdOA;
    i._1hkABiRqbTmIbxb0k2jSqw = i.FBoABosEszqMzVSBNHcdOA;
    i._1xkABiRqbTmIbxb0k2jSqw = i.HRoABosEszqMzVSBNHcdOA;
    // 
    i.IBkABnTAkDm_aGe9ZbsQrAQ = i._5RkABosEszqMzVSBNHcdOA;
    i.IRkABnTAkDm_aGe9ZbsQrAQ = i._5xkABosEszqMzVSBNHcdOA;
    i.IhkABnTAkDm_aGe9ZbsQrAQ = i._8RkABosEszqMzVSBNHcdOA;
    i.IxkABnTAkDm_aGe9ZbsQrAQ = i.__aBkABosEszqMzVSBNHcdOA;
    i.JBkABnTAkDm_aGe9ZbsQrAQ = i.__aRkABosEszqMzVSBNHcdOA;
    i.JRkABnTAkDm_aGe9ZbsQrAQ = i.__bxkABosEszqMzVSBNHcdOA;
    i.JhkABnTAkDm_aGe9ZbsQrAQ = i.GhoABosEszqMzVSBNHcdOA;
    // 
    i.NgEABnMeWzaNooAKOmFm5g = i.DRoABosEszqMzVSBNHcdOA;
    // System.Collections.IEnumerable
    i.EQAABu7N0xGI6ACQJ1TEOg = i.DhoABosEszqMzVSBNHcdOA;
    // System.Collections.IList
    i.FgAABmT3EzGRQDu9EnqWuw = i._7xkABosEszqMzVSBNHcdOA;
    i.FwAABmT3EzGRQDu9EnqWuw = i._8BkABosEszqMzVSBNHcdOA;
    i.GAAABmT3EzGRQDu9EnqWuw = i._8hkABosEszqMzVSBNHcdOA;
    i.GQAABmT3EzGRQDu9EnqWuw = i.__ahkABosEszqMzVSBNHcdOA;
    i.GgAABmT3EzGRQDu9EnqWuw = i.__aBkABosEszqMzVSBNHcdOA;
    i.GwAABmT3EzGRQDu9EnqWuw = i._6BkABosEszqMzVSBNHcdOA;
    i.HAAABmT3EzGRQDu9EnqWuw = i._5hkABosEszqMzVSBNHcdOA;
    i.HQAABmT3EzGRQDu9EnqWuw = i.ERoABosEszqMzVSBNHcdOA;
    i.HgAABmT3EzGRQDu9EnqWuw = i.FRoABosEszqMzVSBNHcdOA;
    i.HwAABmT3EzGRQDu9EnqWuw = i.GxoABosEszqMzVSBNHcdOA;
    i.IAAABmT3EzGRQDu9EnqWuw = i.HRoABosEszqMzVSBNHcdOA;
    // System.Collections.ICollection
    i.EgAABgHRkjqNHOcuXxDpkg = i.__bRkABosEszqMzVSBNHcdOA;
    i.EwAABgHRkjqNHOcuXxDpkg = i._5RkABosEszqMzVSBNHcdOA;
    i.FAAABgHRkjqNHOcuXxDpkg = i._6hkABosEszqMzVSBNHcdOA;
    i.FQAABgHRkjqNHOcuXxDpkg = i._6RkABosEszqMzVSBNHcdOA;
  }
  )(type$KYzfe3XimjuVSFjQZEELng);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.Diagnostics.__Debugger
  function _4UYzUUaNQjCFzuobOvQyow(){};
  _4UYzUUaNQjCFzuobOvQyow.TypeName = "Debugger";
  _4UYzUUaNQjCFzuobOvQyow.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$_4UYzUUaNQjCFzuobOvQyow = _4UYzUUaNQjCFzuobOvQyow.prototype;
  type$_4UYzUUaNQjCFzuobOvQyow.constructor = _4UYzUUaNQjCFzuobOvQyow;
  var basector$_4UYzUUaNQjCFzuobOvQyow = $ctor$(null, null, type$_4UYzUUaNQjCFzuobOvQyow);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.Diagnostics.__Debugger..ctor
  type$_4UYzUUaNQjCFzuobOvQyow.sgAABkaNQjCFzuobOvQyow = function ()
  {
    var a = this;

  };
  var ctor$sgAABkaNQjCFzuobOvQyow = _4UYzUUaNQjCFzuobOvQyow.ctor = $ctor$(null, 'sgAABkaNQjCFzuobOvQyow', type$_4UYzUUaNQjCFzuobOvQyow);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Diagnostics.__Debugger.Break
  function sQAABkaNQjCFzuobOvQyow() { debugger; };
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Math
  function jIpWtTtXDzyrG3QwxgwmZw(){};
  jIpWtTtXDzyrG3QwxgwmZw.TypeName = "Math";
  jIpWtTtXDzyrG3QwxgwmZw.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$jIpWtTtXDzyrG3QwxgwmZw = jIpWtTtXDzyrG3QwxgwmZw.prototype;
  type$jIpWtTtXDzyrG3QwxgwmZw.constructor = jIpWtTtXDzyrG3QwxgwmZw;
  var kgAABDtXDzyrG3QwxgwmZw = null;
  var basector$jIpWtTtXDzyrG3QwxgwmZw = $ctor$(null, null, type$jIpWtTtXDzyrG3QwxgwmZw);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Math..ctor
  type$jIpWtTtXDzyrG3QwxgwmZw.sAAABjtXDzyrG3QwxgwmZw = function ()
  {
    var a = this;

  };
  var ctor$sAAABjtXDzyrG3QwxgwmZw = jIpWtTtXDzyrG3QwxgwmZw.ctor = $ctor$(null, 'sAAABjtXDzyrG3QwxgwmZw', type$jIpWtTtXDzyrG3QwxgwmZw);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Math.Floor
  function ngAABjtXDzyrG3QwxgwmZw(b)
  {
    var c;

    c = Math.floor(b);
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Math.Ceiling
  function nwAABjtXDzyrG3QwxgwmZw(b)
  {
    var c;

    c = Math.ceil(b);
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Math.Atan
  function oAAABjtXDzyrG3QwxgwmZw(b)
  {
    var c;

    c = Math.atan(b);
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Math.Tan
  function oQAABjtXDzyrG3QwxgwmZw(b)
  {
    var c;

    c = Math.tan(b);
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Math.Cos
  function ogAABjtXDzyrG3QwxgwmZw(b)
  {
    var c;

    c = Math.cos(b);
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Math.Sin
  function owAABjtXDzyrG3QwxgwmZw(b)
  {
    var c;

    c = Math.sin(b);
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Math.Abs
  function pAAABjtXDzyrG3QwxgwmZw(b)
  {
    var c;

    c = Math.abs(b);
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Math.Sqrt
  function pQAABjtXDzyrG3QwxgwmZw(b)
  {
    var c;

    c = Math.sqrt(b);
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Math.Abs
  function pgAABjtXDzyrG3QwxgwmZw(b)
  {
    var c;

    c = Math.abs(b);
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Math.Round
  function pwAABjtXDzyrG3QwxgwmZw(b)
  {
    var c;

    c = Math.round(b);
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Math.Max
  function qAAABjtXDzyrG3QwxgwmZw(b, c)
  {
    var d;

    d = Math.max(b, c);
    return d;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Math.Max
  function qQAABjtXDzyrG3QwxgwmZw(b, c)
  {
    var d;

    d = Math.max(b, c);
    return d;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Math.Max
  function qgAABjtXDzyrG3QwxgwmZw(b, c)
  {
    var d;

    d = Math.max(b, c);
    return d;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Math.Min
  function qwAABjtXDzyrG3QwxgwmZw(b, c)
  {
    var d;

    d = Math.min(b, c);
    return d;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Math.Min
  function rAAABjtXDzyrG3QwxgwmZw(b, c)
  {
    var d;

    d = Math.min(b, c);
    return d;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Math.Min
  function rQAABjtXDzyrG3QwxgwmZw(b, c)
  {
    var d;

    d = Math.min(b, c);
    return d;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Math.Sign
  function rgAABjtXDzyrG3QwxgwmZw(b)
  {
    var c, d;

    d = !(b == 0);

    if (!d)
    {
      c = 0;
      return c;
    }

    d = !(b < 0);

    if (!d)
    {
      c = -1;
      return c;
    }

    c = 1;
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Math.Pow
  function rwAABjtXDzyrG3QwxgwmZw(b, c)
  {
    var d;

    d = Math.pow(b, c);
    return d;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Exception.get_Message
  function lQAABq9OGjCe3bHElJJ0LA(a)
  {
    var b;

    b = KBMABlPGPDagr49yP4mjlg(JhMABlPGPDagr49yP4mjlg(a), 'message');
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Exception.InternalConstructor
  function lwAABq9OGjCe3bHElJJ0LA(e) { return new Error(e); };
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Exception.InternalConstructor
  function mQAABq9OGjCe3bHElJJ0LA() { return new Error(); };
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__ArgumentNullException.InternalConstructor
  function _0QAAButUdDuooDX2aLKa3w(b)
  {
    var c;

    c = lwAABq9OGjCe3bHElJJ0LA(gBMABjDeCj_aRJzaBmU9SJg('ArgumentNullException: ', b));
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__NotImplementedException.InternalConstructor
  function nAAABq584TSQo69VDcfM9Q()
  {
    var b;

    b = lwAABq9OGjCe3bHElJJ0LA('NotImplementedException');
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__NotImplementedException.InternalConstructor
  function nQAABq584TSQo69VDcfM9Q(b)
  {
    var c;

    c = lwAABq9OGjCe3bHElJJ0LA(gBMABjDeCj_aRJzaBmU9SJg('NotImplementedException: ', b));
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__WeakReference
  function VxI3MU_aSJzKh5t7FI9mf3A(){};
  VxI3MU_aSJzKh5t7FI9mf3A.TypeName = "WeakReference";
  VxI3MU_aSJzKh5t7FI9mf3A.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$VxI3MU_aSJzKh5t7FI9mf3A = VxI3MU_aSJzKh5t7FI9mf3A.prototype;
  type$VxI3MU_aSJzKh5t7FI9mf3A.constructor = VxI3MU_aSJzKh5t7FI9mf3A;
  var basector$VxI3MU_aSJzKh5t7FI9mf3A = $ctor$(null, null, type$VxI3MU_aSJzKh5t7FI9mf3A);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__WeakReference..ctor
  type$VxI3MU_aSJzKh5t7FI9mf3A.lAAABk_aSJzKh5t7FI9mf3A = function (b)
  {
    var a = this;

  };
  var ctor$lAAABk_aSJzKh5t7FI9mf3A = $ctor$(null, 'lAAABk_aSJzKh5t7FI9mf3A', type$VxI3MU_aSJzKh5t7FI9mf3A);

  // ScriptCoreLib.JavaScript.DOM.ISink+EventNames
  function z7S1Tf5TsTG1q9Y3roKlrQ(){};
  z7S1Tf5TsTG1q9Y3roKlrQ.TypeName = "EventNames";
  z7S1Tf5TsTG1q9Y3roKlrQ.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$z7S1Tf5TsTG1q9Y3roKlrQ = z7S1Tf5TsTG1q9Y3roKlrQ.prototype;
  type$z7S1Tf5TsTG1q9Y3roKlrQ.constructor = z7S1Tf5TsTG1q9Y3roKlrQ;
  type$z7S1Tf5TsTG1q9Y3roKlrQ.EventListener = null;
  type$z7S1Tf5TsTG1q9Y3roKlrQ.EventListenerAlt = null;
  type$z7S1Tf5TsTG1q9Y3roKlrQ.Event = null;
  type$z7S1Tf5TsTG1q9Y3roKlrQ.EventAlt = null;
  var basector$z7S1Tf5TsTG1q9Y3roKlrQ = $ctor$(null, null, type$z7S1Tf5TsTG1q9Y3roKlrQ);
  // ScriptCoreLib.JavaScript.DOM.ISink+EventNames..ctor
  type$z7S1Tf5TsTG1q9Y3roKlrQ.NwAABv5TsTG1q9Y3roKlrQ = function ()
  {
    var a = this;

  };
  var ctor$NwAABv5TsTG1q9Y3roKlrQ = z7S1Tf5TsTG1q9Y3roKlrQ.ctor = $ctor$(null, 'NwAABv5TsTG1q9Y3roKlrQ', type$z7S1Tf5TsTG1q9Y3roKlrQ);

  // ScriptCoreLib.JavaScript.DOM.ISink.InternalEvent
  function LQAABl_atOz_aFJQVdjiI7RQ(a, b, c, d)
  {
    var e, f;

    try
    {
      e = c.fAsABqgPxjmDnjkmC_a5nbw();
      f = !b;

      if (!f)
      {
        f = !BRMABr5xMzijfM5xNYhyrw(a, 'addEventListener');

        if (!f)
        {
          a.addEventListener(d.EventListener, e, 0);
          f = (d.EventListenerAlt == null);

          if (!f)
          {
            a.addEventListener(d.EventListenerAlt, e, 0);
          }

        }

        f = !BRMABr5xMzijfM5xNYhyrw(a, 'attachEvent');

        if (!f)
        {
          a.attachEvent(d.Event, e);
          f = (d.EventAlt == null);

          if (!f)
          {
            a.attachEvent(d.EventAlt, e);
          }

        }

        return;
      }

      f = !BRMABr5xMzijfM5xNYhyrw(a, 'removeEventListener');

      if (!f)
      {
        a.removeEventListener(d.EventListener, e, 0);
        f = (d.EventListenerAlt == null);

        if (!f)
        {
          a.removeEventListener(d.EventListenerAlt, e, 0);
        }

      }

      f = !BRMABr5xMzijfM5xNYhyrw(a, 'detachEvent');

      if (!f)
      {
        a.detachEvent(d.Event, e);
        f = (d.EventAlt == null);

        if (!f)
        {
          a.detachEvent(d.EventAlt, e);
        }

      }

    }
    catch (_ne) {}
  };

  // ScriptCoreLib.JavaScript.DOM.ISink.InternalEvent
  function LgAABl_atOz_aFJQVdjiI7RQ(a, b, c, d, e)
  {
    var f;

    try
    {
      f = new ctor$NwAABv5TsTG1q9Y3roKlrQ();
      f.Event = e;
      f.EventListener = d;
      LQAABl_atOz_aFJQVdjiI7RQ(a, b, c, f);
    }
    catch (_ne) {}
  };

  // ScriptCoreLib.JavaScript.DOM.ISink.InternalEvent
  function LwAABl_atOz_aFJQVdjiI7RQ(a, b, c, d)
  {
    try
    {
      LgAABl_atOz_aFJQVdjiI7RQ(a, b, c, d, gBMABjDeCj_aRJzaBmU9SJg('on', d));
    }
    catch (_ne) {}
  };

  // ScriptCoreLib.JavaScript.DOM.ISink.attachEvent
  // ScriptCoreLib.JavaScript.DOM.ISink.detachEvent
  // ScriptCoreLib.JavaScript.DOM.ISink.addEventListener
  function MgAABl_atOz_aFJQVdjiI7RQ(a, b, c, d)
  {
    a.addEventListener(b, c.fAsABqgPxjmDnjkmC_a5nbw(), d);
  };

  // ScriptCoreLib.JavaScript.DOM.ISink.removeEventListener
  function MwAABl_atOz_aFJQVdjiI7RQ(a, b, c, d)
  {
    a.removeEventListener(b, c.fAsABqgPxjmDnjkmC_a5nbw(), d);
  };

  // ScriptCoreLib.JavaScript.DOM.ISink.addEventListener
  // ScriptCoreLib.JavaScript.DOM.ISink.removeEventListener
  // ScriptCoreLib.JavaScript.DOM.IWindow.alert
  // ScriptCoreLib.JavaScript.DOM.IWindow.confirm
  // ScriptCoreLib.JavaScript.DOM.IWindow.prompt
  // ScriptCoreLib.JavaScript.DOM.IWindow.print
  // ScriptCoreLib.JavaScript.DOM.IWindow.focus
  // ScriptCoreLib.JavaScript.DOM.IWindow.blur
  // ScriptCoreLib.JavaScript.DOM.IWindow.moveTo
  // ScriptCoreLib.JavaScript.DOM.IWindow.escape
  // ScriptCoreLib.JavaScript.DOM.IWindow.unescape
  // ScriptCoreLib.JavaScript.DOM.IWindow.isNaN
  // ScriptCoreLib.JavaScript.DOM.IWindow.open
  // ScriptCoreLib.JavaScript.DOM.IWindow.open
  // ScriptCoreLib.JavaScript.DOM.IWindow.open
  function QBEABtpeljmp_berMhF6qtQ(a, b, c, d, e, f)
  {
    var g, h;

    g = Hw4ABqPHMD_aEoYb_aoNHL9w();
    g.push(fhMABjDeCj_aRJzaBmU9SJg('width=', new Number(d)));
    g.push(fhMABjDeCj_aRJzaBmU9SJg('height=', new Number(e)));
    g.push(gBMABjDeCj_aRJzaBmU9SJg('scrollbars=', ((f) ? 'yes' : 'no')));
    h = a.open(b, c, g.join(','));
    return h;
  };

  // ScriptCoreLib.JavaScript.DOM.IWindow.setTimeout
  // ScriptCoreLib.JavaScript.DOM.IWindow.setTimeout
  // ScriptCoreLib.JavaScript.DOM.IWindow.setTimeout
  function QxEABtpeljmp_berMhF6qtQ(a, b, c)
  {
    var d;

    d = a.setTimeout(b.fAsABqgPxjmDnjkmC_a5nbw(), c);
    return d;
  };

  // ScriptCoreLib.JavaScript.DOM.IWindow.setInterval
  // ScriptCoreLib.JavaScript.DOM.IWindow.setInterval
  // ScriptCoreLib.JavaScript.DOM.IWindow.setInterval
  function RhEABtpeljmp_berMhF6qtQ(a, b, c)
  {
    var d;

    d = a.setInterval(b.fAsABqgPxjmDnjkmC_a5nbw(), c);
    return d;
  };

  // ScriptCoreLib.JavaScript.DOM.IWindow.clearTimeout
  // ScriptCoreLib.JavaScript.DOM.IWindow.clearInterval
  // ScriptCoreLib.JavaScript.DOM.IWindow.add_onfocus
  function SREABtpeljmp_berMhF6qtQ(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 1, b, 'focus');
  };

  // ScriptCoreLib.JavaScript.DOM.IWindow.remove_onfocus
  function ShEABtpeljmp_berMhF6qtQ(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 0, b, 'focus');
  };

  // ScriptCoreLib.JavaScript.DOM.IWindow.add_onblur
  function SxEABtpeljmp_berMhF6qtQ(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 1, b, 'blur');
  };

  // ScriptCoreLib.JavaScript.DOM.IWindow.remove_onblur
  function TBEABtpeljmp_berMhF6qtQ(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 0, b, 'blur');
  };

  // ScriptCoreLib.JavaScript.DOM.IWindow.add_onload
  function TREABtpeljmp_berMhF6qtQ(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 1, b, 'load');
  };

  // ScriptCoreLib.JavaScript.DOM.IWindow.remove_onload
  function ThEABtpeljmp_berMhF6qtQ(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 0, b, 'load');
  };

  // ScriptCoreLib.JavaScript.DOM.IWindow.add_onunload
  function TxEABtpeljmp_berMhF6qtQ(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 1, b, 'unload');
  };

  // ScriptCoreLib.JavaScript.DOM.IWindow.remove_onunload
  function UBEABtpeljmp_berMhF6qtQ(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 0, b, 'unload');
  };

  // ScriptCoreLib.JavaScript.DOM.IWindow.add_onbeforeunload
  function UREABtpeljmp_berMhF6qtQ(a, b)
  {
    var c, d;

    d = /* DOMCreateType */new zMV_bYJK_bfz_aBu6W1XgsETg();
    d.value = b;
    c = new ctor$OQsABovAgzaPrv33odvgUQ(d, '_add_onbeforeunload_b__0');
    LwAABl_atOz_aFJQVdjiI7RQ(a, 1, c, 'beforeunload');
  };

  // ScriptCoreLib.JavaScript.DOM.IWindow.remove_onbeforeunload
  function UhEABtpeljmp_berMhF6qtQ(a, b)
  {
    throw lwAABq9OGjCe3bHElJJ0LA('Not implemented');
  };

  // ScriptCoreLib.JavaScript.DOM.IWindow.add_onresize
  function UxEABtpeljmp_berMhF6qtQ(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 1, b, 'resize');
  };

  // ScriptCoreLib.JavaScript.DOM.IWindow.remove_onresize
  function VBEABtpeljmp_berMhF6qtQ(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 0, b, 'resize');
  };

  // ScriptCoreLib.JavaScript.DOM.IWindow.add_onscroll
  function VREABtpeljmp_berMhF6qtQ(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 1, b, 'scroll');
  };

  // ScriptCoreLib.JavaScript.DOM.IWindow.remove_onscroll
  function VhEABtpeljmp_berMhF6qtQ(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 0, b, 'scroll');
  };

  // ScriptCoreLib.JavaScript.DOM.IWindow.scrollTo
  // ScriptCoreLib.JavaScript.DOM.IWindow.close
  // ScriptCoreLib.JavaScript.DOM.IWindow.eval
  // ScriptCoreLib.JavaScript.DOM.IWindow.InternalHeight
  function WhEABtpeljmp_berMhF6qtQ(w) { 
    var s = w.self;

    if (s && s.innerHeight)
    {
        return s.innerHeight;
    }

    var d = w.document.documentElement;

    if (d && d.clientHeight)
    {
        return d.clientHeight;
    }
    return 0;
 };
  // ScriptCoreLib.JavaScript.DOM.IWindow.InternalWidth
  function WxEABtpeljmp_berMhF6qtQ(w) { 
    var s = w.self;

    if (s && s.innerWidth)
    {
        return s.innerWidth;
    }

    var d = w.document.documentElement;

    if (d && d.clientWidth)
    {
        return d.clientWidth;
    }
    return 0;
 };
  // ScriptCoreLib.JavaScript.DOM.IWindow.get_Height
  function XBEABtpeljmp_berMhF6qtQ(a)
  {
    var b;

    b = WhEABtpeljmp_berMhF6qtQ(a);
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.IWindow.get_Width
  function XREABtpeljmp_berMhF6qtQ(a)
  {
    var b;

    b = WxEABtpeljmp_berMhF6qtQ(a);
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.IActiveX.get_IsSupported
  function Mw8ABmnHcTKxHThhFfFi6A()
  {
    var b, c;

    c = !GBMABr5xMzijfM5xNYhyrw(BBMABr5xMzijfM5xNYhyrw(window), 'ActiveXObject');

    if (!c)
    {
      b = 1;
      return b;
    }

    b = 0;
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.IActiveX.TryCreate
  function NQ8ABmnHcTKxHThhFfFi6A(b)
  {
    var c;

    try
    {
      c = new ActiveXObject(b);
      return c;
    }
    catch (__exc){ }
    c = null;
    return c;
  };

  // ScriptCoreLib.JavaScript.DOM.IActiveX.InternalConstructor
  function Ng8ABmnHcTKxHThhFfFi6A(b)
  {
    var c, d, e, f, g, h;

    c = null;
    f = b;

    for (g = 0; (g < f.length); g++)
    {
      d = f[g];
      c = NQ8ABmnHcTKxHThhFfFi6A(d);
      h = (c == null);

      if (!h)
      {
        break;
      }

    }

    e = c;
    return e;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Console+__BrowserConsole+ConsoleImplementation.CloseConsole
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Console+__BrowserConsole+ConsoleImplementation.OpenConsole
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Console+__BrowserConsole+ConsoleImplementation.WriteString
  // ScriptCoreLib.JavaScript.DOM.INode.get_text
  function OQAABlWwyD6MDQKkPtM6Fg(a)
  {
    var b, c, d;

    b = a;
    d = !BRMABr5xMzijfM5xNYhyrw(b, 'text');

    if (!d)
    {
      c = b.text;
      return c;
    }

    d = !BRMABr5xMzijfM5xNYhyrw(b, 'textContent');

    if (!d)
    {
      c = b.textContent;
      return c;
    }

    throw lwAABq9OGjCe3bHElJJ0LA('.text');
    return c;
  };

  // ScriptCoreLib.JavaScript.DOM.INode.cloneNode
  // ScriptCoreLib.JavaScript.DOM.INode.appendChild
  // ScriptCoreLib.JavaScript.DOM.INode.insertBefore
  // ScriptCoreLib.JavaScript.DOM.INode.insertPreviousSibling
  function PQAABlWwyD6MDQKkPtM6Fg(a, b)
  {
    a.parentNode.insertBefore(b, a);
  };

  // ScriptCoreLib.JavaScript.DOM.INode.insertNextSibling
  function PgAABlWwyD6MDQKkPtM6Fg(a, b)
  {
    var c;

    c = !(a.nextSibling == null);

    if (!c)
    {
      a.parentNode.appendChild(b);
      return;
    }

    PQAABlWwyD6MDQKkPtM6Fg(a.nextSibling, b);
  };

  // ScriptCoreLib.JavaScript.DOM.INode.appendChild
  function PwAABlWwyD6MDQKkPtM6Fg(a, b)
  {
    var c, d, e, f;

    d = b;

    for (e = 0; (e < d.length); e++)
    {
      c = d[e];
      a.appendChild(c);
    }

  };

  // ScriptCoreLib.JavaScript.DOM.INode.appendChild
  function QAAABlWwyD6MDQKkPtM6Fg(a, b)
  {
    var c, d, e, f;

    d = b;

    for (e = 0; (e < d.length); e++)
    {
      c = d[e];
      a.appendChild(Fg8ABqyVHzy7gPr_bdrWEIA(a.ownerDocument, c));
    }

  };

  // ScriptCoreLib.JavaScript.DOM.INode.removeChild
  // ScriptCoreLib.JavaScript.DOM.ITextNode.InternalConstructor
  function FA8ABqyVHzy7gPr_bdrWEIA()
  {
    var b;

    b = FQ8ABqyVHzy7gPr_bdrWEIA('');
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.ITextNode.InternalConstructor
  function FQ8ABqyVHzy7gPr_bdrWEIA(b)
  {
    var c;

    c = Fg8ABqyVHzy7gPr_bdrWEIA(document, b);
    return c;
  };

  // ScriptCoreLib.JavaScript.DOM.ITextNode.InternalConstructor
  function Fg8ABqyVHzy7gPr_bdrWEIA(b, c)
  {
    var d;

    d = b.createTextNode(c);
    return d;
  };

  // ScriptCoreLib.JavaScript.DOM.IDocument.appendChild
  function rQUABvhwZj_afOciLNhDTBw(a, b)
  {
    throw lwAABq9OGjCe3bHElJJ0LA('IDocument.appendChild is forbidden');
  };

  // ScriptCoreLib.JavaScript.DOM.IDocument.createTextNode
  // ScriptCoreLib.JavaScript.DOM.IDocument.hasChildNodes
  // ScriptCoreLib.JavaScript.DOM.IDocument`1.createElementNS
  // ScriptCoreLib.JavaScript.DOM.IDocument`1.createElement
  // ScriptCoreLib.JavaScript.DOM.IDocument`1.getElementsByTagName
  // ScriptCoreLib.JavaScript.DOM.IDocument`1.getElementById
  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLDocument.execCommand
  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLDocument.open
  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLDocument.close
  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLDocument.write
  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLDocument.writeln
  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLDocument.getElementsByName
  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLDocument.createElement
  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLDocument.getElementsByClassName
  function CwoABrOeqzW195CXZ74N9w(a, b, c)
  {
    var d, e;

    d = /* DOMCreateType */new Qfak8D_bmWj2yvyBRDJ4xOA();
    d.className = c;
    e = IA4ABqPHMD_aEoYb_aoNHL9w(a.getElementsByTagName(b), new ctor$KQsABoVjnjetV_aXG4GFkBQ(d, '_getElementsByClassName_b__0'));
    return e;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLDocument.ForEachClassName
  function DAoABrOeqzW195CXZ74N9w(a, b, c)
  {
    Ig4ABqPHMD_aEoYb_aoNHL9w(DQoABrOeqzW195CXZ74N9w(a, b), c);
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLDocument.getElementsByClassName
  function DQoABrOeqzW195CXZ74N9w(a, b)
  {
    var c;

    c = CwoABrOeqzW195CXZ74N9w(a, '\u002a', b);
    return c;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLDocument.open
  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLDocument.open
  function DwoABrOeqzW195CXZ74N9w(a, b)
  {
    var c;

    c = a.open('text\u002fhtml', ((b) ? 'replace' : ''));
    return c;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLDocument.add_onclick
  function EAoABrOeqzW195CXZ74N9w(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 1, b, 'click');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLDocument.remove_onclick
  function EQoABrOeqzW195CXZ74N9w(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 0, b, 'click');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLDocument.add_onkeydown
  function EgoABrOeqzW195CXZ74N9w(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 1, b, 'keydown');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLDocument.remove_onkeydown
  function EwoABrOeqzW195CXZ74N9w(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 0, b, 'keydown');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLDocument.add_onkeypress
  function FAoABrOeqzW195CXZ74N9w(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 1, b, 'keypress');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLDocument.remove_onkeypress
  function FQoABrOeqzW195CXZ74N9w(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 0, b, 'keypress');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLDocument.add_onkeyup
  function FgoABrOeqzW195CXZ74N9w(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 1, b, 'keyup');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLDocument.remove_onkeyup
  function FwoABrOeqzW195CXZ74N9w(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 0, b, 'keyup');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLDocument.add_onmousemove
  function GAoABrOeqzW195CXZ74N9w(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 1, b, 'mousemove');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLDocument.remove_onmousemove
  function GQoABrOeqzW195CXZ74N9w(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 0, b, 'mousemove');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLDocument.add_onmousedown
  function GgoABrOeqzW195CXZ74N9w(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 1, b, 'mousedown');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLDocument.remove_onmousedown
  function GwoABrOeqzW195CXZ74N9w(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 0, b, 'mousedown');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLDocument.add_onmouseup
  function HAoABrOeqzW195CXZ74N9w(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 1, b, 'mouseup');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLDocument.remove_onmouseup
  function HQoABrOeqzW195CXZ74N9w(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 0, b, 'mouseup');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLDocument.add_onmouseover
  function HgoABrOeqzW195CXZ74N9w(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 1, b, 'mouseover');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLDocument.remove_onmouseover
  function HwoABrOeqzW195CXZ74N9w(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 0, b, 'mouseover');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLDocument.add_onmouseout
  function IAoABrOeqzW195CXZ74N9w(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 1, b, 'mouseout');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLDocument.remove_onmouseout
  function IQoABrOeqzW195CXZ74N9w(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 0, b, 'mouseout');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLDocument.add_oncontextmenu
  function IgoABrOeqzW195CXZ74N9w(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 1, b, 'contextmenu');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLDocument.remove_oncontextmenu
  function IwoABrOeqzW195CXZ74N9w(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 0, b, 'contextmenu');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLDocument.getElementsByClassName
  function JAoABrOeqzW195CXZ74N9w(a, b)
  {
    var c;

    c = DQoABrOeqzW195CXZ74N9w(a, b);
    return c;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLDocument.set_DesignMode
  function JQoABrOeqzW195CXZ74N9w(a, b)
  {
    var c;

    c = !b;

    if (!c)
    {
      a.designMode = 'on';
      return;
    }

    a.designMode = 'off';
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLDocument.createEvent
  // ScriptCoreLib.JavaScript.DOM.XML.IXMLDocument.selectSingleNode
  function twUABnwZPj_az_b6m5XFzB4A(a, b)
  {
    var c, d, e, f;

    c = a;
    e = !Mw8ABmnHcTKxHThhFfFi6A();

    if (!e)
    {
      d = c.selectSingleNode(b);
      return d;
    }

    e = !BRMABr5xMzijfM5xNYhyrw(a, 'selectSingleNode');

    if (!e)
    {
      d = c.selectSingleNode(b);
      return d;
    }

    f = [
      b
    ];
    d = new Function('elementPath', '\u000d\u000a       var xpe = new XPathEvaluator();\u000d\u000a           var nsResolver = xpe.createNSResolver( this.ownerDocument == null ? this.documentElement : this.ownerDocument.documentElement);\u000d\u000a           var results = xpe.evaluate(elementPath,this,nsResolver,XPathResult.FIRST_ORDERED_NODE_TYPE, null);\u000d\u000a           return results.singleNodeValue;             \u000d\u000a            ').apply(a, f);
    return d;
  };

  // ScriptCoreLib.JavaScript.DOM.XML.IXMLDocument.selectNodes
  // ScriptCoreLib.JavaScript.DOM.XML.IXMLDocument.InternalConstructor
  function uQUABnwZPj_az_b6m5XFzB4A(name) { 
try
{
            return document.implementation.createDocument('', name, null);
}
catch (ex)
{
var z = new ActiveXObject('Microsoft.XMLDOM');
    z.documentElement = z.createElement(name);

            return z;
}


         };
  // ScriptCoreLib.JavaScript.DOM.XML.IXMLDocument.ToXMLString
  function ugUABnwZPj_az_b6m5XFzB4A(node) { 

  if (typeof XMLSerializer != 'undefined') {
    return new XMLSerializer().serializeToString(node);
  }
  else if (typeof node.xml != 'undefined') {
    return node.xml;
  }
  else {
    return '';
  }
 };
  // ScriptCoreLib.JavaScript.DOM.XML.IXMLDocument.Parse
  function uwUABnwZPj_az_b6m5XFzB4A(xml) { 

 var xmlDocument = null;
  if (typeof DOMParser != 'undefined') {
    xmlDocument = new DOMParser().parseFromString(xml,
'application/xml');
  }
  else if (typeof ActiveXObject != 'undefined') {
    /*@cc_on @*/
    /*@if (@_jscript_version >= 5)
    try {
      xmlDocument = new ActiveXObject('Microsoft.XMLDOM');
      xmlDocument.loadXML(xml);
    }
    catch (e) { }
    @end @*/  
  }
  return xmlDocument;
 };
  // ScriptCoreLib.JavaScript.DOM.XML.IXMLDocument.ToXMLString
  function vAUABnwZPj_az_b6m5XFzB4A(a)
  {
    var b;

    b = ugUABnwZPj_az_b6m5XFzB4A(a.documentElement);
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.IElement.setAttributeNS
  // ScriptCoreLib.JavaScript.DOM.IElement.setAttribute
  // ScriptCoreLib.JavaScript.DOM.IElement.getAttribute
  // ScriptCoreLib.JavaScript.DOM.IElement.hasAttribute
  // ScriptCoreLib.JavaScript.DOM.IElement.removeAttribute
  // ScriptCoreLib.JavaScript.DOM.XML.IXMLElement.InternalConstructor
  function igcABpT5EDGI7bGdvAw5qA(b, c, d)
  {
    var e, f, g;

    e = b.createElement(c);
    g = !(d.length > 0);

    if (!g)
    {
      PwAABlWwyD6MDQKkPtM6Fg(e, d);
    }

    f = e;
    return f;
  };

  // ScriptCoreLib.JavaScript.DOM.XML.IXMLElement.InternalConstructor
  function iwcABpT5EDGI7bGdvAw5qA(b, c, d)
  {
    var e, f, g;

    e = b.createElement(c);
    g = (d == null);

    if (!g)
    {
      e.appendChild(Fg8ABqyVHzy7gPr_bdrWEIA(b, d));
    }

    f = e;
    return f;
  };

  // ScriptCoreLib.JavaScript.DOM.XML.IXMLElement.get_outerXML
  function jAcABpT5EDGI7bGdvAw5qA(a)
  {
    var b;

    b = ugUABnwZPj_az_b6m5XFzB4A(a);
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.XML.IXMLElement.get_innerXML
  function jQcABpT5EDGI7bGdvAw5qA(a)
  {
    var b, c, d, e, f, g;

    b = Hw4ABqPHMD_aEoYb_aoNHL9w();
    e = a.childNodes;

    for (f = 0; (f < e.length); f++)
    {
      c = e[f];
      b.push(jAcABpT5EDGI7bGdvAw5qA(c));
    }

    d = b.join();
    return d;
  };

  var LwAABNcctzqgvmwoTNwgqg = 0;
  var MAAABNcctzqgvmwoTNwgqg = null;
  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.InternalConstructor
  function TwAABtcctzqgvmwoTNwgqg()
  {
    var b, c;

    c = [
      null,
      null,
      null
    ];
    b = VAAABtcctzqgvmwoTNwgqg(c);
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.InternalConstructor
  function UAAABtcctzqgvmwoTNwgqg(b)
  {
    var c;

    c = document.createElement(b);
    return c;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.InternalConstructor
  function UQAABtcctzqgvmwoTNwgqg(b)
  {
    var c;

    c = UwAABtcctzqgvmwoTNwgqg(b, null, null);
    return c;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.InternalConstructor
  function UgAABtcctzqgvmwoTNwgqg(b, c)
  {
    var d;

    d = UwAABtcctzqgvmwoTNwgqg(b, c, null);
    return d;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.InternalConstructor
  function UwAABtcctzqgvmwoTNwgqg(b, c, d)
  {
    var e, f, g, h;

    e = (!(d) ? document : d);
    f = e.createElement(b);
    h = (c == null);

    if (!h)
    {
      f.appendChild(FQ8ABqyVHzy7gPr_bdrWEIA(c));
    }

    g = f;
    return g;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.InternalConstructor
  function VAAABtcctzqgvmwoTNwgqg(b)
  {
    var c;

    c = VQAABtcctzqgvmwoTNwgqg('div', b);
    return c;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.InternalConstructor
  function VQAABtcctzqgvmwoTNwgqg(b, c)
  {
    var d, e;

    d = UwAABtcctzqgvmwoTNwgqg(b, null, null);
    PwAABlWwyD6MDQKkPtM6Fg(d, c);
    e = d;
    return e;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.get_innerText
  function VgAABtcctzqgvmwoTNwgqg(a)
  {
    var b, c;

    c = !(a.childNodes.length == 1);

    if (!c)
    {
      c = !(a.childNodes[0].nodeType == 3);

      if (!c)
      {
        b = a.childNodes[0].nodeValue;
        return b;
      }

    }

    b = '';
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.set_innerText
  function VwAABtcctzqgvmwoTNwgqg(a, b)
  {
    var c, d;

    c = null;
    d = !!a.childNodes.length;

    if (!d)
    {
      c = FA8ABqyVHzy7gPr_bdrWEIA();
      a.appendChild(c);
    }
    else
    {
      d = !(a.childNodes.length == 1);

      if (!d)
      {
        d = !(a.childNodes[0].nodeType == 3);

        if (!d)
        {
          c = a.childNodes[0];
        }
        else
        {
          hQAABtcctzqgvmwoTNwgqg(a);
          c = FA8ABqyVHzy7gPr_bdrWEIA();
          a.appendChild(c);
        }

      }
      else
      {
        hQAABtcctzqgvmwoTNwgqg(a);
        c = FA8ABqyVHzy7gPr_bdrWEIA();
        a.appendChild(c);
      }

    }

    c.nodeValue = b;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.op_Implicit
  function WAAABtcctzqgvmwoTNwgqg(b)
  {
    var c;

    c = b.style;
    return c;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.blur
  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.focus
  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.SetCenteredLocation
  function WwAABtcctzqgvmwoTNwgqg(a, b)
  {
    XAAABtcctzqgvmwoTNwgqg(a, b.X, b.Y);
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.SetCenteredLocation
  function XAAABtcctzqgvmwoTNwgqg(a, b, c)
  {
    a.style.position = 'absolute';
    Sg0ABoNSrD_aFizz5n6sJfw(a.style, (b - (a.clientWidth / 2)), (c - (a.clientHeight / 2)));
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.add_onclick
  function XQAABtcctzqgvmwoTNwgqg(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 1, b, 'click');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.remove_onclick
  function XgAABtcctzqgvmwoTNwgqg(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 0, b, 'click');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.add_ondblclick
  function XwAABtcctzqgvmwoTNwgqg(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 1, b, 'dblclick');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.remove_ondblclick
  function YAAABtcctzqgvmwoTNwgqg(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 0, b, 'dblclick');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.add_onmouseover
  function YQAABtcctzqgvmwoTNwgqg(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 1, b, 'mouseover');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.remove_onmouseover
  function YgAABtcctzqgvmwoTNwgqg(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 0, b, 'mouseover');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.add_onmouseout
  function YwAABtcctzqgvmwoTNwgqg(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 1, b, 'mouseout');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.remove_onmouseout
  function ZAAABtcctzqgvmwoTNwgqg(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 0, b, 'mouseout');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.add_onmousedown
  function ZQAABtcctzqgvmwoTNwgqg(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 1, b, 'mousedown');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.remove_onmousedown
  function ZgAABtcctzqgvmwoTNwgqg(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 0, b, 'mousedown');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.add_onmouseup
  function ZwAABtcctzqgvmwoTNwgqg(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 1, b, 'mouseup');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.remove_onmouseup
  function aAAABtcctzqgvmwoTNwgqg(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 0, b, 'mouseup');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.add_onmousemove
  function aQAABtcctzqgvmwoTNwgqg(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 1, b, 'mousemove');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.remove_onmousemove
  function agAABtcctzqgvmwoTNwgqg(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 0, b, 'mousemove');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.add_onmousewheel
  function awAABtcctzqgvmwoTNwgqg(a, b)
  {
    var c;

    c = new ctor$NwAABv5TsTG1q9Y3roKlrQ();
    c.Event = 'onmousewheel';
    c.EventListener = 'DOMMouseScroll';
    c.EventListenerAlt = 'mousewheel';
    LQAABl_atOz_aFJQVdjiI7RQ(a, 1, b, c);
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.remove_onmousewheel
  function bAAABtcctzqgvmwoTNwgqg(a, b)
  {
    var c;

    c = new ctor$NwAABv5TsTG1q9Y3roKlrQ();
    c.Event = 'onmousewheel';
    c.EventListener = 'DOMMouseScroll';
    c.EventListenerAlt = 'mousewheel';
    LQAABl_atOz_aFJQVdjiI7RQ(a, 0, b, c);
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.add_oncontextmenu
  function bQAABtcctzqgvmwoTNwgqg(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 1, b, 'contextmenu');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.remove_oncontextmenu
  function bgAABtcctzqgvmwoTNwgqg(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 0, b, 'contextmenu');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.add_onselectstart
  function bwAABtcctzqgvmwoTNwgqg(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 1, b, 'selectstart');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.remove_onselectstart
  function cAAABtcctzqgvmwoTNwgqg(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 0, b, 'selectstart');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.add_onscroll
  function cQAABtcctzqgvmwoTNwgqg(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 1, b, 'scroll');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.remove_onscroll
  function cgAABtcctzqgvmwoTNwgqg(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 0, b, 'scroll');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.add_onresize
  function cwAABtcctzqgvmwoTNwgqg(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 1, b, 'resize');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.remove_onresize
  function dAAABtcctzqgvmwoTNwgqg(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 0, b, 'resize');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.add_ondragdrop
  function dQAABtcctzqgvmwoTNwgqg(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 1, b, 'dragdrop');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.remove_ondragdrop
  function dgAABtcctzqgvmwoTNwgqg(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 0, b, 'dragdrop');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.add_onchange
  function dwAABtcctzqgvmwoTNwgqg(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 1, b, 'change');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.remove_onchange
  function eAAABtcctzqgvmwoTNwgqg(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 0, b, 'change');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.add_onfocus
  function eQAABtcctzqgvmwoTNwgqg(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 1, b, 'focus');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.remove_onfocus
  function egAABtcctzqgvmwoTNwgqg(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 0, b, 'focus');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.add_onblur
  function ewAABtcctzqgvmwoTNwgqg(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 1, b, 'blur');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.remove_onblur
  function fAAABtcctzqgvmwoTNwgqg(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 0, b, 'blur');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.add_onkeypress
  function fQAABtcctzqgvmwoTNwgqg(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 1, b, 'keypress');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.remove_onkeypress
  function fgAABtcctzqgvmwoTNwgqg(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 0, b, 'keypress');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.add_onkeyup
  function fwAABtcctzqgvmwoTNwgqg(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 1, b, 'keyup');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.remove_onkeyup
  function gAAABtcctzqgvmwoTNwgqg(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 0, b, 'keyup');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.add_onkeydown
  function gQAABtcctzqgvmwoTNwgqg(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 1, b, 'keydown');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.remove_onkeydown
  function ggAABtcctzqgvmwoTNwgqg(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 0, b, 'keydown');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.EnsureID
  function gwAABtcctzqgvmwoTNwgqg(a)
  {
    var b;

    b = !lxMABjDeCj_aRJzaBmU9SJg(a.id, '');

    if (!b)
    {
      LwAABNcctzqgvmwoTNwgqg = (LwAABNcctzqgvmwoTNwgqg + 1);
      a.id = fxMABjDeCj_aRJzaBmU9SJg(a.id, '$', new Number(LwAABNcctzqgvmwoTNwgqg));
    }

  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.ScrollToBottom
  function hAAABtcctzqgvmwoTNwgqg(a)
  {
    a.scrollTop = (a.scrollHeight - a.clientHeight);
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.removeChildren
  function hQAABtcctzqgvmwoTNwgqg(a)
  {
    var b;

    while (!(a.firstChild == null))
    {
      a.removeChild(a.firstChild);
    }
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.FadeOut
  function hgAABtcctzqgvmwoTNwgqg(a)
  {
    eAcABgltTjWniv3drmFOgg(a);
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.replaceChildrenWith
  function hwAABtcctzqgvmwoTNwgqg(a, b)
  {
    var c;

    hQAABtcctzqgvmwoTNwgqg(a);
    c = [
      b
    ];
    QAAABlWwyD6MDQKkPtM6Fg(a, c);
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.DisableSelection
  function iAAABtcctzqgvmwoTNwgqg(a)
  {
    ZQAABtcctzqgvmwoTNwgqg(a, fwcABm1g3jqH_atPTeraONg());
    bwAABtcctzqgvmwoTNwgqg(a, fwcABm1g3jqH_atPTeraONg());
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.EnableSelection
  function iQAABtcctzqgvmwoTNwgqg(a)
  {
    ZgAABtcctzqgvmwoTNwgqg(a, fwcABm1g3jqH_atPTeraONg());
    cAAABtcctzqgvmwoTNwgqg(a, fwcABm1g3jqH_atPTeraONg());
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.get_Bounds
  function igAABtcctzqgvmwoTNwgqg(a)
  {
    var b, c;

    b = new ctor$vBQABq43Ozes59U5VdfFiw();
    b.Left = a.offsetLeft;
    b.Top = a.offsetTop;
    b.Width = a.scrollWidth;
    b.Height = a.scrollHeight;
    c = b;
    return c;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.DisableContextMenu
  function iwAABtcctzqgvmwoTNwgqg(a)
  {
    bQAABtcctzqgvmwoTNwgqg(a, fwcABm1g3jqH_atPTeraONg());
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.setCapture
  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.releaseCapture
  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.InternalCaptureMouse
  function jgAABtcctzqgvmwoTNwgqg(b)
  {
    var c, d, e, f, g, h, i;

    d = null;
    e = /* DOMCreateType */new uOhATbb8UDSphiXtO1tz3g();
    e.self = b;
    g = !GBMABr5xMzijfM5xNYhyrw(BBMABr5xMzijfM5xNYhyrw(e.self), 'setCapture');

    if (!g)
    {
      e.self.setCapture();

      if (!d)
      {
        d = new ctor$MQsABh_bO7jaSPMhX_aXFxFw(e, '_InternalCaptureMouse_b__3');
      }

      f = d;
      return f;
    }

    e.flag = 0;
    e._capture = new ctor$KQsABoVjnjetV_aXG4GFkBQ(e, '_InternalCaptureMouse_b__4');
    h = MAAABNcctzqgvmwoTNwgqg;

    for (i = 0; (i < h.length); i++)
    {
      c = h[i];
      MgAABl_atOz_aFJQVdjiI7RQ(window, c, e._capture, 1);
    }

    f = new ctor$MQsABh_bO7jaSPMhX_aXFxFw(e, '_InternalCaptureMouse_b__5');
    return f;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.CaptureMouse
  function jwAABtcctzqgvmwoTNwgqg(a)
  {
    var b;

    b = jgAABtcctzqgvmwoTNwgqg(a);
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.dispatchEvent
  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLStyle.get_StyleSheet
  function wxEABtKyTTiMSMom97AAzA(a)
  {
    var b, c;

    c = !BRMABr5xMzijfM5xNYhyrw(a, 'sheet');

    if (!c)
    {
      b = a.sheet;
      return b;
    }

    c = !BRMABr5xMzijfM5xNYhyrw(a, 'styleSheet');

    if (!c)
    {
      b = a.styleSheet;
      return b;
    }

    throw lwAABq9OGjCe3bHElJJ0LA(fhMABjDeCj_aRJzaBmU9SJg('fault at IHTMLStyle.StyleSheet, members: ', _8BIABr5xMzijfM5xNYhyrw(a)));
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLStyle.InternalConstructor
  function xREABtKyTTiMSMom97AAzA()
  {
    var b, c, d;

    b = UQAABtcctzqgvmwoTNwgqg('style');
    try
    {
      d = [
        '\u002f\u002a\u002a\u002f'
      ];
      QAAABlWwyD6MDQKkPtM6Fg(b, d);
    }
    catch (__exc){ }
    b.type = 'text\u002fcss';
    c = b;
    return c;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLArea.InternalConstructor
  function whEABphQMzag9OXj5AUSdg()
  {
    var b;

    b = UQAABtcctzqgvmwoTNwgqg('map');
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLCode.InternalConstructor
  function vhEABhPz7DOFkqeRxbHmBA()
  {
    var b;

    b = UQAABtcctzqgvmwoTNwgqg('code');
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLCode.InternalConstructor
  function wBEABhPz7DOFkqeRxbHmBA(b)
  {
    var c, d;

    c = vhEABhPz7DOFkqeRxbHmBA();
    c.innerHTML = b;
    d = c;
    return d;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLAnchor.InternalConstructor
  function cxEABrVwrT_avMQpcni_aibA(b)
  {
    var c, d;

    c = dREABrVwrT_avMQpcni_aibA('about:blank', b);
    d = c;
    return d;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLAnchor.InternalConstructor
  function dBEABrVwrT_avMQpcni_aibA(b, c)
  {
    var d, e;

    d = UQAABtcctzqgvmwoTNwgqg('a');
    d.href = b;
    d.target = '_blank';
    PwAABlWwyD6MDQKkPtM6Fg(d, c);
    e = d;
    return e;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLAnchor.InternalConstructor
  function dREABrVwrT_avMQpcni_aibA(b, c)
  {
    var d, e, f, g;

    d = UQAABtcctzqgvmwoTNwgqg('a');
    d.href = b;
    d.target = '_blank';
    f = (c == null);

    if (!f)
    {
      g = [
        c
      ];
      QAAABlWwyD6MDQKkPtM6Fg(d, g);
    }

    e = d;
    return e;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLTableColumn.InternalConstructor
  function bREABmaO1zqNdjJHoCqWPg()
  {
    var b;

    b = document.createElement('td');
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLTableColumn.InternalConstructor
  function bhEABmaO1zqNdjJHoCqWPg(b)
  {
    var c, d;

    c = bREABmaO1zqNdjJHoCqWPg();
    PwAABlWwyD6MDQKkPtM6Fg(c, b);
    d = c;
    return d;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLTableColumn.InternalConstructor
  function bxEABmaO1zqNdjJHoCqWPg(b)
  {
    var c, d, e;

    c = bREABmaO1zqNdjJHoCqWPg();
    e = [
      b
    ];
    QAAABlWwyD6MDQKkPtM6Fg(c, e);
    d = c;
    return d;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLLink.InternalConstructor
  function aBEABpirsDqRmn4kP4SKpw()
  {
    var b;

    b = document.createElement('link');
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLLink.InternalConstructor
  function aREABpirsDqRmn4kP4SKpw(b, c, d)
  {
    var e, f;

    e = aBEABpirsDqRmn4kP4SKpw();
    e.rel = b;
    e.href = c;
    e.type = d;
    f = e;
    return f;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLTable.InternalConstructor
  function ZBEABrpyrjCU0Nc2c2_aUmA()
  {
    var b;

    b = document.createElement('table');
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLTable.AddBody
  function ZREABrpyrjCU0Nc2c2_aUmA(a)
  {
    var b, c;

    b = _2AgABu_aTcjq3PlQsUrtClA();
    a.appendChild(b);
    c = b;
    return c;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLSpan.InternalConstructor
  function _6w8ABoh9XzuJJZu1xMIKrw()
  {
    var b;

    b = document.createElement('span');
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLSpan.InternalConstructor
  function _7A8ABoh9XzuJJZu1xMIKrw(b)
  {
    var c, d;

    c = _6w8ABoh9XzuJJZu1xMIKrw();
    c.innerHTML = b;
    d = c;
    return d;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLSpan.InternalConstructor
  function _7Q8ABoh9XzuJJZu1xMIKrw(b)
  {
    var c, d;

    c = _6w8ABoh9XzuJJZu1xMIKrw();
    PwAABlWwyD6MDQKkPtM6Fg(c, b);
    d = c;
    return d;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLScript.InternalConstructor
  function Hg8ABkYoYz6o6k0GE2GJ_aA()
  {
    var b;

    b = UQAABtcctzqgvmwoTNwgqg('script');
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLSelect.InternalConstructor
  function GA8ABhoWrDqYFiE5jtonbw()
  {
    var b;

    b = UQAABtcctzqgvmwoTNwgqg('select');
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLSelect.Add
  function GQ8ABhoWrDqYFiE5jtonbw(a, b)
  {
    var c, d, e, f;

    d = _9BIABr5xMzijfM5xNYhyrw(b);

    for (e = 0; (e < d.length); e++)
    {
      c = d[e];
      Gg8ABhoWrDqYFiE5jtonbw(a, c.Name);
    }

  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLSelect.Add
  function Gg8ABhoWrDqYFiE5jtonbw(a, b)
  {
    var c, d;

    c = aQ0ABucqfzmCZnKh9rL0Ow();
    c.value = b;
    c.innerHTML = b;
    a.appendChild(c);
    d = c;
    return d;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLSelect.Add
  function Gw8ABhoWrDqYFiE5jtonbw(a, b)
  {
    var c, d, e, f;

    d = b;

    for (e = 0; (e < d.length); e++)
    {
      c = d[e];
      Gg8ABhoWrDqYFiE5jtonbw(a, fRMABjDeCj_aRJzaBmU9SJg(new Number(c)));
    }

  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLSelect.Add
  function HA8ABhoWrDqYFiE5jtonbw(a, b)
  {
    var c, d, e, f;

    d = b;

    for (e = 0; (e < d.length); e++)
    {
      c = d[e];
      Gg8ABhoWrDqYFiE5jtonbw(a, c);
    }

  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLFieldset.InternalConstructor
  function fQ4ABrdjdTKGkM3YW_aAVKg()
  {
    var b;

    b = UQAABtcctzqgvmwoTNwgqg('fieldset');
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLOption.InternalConstructor
  function aQ0ABucqfzmCZnKh9rL0Ow()
  {
    var b;

    b = UQAABtcctzqgvmwoTNwgqg('option');
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLEmbed.InternalConstructor
  function ewsABq8htz2AVt_bTi1Qy_ag()
  {
    var b;

    b = UQAABtcctzqgvmwoTNwgqg('embed');
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElementTemplate.InternalConstructor
  function JAsABhwF_bz_aMQccFk7190g() {  };
  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLLegend.InternalConstructor
  function _2ggABlFvBz_a3_aPb91VFLmw()
  {
    var b;

    b = UQAABtcctzqgvmwoTNwgqg('legend');
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLTableBody.AddRow
  function _0wgABu_aTcjq3PlQsUrtClA(a, b)
  {
    var c, d;

    d = [
      FQ8ABqyVHzy7gPr_bdrWEIA(b)
    ];
    c = _1AgABu_aTcjq3PlQsUrtClA(a, d);
    return c;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLTableBody.AddRow
  function _1AgABu_aTcjq3PlQsUrtClA(a, b)
  {
    var c, d, e, f, g, h, i, j;

    c = _1wgABu_aTcjq3PlQsUrtClA(a);
    h = b;

    for (i = 0; (i < h.length); i++)
    {
      d = h[i];
      e = bREABmaO1zqNdjJHoCqWPg();
      f = BBMABr5xMzijfM5xNYhyrw(d);
      j = !(d == null);

      if (!j)
      {
      }
      else
      {
        j = !__ahIABr5xMzijfM5xNYhyrw(f);

        if (!j)
        {
          e.innerHTML = _7RIABr5xMzijfM5xNYhyrw(f);
        }
        else
        {
          e.appendChild(_7hIABr5xMzijfM5xNYhyrw(f));
        }

      }

      c.appendChild(e);
    }

    g = c;
    return g;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLTableBody.AddRowAsColumns
  function _1QgABu_aTcjq3PlQsUrtClA(a, b)
  {
    var c, d, e, f;

    c = new Array(b.length);

    for (d = 0; (d < b.length); d++)
    {
      c[d] = FQ8ABqyVHzy7gPr_bdrWEIA(b[d]);
    }

    e = _1ggABu_aTcjq3PlQsUrtClA(a, c);
    return e;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLTableBody.AddRowAsColumns
  function _1ggABu_aTcjq3PlQsUrtClA(a, b)
  {
    var c, d, e, f, g, h, i, j, k;

    c = new Array(b.length);
    d = _1wgABu_aTcjq3PlQsUrtClA(a);
    e = 0;
    i = b;

    for (j = 0; (j < i.length); j++)
    {
      f = i[j];
      g = bREABmaO1zqNdjJHoCqWPg();
      c[e++] = g;
      k = (f == null);

      if (!k)
      {
        g.appendChild(f);
      }

      d.appendChild(g);
    }

    h = c;
    return h;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLTableBody.AddRow
  function _1wgABu_aTcjq3PlQsUrtClA(a)
  {
    var b, c;

    b = KxMABi8HaDq3hBxD6kJSBA();
    a.appendChild(b);
    c = b;
    return c;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLTableBody.InternalConstructor
  function _2AgABu_aTcjq3PlQsUrtClA()
  {
    var b;

    b = document.createElement('tbody');
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLBold.InternalConstructor
  function lQcABs6x_az6PGKxfQllDig()
  {
    var b;

    b = UQAABtcctzqgvmwoTNwgqg('b');
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLBold.InternalConstructor
  function lgcABs6x_az6PGKxfQllDig(b)
  {
    var c;

    c = UgAABtcctzqgvmwoTNwgqg('b', b);
    return c;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLButton.InternalConstructor
  function jwcABv3gAjiLYwezeH3NJA()
  {
    var b;

    b = UQAABtcctzqgvmwoTNwgqg('button');
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLButton.InternalConstructor
  function kQcABv3gAjiLYwezeH3NJA(b)
  {
    var c, d, e;

    c = jwcABv3gAjiLYwezeH3NJA();
    e = [
      b
    ];
    QAAABlWwyD6MDQKkPtM6Fg(c, e);
    d = c;
    return d;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLButton.Create
  function kgcABv3gAjiLYwezeH3NJA(b, c)
  {
    var d, e, f;

    e = /* DOMCreateType */new _6P0dN8arYzGTx82LfiadXw();
    e.h = c;
    d = kQcABv3gAjiLYwezeH3NJA(b);
    XQAABtcctzqgvmwoTNwgqg(d, new ctor$KQsABoVjnjetV_aXG4GFkBQ(e, '_Create_b__0'));
    cwsABt0jLD6yDQ0X6wt5_aw(d);
    f = d;
    return f;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLParam.InternalConstructor
  function AAYABjuT_azKFN72y_bpW2Jw()
  {
    var b;

    b = UQAABtcctzqgvmwoTNwgqg('param');
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLObject.InternalConstructor
  function __bQUABsdB7je07AQjh263yw()
  {
    var b;

    b = UQAABtcctzqgvmwoTNwgqg('object');
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLObject.Play
  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLForm.InternalConstructor
  function __agUABiSptDWr_aq92zr7yfA()
  {
    var b;

    b = UQAABtcctzqgvmwoTNwgqg('form');
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLForm.submit
  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLMap.InternalConstructor
  function __bgQABoll0jK_afiHbAOIOtA()
  {
    var b;

    b = UQAABtcctzqgvmwoTNwgqg('map');
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLInput.GetInteger
  function tQQABgsuyDCrtsUMq_bYA6w(a)
  {
    var b;

    b = mRIABhk97zGutkNmTIw91A(a.value);
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLInput.GetDouble
  function tgQABgsuyDCrtsUMq_bYA6w(a)
  {
    var b;

    b = iRAABtzT7Ti0XThSh5FnRg(a.value);
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLInput.get_IsInteger
  function twQABgsuyDCrtsUMq_bYA6w(a)
  {
    var b;

    b = !(_8BMABjToaTaQVWaeKug8LQ().exec(a.value) == null);
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLInput.get_IsCurrency
  function uAQABgsuyDCrtsUMq_bYA6w(a)
  {
    var b;

    b = !(_8RMABjToaTaQVWaeKug8LQ().exec(a.value) == null);
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLInput.InternalConstructor
  function vQQABgsuyDCrtsUMq_bYA6w()
  {
    var b;

    b = document.createElement('input');
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLInput.InternalConstructor
  function vgQABgsuyDCrtsUMq_bYA6w(b)
  {
    var c, d, e, f, g;

    c = null;
    d = 'radio';
    f = !(b == d);

    if (!f)
    {
      g = [
        '<input type=\'radio\' name=\'\' value=\'\' \u002f>'
      ];
      c = new Function('e', '\u002f\u002a\u0040cc_on return this.createElement(e); \u0040\u002a\u002f return null;').apply(document, g);
    }

    f = !(c == null);

    if (!f)
    {
      c = vQQABgsuyDCrtsUMq_bYA6w();
      c.type = b;
    }

    e = c;
    return e;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLInput.InternalConstructor
  function vwQABgsuyDCrtsUMq_bYA6w(b, c)
  {
    var d, e;

    d = vgQABgsuyDCrtsUMq_bYA6w(b);
    d.value = c;
    e = d;
    return e;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLInput.InternalConstructor
  function wAQABgsuyDCrtsUMq_bYA6w(b, c, d)
  {
    var e, f, g, h, i, j;

    e = null;
    f = 'radio';
    h = !(b == f);

    if (!h)
    {
      i = [
        null
      ];
      j = [
        '<input type=\'radio\' name=\'',
        c,
        '\' value=\'',
        d,
        '\' \u002f>'
      ];
      i[0] = exMABjDeCj_aRJzaBmU9SJg(j);
      e = new Function('e', '\u002f\u002a\u0040cc_on return this.createElement(e); \u0040\u002a\u002f return null;').apply(document, i);
    }

    h = !(e == null);

    if (!h)
    {
      e = vQQABgsuyDCrtsUMq_bYA6w();
      e.type = b;
      e.name = c;
      e.value = d;
    }

    g = e;
    return g;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLInput.CreateRadio
  function wQQABgsuyDCrtsUMq_bYA6w(b, c, d)
  {
    var e, f, g, h, i, j;

    e = null;
    f = '';
    h = !d;

    if (!h)
    {
      f = ' checked=\'checked\'';
    }

    i = [
      null
    ];
    j = [
      '<input type=\'radio\' name=\'',
      b,
      '\' value=\'',
      c,
      '\'',
      f,
      ' \u002f>'
    ];
    i[0] = exMABjDeCj_aRJzaBmU9SJg(j);
    e = new Function('e', '\u002f\u002a\u0040cc_on return this.createElement(e); \u0040\u002a\u002f return null;').apply(document, i);
    h = !(e == null);

    if (!h)
    {
      e = wAQABgsuyDCrtsUMq_bYA6w('radio', b, c);
      e.checked = d;
    }

    g = e;
    return g;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLInput.CreateCheckbox
  function wgQABgsuyDCrtsUMq_bYA6w(b)
  {
    var c, d;

    c = vgQABgsuyDCrtsUMq_bYA6w('checkbox');
    c.title = b;
    d = c;
    return d;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLLabel.InternalConstructor
  function kwMABhAgYDakVF6y8MYnEg()
  {
    var b;

    b = document.createElement('label');
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLLabel.InternalConstructor
  function lAMABhAgYDakVF6y8MYnEg(b)
  {
    var c, d, e;

    c = kwMABhAgYDakVF6y8MYnEg();
    e = [
      b
    ];
    QAAABlWwyD6MDQKkPtM6Fg(c, e);
    d = c;
    return d;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLLabel.InternalConstructor
  function lQMABhAgYDakVF6y8MYnEg(b, c)
  {
    var d, e;

    d = lAMABhAgYDakVF6y8MYnEg(b);
    gwAABtcctzqgvmwoTNwgqg(c);
    d.htmlFor = c.id;
    e = d;
    return e;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLImage.InternalConstructor
  function hgMABh2AFT20Op_b6nriULA(b)
  {
    var c, d, e;

    try
    {
      c = new Image();
      c.src = b;
      e = c;
    }
    catch (__exc)
    {
      d = gRMABjDeCj_aRJzaBmU9SJg('image failed to load: [', b, ']');
      IA8ABqiGvTSwHMGw_bObBeQ(d);
      throw lwAABq9OGjCe3bHElJJ0LA(d);
    }
    return e;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLImage.add_onerror
  function hwMABh2AFT20Op_b6nriULA(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 1, b, 'error');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLImage.remove_onerror
  function iAMABh2AFT20Op_b6nriULA(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 0, b, 'error');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLImage.op_Implicit
  function iQMABh2AFT20Op_b6nriULA(b)
  {
    var c;

    c = hgMABh2AFT20Op_b6nriULA(b);
    return c;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLImage.InvokeOnComplete
  function igMABh2AFT20Op_b6nriULA(a, b)
  {
    iwMABh2AFT20Op_b6nriULA(a, b, 100);
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLImage.InvokeOnComplete
  function iwMABh2AFT20Op_b6nriULA(a, b, c)
  {
    var d, e;

    d = /* DOMCreateType */new ji9wMVNl0DaYU86TBx_bMlQ();
    d.e = b;
    d.__4__this = a;
    e = !a.complete;

    if (!e)
    {
      d.e.Invoke(a);
      return;
    }

    d.t2 = new ctor$_1QIABilzizCnsZekUeVmgA();
    d.t2._0wIABilzizCnsZekUeVmgA(new ctor$KQsABoVjnjetV_aXG4GFkBQ(d, '_InvokeOnComplete_b__0'));
    d.t2._3QIABilzizCnsZekUeVmgA(c);
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLImage.Reload
  function jAMABh2AFT20Op_b6nriULA(a)
  {
    var b;

    b = a.src;
    a.src = b;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLImage.ToDocumentBackground
  function jQMABh2AFT20Op_b6nriULA(a)
  {
    jgMABh2AFT20Op_b6nriULA(a, document.body.style);
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLImage.ToBackground
  function jgMABh2AFT20Op_b6nriULA(a, b)
  {
    jwMABh2AFT20Op_b6nriULA(a, b, 1);
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLImage.ToBackground
  function jwMABh2AFT20Op_b6nriULA(a, b, c)
  {
    VA0ABoNSrD_aFizz5n6sJfw(b, a.src, c);
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLApplet.InternalConstructor
  function kwAABnhgyT6fR6MQdZae_ag()
  {
    var b;

    b = UQAABtcctzqgvmwoTNwgqg('applet');
    return b;
  };

  // delegate: (sender, args) => Void
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__EventHandler
  function uW0R_bLJfXTmd0AeTG5VQiQ(){};
  uW0R_bLJfXTmd0AeTG5VQiQ.TypeName = "EventHandler";
  uW0R_bLJfXTmd0AeTG5VQiQ.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$uW0R_bLJfXTmd0AeTG5VQiQ = uW0R_bLJfXTmd0AeTG5VQiQ.prototype = new kgJWtah_alDaJsZAeVC_bgaA();
  type$uW0R_bLJfXTmd0AeTG5VQiQ.constructor = uW0R_bLJfXTmd0AeTG5VQiQ;
  type$uW0R_bLJfXTmd0AeTG5VQiQ.IsExtensionMethod = false;
  type$uW0R_bLJfXTmd0AeTG5VQiQ.AsExtensionMethod = function ()
  {
    this.IsExtensionMethod = true;
    return this;
  };
  type$uW0R_bLJfXTmd0AeTG5VQiQ._5xEABrJfXTmd0AeTG5VQiQ = type$kgJWtah_alDaJsZAeVC_bgaA.hgsABqh_alDaJsZAeVC_bgaA;
  var ctor$_5xEABrJfXTmd0AeTG5VQiQ = $ctor$(basector$kgJWtah_alDaJsZAeVC_bgaA, '_5xEABrJfXTmd0AeTG5VQiQ', type$uW0R_bLJfXTmd0AeTG5VQiQ);
  type$uW0R_bLJfXTmd0AeTG5VQiQ.Invoke = function (b, c)
  {
    for (var _i = 0; _i < this.list.length; _i++)
    {
      var _f = this.list[_i];
      var _arguments = Array.prototype.slice.call(arguments).slice(0);
      if(_f.IsExtensionMethod) _arguments.splice(0, 0, _f.Target);
      var _target = _f.IsExtensionMethod ? window : _f.Target;
      _target[_f.Method].apply(_target, _arguments);
    }
  };

  // delegate: (sender, e) => Void
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__EventHandler`1
  function XY27_a3uEfjuPVlzJ1epT7g(){};
  XY27_a3uEfjuPVlzJ1epT7g.TypeName = "EventHandler_1";
  XY27_a3uEfjuPVlzJ1epT7g.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$XY27_a3uEfjuPVlzJ1epT7g = XY27_a3uEfjuPVlzJ1epT7g.prototype = new kgJWtah_alDaJsZAeVC_bgaA();
  type$XY27_a3uEfjuPVlzJ1epT7g.constructor = XY27_a3uEfjuPVlzJ1epT7g;
  type$XY27_a3uEfjuPVlzJ1epT7g.IsExtensionMethod = false;
  type$XY27_a3uEfjuPVlzJ1epT7g.AsExtensionMethod = function ()
  {
    this.IsExtensionMethod = true;
    return this;
  };
  type$XY27_a3uEfjuPVlzJ1epT7g._6xEABnuEfjuPVlzJ1epT7g = type$kgJWtah_alDaJsZAeVC_bgaA.hgsABqh_alDaJsZAeVC_bgaA;
  var ctor$_6xEABnuEfjuPVlzJ1epT7g = $ctor$(basector$kgJWtah_alDaJsZAeVC_bgaA, '_6xEABnuEfjuPVlzJ1epT7g', type$XY27_a3uEfjuPVlzJ1epT7g);
  type$XY27_a3uEfjuPVlzJ1epT7g.Invoke = function (b, c)
  {
    for (var _i = 0; _i < this.list.length; _i++)
    {
      var _f = this.list[_i];
      var _arguments = Array.prototype.slice.call(arguments).slice(0);
      if(_f.IsExtensionMethod) _arguments.splice(0, 0, _f.Target);
      var _target = _f.IsExtensionMethod ? window : _f.Target;
      _target[_f.Method].apply(_target, _arguments);
    }
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__NotSupportedException.InternalConstructor
  function _8REABs69FD_astU8tL1xvXQ()
  {
    var b;

    b = lwAABq9OGjCe3bHElJJ0LA('NotSupportedException');
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__NotSupportedException.InternalConstructor
  function _8hEABs69FD_astU8tL1xvXQ(b)
  {
    var c;

    c = lwAABq9OGjCe3bHElJJ0LA(gBMABjDeCj_aRJzaBmU9SJg('NotSupportedException: ', b));
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Array
  function YNeejDVfYjqt2eSWF8SaOQ(){};
  YNeejDVfYjqt2eSWF8SaOQ.TypeName = "Array";
  YNeejDVfYjqt2eSWF8SaOQ.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$YNeejDVfYjqt2eSWF8SaOQ = YNeejDVfYjqt2eSWF8SaOQ.prototype;
  type$YNeejDVfYjqt2eSWF8SaOQ.constructor = YNeejDVfYjqt2eSWF8SaOQ;
  var basector$YNeejDVfYjqt2eSWF8SaOQ = $ctor$(null, null, type$YNeejDVfYjqt2eSWF8SaOQ);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Array..ctor
  type$YNeejDVfYjqt2eSWF8SaOQ.__ahEABjVfYjqt2eSWF8SaOQ = function ()
  {
    var a = this;

  };
  var ctor$__ahEABjVfYjqt2eSWF8SaOQ = YNeejDVfYjqt2eSWF8SaOQ.ctor = $ctor$(null, '__ahEABjVfYjqt2eSWF8SaOQ', type$YNeejDVfYjqt2eSWF8SaOQ);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Array.IndexOf
  function _8xEABjVfYjqt2eSWF8SaOQ(b, c)
  {
    var d;

    d = Lg4ABqPHMD_aEoYb_aoNHL9w(b, c);
    return d;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Array.InternalCopyElement
  function _9BEABjVfYjqt2eSWF8SaOQ(s, d, i) { d[i] = s[i]; };
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Array.InternalCopyElement
  function _9REABjVfYjqt2eSWF8SaOQ(s, si, d, di) { d[di] = s[si]; };
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Array.Copy
  function _9hEABjVfYjqt2eSWF8SaOQ(b, c, d)
  {
    var e, f;


    for (e = 0; (e < d); e++)
    {
      _9BEABjVfYjqt2eSWF8SaOQ(b, c, e);
    }

  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Array.Copy
  function _9xEABjVfYjqt2eSWF8SaOQ(b, c, d, e, f)
  {
    var g, h;


    for (g = 0; (g < f); g++)
    {
      _9REABjVfYjqt2eSWF8SaOQ(b, (g + c), d, (g + e));
    }

  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Array.Sort
  function __aBEABjVfYjqt2eSWF8SaOQ(b, c)
  {
    var d;

    d = /* DOMCreateType */new n_b1JAgjoYziWbWb_bI9huXg();
    d.c = c;
    Mw4ABqPHMD_aEoYb_aoNHL9w(b, new ctor$PQsABknYYzqUF_bGIp4_aaGQ(d, '_Sort_b__0'));
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Array.Sort
  function __aREABjVfYjqt2eSWF8SaOQ(b, c)
  {
    __aBEABjVfYjqt2eSWF8SaOQ(b, new ctor$jBAABpPfLjS_aPoO7xO4TjA(c, 'mQAABpf0qD_arJIdqFekolg'));
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__StringWriter
  function nsd_aBq2DWTuBNbfIsMcd0A(){};
  nsd_aBq2DWTuBNbfIsMcd0A.TypeName = "StringWriter";
  nsd_aBq2DWTuBNbfIsMcd0A.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$nsd_aBq2DWTuBNbfIsMcd0A = nsd_aBq2DWTuBNbfIsMcd0A.prototype = new Wxo_bUh_bS8TGjoRdFP09sxA();
  type$nsd_aBq2DWTuBNbfIsMcd0A.constructor = nsd_aBq2DWTuBNbfIsMcd0A;
  type$nsd_aBq2DWTuBNbfIsMcd0A.StringBuilder = null;
  var basector$nsd_aBq2DWTuBNbfIsMcd0A = $ctor$(basector$Wxo_bUh_bS8TGjoRdFP09sxA, null, type$nsd_aBq2DWTuBNbfIsMcd0A);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__StringWriter..ctor
  type$nsd_aBq2DWTuBNbfIsMcd0A.OhIABq2DWTuBNbfIsMcd0A = function ()
  {
    var a = this;

    a.StringBuilder = new ctor$_1REABhHFijqvYJf1nSns2Q();
    a.CQYABh_bS8TGjoRdFP09sxA();
  };
  var ctor$OhIABq2DWTuBNbfIsMcd0A = nsd_aBq2DWTuBNbfIsMcd0A.ctor = $ctor$(basector$Wxo_bUh_bS8TGjoRdFP09sxA, 'OhIABq2DWTuBNbfIsMcd0A', type$nsd_aBq2DWTuBNbfIsMcd0A);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__StringWriter.WriteLine
  type$nsd_aBq2DWTuBNbfIsMcd0A.OBIABq2DWTuBNbfIsMcd0A = function (b)
  {
    var a = this;

    a.StringBuilder._3REABhHFijqvYJf1nSns2Q(b);
  };
    nsd_aBq2DWTuBNbfIsMcd0A.prototype.BwYABh_bS8TGjoRdFP09sxA = nsd_aBq2DWTuBNbfIsMcd0A.prototype.OBIABq2DWTuBNbfIsMcd0A;

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__StringWriter.ToString
  type$nsd_aBq2DWTuBNbfIsMcd0A.toString /* ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__StringWriter.ToString */ = function ()
  {
    var a = this, b;

    b = (a.StringBuilder+'');
    return b;
  };
    nsd_aBq2DWTuBNbfIsMcd0A.prototype.toString /* System.Object.ToString */ = nsd_aBq2DWTuBNbfIsMcd0A.prototype.toString /* ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__StringWriter.ToString */;

  // System.IDisposable
  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__StringWriter
  (function (i)  {
    i.xAAABq_bUDz_aWf_aXPRTEtLA = i.CAYABh_bS8TGjoRdFP09sxA;
  }
  )(type$nsd_aBq2DWTuBNbfIsMcd0A);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__StringReader
  function sR1ynuUU_aTC68duFYtujxg(){};
  sR1ynuUU_aTC68duFYtujxg.TypeName = "StringReader";
  sR1ynuUU_aTC68duFYtujxg.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$sR1ynuUU_aTC68duFYtujxg = sR1ynuUU_aTC68duFYtujxg.prototype = new aZFnb_a4m3TqP8M4PCqAT_aw();
  type$sR1ynuUU_aTC68duFYtujxg.constructor = sR1ynuUU_aTC68duFYtujxg;
  type$sR1ynuUU_aTC68duFYtujxg.InputString = null;
  type$sR1ynuUU_aTC68duFYtujxg.Position = 0;
  var basector$sR1ynuUU_aTC68duFYtujxg = $ctor$(basector$aZFnb_a4m3TqP8M4PCqAT_aw, null, type$sR1ynuUU_aTC68duFYtujxg);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__StringReader..ctor
  type$sR1ynuUU_aTC68duFYtujxg.OxIABuUU_aTC68duFYtujxg = function (b)
  {
    var a = this;

    a.cQUABu4m3TqP8M4PCqAT_aw();
    a.InputString = b;
  };
  var ctor$OxIABuUU_aTC68duFYtujxg = $ctor$(basector$aZFnb_a4m3TqP8M4PCqAT_aw, 'OxIABuUU_aTC68duFYtujxg', type$sR1ynuUU_aTC68duFYtujxg);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__StringReader.ReadLine
  type$sR1ynuUU_aTC68duFYtujxg.PBIABuUU_aTC68duFYtujxg = function ()
  {
    var a = this, b, c, d, e, f, g, h;

    h = !(a.Position < eBMABjDeCj_aRJzaBmU9SJg(a.InputString));

    if (!h)
    {
      b = dxMABjDeCj_aRJzaBmU9SJg(a.InputString, '\u000d\u000a', a.Position);
      c = dxMABjDeCj_aRJzaBmU9SJg(a.InputString, '\u000a', a.Position);
      d = eBMABjDeCj_aRJzaBmU9SJg('\u000d\u000a');
      e = 0;
      h = !(b < 0);

      if (!h)
      {
        e = 1;
      }

      h = !(c < b);

      if (!h)
      {
        e = 1;
      }

      h = !e;

      if (!h)
      {
        b = c;
        d = eBMABjDeCj_aRJzaBmU9SJg('\u000a');
      }

      f = a.Position;
      h = !(b < 0);

      if (!h)
      {
        b = eBMABjDeCj_aRJzaBmU9SJg(a.InputString);
        a.Position = b;
      }
      else
      {
        a.Position = (b + d);
      }

      g = khMABjDeCj_aRJzaBmU9SJg(a.InputString, f, (b - f));
      return g;
    }

    g = null;
    return g;
  };
    sR1ynuUU_aTC68duFYtujxg.prototype.bwUABu4m3TqP8M4PCqAT_aw = sR1ynuUU_aTC68duFYtujxg.prototype.PBIABuUU_aTC68duFYtujxg;

  // System.IDisposable
  // ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__StringReader
  (function (i)  {
    i.xAAABq_bUDz_aWf_aXPRTEtLA = i.cAUABu4m3TqP8M4PCqAT_aw;
  }
  )(type$sR1ynuUU_aTC68duFYtujxg);
  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLBreak.InternalConstructor
  function gRIABsrQuTudkoYWzsbWQg()
  {
    var b;

    b = UQAABtcctzqgvmwoTNwgqg('br');
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLDiv.InternalConstructor
  function hRIABrDe5zCamPUDNGeRBw()
  {
    var b;

    b = UQAABtcctzqgvmwoTNwgqg('div');
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLDiv.InternalConstructor
  function hhIABrDe5zCamPUDNGeRBw(b)
  {
    var c, d;

    c = hRIABrDe5zCamPUDNGeRBw();
    c.innerHTML = b;
    d = c;
    return d;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLDiv.InternalConstructor
  function hxIABrDe5zCamPUDNGeRBw(b)
  {
    var c, d;

    c = hRIABrDe5zCamPUDNGeRBw();
    PwAABlWwyD6MDQKkPtM6Fg(c, b);
    d = c;
    return d;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLDiv.ToFullscreen
  function iBIABrDe5zCamPUDNGeRBw(a)
  {
    var b, c, d;

    document.body.style.overflow = 'hidden';
    c = (a.parentNode == document.body);

    if (!c)
    {
      cwsABt0jLD6yDQ0X6wt5_aw(a);
    }

    b = new ctor$xhQABoqVzDa8F5yDSGjsZA(XREABtpeljmp_berMhF6qtQ(window), XBEABtpeljmp_berMhF6qtQ(window));
    d = [
      'fullscreen: ',
      new Number(b.X),
      ', ',
      new Number(b.Y)
    ];
    IA8ABqiGvTSwHMGw_bObBeQ(fBMABjDeCj_aRJzaBmU9SJg(d));
    Sw0ABoNSrD_aFizz5n6sJfw(a.style, 0, 0, b.X, b.Y);
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Type
  function hDwO4_bTjTTG_aL4_ahSLPtTA(){};
  hDwO4_bTjTTG_aL4_ahSLPtTA.TypeName = "Type";
  hDwO4_bTjTTG_aL4_ahSLPtTA.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$hDwO4_bTjTTG_aL4_ahSLPtTA = hDwO4_bTjTTG_aL4_ahSLPtTA.prototype = new rIWe5xv6ETyTub5rOKSvRA();
  type$hDwO4_bTjTTG_aL4_ahSLPtTA.constructor = hDwO4_bTjTTG_aL4_ahSLPtTA;
  type$hDwO4_bTjTTG_aL4_ahSLPtTA._TypeHandle = null;
  var basector$hDwO4_bTjTTG_aL4_ahSLPtTA = $ctor$(basector$rIWe5xv6ETyTub5rOKSvRA, null, type$hDwO4_bTjTTG_aL4_ahSLPtTA);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Type..ctor
  type$hDwO4_bTjTTG_aL4_ahSLPtTA.lhIABvTjTTG_aL4_ahSLPtTA = function ()
  {
    var a = this;

    a.pAMABhv6ETyTub5rOKSvRA();
  };
  var ctor$lhIABvTjTTG_aL4_ahSLPtTA = hDwO4_bTjTTG_aL4_ahSLPtTA.ctor = $ctor$(basector$rIWe5xv6ETyTub5rOKSvRA, 'lhIABvTjTTG_aL4_ahSLPtTA', type$hDwO4_bTjTTG_aL4_ahSLPtTA);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Type.get_Assembly
  type$hDwO4_bTjTTG_aL4_ahSLPtTA.iRIABvTjTTG_aL4_ahSLPtTA = function ()
  {
    var a = this, b, c;

    b = new ctor$qwMABvd3qD6XrPGZP0rKLA();
    b.__Value = BhMABr5xMzijfM5xNYhyrw(a.jRIABvTjTTG_aL4_ahSLPtTA().constructor, 'Assembly');
    c = b;
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Type.get_TypeHandle
  type$hDwO4_bTjTTG_aL4_ahSLPtTA.ihIABvTjTTG_aL4_ahSLPtTA = function ()
  {
    var a = this, b;

    b = a._TypeHandle;
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Type.set_TypeHandle
  type$hDwO4_bTjTTG_aL4_ahSLPtTA.ixIABvTjTTG_aL4_ahSLPtTA = function (b)
  {
    var a = this;

    a._TypeHandle = b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Type.GetField
  type$hDwO4_bTjTTG_aL4_ahSLPtTA.jBIABvTjTTG_aL4_ahSLPtTA = function (b)
  {
    var a = this, c, d, e, f, g, h, i;

    c = null;
    g = _9BIABr5xMzijfM5xNYhyrw(BBMABr5xMzijfM5xNYhyrw(a._TypeHandle.get_Value()));

    for (h = 0; (h < g.length); h++)
    {
      d = g[h];
      i = !lxMABjDeCj_aRJzaBmU9SJg(d.Name, b);

      if (!i)
      {
        e = new ctor$_4ggABttm1DmvK1RUasoEIQ();
        e._Name = d.Name;
        c = e;
        break;
      }

    }

    f = c;
    return f;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Type.AsExpando
  type$hDwO4_bTjTTG_aL4_ahSLPtTA.jRIABvTjTTG_aL4_ahSLPtTA = function ()
  {
    var a = this, b;

    b = BBMABr5xMzijfM5xNYhyrw(a._TypeHandle.get_Value());
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Type.GetFields
  type$hDwO4_bTjTTG_aL4_ahSLPtTA.jhIABvTjTTG_aL4_ahSLPtTA = function ()
  {
    var a = this, b, c, d, e, f, g, h;

    b = new ctor$swAABnGXyTaWJhb6CcyWQQ();
    f = _9BIABr5xMzijfM5xNYhyrw(a.jRIABvTjTTG_aL4_ahSLPtTA());

    for (g = 0; (g < f.length); g++)
    {
      c = f[g];
      d = new ctor$_4ggABttm1DmvK1RUasoEIQ();
      d._Name = c.Name;
      b.vQAABnGXyTaWJhb6CcyWQQ(d);
    }

    e = b.tgAABnGXyTaWJhb6CcyWQQ();
    return e;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Type.GetTypeFromHandle
  function jxIABvTjTTG_aL4_ahSLPtTA(b)
  {
    var c, d;

    c = new ctor$lhIABvTjTTG_aL4_ahSLPtTA();
    c.ixIABvTjTTG_aL4_ahSLPtTA(b);
    d = kBIABvTjTTG_aL4_ahSLPtTA(c);
    return d;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Type.op_Implicit
  function kBIABvTjTTG_aL4_ahSLPtTA(b)
  {
    var c;

    c = b;
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Type.Equals
  type$hDwO4_bTjTTG_aL4_ahSLPtTA.kRIABvTjTTG_aL4_ahSLPtTA = function (b)
  {
    var a = this, c, d, e, f;

    f = a.ihIABvTjTTG_aL4_ahSLPtTA();
    c = f.get_Value();
    f = b.ihIABvTjTTG_aL4_ahSLPtTA();
    d = f.get_Value();
    e = (c == d);
    return e;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Type.get_Name
  type$hDwO4_bTjTTG_aL4_ahSLPtTA.khIABvTjTTG_aL4_ahSLPtTA = function ()
  {
    var a = this, b;

    b = BhMABr5xMzijfM5xNYhyrw(a.jRIABvTjTTG_aL4_ahSLPtTA().constructor, 'TypeName');
    return b;
  };
    hDwO4_bTjTTG_aL4_ahSLPtTA.prototype.oQMABhv6ETyTub5rOKSvRA = hDwO4_bTjTTG_aL4_ahSLPtTA.prototype.khIABvTjTTG_aL4_ahSLPtTA;

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Type.get_Reflection
  type$hDwO4_bTjTTG_aL4_ahSLPtTA.kxIABvTjTTG_aL4_ahSLPtTA = function ()
  {
    var a = this, b;

    b = a.jRIABvTjTTG_aL4_ahSLPtTA().constructor;
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Type.GetCustomAttributes
  type$hDwO4_bTjTTG_aL4_ahSLPtTA.lBIABvTjTTG_aL4_ahSLPtTA = function (b)
  {
    var a = this, c;

    c = a.ogMABhv6ETyTub5rOKSvRA(null, 0);
    return c;
  };
    hDwO4_bTjTTG_aL4_ahSLPtTA.prototype.owMABhv6ETyTub5rOKSvRA = hDwO4_bTjTTG_aL4_ahSLPtTA.prototype.lBIABvTjTTG_aL4_ahSLPtTA;

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Type.GetCustomAttributes
  type$hDwO4_bTjTTG_aL4_ahSLPtTA.lRIABvTjTTG_aL4_ahSLPtTA = function (b, c)
  {
    var a = this, d, e, f, g, h, i, j, k;

    h = !c;

    if (!h)
    {
      throw _8REABs69FD_astU8tL1xvXQ();
    }

    h = !(a.kxIABvTjTTG_aL4_ahSLPtTA().GetAttributes == null);

    if (!h)
    {
      g = [

      ];
      return g;
    }

    d = new ctor$swAABnGXyTaWJhb6CcyWQQ();
    i = a.kxIABvTjTTG_aL4_ahSLPtTA().GetAttributes.apply(a.kxIABvTjTTG_aL4_ahSLPtTA(), []);

    for (j = 0; (j < i.length); j++)
    {
      e = i[j];
      f = 1;
      h = (b == null);

      if (!h)
      {
        k = b.ihIABvTjTTG_aL4_ahSLPtTA();
        h = _3xEABmaCrzG0XVBXGP7j9g(e.Type.prototype, k.get_Value());

        if (!h)
        {
          f = 0;
        }

      }

      h = !f;

      if (!h)
      {
        d.vQAABnGXyTaWJhb6CcyWQQ(e.Value);
      }

    }

    g = d.tgAABnGXyTaWJhb6CcyWQQ();
    return g;
  };
    hDwO4_bTjTTG_aL4_ahSLPtTA.prototype.ogMABhv6ETyTub5rOKSvRA = hDwO4_bTjTTG_aL4_ahSLPtTA.prototype.lRIABvTjTTG_aL4_ahSLPtTA;

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Type+__AttributeReflection
  function bOE_bJMJNUDC9XKC8HW6TiQ(){};
  bOE_bJMJNUDC9XKC8HW6TiQ.TypeName = "__AttributeReflection";
  bOE_bJMJNUDC9XKC8HW6TiQ.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$bOE_bJMJNUDC9XKC8HW6TiQ = bOE_bJMJNUDC9XKC8HW6TiQ.prototype;
  type$bOE_bJMJNUDC9XKC8HW6TiQ.constructor = bOE_bJMJNUDC9XKC8HW6TiQ;
  type$bOE_bJMJNUDC9XKC8HW6TiQ.Type = null;
  type$bOE_bJMJNUDC9XKC8HW6TiQ.Value = null;
  var basector$bOE_bJMJNUDC9XKC8HW6TiQ = $ctor$(null, null, type$bOE_bJMJNUDC9XKC8HW6TiQ);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Type+__AttributeReflection..ctor
  type$bOE_bJMJNUDC9XKC8HW6TiQ.lxIABsJNUDC9XKC8HW6TiQ = function ()
  {
    var a = this;

  };
  var ctor$lxIABsJNUDC9XKC8HW6TiQ = bOE_bJMJNUDC9XKC8HW6TiQ.ctor = $ctor$(null, 'lxIABsJNUDC9XKC8HW6TiQ', type$bOE_bJMJNUDC9XKC8HW6TiQ);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Type+__TypeReflection
  function y_bnZziVh5T6csyiz60_bZ7Q(){};
  y_bnZziVh5T6csyiz60_bZ7Q.TypeName = "__TypeReflection";
  y_bnZziVh5T6csyiz60_bZ7Q.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$y_bnZziVh5T6csyiz60_bZ7Q = y_bnZziVh5T6csyiz60_bZ7Q.prototype;
  type$y_bnZziVh5T6csyiz60_bZ7Q.constructor = y_bnZziVh5T6csyiz60_bZ7Q;
  type$y_bnZziVh5T6csyiz60_bZ7Q.GetAttributes = null;
  var basector$y_bnZziVh5T6csyiz60_bZ7Q = $ctor$(null, null, type$y_bnZziVh5T6csyiz60_bZ7Q);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Type+__TypeReflection..ctor
  type$y_bnZziVh5T6csyiz60_bZ7Q.mBIABiVh5T6csyiz60_bZ7Q = function ()
  {
    var a = this;

  };
  var ctor$mBIABiVh5T6csyiz60_bZ7Q = y_bnZziVh5T6csyiz60_bZ7Q.ctor = $ctor$(null, 'mBIABiVh5T6csyiz60_bZ7Q', type$y_bnZziVh5T6csyiz60_bZ7Q);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Int32
  function tSbqVRk97zGutkNmTIw91A(){};
  tSbqVRk97zGutkNmTIw91A.TypeName = "Int32";
  tSbqVRk97zGutkNmTIw91A.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$tSbqVRk97zGutkNmTIw91A = tSbqVRk97zGutkNmTIw91A.prototype;
  type$tSbqVRk97zGutkNmTIw91A.constructor = tSbqVRk97zGutkNmTIw91A;
  var basector$tSbqVRk97zGutkNmTIw91A = $ctor$(null, null, type$tSbqVRk97zGutkNmTIw91A);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Int32..ctor
  type$tSbqVRk97zGutkNmTIw91A.mxIABhk97zGutkNmTIw91A = function ()
  {
    var a = this;

  };
  var ctor$mxIABhk97zGutkNmTIw91A = tSbqVRk97zGutkNmTIw91A.ctor = $ctor$(null, 'mxIABhk97zGutkNmTIw91A', type$tSbqVRk97zGutkNmTIw91A);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Int32.Parse
  function mRIABhk97zGutkNmTIw91A(e) { return parseInt(e); };
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Int32.CompareTo
  function mhIABhk97zGutkNmTIw91A(a, b)
  {
    var c;

    c = _4RIABr5xMzijfM5xNYhyrw(a, b);
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Boolean
  function eH13nppCsz_axjn3f4mX0IA(){};
  eH13nppCsz_axjn3f4mX0IA.TypeName = "Boolean";
  eH13nppCsz_axjn3f4mX0IA.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$eH13nppCsz_axjn3f4mX0IA = eH13nppCsz_axjn3f4mX0IA.prototype;
  type$eH13nppCsz_axjn3f4mX0IA.constructor = eH13nppCsz_axjn3f4mX0IA;
  var basector$eH13nppCsz_axjn3f4mX0IA = $ctor$(null, null, type$eH13nppCsz_axjn3f4mX0IA);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Boolean..ctor
  type$eH13nppCsz_axjn3f4mX0IA.nRIABppCsz_axjn3f4mX0IA = function ()
  {
    var a = this;

  };
  var ctor$nRIABppCsz_axjn3f4mX0IA = eH13nppCsz_axjn3f4mX0IA.ctor = $ctor$(null, 'nRIABppCsz_axjn3f4mX0IA', type$eH13nppCsz_axjn3f4mX0IA);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Boolean.Parse
  function nBIABppCsz_axjn3f4mX0IA(e) { return !!e; };
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Convert
  function Dsdnp0Tl8zadTLwMJO1nnA(){};
  Dsdnp0Tl8zadTLwMJO1nnA.TypeName = "Convert";
  Dsdnp0Tl8zadTLwMJO1nnA.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$Dsdnp0Tl8zadTLwMJO1nnA = Dsdnp0Tl8zadTLwMJO1nnA.prototype;
  type$Dsdnp0Tl8zadTLwMJO1nnA.constructor = Dsdnp0Tl8zadTLwMJO1nnA;
  var basector$Dsdnp0Tl8zadTLwMJO1nnA = $ctor$(null, null, type$Dsdnp0Tl8zadTLwMJO1nnA);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Convert..ctor
  type$Dsdnp0Tl8zadTLwMJO1nnA.pxIABkTl8zadTLwMJO1nnA = function ()
  {
    var a = this;

  };
  var ctor$pxIABkTl8zadTLwMJO1nnA = Dsdnp0Tl8zadTLwMJO1nnA.ctor = $ctor$(null, 'pxIABkTl8zadTLwMJO1nnA', type$Dsdnp0Tl8zadTLwMJO1nnA);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Convert.ToInt32
  function nhIABkTl8zadTLwMJO1nnA(b)
  {
    var c;

    c = ngAABjtXDzyrG3QwxgwmZw(b);
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Convert.ToInt32
  function nxIABkTl8zadTLwMJO1nnA(b)
  {
    var c;

    c = ngAABjtXDzyrG3QwxgwmZw(b);
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Convert.ToByte
  function oBIABkTl8zadTLwMJO1nnA(b)
  {
    var c;

    c = (b & 255);
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Convert.ToByte
  function oRIABkTl8zadTLwMJO1nnA(b)
  {
    var c;

    c = (ngAABjtXDzyrG3QwxgwmZw(b) & 255);
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Convert.ToDouble
  function ohIABkTl8zadTLwMJO1nnA(b)
  {
    var c;

    c = b;
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Convert.ToString
  function oxIABkTl8zadTLwMJO1nnA(b)
  {
    var c;

    c = bRMABjDeCj_aRJzaBmU9SJg(b);
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Convert.ToDouble
  var pBIABkTl8zadTLwMJO1nnA = function () { return iRAABtzT7Ti0XThSh5FnRg.apply(null, arguments); };
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Convert.ToBoolean
  function pRIABkTl8zadTLwMJO1nnA(b)
  {
    var c;

    c = !!b;
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Convert.ToInt32
  function phIABkTl8zadTLwMJO1nnA(b)
  {
    var c, d;

    d = !b;

    if (!d)
    {
      c = 1;
      return c;
    }

    c = 0;
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.ComponentModel.__Component
  function X0_aa32YobjuKqyxcHwFJ3Q(){};
  X0_aa32YobjuKqyxcHwFJ3Q.TypeName = "Component";
  X0_aa32YobjuKqyxcHwFJ3Q.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$X0_aa32YobjuKqyxcHwFJ3Q = X0_aa32YobjuKqyxcHwFJ3Q.prototype;
  type$X0_aa32YobjuKqyxcHwFJ3Q.constructor = X0_aa32YobjuKqyxcHwFJ3Q;
  type$X0_aa32YobjuKqyxcHwFJ3Q._DesignMode_k__BackingField = false;
  var basector$X0_aa32YobjuKqyxcHwFJ3Q = $ctor$(null, null, type$X0_aa32YobjuKqyxcHwFJ3Q);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.ComponentModel.__Component..ctor
  type$X0_aa32YobjuKqyxcHwFJ3Q.qxIABmYobjuKqyxcHwFJ3Q = function ()
  {
    var a = this;

  };
  var ctor$qxIABmYobjuKqyxcHwFJ3Q = X0_aa32YobjuKqyxcHwFJ3Q.ctor = $ctor$(null, 'qxIABmYobjuKqyxcHwFJ3Q', type$X0_aa32YobjuKqyxcHwFJ3Q);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.ComponentModel.__Component.Dispose
  type$X0_aa32YobjuKqyxcHwFJ3Q.qBIABmYobjuKqyxcHwFJ3Q = function (b)
  {
    var a = this;

  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.ComponentModel.__Component.get_DesignMode
  type$X0_aa32YobjuKqyxcHwFJ3Q.qRIABmYobjuKqyxcHwFJ3Q = function ()
  {
    return this._DesignMode_k__BackingField;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.ComponentModel.__Component.set_DesignMode
  type$X0_aa32YobjuKqyxcHwFJ3Q.qhIABmYobjuKqyxcHwFJ3Q = function (b)
  {
    var a = this;

    a._DesignMode_k__BackingField = b;
  };

  // ScriptCoreLib.JavaScript.Runtime.ExpandoMember
  function mc_a8T_a_aiVzem_agWG9y0ZrA(){};
  mc_a8T_a_aiVzem_agWG9y0ZrA.TypeName = "ExpandoMember";
  mc_a8T_a_aiVzem_agWG9y0ZrA.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$mc_a8T_a_aiVzem_agWG9y0ZrA = mc_a8T_a_aiVzem_agWG9y0ZrA.prototype;
  type$mc_a8T_a_aiVzem_agWG9y0ZrA.constructor = mc_a8T_a_aiVzem_agWG9y0ZrA;
  type$mc_a8T_a_aiVzem_agWG9y0ZrA.Owner = null;
  type$mc_a8T_a_aiVzem_agWG9y0ZrA.Name = null;
  var basector$mc_a8T_a_aiVzem_agWG9y0ZrA = $ctor$(null, null, type$mc_a8T_a_aiVzem_agWG9y0ZrA);
  // ScriptCoreLib.JavaScript.Runtime.ExpandoMember..ctor
  type$mc_a8T_a_aiVzem_agWG9y0ZrA.zxIABu_aiVzem_agWG9y0ZrA = function (b, c)
  {
    var a = this;

    a.Owner = b;
    a.Name = c;
  };
  var ctor$zxIABu_aiVzem_agWG9y0ZrA = $ctor$(null, 'zxIABu_aiVzem_agWG9y0ZrA', type$mc_a8T_a_aiVzem_agWG9y0ZrA);

  // ScriptCoreLib.JavaScript.Runtime.ExpandoMember.Invoke
  type$mc_a8T_a_aiVzem_agWG9y0ZrA.zhIABu_aiVzem_agWG9y0ZrA = function (b)
  {
    var a = this, c;

    c = _7hIABr5xMzijfM5xNYhyrw(a._1hIABu_aiVzem_agWG9y0ZrA()).apply(a.Owner, b);
    return c;
  };

  // ScriptCoreLib.JavaScript.Runtime.ExpandoMember.get_Index
  type$mc_a8T_a_aiVzem_agWG9y0ZrA._0BIABu_aiVzem_agWG9y0ZrA = function ()
  {
    var a = this, b, c;

    c = !__aBIABr5xMzijfM5xNYhyrw(a.Owner);

    if (!c)
    {
      b = mRIABhk97zGutkNmTIw91A(a.Name);
      return b;
    }

    b = -1;
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.ExpandoMember.get_Value
  type$mc_a8T_a_aiVzem_agWG9y0ZrA._0RIABu_aiVzem_agWG9y0ZrA = function ()
  {
    var a = this, b;

    b = CBMABr5xMzijfM5xNYhyrw(a.Owner, a.Name);
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.ExpandoMember.set_Value
  type$mc_a8T_a_aiVzem_agWG9y0ZrA._0hIABu_aiVzem_agWG9y0ZrA = function (b)
  {
    var a = this;

    CRMABr5xMzijfM5xNYhyrw(a.Owner, a.Name, b);
  };

  // ScriptCoreLib.JavaScript.Runtime.ExpandoMember.get_TypeConstructorData
  type$mc_a8T_a_aiVzem_agWG9y0ZrA._0xIABu_aiVzem_agWG9y0ZrA = function ()
  {
    var a = this, b, c;

    c = !(_5BIABr5xMzijfM5xNYhyrw(a.Owner) == null);

    if (!c)
    {
      b = null;
      return b;
    }

    b = DRMABr5xMzijfM5xNYhyrw(_5BIABr5xMzijfM5xNYhyrw(a.Owner), a.Name);
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.ExpandoMember.ConstructorOfTypeName
  function _1BIABu_aiVzem_agWG9y0ZrA(b)
  {
    var c;

    c = CBMABr5xMzijfM5xNYhyrw(BBMABr5xMzijfM5xNYhyrw(window), b);
    return c;
  };

  // ScriptCoreLib.JavaScript.Runtime.ExpandoMember.get_TypeConstructor
  type$mc_a8T_a_aiVzem_agWG9y0ZrA._1RIABu_aiVzem_agWG9y0ZrA = function ()
  {
    var a = this, b, c, d;

    b = a._0xIABu_aiVzem_agWG9y0ZrA();
    d = !__ahIABr5xMzijfM5xNYhyrw(b);

    if (!d)
    {
      c = _1BIABu_aiVzem_agWG9y0ZrA(_7RIABr5xMzijfM5xNYhyrw(b));
      return c;
    }

    d = !__aBIABr5xMzijfM5xNYhyrw(b);

    if (!d)
    {
      c = _1BIABu_aiVzem_agWG9y0ZrA(_7RIABr5xMzijfM5xNYhyrw(DRMABr5xMzijfM5xNYhyrw(b, new Number(0))));
      return c;
    }

    c = null;
    return c;
  };

  // ScriptCoreLib.JavaScript.Runtime.ExpandoMember.get_Self
  type$mc_a8T_a_aiVzem_agWG9y0ZrA._1hIABu_aiVzem_agWG9y0ZrA = function ()
  {
    var a = this, b;

    b = CBMABr5xMzijfM5xNYhyrw(a.Owner, a.Name);
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.ExpandoMember.CopyTo
  type$mc_a8T_a_aiVzem_agWG9y0ZrA._1xIABu_aiVzem_agWG9y0ZrA = function (b)
  {
    var a = this;

    DBMABr5xMzijfM5xNYhyrw(b, a.Name, a._1hIABu_aiVzem_agWG9y0ZrA());
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.ReferenceEquals
  function _2BIABr5xMzijfM5xNYhyrw(a, b) { return a === b; };
  // ScriptCoreLib.JavaScript.Runtime.Expando.Compare
  function _4RIABr5xMzijfM5xNYhyrw(a, b) { return (a<b)?-1:(b<a?1:0); };
  // ScriptCoreLib.JavaScript.Runtime.Expando.Of
  function BBMABr5xMzijfM5xNYhyrw(b)
  {
    var c;

    c = b;
    return c;
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.GetMember
  function CBMABr5xMzijfM5xNYhyrw(a, b)
  {
    var c;

    c = BhMABr5xMzijfM5xNYhyrw(a, b);
    return c;
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.ExportCallback
  function EhMABr5xMzijfM5xNYhyrw(b, c)
  {
    IA8ABqiGvTSwHMGw_bObBeQ(gBMABjDeCj_aRJzaBmU9SJg('ExportCallback \u0040 ', b));
    CRMABr5xMzijfM5xNYhyrw(BBMABr5xMzijfM5xNYhyrw(window), b, c);
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.Find
  function _6xIABr5xMzijfM5xNYhyrw(a, b)
  {
    var c, d, e, f, g, h, i;

    c = _8xIABr5xMzijfM5xNYhyrw(a);
    d = new ctor$IxMABvqhLTy1Lxz2DoHTSw();
    g = c;

    for (h = 0; (h < g.length); h++)
    {
      e = g[h];
      d.Member = e;
      d.Item = _7hIABr5xMzijfM5xNYhyrw(e._1hIABu_aiVzem_agWG9y0ZrA());
      b.Invoke(d);
      i = !d.Found;

      if (!i)
      {
        break;
      }

    }

    f = ((d.Found) ? d : null);
    return f;
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.InternalGetMember
  function BhMABr5xMzijfM5xNYhyrw(o, m) { return o[m] };
  // ScriptCoreLib.JavaScript.Runtime.Expando.InternalSetMember
  function BxMABr5xMzijfM5xNYhyrw(o, m, v) { o[m] = v };
  // ScriptCoreLib.JavaScript.Runtime.Expando.get_IsArray
  function __aBIABr5xMzijfM5xNYhyrw(a)
  {
    var b;

    b = (ABMABr5xMzijfM5xNYhyrw(a) && _9xIABr5xMzijfM5xNYhyrw(a, window.Array));
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.InternalIsMember
  function BRMABr5xMzijfM5xNYhyrw(o, m) { try { return o[m] != void(0); } catch (exc) { return 'unknown'; }  };
  // ScriptCoreLib.JavaScript.Runtime.Expando.FromJSON
  function _3xIABr5xMzijfM5xNYhyrw(b, c)
  {
    var d, e;

    e = !c;

    if (!e)
    {
      d = _4BIABr5xMzijfM5xNYhyrw(YQgABl6xZjKlcaFaQuMTTA(b));
      return d;
    }

    d = _4BIABr5xMzijfM5xNYhyrw(b);
    return d;
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.To
  function _7hIABr5xMzijfM5xNYhyrw(a)
  {
    var b;

    b = a;
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.ToJSON
  function _2RIABr5xMzijfM5xNYhyrw(a)
  {
    var b, c, d, e, f, g;

    b = a;
    c = new ctor$Gg4ABvXKHjSzDZNs7b2S4g();
    g = !__ahIABr5xMzijfM5xNYhyrw(b);

    if (!g)
    {
      c.Eg4ABvXKHjSzDZNs7b2S4g('\"');
      c.Eg4ABvXKHjSzDZNs7b2S4g(_3RIABr5xMzijfM5xNYhyrw(b));
      c.Eg4ABvXKHjSzDZNs7b2S4g('\"');
    }
    else
    {
      g = !__bxIABr5xMzijfM5xNYhyrw(b);

      if (!g)
      {
        c.Eg4ABvXKHjSzDZNs7b2S4g(b);
      }
      else
      {
        g = !ABMABr5xMzijfM5xNYhyrw(b);

        if (!g)
        {
          g = !AhMABr5xMzijfM5xNYhyrw(b);

          if (!g)
          {
            c.Eg4ABvXKHjSzDZNs7b2S4g('null');
          }
          else
          {
            g = !__aBIABr5xMzijfM5xNYhyrw(b);

            if (!g)
            {
              c.Eg4ABvXKHjSzDZNs7b2S4g('[');
            }
            else
            {
              c.Eg4ABvXKHjSzDZNs7b2S4g('{');
            }

            d = _9BIABr5xMzijfM5xNYhyrw(b);

            for (e = 0; (e < d.length); e++)
            {
              g = !(e > 0);

              if (!g)
              {
                c.Eg4ABvXKHjSzDZNs7b2S4g(',');
              }

              g = __aBIABr5xMzijfM5xNYhyrw(b);

              if (!g)
              {
                c.Eg4ABvXKHjSzDZNs7b2S4g(_2RIABr5xMzijfM5xNYhyrw(BBMABr5xMzijfM5xNYhyrw(d[e].Name)));
                c.Eg4ABvXKHjSzDZNs7b2S4g(':');
              }

              c.Eg4ABvXKHjSzDZNs7b2S4g(_2RIABr5xMzijfM5xNYhyrw(d[e]._1hIABu_aiVzem_agWG9y0ZrA()));
            }

            g = !__aBIABr5xMzijfM5xNYhyrw(b);

            if (!g)
            {
              c.Eg4ABvXKHjSzDZNs7b2S4g(']');
            }
            else
            {
              c.Eg4ABvXKHjSzDZNs7b2S4g('}');
            }

          }

        }
        else
        {
          g = !__bBIABr5xMzijfM5xNYhyrw(b);

          if (!g)
          {
            c.Eg4ABvXKHjSzDZNs7b2S4g(((_7hIABr5xMzijfM5xNYhyrw(b)) ? 'true' : 'false'));
          }

        }

      }

    }

    f = c.Fw4ABvXKHjSzDZNs7b2S4g();
    return f;
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.get_TypeMetaName
  function _4hIABr5xMzijfM5xNYhyrw(a)
  {
    var b, c;

    c = !(_5BIABr5xMzijfM5xNYhyrw(a) == null);

    if (!c)
    {
      b = null;
      return b;
    }

    b = _7RIABr5xMzijfM5xNYhyrw(DRMABr5xMzijfM5xNYhyrw(_5BIABr5xMzijfM5xNYhyrw(a), '$0'));
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.Clone
  function _2hIABr5xMzijfM5xNYhyrw(a)
  {
    var b;

    b = _6RIABr5xMzijfM5xNYhyrw(_8xIABr5xMzijfM5xNYhyrw(a));
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.PHPSerialize
  function _2xIABr5xMzijfM5xNYhyrw(a)
  {
    var b;

    b = _3BIABr5xMzijfM5xNYhyrw(a, 0);
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.PHPSerialize
  function _3BIABr5xMzijfM5xNYhyrw(a, b)
  {
    var c, d, e, f, g, h, i, j, k, l;

    c = new ctor$Gg4ABvXKHjSzDZNs7b2S4g();
    i = !ABMABr5xMzijfM5xNYhyrw(a);

    if (!i)
    {
      d = _9BIABr5xMzijfM5xNYhyrw(a);
      c.Eg4ABvXKHjSzDZNs7b2S4g(fxMABjDeCj_aRJzaBmU9SJg('a:', new Number(d.length), ':{'));
      e = new ctor$Gg4ABvXKHjSzDZNs7b2S4g();
      j = d;

      for (k = 0; (k < j.length); k++)
      {
        f = j[k];
        e.Eg4ABvXKHjSzDZNs7b2S4g(_3BIABr5xMzijfM5xNYhyrw(BBMABr5xMzijfM5xNYhyrw(f.Name), (b + 1)));
        e.Eg4ABvXKHjSzDZNs7b2S4g(_3BIABr5xMzijfM5xNYhyrw(f._1hIABu_aiVzem_agWG9y0ZrA(), (b + 1)));
      }

      e.EQ4ABvXKHjSzDZNs7b2S4g();
      c.Eg4ABvXKHjSzDZNs7b2S4g(e.GA4ABvXKHjSzDZNs7b2S4g(';'));
      c.Eg4ABvXKHjSzDZNs7b2S4g('}');
    }
    else
    {
      i = !__ahIABr5xMzijfM5xNYhyrw(a);

      if (!i)
      {
        g = _7RIABr5xMzijfM5xNYhyrw(a);
        l = [
          's:',
          new Number(eBMABjDeCj_aRJzaBmU9SJg(g)),
          ':\"',
          g,
          '\"'
        ];
        c.Eg4ABvXKHjSzDZNs7b2S4g(fBMABjDeCj_aRJzaBmU9SJg(l));
      }
      else
      {
        i = !__bBIABr5xMzijfM5xNYhyrw(a);

        if (!i)
        {
          c.Eg4ABvXKHjSzDZNs7b2S4g(fhMABjDeCj_aRJzaBmU9SJg('i:', new Number((_7hIABr5xMzijfM5xNYhyrw(a) || 0))));
        }
        else
        {
          i = !__bxIABr5xMzijfM5xNYhyrw(a);

          if (!i)
          {
            i = !__bRIABr5xMzijfM5xNYhyrw(a);

            if (!i)
            {
              c.Eg4ABvXKHjSzDZNs7b2S4g(fhMABjDeCj_aRJzaBmU9SJg('d:', _7hIABr5xMzijfM5xNYhyrw(a)));
            }
            else
            {
              c.Eg4ABvXKHjSzDZNs7b2S4g(fhMABjDeCj_aRJzaBmU9SJg('i:', new Number(_7hIABr5xMzijfM5xNYhyrw(a))));
            }

          }

        }

      }

    }

    h = c.Fw4ABvXKHjSzDZNs7b2S4g();
    return h;
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.get_Literal
  function _3RIABr5xMzijfM5xNYhyrw(a)
  {
    var b, c, d, e, f, g, h, i;

    i = !__ahIABr5xMzijfM5xNYhyrw(a);

    if (!i)
    {
      b = new ctor$Gg4ABvXKHjSzDZNs7b2S4g();
      c = _7RIABr5xMzijfM5xNYhyrw(a);

      for (d = 0; (d < eBMABjDeCj_aRJzaBmU9SJg(c)); d++)
      {
        e = eRMABjDeCj_aRJzaBmU9SJg(c, d);
        f = bBMABjDeCj_aRJzaBmU9SJg(c, d);
        i = !(dRMABjDeCj_aRJzaBmU9SJg('\"\'\u005c\u0008\u000c\u000a\u000d\u0009', e) > -1);

        if (!i)
        {
          g = XggABl6xZjKlcaFaQuMTTA(f);
          i = (f > 255);

          if (!i)
          {
            g = gBMABjDeCj_aRJzaBmU9SJg('00', g);
          }

          b.Eg4ABvXKHjSzDZNs7b2S4g(gBMABjDeCj_aRJzaBmU9SJg('\u005cu', g));
        }
        else
        {
          i = !(f > 255);

          if (!i)
          {
            b.Eg4ABvXKHjSzDZNs7b2S4g(gBMABjDeCj_aRJzaBmU9SJg('\u005cu', XggABl6xZjKlcaFaQuMTTA(f)));
          }
          else
          {
            b.Eg4ABvXKHjSzDZNs7b2S4g(bRMABjDeCj_aRJzaBmU9SJg(e));
          }

        }

      }

      h = b.Fw4ABvXKHjSzDZNs7b2S4g();
      return h;
    }

    h = null;
    return h;
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.FromJSONProtocolString
  function _3hIABr5xMzijfM5xNYhyrw(b)
  {
    var c, d, e, f;

    c = dhMABjDeCj_aRJzaBmU9SJg(b, 'json:\u002f\u002f');
    f = !(c > -1);

    if (!f)
    {
      d = kRMABjDeCj_aRJzaBmU9SJg(b, (c + eBMABjDeCj_aRJzaBmU9SJg('json:\u002f\u002f')));
      e = _4BIABr5xMzijfM5xNYhyrw(d);
      return e;
    }

    e = null;
    return e;
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.FromJSON
  function _4BIABr5xMzijfM5xNYhyrw(b)
  {
    var c, d, e;

    c = null;
    e = (b == null);

    if (!e)
    {
      try
      {
        c = Ow4ABlsV3T_aPxZ1ZRmsarQ(new Function(gRMABjDeCj_aRJzaBmU9SJg('return (', b, ');')));
      }
      catch (__exc)
      {
        throw lwAABq9OGjCe3bHElJJ0LA(gBMABjDeCj_aRJzaBmU9SJg('Could not create object from json string : ', b));
      }
    }

    d = c;
    return d;
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.get_TypeDefaultConstructor
  function _4xIABr5xMzijfM5xNYhyrw(a)
  {
    var b, c;

    c = !(_5BIABr5xMzijfM5xNYhyrw(a) == null);

    if (!c)
    {
      b = null;
      return b;
    }

    b = _7RIABr5xMzijfM5xNYhyrw(DRMABr5xMzijfM5xNYhyrw(_5BIABr5xMzijfM5xNYhyrw(a), '$1'));
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.get_Metadata
  function _5BIABr5xMzijfM5xNYhyrw(a)
  {
    var b;

    b = DRMABr5xMzijfM5xNYhyrw(a, '$0');
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.InternalConstructor
  function _6BIABr5xMzijfM5xNYhyrw() { return {}; };
  // ScriptCoreLib.JavaScript.Runtime.Expando.InternalConstructor
  function _6RIABr5xMzijfM5xNYhyrw(b)
  {
    var c, d;

    c = _6BIABr5xMzijfM5xNYhyrw();
    _7BIABr5xMzijfM5xNYhyrw(b, c);
    d = c;
    return d;
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.InternalConstructor
  function _6hIABr5xMzijfM5xNYhyrw(ctor) { return new ctor(); };
  // ScriptCoreLib.JavaScript.Runtime.Expando.CopyTo
  function _7BIABr5xMzijfM5xNYhyrw(b, c)
  {
    var d, e, f, g;

    e = b;

    for (f = 0; (f < e.length); f++)
    {
      d = e[f];
      d._1xIABu_aiVzem_agWG9y0ZrA(c);
    }

  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.GetValue
  function _7RIABr5xMzijfM5xNYhyrw(a)
  {
    var b;

    b = (a+'');
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.InternalType
  function _7xIABr5xMzijfM5xNYhyrw(e) { return typeof e; };
  // ScriptCoreLib.JavaScript.Runtime.Expando.InternalGetMemberNames
  function _8BIABr5xMzijfM5xNYhyrw(e) { var x = []; for (var z in e) x.push(z); return x; };
  // ScriptCoreLib.JavaScript.Runtime.Expando.GetMemberNames
  function _8RIABr5xMzijfM5xNYhyrw(a)
  {
    var b;

    b = _8BIABr5xMzijfM5xNYhyrw(a);
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.GetMembers
  function _8hIABr5xMzijfM5xNYhyrw(a, b, c, d, e, f, g)
  {
    var h, i, j, k, l, m, n, o, p, q, r, s, t, u;

    h = Hw4ABqPHMD_aEoYb_aoNHL9w();
    s = _8RIABr5xMzijfM5xNYhyrw(a);

    for (t = 0; (t < s.length); t++)
    {
      i = s[t];
      j = 1;
      u = !lxMABjDeCj_aRJzaBmU9SJg(i, '$0');

      if (!u)
      {
        j = 0;
      }

      u = !j;

      if (!u)
      {
        k = new ctor$zxIABu_aiVzem_agWG9y0ZrA(a, i);
        l = (__ahIABr5xMzijfM5xNYhyrw(k._1hIABu_aiVzem_agWG9y0ZrA()) && b);
        m = (__bBIABr5xMzijfM5xNYhyrw(k._1hIABu_aiVzem_agWG9y0ZrA()) && c);
        n = (__bxIABr5xMzijfM5xNYhyrw(k._1hIABu_aiVzem_agWG9y0ZrA()) && d);
        o = (ABMABr5xMzijfM5xNYhyrw(k._1hIABu_aiVzem_agWG9y0ZrA()) && e);
        p = (__axIABr5xMzijfM5xNYhyrw(k._1hIABu_aiVzem_agWG9y0ZrA()) && f);
        q = (ARMABr5xMzijfM5xNYhyrw(k._1hIABu_aiVzem_agWG9y0ZrA()) && g);
        u = (!l && (!m && (!n && (!o && (!p && !q)))));

        if (!u)
        {
          h.push(k);
        }

      }

    }

    r = Lw4ABqPHMD_aEoYb_aoNHL9w(h);
    return r;
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.GetMembers
  function _8xIABr5xMzijfM5xNYhyrw(a)
  {
    var b, c, d, e, f, g;

    b = Hw4ABqPHMD_aEoYb_aoNHL9w();
    e = _8RIABr5xMzijfM5xNYhyrw(a);

    for (f = 0; (f < e.length); f++)
    {
      c = e[f];
      b.push(new ctor$zxIABu_aiVzem_agWG9y0ZrA(a, c));
    }

    d = Lw4ABqPHMD_aEoYb_aoNHL9w(b);
    return d;
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.GetFields
  function _9BIABr5xMzijfM5xNYhyrw(a)
  {
    var b;

    b = _8hIABr5xMzijfM5xNYhyrw(a, 1, 1, 1, 1, 0, 0);
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.GetFunctions
  function _9RIABr5xMzijfM5xNYhyrw(a)
  {
    var b;

    b = _8hIABr5xMzijfM5xNYhyrw(a, 0, 0, 0, 0, 1, 0);
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.InternalIsInstanceOf
  function _9hIABr5xMzijfM5xNYhyrw(e, c) { return (e instanceof c); };
  // ScriptCoreLib.JavaScript.Runtime.Expando.IsInstanceOf
  function _9xIABr5xMzijfM5xNYhyrw(a, b)
  {
    var c;

    c = _9hIABr5xMzijfM5xNYhyrw(a, b);
    return c;
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.IsArrayOf
  function __aRIABr5xMzijfM5xNYhyrw(a, b)
  {
    var c, d, e, f;

    e = !__aBIABr5xMzijfM5xNYhyrw(a);

    if (!e)
    {
      c = _7hIABr5xMzijfM5xNYhyrw(a);
      e = !(c.length > 0);

      if (!e)
      {
        f = [
          b,
          LA4ABqPHMD_aEoYb_aoNHL9w(c, 0)
        ];
        d = DhMABr5xMzijfM5xNYhyrw(f);
        return d;
      }

    }

    d = 0;
    return d;
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.get_IsString
  function __ahIABr5xMzijfM5xNYhyrw(a)
  {
    var b;

    b = lxMABjDeCj_aRJzaBmU9SJg(AxMABr5xMzijfM5xNYhyrw(a), 'string');
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.get_IsFunction
  function __axIABr5xMzijfM5xNYhyrw(a)
  {
    var b;

    b = lxMABjDeCj_aRJzaBmU9SJg(AxMABr5xMzijfM5xNYhyrw(a), 'function');
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.get_IsBoolean
  function __bBIABr5xMzijfM5xNYhyrw(a)
  {
    var b;

    b = lxMABjDeCj_aRJzaBmU9SJg(AxMABr5xMzijfM5xNYhyrw(a), 'boolean');
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.get_IsDouble
  function __bRIABr5xMzijfM5xNYhyrw(a)
  {
    var b, c, d;

    d = __bxIABr5xMzijfM5xNYhyrw(a);

    if (!d)
    {
      c = 0;
      return c;
    }

    b = _7hIABr5xMzijfM5xNYhyrw(a);
    c = !(pwAABjtXDzyrG3QwxgwmZw(b) == b);
    return c;
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.IsNativeNumberObject
  function __bhIABr5xMzijfM5xNYhyrw(e) { return e instanceof Number; };
  // ScriptCoreLib.JavaScript.Runtime.Expando.get_IsNumber
  function __bxIABr5xMzijfM5xNYhyrw(a)
  {
    var b, c;

    c = !__bhIABr5xMzijfM5xNYhyrw(a);

    if (!c)
    {
      b = 1;
      return b;
    }

    b = lxMABjDeCj_aRJzaBmU9SJg(AxMABr5xMzijfM5xNYhyrw(a), 'number');
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.get_IsObject
  function ABMABr5xMzijfM5xNYhyrw(a)
  {
    var b;

    b = lxMABjDeCj_aRJzaBmU9SJg(AxMABr5xMzijfM5xNYhyrw(a), 'object');
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.get_IsUndefined
  function ARMABr5xMzijfM5xNYhyrw(a)
  {
    var b;

    b = lxMABjDeCj_aRJzaBmU9SJg(AxMABr5xMzijfM5xNYhyrw(a), 'undefined');
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.get_IsNull
  function AhMABr5xMzijfM5xNYhyrw(a)
  {
    var b;

    b = (ABMABr5xMzijfM5xNYhyrw(a) && (_7hIABr5xMzijfM5xNYhyrw(a) == null));
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.get_TypeString
  function AxMABr5xMzijfM5xNYhyrw(a)
  {
    var b;

    b = _7xIABr5xMzijfM5xNYhyrw(a);
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.SetMember
  function CRMABr5xMzijfM5xNYhyrw(a, b, c)
  {
    BxMABr5xMzijfM5xNYhyrw(a, b, c);
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.GetMemberOf
  function ChMABr5xMzijfM5xNYhyrw(b, c, d, e)
  {
    var f;

    f = CxMABr5xMzijfM5xNYhyrw(BBMABr5xMzijfM5xNYhyrw(b), c, d, e);
    return f;
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.GetMember
  function CxMABr5xMzijfM5xNYhyrw(a, b, c, d)
  {
    var e, f;

    f = !BRMABr5xMzijfM5xNYhyrw(a, b);

    if (!f)
    {
      e = BhMABr5xMzijfM5xNYhyrw(a, b);
      return e;
    }

    f = !BRMABr5xMzijfM5xNYhyrw(a, c);

    if (!f)
    {
      e = BhMABr5xMzijfM5xNYhyrw(a, c);
      return e;
    }

    e = d;
    return e;
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.set_Item
  function DBMABr5xMzijfM5xNYhyrw(a, b, c)
  {
    BxMABr5xMzijfM5xNYhyrw(a, b, c);
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.get_Item
  function DRMABr5xMzijfM5xNYhyrw(a, b)
  {
    var c;

    c = BhMABr5xMzijfM5xNYhyrw(a, b);
    return c;
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.IsSameType
  function DhMABr5xMzijfM5xNYhyrw(b)
  {
    var c, d, e, f, g;

    c = 1;
    g = !(b.length > 1);

    if (!g)
    {
      d = BBMABr5xMzijfM5xNYhyrw(b[0]).constructor;

      for (e = 1; (e < b.length); e++)
      {
        g = (BBMABr5xMzijfM5xNYhyrw(b[e]).constructor == d);

        if (!g)
        {
          f = 0;
          return f;
        }

      }

    }

    f = c;
    return f;
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.Invoke
  function DxMABr5xMzijfM5xNYhyrw(o, m) { o[m](); };
  // ScriptCoreLib.JavaScript.Runtime.Expando.Invoke
  function EBMABr5xMzijfM5xNYhyrw(a, b)
  {
    DxMABr5xMzijfM5xNYhyrw(a, b);
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.CreateType
  function ERMABr5xMzijfM5xNYhyrw(a)
  {
    var b, c;

    b = Ow4ABlsV3T_aPxZ1ZRmsarQ(a.constructor);
    EBMABr5xMzijfM5xNYhyrw(b, _4xIABr5xMzijfM5xNYhyrw(a));
    c = b;
    return c;
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.ExportCallback
  function ExMABr5xMzijfM5xNYhyrw(b, c)
  {
    EhMABr5xMzijfM5xNYhyrw(b, Qw4ABlsV3T_aPxZ1ZRmsarQ(c));
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.GetUniqueID
  function FBMABr5xMzijfM5xNYhyrw(b)
  {
    var c;

    c = gBMABjDeCj_aRJzaBmU9SJg(b, XggABl6xZjKlcaFaQuMTTA(new ctor$__bwQABm4UGD6K4rxXW7dcsg().AQUABm4UGD6K4rxXW7dcsg(32000)));
    return c;
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.ResolveDualNotation
  function FRMABr5xMzijfM5xNYhyrw(b)
  {
    var c;

    c = !(b.Target == null);

    if (!c)
    {
      b.Target = _7hIABr5xMzijfM5xNYhyrw(_3xIABr5xMzijfM5xNYhyrw(b.Stream, b.IsBase64));
    }

  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.ToConsole
  function FhMABr5xMzijfM5xNYhyrw(a)
  {
    var b, c, d, e, f, g;

    IA8ABqiGvTSwHMGw_bObBeQ('functions:');
    b = 20;
    d = _9RIABr5xMzijfM5xNYhyrw(a);

    for (e = 0; (e < d.length); e++)
    {
      c = d[e];
      IA8ABqiGvTSwHMGw_bObBeQ(jBMABjDeCj_aRJzaBmU9SJg(c.Name, b));
    }

    IA8ABqiGvTSwHMGw_bObBeQ('fields:');
    d = _9BIABr5xMzijfM5xNYhyrw(a);

    for (e = 0; (e < d.length); e++)
    {
      c = d[e];
      g = [
        jBMABjDeCj_aRJzaBmU9SJg(c.Name, b),
        ' = (',
        AxMABr5xMzijfM5xNYhyrw(c._1hIABu_aiVzem_agWG9y0ZrA()),
        ')',
        c._0RIABu_aiVzem_agWG9y0ZrA()
      ];
      IA8ABqiGvTSwHMGw_bObBeQ(exMABjDeCj_aRJzaBmU9SJg(g));
    }

  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.InternalContains
  function FxMABr5xMzijfM5xNYhyrw(m, t) { return (m in t); };
  // ScriptCoreLib.JavaScript.Runtime.Expando.Contains
  function GBMABr5xMzijfM5xNYhyrw(a, b)
  {
    var c;

    c = FxMABr5xMzijfM5xNYhyrw(b, a);
    return c;
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.CopyTo
  function GRMABr5xMzijfM5xNYhyrw(a, b)
  {
    var c, d, e, f, g;

    c = BBMABr5xMzijfM5xNYhyrw(b);
    e = _8xIABr5xMzijfM5xNYhyrw(a);

    for (f = 0; (f < e.length); f++)
    {
      d = e[f];
      d._1xIABu_aiVzem_agWG9y0ZrA(c);
    }

  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.InternalRemove
  function GhMABr5xMzijfM5xNYhyrw(t, key) { delete t[key]; };
  // ScriptCoreLib.JavaScript.Runtime.Expando.Remove
  function GxMABr5xMzijfM5xNYhyrw(a, b)
  {
    GhMABr5xMzijfM5xNYhyrw(a, b);
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando.InternalRemoveAll
  function HBMABr5xMzijfM5xNYhyrw(t) { for (var i in t) delete t[i]; };
  // ScriptCoreLib.JavaScript.Runtime.Expando.RemoveAll
  // ScriptCoreLib.JavaScript.Runtime.Expando+TypeNameResolver
  function __aNGDcaRTLzadJH5YDggP_bg(){};
  __aNGDcaRTLzadJH5YDggP_bg.TypeName = "TypeNameResolver";
  __aNGDcaRTLzadJH5YDggP_bg.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$__aNGDcaRTLzadJH5YDggP_bg = __aNGDcaRTLzadJH5YDggP_bg.prototype;
  type$__aNGDcaRTLzadJH5YDggP_bg.constructor = __aNGDcaRTLzadJH5YDggP_bg;
  type$__aNGDcaRTLzadJH5YDggP_bg.Type = null;
  type$__aNGDcaRTLzadJH5YDggP_bg.TypeName = null;
  var basector$__aNGDcaRTLzadJH5YDggP_bg = $ctor$(null, null, type$__aNGDcaRTLzadJH5YDggP_bg);
  // ScriptCoreLib.JavaScript.Runtime.Expando+TypeNameResolver..ctor
  type$__aNGDcaRTLzadJH5YDggP_bg.HhMABqRTLzadJH5YDggP_bg = function (b, c)
  {
    var a = this;

    a.Type = b;
    a.TypeName = c;
  };
  var ctor$HhMABqRTLzadJH5YDggP_bg = $ctor$(null, 'HhMABqRTLzadJH5YDggP_bg', type$__aNGDcaRTLzadJH5YDggP_bg);

  // ScriptCoreLib.JavaScript.Runtime.Expando+TypeActivator
  function cUs1Lnmu9zC8e6oGJlSK_bQ(){};
  cUs1Lnmu9zC8e6oGJlSK_bQ.TypeName = "TypeActivator";
  cUs1Lnmu9zC8e6oGJlSK_bQ.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$cUs1Lnmu9zC8e6oGJlSK_bQ = cUs1Lnmu9zC8e6oGJlSK_bQ.prototype;
  type$cUs1Lnmu9zC8e6oGJlSK_bQ.constructor = cUs1Lnmu9zC8e6oGJlSK_bQ;
  type$cUs1Lnmu9zC8e6oGJlSK_bQ.Type = null;
  type$cUs1Lnmu9zC8e6oGJlSK_bQ.TypeName = null;
  type$cUs1Lnmu9zC8e6oGJlSK_bQ.MemberActivator = null;
  var basector$cUs1Lnmu9zC8e6oGJlSK_bQ = $ctor$(null, null, type$cUs1Lnmu9zC8e6oGJlSK_bQ);
  // ScriptCoreLib.JavaScript.Runtime.Expando+TypeActivator..ctor
  type$cUs1Lnmu9zC8e6oGJlSK_bQ.IBMABnmu9zC8e6oGJlSK_bQ = function (b)
  {
    var a = this;

    a.MemberActivator = _6BIABr5xMzijfM5xNYhyrw();
    a.TypeName = b;
  };
  var ctor$IBMABnmu9zC8e6oGJlSK_bQ = $ctor$(null, 'IBMABnmu9zC8e6oGJlSK_bQ', type$cUs1Lnmu9zC8e6oGJlSK_bQ);

  // ScriptCoreLib.JavaScript.Runtime.Expando+TypeActivator.get_TypeExpando
  type$cUs1Lnmu9zC8e6oGJlSK_bQ.HxMABnmu9zC8e6oGJlSK_bQ = function ()
  {
    var a = this, b;

    b = BBMABr5xMzijfM5xNYhyrw(a.Type);
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando+TypeActivator.set_Item
  type$cUs1Lnmu9zC8e6oGJlSK_bQ.IRMABnmu9zC8e6oGJlSK_bQ = function (b, c)
  {
    var a = this;

    CRMABr5xMzijfM5xNYhyrw(a.MemberActivator, b, c);
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando+TypeActivator.get_Item
  type$cUs1Lnmu9zC8e6oGJlSK_bQ.IhMABnmu9zC8e6oGJlSK_bQ = function (b)
  {
    var a = this, c;

    c = CBMABr5xMzijfM5xNYhyrw(a.MemberActivator, b);
    return c;
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando+FindArgs`1
  function OnqCTfqhLTy1Lxz2DoHTSw(){};
  OnqCTfqhLTy1Lxz2DoHTSw.TypeName = "FindArgs_1";
  OnqCTfqhLTy1Lxz2DoHTSw.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$OnqCTfqhLTy1Lxz2DoHTSw = OnqCTfqhLTy1Lxz2DoHTSw.prototype;
  type$OnqCTfqhLTy1Lxz2DoHTSw.constructor = OnqCTfqhLTy1Lxz2DoHTSw;
  type$OnqCTfqhLTy1Lxz2DoHTSw.Found = false;
  type$OnqCTfqhLTy1Lxz2DoHTSw.Member = null;
  type$OnqCTfqhLTy1Lxz2DoHTSw.Item = null;
  var basector$OnqCTfqhLTy1Lxz2DoHTSw = $ctor$(null, null, type$OnqCTfqhLTy1Lxz2DoHTSw);
  // ScriptCoreLib.JavaScript.Runtime.Expando+FindArgs`1..ctor
  type$OnqCTfqhLTy1Lxz2DoHTSw.IxMABvqhLTy1Lxz2DoHTSw = function ()
  {
    var a = this;

    a.Found = 0;
  };
  var ctor$IxMABvqhLTy1Lxz2DoHTSw = OnqCTfqhLTy1Lxz2DoHTSw.ctor = $ctor$(null, 'IxMABvqhLTy1Lxz2DoHTSw', type$OnqCTfqhLTy1Lxz2DoHTSw);

  // ScriptCoreLib.JavaScript.Runtime.Expando`2.InternalConstructor
  function JRMABlPGPDagr49yP4mjlg()
  {
    var b;

    b = _7hIABr5xMzijfM5xNYhyrw(_6BIABr5xMzijfM5xNYhyrw());
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando`2.Of
  function JhMABlPGPDagr49yP4mjlg(b)
  {
    var c;

    c = _7hIABr5xMzijfM5xNYhyrw(BBMABr5xMzijfM5xNYhyrw(b));
    return c;
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando`2.set_Item
  function JxMABlPGPDagr49yP4mjlg(a, b, c)
  {
    BxMABr5xMzijfM5xNYhyrw(a, b, c);
  };

  // ScriptCoreLib.JavaScript.Runtime.Expando`2.get_Item
  function KBMABlPGPDagr49yP4mjlg(a, b)
  {
    var c;

    c = BhMABr5xMzijfM5xNYhyrw(a, b);
    return c;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLTableRow.InternalConstructor
  function KxMABi8HaDq3hBxD6kJSBA()
  {
    var b;

    b = document.createElement('tr');
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLTableRow.InternalConstructor
  function LBMABi8HaDq3hBxD6kJSBA(b)
  {
    var c, d;

    c = KxMABi8HaDq3hBxD6kJSBA();
    PwAABlWwyD6MDQKkPtM6Fg(c, b);
    d = c;
    return d;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLTableRow.AddColumn
  function LRMABi8HaDq3hBxD6kJSBA(a)
  {
    var b, c;

    b = bREABmaO1zqNdjJHoCqWPg();
    a.appendChild(b);
    c = b;
    return c;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLTableRow.AddColumn
  function LhMABi8HaDq3hBxD6kJSBA(a, b)
  {
    var c, d;

    c = bREABmaO1zqNdjJHoCqWPg();
    c.innerHTML = b;
    a.appendChild(c);
    d = c;
    return d;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLTableRow.AddColumn
  function LxMABi8HaDq3hBxD6kJSBA(a, b)
  {
    var c, d;

    c = bhEABmaO1zqNdjJHoCqWPg(b);
    a.appendChild(c);
    d = c;
    return d;
  };

  // ScriptCoreLib.JavaScript.Serialized.ObjectStreamHelper`1
  function n8c_b14YPyT_a_afjO7wLrjcQ(){};
  n8c_b14YPyT_a_afjO7wLrjcQ.TypeName = "ObjectStreamHelper_1";
  n8c_b14YPyT_a_afjO7wLrjcQ.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$n8c_b14YPyT_a_afjO7wLrjcQ = n8c_b14YPyT_a_afjO7wLrjcQ.prototype;
  type$n8c_b14YPyT_a_afjO7wLrjcQ.constructor = n8c_b14YPyT_a_afjO7wLrjcQ;
  type$n8c_b14YPyT_a_afjO7wLrjcQ._Stream = null;
  type$n8c_b14YPyT_a_afjO7wLrjcQ._Item = null;
  var basector$n8c_b14YPyT_a_afjO7wLrjcQ = $ctor$(null, null, type$n8c_b14YPyT_a_afjO7wLrjcQ);
  // ScriptCoreLib.JavaScript.Serialized.ObjectStreamHelper`1..ctor
  type$n8c_b14YPyT_a_afjO7wLrjcQ.QhMABoYPyT_a_afjO7wLrjcQ = function ()
  {
    var a = this;

  };
  var ctor$QhMABoYPyT_a_afjO7wLrjcQ = n8c_b14YPyT_a_afjO7wLrjcQ.ctor = $ctor$(null, 'QhMABoYPyT_a_afjO7wLrjcQ', type$n8c_b14YPyT_a_afjO7wLrjcQ);

  // ScriptCoreLib.JavaScript.Serialized.ObjectStreamHelper`1.get_Stream
  type$n8c_b14YPyT_a_afjO7wLrjcQ.PhMABoYPyT_a_afjO7wLrjcQ = function ()
  {
    var a = this, b;

    b = a._Stream;
    return b;
  };

  // ScriptCoreLib.JavaScript.Serialized.ObjectStreamHelper`1.set_Stream
  type$n8c_b14YPyT_a_afjO7wLrjcQ.PxMABoYPyT_a_afjO7wLrjcQ = function (b)
  {
    var a = this;

    a._Stream = b;
    a._Item = ZQgABl6xZjKlcaFaQuMTTA(b, 1);
  };

  // ScriptCoreLib.JavaScript.Serialized.ObjectStreamHelper`1.get_Item
  type$n8c_b14YPyT_a_afjO7wLrjcQ.QBMABoYPyT_a_afjO7wLrjcQ = function ()
  {
    var a = this, b;

    b = a._Item;
    return b;
  };

  // ScriptCoreLib.JavaScript.Serialized.ObjectStreamHelper`1.set_Item
  type$n8c_b14YPyT_a_afjO7wLrjcQ.QRMABoYPyT_a_afjO7wLrjcQ = function (b)
  {
    var a = this;

    a._Item = b;
    a._Stream = YAgABl6xZjKlcaFaQuMTTA(_2RIABr5xMzijfM5xNYhyrw(BBMABr5xMzijfM5xNYhyrw(a._Item)));
  };

  // 
  // ScriptCoreLib.JavaScript.Serialized.ObjectStreamHelper`1
  (function (i)  {
    i.ewgABpyfmDS26OJgOaz_baA = i.PhMABoYPyT_a_afjO7wLrjcQ;
    i.fAgABpyfmDS26OJgOaz_baA = i.PxMABoYPyT_a_afjO7wLrjcQ;
    i.fQgABpyfmDS26OJgOaz_baA = i.QBMABoYPyT_a_afjO7wLrjcQ;
    i.fggABpyfmDS26OJgOaz_baA = i.QRMABoYPyT_a_afjO7wLrjcQ;
  }
  )(type$n8c_b14YPyT_a_afjO7wLrjcQ);
  // ScriptCoreLib.JavaScript.Runtime.WorkPool
  function YfMQ2NwijzGJDK4R_a8wyvw(){};
  YfMQ2NwijzGJDK4R_a8wyvw.TypeName = "WorkPool";
  YfMQ2NwijzGJDK4R_a8wyvw.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$YfMQ2NwijzGJDK4R_a8wyvw = YfMQ2NwijzGJDK4R_a8wyvw.prototype;
  type$YfMQ2NwijzGJDK4R_a8wyvw.constructor = YfMQ2NwijzGJDK4R_a8wyvw;
  type$YfMQ2NwijzGJDK4R_a8wyvw.List = null;
  type$YfMQ2NwijzGJDK4R_a8wyvw.Worker = null;
  type$YfMQ2NwijzGJDK4R_a8wyvw.Interval = 0;
  type$YfMQ2NwijzGJDK4R_a8wyvw.Timeout = 0;
  type$YfMQ2NwijzGJDK4R_a8wyvw.Abort = null;
  type$YfMQ2NwijzGJDK4R_a8wyvw.Error = null;
  var basector$YfMQ2NwijzGJDK4R_a8wyvw = $ctor$(null, null, type$YfMQ2NwijzGJDK4R_a8wyvw);
  // ScriptCoreLib.JavaScript.Runtime.WorkPool..ctor
  type$YfMQ2NwijzGJDK4R_a8wyvw.QxMABtwijzGJDK4R_a8wyvw = function (b)
  {
    var a = this;

    a.RBMABtwijzGJDK4R_a8wyvw();
    a.Interval = b;
  };
  var ctor$QxMABtwijzGJDK4R_a8wyvw = $ctor$(null, 'QxMABtwijzGJDK4R_a8wyvw', type$YfMQ2NwijzGJDK4R_a8wyvw);

  // ScriptCoreLib.JavaScript.Runtime.WorkPool..ctor
  type$YfMQ2NwijzGJDK4R_a8wyvw.RBMABtwijzGJDK4R_a8wyvw = function ()
  {
    var a = this;

    a.List = new ctor$swAABnGXyTaWJhb6CcyWQQ();
    a.Worker = new ctor$_1QIABilzizCnsZekUeVmgA();
    a.Interval = 100;
    a.Timeout = 5000;
    a.Worker._0wIABilzizCnsZekUeVmgA(new ctor$KQsABoVjnjetV_aXG4GFkBQ(a, 'SRMABtwijzGJDK4R_a8wyvw'));
  };
  var ctor$RBMABtwijzGJDK4R_a8wyvw = YfMQ2NwijzGJDK4R_a8wyvw.ctor = $ctor$(null, 'RBMABtwijzGJDK4R_a8wyvw', type$YfMQ2NwijzGJDK4R_a8wyvw);

  // ScriptCoreLib.JavaScript.Runtime.WorkPool.add_Abort
  type$YfMQ2NwijzGJDK4R_a8wyvw.RRMABtwijzGJDK4R_a8wyvw = function (b)
  {
    var a = this;

    a.Abort = fwsABqgPxjmDnjkmC_a5nbw(a.Abort, b);
  };

  // ScriptCoreLib.JavaScript.Runtime.WorkPool.remove_Abort
  type$YfMQ2NwijzGJDK4R_a8wyvw.RhMABtwijzGJDK4R_a8wyvw = function (b)
  {
    var a = this;

    a.Abort = gQsABqgPxjmDnjkmC_a5nbw(a.Abort, b);
  };

  // ScriptCoreLib.JavaScript.Runtime.WorkPool.add_Error
  type$YfMQ2NwijzGJDK4R_a8wyvw.RxMABtwijzGJDK4R_a8wyvw = function (b)
  {
    var a = this;

    a.Error = fwsABqgPxjmDnjkmC_a5nbw(a.Error, b);
  };

  // ScriptCoreLib.JavaScript.Runtime.WorkPool.remove_Error
  type$YfMQ2NwijzGJDK4R_a8wyvw.SBMABtwijzGJDK4R_a8wyvw = function (b)
  {
    var a = this;

    a.Error = gQsABqgPxjmDnjkmC_a5nbw(a.Error, b);
  };

  // ScriptCoreLib.JavaScript.Runtime.WorkPool.Worker_Tick
  type$YfMQ2NwijzGJDK4R_a8wyvw.SRMABtwijzGJDK4R_a8wyvw = function (b)
  {
    var a = this, c, d, e, f;

    try
    {
      c = a.List.ugAABnGXyTaWJhb6CcyWQQ(0);
      a.List.uQAABnGXyTaWJhb6CcyWQQ(0);
      d = Zg0ABijAJDClltROMHFY_bw().getTime();
      c.Handler.Invoke();
      f = !((Zg0ABijAJDClltROMHFY_bw().getTime() - d) > a.Timeout);

      if (!f)
      {
        IA8ABqiGvTSwHMGw_bObBeQ('workpool timeout exceeded');
        sQgABrDT9zCTidQoMa3Dig(a.Abort, a);
        a.List.vwAABnGXyTaWJhb6CcyWQQ();
      }

    }
    catch (__exc)
    {
      e = __exc;
      f = (a.Error == null);

      if (!f)
      {
        a.Error.Invoke(e);
      }

    }
    a.TxMABtwijzGJDK4R_a8wyvw();
  };

  // ScriptCoreLib.JavaScript.Runtime.WorkPool.op_Addition
  function ShMABtwijzGJDK4R_a8wyvw(b, c)
  {
    var d;

    b.SxMABtwijzGJDK4R_a8wyvw(c);
    d = b;
    return d;
  };

  // ScriptCoreLib.JavaScript.Runtime.WorkPool.Add
  type$YfMQ2NwijzGJDK4R_a8wyvw.SxMABtwijzGJDK4R_a8wyvw = function (b)
  {
    var a = this, c;

    c = new ctor$UBMABt_ao4Dqdi9QD1wOqFQ();
    c.Handler = b;
    a.List.vQAABnGXyTaWJhb6CcyWQQ(c);
    a.TxMABtwijzGJDK4R_a8wyvw();
  };

  // ScriptCoreLib.JavaScript.Runtime.WorkPool.set_Item
  type$YfMQ2NwijzGJDK4R_a8wyvw.TBMABtwijzGJDK4R_a8wyvw = function (b, c)
  {
    var a = this;

    a.ThMABtwijzGJDK4R_a8wyvw(b);
    a.TRMABtwijzGJDK4R_a8wyvw(c, b);
  };

  // ScriptCoreLib.JavaScript.Runtime.WorkPool.Add
  type$YfMQ2NwijzGJDK4R_a8wyvw.TRMABtwijzGJDK4R_a8wyvw = function (b, c)
  {
    var a = this, d;

    d = new ctor$UBMABt_ao4Dqdi9QD1wOqFQ();
    d.Handler = b;
    d.Key = c;
    a.List.vQAABnGXyTaWJhb6CcyWQQ(d);
    a.TxMABtwijzGJDK4R_a8wyvw();
  };

  // ScriptCoreLib.JavaScript.Runtime.WorkPool.Remove
  type$YfMQ2NwijzGJDK4R_a8wyvw.ThMABtwijzGJDK4R_a8wyvw = function (b)
  {
    var a = this, c;

    c = /* DOMCreateType */new grnA8PMxvjau_aUwl9tFniA();
    c.key = b;
    a.List.xQAABnGXyTaWJhb6CcyWQQ(new ctor$hRAABinIHzuOQ9HytTgcwQ(c, '_Remove_b__0'));
    a.TxMABtwijzGJDK4R_a8wyvw();
  };

  // ScriptCoreLib.JavaScript.Runtime.WorkPool.Touch
  type$YfMQ2NwijzGJDK4R_a8wyvw.TxMABtwijzGJDK4R_a8wyvw = function ()
  {
    var a = this, b;

    b = !(a.List.wgAABnGXyTaWJhb6CcyWQQ() > 0);

    if (!b)
    {
      a.Worker._3wIABilzizCnsZekUeVmgA(a.Interval);
      return;
    }

    a.Worker._4QIABilzizCnsZekUeVmgA();
  };

  // ScriptCoreLib.JavaScript.Runtime.WorkPool+EntryItem
  function FqsaKt_ao4Dqdi9QD1wOqFQ(){};
  FqsaKt_ao4Dqdi9QD1wOqFQ.TypeName = "EntryItem";
  FqsaKt_ao4Dqdi9QD1wOqFQ.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$FqsaKt_ao4Dqdi9QD1wOqFQ = FqsaKt_ao4Dqdi9QD1wOqFQ.prototype;
  type$FqsaKt_ao4Dqdi9QD1wOqFQ.constructor = FqsaKt_ao4Dqdi9QD1wOqFQ;
  type$FqsaKt_ao4Dqdi9QD1wOqFQ.Key = null;
  type$FqsaKt_ao4Dqdi9QD1wOqFQ.Handler = null;
  var basector$FqsaKt_ao4Dqdi9QD1wOqFQ = $ctor$(null, null, type$FqsaKt_ao4Dqdi9QD1wOqFQ);
  // ScriptCoreLib.JavaScript.Runtime.WorkPool+EntryItem..ctor
  type$FqsaKt_ao4Dqdi9QD1wOqFQ.UBMABt_ao4Dqdi9QD1wOqFQ = function ()
  {
    var a = this;

  };
  var ctor$UBMABt_ao4Dqdi9QD1wOqFQ = FqsaKt_ao4Dqdi9QD1wOqFQ.ctor = $ctor$(null, 'UBMABt_ao4Dqdi9QD1wOqFQ', type$FqsaKt_ao4Dqdi9QD1wOqFQ);

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLIFrame.InternalConstructor
  function UhMABndWODWM_a2H5kzsXXg()
  {
    var b;

    b = UQAABtcctzqgvmwoTNwgqg('iframe');
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLIFrame.add_onload
  function UxMABndWODWM_a2H5kzsXXg(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 1, b, 'load');
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLIFrame.remove_onload
  function VBMABndWODWM_a2H5kzsXXg(a, b)
  {
    LwAABl_atOz_aFJQVdjiI7RQ(a, 0, b, 'load');
  };

  // ScriptCoreLib.JavaScript.Controls.DragHelper
  function rPcJWj_bUMzuUZVYhmc9fzQ(){};
  rPcJWj_bUMzuUZVYhmc9fzQ.TypeName = "DragHelper";
  rPcJWj_bUMzuUZVYhmc9fzQ.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$rPcJWj_bUMzuUZVYhmc9fzQ = rPcJWj_bUMzuUZVYhmc9fzQ.prototype;
  type$rPcJWj_bUMzuUZVYhmc9fzQ.constructor = rPcJWj_bUMzuUZVYhmc9fzQ;
  type$rPcJWj_bUMzuUZVYhmc9fzQ.IsDrag = false;
  type$rPcJWj_bUMzuUZVYhmc9fzQ.Position = null;
  type$rPcJWj_bUMzuUZVYhmc9fzQ.OffsetPosition = null;
  type$rPcJWj_bUMzuUZVYhmc9fzQ.DragStartValidate = null;
  type$rPcJWj_bUMzuUZVYhmc9fzQ.DragStart = null;
  type$rPcJWj_bUMzuUZVYhmc9fzQ.DragMove = null;
  type$rPcJWj_bUMzuUZVYhmc9fzQ.MiddleClick = null;
  type$rPcJWj_bUMzuUZVYhmc9fzQ.DragMoveFilter = null;
  type$rPcJWj_bUMzuUZVYhmc9fzQ.DragStop = null;
  type$rPcJWj_bUMzuUZVYhmc9fzQ.Control = null;
  type$rPcJWj_bUMzuUZVYhmc9fzQ.ondocumentmousemove = null;
  type$rPcJWj_bUMzuUZVYhmc9fzQ.ondocumentmouseup = null;
  type$rPcJWj_bUMzuUZVYhmc9fzQ.onmousedown = null;
  type$rPcJWj_bUMzuUZVYhmc9fzQ.History = null;
  type$rPcJWj_bUMzuUZVYhmc9fzQ._Enabled = false;
  type$rPcJWj_bUMzuUZVYhmc9fzQ.DragStartCursorPosition = null;
  type$rPcJWj_bUMzuUZVYhmc9fzQ.HoverTime = 0;
  var basector$rPcJWj_bUMzuUZVYhmc9fzQ = $ctor$(null, null, type$rPcJWj_bUMzuUZVYhmc9fzQ);
  // ScriptCoreLib.JavaScript.Controls.DragHelper..ctor
  type$rPcJWj_bUMzuUZVYhmc9fzQ.YRMABj_bUMzuUZVYhmc9fzQ = function (b)
  {
    var a = this, c, d, e;

    c = null;
    d = null;
    e = null;
    a.Position = new ctor$xhQABoqVzDa8F5yDSGjsZA(0, 0);
    a.OffsetPosition = new ctor$xhQABoqVzDa8F5yDSGjsZA(0, 0);
    a.DragMoveFilter = new ctor$ew0ABqpAQj_apuj6uPJmsSA(30);
    a.DragStartCursorPosition = new ctor$xhQABoqVzDa8F5yDSGjsZA(0, 0);
    a.HoverTime = 1000;
    a.Control = b;

    if (!c)
    {
      c = new ctor$KQsABoVjnjetV_aXG4GFkBQ(a, 'YxMABj_bUMzuUZVYhmc9fzQ');
    }

    a.ondocumentmousemove = c;

    if (!d)
    {
      d = new ctor$KQsABoVjnjetV_aXG4GFkBQ(a, 'ZBMABj_bUMzuUZVYhmc9fzQ');
    }

    a.ondocumentmouseup = d;

    if (!e)
    {
      e = new ctor$KQsABoVjnjetV_aXG4GFkBQ(a, 'ZRMABj_bUMzuUZVYhmc9fzQ');
    }

    a.onmousedown = fwsABqgPxjmDnjkmC_a5nbw(a.onmousedown, e);
  };
  var ctor$YRMABj_bUMzuUZVYhmc9fzQ = $ctor$(null, 'YRMABj_bUMzuUZVYhmc9fzQ', type$rPcJWj_bUMzuUZVYhmc9fzQ);

  // ScriptCoreLib.JavaScript.Controls.DragHelper.add_DragStartValidate
  type$rPcJWj_bUMzuUZVYhmc9fzQ.VRMABj_bUMzuUZVYhmc9fzQ = function (b)
  {
    var a = this;

    a.DragStartValidate = fwsABqgPxjmDnjkmC_a5nbw(a.DragStartValidate, b);
  };

  // ScriptCoreLib.JavaScript.Controls.DragHelper.remove_DragStartValidate
  type$rPcJWj_bUMzuUZVYhmc9fzQ.VhMABj_bUMzuUZVYhmc9fzQ = function (b)
  {
    var a = this;

    a.DragStartValidate = gQsABqgPxjmDnjkmC_a5nbw(a.DragStartValidate, b);
  };

  // ScriptCoreLib.JavaScript.Controls.DragHelper.add_DragStart
  type$rPcJWj_bUMzuUZVYhmc9fzQ.VxMABj_bUMzuUZVYhmc9fzQ = function (b)
  {
    var a = this;

    a.DragStart = fwsABqgPxjmDnjkmC_a5nbw(a.DragStart, b);
  };

  // ScriptCoreLib.JavaScript.Controls.DragHelper.remove_DragStart
  type$rPcJWj_bUMzuUZVYhmc9fzQ.WBMABj_bUMzuUZVYhmc9fzQ = function (b)
  {
    var a = this;

    a.DragStart = gQsABqgPxjmDnjkmC_a5nbw(a.DragStart, b);
  };

  // ScriptCoreLib.JavaScript.Controls.DragHelper.add_DragMove
  type$rPcJWj_bUMzuUZVYhmc9fzQ.WRMABj_bUMzuUZVYhmc9fzQ = function (b)
  {
    var a = this;

    a.DragMove = fwsABqgPxjmDnjkmC_a5nbw(a.DragMove, b);
  };

  // ScriptCoreLib.JavaScript.Controls.DragHelper.remove_DragMove
  type$rPcJWj_bUMzuUZVYhmc9fzQ.WhMABj_bUMzuUZVYhmc9fzQ = function (b)
  {
    var a = this;

    a.DragMove = gQsABqgPxjmDnjkmC_a5nbw(a.DragMove, b);
  };

  // ScriptCoreLib.JavaScript.Controls.DragHelper.add_MiddleClick
  type$rPcJWj_bUMzuUZVYhmc9fzQ.WxMABj_bUMzuUZVYhmc9fzQ = function (b)
  {
    var a = this;

    a.MiddleClick = fwsABqgPxjmDnjkmC_a5nbw(a.MiddleClick, b);
  };

  // ScriptCoreLib.JavaScript.Controls.DragHelper.remove_MiddleClick
  type$rPcJWj_bUMzuUZVYhmc9fzQ.XBMABj_bUMzuUZVYhmc9fzQ = function (b)
  {
    var a = this;

    a.MiddleClick = gQsABqgPxjmDnjkmC_a5nbw(a.MiddleClick, b);
  };

  // ScriptCoreLib.JavaScript.Controls.DragHelper.add_DragStop
  type$rPcJWj_bUMzuUZVYhmc9fzQ.XRMABj_bUMzuUZVYhmc9fzQ = function (b)
  {
    var a = this;

    a.DragStop = fwsABqgPxjmDnjkmC_a5nbw(a.DragStop, b);
  };

  // ScriptCoreLib.JavaScript.Controls.DragHelper.remove_DragStop
  type$rPcJWj_bUMzuUZVYhmc9fzQ.XhMABj_bUMzuUZVYhmc9fzQ = function (b)
  {
    var a = this;

    a.DragStop = gQsABqgPxjmDnjkmC_a5nbw(a.DragStop, b);
  };

  // ScriptCoreLib.JavaScript.Controls.DragHelper.get_Enabled
  type$rPcJWj_bUMzuUZVYhmc9fzQ.XxMABj_bUMzuUZVYhmc9fzQ = function ()
  {
    var a = this, b;

    b = a._Enabled;
    return b;
  };

  // ScriptCoreLib.JavaScript.Controls.DragHelper.set_Enabled
  type$rPcJWj_bUMzuUZVYhmc9fzQ.YBMABj_bUMzuUZVYhmc9fzQ = function (b)
  {
    var a = this, c;

    c = (a._Enabled == b);

    if (!c)
    {
      c = !b;

      if (!c)
      {
        ZQAABtcctzqgvmwoTNwgqg(a.Control, a.onmousedown);
      }
      else
      {
        ZgAABtcctzqgvmwoTNwgqg(a.Control, a.onmousedown);
      }

    }

    a._Enabled = b;
  };

  // ScriptCoreLib.JavaScript.Controls.DragHelper.DragTo
  type$rPcJWj_bUMzuUZVYhmc9fzQ.YhMABj_bUMzuUZVYhmc9fzQ = function (b)
  {
    var a = this, c;

    c = /* DOMCreateType */new XDckZ6nFGzGCpT3OQKCfFA();
    c.point = b;
    c.__4__this = a;
    a.DragMoveFilter.fQ0ABqpAQj_apuj6uPJmsSA(new ctor$LQsABharvz6dEOdzk7hI5Q(c, '_DragTo_b__6'));
  };

  // ScriptCoreLib.JavaScript.Controls.DragHelper.<.ctor>b__0
  type$rPcJWj_bUMzuUZVYhmc9fzQ.YxMABj_bUMzuUZVYhmc9fzQ = function (b)
  {
    var a = this;

    a.YhMABj_bUMzuUZVYhmc9fzQ(yxQABoqVzDa8F5yDSGjsZA(_2RQABmvdsDi9SJKe_axnpQQ(b), a.OffsetPosition));
  };

  // ScriptCoreLib.JavaScript.Controls.DragHelper.<.ctor>b__1
  type$rPcJWj_bUMzuUZVYhmc9fzQ.ZBMABj_bUMzuUZVYhmc9fzQ = function (b)
  {
    var a = this, c, d;

    c = yxQABoqVzDa8F5yDSGjsZA(a.DragStartCursorPosition, _2RQABmvdsDi9SJKe_axnpQQ(b));
    a.IsDrag = 0;
    sggABrDT9zCTidQoMa3Dig(a.DragStop);
    GQoABrOeqzW195CXZ74N9w(document, a.ondocumentmousemove);
    HQoABrOeqzW195CXZ74N9w(document, a.ondocumentmouseup);
    d = !(_3xQABmvdsDi9SJKe_axnpQQ(b) == 2);

    if (!d)
    {
      d = !(c._0RQABoqVzDa8F5yDSGjsZA() < 128);

      if (!d)
      {
        sggABrDT9zCTidQoMa3Dig(a.MiddleClick);
      }

    }

  };

  // ScriptCoreLib.JavaScript.Controls.DragHelper.<.ctor>b__2
  type$rPcJWj_bUMzuUZVYhmc9fzQ.ZRMABj_bUMzuUZVYhmc9fzQ = function (b)
  {
    var a = this, c, d;

    a.DragStartCursorPosition = _2RQABmvdsDi9SJKe_axnpQQ(b);
    c = new ctor$SwsABgF8xDasrw7uIe6gQA();
    c.Value = 1;
    c.RQsABgF8xDasrw7uIe6gQA(a.DragStartValidate);
    d = c.Value;

    if (!d)
    {
      return;
    }

    d = (a.History == null);

    if (!d)
    {
      a.History.vQAABnGXyTaWJhb6CcyWQQ(a.Position);
    }

    a.OffsetPosition = yxQABoqVzDa8F5yDSGjsZA(_2RQABmvdsDi9SJKe_axnpQQ(b), a.Position);
    a.IsDrag = 1;
    sggABrDT9zCTidQoMa3Dig(a.DragStart);
    GAoABrOeqzW195CXZ74N9w(document, a.ondocumentmousemove);
    HAoABrOeqzW195CXZ74N9w(document, a.ondocumentmouseup);
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.InternalConstructor
  function ZxMABjDeCj_aRJzaBmU9SJg(b, c)
  {
    var d, e, f, g;

    d = new ctor$_1REABhHFijqvYJf1nSns2Q();

    for (e = 0; (e < c); e++)
    {
      d._2hEABhHFijqvYJf1nSns2Q(bRMABjDeCj_aRJzaBmU9SJg(b));
    }

    f = (d+'');
    return f;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.Format
  function aBMABjDeCj_aRJzaBmU9SJg(b, c)
  {
    var d;

    d = hBMABjDeCj_aRJzaBmU9SJg(b, '{0}', fRMABjDeCj_aRJzaBmU9SJg(c));
    return d;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.Format
  function aRMABjDeCj_aRJzaBmU9SJg(b, c, d)
  {
    var e;

    e = hBMABjDeCj_aRJzaBmU9SJg(hBMABjDeCj_aRJzaBmU9SJg(b, '{0}', fRMABjDeCj_aRJzaBmU9SJg(c)), '{1}', fRMABjDeCj_aRJzaBmU9SJg(d));
    return e;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.Format
  function ahMABjDeCj_aRJzaBmU9SJg(b, c)
  {
    var d, e, f, g;

    d = b;

    for (e = 0; (e < c.length); e++)
    {
      d = hBMABjDeCj_aRJzaBmU9SJg(d, fxMABjDeCj_aRJzaBmU9SJg('{', new Number(e), '}'), (c[e]+''));
    }

    f = d;
    return f;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.IsNullOrEmpty
  function axMABjDeCj_aRJzaBmU9SJg(b)
  {
    var c, d;

    d = !(b == null);

    if (!d)
    {
      c = 1;
      return c;
    }

    d = !lxMABjDeCj_aRJzaBmU9SJg(b, '');

    if (!d)
    {
      c = 1;
      return c;
    }

    c = 0;
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.GetCharCodeAt
  function bBMABjDeCj_aRJzaBmU9SJg(e, o) { return e.charCodeAt(o); };
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.FromCharCode
  function bRMABjDeCj_aRJzaBmU9SJg(i) { return String.fromCharCode(i); };
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.CompareTo
  function bhMABjDeCj_aRJzaBmU9SJg(a, b)
  {
    var c;

    c = _4RIABr5xMzijfM5xNYhyrw(a, b);
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.InternalCharAt
  function bxMABjDeCj_aRJzaBmU9SJg(e, i) { return e.charAt(i); };
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.InternalLength
  function cBMABjDeCj_aRJzaBmU9SJg(e) { return e.length; };
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.InternalLastIndexOf
  function cRMABjDeCj_aRJzaBmU9SJg(e, c) { return e.lastIndexOf(c); };
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.InternalIndexOf
  function chMABjDeCj_aRJzaBmU9SJg(e, c) { return e.indexOf(c); };
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.InternalIndexOf
  function cxMABjDeCj_aRJzaBmU9SJg(e, c, pos) { return e.indexOf(c, pos); };
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.LastIndexOf
  function dBMABjDeCj_aRJzaBmU9SJg(a, b)
  {
    var c;

    c = cRMABjDeCj_aRJzaBmU9SJg(a, b);
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.IndexOf
  function dRMABjDeCj_aRJzaBmU9SJg(a, b)
  {
    var c;

    c = chMABjDeCj_aRJzaBmU9SJg(a, b);
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.IndexOf
  function dhMABjDeCj_aRJzaBmU9SJg(a, b)
  {
    var c;

    c = chMABjDeCj_aRJzaBmU9SJg(a, b);
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.IndexOf
  function dxMABjDeCj_aRJzaBmU9SJg(a, b, c)
  {
    var d;

    d = cxMABjDeCj_aRJzaBmU9SJg(a, b, c);
    return d;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.get_Length
  function eBMABjDeCj_aRJzaBmU9SJg(a)
  {
    var b;

    b = cBMABjDeCj_aRJzaBmU9SJg(a);
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.get_Chars
  function eRMABjDeCj_aRJzaBmU9SJg(a, b)
  {
    var c;

    c = bBMABjDeCj_aRJzaBmU9SJg(a, b);
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.Contains
  function ehMABjDeCj_aRJzaBmU9SJg(a, b)
  {
    var c;

    c = (chMABjDeCj_aRJzaBmU9SJg(a, b) > -1);
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.Concat
  function exMABjDeCj_aRJzaBmU9SJg(a0) { return a0.join(''); };
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.Concat
  function fBMABjDeCj_aRJzaBmU9SJg(a0) { return a0.join(''); };
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.Concat
  function fRMABjDeCj_aRJzaBmU9SJg(a0) { return a0+''; };
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.Concat
  function fhMABjDeCj_aRJzaBmU9SJg(a0, a1) { return a0+a1 };
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.Concat
  function fxMABjDeCj_aRJzaBmU9SJg(a0, a1, a2) { return a0+a1+a2 };
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.Concat
  function gBMABjDeCj_aRJzaBmU9SJg(a0, a1) { return a0+a1 };
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.Concat
  function gRMABjDeCj_aRJzaBmU9SJg(a0, a1, a2) { return a0+a1+a2 };
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.Concat
  function ghMABjDeCj_aRJzaBmU9SJg(a0, a1, a2, a3) { return a0+a1+a2+a3 };
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.InternalReplace
  function gxMABjDeCj_aRJzaBmU9SJg(a, a0, a1, a2) { return a0.split(a1).join(a2) }
;  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.Replace
  function hBMABjDeCj_aRJzaBmU9SJg(a, b, c)
  {
    var d;

    d = gxMABjDeCj_aRJzaBmU9SJg(a, a, b, c);
    return d;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.Join
  function hRMABjDeCj_aRJzaBmU9SJg(a0, a1) { return a1.join(a0); };
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.toLowerCase
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.toUpperCase
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.ToLower
  function iBMABjDeCj_aRJzaBmU9SJg(a)
  {
    var b;

    b = a.toLowerCase();
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.ToUpper
  function iRMABjDeCj_aRJzaBmU9SJg(a)
  {
    var b;

    b = a.toUpperCase();
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.Trim
  function ihMABjDeCj_aRJzaBmU9SJg(a)
  {
    var b, c;

    c = !lxMABjDeCj_aRJzaBmU9SJg(a, null);

    if (!c)
    {
      b = null;
      return b;
    }

    b = _7hMABjToaTaQVWaeKug8LQ(_7xMABjToaTaQVWaeKug8LQ(), a, '');
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.PadRight
  function ixMABjDeCj_aRJzaBmU9SJg(a, b)
  {
    var c;

    c = jRMABjDeCj_aRJzaBmU9SJg(a, b, 32);
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.PadLeft
  function jBMABjDeCj_aRJzaBmU9SJg(a, b)
  {
    var c;

    c = jhMABjDeCj_aRJzaBmU9SJg(a, b, 32);
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.PadRight
  function jRMABjDeCj_aRJzaBmU9SJg(a, b, c)
  {
    var d, e, f;


    for (d = a; (eBMABjDeCj_aRJzaBmU9SJg(d) < b); d = gBMABjDeCj_aRJzaBmU9SJg(d, WggABl6xZjKlcaFaQuMTTA(c)))
    {
    }

    e = d;
    return e;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.PadLeft
  function jhMABjDeCj_aRJzaBmU9SJg(a, b, c)
  {
    var d, e, f;


    for (d = a; (eBMABjDeCj_aRJzaBmU9SJg(d) < b); d = gBMABjDeCj_aRJzaBmU9SJg(WggABl6xZjKlcaFaQuMTTA(c), d))
    {
    }

    e = d;
    return e;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.InternalSubstring
  function jxMABjDeCj_aRJzaBmU9SJg(a0, a1) { return a0.substr(a1); };
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.InternalSubstring
  function kBMABjDeCj_aRJzaBmU9SJg(a0, a1, a2) { return a0.substr(a1, a2); };
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.Substring
  function kRMABjDeCj_aRJzaBmU9SJg(a, b)
  {
    var c;

    c = jxMABjDeCj_aRJzaBmU9SJg(a, b);
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.Substring
  function khMABjDeCj_aRJzaBmU9SJg(a, b, c)
  {
    var d;

    d = kBMABjDeCj_aRJzaBmU9SJg(a, b, c);
    return d;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.Split
  function kxMABjDeCj_aRJzaBmU9SJg(a, b)
  {
    var c;

    c = MA4ABqPHMD_aEoYb_aoNHL9w(MQ4ABqPHMD_aEoYb_aoNHL9w(a, bRMABjDeCj_aRJzaBmU9SJg(b[0])));
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.Split
  function lBMABjDeCj_aRJzaBmU9SJg(a, b, c)
  {
    var d, e, f, g, h, i, j;

    h = (b.length == 1);

    if (!h)
    {
      throw nAAABq584TSQo69VDcfM9Q();
    }

    d = MQ4ABqPHMD_aEoYb_aoNHL9w(a, b[0]);
    h = !!c;

    if (!h)
    {
      g = MA4ABqPHMD_aEoYb_aoNHL9w(d);
      return g;
    }

    e = Hw4ABqPHMD_aEoYb_aoNHL9w();
    i = Lw4ABqPHMD_aEoYb_aoNHL9w(d);

    for (j = 0; (j < i.length); j++)
    {
      f = i[j];
      h = axMABjDeCj_aRJzaBmU9SJg(f);

      if (!h)
      {
        e.push(f);
      }

    }

    g = Lw4ABqPHMD_aEoYb_aoNHL9w(e);
    return g;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.EndsWith
  function lRMABjDeCj_aRJzaBmU9SJg(a, b)
  {
    var c;

    c = lxMABjDeCj_aRJzaBmU9SJg(jxMABjDeCj_aRJzaBmU9SJg(a, (eBMABjDeCj_aRJzaBmU9SJg(a) - eBMABjDeCj_aRJzaBmU9SJg(b))), b);
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.StartsWith
  function lhMABjDeCj_aRJzaBmU9SJg(a, b)
  {
    var c;

    c = lxMABjDeCj_aRJzaBmU9SJg(kBMABjDeCj_aRJzaBmU9SJg(a, 0, eBMABjDeCj_aRJzaBmU9SJg(b)), b);
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.op_Equality
  function lxMABjDeCj_aRJzaBmU9SJg(a, b) { return a == b };
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.Equals
  function mBMABjDeCj_aRJzaBmU9SJg(a, b)
  {
    var c;

    c = lxMABjDeCj_aRJzaBmU9SJg(a, b);
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.op_Inequality
  function mRMABjDeCj_aRJzaBmU9SJg(a, b) { return a != b };
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__String.GetHashCode
  function mhMABjDeCj_aRJzaBmU9SJg(a)
  {
    var b;

    b = a._5BEABmaCrzG0XVBXGP7j9g();
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.__ArrayList
  function WJieLjf1uTSfGmgy0X6ikQ(){};
  WJieLjf1uTSfGmgy0X6ikQ.TypeName = "ArrayList";
  WJieLjf1uTSfGmgy0X6ikQ.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$WJieLjf1uTSfGmgy0X6ikQ = WJieLjf1uTSfGmgy0X6ikQ.prototype;
  type$WJieLjf1uTSfGmgy0X6ikQ.constructor = WJieLjf1uTSfGmgy0X6ikQ;
  type$WJieLjf1uTSfGmgy0X6ikQ.items = null;
  var basector$WJieLjf1uTSfGmgy0X6ikQ = $ctor$(null, null, type$WJieLjf1uTSfGmgy0X6ikQ);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.__ArrayList..ctor
  type$WJieLjf1uTSfGmgy0X6ikQ.nBMABjf1uTSfGmgy0X6ikQ = function ()
  {
    var a = this;

    a.items = Hw4ABqPHMD_aEoYb_aoNHL9w();
  };
  var ctor$nBMABjf1uTSfGmgy0X6ikQ = WJieLjf1uTSfGmgy0X6ikQ.ctor = $ctor$(null, 'nBMABjf1uTSfGmgy0X6ikQ', type$WJieLjf1uTSfGmgy0X6ikQ);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.__ArrayList.Add
  type$WJieLjf1uTSfGmgy0X6ikQ.mxMABjf1uTSfGmgy0X6ikQ = function (b)
  {
    var a = this;

    a.items.push(b);
  };

  // delegate: (sender, e) => Void
  // ScriptCoreLib.JavaScript.BCLImplementation.System.ComponentModel.__ListChangedEventHandler
  function cqlzsGnZ1TCHsv3y5Z56zg(){};
  cqlzsGnZ1TCHsv3y5Z56zg.TypeName = "ListChangedEventHandler";
  cqlzsGnZ1TCHsv3y5Z56zg.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$cqlzsGnZ1TCHsv3y5Z56zg = cqlzsGnZ1TCHsv3y5Z56zg.prototype = new kgJWtah_alDaJsZAeVC_bgaA();
  type$cqlzsGnZ1TCHsv3y5Z56zg.constructor = cqlzsGnZ1TCHsv3y5Z56zg;
  type$cqlzsGnZ1TCHsv3y5Z56zg.IsExtensionMethod = false;
  type$cqlzsGnZ1TCHsv3y5Z56zg.AsExtensionMethod = function ()
  {
    this.IsExtensionMethod = true;
    return this;
  };
  type$cqlzsGnZ1TCHsv3y5Z56zg.nRMABmnZ1TCHsv3y5Z56zg = type$kgJWtah_alDaJsZAeVC_bgaA.hgsABqh_alDaJsZAeVC_bgaA;
  var ctor$nRMABmnZ1TCHsv3y5Z56zg = $ctor$(basector$kgJWtah_alDaJsZAeVC_bgaA, 'nRMABmnZ1TCHsv3y5Z56zg', type$cqlzsGnZ1TCHsv3y5Z56zg);
  type$cqlzsGnZ1TCHsv3y5Z56zg.Invoke = function (b, c)
  {
    for (var _i = 0; _i < this.list.length; _i++)
    {
      var _f = this.list[_i];
      var _arguments = Array.prototype.slice.call(arguments).slice(0);
      if(_f.IsExtensionMethod) _arguments.splice(0, 0, _f.Target);
      var _target = _f.IsExtensionMethod ? window : _f.Target;
      _target[_f.Method].apply(_target, _arguments);
    }
  };

  // ScriptCoreLib.JavaScript.Runtime.JSColor
  function bZtcPqghWDOMYBdQVLoLIQ(){};
  bZtcPqghWDOMYBdQVLoLIQ.TypeName = "JSColor";
  bZtcPqghWDOMYBdQVLoLIQ.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$bZtcPqghWDOMYBdQVLoLIQ = bZtcPqghWDOMYBdQVLoLIQ.prototype;
  type$bZtcPqghWDOMYBdQVLoLIQ.constructor = bZtcPqghWDOMYBdQVLoLIQ;
  var zAYABKghWDOMYBdQVLoLIQ = null;
  var zQYABKghWDOMYBdQVLoLIQ = null;
  var zgYABKghWDOMYBdQVLoLIQ = null;
  var zwYABKghWDOMYBdQVLoLIQ = null;
  var _0AYABKghWDOMYBdQVLoLIQ = null;
  var _0QYABKghWDOMYBdQVLoLIQ = null;
  type$bZtcPqghWDOMYBdQVLoLIQ.R = null;
  type$bZtcPqghWDOMYBdQVLoLIQ.G = null;
  type$bZtcPqghWDOMYBdQVLoLIQ.B = null;
  type$bZtcPqghWDOMYBdQVLoLIQ.Value = null;
  type$bZtcPqghWDOMYBdQVLoLIQ.H = null;
  type$bZtcPqghWDOMYBdQVLoLIQ.L = null;
  type$bZtcPqghWDOMYBdQVLoLIQ.S = null;
  type$bZtcPqghWDOMYBdQVLoLIQ.isHLS = false;
  var basector$bZtcPqghWDOMYBdQVLoLIQ = $ctor$(null, null, type$bZtcPqghWDOMYBdQVLoLIQ);
  // ScriptCoreLib.JavaScript.Runtime.JSColor..ctor
  type$bZtcPqghWDOMYBdQVLoLIQ.yRMABqghWDOMYBdQVLoLIQ = function ()
  {
    var a = this;

  };
  var ctor$yRMABqghWDOMYBdQVLoLIQ = bZtcPqghWDOMYBdQVLoLIQ.ctor = $ctor$(null, 'yRMABqghWDOMYBdQVLoLIQ', type$bZtcPqghWDOMYBdQVLoLIQ);

  // ScriptCoreLib.JavaScript.Runtime.JSColor.get_Red
  function vBMABqghWDOMYBdQVLoLIQ()
  {
    var b;

    b = xRMABqghWDOMYBdQVLoLIQ(255, 0, 0);
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.JSColor.get_Green
  function vRMABqghWDOMYBdQVLoLIQ()
  {
    var b;

    b = xRMABqghWDOMYBdQVLoLIQ(0, 255, 0);
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.JSColor.get_Blue
  function vhMABqghWDOMYBdQVLoLIQ()
  {
    var b;

    b = xRMABqghWDOMYBdQVLoLIQ(0, 0, 255);
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.JSColor.get_Cyan
  function vxMABqghWDOMYBdQVLoLIQ()
  {
    var b;

    b = xRMABqghWDOMYBdQVLoLIQ(0, 255, 255);
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.JSColor.FromValue
  function wBMABqghWDOMYBdQVLoLIQ(b)
  {
    var c, d;

    c = new ctor$yRMABqghWDOMYBdQVLoLIQ();
    c.Value = b;
    d = c;
    return d;
  };

  // ScriptCoreLib.JavaScript.Runtime.JSColor.HueToRGB
  function wRMABqghWDOMYBdQVLoLIQ(b, c, d)
  {
    var e, f;

    f = !(d < 0);

    if (!f)
    {
      d = (d + 240);
    }

    f = !(d > 240);

    if (!f)
    {
      d = (d - 240);
    }

    f = !(d < 40);

    if (!f)
    {
      e = (b + ((((c - b) * d) + 20) / 40));
      return e;
    }

    f = !(d < 120);

    if (!f)
    {
      e = c;
      return e;
    }

    f = !(d < 160);

    if (!f)
    {
      e = (b + ((((c - b) * (160 - d)) + 20) / 40));
      return e;
    }

    e = b;
    return e;
  };

  // ScriptCoreLib.JavaScript.Runtime.JSColor.ToRGB
  type$bZtcPqghWDOMYBdQVLoLIQ.whMABqghWDOMYBdQVLoLIQ = function ()
  {
    var a = this, b, c, d, e, f, g;

    b = new ctor$yRMABqghWDOMYBdQVLoLIQ();
    g = !!a.S;

    if (!g)
    {
      c = ((a.L * 255) / 240);
      b.R = c;
      b.G = c;
      b.B = c;
      g = (a.H == 160);

      if (!g)
      {
      }

    }
    else
    {
      g = (a.L > 120);

      if (!g)
      {
        e = (((a.L * (240 + a.S)) + 120) / 240);
      }
      else
      {
        e = ((a.L + a.S) - (((a.L * a.S) + 120) / 240));
      }

      d = ((2 * a.L) - e);
      b.R = YggABl6xZjKlcaFaQuMTTA((((wRMABqghWDOMYBdQVLoLIQ(d, e, (a.H + 80)) * 255) + 120) / 240));
      b.G = YggABl6xZjKlcaFaQuMTTA((((wRMABqghWDOMYBdQVLoLIQ(d, e, a.H) * 255) + 120) / 240));
      b.B = YggABl6xZjKlcaFaQuMTTA((((wRMABqghWDOMYBdQVLoLIQ(d, e, (a.H - 80)) * 255) + 120) / 240));
    }

    f = b;
    return f;
  };

  // ScriptCoreLib.JavaScript.Runtime.JSColor.ToHLS
  type$bZtcPqghWDOMYBdQVLoLIQ.wxMABqghWDOMYBdQVLoLIQ = function ()
  {
    var a = this, b, c, d, e, f, g, h, i, j, k, l;

    b = new ctor$yRMABqghWDOMYBdQVLoLIQ();
    b.isHLS = 1;
    c = Math.max(Math.max(a.R, a.G), a.B);
    d = Math.min(Math.min(a.R, a.G), a.B);
    f = ((((c + d) * 240) + 255) / 510);
    l = !(c == d);

    if (!l)
    {
      g = 0;
      e = 160;
    }
    else
    {
      l = (f > 120);

      if (!l)
      {
        g = ((((c - d) * 240) + ((c + d) / 2)) / (c + d));
      }
      else
      {
        g = ((((c - d) * 240) + (((510 - c) - d) / 2)) / ((510 - c) - d));
      }

      h = ((((c - a.R) * 40) + ((c - d) / 2)) / (c - d));
      i = ((((c - a.G) * 40) + ((c - d) / 2)) / (c - d));
      j = ((((c - a.B) * 40) + ((c - d) / 2)) / (c - d));
      l = !(a.R == c);

      if (!l)
      {
        e = (j - i);
      }
      else
      {
        l = !(a.G == c);

        if (!l)
        {
          e = ((80 + h) - j);
        }
        else
        {
          e = ((160 + i) - h);
        }

      }

      l = !(e < 0);

      if (!l)
      {
        e += 240;
      }

      l = !(e > 240);

      if (!l)
      {
        e -= 240;
      }

    }

    b.H = YggABl6xZjKlcaFaQuMTTA(e);
    b.L = YggABl6xZjKlcaFaQuMTTA(f);
    b.S = YggABl6xZjKlcaFaQuMTTA(g);
    k = b;
    return k;
  };

  // ScriptCoreLib.JavaScript.Runtime.JSColor.FromHLS
  function xBMABqghWDOMYBdQVLoLIQ(b, c, d)
  {
    var e, f;

    e = new ctor$yRMABqghWDOMYBdQVLoLIQ();
    e.H = b;
    e.L = c;
    e.S = d;
    e.isHLS = 1;
    f = e;
    return f;
  };

  // ScriptCoreLib.JavaScript.Runtime.JSColor.FromRGB
  function xRMABqghWDOMYBdQVLoLIQ(b, c, d)
  {
    var e, f;

    e = new ctor$yRMABqghWDOMYBdQVLoLIQ();
    e.R = b;
    e.G = c;
    e.B = d;
    f = e;
    return f;
  };

  // ScriptCoreLib.JavaScript.Runtime.JSColor.FromGray
  function xhMABqghWDOMYBdQVLoLIQ(b)
  {
    var c;

    c = xRMABqghWDOMYBdQVLoLIQ(b, b, b);
    return c;
  };

  // ScriptCoreLib.JavaScript.Runtime.JSColor.op_Implicit
  function xxMABqghWDOMYBdQVLoLIQ(b)
  {
    var c;

    c = (b+'');
    return c;
  };

  // ScriptCoreLib.JavaScript.Runtime.JSColor.ToString
  type$bZtcPqghWDOMYBdQVLoLIQ.toString /* ScriptCoreLib.JavaScript.Runtime.JSColor.ToString */ = function ()
  {
    var a = this, b, c, d, e;

    b = a;
    d = (b.Value == null);

    if (!d)
    {
      c = b.Value;
      return c;
    }

    d = !b.isHLS;

    if (!d)
    {
      b = b.whMABqghWDOMYBdQVLoLIQ();
    }

    e = [
      'RGB(',
      b.R,
      ', ',
      b.G,
      ', ',
      b.B,
      ')'
    ];
    c = fBMABjDeCj_aRJzaBmU9SJg(e);
    return c;
  };
    bZtcPqghWDOMYBdQVLoLIQ.prototype.toString /* System.Object.ToString */ = bZtcPqghWDOMYBdQVLoLIQ.prototype.toString /* ScriptCoreLib.JavaScript.Runtime.JSColor.ToString */;

  // ScriptCoreLib.JavaScript.Runtime.JSColor+System.get_ActiveBorder
  function yxMABk_aVmjm3o_apvzEcgcA()
  {
    var b;

    b = wBMABqghWDOMYBdQVLoLIQ('ActiveBorder');
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.JSColor+System.get_ActiveCaption
  function zBMABk_aVmjm3o_apvzEcgcA()
  {
    var b;

    b = wBMABqghWDOMYBdQVLoLIQ('ActiveCaption');
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.JSColor+System.get_AppWorkspace
  function zRMABk_aVmjm3o_apvzEcgcA()
  {
    var b;

    b = wBMABqghWDOMYBdQVLoLIQ('AppWorkspace');
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.JSColor+System.get_Background
  function zhMABk_aVmjm3o_apvzEcgcA()
  {
    var b;

    b = wBMABqghWDOMYBdQVLoLIQ('Background');
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.JSColor+System.get_ButtonFace
  function zxMABk_aVmjm3o_apvzEcgcA()
  {
    var b;

    b = wBMABqghWDOMYBdQVLoLIQ('ButtonFace');
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.JSColor+System.get_ButtonHighlight
  function _0BMABk_aVmjm3o_apvzEcgcA()
  {
    var b;

    b = wBMABqghWDOMYBdQVLoLIQ('ButtonHighlight');
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.JSColor+System.get_ButtonShadow
  function _0RMABk_aVmjm3o_apvzEcgcA()
  {
    var b;

    b = wBMABqghWDOMYBdQVLoLIQ('ButtonShadow');
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.JSColor+System.get_ButtonText
  function _0hMABk_aVmjm3o_apvzEcgcA()
  {
    var b;

    b = wBMABqghWDOMYBdQVLoLIQ('ButtonText');
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.JSColor+System.get_CaptionText
  function _0xMABk_aVmjm3o_apvzEcgcA()
  {
    var b;

    b = wBMABqghWDOMYBdQVLoLIQ('CaptionText');
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.JSColor+System.get_GrayText
  function _1BMABk_aVmjm3o_apvzEcgcA()
  {
    var b;

    b = wBMABqghWDOMYBdQVLoLIQ('GrayText');
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.JSColor+System.get_Highlight
  function _1RMABk_aVmjm3o_apvzEcgcA()
  {
    var b;

    b = wBMABqghWDOMYBdQVLoLIQ('Highlight');
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.JSColor+System.get_HighlightText
  function _1hMABk_aVmjm3o_apvzEcgcA()
  {
    var b;

    b = wBMABqghWDOMYBdQVLoLIQ('HighlightText');
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.JSColor+System.get_InactiveBorder
  function _1xMABk_aVmjm3o_apvzEcgcA()
  {
    var b;

    b = wBMABqghWDOMYBdQVLoLIQ('InactiveBorder');
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.JSColor+System.get_InactiveCaption
  function _2BMABk_aVmjm3o_apvzEcgcA()
  {
    var b;

    b = wBMABqghWDOMYBdQVLoLIQ('InactiveCaption');
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.JSColor+System.get_InactiveCaptionText
  function _2RMABk_aVmjm3o_apvzEcgcA()
  {
    var b;

    b = wBMABqghWDOMYBdQVLoLIQ('InactiveCaptionText');
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.JSColor+System.get_InfoBackground
  function _2hMABk_aVmjm3o_apvzEcgcA()
  {
    var b;

    b = wBMABqghWDOMYBdQVLoLIQ('InfoBackground');
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.JSColor+System.get_InfoText
  function _2xMABk_aVmjm3o_apvzEcgcA()
  {
    var b;

    b = wBMABqghWDOMYBdQVLoLIQ('InfoText');
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.JSColor+System.get_Menu
  function _3BMABk_aVmjm3o_apvzEcgcA()
  {
    var b;

    b = wBMABqghWDOMYBdQVLoLIQ('Menu');
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.JSColor+System.get_MenuText
  function _3RMABk_aVmjm3o_apvzEcgcA()
  {
    var b;

    b = wBMABqghWDOMYBdQVLoLIQ('MenuText');
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.JSColor+System.get_Scrollbar
  function _3hMABk_aVmjm3o_apvzEcgcA()
  {
    var b;

    b = wBMABqghWDOMYBdQVLoLIQ('Scrollbar');
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.JSColor+System.get_ThreeDDarkShadow
  function _3xMABk_aVmjm3o_apvzEcgcA()
  {
    var b;

    b = wBMABqghWDOMYBdQVLoLIQ('ThreeDDarkShadow');
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.JSColor+System.get_ThreeDFace
  function _4BMABk_aVmjm3o_apvzEcgcA()
  {
    var b;

    b = wBMABqghWDOMYBdQVLoLIQ('ThreeDFace');
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.JSColor+System.get_ThreeDHighlight
  function _4RMABk_aVmjm3o_apvzEcgcA()
  {
    var b;

    b = wBMABqghWDOMYBdQVLoLIQ('ThreeDHighlight');
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.JSColor+System.get_ThreeDLightShadow
  function _4hMABk_aVmjm3o_apvzEcgcA()
  {
    var b;

    b = wBMABqghWDOMYBdQVLoLIQ('ThreeDLightShadow');
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.JSColor+System.get_ThreeDShadow
  function _4xMABk_aVmjm3o_apvzEcgcA()
  {
    var b;

    b = wBMABqghWDOMYBdQVLoLIQ('ThreeDShadow');
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.JSColor+System.get_Window
  function _5BMABk_aVmjm3o_apvzEcgcA()
  {
    var b;

    b = wBMABqghWDOMYBdQVLoLIQ('Window');
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.JSColor+System.get_WindowFrame
  function _5RMABk_aVmjm3o_apvzEcgcA()
  {
    var b;

    b = wBMABqghWDOMYBdQVLoLIQ('WindowFrame');
    return b;
  };

  // ScriptCoreLib.JavaScript.Runtime.JSColor+System.get_WindowText
  function _5hMABk_aVmjm3o_apvzEcgcA()
  {
    var b;

    b = wBMABqghWDOMYBdQVLoLIQ('WindowText');
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.IRegExp.InternalConstructor
  function _6BMABjToaTaQVWaeKug8LQ(e) { return new RegExp(e); };
  // ScriptCoreLib.JavaScript.DOM.IRegExp.InternalConstructor
  function _6hMABjToaTaQVWaeKug8LQ(e, mod) { return new RegExp(e, mod); };
  // ScriptCoreLib.JavaScript.DOM.IRegExp.exec
  // ScriptCoreLib.JavaScript.DOM.IRegExp.exec
  // ScriptCoreLib.JavaScript.DOM.IRegExp.replace
  function _7RMABjToaTaQVWaeKug8LQ(r, e, v) { return e.replace(r, v); };
  // ScriptCoreLib.JavaScript.DOM.IRegExp.replace
  function _7hMABjToaTaQVWaeKug8LQ(a, b, c)
  {
    var d;

    d = _7RMABjToaTaQVWaeKug8LQ(a, b, c);
    return d;
  };

  // ScriptCoreLib.JavaScript.DOM.IRegExp.get_Trim
  function _7xMABjToaTaQVWaeKug8LQ()
  {
    var b;

    b = _6hMABjToaTaQVWaeKug8LQ('\u005e\u005cs\u002a\u007c\u005cs\u002a$', 'g');
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.IRegExp.get_Integer
  function _8BMABjToaTaQVWaeKug8LQ()
  {
    var b;

    b = _6BMABjToaTaQVWaeKug8LQ('\u005e\u005cd+$');
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.IRegExp.get_Currency
  function _8RMABjToaTaQVWaeKug8LQ()
  {
    var b;

    b = _6BMABjToaTaQVWaeKug8LQ('\u005e[0-9]{1,3}(?:,?[0-9]{3})\u002a(?:\u005c.[0-9]{2})?$');
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.IRegExp.ExecToArray
  function _8hMABjToaTaQVWaeKug8LQ(a, b)
  {
    var c, d, e, f;

    c = Hw4ABqPHMD_aEoYb_aoNHL9w();
    d = a.exec(b);
    while ((d && (c.length < 80)))
    {
      c.push(d);
      d = a.exec(b);
    }
    e = MA4ABqPHMD_aEoYb_aoNHL9w(c);
    return e;
  };

  // ScriptCoreLib.JavaScript.DOM.IRegExp.ExecToArray
  function _8xMABjToaTaQVWaeKug8LQ(b, c, d)
  {
    var e;

    e = _9BMABjToaTaQVWaeKug8LQ(_6hMABjToaTaQVWaeKug8LQ(b, 'g'), c, d);
    return e;
  };

  // ScriptCoreLib.JavaScript.DOM.IRegExp.ExecToArray
  function _9BMABjToaTaQVWaeKug8LQ(a, b, c)
  {
    var d, e, f, g;

    d = Hw4ABqPHMD_aEoYb_aoNHL9w();
    e = a.exec(b);
    while ((e && (d.length < 80)))
    {
      d.push(e[c]);
      e = a.exec(b);
    }
    f = MA4ABqPHMD_aEoYb_aoNHL9w(d);
    return f;
  };

  // ScriptCoreLib.JavaScript.Query.InternalSequenceImplementation.AsEnumerable
  function ChQABi2cDDCKLtCQWJ9PkA(b)
  {
    var c, d, e;

    e = !(b == null);

    if (!e)
    {
      d = null;
      return d;
    }

    c = BBMABr5xMzijfM5xNYhyrw(b);
    e = __aBIABr5xMzijfM5xNYhyrw(c);

    if (!e)
    {
      e = !(c.prototype == null);

      if (!e)
      {
        e = !BRMABr5xMzijfM5xNYhyrw(c, 'length');

        if (!e)
        {
        }
        else
        {
          d = b;
          return d;
        }

      }
      else
      {
        d = b;
        return d;
      }

    }

    d = rggABhLuAz2cCkYgRA33LQ(_7hIABr5xMzijfM5xNYhyrw(c));
    return d;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Queue`1
  function G_bYo962yajuICugOLN_a8lw(){};
  G_bYo962yajuICugOLN_a8lw.TypeName = "Queue_1";
  G_bYo962yajuICugOLN_a8lw.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$G_bYo962yajuICugOLN_a8lw = G_bYo962yajuICugOLN_a8lw.prototype;
  type$G_bYo962yajuICugOLN_a8lw.constructor = G_bYo962yajuICugOLN_a8lw;
  type$G_bYo962yajuICugOLN_a8lw.InternalList = null;
  var basector$G_bYo962yajuICugOLN_a8lw = $ctor$(null, null, type$G_bYo962yajuICugOLN_a8lw);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Queue`1..ctor
  type$G_bYo962yajuICugOLN_a8lw.JhQABq2yajuICugOLN_a8lw = function ()
  {
    var a = this;

    a.JxQABq2yajuICugOLN_a8lw(null);
  };
  var ctor$JhQABq2yajuICugOLN_a8lw = G_bYo962yajuICugOLN_a8lw.ctor = $ctor$(null, 'JhQABq2yajuICugOLN_a8lw', type$G_bYo962yajuICugOLN_a8lw);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Queue`1..ctor
  type$G_bYo962yajuICugOLN_a8lw.JxQABq2yajuICugOLN_a8lw = function (b)
  {
    var a = this, c, d, e;

    a.InternalList = Hw4ABqPHMD_aEoYb_aoNHL9w();
    d = (b == null);

    if (!d)
    {
      e = b.NgEABnMeWzaNooAKOmFm5g();
      try
      {
        while (e.qAAABu7N0xGI6ACQJ1TEOg())
        {
          c = e.xQAABrYmRzSu_anO2U_bk1MA();
          a.LRQABq2yajuICugOLN_a8lw(c);
        }
      }
      finally
      {
        d = (e == null);

        if (!d)
        {
          e.xAAABq_bUDz_aWf_aXPRTEtLA();
        }

      }
    }

  };
  var ctor$JxQABq2yajuICugOLN_a8lw = $ctor$(null, 'JxQABq2yajuICugOLN_a8lw', type$G_bYo962yajuICugOLN_a8lw);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Queue`1.get_Count
  type$G_bYo962yajuICugOLN_a8lw.KBQABq2yajuICugOLN_a8lw = function ()
  {
    var a = this, b;

    b = a.InternalList.length;
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Queue`1.Clear
  type$G_bYo962yajuICugOLN_a8lw.KRQABq2yajuICugOLN_a8lw = function ()
  {
    var a = this;

    a.InternalList.splice(0, a.KBQABq2yajuICugOLN_a8lw());
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Queue`1.Contains
  type$G_bYo962yajuICugOLN_a8lw.KhQABq2yajuICugOLN_a8lw = function (b)
  {
    var a = this, c;

    c = !(Lg4ABqPHMD_aEoYb_aoNHL9w(a.InternalList, b) == -1);
    return c;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Queue`1.CopyTo
  type$G_bYo962yajuICugOLN_a8lw.KxQABq2yajuICugOLN_a8lw = function (b, c)
  {
    var a = this;

    throw nAAABq584TSQo69VDcfM9Q();
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Queue`1.Dequeue
  type$G_bYo962yajuICugOLN_a8lw.LBQABq2yajuICugOLN_a8lw = function ()
  {
    var a = this, b;

    b = a.InternalList.shift();
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Queue`1.Enqueue
  type$G_bYo962yajuICugOLN_a8lw.LRQABq2yajuICugOLN_a8lw = function (b)
  {
    var a = this;

    a.InternalList.push(b);
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Queue`1.GetEnumerator
  type$G_bYo962yajuICugOLN_a8lw.LhQABq2yajuICugOLN_a8lw = function ()
  {
    var a = this, b;

    b = new ctor$OxQABrO7iTaCp8Nh8TVYhg(a);
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Queue`1.get_ArrayReference
  type$G_bYo962yajuICugOLN_a8lw.LxQABq2yajuICugOLN_a8lw = function ()
  {
    var a = this, b;

    b = a.InternalList;
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Queue`1.Peek
  type$G_bYo962yajuICugOLN_a8lw.MBQABq2yajuICugOLN_a8lw = function ()
  {
    var a = this, b;

    b = a.LxQABq2yajuICugOLN_a8lw()[0];
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Queue`1.get_ArrayReferenceCloned
  type$G_bYo962yajuICugOLN_a8lw.MRQABq2yajuICugOLN_a8lw = function ()
  {
    var a = this, b;

    b = a.InternalList.slice(0);
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Queue`1.ToArray
  type$G_bYo962yajuICugOLN_a8lw.MhQABq2yajuICugOLN_a8lw = function ()
  {
    var a = this, b;

    b = a.MRQABq2yajuICugOLN_a8lw();
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Queue`1.TrimExcess
  type$G_bYo962yajuICugOLN_a8lw.MxQABq2yajuICugOLN_a8lw = function ()
  {
    var a = this;

    throw nAAABq584TSQo69VDcfM9Q();
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Queue`1.System.Collections.IEnumerable.GetEnumerator
  type$G_bYo962yajuICugOLN_a8lw.NBQABq2yajuICugOLN_a8lw = function ()
  {
    var a = this, b;

    b = a.LhQABq2yajuICugOLN_a8lw();
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Queue`1.System.Collections.ICollection.CopyTo
  type$G_bYo962yajuICugOLN_a8lw.NRQABq2yajuICugOLN_a8lw = function (b, c)
  {
    var a = this;

    throw nAAABq584TSQo69VDcfM9Q();
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Queue`1.System.Collections.ICollection.get_Count
  type$G_bYo962yajuICugOLN_a8lw.NhQABq2yajuICugOLN_a8lw = function ()
  {
    var a = this, b;

    b = a.KBQABq2yajuICugOLN_a8lw();
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Queue`1.System.Collections.ICollection.get_IsSynchronized
  type$G_bYo962yajuICugOLN_a8lw.NxQABq2yajuICugOLN_a8lw = function ()
  {
    var a = this;

    throw nAAABq584TSQo69VDcfM9Q();
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Queue`1.System.Collections.ICollection.get_SyncRoot
  type$G_bYo962yajuICugOLN_a8lw.OBQABq2yajuICugOLN_a8lw = function ()
  {
    var a = this;

    throw nAAABq584TSQo69VDcfM9Q();
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Queue`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator
  type$G_bYo962yajuICugOLN_a8lw.ORQABq2yajuICugOLN_a8lw = function ()
  {
    var a = this, b;

    b = a.LhQABq2yajuICugOLN_a8lw();
    return b;
  };

  // 
  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Queue`1
  (function (i)  {
    i.NgEABnMeWzaNooAKOmFm5g = i.ORQABq2yajuICugOLN_a8lw;
    // System.Collections.IEnumerable
    i.EQAABu7N0xGI6ACQJ1TEOg = i.NBQABq2yajuICugOLN_a8lw;
    // System.Collections.ICollection
    i.EgAABgHRkjqNHOcuXxDpkg = i.NRQABq2yajuICugOLN_a8lw;
    i.EwAABgHRkjqNHOcuXxDpkg = i.NhQABq2yajuICugOLN_a8lw;
    i.FAAABgHRkjqNHOcuXxDpkg = i.OBQABq2yajuICugOLN_a8lw;
    i.FQAABgHRkjqNHOcuXxDpkg = i.NxQABq2yajuICugOLN_a8lw;
  }
  )(type$G_bYo962yajuICugOLN_a8lw);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Queue`1+__Enumerator
  function rxjo9LO7iTaCp8Nh8TVYhg(){};
  rxjo9LO7iTaCp8Nh8TVYhg.TypeName = "Enumerator";
  rxjo9LO7iTaCp8Nh8TVYhg.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$rxjo9LO7iTaCp8Nh8TVYhg = rxjo9LO7iTaCp8Nh8TVYhg.prototype;
  type$rxjo9LO7iTaCp8Nh8TVYhg.constructor = rxjo9LO7iTaCp8Nh8TVYhg;
  type$rxjo9LO7iTaCp8Nh8TVYhg.value = null;
  var basector$rxjo9LO7iTaCp8Nh8TVYhg = $ctor$(null, null, type$rxjo9LO7iTaCp8Nh8TVYhg);
  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Queue`1+__Enumerator..ctor
  type$rxjo9LO7iTaCp8Nh8TVYhg.OhQABrO7iTaCp8Nh8TVYhg = function ()
  {
    var a = this;

    a.OxQABrO7iTaCp8Nh8TVYhg(null);
  };
  var ctor$OhQABrO7iTaCp8Nh8TVYhg = rxjo9LO7iTaCp8Nh8TVYhg.ctor = $ctor$(null, 'OhQABrO7iTaCp8Nh8TVYhg', type$rxjo9LO7iTaCp8Nh8TVYhg);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Queue`1+__Enumerator..ctor
  type$rxjo9LO7iTaCp8Nh8TVYhg.OxQABrO7iTaCp8Nh8TVYhg = function (b)
  {
    var a = this, c, d;

    d = (b == null);

    if (!d)
    {
      c = new ctor$pggABhLuAz2cCkYgRA33LQ(b.MhQABq2yajuICugOLN_a8lw());
      a.value = c.NgEABnMeWzaNooAKOmFm5g();
    }

  };
  var ctor$OxQABrO7iTaCp8Nh8TVYhg = $ctor$(null, 'OxQABrO7iTaCp8Nh8TVYhg', type$rxjo9LO7iTaCp8Nh8TVYhg);

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Queue`1+__Enumerator.get_Current
  type$rxjo9LO7iTaCp8Nh8TVYhg.PBQABrO7iTaCp8Nh8TVYhg = function ()
  {
    var a = this, b;

    b = a.value.xQAABrYmRzSu_anO2U_bk1MA();
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Queue`1+__Enumerator.Dispose
  type$rxjo9LO7iTaCp8Nh8TVYhg.PRQABrO7iTaCp8Nh8TVYhg = function ()
  {
    var a = this;

    a.value.xAAABq_bUDz_aWf_aXPRTEtLA();
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Queue`1+__Enumerator.System.Collections.IEnumerator.get_Current
  type$rxjo9LO7iTaCp8Nh8TVYhg.PhQABrO7iTaCp8Nh8TVYhg = function ()
  {
    var a = this, b;

    b = a.value.xQAABrYmRzSu_anO2U_bk1MA();
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Queue`1+__Enumerator.MoveNext
  type$rxjo9LO7iTaCp8Nh8TVYhg.PxQABrO7iTaCp8Nh8TVYhg = function ()
  {
    var a = this, b;

    b = a.value.qAAABu7N0xGI6ACQJ1TEOg();
    return b;
  };

  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Queue`1+__Enumerator.Reset
  type$rxjo9LO7iTaCp8Nh8TVYhg.QBQABrO7iTaCp8Nh8TVYhg = function ()
  {
    var a = this;

    a.value.qgAABu7N0xGI6ACQJ1TEOg();
  };

  // 
  // ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Queue`1+__Enumerator
  (function (i)  {
    i.xQAABrYmRzSu_anO2U_bk1MA = i.PBQABrO7iTaCp8Nh8TVYhg;
    // System.IDisposable
    i.xAAABq_bUDz_aWf_aXPRTEtLA = i.PRQABrO7iTaCp8Nh8TVYhg;
    // System.Collections.IEnumerator
    i.qAAABu7N0xGI6ACQJ1TEOg = i.PxQABrO7iTaCp8Nh8TVYhg;
    i.qQAABu7N0xGI6ACQJ1TEOg = i.PhQABrO7iTaCp8Nh8TVYhg;
    i.qgAABu7N0xGI6ACQJ1TEOg = i.QBQABrO7iTaCp8Nh8TVYhg;
  }
  )(type$rxjo9LO7iTaCp8Nh8TVYhg);
  // ScriptCoreLib.Shared.Drawing.RectangleInfo
  function _49ZFUFGffzGByMt4B_bz5uA(){};
  _49ZFUFGffzGByMt4B_bz5uA.TypeName = "RectangleInfo";
  _49ZFUFGffzGByMt4B_bz5uA.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$_49ZFUFGffzGByMt4B_bz5uA = _49ZFUFGffzGByMt4B_bz5uA.prototype;
  type$_49ZFUFGffzGByMt4B_bz5uA.constructor = _49ZFUFGffzGByMt4B_bz5uA;
  type$_49ZFUFGffzGByMt4B_bz5uA.Left = 0;
  type$_49ZFUFGffzGByMt4B_bz5uA.Top = 0;
  type$_49ZFUFGffzGByMt4B_bz5uA.Width = 0;
  type$_49ZFUFGffzGByMt4B_bz5uA.Height = 0;
  type$_49ZFUFGffzGByMt4B_bz5uA.$0 = {};
  type$_49ZFUFGffzGByMt4B_bz5uA.$0.$0 = 'RectangleInfo';
  type$_49ZFUFGffzGByMt4B_bz5uA.$0.$1 = 'rBQABlGffzGByMt4B_bz5uA';

  var basector$_49ZFUFGffzGByMt4B_bz5uA = $ctor$(null, null, type$_49ZFUFGffzGByMt4B_bz5uA);
  // ScriptCoreLib.Shared.Drawing.RectangleInfo..ctor
  type$_49ZFUFGffzGByMt4B_bz5uA.rBQABlGffzGByMt4B_bz5uA = function ()
  {
    var a = this;

  };
  var ctor$rBQABlGffzGByMt4B_bz5uA = _49ZFUFGffzGByMt4B_bz5uA.ctor = $ctor$(null, 'rBQABlGffzGByMt4B_bz5uA', type$_49ZFUFGffzGByMt4B_bz5uA);

  // ScriptCoreLib.Shared.Drawing.Rectangle
  function BkK2rq43Ozes59U5VdfFiw(){};
  BkK2rq43Ozes59U5VdfFiw.TypeName = "Rectangle";
  BkK2rq43Ozes59U5VdfFiw.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$BkK2rq43Ozes59U5VdfFiw = BkK2rq43Ozes59U5VdfFiw.prototype = new _49ZFUFGffzGByMt4B_bz5uA();
  type$BkK2rq43Ozes59U5VdfFiw.constructor = BkK2rq43Ozes59U5VdfFiw;
  var basector$BkK2rq43Ozes59U5VdfFiw = $ctor$(basector$_49ZFUFGffzGByMt4B_bz5uA, null, type$BkK2rq43Ozes59U5VdfFiw);
  // ScriptCoreLib.Shared.Drawing.Rectangle..ctor
  type$BkK2rq43Ozes59U5VdfFiw.vBQABq43Ozes59U5VdfFiw = function ()
  {
    var a = this;

    a.rBQABlGffzGByMt4B_bz5uA();
  };
  var ctor$vBQABq43Ozes59U5VdfFiw = BkK2rq43Ozes59U5VdfFiw.ctor = $ctor$(basector$_49ZFUFGffzGByMt4B_bz5uA, 'vBQABq43Ozes59U5VdfFiw', type$BkK2rq43Ozes59U5VdfFiw);

  // ScriptCoreLib.Shared.Drawing.Rectangle.Contains
  type$BkK2rq43Ozes59U5VdfFiw.rRQABq43Ozes59U5VdfFiw = function (b)
  {
    var a = this, c, d;

    d = !(b.X < a.Left);

    if (!d)
    {
      c = 0;
      return c;
    }

    d = !(b.Y < a.Top);

    if (!d)
    {
      c = 0;
      return c;
    }

    d = !(b.X > a.sxQABq43Ozes59U5VdfFiw());

    if (!d)
    {
      c = 0;
      return c;
    }

    d = !(b.Y > a.tRQABq43Ozes59U5VdfFiw());

    if (!d)
    {
      c = 0;
      return c;
    }

    c = 1;
    return c;
  };

  // ScriptCoreLib.Shared.Drawing.Rectangle.op_Division
  function rhQABq43Ozes59U5VdfFiw(b, c)
  {
    var d;

    d = uBQABq43Ozes59U5VdfFiw((b.Left / c), (b.Top / c), (b.Width / c), (b.Height / c));
    return d;
  };

  // ScriptCoreLib.Shared.Drawing.Rectangle.op_Multiply
  function rxQABq43Ozes59U5VdfFiw(b, c)
  {
    var d;

    d = uBQABq43Ozes59U5VdfFiw((b.Left * c), (b.Top * c), (b.Width * c), (b.Height * c));
    return d;
  };

  // ScriptCoreLib.Shared.Drawing.Rectangle.get_Location
  type$BkK2rq43Ozes59U5VdfFiw.sBQABq43Ozes59U5VdfFiw = function ()
  {
    var a = this, b;

    b = new ctor$xhQABoqVzDa8F5yDSGjsZA(a.Left, a.Top);
    return b;
  };

  // ScriptCoreLib.Shared.Drawing.Rectangle.op_Implicit
  function sRQABq43Ozes59U5VdfFiw(b)
  {
    var c;

    c = new ctor$xhQABoqVzDa8F5yDSGjsZA(b.Left, b.Top);
    return c;
  };

  // ScriptCoreLib.Shared.Drawing.Rectangle.get_Size
  type$BkK2rq43Ozes59U5VdfFiw.shQABq43Ozes59U5VdfFiw = function ()
  {
    var a = this, b;

    b = vRQABgR4TjGWvpZdG1JEaA(a.Width, a.Height);
    return b;
  };

  // ScriptCoreLib.Shared.Drawing.Rectangle.get_Right
  type$BkK2rq43Ozes59U5VdfFiw.sxQABq43Ozes59U5VdfFiw = function ()
  {
    var a = this, b;

    b = (a.Left + a.Width);
    return b;
  };

  // ScriptCoreLib.Shared.Drawing.Rectangle.set_Right
  type$BkK2rq43Ozes59U5VdfFiw.tBQABq43Ozes59U5VdfFiw = function (b)
  {
    var a = this;

    a.Width = (b - a.Left);
  };

  // ScriptCoreLib.Shared.Drawing.Rectangle.get_Bottom
  type$BkK2rq43Ozes59U5VdfFiw.tRQABq43Ozes59U5VdfFiw = function ()
  {
    var a = this, b;

    b = (a.Top + a.Height);
    return b;
  };

  // ScriptCoreLib.Shared.Drawing.Rectangle.set_Bottom
  type$BkK2rq43Ozes59U5VdfFiw.thQABq43Ozes59U5VdfFiw = function (b)
  {
    var a = this;

    a.Height = (b - a.Top);
  };

  // ScriptCoreLib.Shared.Drawing.Rectangle.IntersectsWith
  type$BkK2rq43Ozes59U5VdfFiw.txQABq43Ozes59U5VdfFiw = function (b)
  {
    var a = this, c, d, e, f, g;

    c = (b.Left < a.sxQABq43Ozes59U5VdfFiw());
    d = (a.Left < b.sxQABq43Ozes59U5VdfFiw());
    e = (b.Top < a.tRQABq43Ozes59U5VdfFiw());
    f = (a.Top < b.tRQABq43Ozes59U5VdfFiw());
    g = (c && (d && (e && f)));
    return g;
  };

  // ScriptCoreLib.Shared.Drawing.Rectangle.Of
  function uBQABq43Ozes59U5VdfFiw(b, c, d, e)
  {
    var f, g;

    f = new ctor$vBQABq43Ozes59U5VdfFiw();
    f.Left = b;
    f.Top = c;
    f.Width = d;
    f.Height = e;
    g = f;
    return g;
  };

  // ScriptCoreLib.Shared.Drawing.Rectangle.ToString
  type$BkK2rq43Ozes59U5VdfFiw.toString /* ScriptCoreLib.Shared.Drawing.Rectangle.ToString */ = function ()
  {
    var a = this, b, c;

    c = [
      '[',
      new Number(a.Left),
      ', ',
      new Number(a.Top),
      ', ',
      new Number(a.Width),
      ', ',
      new Number(a.Height),
      ']'
    ];
    b = fBMABjDeCj_aRJzaBmU9SJg(c);
    return b;
  };
    BkK2rq43Ozes59U5VdfFiw.prototype.toString /* System.Object.ToString */ = BkK2rq43Ozes59U5VdfFiw.prototype.toString /* ScriptCoreLib.Shared.Drawing.Rectangle.ToString */;

  // ScriptCoreLib.Shared.Drawing.Rectangle.Of
  function uhQABq43Ozes59U5VdfFiw(b, c)
  {
    var d;

    d = uBQABq43Ozes59U5VdfFiw(b.X, b.Y, c.Width, c.Height);
    return d;
  };

  // ScriptCoreLib.Shared.Drawing.Rectangle.Offset
  type$BkK2rq43Ozes59U5VdfFiw.uxQABq43Ozes59U5VdfFiw = function (b)
  {
    var a = this;

    a.Left = (a.Left + b.X);
    a.Top = (a.Top + b.Y);
  };

  // ScriptCoreLib.Shared.Drawing.Size
  function _6mLe_aQR4TjGWvpZdG1JEaA(){};
  _6mLe_aQR4TjGWvpZdG1JEaA.TypeName = "Size";
  _6mLe_aQR4TjGWvpZdG1JEaA.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$_6mLe_aQR4TjGWvpZdG1JEaA = _6mLe_aQR4TjGWvpZdG1JEaA.prototype;
  type$_6mLe_aQR4TjGWvpZdG1JEaA.constructor = _6mLe_aQR4TjGWvpZdG1JEaA;
  type$_6mLe_aQR4TjGWvpZdG1JEaA.Width = 0;
  type$_6mLe_aQR4TjGWvpZdG1JEaA.Height = 0;
  var basector$_6mLe_aQR4TjGWvpZdG1JEaA = $ctor$(null, null, type$_6mLe_aQR4TjGWvpZdG1JEaA);
  // ScriptCoreLib.Shared.Drawing.Size..ctor
  type$_6mLe_aQR4TjGWvpZdG1JEaA.vhQABgR4TjGWvpZdG1JEaA = function ()
  {
    var a = this;

  };
  var ctor$vhQABgR4TjGWvpZdG1JEaA = _6mLe_aQR4TjGWvpZdG1JEaA.ctor = $ctor$(null, 'vhQABgR4TjGWvpZdG1JEaA', type$_6mLe_aQR4TjGWvpZdG1JEaA);

  // ScriptCoreLib.Shared.Drawing.Size.Of
  function vRQABgR4TjGWvpZdG1JEaA(b, c)
  {
    var d, e;

    d = new ctor$vhQABgR4TjGWvpZdG1JEaA();
    d.Width = b;
    d.Height = c;
    e = d;
    return e;
  };

  // ScriptCoreLib.Shared.Drawing.Point`1
  function N5CTXpqMhzKpFtmPjF_atWQ(){};
  N5CTXpqMhzKpFtmPjF_atWQ.TypeName = "Point_1";
  N5CTXpqMhzKpFtmPjF_atWQ.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$N5CTXpqMhzKpFtmPjF_atWQ = N5CTXpqMhzKpFtmPjF_atWQ.prototype;
  type$N5CTXpqMhzKpFtmPjF_atWQ.constructor = N5CTXpqMhzKpFtmPjF_atWQ;
  type$N5CTXpqMhzKpFtmPjF_atWQ.X = null;
  type$N5CTXpqMhzKpFtmPjF_atWQ.Y = null;
  type$N5CTXpqMhzKpFtmPjF_atWQ.$0 = {};
  type$N5CTXpqMhzKpFtmPjF_atWQ.$0.$0 = 'Point`1';
  type$N5CTXpqMhzKpFtmPjF_atWQ.$0.$1 = 'vxQABpqMhzKpFtmPjF_atWQ';

  var basector$N5CTXpqMhzKpFtmPjF_atWQ = $ctor$(null, null, type$N5CTXpqMhzKpFtmPjF_atWQ);
  // ScriptCoreLib.Shared.Drawing.Point`1..ctor
  type$N5CTXpqMhzKpFtmPjF_atWQ.vxQABpqMhzKpFtmPjF_atWQ = function ()
  {
    var a = this;

  };
  var ctor$vxQABpqMhzKpFtmPjF_atWQ = N5CTXpqMhzKpFtmPjF_atWQ.ctor = $ctor$(null, 'vxQABpqMhzKpFtmPjF_atWQ', type$N5CTXpqMhzKpFtmPjF_atWQ);

  // ScriptCoreLib.Shared.Drawing.Point
  function s3R06oqVzDa8F5yDSGjsZA(){};
  s3R06oqVzDa8F5yDSGjsZA.TypeName = "Point";
  s3R06oqVzDa8F5yDSGjsZA.Assembly = W_bfCHB0Un0CgJ_aixUydYLg;
  var type$s3R06oqVzDa8F5yDSGjsZA = s3R06oqVzDa8F5yDSGjsZA.prototype = new N5CTXpqMhzKpFtmPjF_atWQ();
  type$s3R06oqVzDa8F5yDSGjsZA.constructor = s3R06oqVzDa8F5yDSGjsZA;
  type$s3R06oqVzDa8F5yDSGjsZA.$0 = {};
  type$s3R06oqVzDa8F5yDSGjsZA.$0.$0 = 'Point';

  var basector$s3R06oqVzDa8F5yDSGjsZA = $ctor$(basector$N5CTXpqMhzKpFtmPjF_atWQ, null, type$s3R06oqVzDa8F5yDSGjsZA);
  // ScriptCoreLib.Shared.Drawing.Point..ctor
  type$s3R06oqVzDa8F5yDSGjsZA.xhQABoqVzDa8F5yDSGjsZA = function (b, c)
  {
    var a = this;

    a.vxQABpqMhzKpFtmPjF_atWQ();
    a.X = b;
    a.Y = c;
  };
  var ctor$xhQABoqVzDa8F5yDSGjsZA = $ctor$(basector$N5CTXpqMhzKpFtmPjF_atWQ, 'xhQABoqVzDa8F5yDSGjsZA', type$s3R06oqVzDa8F5yDSGjsZA);

  // ScriptCoreLib.Shared.Drawing.Point.WithMargin
  type$s3R06oqVzDa8F5yDSGjsZA.wBQABoqVzDa8F5yDSGjsZA = function (b)
  {
    var a = this, c;

    c = uBQABq43Ozes59U5VdfFiw((a.X - b), (a.Y - b), (b * 2), (b * 2));
    return c;
  };

  // ScriptCoreLib.Shared.Drawing.Point.op_Multiply
  function wRQABoqVzDa8F5yDSGjsZA(b, c)
  {
    var d;

    d = new ctor$xhQABoqVzDa8F5yDSGjsZA((b.X * c), (b.Y * c));
    return d;
  };

  // ScriptCoreLib.Shared.Drawing.Point.op_Division
  function whQABoqVzDa8F5yDSGjsZA(b, c)
  {
    var d;

    d = new ctor$xhQABoqVzDa8F5yDSGjsZA((b.X / c), (b.Y / c));
    return d;
  };

  // ScriptCoreLib.Shared.Drawing.Point.Min
  type$s3R06oqVzDa8F5yDSGjsZA.wxQABoqVzDa8F5yDSGjsZA = function (b)
  {
    var a = this, c, d, e;

    c = new ctor$xhQABoqVzDa8F5yDSGjsZA(a.X, a.Y);
    e = !(c.X > b.X);

    if (!e)
    {
      c.X = b.X;
    }

    e = !(c.Y > b.Y);

    if (!e)
    {
      c.Y = b.Y;
    }

    d = c;
    return d;
  };

  // ScriptCoreLib.Shared.Drawing.Point.Max
  type$s3R06oqVzDa8F5yDSGjsZA.xBQABoqVzDa8F5yDSGjsZA = function (b)
  {
    var a = this, c, d, e;

    c = new ctor$xhQABoqVzDa8F5yDSGjsZA(a.X, a.Y);
    e = !(c.X < b.X);

    if (!e)
    {
      c.X = b.X;
    }

    e = !(c.Y < b.Y);

    if (!e)
    {
      c.Y = b.Y;
    }

    d = c;
    return d;
  };

  // ScriptCoreLib.Shared.Drawing.Point.get_Zero
  function xRQABoqVzDa8F5yDSGjsZA()
  {
    var b;

    b = new ctor$xhQABoqVzDa8F5yDSGjsZA(0, 0);
    return b;
  };

  // ScriptCoreLib.Shared.Drawing.Point.ToString
  type$s3R06oqVzDa8F5yDSGjsZA.toString /* ScriptCoreLib.Shared.Drawing.Point.ToString */ = function ()
  {
    var a = this, b, c;

    c = [
      '[',
      new Number(a.X),
      ', ',
      new Number(a.Y),
      ']'
    ];
    b = fBMABjDeCj_aRJzaBmU9SJg(c);
    return b;
  };
    s3R06oqVzDa8F5yDSGjsZA.prototype.toString /* System.Object.ToString */ = s3R06oqVzDa8F5yDSGjsZA.prototype.toString /* ScriptCoreLib.Shared.Drawing.Point.ToString */;

  // ScriptCoreLib.Shared.Drawing.Point.AsPosition
  type$s3R06oqVzDa8F5yDSGjsZA.yBQABoqVzDa8F5yDSGjsZA = function ()
  {
    var a = this, b;

    b = fxMABjDeCj_aRJzaBmU9SJg(new Number(a.X), ' ', new Number(a.Y));
    return b;
  };

  // ScriptCoreLib.Shared.Drawing.Point.Offset
  type$s3R06oqVzDa8F5yDSGjsZA.yRQABoqVzDa8F5yDSGjsZA = function (b)
  {
    var a = this;

    a.X = (a.X + b.X);
    a.Y = (a.Y + b.Y);
  };

  // ScriptCoreLib.Shared.Drawing.Point.CopyTo
  type$s3R06oqVzDa8F5yDSGjsZA.yhQABoqVzDa8F5yDSGjsZA = function (b)
  {
    var a = this;

    b.X = a.X;
    b.Y = a.Y;
  };

  // ScriptCoreLib.Shared.Drawing.Point.op_Subtraction
  function yxQABoqVzDa8F5yDSGjsZA(b, c)
  {
    var d;

    d = new ctor$xhQABoqVzDa8F5yDSGjsZA((b.X - c.X), (b.Y - c.Y));
    return d;
  };

  // ScriptCoreLib.Shared.Drawing.Point.op_Addition
  function zBQABoqVzDa8F5yDSGjsZA(b, c)
  {
    var d;

    d = new ctor$xhQABoqVzDa8F5yDSGjsZA((b.X + c.X), (b.Y + c.Y));
    return d;
  };

  // ScriptCoreLib.Shared.Drawing.Point.op_Division
  function zRQABoqVzDa8F5yDSGjsZA(b, c)
  {
    var d;

    d = new ctor$xhQABoqVzDa8F5yDSGjsZA((b.X / c), (b.Y / c));
    return d;
  };

  // ScriptCoreLib.Shared.Drawing.Point.op_Multiply
  function zhQABoqVzDa8F5yDSGjsZA(b, c)
  {
    var d;

    d = new ctor$xhQABoqVzDa8F5yDSGjsZA((b.X * c), (b.Y * c));
    return d;
  };

  // ScriptCoreLib.Shared.Drawing.Point.Of
  function zxQABoqVzDa8F5yDSGjsZA(b)
  {
    var c, d;

    d = !(b == null);

    if (!d)
    {
      c = new ctor$xhQABoqVzDa8F5yDSGjsZA(0, 0);
      return c;
    }

    c = new ctor$xhQABoqVzDa8F5yDSGjsZA(b.X, b.Y);
    return c;
  };

  // ScriptCoreLib.Shared.Drawing.Point.SpawnHelper
  function _0BQABoqVzDa8F5yDSGjsZA(b)
  {
    b.Target = zxQABoqVzDa8F5yDSGjsZA(b.Target);
  };

  // ScriptCoreLib.Shared.Drawing.Point.get_Z
  type$s3R06oqVzDa8F5yDSGjsZA._0RQABoqVzDa8F5yDSGjsZA = function ()
  {
    var a = this, b;

    b = ((a.X * a.X) + (a.Y * a.Y));
    return b;
  };

  // ScriptCoreLib.Shared.Drawing.Point.CompareRange
  type$s3R06oqVzDa8F5yDSGjsZA._0hQABoqVzDa8F5yDSGjsZA = function (b, c)
  {
    var a = this, d, e, f, g, h, i;

    d = (a.X - b.X);
    e = (a.Y - b.Y);
    f = ((d * d) + (e * e));
    g = (c * c);
    i = !(f == g);

    if (!i)
    {
      h = 0;
      return h;
    }

    i = !(f < g);

    if (!i)
    {
      h = -1;
      return h;
    }

    h = 1;
    return h;
  };

  // ScriptCoreLib.JavaScript.DOM.IEvent.get_IsReturn
  function _0xQABmvdsDi9SJKe_axnpQQ(a)
  {
    var b;

    b = (_1RQABmvdsDi9SJKe_axnpQQ(a) == 13);
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.IEvent.get_IsEscape
  function _1BQABmvdsDi9SJKe_axnpQQ(a)
  {
    var b;

    b = (_1RQABmvdsDi9SJKe_axnpQQ(a) == 27);
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.IEvent.get_KeyCode
  function _1RQABmvdsDi9SJKe_axnpQQ(a)
  {
    var b, c, d, e;

    b = 0;
    e = !BRMABr5xMzijfM5xNYhyrw(a, 'charCode');

    if (!e)
    {
      b = BhMABr5xMzijfM5xNYhyrw(a, 'charCode');
      e = !!b;

      if (!e)
      {
        e = !BRMABr5xMzijfM5xNYhyrw(a, 'keyCode');

        if (!e)
        {
          c = BhMABr5xMzijfM5xNYhyrw(a, 'keyCode');
          b = c;
        }

      }

    }
    else
    {
      e = !BRMABr5xMzijfM5xNYhyrw(a, 'keyCode');

      if (!e)
      {
        b = BhMABr5xMzijfM5xNYhyrw(a, 'keyCode');
      }

    }

    d = b;
    return d;
  };

  // ScriptCoreLib.JavaScript.DOM.IEvent.get_WheelDirection
  function _1hQABmvdsDi9SJKe_axnpQQ(a)
  {
    var b, c, d;

    b = 0;
    d = !BRMABr5xMzijfM5xNYhyrw(a, 'detail');

    if (!d)
    {
      b = (-BhMABr5xMzijfM5xNYhyrw(a, 'detail'));
    }

    d = !BRMABr5xMzijfM5xNYhyrw(a, 'wheelDelta');

    if (!d)
    {
      b = BhMABr5xMzijfM5xNYhyrw(a, 'wheelDelta');
    }

    d = !!b;

    if (!d)
    {
      c = 0;
      return c;
    }

    d = !(b > 0);

    if (!d)
    {
      c = 1;
      return c;
    }

    c = -1;
    return c;
  };

  // ScriptCoreLib.JavaScript.DOM.IEvent.get_OffsetX
  function _1xQABmvdsDi9SJKe_axnpQQ(a)
  {
    var b;

    b = ChMABr5xMzijfM5xNYhyrw(a, 'layerX', 'offsetX', 0);
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.IEvent.get_OffsetY
  function _2BQABmvdsDi9SJKe_axnpQQ(a)
  {
    var b;

    b = ChMABr5xMzijfM5xNYhyrw(a, 'layerY', 'offsetY', 0);
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.IEvent.get_CursorPosition
  function _2RQABmvdsDi9SJKe_axnpQQ(a)
  {
    var b;

    b = new ctor$xhQABoqVzDa8F5yDSGjsZA(_2xQABmvdsDi9SJKe_axnpQQ(a), _3BQABmvdsDi9SJKe_axnpQQ(a));
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.IEvent.get_OffsetPosition
  function _2hQABmvdsDi9SJKe_axnpQQ(a)
  {
    var b;

    b = new ctor$xhQABoqVzDa8F5yDSGjsZA(_1xQABmvdsDi9SJKe_axnpQQ(a), _2BQABmvdsDi9SJKe_axnpQQ(a));
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.IEvent.get_CursorX
  function _2xQABmvdsDi9SJKe_axnpQQ(a)
  {
    var b, c, d;

    b = 0;
    d = !BRMABr5xMzijfM5xNYhyrw(a, 'pageX');

    if (!d)
    {
      b = a.pageX;
    }
    else
    {
      d = !BRMABr5xMzijfM5xNYhyrw(a, 'clientX');

      if (!d)
      {
        b = a.clientX;
      }

    }

    c = (b + _5RQABmvdsDi9SJKe_axnpQQ(a).ownerDocument.documentElement.scrollLeft);
    return c;
  };

  // ScriptCoreLib.JavaScript.DOM.IEvent.get_CursorY
  function _3BQABmvdsDi9SJKe_axnpQQ(a)
  {
    var b, c, d;

    b = 0;
    d = !BRMABr5xMzijfM5xNYhyrw(a, 'pageY');

    if (!d)
    {
      b = a.pageY;
    }

    d = !BRMABr5xMzijfM5xNYhyrw(a, 'clientY');

    if (!d)
    {
      b = a.clientY;
    }

    c = (b + _5RQABmvdsDi9SJKe_axnpQQ(a).ownerDocument.documentElement.scrollTop);
    return c;
  };

  // ScriptCoreLib.JavaScript.DOM.IEvent.StopPropagation
  function _3RQABmvdsDi9SJKe_axnpQQ(a)
  {
    _3hQABmvdsDi9SJKe_axnpQQ(a);
  };

  // ScriptCoreLib.JavaScript.DOM.IEvent.InternalStopPropagation
  function _3hQABmvdsDi9SJKe_axnpQQ(a0) { 
            if (a0['cancelBubble'] != void(0)) 
                a0.cancelBubble = true;

            if (a0['stopPropagation'] != void(0)) 
                a0.stopPropagation(); 
             };
  // ScriptCoreLib.JavaScript.DOM.IEvent.get_MouseButton
  function _3xQABmvdsDi9SJKe_axnpQQ(a)
  {
    var b, c;

    c = !BRMABr5xMzijfM5xNYhyrw(a, 'which');

    if (!c)
    {
      c = !(a.which == 3);

      if (!c)
      {
        b = 3;
        return b;
      }

      c = !(a.which == 2);

      if (!c)
      {
        b = 2;
        return b;
      }

      c = !(a.which == 1);

      if (!c)
      {
        b = 1;
        return b;
      }

    }

    c = !BRMABr5xMzijfM5xNYhyrw(a, 'button');

    if (!c)
    {
      c = !(a.button == 2);

      if (!c)
      {
        b = 3;
        return b;
      }

      c = !(a.button == 4);

      if (!c)
      {
        b = 2;
        return b;
      }

      c = !(a.button == 1);

      if (!c)
      {
        b = 1;
        return b;
      }

    }

    b = 0;
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.IEvent.get_IsMozilla
  function _4BQABmvdsDi9SJKe_axnpQQ(a)
  {
    var b;

    b = _4RQABmvdsDi9SJKe_axnpQQ(a);
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.IEvent.InternalIsMozilla
  function _4RQABmvdsDi9SJKe_axnpQQ(a0) { 
            return !window['event'];
             };
  // ScriptCoreLib.JavaScript.DOM.IEvent.PreventDefault
  function _4hQABmvdsDi9SJKe_axnpQQ(a)
  {
    _4xQABmvdsDi9SJKe_axnpQQ(a);
  };

  // ScriptCoreLib.JavaScript.DOM.IEvent.InternalPreventDefault
  function _4xQABmvdsDi9SJKe_axnpQQ(a) { 
           
            if ('returnValue' in a)
                a.returnValue = false;

            if ('stopPropagation' in a) 
                a.preventDefault(); 
             };
  // ScriptCoreLib.JavaScript.DOM.IEvent.InternalEvent
  function _5BQABmvdsDi9SJKe_axnpQQ(a0) { 
            if (a0['target'] != void(0)) 
                return a0.target;
            if (a0['srcElement'] != void(0)) 
                return a0.srcElement;
             };
  // ScriptCoreLib.JavaScript.DOM.IEvent.get_Element
  function _5RQABmvdsDi9SJKe_axnpQQ(a)
  {
    var b;

    b = _5BQABmvdsDi9SJKe_axnpQQ(a);
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.IEvent.initMouseEvent
  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLTextArea.get_Lines
  function _6BQABkLNtTOpMZjL2GlE4w(a)
  {
    var b;

    b = NQ4ABqPHMD_aEoYb_aoNHL9w(a.value);
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLTextArea.InternalConstructor
  function _6xQABkLNtTOpMZjL2GlE4w()
  {
    var b;

    b = UQAABtcctzqgvmwoTNwgqg('textarea');
    return b;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLTextArea.InternalConstructor
  function _7BQABkLNtTOpMZjL2GlE4w(b)
  {
    var c, d;

    c = _6xQABkLNtTOpMZjL2GlE4w();
    c.value = b;
    d = c;
    return d;
  };

  // Closure type for ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement+<>c__DisplayClass7
  function uOhATbb8UDSphiXtO1tz3g() {}  var type$uOhATbb8UDSphiXtO1tz3g = uOhATbb8UDSphiXtO1tz3g.prototype;
  type$uOhATbb8UDSphiXtO1tz3g.constructor = uOhATbb8UDSphiXtO1tz3g;
  type$uOhATbb8UDSphiXtO1tz3g.flag = false;
  type$uOhATbb8UDSphiXtO1tz3g._capture = null;
  type$uOhATbb8UDSphiXtO1tz3g.self = null;
  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement+<>c__DisplayClass7.<InternalCaptureMouse>b__3
  type$uOhATbb8UDSphiXtO1tz3g._InternalCaptureMouse_b__3 = function ()
  {
    var a = this;

    a.self.releaseCapture();
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement+<>c__DisplayClass7.<InternalCaptureMouse>b__4
  type$uOhATbb8UDSphiXtO1tz3g._InternalCaptureMouse_b__4 = function (b)
  {
    var a = this, c, d;

    d = !a.flag;

    if (!d)
    {
      return;
    }

    a.flag = 1;
    _3RQABmvdsDi9SJKe_axnpQQ(b);
    c = document.createEvent('MouseEvents');
    c.initMouseEvent(b.type, b.bubbles, b.cancelable, b.view, b.detail, b.screenX, b.screenY, new Number(b.clientX), new Number(b.clientY), new Boolean(b.ctrlKey), new Boolean(b.altKey), new Boolean(b.shiftKey), b.metaKey, new Number(b.button), b.relatedTarget);
    a.self.dispatchEvent(c);
    a.flag = 0;
  };

  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement+<>c__DisplayClass7.<InternalCaptureMouse>b__5
  type$uOhATbb8UDSphiXtO1tz3g._InternalCaptureMouse_b__5 = function ()
  {
    var a = this, b, c, d, e;

    c = MAAABNcctzqgvmwoTNwgqg;

    for (d = 0; (d < c.length); d++)
    {
      b = c[d];
      MwAABl_atOz_aFJQVdjiI7RQ(window, b, a._capture, 1);
    }

  };

  // Closure type for ScriptCoreLib.JavaScript.Runtime.Timer+<>c__DisplayClass2
  function XBYWZfIxUjayT78g3b_aPsA() {}  var type$XBYWZfIxUjayT78g3b_aPsA = XBYWZfIxUjayT78g3b_aPsA.prototype;
  type$XBYWZfIxUjayT78g3b_aPsA.constructor = XBYWZfIxUjayT78g3b_aPsA;
  type$XBYWZfIxUjayT78g3b_aPsA.__4__this = null;
  type$XBYWZfIxUjayT78g3b_aPsA.interval = 0;
  // ScriptCoreLib.JavaScript.Runtime.Timer+<>c__DisplayClass2.<.ctor>b__0
  type$XBYWZfIxUjayT78g3b_aPsA.__ctor_b__0 = function ()
  {
    var a = this, b;

    b = !(a.interval > 0);

    if (!b)
    {
      a.__4__this._3QIABilzizCnsZekUeVmgA(a.interval);
      return;
    }

    a.__4__this._2QIABilzizCnsZekUeVmgA();
  };

  // Closure type for ScriptCoreLib.JavaScript.Runtime.Timer+<>c__DisplayClass5
  function w9DU74gzczKRXuw3d3noSw() {}  var type$w9DU74gzczKRXuw3d3noSw = w9DU74gzczKRXuw3d3noSw.prototype;
  type$w9DU74gzczKRXuw3d3noSw.constructor = w9DU74gzczKRXuw3d3noSw;
  type$w9DU74gzczKRXuw3d3noSw.dx = null;
  // ScriptCoreLib.JavaScript.Runtime.Timer+<>c__DisplayClass5.<Do>b__4
  type$w9DU74gzczKRXuw3d3noSw._Do_b__4 = function (b)
  {
    var a = this, c, d;

    d = !(a.dx.length > 0);

    if (!d)
    {
      c = a.dx.shift();
      d = (c == null);

      if (!d)
      {
        c.Invoke();
      }

      return;
    }

    b._4QIABilzizCnsZekUeVmgA();
  };

  // Closure type for ScriptCoreLib.JavaScript.Runtime.Timer+<>c__DisplayClass8
  function JeHOowsSMTebt9kMJXYf5w() {}  var type$JeHOowsSMTebt9kMJXYf5w = JeHOowsSMTebt9kMJXYf5w.prototype;
  type$JeHOowsSMTebt9kMJXYf5w.constructor = JeHOowsSMTebt9kMJXYf5w;
  type$JeHOowsSMTebt9kMJXYf5w.h = null;
  // ScriptCoreLib.JavaScript.Runtime.Timer+<>c__DisplayClass8.<DoAsync>b__7
  type$JeHOowsSMTebt9kMJXYf5w._DoAsync_b__7 = function (b)
  {
    var a = this;

    a.h.Invoke();
  };

  // Closure type for ScriptCoreLib.JavaScript.Runtime.Timer+<>c__DisplayClassb
  function baR6i9v8HDWWH5n0EvcT5Q() {}  var type$baR6i9v8HDWWH5n0EvcT5Q = baR6i9v8HDWWH5n0EvcT5Q.prototype;
  type$baR6i9v8HDWWH5n0EvcT5Q.constructor = baR6i9v8HDWWH5n0EvcT5Q;
  type$baR6i9v8HDWWH5n0EvcT5Q.timer = null;
  type$baR6i9v8HDWWH5n0EvcT5Q.p = null;
  type$baR6i9v8HDWWH5n0EvcT5Q.h = null;
  // ScriptCoreLib.JavaScript.Runtime.Timer+<>c__DisplayClassb.<Trigger>b__a
  type$baR6i9v8HDWWH5n0EvcT5Q._Trigger_b__a = function (b)
  {
    var a = this, c;

    c = !RgsABgF8xDasrw7uIe6gQA(a.p);

    if (!c)
    {
      a.timer._4QIABilzizCnsZekUeVmgA();
      sggABrDT9zCTidQoMa3Dig(a.h);
    }

  };

  // Closure type for ScriptCoreLib.JavaScript.DOM.HTML.IHTMLImage+<>c__DisplayClass1
  function ji9wMVNl0DaYU86TBx_bMlQ() {}  var type$ji9wMVNl0DaYU86TBx_bMlQ = ji9wMVNl0DaYU86TBx_bMlQ.prototype;
  type$ji9wMVNl0DaYU86TBx_bMlQ.constructor = ji9wMVNl0DaYU86TBx_bMlQ;
  type$ji9wMVNl0DaYU86TBx_bMlQ.t2 = null;
  type$ji9wMVNl0DaYU86TBx_bMlQ.__4__this = null;
  type$ji9wMVNl0DaYU86TBx_bMlQ.e = null;
  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLImage+<>c__DisplayClass1.<InvokeOnComplete>b__0
  type$ji9wMVNl0DaYU86TBx_bMlQ._InvokeOnComplete_b__0 = function (b)
  {
    var a = this, c;

    c = !a.__4__this.complete;

    if (!c)
    {
      a.t2._4QIABilzizCnsZekUeVmgA();
      a.e.Invoke(a.__4__this);
    }

  };

  // Anonymous type
  function VIJu8dnAcjyowfCbdKBmXA() {}  var type$VIJu8dnAcjyowfCbdKBmXA = VIJu8dnAcjyowfCbdKBmXA.prototype;
  type$VIJu8dnAcjyowfCbdKBmXA.constructor = VIJu8dnAcjyowfCbdKBmXA;
  type$VIJu8dnAcjyowfCbdKBmXA._mode_i__Field = null;
  type$VIJu8dnAcjyowfCbdKBmXA._access_i__Field = null;
  type$VIJu8dnAcjyowfCbdKBmXA._share_i__Field = null;
  // <>f__AnonymousType0`3.get_mode
  type$VIJu8dnAcjyowfCbdKBmXA.get_mode = function ()
  {
    return this._mode_i__Field;
  };

  // <>f__AnonymousType0`3.get_access
  type$VIJu8dnAcjyowfCbdKBmXA.get_access = function ()
  {
    return this._access_i__Field;
  };

  // <>f__AnonymousType0`3.get_share
  type$VIJu8dnAcjyowfCbdKBmXA.get_share = function ()
  {
    return this._share_i__Field;
  };

  // <>f__AnonymousType0`3.ToString
  type$VIJu8dnAcjyowfCbdKBmXA.toString /* <>f__AnonymousType0`3.ToString */ = function ()
  {
    var a = this, b, c;

    b = new ctor$_1REABhHFijqvYJf1nSns2Q();
    b._2hEABhHFijqvYJf1nSns2Q('{ mode = ');
    b._2xEABhHFijqvYJf1nSns2Q(a._mode_i__Field);
    b._2hEABhHFijqvYJf1nSns2Q(', access = ');
    b._2xEABhHFijqvYJf1nSns2Q(a._access_i__Field);
    b._2hEABhHFijqvYJf1nSns2Q(', share = ');
    b._2xEABhHFijqvYJf1nSns2Q(a._share_i__Field);
    b._2hEABhHFijqvYJf1nSns2Q(' }');
    c = (b+'');
    return c;
  };
    VIJu8dnAcjyowfCbdKBmXA.prototype.toString /* System.Object.ToString */ = VIJu8dnAcjyowfCbdKBmXA.prototype.toString /* <>f__AnonymousType0`3.ToString */;

  // <>f__AnonymousType0`3.Equals
  type$VIJu8dnAcjyowfCbdKBmXA.Equals = function (b)
  {
    throw 'Not implemented, Equals';
  };
    VIJu8dnAcjyowfCbdKBmXA.prototype.AwAABnwCHD6Y1dqcmGKqIQ = VIJu8dnAcjyowfCbdKBmXA.prototype.Equals;

  // <>f__AnonymousType0`3.GetHashCode
  type$VIJu8dnAcjyowfCbdKBmXA.GetHashCode = function ()
  {
    throw 'Not implemented, GetHashCode';
  };
    VIJu8dnAcjyowfCbdKBmXA.prototype.BwAABnwCHD6Y1dqcmGKqIQ = VIJu8dnAcjyowfCbdKBmXA.prototype.GetHashCode;

  // <>f__AnonymousType0`3..ctor
  type$VIJu8dnAcjyowfCbdKBmXA.GxUABtnAcjyowfCbdKBmXA = function (b, c, d)
  {
    var a = this;

    a._mode_i__Field = b;
    a._access_i__Field = c;
    a._share_i__Field = d;
  };
  var ctor$GxUABtnAcjyowfCbdKBmXA = $ctor$(null, 'GxUABtnAcjyowfCbdKBmXA', type$VIJu8dnAcjyowfCbdKBmXA);
  // Closure type for ScriptCoreLib.JavaScript.Runtime.Fader+<>c__DisplayClass2
  function rcNS0INtdzaH79GFOgRlFw() {}  var type$rcNS0INtdzaH79GFOgRlFw = rcNS0INtdzaH79GFOgRlFw.prototype;
  type$rcNS0INtdzaH79GFOgRlFw.constructor = rcNS0INtdzaH79GFOgRlFw;
  type$rcNS0INtdzaH79GFOgRlFw.target = null;
  type$rcNS0INtdzaH79GFOgRlFw.fadetime = 0;
  // ScriptCoreLib.JavaScript.Runtime.Fader+<>c__DisplayClass2.<FadeOut>b__0
  type$rcNS0INtdzaH79GFOgRlFw._FadeOut_b__0 = function (b)
  {
    var a = this, c;

    c = /* DOMCreateType */new _7Jb19DyNcTqqawPUiKaN_bg();
    c.CS___8__locals3 = a;
    c.a = null;
    c.a = new ctor$_1gIABilzizCnsZekUeVmgA(new ctor$KQsABoVjnjetV_aXG4GFkBQ(c, '_FadeOut_b__1'));
    c.a._2wIABilzizCnsZekUeVmgA((a.fadetime / 25), 25);
  };

  // Closure type for ScriptCoreLib.JavaScript.Runtime.Fader+<>c__DisplayClass2+<>c__DisplayClass4
  function _7Jb19DyNcTqqawPUiKaN_bg() {}  var type$_7Jb19DyNcTqqawPUiKaN_bg = _7Jb19DyNcTqqawPUiKaN_bg.prototype;
  type$_7Jb19DyNcTqqawPUiKaN_bg.constructor = _7Jb19DyNcTqqawPUiKaN_bg;
  type$_7Jb19DyNcTqqawPUiKaN_bg.CS___8__locals3 = null;
  type$_7Jb19DyNcTqqawPUiKaN_bg.a = null;
  // ScriptCoreLib.JavaScript.Runtime.Fader+<>c__DisplayClass2+<>c__DisplayClass4.<FadeOut>b__1
  type$_7Jb19DyNcTqqawPUiKaN_bg._FadeOut_b__1 = function (b)
  {
    var a = this, c;

    UA0ABoNSrD_aFizz5n6sJfw(a.CS___8__locals3.target.style, (1 - (a.a.Counter / a.a.TimeToLive)));
    c = !(a.a.Counter == a.a.TimeToLive);

    if (!c)
    {
      cAsABt0jLD6yDQ0X6wt5_aw(a.CS___8__locals3.target);
    }

  };

  // Closure type for ScriptCoreLib.JavaScript.Runtime.Fader+<>c__DisplayClass8
  function _6yZECZP9ozm5xQDaInToeg() {}  var type$_6yZECZP9ozm5xQDaInToeg = _6yZECZP9ozm5xQDaInToeg.prototype;
  type$_6yZECZP9ozm5xQDaInToeg.constructor = _6yZECZP9ozm5xQDaInToeg;
  type$_6yZECZP9ozm5xQDaInToeg.target = null;
  type$_6yZECZP9ozm5xQDaInToeg.fadetime = 0;
  type$_6yZECZP9ozm5xQDaInToeg.done = null;
  // ScriptCoreLib.JavaScript.Runtime.Fader+<>c__DisplayClass8.<Fade>b__6
  type$_6yZECZP9ozm5xQDaInToeg._Fade_b__6 = function (b)
  {
    var a = this, c;

    c = /* DOMCreateType */new s_b6mYBuJvT6rx_bgkPK5kRA();
    c.CS___8__locals9 = a;
    c.a = null;
    c.a = new ctor$_1gIABilzizCnsZekUeVmgA(new ctor$KQsABoVjnjetV_aXG4GFkBQ(c, '_Fade_b__7'));
    c.a._2wIABilzizCnsZekUeVmgA((a.fadetime / 25), 25);
  };

  // Closure type for ScriptCoreLib.JavaScript.Runtime.Fader+<>c__DisplayClass8+<>c__DisplayClassa
  function s_b6mYBuJvT6rx_bgkPK5kRA() {}  var type$s_b6mYBuJvT6rx_bgkPK5kRA = s_b6mYBuJvT6rx_bgkPK5kRA.prototype;
  type$s_b6mYBuJvT6rx_bgkPK5kRA.constructor = s_b6mYBuJvT6rx_bgkPK5kRA;
  type$s_b6mYBuJvT6rx_bgkPK5kRA.CS___8__locals9 = null;
  type$s_b6mYBuJvT6rx_bgkPK5kRA.a = null;
  // ScriptCoreLib.JavaScript.Runtime.Fader+<>c__DisplayClass8+<>c__DisplayClassa.<Fade>b__7
  type$s_b6mYBuJvT6rx_bgkPK5kRA._Fade_b__7 = function (b)
  {
    var a = this, c;

    UA0ABoNSrD_aFizz5n6sJfw(a.CS___8__locals9.target.style, (1 - (a.a.Counter / a.a.TimeToLive)));
    c = !(a.a.Counter == a.a.TimeToLive);

    if (!c)
    {
      c = (a.CS___8__locals9.done == null);

      if (!c)
      {
        a.CS___8__locals9.done.Invoke();
      }

    }

  };

  // Closure type for ScriptCoreLib.JavaScript.Runtime.Fader+<>c__DisplayClasse
  function _4f5T_a0uoNDWSVnEPOnQ_agw() {}  var type$_4f5T_a0uoNDWSVnEPOnQ_agw = _4f5T_a0uoNDWSVnEPOnQ_agw.prototype;
  type$_4f5T_a0uoNDWSVnEPOnQ_agw.constructor = _4f5T_a0uoNDWSVnEPOnQ_agw;
  type$_4f5T_a0uoNDWSVnEPOnQ_agw.target = null;
  type$_4f5T_a0uoNDWSVnEPOnQ_agw.fadetime = 0;
  type$_4f5T_a0uoNDWSVnEPOnQ_agw.cotargets = null;
  // ScriptCoreLib.JavaScript.Runtime.Fader+<>c__DisplayClasse.<FadeAndRemove>b__c
  type$_4f5T_a0uoNDWSVnEPOnQ_agw._FadeAndRemove_b__c = function (b)
  {
    var a = this, c;

    c = /* DOMCreateType */new ijjquPJdCze_aPbvT1rGJPQ();
    c.CS___8__localsf = a;
    c.a = null;
    c.a = new ctor$_1gIABilzizCnsZekUeVmgA(new ctor$KQsABoVjnjetV_aXG4GFkBQ(c, '_FadeAndRemove_b__d'));
    c.a._2wIABilzizCnsZekUeVmgA((a.fadetime / 25), 25);
  };

  // Closure type for ScriptCoreLib.JavaScript.Runtime.Fader+<>c__DisplayClasse+<>c__DisplayClass10
  function ijjquPJdCze_aPbvT1rGJPQ() {}  var type$ijjquPJdCze_aPbvT1rGJPQ = ijjquPJdCze_aPbvT1rGJPQ.prototype;
  type$ijjquPJdCze_aPbvT1rGJPQ.constructor = ijjquPJdCze_aPbvT1rGJPQ;
  type$ijjquPJdCze_aPbvT1rGJPQ.CS___8__localsf = null;
  type$ijjquPJdCze_aPbvT1rGJPQ.a = null;
  // ScriptCoreLib.JavaScript.Runtime.Fader+<>c__DisplayClasse+<>c__DisplayClass10.<FadeAndRemove>b__d
  type$ijjquPJdCze_aPbvT1rGJPQ._FadeAndRemove_b__d = function (b)
  {
    var a = this, c, d, e, f;

    UA0ABoNSrD_aFizz5n6sJfw(a.CS___8__localsf.target.style, (1 - (a.a.Counter / a.a.TimeToLive)));
    d = !(a.a.Counter == a.a.TimeToLive);

    if (!d)
    {
      cgsABt0jLD6yDQ0X6wt5_aw(a.CS___8__localsf.target);
      e = a.CS___8__localsf.cotargets;

      for (f = 0; (f < e.length); f++)
      {
        c = e[f];
        cgsABt0jLD6yDQ0X6wt5_aw(c);
      }

    }

  };

  // Closure type for ScriptCoreLib.JavaScript.Runtime.Fader+<>c__DisplayClass16
  function UeC9_b_a7LCzCo_aWeeKy6C6A() {}  var type$UeC9_b_a7LCzCo_aWeeKy6C6A = UeC9_b_a7LCzCo_aWeeKy6C6A.prototype;
  type$UeC9_b_a7LCzCo_aWeeKy6C6A.constructor = UeC9_b_a7LCzCo_aWeeKy6C6A;
  type$UeC9_b_a7LCzCo_aWeeKy6C6A.e = null;
  // ScriptCoreLib.JavaScript.Runtime.Fader+<>c__DisplayClass16.<FlashAndFadeOut>b__12
  type$UeC9_b_a7LCzCo_aWeeKy6C6A._FlashAndFadeOut_b__12 = function ()
  {
    var a = this;

    cAsABt0jLD6yDQ0X6wt5_aw(a.e);
  };

  // ScriptCoreLib.JavaScript.Runtime.Fader+<>c__DisplayClass16.<FlashAndFadeOut>b__13
  type$UeC9_b_a7LCzCo_aWeeKy6C6A._FlashAndFadeOut_b__13 = function ()
  {
    var a = this;

    bgsABt0jLD6yDQ0X6wt5_aw(a.e);
  };

  // ScriptCoreLib.JavaScript.Runtime.Fader+<>c__DisplayClass16.<FlashAndFadeOut>b__14
  type$UeC9_b_a7LCzCo_aWeeKy6C6A._FlashAndFadeOut_b__14 = function ()
  {
    var a = this;

    cAsABt0jLD6yDQ0X6wt5_aw(a.e);
  };

  // ScriptCoreLib.JavaScript.Runtime.Fader+<>c__DisplayClass16.<FlashAndFadeOut>b__15
  type$UeC9_b_a7LCzCo_aWeeKy6C6A._FlashAndFadeOut_b__15 = function ()
  {
    var a = this;

    bgsABt0jLD6yDQ0X6wt5_aw(a.e);
  };

  // Closure type for ScriptCoreLib.JavaScript.Native+<>c__DisplayClass4
  function C7kJwBz59TCfTnpwidtIag() {}  var type$C7kJwBz59TCfTnpwidtIag = C7kJwBz59TCfTnpwidtIag.prototype;
  type$C7kJwBz59TCfTnpwidtIag.constructor = C7kJwBz59TCfTnpwidtIag;
  type$C7kJwBz59TCfTnpwidtIag.id = null;
  type$C7kJwBz59TCfTnpwidtIag.Spawn = null;
  // ScriptCoreLib.JavaScript.Native+<>c__DisplayClass4.<Spawn>b__2
  type$C7kJwBz59TCfTnpwidtIag._Spawn_b__2 = function (b)
  {
    var a = this;

    Ig4ABqPHMD_aEoYb_aoNHL9w(DQoABrOeqzW195CXZ74N9w(document, a.id), new ctor$KQsABoVjnjetV_aXG4GFkBQ(a, '_Spawn_b__3'));
  };

  // ScriptCoreLib.JavaScript.Native+<>c__DisplayClass4.<Spawn>b__3
  type$C7kJwBz59TCfTnpwidtIag._Spawn_b__3 = function (b)
  {
    var a = this;

    IA8ABqiGvTSwHMGw_bObBeQ(gRMABjDeCj_aRJzaBmU9SJg('spawn: {', a.id, '}'));
    a.Spawn.Invoke(b);
  };

  // Closure type for ScriptCoreLib.JavaScript.Native+<>c__DisplayClass8
  function iayK3Y_bp2zi_ajOCidnWQkA() {}  var type$iayK3Y_bp2zi_ajOCidnWQkA = iayK3Y_bp2zi_ajOCidnWQkA.prototype;
  type$iayK3Y_bp2zi_ajOCidnWQkA.constructor = iayK3Y_bp2zi_ajOCidnWQkA;
  type$iayK3Y_bp2zi_ajOCidnWQkA.id = null;
  type$iayK3Y_bp2zi_ajOCidnWQkA.s = null;
  // ScriptCoreLib.JavaScript.Native+<>c__DisplayClass8.<Spawn>b__6
  type$iayK3Y_bp2zi_ajOCidnWQkA._Spawn_b__6 = function (b)
  {
    var a = this;

    Ig4ABqPHMD_aEoYb_aoNHL9w(DQoABrOeqzW195CXZ74N9w(document, a.id), new ctor$KQsABoVjnjetV_aXG4GFkBQ(a, '_Spawn_b__7'));
  };

  // ScriptCoreLib.JavaScript.Native+<>c__DisplayClass8.<Spawn>b__7
  type$iayK3Y_bp2zi_ajOCidnWQkA._Spawn_b__7 = function (b)
  {
    var a = this;

    IA8ABqiGvTSwHMGw_bObBeQ(gRMABjDeCj_aRJzaBmU9SJg('spawn: {', a.id, '}'));
    a.s.Invoke(b, a.id);
  };

  // Closure type for ScriptCoreLib.JavaScript.DOM.HTML.IHTMLButton+<>c__DisplayClass1
  function _6P0dN8arYzGTx82LfiadXw() {}  var type$_6P0dN8arYzGTx82LfiadXw = _6P0dN8arYzGTx82LfiadXw.prototype;
  type$_6P0dN8arYzGTx82LfiadXw.constructor = _6P0dN8arYzGTx82LfiadXw;
  type$_6P0dN8arYzGTx82LfiadXw.h = null;
  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLButton+<>c__DisplayClass1.<Create>b__0
  type$_6P0dN8arYzGTx82LfiadXw._Create_b__0 = function (b)
  {
    var a = this;

    sggABrDT9zCTidQoMa3Dig(a.h);
  };

  // Anonymous type
  function gWbJ53YBADScCNBkNEK2Qw() {}  var type$gWbJ53YBADScCNBkNEK2Qw = gWbJ53YBADScCNBkNEK2Qw.prototype;
  type$gWbJ53YBADScCNBkNEK2Qw.constructor = gWbJ53YBADScCNBkNEK2Qw;
  type$gWbJ53YBADScCNBkNEK2Qw._Position_i__Field = null;
  type$gWbJ53YBADScCNBkNEK2Qw._Length_i__Field = null;
  type$gWbJ53YBADScCNBkNEK2Qw._num_i__Field = null;
  type$gWbJ53YBADScCNBkNEK2Qw._value_i__Field = null;
  // <>f__AnonymousType1`4.get_Position
  type$gWbJ53YBADScCNBkNEK2Qw.get_Position = function ()
  {
    return this._Position_i__Field;
  };

  // <>f__AnonymousType1`4.get_Length
  type$gWbJ53YBADScCNBkNEK2Qw.get_Length = function ()
  {
    return this._Length_i__Field;
  };

  // <>f__AnonymousType1`4.get_num
  type$gWbJ53YBADScCNBkNEK2Qw.get_num = function ()
  {
    return this._num_i__Field;
  };

  // <>f__AnonymousType1`4.get_value
  type$gWbJ53YBADScCNBkNEK2Qw.get_value = function ()
  {
    return this._value_i__Field;
  };

  // <>f__AnonymousType1`4.ToString
  type$gWbJ53YBADScCNBkNEK2Qw.toString /* <>f__AnonymousType1`4.ToString */ = function ()
  {
    var a = this, b, c;

    b = new ctor$_1REABhHFijqvYJf1nSns2Q();
    b._2hEABhHFijqvYJf1nSns2Q('{ Position = ');
    b._2xEABhHFijqvYJf1nSns2Q(a._Position_i__Field);
    b._2hEABhHFijqvYJf1nSns2Q(', Length = ');
    b._2xEABhHFijqvYJf1nSns2Q(a._Length_i__Field);
    b._2hEABhHFijqvYJf1nSns2Q(', num = ');
    b._2xEABhHFijqvYJf1nSns2Q(a._num_i__Field);
    b._2hEABhHFijqvYJf1nSns2Q(', value = ');
    b._2xEABhHFijqvYJf1nSns2Q(a._value_i__Field);
    b._2hEABhHFijqvYJf1nSns2Q(' }');
    c = (b+'');
    return c;
  };
    gWbJ53YBADScCNBkNEK2Qw.prototype.toString /* System.Object.ToString */ = gWbJ53YBADScCNBkNEK2Qw.prototype.toString /* <>f__AnonymousType1`4.ToString */;

  // <>f__AnonymousType1`4.Equals
  type$gWbJ53YBADScCNBkNEK2Qw.Equals = function (b)
  {
    throw 'Not implemented, Equals';
  };
    gWbJ53YBADScCNBkNEK2Qw.prototype.AwAABnwCHD6Y1dqcmGKqIQ = gWbJ53YBADScCNBkNEK2Qw.prototype.Equals;

  // <>f__AnonymousType1`4.GetHashCode
  type$gWbJ53YBADScCNBkNEK2Qw.GetHashCode = function ()
  {
    throw 'Not implemented, GetHashCode';
  };
    gWbJ53YBADScCNBkNEK2Qw.prototype.BwAABnwCHD6Y1dqcmGKqIQ = gWbJ53YBADScCNBkNEK2Qw.prototype.GetHashCode;

  // <>f__AnonymousType1`4..ctor
  type$gWbJ53YBADScCNBkNEK2Qw.QRUABnYBADScCNBkNEK2Qw = function (b, c, d, e)
  {
    var a = this;

    a._Position_i__Field = b;
    a._Length_i__Field = c;
    a._num_i__Field = d;
    a._value_i__Field = e;
  };
  var ctor$QRUABnYBADScCNBkNEK2Qw = $ctor$(null, 'QRUABnYBADScCNBkNEK2Qw', type$gWbJ53YBADScCNBkNEK2Qw);
  // Anonymous type
  function XpUss5krLDCk5CUZLQTt9w() {}  var type$XpUss5krLDCk5CUZLQTt9w = XpUss5krLDCk5CUZLQTt9w.prototype;
  type$XpUss5krLDCk5CUZLQTt9w.constructor = XpUss5krLDCk5CUZLQTt9w;
  type$XpUss5krLDCk5CUZLQTt9w._Position_i__Field = null;
  type$XpUss5krLDCk5CUZLQTt9w._Length_i__Field = null;
  type$XpUss5krLDCk5CUZLQTt9w._num_i__Field = null;
  // <>f__AnonymousType2`3.get_Position
  type$XpUss5krLDCk5CUZLQTt9w.get_Position = function ()
  {
    return this._Position_i__Field;
  };

  // <>f__AnonymousType2`3.get_Length
  type$XpUss5krLDCk5CUZLQTt9w.get_Length = function ()
  {
    return this._Length_i__Field;
  };

  // <>f__AnonymousType2`3.get_num
  type$XpUss5krLDCk5CUZLQTt9w.get_num = function ()
  {
    return this._num_i__Field;
  };

  // <>f__AnonymousType2`3.ToString
  type$XpUss5krLDCk5CUZLQTt9w.toString /* <>f__AnonymousType2`3.ToString */ = function ()
  {
    var a = this, b, c;

    b = new ctor$_1REABhHFijqvYJf1nSns2Q();
    b._2hEABhHFijqvYJf1nSns2Q('{ Position = ');
    b._2xEABhHFijqvYJf1nSns2Q(a._Position_i__Field);
    b._2hEABhHFijqvYJf1nSns2Q(', Length = ');
    b._2xEABhHFijqvYJf1nSns2Q(a._Length_i__Field);
    b._2hEABhHFijqvYJf1nSns2Q(', num = ');
    b._2xEABhHFijqvYJf1nSns2Q(a._num_i__Field);
    b._2hEABhHFijqvYJf1nSns2Q(' }');
    c = (b+'');
    return c;
  };
    XpUss5krLDCk5CUZLQTt9w.prototype.toString /* System.Object.ToString */ = XpUss5krLDCk5CUZLQTt9w.prototype.toString /* <>f__AnonymousType2`3.ToString */;

  // <>f__AnonymousType2`3.Equals
  type$XpUss5krLDCk5CUZLQTt9w.Equals = function (b)
  {
    throw 'Not implemented, Equals';
  };
    XpUss5krLDCk5CUZLQTt9w.prototype.AwAABnwCHD6Y1dqcmGKqIQ = XpUss5krLDCk5CUZLQTt9w.prototype.Equals;

  // <>f__AnonymousType2`3.GetHashCode
  type$XpUss5krLDCk5CUZLQTt9w.GetHashCode = function ()
  {
    throw 'Not implemented, GetHashCode';
  };
    XpUss5krLDCk5CUZLQTt9w.prototype.BwAABnwCHD6Y1dqcmGKqIQ = XpUss5krLDCk5CUZLQTt9w.prototype.GetHashCode;

  // <>f__AnonymousType2`3..ctor
  type$XpUss5krLDCk5CUZLQTt9w.SRUABpkrLDCk5CUZLQTt9w = function (b, c, d)
  {
    var a = this;

    a._Position_i__Field = b;
    a._Length_i__Field = c;
    a._num_i__Field = d;
  };
  var ctor$SRUABpkrLDCk5CUZLQTt9w = $ctor$(null, 'SRUABpkrLDCk5CUZLQTt9w', type$XpUss5krLDCk5CUZLQTt9w);
  // Closure type for ScriptCoreLib.JavaScript.DOM.HTML.IHTMLDocument+<>c__DisplayClass1
  function Qfak8D_bmWj2yvyBRDJ4xOA() {}  var type$Qfak8D_bmWj2yvyBRDJ4xOA = Qfak8D_bmWj2yvyBRDJ4xOA.prototype;
  type$Qfak8D_bmWj2yvyBRDJ4xOA.constructor = Qfak8D_bmWj2yvyBRDJ4xOA;
  type$Qfak8D_bmWj2yvyBRDJ4xOA.className = null;
  // ScriptCoreLib.JavaScript.DOM.HTML.IHTMLDocument+<>c__DisplayClass1.<getElementsByClassName>b__0
  type$Qfak8D_bmWj2yvyBRDJ4xOA._getElementsByClassName_b__0 = function (b)
  {
    var a = this, c;

    c = 0;
    try
    {
      c = lxMABjDeCj_aRJzaBmU9SJg(b.Item.className, a.className);
    }
    catch (__exc)
    {
      c = 0;
    }
    b.Include = c;
  };

  // Closure type for ScriptCoreLib.JavaScript.Extensions.Extensions+<>c__DisplayClass1
  function jFOh6mqSAjClHaxCM7KWRQ() {}  var type$jFOh6mqSAjClHaxCM7KWRQ = jFOh6mqSAjClHaxCM7KWRQ.prototype;
  type$jFOh6mqSAjClHaxCM7KWRQ.constructor = jFOh6mqSAjClHaxCM7KWRQ;
  type$jFOh6mqSAjClHaxCM7KWRQ.alias = null;
  // ScriptCoreLib.JavaScript.Extensions.Extensions+<>c__DisplayClass1.<Spawn>b__0
  type$jFOh6mqSAjClHaxCM7KWRQ._Spawn_b__0 = function (b)
  {
    var a = this;

    sgMABjmXVjiltWVkuVHBiA(a.alias);
  };

  // Closure type for ScriptCoreLib.JavaScript.Extensions.Extensions+<>c__DisplayClass4
  function rqen1WoNYTG_bP1KupTgpsw() {}  var type$rqen1WoNYTG_bP1KupTgpsw = rqen1WoNYTG_bP1KupTgpsw.prototype;
  type$rqen1WoNYTG_bP1KupTgpsw.constructor = rqen1WoNYTG_bP1KupTgpsw;
  type$rqen1WoNYTG_bP1KupTgpsw.h = null;
  // ScriptCoreLib.JavaScript.Extensions.Extensions+<>c__DisplayClass4.<SpawnTo>b__3
  type$rqen1WoNYTG_bP1KupTgpsw._SpawnTo_b__3 = function (b)
  {
    var a = this;

    a.h.Invoke(b);
  };

  // Closure type for ScriptCoreLib.JavaScript.Extensions.Extensions+<>c__DisplayClass7`1
  function DSCszieMWziaVWaR7SimNg() {}  var type$DSCszieMWziaVWaR7SimNg = DSCszieMWziaVWaR7SimNg.prototype;
  type$DSCszieMWziaVWaR7SimNg.constructor = DSCszieMWziaVWaR7SimNg;
  type$DSCszieMWziaVWaR7SimNg.h = null;
  // ScriptCoreLib.JavaScript.Extensions.Extensions+<>c__DisplayClass7`1.<SpawnTo>b__6
  type$DSCszieMWziaVWaR7SimNg._SpawnTo_b__6 = function (b, c)
  {
    var a = this;

    a.h.Invoke(b);
  };

  // Closure type for ScriptCoreLib.JavaScript.Extensions.Extensions+<>c__DisplayClassa`1
  function llDStxfsVjK2NM94Dy3f_bw() {}  var type$llDStxfsVjK2NM94Dy3f_bw = llDStxfsVjK2NM94Dy3f_bw.prototype;
  type$llDStxfsVjK2NM94Dy3f_bw.constructor = llDStxfsVjK2NM94Dy3f_bw;
  type$llDStxfsVjK2NM94Dy3f_bw.KnownTypes = null;
  type$llDStxfsVjK2NM94Dy3f_bw.h = null;
  // ScriptCoreLib.JavaScript.Extensions.Extensions+<>c__DisplayClassa`1.<SpawnTo>b__9
  type$llDStxfsVjK2NM94Dy3f_bw._SpawnTo_b__9 = function (b)
  {
    var a = this, c, d, e, f;

    f = !lxMABjDeCj_aRJzaBmU9SJg(b.nodeName, 'SCRIPT');

    if (!f)
    {
      c = b;
      d = OQAABlWwyD6MDQKkPtM6Fg(b);
      f = !lxMABjDeCj_aRJzaBmU9SJg(c.type, 'text\u002fxml');

      if (!f)
      {
        e = uwUABnwZPj_az_b6m5XFzB4A(d);
        a.h.Invoke(dQsABt0jLD6yDQ0X6wt5_aw(e, a.KnownTypes), b);
      }
      else
      {
        f = !lxMABjDeCj_aRJzaBmU9SJg(c.type, 'text\u002fjson');

        if (!f)
        {
          a.h.Invoke(_4BIABr5xMzijfM5xNYhyrw(d), b);
        }

      }

    }

  };

  // Closure type for ScriptCoreLib.JavaScript.DOM.IWindow+<>c__DisplayClass1
  function zMV_bYJK_bfz_aBu6W1XgsETg() {}  var type$zMV_bYJK_bfz_aBu6W1XgsETg = zMV_bYJK_bfz_aBu6W1XgsETg.prototype;
  type$zMV_bYJK_bfz_aBu6W1XgsETg.constructor = zMV_bYJK_bfz_aBu6W1XgsETg;
  type$zMV_bYJK_bfz_aBu6W1XgsETg.value = null;
  // ScriptCoreLib.JavaScript.DOM.IWindow+<>c__DisplayClass1.<add_onbeforeunload>b__0
  type$zMV_bYJK_bfz_aBu6W1XgsETg._add_onbeforeunload_b__0 = function (b)
  {
    var a = this, c, d;

    c = new ctor$YhEABkKpEzGpiLUgRF75_bg();
    a.value.Invoke(c);
    b.returnValue = c.Text;
    d = c.Text;
    return d;
  };

  // Closure type for ScriptCoreLib.JavaScript.BCLImplementation.System.__Array+<>c__DisplayClass1`1
  function n_b1JAgjoYziWbWb_bI9huXg() {}  var type$n_b1JAgjoYziWbWb_bI9huXg = n_b1JAgjoYziWbWb_bI9huXg.prototype;
  type$n_b1JAgjoYziWbWb_bI9huXg.constructor = n_b1JAgjoYziWbWb_bI9huXg;
  type$n_b1JAgjoYziWbWb_bI9huXg.c = null;
  // ScriptCoreLib.JavaScript.BCLImplementation.System.__Array+<>c__DisplayClass1`1.<Sort>b__0
  type$n_b1JAgjoYziWbWb_bI9huXg._Sort_b__0 = function (b, c)
  {
    return this.c.Invoke(b, c);
  };

  // Closure type for ScriptCoreLib.JavaScript.Runtime.WorkPool+<>c__DisplayClass1
  function grnA8PMxvjau_aUwl9tFniA() {}  var type$grnA8PMxvjau_aUwl9tFniA = grnA8PMxvjau_aUwl9tFniA.prototype;
  type$grnA8PMxvjau_aUwl9tFniA.constructor = grnA8PMxvjau_aUwl9tFniA;
  type$grnA8PMxvjau_aUwl9tFniA.key = null;
  // ScriptCoreLib.JavaScript.Runtime.WorkPool+<>c__DisplayClass1.<Remove>b__0
  type$grnA8PMxvjau_aUwl9tFniA._Remove_b__0 = function (b)
  {
    var a = this, c;

    c = lxMABjDeCj_aRJzaBmU9SJg(b.Key, a.key);
    return c;
  };

  // Closure type for ScriptCoreLib.JavaScript.Controls.DragHelper+<>c__DisplayClass7
  function XDckZ6nFGzGCpT3OQKCfFA() {}  var type$XDckZ6nFGzGCpT3OQKCfFA = XDckZ6nFGzGCpT3OQKCfFA.prototype;
  type$XDckZ6nFGzGCpT3OQKCfFA.constructor = XDckZ6nFGzGCpT3OQKCfFA;
  type$XDckZ6nFGzGCpT3OQKCfFA.__4__this = null;
  type$XDckZ6nFGzGCpT3OQKCfFA.point = null;
  // ScriptCoreLib.JavaScript.Controls.DragHelper+<>c__DisplayClass7.<DragTo>b__6
  type$XDckZ6nFGzGCpT3OQKCfFA._DragTo_b__6 = function ()
  {
    var a = this;

    a.__4__this.Position = a.point;
    sggABrDT9zCTidQoMa3Dig(a.__4__this.DragMove);
  };

  // Are the references up to date?
  // Are they imported in the dependency sort order?
  W_bfCHB0Un0CgJ_aixUydYLg.Types = [FBMUy2aCrzG0XVBXGP7j9g,_4_aNPjRHFijqvYJf1nSns2Q,CGA9Rijy7DqW6zC1OmUAYA,vF5X5UKpEzGpiLUgRF75_bg,G2TFCdcYOj6koLXQMjq6PA,Yxho2_b1_afzqTeXth6kRKyA,_1n6ypcVTDjm0iPhLuf0mGg,_9_aEMqNzT7Ti0XThSh5FnRg,JhBtWXL_bGTm30vFuPKB5rQ,EOQvqcOtIzm5NAIKAKvPQg,wOR0Dn55WzWtTxnfDBbnkQ,zf0v_aGuINTivHicv_aPWjJw,vp1c5CVSVjSzSGzRnERclQ,hj_aCuhbA4D2OPLnwMhJAvw,T4NSm6iGvTSwHMGw_bObBeQ,LgFommn_bwTyJHmssIjkNiQ,wffJRvXKHjSzDZNs7b2S4g,Dhmwyss5uDO5UHO4wbYV7Q,__amoPTqpAQj_apuj6uPJmsSA,PZ9ITdF_bMzODN61n2IR6Jw,ulZ6nbrjrzGhrKOv71Rbfw,L5pu4WOwEjOAsqGKZ0msvA,_22cEaagPxjmDnjkmC_a5nbw,kgJWtah_alDaJsZAeVC_bgaA,p1D1pilS0Dabl5_a552x63g,QdvYmrjuJTGprs5OFzkcPA,cXhOvPhugDmzVMjtbtSocg,K72XfZMh5j_aGre4JjPD2dQ,F8DHswF8xDasrw7uIe6gQA,gtVjaKxcBjuvbuvUgRNVDw,__bE3_bDCPSpzKAec_a9tCWuMA,PuN33GmiwjiGbtK8fLrm_bw,_9YB69m_aNFTqBCXCdcvj_azQ,_82itIHGRWzucLr1QYx_bscw,nag6H_b96VjiMaEh0hnX_bhg,Jw8JzpJOUTu084nfg8QmiA,tp0_bSnFuvjCQaUwK5cXJNA,Ut9JsUT1jDWCJ0XOWBoljg,zuYGquIWQTOzXhToksaJfw,F6BNHISAiTucEpWQKJMKMw,PN7Sf9o2cTOZ_bfHP8FSspw,GlqbXKnefTmTgBvr2WI7_aA,h4q24RLuAz2cCkYgRA33LQ,_5po7aloEVz2Ws5_aM4658JQ,HcuGEoSh8ziUMprx3cOUUw,__abhl3Th1cD6tAkOjNbk7qw,bR_ayb7e53TOpA9okUA4AYQ,mdhTIqtl8TqzEhtngH6T_bw,ZJSzmB3T7DmrLa5jZ1qOVA,i4RNkwltTjWniv3drmFOgg,Wxo_bUh_bS8TGjoRdFP09sxA,_7TuJuSJncDag7F9nfDUbfQ,zHA_aARd1cjKlJqjNj8xntA,aZFnb_a4m3TqP8M4PCqAT_aw,_4wJc_bUv8Sz_awaRYQafikfw,crOifgM6eDasj50UUSMO8Q,fKSRyufUEjqbrc6Q5O7ryw,apo1IMfB_bTOt1XXfMkgmbQ,AmDbum4UGD6K4rxXW7dcsg,Y8eKzTmXVjiltWVkuVHBiA,UkV3xMP0KzS_bdRyy1rdx_bg,wfLcjvd3qD6XrPGZP0rKLA,GVZC9UAznTmriroAm2yWgg,rIWe5xv6ETyTub5rOKSvRA,SyVYHNtm1DmvK1RUasoEIQ,bluEd7h_afT6gMvrmjMrwWw,VqxmIClzizCnsZekUeVmgA,NumX0sMIBDafRMsBisUoIQ,__b56JeU8ScD2xMP0_bk23JjQ,LsjGOFMF5jCv0uAularNmw,__buKZpkodmziAk5_bSoww_a8g,Eum5pOT_aVDKnn40FH1TlGg,wljBXXGXyTaWJhb6CcyWQQ,t_aoaHaUXOTWBTxfXH2TMTA,KYzfe3XimjuVSFjQZEELng,_4UYzUUaNQjCFzuobOvQyow,jIpWtTtXDzyrG3QwxgwmZw,VxI3MU_aSJzKh5t7FI9mf3A,z7S1Tf5TsTG1q9Y3roKlrQ,YNeejDVfYjqt2eSWF8SaOQ,nsd_aBq2DWTuBNbfIsMcd0A,sR1ynuUU_aTC68duFYtujxg,hDwO4_bTjTTG_aL4_ahSLPtTA,bOE_bJMJNUDC9XKC8HW6TiQ,y_bnZziVh5T6csyiz60_bZ7Q,tSbqVRk97zGutkNmTIw91A,eH13nppCsz_axjn3f4mX0IA,Dsdnp0Tl8zadTLwMJO1nnA,X0_aa32YobjuKqyxcHwFJ3Q,mc_a8T_a_aiVzem_agWG9y0ZrA,__aNGDcaRTLzadJH5YDggP_bg,cUs1Lnmu9zC8e6oGJlSK_bQ,OnqCTfqhLTy1Lxz2DoHTSw,n8c_b14YPyT_a_afjO7wLrjcQ,YfMQ2NwijzGJDK4R_a8wyvw,FqsaKt_ao4Dqdi9QD1wOqFQ,rPcJWj_bUMzuUZVYhmc9fzQ,WJieLjf1uTSfGmgy0X6ikQ,bZtcPqghWDOMYBdQVLoLIQ,G_bYo962yajuICugOLN_a8lw,rxjo9LO7iTaCp8Nh8TVYhg,_49ZFUFGffzGByMt4B_bz5uA,BkK2rq43Ozes59U5VdfFiw,_6mLe_aQR4TjGWvpZdG1JEaA,N5CTXpqMhzKpFtmPjF_atWQ,s3R06oqVzDa8F5yDSGjsZA];
  W_bfCHB0Un0CgJ_aixUydYLg.References = [];

  (function()
  {
    rwUABBbA4D2OPLnwMhJAvw = null;
    sQUABBbA4D2OPLnwMhJAvw = 0;
  }
  )();

  (function()
  {
    xAQABNF_bMzODN61n2IR6Jw = new ctor$Og0ABtF_bMzODN61n2IR6Jw();
  }
  )();

  (function()
  {
    var b;

    b = [
      0,
      31,
      59,
      90,
      120,
      151,
      181,
      212,
      243,
      273,
      304,
      334,
      365
    ];
    cwMABOIWQTOzXhToksaJfw = EQkABuIWQTOzXhToksaJfw(b);
    cwMABOIWQTOzXhToksaJfw[0] = 0;
    b = [
      0,
      31,
      60,
      91,
      121,
      152,
      182,
      213,
      244,
      274,
      305,
      335,
      366
    ];
    cgMABOIWQTOzXhToksaJfw = EQkABuIWQTOzXhToksaJfw(b);
    cgMABOIWQTOzXhToksaJfw[0] = 0;
  }
  )();

  (function()
  {
    ZgMABLDT9zCTidQoMa3Dig = 'Web.Runtime.FormTemplate';
    ZwMABLDT9zCTidQoMa3Dig = 'json_field';
  }
  )();

  (function()
  {
    WwMABF6xZjKlcaFaQuMTTA = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+\u002f=';
  }
  )();

  (function()
  {
  }
  )();

  (function()
  {
    mQEABMMIBDafRMsBisUoIQ = _5QEABsMIBDafRMsBisUoIQ(new ctor$_5wEABsMIBDafRMsBisUoIQ());
  }
  )();

  (function()
  {
    var b;

    LwAABNcctzqgvmwoTNwgqg = 0;
    b = [
      'click',
      'mousedown',
      'mouseup',
      'mousemove',
      'mouseover',
      'mouseout'
    ];
    MAAABNcctzqgvmwoTNwgqg = b;
  }
  )();

  (function()
  {
    zAYABKghWDOMYBdQVLoLIQ = xRMABqghWDOMYBdQVLoLIQ(255, 255, 0);
    zQYABKghWDOMYBdQVLoLIQ = xhMABqghWDOMYBdQVLoLIQ(128);
    zgYABKghWDOMYBdQVLoLIQ = xhMABqghWDOMYBdQVLoLIQ(0);
    zwYABKghWDOMYBdQVLoLIQ = wBMABqghWDOMYBdQVLoLIQ('transparent');
    _0AYABKghWDOMYBdQVLoLIQ = wBMABqghWDOMYBdQVLoLIQ('');
    _0QYABKghWDOMYBdQVLoLIQ = xhMABqghWDOMYBdQVLoLIQ(255);
  }
  )();

