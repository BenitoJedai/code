insert into AppSnapshot (AppSnapshotContent) values (@AppSnapshotContent /* longtext */)

-- why does mysql not like the cast in insert clause? (cast(foo as longtext))
