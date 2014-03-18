select TheGridTableLog.ContentReferenceKey
                    , TheGridTable.ContentValue
                    , TheGridTable.ContentComment  
                    , (select count(*) from TheGridTable t2 where t2.ParentContentKey = TheGridTable.ContentKey) as ContentChildren 

                     from 
                     TheGridTableLog
                    , TheGridTable
                     where 
                     TheGridTableLog.ContentReferenceKey = TheGridTable.ContentKey
                     and TheGridTableLog.ContentKey > @FromTransaction /* integer */
                     and TheGridTableLog.ContentKey <= @ToTransaction /* integer */

					and (
					 (TheGridTable.ParentContentKey is null and (@ParentContentKey1 is null or @ParentContentKey3 = ''))
				   or  TheGridTable.ParentContentKey = @ParentContentKey2)

         