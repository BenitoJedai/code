insert into Delta (ticks, x, y, z) values (
@ticks /* bigint */, 
coalesce(@x /* integer */, 0), 
coalesce(@y /* integer */, 0),
coalesce(@z /* integer */, 0)
)