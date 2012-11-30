select 

-- ivec3
coalesce(sum(x), 0) as x, 
coalesce(sum(y), 0) as y, 
coalesce(sum(z), 0) as z
 from Delta
 -- interested in entries inserted after last known ticks
 where ticks > @ticks
