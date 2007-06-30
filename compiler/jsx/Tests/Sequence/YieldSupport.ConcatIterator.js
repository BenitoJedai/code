
 
    var ConcatIterator = function ConcatIteratorConstructor (_state)
    {
        this._1_state = _state;
    }
    

        
    ConcatIterator.prototype.GetEnumerator = function ()
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
        

        
        _ret.first = this._3_first;
        _ret.second = this._3_second;
        
        return _ret;
    }
    
    ConcatIterator.prototype.MoveNext = function ()
    {
       
        switch (this._1_state)
        {
            default:
            case 1:
            case 3:
                return false;
            case 0:
            case 2:
            case 4:
                
                switch (this._1_state)
                {
                    case 0:
                    case 2:
                        switch (this._1_state)
                        {
                            case 0:
                                this._1_state = -1;
                                this._7_wrap8 = this.first.GetEnumerator();
                                this._1_state = 1;       
                                break;
                            case 2:
                                this._1_state = 1;       
                                break;
                        }
                        
                        while (this._7_wrap8.MoveNext())
                        {
                            this._e_5_6 = this._7_wrap8.get_Current();
                            
                            this._2_current = this._e_5_6;
                            this._1_state = 2;
                            
                            return true;                                         
                        }
                        
                        this._1_state = -1;    
                        this._7_wrap9 = this.second.GetEnumerator();    
                        this._1_state = 3;   
                        break;
                    case 4:
                        this._1_state = 3;   
                        break;
                }
                
                while (this._7_wrap9.MoveNext())
                {
                    this._e_5_7 = this._7_wrap9.get_Current();
                    
                    this._2_current = this._e_5_7;
                    this._1_state = 4;
                    
                    return true;
                }      
                
                return false;            
        }
        
        throw new Error("NotSupported");
    }
    
    ConcatIterator.prototype.get_Current = function ()
    {
        return this._2_current;
    }
        
    

