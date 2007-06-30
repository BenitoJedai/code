
 
    var MixedIterator = function MixedIteratorConstructor (_state)
    {
        this._1_state = _state;
    }
    

        
    MixedIterator.prototype.GetEnumerator = function ()
    {
        var _ret = null;
        
        if (this._1_state == -2)
        {
            this._1_state = 0;
            _ret = this;
        }
        else
        {
            _ret = new ConcatIterator(0);
        }
        

        
        _ret.a = this._3_a;
        _ret.b = this._3_b;
        
        return _ret;
    }
    
    MixedIterator.prototype.MoveNext = function ()
    {
        var state = this._1_state;
        
        switch (state)
        {
          default:
          case 4:
          case 6:
            return false;
          case 0:
          case 1:
          case 2:
          case 3:
          case 5:
          case 7:
          case 8:
          case 9:
            switch (state)
            {
                case 0:
                    this._1_state = -1;
                    this._2_current = "start";
                    this._1_state = 1;
                    return true;
                
            }
        }        
        return false;
    }
    
    MixedIterator.prototype.get_Current = function ()
    {
        return this._2_current;
    }
        
    

