select AppSnapshotContent 
 from AppSnapshot
where AppSnapshotKey = @AppSnapshotKey /* integer */


-- why does mysql not like the cast in insert clause? (cast(foo as longtext))
