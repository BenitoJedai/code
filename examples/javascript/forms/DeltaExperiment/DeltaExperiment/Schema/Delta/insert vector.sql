insert into Delta (ticks, x, y, z) values (
@ticks, 
coalesce( @x, 0), 
coalesce(@y, 0),
coalesce(@z, 0)
)