
    var Fuzzy = function (
        _op,
        _a,
        _b, 
        _c,
        _before,
        _after,
        _end)
    {
        var d__1 = new FuzzyIterator(-2);
        
        d__1._3__op = _op;
        d__1._3__a = _a;
        d__1._3__b = _b;
        d__1._3__c = _c;
        d__1._3__before = _before;
        d__1._3__after = _after;
        d__1._3__end = _end;
        
        return d__1;
        
    }
    
    var FuzzyIterator = function FuzzyIteratorConstructor (_state)
    {
        this._1_state = _state;
    }

    FuzzyIterator.prototype.GetEnumerator = function ()
    {
        var _ret = null;
        
        if (this._1_state == -2)
        {
            this._1_state = 0;
            _ret = this;
        }
        else
        {
            _ret = new FuzzyIterator(0);
        }
        
        _ret.op = this._3__op;
        _ret.a = this._3__a;
        _ret.b = this._3__b;
        _ret.c = this._3__c;
        _ret.before = this._3__before;
        _ret.after = this._3__after;
        _ret.end = this._3__end;
                
        return _ret;
    }
    
    FuzzyIterator.prototype.MoveNext = function ()
    {
        switch (this._1_state)
        {
            case 0: case 2: case 4: case 6:
            switch (this._1_state)
            {
                case 0: case 2: case 4:
                    switch (this.op)
                    {
                        case 0: case 1:
                        switch (this._1_state)
                        {
                            case 0: case 2:                            
                                switch (this.op)
                                {
                                    case 0:
                                    switch (this._1_state)
                                    {
                                        case 0:
                                            this._1_state = -1;
                                            this._7_wrap1d = this.a.GetEnumerator();
                                            
                                            // init
                                            this._1_state = 1;    
                                            
                                            break;                                            
                                        case 2:                                          
                                            // continue
                                            this._1_state = 1;
                                            
                                            this.after();
                                            break;
                                        default:
                                            throw "_1_state != 2";
                                    }
                                    
                                    while (this._7_wrap1d.MoveNext())
                                    {
                                        this._e_5_19 = this._7_wrap1d.get_Current();
                                        
                                        this.before();
                                        
                                        this._2_current = this._e_5_19;
                                        
                                        // yield
                                        this._1_state = 2;
                                        
                                        return true;
                                    }
                                    break;
                                }
                                
                                this._1_state = -1;
                                this._7_wrap1e = this.b.GetEnumerator();   
                                
                                // init
                                this._1_state = 3;
                                break;
                            case 4:
                                // continue
                                this._1_state = 3;
                                    
                                this.after();   
                                break;
                            default:   
                               throw "_1_state != 4";              
                        }
                        
                        while (this._7_wrap1e.MoveNext())
                        {
                            this._e_5_1a = this._7_wrap1e.get_Current();
                            
                            this.before();
                            
                            this._2_current = this._e_5_1a;
                            
                            // yield
                            this._1_state = 4;
                            
                            return true;
                        }       
                        break;                        
                    }
      
                    this._1_state = -1;    
                    this._v_5_1b = 0;   
                    this._7_wrap1f = this.c.GetEnumerator();  
                    
                    // init
                    this._1_state = 5;    
            
                    break;
                case 6:
                    // continue
                    this._1_state = 5;    
                    
                    this.after();
                    
                    break;
                default:
                    throw "_1_state != 6";
            }
           
            while (this._7_wrap1f.MoveNext())
            {
                this._e_5_1c = this._7_wrap1f.get_Current();
                
                if (this.op % 2 != 0)
                    if (this._v_5_1b++ % 2 == 0)
                        continue;                

                this.before();
                
                this._2_current = this._e_5_1c;
                
                // yield
                this._1_state = 6;
                
                return true;
            }     
            
            this._1_state = -1;                
            this.end();
        }

        return false;
    }
    
    FuzzyIterator.prototype.get_Current = function ()
    {
        return this._2_current;
    }
        
    

