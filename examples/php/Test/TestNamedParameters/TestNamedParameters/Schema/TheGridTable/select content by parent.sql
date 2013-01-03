select
        t1.ContentKey
        , t1.ContentValue
        , t1.ContentComment
        , (select count(*) from TheGridTable t2 where t2.ParentContentKey = t1.ContentKey) as ContentChildren 
            from TheGridTable t1

where t1.ParentContentKey = @ParentContentKey
