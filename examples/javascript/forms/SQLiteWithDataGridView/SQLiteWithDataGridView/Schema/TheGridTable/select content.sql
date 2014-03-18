select t1.ContentKey
        , t1.ContentValue
        , t1.ContentComment
        , (select count(*) from TheGridTable t2 where t2.ParentContentKey = t1.ContentKey) as ContentChildren 
            from TheGridTable t1


-- best way             
where (t1.ParentContentKey is null and (@ParentContentKey1 is null or @ParentContentKey2 = '')) or t1.ParentContentKey = @ParentContentKey3
