select 

coalesce(cast(max(ms) as signed), 0) as ms, 

-- CAST(SUM(price) AS SIGNED)
-- ivec3
coalesce(cast(sum(x) as signed), 0) as x, 
coalesce(cast(sum(y) as signed), 0) as y, 

coalesce(cast(sum(goleft) as signed), 0) as goleft,
coalesce(cast(sum(goup) as signed), 0) as goup,
coalesce(cast(sum(goright) as signed), 0) as goright,
coalesce(cast(sum(godown) as signed), 0) as godown

 from SensorySync

 -- interested in entries inserted after last known ticks
 where ms > @ms /* bigint */
