// il2script v1.0
// http://zproxy.wordpress.com
// target assambly: X:\c#\MySource\MyDiagnostics\bin\Debug\ScriptCoreLib.dll
  function _10ceeb18a4cbe13d8b39064718363c16_6000003()
  {
    var _1;

    _1 = new Date();
    return _1;
  };

  function _fff5dfbea4be5f309725e9da7675f2d3_6000005(_, _1, _2, _3)
  {
    var _4;
    var _5;

    _4 = _2._ef96c7385900b23fac93aff2ee41b5b3_4000028;
    _5 = !_1;

    if (!(_5))
    {
      _5 = !_db663af156d2293abc8545ac5d254a3a_6000041(_, 'addEventListener');

      if (!(_5))
      {
        _.addEventListener(_3, _4, 0);
      }

      _5 = !_db663af156d2293abc8545ac5d254a3a_6000041(_, 'attachEvent');

      if (!(_5))
      {
        _.attachEvent(_51819e5057ac1f3fad6b279ad6c476aa_6000095('on', _3), _4);
      }

      return;
    }

    _5 = !_db663af156d2293abc8545ac5d254a3a_6000041(_, 'removeEventListener');

    if (!(_5))
    {
      _.removeEventListener(_3, _4, 0);
    }

    _5 = !_db663af156d2293abc8545ac5d254a3a_6000041(_, 'detachEvent');

    if (!(_5))
    {
      _.detachEvent(_51819e5057ac1f3fad6b279ad6c476aa_6000095('on', _3), _4);
    }

  };

  function _bf83260276f10d32860bf66e519aca0c_600000c(_1)
  {
    var _2;
    var _3;
    var _4;
    var _5;
    var _6;

    _4 = _1;
    _5 = 0;
    while ((_5 < _4.length))
    {
      _2 = _4[_5];
      try
      {
        _3 = new ActiveXObject(_2);
        return _3;
      }
      catch (__exc)
      {
      }
      _5++;
    }
    _3 = null;
    return _3;
  };

  function _84927875577c9239bf3c740da1569016_600000d(_, _1, _2, _3)
  {
    _.position = 'absolute';
    _84927875577c9239bf3c740da1569016_600000f(_, ((_1.clientWidth - _2) / 2), ((_1.clientHeight - _3) / 2), _2, _3);
  };

  function _84927875577c9239bf3c740da1569016_600000e(_, _1, _2)
  {
    _.position = 'absolute';
    _.left = _51819e5057ac1f3fad6b279ad6c476aa_6000095(new Number(_1), 'px');
    _.top = _51819e5057ac1f3fad6b279ad6c476aa_6000095(new Number(_2), 'px');
  };

  function _84927875577c9239bf3c740da1569016_600000f(_, _1, _2, _3, _4)
  {
    _84927875577c9239bf3c740da1569016_600000e(_, _1, _2);
    _84927875577c9239bf3c740da1569016_6000012(_, _3, _4);
  };

  function _84927875577c9239bf3c740da1569016_6000010(_, _1, _2, _3)
  {
    _84927875577c9239bf3c740da1569016_600000e(_, (_1.offsetLeft - _2), (_1.offsetTop - _3));
    _84927875577c9239bf3c740da1569016_6000012(_, (_1.clientWidth + (_2 * 2)), (_1.clientHeight + (_3 * 2)));
  };

  function _84927875577c9239bf3c740da1569016_6000011(_, _1)
  {
    _.left = _1.left;
    _.top = _1.top;
    _.width = _1.width;
    _.height = _1.height;
  };

  function _84927875577c9239bf3c740da1569016_6000012(_, _1, _2)
  {
    _.width = _51819e5057ac1f3fad6b279ad6c476aa_6000095(new Number(_1), 'px');
    _.height = _51819e5057ac1f3fad6b279ad6c476aa_6000095(new Number(_2), 'px');
  };

  function _84927875577c9239bf3c740da1569016_6000013(_, _1)
  {
    _84927875577c9239bf3c740da1569016_6000012(_, _1.width, _1.height);
  };

  function _84927875577c9239bf3c740da1569016_6000014(_, _1)
  {
    _84927875577c9239bf3c740da1569016_6000012(_, _1.clientWidth, _1.clientHeight);
  };

  function _84927875577c9239bf3c740da1569016_6000015(a0, a1) { 
            a0.filter = 'Alpha(Opacity=' + (a1 * 100) + ')';
            a0.opacity = a1;
         };
  function _84927875577c9239bf3c740da1569016_6000016(_, _1)
  {
    _84927875577c9239bf3c740da1569016_6000015(_, _1);
  };

  function _84927875577c9239bf3c740da1569016_6000017(a0, a1) { 
            a0.cssFloat = a1;
            a0.styleFloat = a1;
         };
  function _84927875577c9239bf3c740da1569016_6000018(_, _1)
  {
    _84927875577c9239bf3c740da1569016_6000017(_, _1);
  };

  function _2674032b7fa8283bb911f8f92f0c144f() { };
  _2674032b7fa8283bb911f8f92f0c144f.prototype._2674032b7fa8283bb911f8f92f0c144f_600001a = function ()
  {
    var _ = this;

  };

  _2674032b7fa8283bb911f8f92f0c144f.prototype._2674032b7fa8283bb911f8f92f0c144f_600001b = function (_1)
  {
    var _ = this;
    var _2;

    _2 = (_ == _1);
    return _2;
  };
    _2674032b7fa8283bb911f8f92f0c144f.prototype._01fec5817c021c3e98d5da9c9862aa21_6000003 = _2674032b7fa8283bb911f8f92f0c144f.prototype._2674032b7fa8283bb911f8f92f0c144f_600001b;

  _2674032b7fa8283bb911f8f92f0c144f.prototype._2674032b7fa8283bb911f8f92f0c144f_600001c = function ()
  {
    var _ = this;
    var _1;

    _1 = 0;
    return _1;
  };
    _2674032b7fa8283bb911f8f92f0c144f.prototype._01fec5817c021c3e98d5da9c9862aa21_6000007 = _2674032b7fa8283bb911f8f92f0c144f.prototype._2674032b7fa8283bb911f8f92f0c144f_600001c;

  function _ef96c7385900b23fac93aff2ee41b5b3()
  {
    var __base = new _2674032b7fa8283bb911f8f92f0c144f(); 
    for (var i in __base) if (this[i] == undefined) this[i] = __base[i]; 
    if (this['toString'] == undefined) this['toString'] = __base['toString']; 
  };

  _ef96c7385900b23fac93aff2ee41b5b3.prototype._ef96c7385900b23fac93aff2ee41b5b3_4000026 = null;
  _ef96c7385900b23fac93aff2ee41b5b3.prototype._ef96c7385900b23fac93aff2ee41b5b3_4000027 = null;
  _ef96c7385900b23fac93aff2ee41b5b3.prototype._ef96c7385900b23fac93aff2ee41b5b3_4000028 = null;
  _ef96c7385900b23fac93aff2ee41b5b3.prototype._ef96c7385900b23fac93aff2ee41b5b3_600001d = function (_1, _2)
  {
    var _ = this;

    _._2674032b7fa8283bb911f8f92f0c144f_600001a();
    _._ef96c7385900b23fac93aff2ee41b5b3_4000026 = (!(_1) ? window : _1);
    _._ef96c7385900b23fac93aff2ee41b5b3_4000027 = _2;
    _._ef96c7385900b23fac93aff2ee41b5b3_4000028 = _ef96c7385900b23fac93aff2ee41b5b3_6000022(_._ef96c7385900b23fac93aff2ee41b5b3_4000026, _._ef96c7385900b23fac93aff2ee41b5b3_4000027);
  };

  _ef96c7385900b23fac93aff2ee41b5b3.prototype._ef96c7385900b23fac93aff2ee41b5b3_600001e = function ()
  {
    var _ = this;

    _ef96c7385900b23fac93aff2ee41b5b3_6000021(_._ef96c7385900b23fac93aff2ee41b5b3_4000026, _._ef96c7385900b23fac93aff2ee41b5b3_4000027);
  };

  _ef96c7385900b23fac93aff2ee41b5b3.prototype._ef96c7385900b23fac93aff2ee41b5b3_600001f = function ()
  {
    var _ = this;

    _._ef96c7385900b23fac93aff2ee41b5b3_6000020();
  };

  _ef96c7385900b23fac93aff2ee41b5b3.prototype._ef96c7385900b23fac93aff2ee41b5b3_6000020 = function ()
  {
    var _ = this;

    _._ef96c7385900b23fac93aff2ee41b5b3_600001e();
  };

  function _ef96c7385900b23fac93aff2ee41b5b3_6000021(o, p) { o[p]() };
  function _ef96c7385900b23fac93aff2ee41b5b3_6000022(o, p) { return function(a0) { return o[p](a0); } };
  function _ef96c7385900b23fac93aff2ee41b5b3_6000023(_1, _2)
  {
    var _3;
    var _4;

    _4 = !(_1 == null);

    if (!(_4))
    {
      _3 = _2;
      return _3;
    }

    _4 = !(_2 == null);

    if (!(_4))
    {
      _3 = _1;
      return _3;
    }

    _3 = _1._ef96c7385900b23fac93aff2ee41b5b3_6000024(_2);
    return _3;
  };

  _ef96c7385900b23fac93aff2ee41b5b3.prototype._ef96c7385900b23fac93aff2ee41b5b3_6000024 = function (_1)
  {
    var _ = this;
    var _2;

    _2 = null;
    return _2;
  };

  function _ef96c7385900b23fac93aff2ee41b5b3_6000025(_1, _2)
  {
    var _3;
    var _4;

    _4 = !(_1 == null);

    if (!(_4))
    {
      _3 = null;
      return _3;
    }

    _4 = !(_2 == null);

    if (!(_4))
    {
      _3 = _1;
      return _3;
    }

    _3 = _1._ef96c7385900b23fac93aff2ee41b5b3_6000026(_2);
    return _3;
  };

  _ef96c7385900b23fac93aff2ee41b5b3.prototype._ef96c7385900b23fac93aff2ee41b5b3_6000026 = function (_1)
  {
    var _ = this;
    var _2;
    var _3;

    _3 = _1._2674032b7fa8283bb911f8f92f0c144f_600001b(_);

    if (!(_3))
    {
      _2 = _;
      return _2;
    }

    _2 = null;
    return _2;
  };

  _ef96c7385900b23fac93aff2ee41b5b3.prototype._ef96c7385900b23fac93aff2ee41b5b3_6000027 = function (_1)
  {
    var _ = this;
    var _2;

    _2 = (_981345cae273063d953473b406682026_60000d2(_1._ef96c7385900b23fac93aff2ee41b5b3_4000027, _._ef96c7385900b23fac93aff2ee41b5b3_4000027) && (_1._ef96c7385900b23fac93aff2ee41b5b3_4000026 == _._ef96c7385900b23fac93aff2ee41b5b3_4000026));
    return _2;
  };
    _ef96c7385900b23fac93aff2ee41b5b3.prototype._2674032b7fa8283bb911f8f92f0c144f_600001b = _ef96c7385900b23fac93aff2ee41b5b3.prototype._ef96c7385900b23fac93aff2ee41b5b3_6000027;

  _ef96c7385900b23fac93aff2ee41b5b3.prototype._ef96c7385900b23fac93aff2ee41b5b3_6000028 = function ()
  {
    var _ = this;
    var _1;

    _1 = 0;
    return _1;
  };
    _ef96c7385900b23fac93aff2ee41b5b3.prototype._2674032b7fa8283bb911f8f92f0c144f_600001c = _ef96c7385900b23fac93aff2ee41b5b3.prototype._ef96c7385900b23fac93aff2ee41b5b3_6000028;

  function _8e14c7f3449eef308e23f62cdcb0e09b()
  {
    var __base = new _ef96c7385900b23fac93aff2ee41b5b3(); 
    for (var i in __base) if (this[i] == undefined) this[i] = __base[i]; 
    if (this['toString'] == undefined) this['toString'] = __base['toString']; 
  };

  _8e14c7f3449eef308e23f62cdcb0e09b.prototype._8e14c7f3449eef308e23f62cdcb0e09b_4000029 = null;
  _8e14c7f3449eef308e23f62cdcb0e09b.prototype._8e14c7f3449eef308e23f62cdcb0e09b_6000029 = function (_1, _2)
  {
    var _ = this;

    _._8e14c7f3449eef308e23f62cdcb0e09b_4000029 = new Array();
    _._ef96c7385900b23fac93aff2ee41b5b3_600001d(_1, _2);
    _._8e14c7f3449eef308e23f62cdcb0e09b_4000029.push(_);
  };

  _8e14c7f3449eef308e23f62cdcb0e09b.prototype._8e14c7f3449eef308e23f62cdcb0e09b_600002a = function (_1)
  {
    var _ = this;
    var _2;

    _._8e14c7f3449eef308e23f62cdcb0e09b_4000029.push(_1);
    _2 = _;
    return _2;
  };
    _8e14c7f3449eef308e23f62cdcb0e09b.prototype._ef96c7385900b23fac93aff2ee41b5b3_6000024 = _8e14c7f3449eef308e23f62cdcb0e09b.prototype._8e14c7f3449eef308e23f62cdcb0e09b_600002a;

  _8e14c7f3449eef308e23f62cdcb0e09b.prototype._8e14c7f3449eef308e23f62cdcb0e09b_600002b = function ()
  {
    var _ = this;
    var _1;
    var _2;
    var _3;
    var _4;

    _2 = _1330e3cc5b78ce34a942b0c73eb79bd6_600003d(_._8e14c7f3449eef308e23f62cdcb0e09b_4000029);
    _3 = 0;
    while ((_3 < _2.length))
    {
      _1 = _2[_3];
      _1._ef96c7385900b23fac93aff2ee41b5b3_600001e();
      _3++;
    }
  };
    _8e14c7f3449eef308e23f62cdcb0e09b.prototype._ef96c7385900b23fac93aff2ee41b5b3_6000020 = _8e14c7f3449eef308e23f62cdcb0e09b.prototype._8e14c7f3449eef308e23f62cdcb0e09b_600002b;

  function _30049bd5b145bb3a8904c2ff463e6683() { };
  _30049bd5b145bb3a8904c2ff463e6683.prototype._30049bd5b145bb3a8904c2ff463e6683_400002a = '';
  var _30049bd5b145bb3a8904c2ff463e6683_400002b = 0;
  _30049bd5b145bb3a8904c2ff463e6683.prototype._30049bd5b145bb3a8904c2ff463e6683_600002c = function (_1)
  {
    var _ = this;

    _._30049bd5b145bb3a8904c2ff463e6683_400002a = _1;
    _._30049bd5b145bb3a8904c2ff463e6683_600002d();
    _30049bd5b145bb3a8904c2ff463e6683_6000031(_51819e5057ac1f3fad6b279ad6c476aa_6000096('<', _._30049bd5b145bb3a8904c2ff463e6683_400002a, '>'));
    _30049bd5b145bb3a8904c2ff463e6683_400002b = (_30049bd5b145bb3a8904c2ff463e6683_400002b + 1);
  };

  _30049bd5b145bb3a8904c2ff463e6683.prototype._30049bd5b145bb3a8904c2ff463e6683_600002d = function ()
  {
    var _ = this;
    var _1;
    var _2;

    _1 = _30049bd5b145bb3a8904c2ff463e6683_400002b;
    while ((_1-- > 0))
    {
      _30049bd5b145bb3a8904c2ff463e6683_6000030(' ');
    }
  };

  function _30049bd5b145bb3a8904c2ff463e6683_600002e(w0, e0) { 
            if ('dump' in w0)
                w0.dump(e0);
             };
  function _30049bd5b145bb3a8904c2ff463e6683_600002f(_1)
  {
    _30049bd5b145bb3a8904c2ff463e6683_600002e(window, _1);
  };

  function _30049bd5b145bb3a8904c2ff463e6683_6000030(_1)
  {
    _30049bd5b145bb3a8904c2ff463e6683_600002f(_1);
  };

  function _30049bd5b145bb3a8904c2ff463e6683_6000031(_1)
  {
    _30049bd5b145bb3a8904c2ff463e6683_600002f(_1);
    _30049bd5b145bb3a8904c2ff463e6683_600002f('\x0a');
  };

  _30049bd5b145bb3a8904c2ff463e6683.prototype._30049bd5b145bb3a8904c2ff463e6683_6000032 = function ()
  {
    var _ = this;

    _30049bd5b145bb3a8904c2ff463e6683_400002b = (_30049bd5b145bb3a8904c2ff463e6683_400002b - 1);
    _._30049bd5b145bb3a8904c2ff463e6683_600002d();
    _30049bd5b145bb3a8904c2ff463e6683_6000031(_51819e5057ac1f3fad6b279ad6c476aa_6000096('<\x2f', _._30049bd5b145bb3a8904c2ff463e6683_400002a, '>'));
  };

  _30049bd5b145bb3a8904c2ff463e6683.prototype._987a5d80afd40f3f967fe5cf45312d2c_60000c0 = _30049bd5b145bb3a8904c2ff463e6683.prototype._30049bd5b145bb3a8904c2ff463e6683_6000032;

  function _1330e3cc5b78ce34a942b0c73eb79bd6_6000035(_1, _2)
  {
    var _3;

    _1.push(_2);
    _3 = _1;
    return _3;
  };

  function _1330e3cc5b78ce34a942b0c73eb79bd6_600003b(_, _1)
  {
    var _2;

    _2 = /* [0x0008] unbox.any  +1 -1{[0x0003] call       +1 -2{[0x0001] ldarg.0    +1 -0} {[0x0002] ldarg.1    +1 -0} }  */_db663af156d2293abc8545ac5d254a3a_6000042(_, _1);
    return _2;
  };

  function _1330e3cc5b78ce34a942b0c73eb79bd6_600003c(_, _1, _2)
  {
    void (_db663af156d2293abc8545ac5d254a3a_6000043(_, _1, /* box[T] */ _2));
  };

  function _1330e3cc5b78ce34a942b0c73eb79bd6_600003d(_)
  {
    var _1;

    _1 = _;
    return _1;
  };

  function _1330e3cc5b78ce34a942b0c73eb79bd6_600003e(e, d) { return e.split(d); };
  function _db663af156d2293abc8545ac5d254a3a_6000044(_, _1)
  {
    var _2;

    _2 = /* [0x0008] unbox.any  +1 -1{[0x0003] call       +1 -2{[0x0001] ldarg.0    +1 -0} {[0x0002] ldarg.1    +1 -0} }  */_db663af156d2293abc8545ac5d254a3a_6000042(_, _1);
    return _2;
  };

  function _db663af156d2293abc8545ac5d254a3a_6000045(_1, _2, _3, _4)
  {
    var _5;

    _5 = _db663af156d2293abc8545ac5d254a3a_6000046(_db663af156d2293abc8545ac5d254a3a_6000040(_1), _2, _3, _4);
    return _5;
  };

  function _db663af156d2293abc8545ac5d254a3a_6000046(_, _1, _2, _3)
  {
    var _4;
    var _5;

    _5 = !_db663af156d2293abc8545ac5d254a3a_6000041(_, _1);

    if (!(_5))
    {
      _4 = /* [0x0016] unbox.any  +1 -1{[0x0011] call       +1 -2{[0x000f] ldarg.0    +1 -0} {[0x0010] ldarg.1    +1 -0} }  */_db663af156d2293abc8545ac5d254a3a_6000042(_, _1);
      return _4;
    }

    _5 = !_db663af156d2293abc8545ac5d254a3a_6000041(_, _2);

    if (!(_5))
    {
      _4 = /* [0x0033] unbox.any  +1 -1{[0x002e] call       +1 -2{[0x002c] ldarg.0    +1 -0} {[0x002d] ldarg.2    +1 -0} }  */_db663af156d2293abc8545ac5d254a3a_6000042(_, _2);
      return _4;
    }

    _4 = _3;
    return _4;
  };

  function _db663af156d2293abc8545ac5d254a3a_6000040(_1)
  {
    var _2;

    _2 = _1;
    return _2;
  };

  function _db663af156d2293abc8545ac5d254a3a_6000041(o, m) { return m in o };
  function _db663af156d2293abc8545ac5d254a3a_6000042(o, m) { return o[m] };
  function _db663af156d2293abc8545ac5d254a3a_6000043(o, m, v) { o[m] = v };
  function _db663af156d2293abc8545ac5d254a3a_6000047(_, _1, _2)
  {
    void (_db663af156d2293abc8545ac5d254a3a_6000043(_, _1, _2));
  };

  function _db663af156d2293abc8545ac5d254a3a_6000048(_, _1)
  {
    var _2;

    _2 = _db663af156d2293abc8545ac5d254a3a_6000042(_, _1);
    return _2;
  };

  function _28a9329942a5be30a0ba41ddfe346990()
  {
    var __base = new _8e14c7f3449eef308e23f62cdcb0e09b(); 
    for (var i in __base) if (this[i] == undefined) this[i] = __base[i]; 
    if (this['toString'] == undefined) this['toString'] = __base['toString']; 
  };

  _28a9329942a5be30a0ba41ddfe346990.prototype._28a9329942a5be30a0ba41ddfe346990_600004a = function (_1, _2)
  {
    var _ = this;

    _._8e14c7f3449eef308e23f62cdcb0e09b_6000029(_1, _2);
  };

  function _90dbc227e9d87136aad24505eb7f5b53_600005f(_, _1, _2, _3)
  {
    var _4;

    _4 = _.max(_.min(_1, _2), _3);
    return _4;
  };

  function _8c3a9b9a27aa683db91d2642d8d00a78_6000063(_, _1)
  {
    _.appendChild(_8a2ade548a7da73b9f7737146988ad27_6000066(_1));
  };

  function _8a2ade548a7da73b9f7737146988ad27_6000066(_1)
  {
    var _2;

    _2 = document.createTextNode(_1);
    return _2;
  };

  function _9a7f90838c037e3fa878dfdab66ea5b1_600006b(_, _1)
  {
    var _2;

    _2 = _.open('text\x2fhtml', ((_1) ? 'replace' : ''));
    return _2;
  };

  function _9a7f90838c037e3fa878dfdab66ea5b1_6000070(_, _1)
  {
    _fff5dfbea4be5f309725e9da7675f2d3_6000005(_, 1, _1, 'click');
  };

  function _9a7f90838c037e3fa878dfdab66ea5b1_6000071(_, _1)
  {
    _fff5dfbea4be5f309725e9da7675f2d3_6000005(_, 0, _1, 'click');
  };

  function _9a7f90838c037e3fa878dfdab66ea5b1_6000072(_, _1)
  {
    _fff5dfbea4be5f309725e9da7675f2d3_6000005(_, 1, _1, 'mousemove');
  };

  function _9a7f90838c037e3fa878dfdab66ea5b1_6000073(_, _1)
  {
    _fff5dfbea4be5f309725e9da7675f2d3_6000005(_, 0, _1, 'mousemove');
  };

  function _9a7f90838c037e3fa878dfdab66ea5b1_6000074(_, _1)
  {
    _fff5dfbea4be5f309725e9da7675f2d3_6000005(_, 1, _1, 'mousedown');
  };

  function _9a7f90838c037e3fa878dfdab66ea5b1_6000075(_, _1)
  {
    _fff5dfbea4be5f309725e9da7675f2d3_6000005(_, 0, _1, 'mousedown');
  };

  function _9a7f90838c037e3fa878dfdab66ea5b1_6000076(_, _1)
  {
    _fff5dfbea4be5f309725e9da7675f2d3_6000005(_, 1, _1, 'mouseup');
  };

  function _9a7f90838c037e3fa878dfdab66ea5b1_6000077(_, _1)
  {
    _fff5dfbea4be5f309725e9da7675f2d3_6000005(_, 0, _1, 'mouseup');
  };

  function _cb8322a5d424f63682ff24fe299218a1_600007f(_, _1, _2, _3, _4, _5)
  {
    var _6;
    var _7;

    _6 = new Array();
    _6.push(_51819e5057ac1f3fad6b279ad6c476aa_6000095('width\x3d', new Number(_3)));
    _6.push(_51819e5057ac1f3fad6b279ad6c476aa_6000095('height\x3d', new Number(_4)));
    _6.push(_51819e5057ac1f3fad6b279ad6c476aa_6000095('scrollbars\x3d', ((_5) ? 'yes' : 'no')));
    _7 = _.open(_1, _2, _6.join(','));
    return _7;
  };

  function _cb8322a5d424f63682ff24fe299218a1_6000082(_, _1, _2)
  {
    var _3;

    _3 = _.setTimeout(_1._ef96c7385900b23fac93aff2ee41b5b3_4000028, _2);
    return _3;
  };

  function _cb8322a5d424f63682ff24fe299218a1_6000085(_, _1, _2)
  {
    var _3;

    _3 = _.setInterval(_1._ef96c7385900b23fac93aff2ee41b5b3_4000028, _2);
    return _3;
  };

  function _cb8322a5d424f63682ff24fe299218a1_6000088(_, _1)
  {
    _fff5dfbea4be5f309725e9da7675f2d3_6000005(_, 1, _1, 'load');
  };

  function _cb8322a5d424f63682ff24fe299218a1_6000089(_, _1)
  {
    _fff5dfbea4be5f309725e9da7675f2d3_6000005(_, 0, _1, 'load');
  };

  function _cb8322a5d424f63682ff24fe299218a1_600008a(_, _1)
  {
    _fff5dfbea4be5f309725e9da7675f2d3_6000005(_, 1, _1, 'unload');
  };

  function _cb8322a5d424f63682ff24fe299218a1_600008b(_, _1)
  {
    _fff5dfbea4be5f309725e9da7675f2d3_6000005(_, 0, _1, 'unload');
  };

  function _cb8322a5d424f63682ff24fe299218a1_600008c(_, _1)
  {
    _fff5dfbea4be5f309725e9da7675f2d3_6000005(_, 1, _1, 'resize');
  };

  function _cb8322a5d424f63682ff24fe299218a1_600008d(_, _1)
  {
    _fff5dfbea4be5f309725e9da7675f2d3_6000005(_, 0, _1, 'resize');
  };

  function _84e4e1e7fda9783793c35f1edcdc68fd()
  {
    var __base = new _8e14c7f3449eef308e23f62cdcb0e09b(); 
    for (var i in __base) if (this[i] == undefined) this[i] = __base[i]; 
    if (this['toString'] == undefined) this['toString'] = __base['toString']; 
  };

  _84e4e1e7fda9783793c35f1edcdc68fd.prototype._84e4e1e7fda9783793c35f1edcdc68fd_600008f = function (_1, _2)
  {
    var _ = this;

    _._8e14c7f3449eef308e23f62cdcb0e09b_6000029(_1, _2);
  };

  function _51819e5057ac1f3fad6b279ad6c476aa()
  {
    var __base = new _2674032b7fa8283bb911f8f92f0c144f(); 
    for (var i in __base) if (this[i] == undefined) this[i] = __base[i]; 
    if (this['toString'] == undefined) this['toString'] = __base['toString']; 
  };

  _51819e5057ac1f3fad6b279ad6c476aa.prototype._51819e5057ac1f3fad6b279ad6c476aa_600009c = function ()
  {
    var _ = this;

    _._2674032b7fa8283bb911f8f92f0c144f_600001a();
  };

  function _51819e5057ac1f3fad6b279ad6c476aa_6000090(e, i) { return e.charAt(i); };
  function _51819e5057ac1f3fad6b279ad6c476aa_6000091(e) { return e.length; };
  function _51819e5057ac1f3fad6b279ad6c476aa_6000092(_)
  {
    var _1;

    _1 = _51819e5057ac1f3fad6b279ad6c476aa_6000091(_);
    return _1;
  };

  function _51819e5057ac1f3fad6b279ad6c476aa_6000093(_, _1)
  {
    var _2;

    _2 = _51819e5057ac1f3fad6b279ad6c476aa_6000090(_, _1);
    return _2;
  };

  function _51819e5057ac1f3fad6b279ad6c476aa_6000094(a0) { return a0.join('') };
  function _51819e5057ac1f3fad6b279ad6c476aa_6000095(a0, a1) { return a0+a1 };
  function _51819e5057ac1f3fad6b279ad6c476aa_6000096(a0, a1, a2) { return a0+a1+a2 };
  function _51819e5057ac1f3fad6b279ad6c476aa_6000097(a0, a1, a2, a3) { return a0+a1+a2+a3 };
  function _51819e5057ac1f3fad6b279ad6c476aa_6000098(_, a0, a1, a2) { return a0.split(a1).join(a2) }; 
  function _51819e5057ac1f3fad6b279ad6c476aa_6000099(_, _1, _2)
  {
    var _3;

    _3 = _51819e5057ac1f3fad6b279ad6c476aa_6000098(_, _, _1, _2);
    return _3;
  };

  function _51819e5057ac1f3fad6b279ad6c476aa_600009a(a, b) { return a == b };
  function _51819e5057ac1f3fad6b279ad6c476aa_600009b(a, b) { return a != b };
  function _e0ffb070dcd58f31836cbd71252c4644()
  {
    var __base = new _2674032b7fa8283bb911f8f92f0c144f(); 
    for (var i in __base) if (this[i] == undefined) this[i] = __base[i]; 
    if (this['toString'] == undefined) this['toString'] = __base['toString']; 
  };

  _e0ffb070dcd58f31836cbd71252c4644.prototype._e0ffb070dcd58f31836cbd71252c4644_600009e = function ()
  {
    var _ = this;

    _._2674032b7fa8283bb911f8f92f0c144f_600001a();
  };

  function _e0ffb070dcd58f31836cbd71252c4644_600009d(e) { return parseInt(e); };
  function _e55fccf355d36034b7a5eae08f5c979c()
  {
    var __base = new _2674032b7fa8283bb911f8f92f0c144f(); 
    for (var i in __base) if (this[i] == undefined) this[i] = __base[i]; 
    if (this['toString'] == undefined) this['toString'] = __base['toString']; 
  };

  _e55fccf355d36034b7a5eae08f5c979c.prototype._e55fccf355d36034b7a5eae08f5c979c_4000046 = null;
  _e55fccf355d36034b7a5eae08f5c979c.prototype._e55fccf355d36034b7a5eae08f5c979c_4000047 = null;
  _e55fccf355d36034b7a5eae08f5c979c.prototype._e55fccf355d36034b7a5eae08f5c979c_4000048 = null;
  _e55fccf355d36034b7a5eae08f5c979c.prototype._e55fccf355d36034b7a5eae08f5c979c_60000a8 = function ()
  {
    var _ = this;

    _._2674032b7fa8283bb911f8f92f0c144f_600001a();
  };

  function _e55fccf355d36034b7a5eae08f5c979c_600009f()
  {
    var _1;

    _1 = _e55fccf355d36034b7a5eae08f5c979c_60000a5(255, 0, 0);
    return _1;
  };

  function _e55fccf355d36034b7a5eae08f5c979c_60000a0()
  {
    var _1;

    _1 = _e55fccf355d36034b7a5eae08f5c979c_60000a5(0, 255, 0);
    return _1;
  };

  function _e55fccf355d36034b7a5eae08f5c979c_60000a1()
  {
    var _1;

    _1 = _e55fccf355d36034b7a5eae08f5c979c_60000a5(0, 0, 255);
    return _1;
  };

  function _e55fccf355d36034b7a5eae08f5c979c_60000a2()
  {
    var _1;

    _1 = _e55fccf355d36034b7a5eae08f5c979c_60000a4(0);
    return _1;
  };

  function _e55fccf355d36034b7a5eae08f5c979c_60000a3()
  {
    var _1;

    _1 = _e55fccf355d36034b7a5eae08f5c979c_60000a4(255);
    return _1;
  };

  function _e55fccf355d36034b7a5eae08f5c979c_60000a4(_1)
  {
    var _2;

    _2 = _e55fccf355d36034b7a5eae08f5c979c_60000a5(_1, _1, _1);
    return _2;
  };

  function _e55fccf355d36034b7a5eae08f5c979c_60000a5(_1, _2, _3)
  {
    var _4;
    var _5;

    _4 = ( (function () { var __value = new _e55fccf355d36034b7a5eae08f5c979c(); __value._e55fccf355d36034b7a5eae08f5c979c_60000a8(); return __value; })()  );
    _4._e55fccf355d36034b7a5eae08f5c979c_4000046 = _1;
    _4._e55fccf355d36034b7a5eae08f5c979c_4000047 = _2;
    _4._e55fccf355d36034b7a5eae08f5c979c_4000048 = _3;
    _5 = _4;
    return _5;
  };

  function _e55fccf355d36034b7a5eae08f5c979c_60000a6(_1)
  {
    var _2;

    _2 = (_1+'');
    return _2;
  };

  _e55fccf355d36034b7a5eae08f5c979c.prototype.toString /* ScriptCoreLib.Runtime.Color.ToString */ = function ()
  {
    var _ = this;
    var _1;
    var _2;

    _2 = [];
    _2[0] = 'RGB(';
    _2[1] = /* box[System.Byte] */ _._e55fccf355d36034b7a5eae08f5c979c_4000046;
    _2[2] = ', ';
    _2[3] = /* box[System.Byte] */ _._e55fccf355d36034b7a5eae08f5c979c_4000047;
    _2[4] = ', ';
    _2[5] = /* box[System.Byte] */ _._e55fccf355d36034b7a5eae08f5c979c_4000048;
    _2[6] = ')';
    _1 = _51819e5057ac1f3fad6b279ad6c476aa_6000094(_2);
    return _1;
  };
    _e55fccf355d36034b7a5eae08f5c979c.prototype.toString /* System.Object.ToString */ = _e55fccf355d36034b7a5eae08f5c979c.prototype.toString /* ScriptCoreLib.Runtime.Color.ToString */;

  function _8123263f5323043bb94d445d3ec42955() { };
  _8123263f5323043bb94d445d3ec42955.prototype._8123263f5323043bb94d445d3ec42955_60000ac = function ()
  {
    var _ = this;

  };

  function _8123263f5323043bb94d445d3ec42955_60000a9(_1, _2)
  {
    var _3;
    var _4;
    var _5;
    var _6;
    var _7;
    var _8;
    var _9;

    _3 = '';
    _4 = '0123456789ABCDEF';
    _5 = _1;
    _7 = 0;
    while ((_5 > 0.9))
    {
      _7++;
      _6 = _5;
      _3 = _51819e5057ac1f3fad6b279ad6c476aa_6000095(/* box[System.Char] */ _51819e5057ac1f3fad6b279ad6c476aa_6000093(_4, (_6 % _2)), _3);
      _5 = Math.floor((_6 / _2));
    }
    _8 = _3;
    return _8;
  };

  function _8123263f5323043bb94d445d3ec42955_60000aa(_1)
  {
    var _2;

    _2 = _8123263f5323043bb94d445d3ec42955_60000a9(_1, 16);
    return _2;
  };

  function _8123263f5323043bb94d445d3ec42955_60000ab(_1)
  {
    var _2;

    _2 = _8123263f5323043bb94d445d3ec42955_60000a9(_1, 16);
    return _2;
  };

  function _51e62ec53a4a4e3cac71e4ca2e6ff69d_60000b6(_)
  {
    var _1;

    _1 = (_.readyState == 4);
    return _1;
  };

  function _51e62ec53a4a4e3cac71e4ca2e6ff69d_60000b7()
  {
    var _1;
    var _2;
    var _3;
    var _4;

    _3 = [];
    _3[0] = 'Msxml2.XMLHTTP.3.0';
    _3[1] = 'Microsoft.XMLHTTP';
    _1 = _bf83260276f10d32860bf66e519aca0c_600000c(_3);
    _4 = !(_1 == null);

    if (!(_4))
    {
      try
      {
        _1 = new XMLHttpRequest();
      }
      catch (__exc)
      {
      }
    }

    _2 = _1;
    return _2;
  };

  function _892db40a7c070032bac57b1605e898e5_60000b9(_)
  {
    var _1;

    _1 = _db663af156d2293abc8545ac5d254a3a_6000045(_, 'layerX', 'offsetX', 0);
    return _1;
  };

  function _892db40a7c070032bac57b1605e898e5_60000ba(_)
  {
    var _1;

    _1 = _db663af156d2293abc8545ac5d254a3a_6000045(_, 'layerY', 'offsetY', 0);
    return _1;
  };

  function _892db40a7c070032bac57b1605e898e5_60000bb(_)
  {
    var _1;
    var _2;
    var _3;

    _1 = 0;
    _3 = !_db663af156d2293abc8545ac5d254a3a_6000041(_, 'pageX');

    if (!(_3))
    {
      _1 = _.pageX;
    }
    else
    {
      _3 = !_db663af156d2293abc8545ac5d254a3a_6000041(_, 'clientX');

      if (!(_3))
      {
        _1 = (_.clientX + document.body.scrollLeft);
      }

    }

    _2 = _1;
    return _2;
  };

  function _892db40a7c070032bac57b1605e898e5_60000bc(_)
  {
    var _1;
    var _2;

    _2 = !_db663af156d2293abc8545ac5d254a3a_6000041(_, 'pageY');

    if (!(_2))
    {
      _1 = _.pageY;
      return _1;
    }

    _2 = !_db663af156d2293abc8545ac5d254a3a_6000041(_, 'clientY');

    if (!(_2))
    {
      _1 = (_.clientY + document.body.scrollTop);
      return _1;
    }

    _1 = 0;
    return _1;
  };

  function _892db40a7c070032bac57b1605e898e5_60000bd(_)
  {
    _892db40a7c070032bac57b1605e898e5_60000be(_);
  };

  function _892db40a7c070032bac57b1605e898e5_60000be(a0) { 
            if ('cancelBubble' in a0) 
                a0.cancelBubble = true;

            if ('stopPropagation' in a0) 
                a0.stopPropagation(); 
             };
  function _892db40a7c070032bac57b1605e898e5_60000bf(_)
  {
    _892db40a7c070032bac57b1605e898e5_60000c0(_);
  };

  function _892db40a7c070032bac57b1605e898e5_60000c0(a0) { 
            if ('returnValue' in a0) 
                a0.returnValue = false;

            if ('preventDefault' in a0) 
                a0.preventDefault(); 
             };
  function _892db40a7c070032bac57b1605e898e5_60000c1(a0) { 
            if ('target' in a0)
                return a0.target;

            if ('srcElement' in a0)
                return a0.srcElement;
             };
  function _892db40a7c070032bac57b1605e898e5_60000c2(_)
  {
    var _1;

    _1 = _892db40a7c070032bac57b1605e898e5_60000c1(_);
    return _1;
  };

  function _0f5fb9bb58bcbf36ba78442345b5c906() { };
  var _0f5fb9bb58bcbf36ba78442345b5c906_4000052 = null;
  var _0f5fb9bb58bcbf36ba78442345b5c906_4000053 = null;
  var _0f5fb9bb58bcbf36ba78442345b5c906_4000054 = null;
  var _0f5fb9bb58bcbf36ba78442345b5c906_4000055 = null;
  var _0f5fb9bb58bcbf36ba78442345b5c906_4000056 = null;
  _0f5fb9bb58bcbf36ba78442345b5c906.prototype._0f5fb9bb58bcbf36ba78442345b5c906_60000c8 = function ()
  {
    var _ = this;

  };

  function _0f5fb9bb58bcbf36ba78442345b5c906_60000c4() { alert('ScriptCoreLib 2005'); };
  function _0f5fb9bb58bcbf36ba78442345b5c906_60000c5() { debugger; };
  function _0f5fb9bb58bcbf36ba78442345b5c906_60000c7(e, o) { return e.charCodeAt(o); };
  function _0f5fb9bb58bcbf36ba78442345b5c906_60000c9(_1)
  {
    _892db40a7c070032bac57b1605e898e5_60000bf(_1);
    _892db40a7c070032bac57b1605e898e5_60000bd(_1);
  };

  function _981345cae273063d953473b406682026()
  {
    var __base = new _2674032b7fa8283bb911f8f92f0c144f(); 
    for (var i in __base) if (this[i] == undefined) this[i] = __base[i]; 
    if (this['toString'] == undefined) this['toString'] = __base['toString']; 
  };

  _981345cae273063d953473b406682026.prototype._981345cae273063d953473b406682026_60000d6 = function ()
  {
    var _ = this;

    _._2674032b7fa8283bb911f8f92f0c144f_600001a();
  };

  function _981345cae273063d953473b406682026_60000d2(a, b) { return a==b };
  function _981345cae273063d953473b406682026_60000d3(a, b) { return a!=b };
  _981345cae273063d953473b406682026.prototype._981345cae273063d953473b406682026_60000d4 = function (_1)
  {
    var _ = this;
    var _2;

    _2 = _981345cae273063d953473b406682026_60000d2(_, _1);
    return _2;
  };
    _981345cae273063d953473b406682026.prototype._2674032b7fa8283bb911f8f92f0c144f_600001b = _981345cae273063d953473b406682026.prototype._981345cae273063d953473b406682026_60000d4;

  _981345cae273063d953473b406682026.prototype._981345cae273063d953473b406682026_60000d5 = function ()
  {
    var _ = this;
    var _1;

    _1 = 0;
    return _1;
  };
    _981345cae273063d953473b406682026.prototype._2674032b7fa8283bb911f8f92f0c144f_600001c = _981345cae273063d953473b406682026.prototype._981345cae273063d953473b406682026_60000d5;

  function _cd0399f743f47f3d81c79db4953ab92d()
  {
    var __base = new _2674032b7fa8283bb911f8f92f0c144f(); 
    for (var i in __base) if (this[i] == undefined) this[i] = __base[i]; 
    if (this['toString'] == undefined) this['toString'] = __base['toString']; 
  };

  _cd0399f743f47f3d81c79db4953ab92d.prototype._cd0399f743f47f3d81c79db4953ab92d_4000057 = null;
  _cd0399f743f47f3d81c79db4953ab92d.prototype._cd0399f743f47f3d81c79db4953ab92d_4000058 = 0;
  _cd0399f743f47f3d81c79db4953ab92d.prototype._cd0399f743f47f3d81c79db4953ab92d_4000059 = null;
  _cd0399f743f47f3d81c79db4953ab92d.prototype._cd0399f743f47f3d81c79db4953ab92d_400005a = null;
  _cd0399f743f47f3d81c79db4953ab92d.prototype._cd0399f743f47f3d81c79db4953ab92d_400005b = 0;
  _cd0399f743f47f3d81c79db4953ab92d.prototype._cd0399f743f47f3d81c79db4953ab92d_400005c = 0;
  _cd0399f743f47f3d81c79db4953ab92d.prototype._cd0399f743f47f3d81c79db4953ab92d_400005d = 0;
  _cd0399f743f47f3d81c79db4953ab92d.prototype._cd0399f743f47f3d81c79db4953ab92d_60000d9 = function ()
  {
    var _ = this;

    _._cd0399f743f47f3d81c79db4953ab92d_400005c = 1;
    _._cd0399f743f47f3d81c79db4953ab92d_400005d = 0;
    _._2674032b7fa8283bb911f8f92f0c144f_600001a();
  };

  _cd0399f743f47f3d81c79db4953ab92d.prototype._cd0399f743f47f3d81c79db4953ab92d_60000da = function (_1)
  {
    var _ = this;

    _._cd0399f743f47f3d81c79db4953ab92d_400005c = 1;
    _._cd0399f743f47f3d81c79db4953ab92d_400005d = 0;
    _._2674032b7fa8283bb911f8f92f0c144f_600001a();
    _._cd0399f743f47f3d81c79db4953ab92d_4000057 = _ef96c7385900b23fac93aff2ee41b5b3_6000023(_._cd0399f743f47f3d81c79db4953ab92d_4000057, _1);
  };

  _cd0399f743f47f3d81c79db4953ab92d.prototype._cd0399f743f47f3d81c79db4953ab92d_60000d7 = function (_1)
  {
    var _ = this;

    _._cd0399f743f47f3d81c79db4953ab92d_4000057 = _ef96c7385900b23fac93aff2ee41b5b3_6000023(_._cd0399f743f47f3d81c79db4953ab92d_4000057, _1);
  };

  _cd0399f743f47f3d81c79db4953ab92d.prototype._cd0399f743f47f3d81c79db4953ab92d_60000d8 = function (_1)
  {
    var _ = this;

    _._cd0399f743f47f3d81c79db4953ab92d_4000057 = _ef96c7385900b23fac93aff2ee41b5b3_6000025(_._cd0399f743f47f3d81c79db4953ab92d_4000057, _1);
  };

  _cd0399f743f47f3d81c79db4953ab92d.prototype._cd0399f743f47f3d81c79db4953ab92d_60000db = function ()
  {
    var _ = this;
    var _1;

    _1 = (_._cd0399f743f47f3d81c79db4953ab92d_4000057 == null);

    if (!(_1))
    {
      _._cd0399f743f47f3d81c79db4953ab92d_4000057._ef96c7385900b23fac93aff2ee41b5b3_600001f();
    }

    _._cd0399f743f47f3d81c79db4953ab92d_400005b = (_._cd0399f743f47f3d81c79db4953ab92d_400005b + _._cd0399f743f47f3d81c79db4953ab92d_400005c);
    _1 = ((_._cd0399f743f47f3d81c79db4953ab92d_400005d <= 0) ? 1 : !(_._cd0399f743f47f3d81c79db4953ab92d_400005b > _._cd0399f743f47f3d81c79db4953ab92d_400005d));

    if (!(_1))
    {
      _._cd0399f743f47f3d81c79db4953ab92d_60000e0();
    }

  };

  function _cd0399f743f47f3d81c79db4953ab92d_60000dc(_1, _2)
  {
    var _3;
    var _4;

    _3 = ( (function () { var __value = new _cd0399f743f47f3d81c79db4953ab92d(); __value._cd0399f743f47f3d81c79db4953ab92d_60000d9(); return __value; })()  );
    _3._cd0399f743f47f3d81c79db4953ab92d_4000057 = _ef96c7385900b23fac93aff2ee41b5b3_6000023(_3._cd0399f743f47f3d81c79db4953ab92d_4000057, _1);
    _3._cd0399f743f47f3d81c79db4953ab92d_60000de(_2);
    _4 = _3;
    return _4;
  };

  _cd0399f743f47f3d81c79db4953ab92d.prototype._cd0399f743f47f3d81c79db4953ab92d_60000dd = function (_1, _2)
  {
    var _ = this;

    _._cd0399f743f47f3d81c79db4953ab92d_400005d = _2;
    _._cd0399f743f47f3d81c79db4953ab92d_60000de(_1);
  };

  _cd0399f743f47f3d81c79db4953ab92d.prototype._cd0399f743f47f3d81c79db4953ab92d_60000de = function (_1)
  {
    var _ = this;

    _._cd0399f743f47f3d81c79db4953ab92d_400005a = 1;
    _._cd0399f743f47f3d81c79db4953ab92d_4000058 = _cb8322a5d424f63682ff24fe299218a1_6000085(window, ( (function () { var __value = new _84e4e1e7fda9783793c35f1edcdc68fd(); __value._84e4e1e7fda9783793c35f1edcdc68fd_600008f(_, '_cd0399f743f47f3d81c79db4953ab92d_60000db'); return __value; })()  ), _1);
  };

  _cd0399f743f47f3d81c79db4953ab92d.prototype._cd0399f743f47f3d81c79db4953ab92d_60000df = function (_1)
  {
    var _ = this;

    _._cd0399f743f47f3d81c79db4953ab92d_4000059 = 1;
    _._cd0399f743f47f3d81c79db4953ab92d_4000058 = _cb8322a5d424f63682ff24fe299218a1_6000082(window, ( (function () { var __value = new _84e4e1e7fda9783793c35f1edcdc68fd(); __value._84e4e1e7fda9783793c35f1edcdc68fd_600008f(_, '_cd0399f743f47f3d81c79db4953ab92d_60000db'); return __value; })()  ), _1);
  };

  _cd0399f743f47f3d81c79db4953ab92d.prototype._cd0399f743f47f3d81c79db4953ab92d_60000e0 = function ()
  {
    var _ = this;
    var _1;

    _1 = !_._cd0399f743f47f3d81c79db4953ab92d_4000059;

    if (!(_1))
    {
      window.clearTimeout(_._cd0399f743f47f3d81c79db4953ab92d_4000058);
    }

    _1 = !_._cd0399f743f47f3d81c79db4953ab92d_400005a;

    if (!(_1))
    {
      window.clearInterval(_._cd0399f743f47f3d81c79db4953ab92d_4000058);
    }

    _._cd0399f743f47f3d81c79db4953ab92d_400005a = 0;
    _._cd0399f743f47f3d81c79db4953ab92d_4000059 = 0;
    _._cd0399f743f47f3d81c79db4953ab92d_4000058 = 0;
    _._cd0399f743f47f3d81c79db4953ab92d_400005b = 0;
  };

  function _8e8b204c4cc1a735a3a9cf6dd948d6f7_60000e1(_)
  {
    var _1;
    var _2;

    _1 = _.parentNode;
    _2 = (_1 == null);

    if (!(_2))
    {
      _1.removeChild(_);
    }

  };

  function _8e8b204c4cc1a735a3a9cf6dd948d6f7_60000e2(_1)
  {
    var _2;
    var _3;

    _2 = _8e8b204c4cc1a735a3a9cf6dd948d6f7_60000e3();
    _2.appendChild(_8a2ade548a7da73b9f7737146988ad27_6000066(_1));
    _3 = _2;
    return _3;
  };

  function _8e8b204c4cc1a735a3a9cf6dd948d6f7_60000e3()
  {
    var _1;

    _1 = _8e8b204c4cc1a735a3a9cf6dd948d6f7_60000e4('div');
    return _1;
  };

  function _8e8b204c4cc1a735a3a9cf6dd948d6f7_60000e4(_1)
  {
    var _2;

    _2 = document.createElement(_1);
    return _2;
  };

  function _8e8b204c4cc1a735a3a9cf6dd948d6f7_60000e5(_1)
  {
    var _2;

    _2 = _1.style;
    return _2;
  };

  function _8e8b204c4cc1a735a3a9cf6dd948d6f7_60000e6(_, _1)
  {
    var _2;
    var _3;

    _2 = document.createElement(_1);
    _.appendChild(_2);
    _3 = _2;
    return _3;
  };

  function _8e8b204c4cc1a735a3a9cf6dd948d6f7_60000e7(_, _1)
  {
    _fff5dfbea4be5f309725e9da7675f2d3_6000005(_, 1, _1, 'click');
  };

  function _8e8b204c4cc1a735a3a9cf6dd948d6f7_60000e8(_, _1)
  {
    _fff5dfbea4be5f309725e9da7675f2d3_6000005(_, 0, _1, 'click');
  };

  function _8e8b204c4cc1a735a3a9cf6dd948d6f7_60000e9(_, _1)
  {
    _fff5dfbea4be5f309725e9da7675f2d3_6000005(_, 1, _1, 'dblclick');
  };

  function _8e8b204c4cc1a735a3a9cf6dd948d6f7_60000ea(_, _1)
  {
    _fff5dfbea4be5f309725e9da7675f2d3_6000005(_, 0, _1, 'dblclick');
  };

  function _8e8b204c4cc1a735a3a9cf6dd948d6f7_60000eb(_, _1)
  {
    _fff5dfbea4be5f309725e9da7675f2d3_6000005(_, 1, _1, 'mouseover');
  };

  function _8e8b204c4cc1a735a3a9cf6dd948d6f7_60000ec(_, _1)
  {
    _fff5dfbea4be5f309725e9da7675f2d3_6000005(_, 0, _1, 'mouseover');
  };

  function _8e8b204c4cc1a735a3a9cf6dd948d6f7_60000ed(_, _1)
  {
    _fff5dfbea4be5f309725e9da7675f2d3_6000005(_, 1, _1, 'mouseout');
  };

  function _8e8b204c4cc1a735a3a9cf6dd948d6f7_60000ee(_, _1)
  {
    _fff5dfbea4be5f309725e9da7675f2d3_6000005(_, 0, _1, 'mouseout');
  };

  function _8e8b204c4cc1a735a3a9cf6dd948d6f7_60000ef(_, _1)
  {
    _fff5dfbea4be5f309725e9da7675f2d3_6000005(_, 1, _1, 'mousedown');
  };

  function _8e8b204c4cc1a735a3a9cf6dd948d6f7_60000f0(_, _1)
  {
    _fff5dfbea4be5f309725e9da7675f2d3_6000005(_, 0, _1, 'mousedown');
  };

  function _8e8b204c4cc1a735a3a9cf6dd948d6f7_60000f1(_, _1)
  {
    _fff5dfbea4be5f309725e9da7675f2d3_6000005(_, 1, _1, 'mouseup');
  };

  function _8e8b204c4cc1a735a3a9cf6dd948d6f7_60000f2(_, _1)
  {
    _fff5dfbea4be5f309725e9da7675f2d3_6000005(_, 0, _1, 'mouseup');
  };

  function _8e8b204c4cc1a735a3a9cf6dd948d6f7_60000f3(_, _1)
  {
    _fff5dfbea4be5f309725e9da7675f2d3_6000005(_, 1, _1, 'mousemove');
  };

  function _8e8b204c4cc1a735a3a9cf6dd948d6f7_60000f4(_, _1)
  {
    _fff5dfbea4be5f309725e9da7675f2d3_6000005(_, 0, _1, 'mousemove');
  };

  function _8e8b204c4cc1a735a3a9cf6dd948d6f7_60000f5(_, _1)
  {
    _fff5dfbea4be5f309725e9da7675f2d3_6000005(_, 1, _1, 'contextmenu');
  };

  function _8e8b204c4cc1a735a3a9cf6dd948d6f7_60000f6(_, _1)
  {
    _fff5dfbea4be5f309725e9da7675f2d3_6000005(_, 0, _1, 'contextmenu');
  };

  function _8e8b204c4cc1a735a3a9cf6dd948d6f7_60000f7(_, _1)
  {
    _fff5dfbea4be5f309725e9da7675f2d3_6000005(_, 1, _1, 'selectionstart');
  };

  function _8e8b204c4cc1a735a3a9cf6dd948d6f7_60000f8(_, _1)
  {
    _fff5dfbea4be5f309725e9da7675f2d3_6000005(_, 0, _1, 'selectionstart');
  };

  function _8e8b204c4cc1a735a3a9cf6dd948d6f7_60000f9(_, _1)
  {
    _fff5dfbea4be5f309725e9da7675f2d3_6000005(_, 1, _1, 'change');
  };

  function _8e8b204c4cc1a735a3a9cf6dd948d6f7_60000fa(_, _1)
  {
    _fff5dfbea4be5f309725e9da7675f2d3_6000005(_, 0, _1, 'change');
  };

  function _505744091e356e37808e1afa5025f018_60000fe(_1)
  {
    var _2;
    var _3;
    var _4;
    var _5;
    var _6;
    var _7;

    _2 = new Array();
    _5 = _1;
    _6 = 0;
    while ((_6 < _5.length))
    {
      _3 = _5[_6];
      _7 = _3.complete;

      if (!(_7))
      {
        _2.push(_3);
      }

      _6++;
    }
    _4 = _1330e3cc5b78ce34a942b0c73eb79bd6_600003d(_2);
    return _4;
  };

  function _505744091e356e37808e1afa5025f018_60000ff(_1, _2)
  {
    var _3;
    var _4;
    var _5;

    _4 = new _f4c12cb3d41cdf3b81babf91972cf411();
    _4._f4c12cb3d41cdf3b81babf91972cf411_4000077 = _2;
    _3 = new Image();
    _505744091e356e37808e1afa5025f018_6000101(_3, ( (function () { var __value = new _28a9329942a5be30a0ba41ddfe346990(); __value._28a9329942a5be30a0ba41ddfe346990_600004a(_4, '_f4c12cb3d41cdf3b81babf91972cf411_6000115'); return __value; })()  ));
    _3.src = _1;
    _5 = _3;
    return _5;
  };

  function _505744091e356e37808e1afa5025f018_6000100(_1)
  {
    var _2;
    var _3;

    _2 = new Image();
    _2.src = _1;
    _3 = _2;
    return _3;
  };

  function _505744091e356e37808e1afa5025f018_6000101(_, _1)
  {
    _fff5dfbea4be5f309725e9da7675f2d3_6000005(_, 1, _1, 'load');
  };

  function _505744091e356e37808e1afa5025f018_6000102(_, _1)
  {
    _fff5dfbea4be5f309725e9da7675f2d3_6000005(_, 0, _1, 'load');
  };

  function _505744091e356e37808e1afa5025f018_6000103(_, _1)
  {
    _1.background = _51819e5057ac1f3fad6b279ad6c476aa_6000096('url(\x27', _.src, '\x27)');
  };

  function _e67375e7a9d482389f54cf54e52283b4_6000104(_1, _2)
  {
    var _3;
    var _4;

    _3 = _8e8b204c4cc1a735a3a9cf6dd948d6f7_60000e4('EMBED');
    _2.appendChild(_3);
    _3.style.position = 'absolute';
    _84927875577c9239bf3c740da1569016_600000f(_3.style, 0, 0, 0, 0);
    _3.autostart = 1;
    _3.src = _1;
    _4 = _3;
    return _4;
  };

  function _13922dee8795c033a07aed3a1dcf6c33_6000106(_1, _2)
  {
    var _3;
    var _4;

    _3 = document.createElement('input');
    _3.value = _2;
    _3.type = _1;
    _4 = _3;
    return _4;
  };

  function _3082276c948d4a34ba4981e103211c16()
  {
    var __base = new _2674032b7fa8283bb911f8f92f0c144f(); 
    for (var i in __base) if (this[i] == undefined) this[i] = __base[i]; 
    if (this['toString'] == undefined) this['toString'] = __base['toString']; 
  };

  _3082276c948d4a34ba4981e103211c16.prototype._3082276c948d4a34ba4981e103211c16_600010b = function ()
  {
    var _ = this;

    _._2674032b7fa8283bb911f8f92f0c144f_600001a();
  };

  function _3082276c948d4a34ba4981e103211c16_6000109(_1, _2, _3)
  {
    var _4;

    _4 = new _a6aa5b41e919a7398a0b251feb2b57cc();
    _4._a6aa5b41e919a7398a0b251feb2b57cc_4000078 = _1;
    _4._a6aa5b41e919a7398a0b251feb2b57cc_4000079 = _3;
    _84927875577c9239bf3c740da1569016_6000016(_4._a6aa5b41e919a7398a0b251feb2b57cc_4000078.style, 1);
    ( (function () { var __value = new _cd0399f743f47f3d81c79db4953ab92d(); __value._cd0399f743f47f3d81c79db4953ab92d_60000da(( (function () { var __value = new _84e4e1e7fda9783793c35f1edcdc68fd(); __value._84e4e1e7fda9783793c35f1edcdc68fd_600008f(_4, '_a6aa5b41e919a7398a0b251feb2b57cc_6000118'); return __value; })()  )); return __value; })()  )._cd0399f743f47f3d81c79db4953ab92d_60000df(_2);
  };

  function _3082276c948d4a34ba4981e103211c16_600010a(_1, _2, _3, _4)
  {
    var _5;

    _5 = new _e4cf2d06c709c93c994023c166cacd7f();
    _5._e4cf2d06c709c93c994023c166cacd7f_400007c = _1;
    _5._e4cf2d06c709c93c994023c166cacd7f_400007d = _3;
    _5._e4cf2d06c709c93c994023c166cacd7f_400007e = _4;
    ( (function () { var __value = new _cd0399f743f47f3d81c79db4953ab92d(); __value._cd0399f743f47f3d81c79db4953ab92d_60000da(( (function () { var __value = new _84e4e1e7fda9783793c35f1edcdc68fd(); __value._84e4e1e7fda9783793c35f1edcdc68fd_600008f(_5, '_e4cf2d06c709c93c994023c166cacd7f_600011c'); return __value; })()  )); return __value; })()  )._cd0399f743f47f3d81c79db4953ab92d_60000df(_2);
  };

  function _187a7e713247d630b7e9801d5f2a6682() { };
  _187a7e713247d630b7e9801d5f2a6682.prototype._187a7e713247d630b7e9801d5f2a6682_600010e = function ()
  {
    var _ = this;

  };

  function _187a7e713247d630b7e9801d5f2a6682_600010c(e) { var x = []; for (var z in e) x.push(z); return x; };
  function _187a7e713247d630b7e9801d5f2a6682_600010d(_1)
  {
    var _2;
    var _3;
    var _4;
    var _5;
    var _6;
    var _7;
    var _8;

    _2 = _187a7e713247d630b7e9801d5f2a6682_600010c(_1);
    _3 = new Array();
    _6 = _2;
    _7 = 0;
    while ((_7 < _6.length))
    {
      _4 = _6[_7];
      _3.push(_10cbedf29a71b4319eed5a1f52f9e757_6000112(_1, _4));
      _7++;
    }
    _5 = _1330e3cc5b78ce34a942b0c73eb79bd6_600003d(_3);
    return _5;
  };

  function _10cbedf29a71b4319eed5a1f52f9e757() { };
  _10cbedf29a71b4319eed5a1f52f9e757.prototype._10cbedf29a71b4319eed5a1f52f9e757_4000074 = '';
  _10cbedf29a71b4319eed5a1f52f9e757.prototype._10cbedf29a71b4319eed5a1f52f9e757_4000075 = '';
  _10cbedf29a71b4319eed5a1f52f9e757.prototype._10cbedf29a71b4319eed5a1f52f9e757_4000076 = null;
  _10cbedf29a71b4319eed5a1f52f9e757.prototype._10cbedf29a71b4319eed5a1f52f9e757_6000113 = function ()
  {
    var _ = this;

  };

  _10cbedf29a71b4319eed5a1f52f9e757.prototype._10cbedf29a71b4319eed5a1f52f9e757_600010f = function ()
  {
    var _ = this;
    var _1;
    var _2;

    _2 = [];
    _2[0] = '(';
    _2[1] = _._10cbedf29a71b4319eed5a1f52f9e757_4000075;
    _2[2] = ') ';
    _2[3] = _._10cbedf29a71b4319eed5a1f52f9e757_4000074;
    _2[4] = ((_._10cbedf29a71b4319eed5a1f52f9e757_6000111()) ? _51819e5057ac1f3fad6b279ad6c476aa_6000095(' \x3d ', _._10cbedf29a71b4319eed5a1f52f9e757_4000076) : '');
    _1 = _8a2ade548a7da73b9f7737146988ad27_6000066(_51819e5057ac1f3fad6b279ad6c476aa_6000094(_2));
    return _1;
  };

  function _10cbedf29a71b4319eed5a1f52f9e757_6000110(e) { return (typeof e); };
  _10cbedf29a71b4319eed5a1f52f9e757.prototype._10cbedf29a71b4319eed5a1f52f9e757_6000111 = function ()
  {
    var _ = this;
    var _1;

    _1 = _51819e5057ac1f3fad6b279ad6c476aa_600009a(_._10cbedf29a71b4319eed5a1f52f9e757_4000075, 'number');
    return _1;
  };

  function _10cbedf29a71b4319eed5a1f52f9e757_6000112(_1, _2)
  {
    var _3;
    var _4;

    _3 = ( (function () { var __value = new _10cbedf29a71b4319eed5a1f52f9e757(); __value._10cbedf29a71b4319eed5a1f52f9e757_6000113(); return __value; })()  );
    _3._10cbedf29a71b4319eed5a1f52f9e757_4000074 = _2;
    _3._10cbedf29a71b4319eed5a1f52f9e757_4000076 = _db663af156d2293abc8545ac5d254a3a_6000048(_db663af156d2293abc8545ac5d254a3a_6000040(_1), _2);
    _3._10cbedf29a71b4319eed5a1f52f9e757_4000075 = _10cbedf29a71b4319eed5a1f52f9e757_6000110(_3._10cbedf29a71b4319eed5a1f52f9e757_4000076);
    _4 = _3;
    return _4;
  };

  function _f4c12cb3d41cdf3b81babf91972cf411() {  };
  _f4c12cb3d41cdf3b81babf91972cf411.prototype._f4c12cb3d41cdf3b81babf91972cf411_4000077 = null;
  _f4c12cb3d41cdf3b81babf91972cf411.prototype._f4c12cb3d41cdf3b81babf91972cf411_6000115 = function (_1)
  {
    var _ = this;

    ( (function () { var __value = new _cd0399f743f47f3d81c79db4953ab92d(); __value._cd0399f743f47f3d81c79db4953ab92d_60000da(( (function () { var __value = new _84e4e1e7fda9783793c35f1edcdc68fd(); __value._84e4e1e7fda9783793c35f1edcdc68fd_600008f(_, '_f4c12cb3d41cdf3b81babf91972cf411_6000116'); return __value; })()  )); return __value; })()  )._cd0399f743f47f3d81c79db4953ab92d_60000df(100);
  };

  _f4c12cb3d41cdf3b81babf91972cf411.prototype._f4c12cb3d41cdf3b81babf91972cf411_6000116 = function ()
  {
    var _ = this;

    _._f4c12cb3d41cdf3b81babf91972cf411_4000077._ef96c7385900b23fac93aff2ee41b5b3_600001f();
  };

  function _a6aa5b41e919a7398a0b251feb2b57cc() {  };
  _a6aa5b41e919a7398a0b251feb2b57cc.prototype._a6aa5b41e919a7398a0b251feb2b57cc_4000078 = null;
  _a6aa5b41e919a7398a0b251feb2b57cc.prototype._a6aa5b41e919a7398a0b251feb2b57cc_4000079 = 0;
  _a6aa5b41e919a7398a0b251feb2b57cc.prototype._a6aa5b41e919a7398a0b251feb2b57cc_6000118 = function ()
  {
    var _ = this;
    var _1;

    _1 = new _19c4b304994e3a3f96eb31e10684e7ba();
    _1._19c4b304994e3a3f96eb31e10684e7ba_400007a = _;
    _1._19c4b304994e3a3f96eb31e10684e7ba_400007b = null;
    _1._19c4b304994e3a3f96eb31e10684e7ba_400007b = ( (function () { var __value = new _cd0399f743f47f3d81c79db4953ab92d(); __value._cd0399f743f47f3d81c79db4953ab92d_60000da(( (function () { var __value = new _84e4e1e7fda9783793c35f1edcdc68fd(); __value._84e4e1e7fda9783793c35f1edcdc68fd_600008f(_1, '_19c4b304994e3a3f96eb31e10684e7ba_600011a'); return __value; })()  )); return __value; })()  );
    _1._19c4b304994e3a3f96eb31e10684e7ba_400007b._cd0399f743f47f3d81c79db4953ab92d_60000dd((_._a6aa5b41e919a7398a0b251feb2b57cc_4000079 / 25), 25);
  };

  function _19c4b304994e3a3f96eb31e10684e7ba() {  };
  _19c4b304994e3a3f96eb31e10684e7ba.prototype._19c4b304994e3a3f96eb31e10684e7ba_400007a = null;
  _19c4b304994e3a3f96eb31e10684e7ba.prototype._19c4b304994e3a3f96eb31e10684e7ba_400007b = null;
  _19c4b304994e3a3f96eb31e10684e7ba.prototype._19c4b304994e3a3f96eb31e10684e7ba_600011a = function ()
  {
    var _ = this;
    var _1;

    _84927875577c9239bf3c740da1569016_6000016(_._19c4b304994e3a3f96eb31e10684e7ba_400007a._a6aa5b41e919a7398a0b251feb2b57cc_4000078.style, (1 - (_._19c4b304994e3a3f96eb31e10684e7ba_400007b._cd0399f743f47f3d81c79db4953ab92d_400005b / _._19c4b304994e3a3f96eb31e10684e7ba_400007b._cd0399f743f47f3d81c79db4953ab92d_400005d)));
    _1 = !(_._19c4b304994e3a3f96eb31e10684e7ba_400007b._cd0399f743f47f3d81c79db4953ab92d_400005b == _._19c4b304994e3a3f96eb31e10684e7ba_400007b._cd0399f743f47f3d81c79db4953ab92d_400005d);

    if (!(_1))
    {
      _84927875577c9239bf3c740da1569016_6000016(_._19c4b304994e3a3f96eb31e10684e7ba_400007a._a6aa5b41e919a7398a0b251feb2b57cc_4000078.style, 0);
    }

  };

  function _e4cf2d06c709c93c994023c166cacd7f() {  };
  _e4cf2d06c709c93c994023c166cacd7f.prototype._e4cf2d06c709c93c994023c166cacd7f_400007c = null;
  _e4cf2d06c709c93c994023c166cacd7f.prototype._e4cf2d06c709c93c994023c166cacd7f_400007d = 0;
  _e4cf2d06c709c93c994023c166cacd7f.prototype._e4cf2d06c709c93c994023c166cacd7f_400007e = null;
  _e4cf2d06c709c93c994023c166cacd7f.prototype._e4cf2d06c709c93c994023c166cacd7f_600011c = function ()
  {
    var _ = this;
    var _1;

    _1 = new _5715265537680b3088d339ab35c83c5b();
    _1._5715265537680b3088d339ab35c83c5b_400007f = _;
    _1._5715265537680b3088d339ab35c83c5b_4000080 = null;
    _1._5715265537680b3088d339ab35c83c5b_4000080 = ( (function () { var __value = new _cd0399f743f47f3d81c79db4953ab92d(); __value._cd0399f743f47f3d81c79db4953ab92d_60000da(( (function () { var __value = new _84e4e1e7fda9783793c35f1edcdc68fd(); __value._84e4e1e7fda9783793c35f1edcdc68fd_600008f(_1, '_5715265537680b3088d339ab35c83c5b_600011e'); return __value; })()  )); return __value; })()  );
    _1._5715265537680b3088d339ab35c83c5b_4000080._cd0399f743f47f3d81c79db4953ab92d_60000dd((_._e4cf2d06c709c93c994023c166cacd7f_400007d / 25), 25);
  };

  function _5715265537680b3088d339ab35c83c5b() {  };
  _5715265537680b3088d339ab35c83c5b.prototype._5715265537680b3088d339ab35c83c5b_400007f = null;
  _5715265537680b3088d339ab35c83c5b.prototype._5715265537680b3088d339ab35c83c5b_4000080 = null;
  _5715265537680b3088d339ab35c83c5b.prototype._5715265537680b3088d339ab35c83c5b_600011e = function ()
  {
    var _ = this;
    var _1;
    var _2;
    var _3;
    var _4;

    _84927875577c9239bf3c740da1569016_6000016(_._5715265537680b3088d339ab35c83c5b_400007f._e4cf2d06c709c93c994023c166cacd7f_400007c.style, (1 - (_._5715265537680b3088d339ab35c83c5b_4000080._cd0399f743f47f3d81c79db4953ab92d_400005b / _._5715265537680b3088d339ab35c83c5b_4000080._cd0399f743f47f3d81c79db4953ab92d_400005d)));
    _2 = !(_._5715265537680b3088d339ab35c83c5b_4000080._cd0399f743f47f3d81c79db4953ab92d_400005b == _._5715265537680b3088d339ab35c83c5b_4000080._cd0399f743f47f3d81c79db4953ab92d_400005d);

    if (!(_2))
    {
      _8e8b204c4cc1a735a3a9cf6dd948d6f7_60000e1(_._5715265537680b3088d339ab35c83c5b_400007f._e4cf2d06c709c93c994023c166cacd7f_400007c);
      _3 = _._5715265537680b3088d339ab35c83c5b_400007f._e4cf2d06c709c93c994023c166cacd7f_400007e;
      _4 = 0;
      while ((_4 < _3.length))
      {
        _1 = _3[_4];
        _8e8b204c4cc1a735a3a9cf6dd948d6f7_60000e1(_1);
        _4++;
      }
    }

  };


  if (!(_0f5fb9bb58bcbf36ba78442345b5c906_4000056))
  {
    _0f5fb9bb58bcbf36ba78442345b5c906_4000056 = ( (function () { var __value = new _28a9329942a5be30a0ba41ddfe346990(); __value._28a9329942a5be30a0ba41ddfe346990_600004a(null, '_0f5fb9bb58bcbf36ba78442345b5c906_60000c9'); return __value; })()  );
  }

  _0f5fb9bb58bcbf36ba78442345b5c906_4000055 = _0f5fb9bb58bcbf36ba78442345b5c906_4000056;

// created at 20.11.2005 13:24:46
// all rights reserved
