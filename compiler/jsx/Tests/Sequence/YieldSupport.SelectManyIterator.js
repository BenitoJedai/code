
 
    var SelectManyIterator = function SelectManyIteratorConstructor (_state)
    {
        this._1_state = _state;
    }
    

        
    SelectManyIterator.prototype.GetEnumerator = function ()
    {
        var _ret = null;
        
        if (this._1_state == -2)
        {
            this._1_state = 0;
            _ret = this;
        }
        else
        {
            _ret = new SelectManyIterator(0);
        }
        

        
        _ret.source = this._3_source;
        _ret.selector = this._3_selector;
        
        return _ret;
    }
    
    SelectManyIterator.prototype.MoveNext = function ()
    {
        var Label_00A4 = false;
        
        switch (this._1_state)
        {
            case 0:
                break;
            case 3:
                Label_00A4 = true;
                break;
            default:
                return false;
        }
        
        if (!Label_00A4)
        {
            this._1_state = -1;
            this._7_wrap = this.source.GetEnumerator();
            this._1_state = 1;
        }

        while (Label_00A4 || this._7_wrap.MoveNext())
        {
              if (!Label_00A4)
              {        
                  this._e_5 = this._7_wrap.get_Current();
                  this._7_wrap4 = this.selector(this._e_5).GetEnumerator();
                  this._1_state = 2;
              }
              else
              {
                    Label_00A4 = false;
                    this._1_state = 2;
              }
              while (this._7_wrap4.MoveNext())
              {
                    this._e_5_2 = this._7_wrap4.get_Current();
                    this._2_current = this._e_5_2;
                    
                    this._1_state = 3;
                    return true;
              }
              this._1_state = 1;

        }
        this._1_state = -1;

        return false;

    }
    
    SelectManyIterator.prototype.get_Current = function ()
    {
        return this._2_current;
    }
        
    

