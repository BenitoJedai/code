select 
-- we want to merge to our untyped csv dataset that only knows strings now
-- oh and jsc cannot yet serialize typed datatable.. nor typeof, nr generic datatables
cast( delay as text) as delay, 
text 
 from FooTable