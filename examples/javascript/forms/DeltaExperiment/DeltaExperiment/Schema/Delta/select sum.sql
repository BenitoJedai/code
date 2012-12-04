select 

coalesce(cast(max(ticks) as signed), 0) as ticks, 

-- CAST(SUM(price) AS SIGNED)
-- ivec3
coalesce(cast(sum(x) as signed), 0) as x, 
coalesce(cast(sum(y) as signed), 0) as y, 
coalesce(cast(sum(z) as signed), 0) as z
 from Delta
 -- interested in entries inserted after last known ticks
 where ticks > @ticks /* bigint */
