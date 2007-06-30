
        
    var WhereIterator = function WhereIteratorConstructor (_state)
    {
        this._1_state = _state;
    }
    

  
        
    WhereIterator.prototype.GetEnumerator = function ()
    {
        var _ret = null;
        
        if (this._1_state == -2)
        {
            this._1_state = 0;
            _ret = this;
        }
        else
        {
            _ret = new WhereIterator(0);
        }
        

        
        _ret.predicate = this._3_predicate;
        _ret.source = this._3_source;
        
        return _ret;
    }
    
//        public static IEnumerable<T> WhereIterator<T>(IEnumerable<T> source, Func<T, bool> predicate)
//        {
//            foreach (var e in source)
//            {
//                if (predicate(e)) yield return e;
//            }
//        }    
    WhereIterator.prototype.MoveNext = function ()
    {
        switch (this._1_state)
        {
            default:
            case 1:
                return false;
            case 0:
            case 2:
            
                switch (this._1_state)
                {
                    case 0:
                        this._1_state = -1;
                        this._7_wrap = this.source.GetEnumerator();
                        break;
                    case 2:
                        this._1_state = 1;
                        break;
                }
                
                while (this._7_wrap.MoveNext())
                {
                    this._e_5 = this._7_wrap.get_Current();
                    
                    if (!this.predicate(this._e_5))
                        continue;
                    
                     this._2_current = this._e_5;
                    this._1_state = 2;
                
                    return true;
                }
                
                this._1_state = -1;
                
                if (this._7_wrap != null)                
                {
                    // dispose;
                    
                }
                
            return false;
        }
    
    
    
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
                
                if (!this.predicate(this._e_5))
                    continue;
                    
                this._2_current = this._e_5;
                this._1_state = 2;
                
                return true;
            }
            
            this._1_state = -1;
        }
        
        return false;
    }
    
    WhereIterator.prototype.get_Current = function ()
    {
        return this._2_current;
    }
        
    

