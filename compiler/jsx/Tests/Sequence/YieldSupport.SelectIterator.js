
 
    var SelectIterator = function SelectIteratorConstructor (_state)
    {
        this._1_state = _state;
    }
    

        
    SelectIterator.prototype.GetEnumerator = function ()
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
        

        
        _ret.source = this._3_source;
        _ret.selector = this._3_selector;
        
        return _ret;
    }
    
    SelectIterator.prototype.MoveNext = function ()
    {
        if (this._1_state == 0 || this._1_state == 2)
        {
            if (this._1_state == 0)
            {
                this._1_state = -1;
                this._7_wrap = this.source.GetEnumerator();
            }
            
            this._1_state = 1;
            
            while (this._7_wrap.MoveNext())
            {
                this._e_5 = this._7_wrap.get_Current();
 
                this._2_current = this.selector(this._e_5);
                this._1_state = 2;
                
                return true;
            }
            
            this._1_state = -1;
        }
        
        return false;
    }
    
    SelectIterator.prototype.get_Current = function ()
    {
        return this._2_current;
    }
        
    

