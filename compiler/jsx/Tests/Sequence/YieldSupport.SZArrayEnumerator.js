    var SZArrayEnumerator = function SZArrayEnumeratorConstructor (_array)
    {
        this._array = _array;
        this._index = -1;
        this._endIndex = _array.length;
    }


    
    SZArrayEnumerator.prototype.get_Current = function ()
    {
           if (this._index < 0)
                  throw "InvalidOperation_EnumNotStarted";
            if (this._index >= this._endIndex)
                  throw "InvalidOperation_EnumEnded";

        
        return this._array[this._index];
    }
    
    SZArrayEnumerator.prototype.MoveNext = function ()
    {
          if (this._index < this._endIndex)
          {
                this._index++;
                return (this._index < this._endIndex);
          }
          return false;

        
    }
    
